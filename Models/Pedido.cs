using EspacioEntidades;
using System.Linq;


namespace EspacioEntidades
{
    public enum Estado
    {
        pendiente,
        aceptado,
        cancelado
    }
    public class Pedido
    {
        private int nro;
        private string obs;
        private Cliente cliente;
        private Estado estado;
        private Cadete cadete;

        public int Nro { get => nro; set => nro = value; }
        public string Obs { get => obs; set => obs = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
        public Estado Estado { get => estado; set => estado = value; }
        public Cadete Cadete { get => cadete; set => cadete = value; }

        public Pedido(int nro, string obs, Cliente cliente)
        {
            this.Nro = nro;
            this.Obs = obs;
            this.Cliente = cliente;
            this.Estado = Estado.pendiente;
            // this.cadete = cadete;
        }




        //metodos

        public string verDatosCliente()
        {
            return Cliente.Nombre + "-" + Cliente.Telefono
             + "-" + Cliente.Direccion + "-" + Cliente.DatosReferenciaDireccion; ;
        }


        public string verDireccionCliente()
        {
            return Cliente.Direccion;
        }

        public void AceptarPedido()
        {
            Estado = Estado.aceptado;
            Cadete.CantPedidos++;
        }
        public void CancelarPedido()
        {
            Estado = Estado.cancelado;
            Cadete.CantPedidos--;
        }

        public void AsignarCadete(Cadete cadete)
        {
            Cadete=cadete;
            AceptarPedido();
        }
        public void DesasignarCadete()
        {
            CancelarPedido();
            Cadete=null;
        }
    }


}