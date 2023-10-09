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
        public int nro;
        public string obs;
        public Cliente cliente;
        public Estado estado;
        public Cadete cadete;


        public int Nro { get => nro; set => nro = value; }
        public string Obs { get => obs; set => obs = value; }
        internal Estado Estado { get => estado; set => estado = value; }
        private Cliente Cliente { get => cliente; set => cliente = value; }
        public Cadete Cadete { get => cadete; set => cadete = value; }
        public Pedido(int nro, string obs, Cliente cliente)
        {
            this.nro = nro;
            this.obs = obs;
            this.cliente = cliente;
            this.estado = Estado.pendiente;
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