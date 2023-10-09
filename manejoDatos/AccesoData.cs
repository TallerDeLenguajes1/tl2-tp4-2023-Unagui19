using EspacioEntidades;
using System.Security.AccessControl;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EspacioAccesoData
{
    public abstract class AccesoData
    {
        public virtual List<Cadete> getCadetes(string path){return null;}
        public virtual Cadeteria getCadeteria(string path){return null;}        
        public virtual List<Pedido> GetPedidos(){return null;}        
    }



    public class AccesoJson:AccesoData
    {

        public string leerArchivo(string path)
        {
            string? documento;
            using (var fs = new FileStream(path,FileMode.Open)){
                //abrir el archivo
                using(var sr = new StreamReader(fs)){// para leer
                    documento= sr.ReadToEnd(); 
                    // Console.WriteLine(documento);
                    sr.Close();
                }
                fs.Close();
            }           
            return documento; 
        }

        public override Cadeteria getCadeteria(string path)
        {
            string documento=leerArchivo(path);
            Cadeteria cadeteria= JsonSerializer.Deserialize<Cadeteria>(documento); 
            // Console.WriteLine($"{cadeteria.Nombre},{cadeteria.Telefono},{cadeteria.Code}");
            return cadeteria; 
        }


        public override List<Cadete> getCadetes(string path)
        {
            string documento=leerArchivo(path);
            List<Cadete> cadetes= JsonSerializer.Deserialize<List<Cadete>>(documento); 
            // Console.WriteLine($"{cadeteria.Nombre},{cadeteria.Telefono},{cadeteria.Code}");
            return cadetes; 
        }

    }


    public class AccesoADatosCadeteria:AccesoJson
    {
        public Cadeteria Obtener()
        {
            return getCadeteria("datos/cadeteria.json");
        }

    }

    public class AccesoADatosCadetes:AccesoJson
    {
        public List<Cadete> Obtener()
        {
            return getCadetes("datos/Cadetes.json");
        }

    }

    public class AccesoADatosPedidos:AccesoJson
    {
        public void guardarPedidos(List<Pedido> pedidos)
        {    
            var fst = new FileStream("datos/pedidos.json",FileMode.OpenOrCreate);
            var options = new JsonSerializerOptions { WriteIndented = true };
            string archivoJson = JsonSerializer.Serialize(pedidos,options);
            using (var sw =new StreamWriter(fst))
            {
                sw.WriteLine(archivoJson);
                sw.Close();
            }//PARA CREAR UN JSON y guardar o solo guardar 
            fst.Close();
        }
        public List<Pedido> Obtener()
        {
            string documento=leerArchivo("datos/pedidos.json");
            List<Pedido> pedidos= JsonSerializer.Deserialize<List<Pedido>>(documento); 
            // Console.WriteLine($"{cadeteria.Nombre},{cadeteria.Telefono},{cadeteria.Code}");
            return pedidos; 
        }

    }




    public class AccesoCSV:AccesoData
    {
        public override Cadeteria getCadeteria(string path)
        {
            Cadeteria cadeteria=new Cadeteria("","");
            var csv = new FileStream(path, FileMode.Open);//abrir el archivo
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

        public override List<Cadete> getCadetes(string path)
        {
            List<Cadete> cadetes = new List<Cadete>();
            var csv = new FileStream(path, FileMode.Open);//abrir el archivo
            var sr = new StreamReader(csv);// para leer
            while (!sr.EndOfStream)
            {
                string linea = sr.ReadLine();
                string[] fields = linea.Split(',');//para leer cada uno de los elementos hasta el ,
                // documento.Add(fields);            // string agregar = string.Join(";","cebolla");
                cadetes.Add(new Cadete(int.Parse(fields[0]), fields[1], fields[2], fields[3]));
            }
            csv.Close();
            return cadetes;
        }
        // public void GenerarInforme(Cadeteria cadeteria)//genera un archivo .csv
        // {
        //     FileStream fs = new FileStream("Informe.csv", FileMode.Create);
        //     using (StreamWriter writer = new StreamWriter(fs))
        //     {
        //         int i = 0, cantPedidosTotal = 0;
        //         double sumador = 0;
        //         // writer.WriteLine("Indice"+" "+"Nombre"+" "+"Extension");

        //         writer.WriteLine($"Cadeteria {cadeteria.Nombre} || telefono: {cadeteria.Telefono}\n");
        //         foreach (var cadete in cadeteria.ListadoCadetes)
        //         {

        //             writer.WriteLine($"{cadete.Id}; Nombre: {cadete.Nombre}; monto:{cadeteria.jornalACobrar(cadete.Id)}; Cantidad de pedidos: {cadete.CantPedidos}");
        //             i++;
        //             sumador += cadeteria.jornalACobrar(cadete.Id);
        //             cantPedidosTotal += cadete.CantPedidos;
        //         }

        //         writer.WriteLine("");
        //         writer.WriteLine("Monto total: " + sumador);
        //         writer.WriteLine("Cantidad total de pedidos: " + cantPedidosTotal);

        //         writer.WriteLine("Promedio de pedidos por cadete: " + (cantPedidosTotal / cadeteria.ListadoCadetes.Count()));
        //     }
        // }
    }
}




// public string GenerarInforme()
// {
//     string cuerpo = "";

//     cuerpo = cadete.Nombre + "; " + cadete.jornalACobrar() + "; " + cadete.ListadoPedidos.Count();

//     return cuerpo;
// }



