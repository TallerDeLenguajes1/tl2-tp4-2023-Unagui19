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
        private static int contadorPedidos;

        // AccesoJson helper ;
        private static Cadeteria cadeteria;

        public static Cadeteria GetCadeteria()
        {
            if (cadeteria == null)
            {
                AccesoJson helper = new AccesoJson();
                cadeteria = helper.getCadeteria("datos/cadeteria.json");
                cadeteria.listadoCadetes=helper.getCadetes("datos/Cadetes.json");
                // List<Cadete> cadetes = helper.getCadetes("datos/Cadetes.json");
            }
            return cadeteria;
        }
        public Cadeteria(string nombre, string telefono)
        {
            this.Nombre = nombre;
            this.Telefono = telefono;
            this.ListadoCadetes = new List<Cadete>();
            this.ListadoPedidos = new List<Pedido>();
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }
        public List<Pedido> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }

        public Pedido crearPedido(string nombre, string direc, double telefono, string referencias, string obs)
        {
            Pedido nuevoPedido = new Pedido(nombre, direc, telefono, referencias, obs);
            contadorPedidos++;
            nuevoPedido.Nro=contadorPedidos+1;
            ListadoPedidos.Add(nuevoPedido);
            return nuevoPedido;
        }
        public Pedido crearPedido()
        {
            Pedido nuevoPedido = new Pedido();
            contadorPedidos++;
            nuevoPedido.Nro=contadorPedidos+1;
            ListadoPedidos.Add(nuevoPedido);
            return nuevoPedido;
        }
        public bool AgregarPedido(Pedido pedido)
        {
            pedido.Nro++;
            this.listadoPedidos.Add(pedido);
            return listadoPedidos.FirstOrDefault(ped => ped == pedido, null) != null;
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
            Cadete cadete = this.listadoCadetes.FirstOrDefault(cadete => cadete.Id == id);
            return cadete;
        }
        public Pedido BuscarporPedidoPorNumero(int nPedido)
        {
            Pedido pedido = this.listadoPedidos.FirstOrDefault(pedido => pedido.Nro == nPedido);
            return pedido;
        }

        public double jornalACobrar(int idCadete)
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

    }
}