using System.ComponentModel;
using EspacioEntidades;
using System.Linq;
using cadeteriaAPI;
using EspacioAccesoData;

namespace EspacioEntidades
{
    public class Cadeteria
    {
        private string nombre;
        private string telefono;
        private List<Cadete> listadoCadetes;
        private List<Pedido> listadoPedidos;
        private int contadorPedidos;

        // AccesoJson helper ;
        private static Cadeteria instancia;

        public static Cadeteria GetInstancia()
        {
            if (instancia == null)
            {
                AccesoJson helper = new AccesoJson();
                instancia = helper.getCadeteria("datos/cadeteria.json");
                instancia.listadoCadetes=helper.getCadetes("datos/Cadetes.json");
                // List<Cadete> cadetes = helper.getCadetes("datos/Cadetes.json");
            }
            return instancia;
        }
        public Cadeteria(string nombre, string telefono)
        {
            Nombre = nombre;
            Telefono = telefono;
            ListadoCadetes = new List<Cadete>();
            ListadoPedidos = new List<Pedido>();
            contadorPedidos=0;
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }
        public List<Pedido> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }

        // public Pedido crearPedido(string nombre, string direc, double telefono, string referencias, string obs)
        // {
        //     Pedido nuevoPedido = new Pedido(nombre, direc, telefono, referencias, obs);
        //     contadorPedidos++;
        //     nuevoPedido.Nro=contadorPedidos+1;
        //     ListadoPedidos.Add(nuevoPedido);
        //     return nuevoPedido;
        // // }
        // public Pedido crearPedido(Pedido pedido)
        // {
        //     contadorPedidos++;
        //     pedido.Nro=contadorPedidos+1;
        //     ListadoPedidos.Add(pedido);
        //     return pedido;
        // }
        public bool AgregarPedido(Pedido nuevoPedido)
        {
            if(nuevoPedido==null) return false;
            contadorPedidos++;
            nuevoPedido.Nro=contadorPedidos;
            listadoPedidos.Add(nuevoPedido);
            return true;
            // return listadoPedidos.FirstOrDefault(ped => ped == nuevoPedido, null) != null;
        }

        public void AsignarCadeteAPedido(int idCadete, int numeroPedido) //asigna un cadete a un pedido ( tp3)
        {
            // Cadete cadete = this.listadoCadetes.First(cadete => cadete.Id == id);
            Cadete cadete = BuscarporCadetePorId(idCadete);
            if(cadete!=null){
                Pedido pedido = BuscarporPedidoPorNumero(numeroPedido);
                if (pedido !=null)
                {
                    pedido.AsignarCadete(cadete);
                }
            }
            else{

            }
        }
        
        public void ReasignarCadete(int idCadeteNuevo, int numeroPedido)
        {
            // Pedido pedido= .FirstOrDefault(pedido => cadete.Id == idCadete2);
            Pedido aux = BuscarporPedidoPorNumero(numeroPedido);
            aux.DesasignarCadete();
            aux.AsignarCadete(BuscarporCadetePorId(idCadeteNuevo));
        }
        public void CambiarEstado(int numeroPedido, int estado)
        {
                foreach (var item in ListadoPedidos)
                {
                    if (item.Nro == numeroPedido)
                    {
                        if (estado == (int)Estado.cancelado)
                        {
                            
                            item.AceptarPedido();
                        }
                        else
                        {
                            item.CancelarPedido();
                        }
                    }
                }
        }
        public Cadete BuscarporCadetePorId(int id)
        {
            Cadete cadete = listadoCadetes.FirstOrDefault(cadete => cadete.Id == id);
            return cadete;
        }
        public Pedido BuscarporPedidoPorNumero(int nPedido)
        {
            Pedido pedido = listadoPedidos.FirstOrDefault(pedido => pedido.Nro == nPedido);
            return pedido;
        }

        public double JornalACobrar(int idCadete)
        {
            // int contador = 0;
            Cadete cadete = BuscarporCadetePorId(idCadete); 
            if (cadete!=null)
            {
                return 500 * cadete.CantPedidos;  
            }
            else{
                return -1;
            }
        }

        public string GetInforme()
        {
            double montoTotal = 0;
            int totalEnvios = 0;
            string informe = "Informe de Pedidos:\n";

            foreach (Cadete cadete in listadoCadetes)
            {
                int enviosCadete = cadete.CantPedidos;
                double montoCadete = JornalACobrar(cadete.Id);

                informe += $"Cadete: {cadete.Nombre}\n";
                informe += $"Cantidad de Envíos: {enviosCadete}\n";
                informe += $"Monto Ganado: ${montoCadete}\n\n";

                totalEnvios += enviosCadete;
                montoTotal += montoCadete;
            }

            double promedioEnviosPorCadete = (double)totalEnvios / listadoCadetes.Count;

            informe += "Resumen General:\n";
            informe += $"Total de Envíos: {totalEnvios}\n";
            informe += $"Monto Total Ganado: ${montoTotal}\n";
            informe += $"Cantidad Promedio de Envíos por Cadete: {promedioEnviosPorCadete:F2}";

            return informe;
        }

    }
}