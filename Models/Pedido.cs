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




        // public Pedido(string nombre, string direccion, string telefono, string datosReferenciaDireccion, string obs)
        // {
        //     this.nro=0;
        //     this.estado = Estado.pendiente;
        //     cliente = new Cliente(nombre, direccion, telefono, datosReferenciaDireccion);//esto es porque es composicion
        //     this.Obs = obs;
        //     cadete = new Cadete();
        // }
        
        // public Pedido(int numero,string observacion,Cliente client)
        // {
        //     Nro=numero;
        //     Obs = observacion;
        //     Estado = Estado.pendiente;
        //     Cliente = client;//esto es porque es composicion
        //     // cadete = new Cadete();
        // }
        //     public Pedido()
        // {
        //     Nro=0;
        //     // this.estado = Estado.pendiente;
        //     // cliente = new Cliente();//esto es porque es composicion
        //     // this.Obs = "";
        //     // cadete = new Cadete();
        // }

        // public Pedido()
        // {
        //     this.nro=0;
        //     // this.estado = Estado.pendiente;
        //     // cliente = new Cliente();//esto es porque es composicion
        //     // this.Obs = "";
        //     // cadete = new Cadete();
        // }
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