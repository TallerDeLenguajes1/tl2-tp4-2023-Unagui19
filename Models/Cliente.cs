namespace EspacioEntidades
{
    class Cliente
    {
        private string nombre;
        private string direccion;
        private double telefono;
        private string datosReferenciaDireccion;


        public Cliente(string nombre, string direccion, double telefono, string datosReferenciaDireccion)
        {
            this.Nombre = nombre;
            this.Direccion = direccion;
            this.Telefono = telefono;
            this.DatosReferenciaDireccion = datosReferenciaDireccion;
        }

        public Cliente()
        {
            
        }


        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public double Telefono { get => telefono; set => telefono = value; }
        public string DatosReferenciaDireccion { get => datosReferenciaDireccion; set => datosReferenciaDireccion = value; }
    }
}