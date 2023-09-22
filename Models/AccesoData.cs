using EspacioEntidades;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace EspacioAccesoData
{
    public abstract class AccesoDato
    {
        public abstract List<Cadete> GetCadetes();
        public abstract Cadeteria getCadeteria();
    }



    public class AccesoJson:AccesoDato
    {
        public override Cadeteria getCadeteria()
        {

                string textoJson = File.ReadAllText("Cadeteria.Json");
                Cadeteria cadeteria = JsonSerializer.Deserialize<Cadeteria>(textoJson);
                return cadeteria;
        }

        public override List<Cadete> GetCadetes()
        {
             string textoJson = File.ReadAllText("Cadetes.Json");
            List<Cadete> nuevaLista = JsonSerializer.Deserialize<List<Cadete>>(textoJson);
            return nuevaLista;
        }
    }



    public class AccesoCSV:AccesoDato
    {
        public override Cadeteria getCadeteria()
        {
            Cadeteria cadeteria=new Cadeteria("","");
            var csv = new FileStream("Cadeteria.CSV", FileMode.Open);//abrir el archivo
            var sr = new StreamReader(csv);// para leer
            while (!sr.EndOfStream)
            {
                string linea = sr.ReadLine();
                string[] fields = linea.Split(',');//para leer cada uno de los elementos hasta la ","
                
                Cadeteria cadeteria1=new Cadeteria(fields[0], fields[1]);
                cadeteria=cadeteria1;
            }
            csv.Close();
            return (cadeteria);
        }

        public override List<Cadete> GetCadetes()
        {
            List<Cadete> cadetes = new List<Cadete>();
            var csv = new FileStream("Cadetes.CSV", FileMode.Open);//abrir el archivo
            var sr = new StreamReader(csv);// para leer
            while (!sr.EndOfStream)
            {
                string linea = sr.ReadLine();
                string[] fields = linea.Split(',');//para leer cada uno de los elementos hasta el ,
                // documento.Add(fields);            // string agregar = string.Join(";","cebolla");
                cadetes.Add(new Cadete(int.Parse(fields[0]), fields[1], fields[2], double.Parse(fields[3])));
            }
            csv.Close();
            return cadetes;
        }
        public void GenerarInforme(Cadeteria cadeteria)//genera un archivo .csv
        {
            FileStream fs = new FileStream("Informe.csv", FileMode.Create);
            using (StreamWriter writer = new StreamWriter(fs))
            {
                int i = 0, cantPedidosTotal = 0;
                double sumador = 0;
                // writer.WriteLine("Indice"+" "+"Nombre"+" "+"Extension");

                writer.WriteLine($"Cadeteria {cadeteria.Nombre} || telefono: {cadeteria.Telefono}\n");
                foreach (var cadete in cadeteria.ListadoCadetes)
                {

                    writer.WriteLine($"{cadete.Id}; Nombre: {cadete.Nombre}; monto:{cadeteria.jornalACobrar(cadete.Id)}; Cantidad de pedidos: {cadete.CantPedidos}");
                    i++;
                    sumador += cadeteria.jornalACobrar(cadete.Id);
                    cantPedidosTotal += cadete.CantPedidos;
                }

                writer.WriteLine("");
                writer.WriteLine("Monto total: " + sumador);
                writer.WriteLine("Cantidad total de pedidos: " + cantPedidosTotal);

                writer.WriteLine("Promedio de pedidos por cadete: " + (cantPedidosTotal / cadeteria.ListadoCadetes.Count()));
            }
        }
    }

}



// public string GenerarInforme()
// {
//     string cuerpo = "";

//     cuerpo = cadete.Nombre + "; " + cadete.jornalACobrar() + "; " + cadete.ListadoPedidos.Count();

//     return cuerpo;
// }



