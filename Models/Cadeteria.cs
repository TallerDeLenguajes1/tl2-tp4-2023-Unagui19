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
        private static Cadeteria inicializado;

        public static Cadeteria GetCadeteriaSingleton()
        {
            if (inicializado == null)
            {
                AccesoData helper = new AccesoJson();
                Cadeteria cadeteria = helper.getCadeteria("cadeteria.json");
                List<Cadete> cadetes = helper.getCadetes("Cadetes.json");
            }
            return inicializado;
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
            ListadoPedidos.Add(nuevoPedido);
            return nuevoPedido;
        }

        public void AsignarCadeteAPedido(int id, int numeroPedido) //asigna un cadete a un pedido ( tp3)
        {
            // Cadete cadete = this.listadoCadetes.First(cadete => cadete.Id == id);
            Cadete cadete = BuscarporCadetePorId(id);
            Pedido pedido = BuscarporPedidoPorNumero(numeroPedido);
            pedido.AsignarCadete(cadete);
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