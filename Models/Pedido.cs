using EspacioEntidades;
using System.Linq;

enum Estado
{
    pendiente,
    aceptado,
    cancelado
}

namespace EspacioEntidades
{
    public class Pedido
    {
        private int nro;
        private Cliente cliente;
        private string obs;
        private Estado estado;
        private Cadete cadete;


        public int Nro { get => nro; set => nro = value; }
        public string Obs { get => obs; set => obs = value; }
        private Cliente Cliente { get => cliente; set => cliente = value; }
        internal Estado Estado { get => estado; set => estado = value; }
        public Cadete Cadete { get => cadete; set => cadete = value; }




        public Pedido(string nombre, string direccion, double telefono, string datosReferenciaDireccion, string obs)
        {
            this.nro=0;
            this.estado = Estado.pendiente;
            cliente = new Cliente(nombre, direccion, telefono, datosReferenciaDireccion);//esto es porque es composicion
            this.Obs = obs;
            cadete = new Cadete();
        }

        public Pedido()
        {
            this.nro=0;
            // this.estado = Estado.pendiente;
            // cliente = new Cliente();//esto es porque es composicion
            // this.Obs = "";
            // cadete = new Cadete();
        }
        //metodos

        public string verDatosCliente()
        {
            return cliente.Nombre + "-" + cliente.Telefono
             + "-" + Cliente.Direccion + "-" + Cliente.DatosReferenciaDireccion; ;
        }


        public string verDireccionCliente()
        {
            return cliente.Direccion;
        }

        public void AceptarPedido()
        {
            estado = Estado.aceptado;
            cadete.CantPedidos++;
        }
        public void CancelarPedido()
        {
            estado = Estado.cancelado;
            cadete.CantPedidos--;
        }

        public void AsignarCadete(Cadete cadete)
        {
            this.Cadete=cadete;
            AceptarPedido();
        }
        public void DesasignarCadete()
        {
            CancelarPedido();
            this.Cadete=null;
        }
    }


}