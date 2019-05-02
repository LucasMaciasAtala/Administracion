namespace Modelos
{
    public abstract class PersonaBase : Entidad
    {
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public int Documento { get; set; }

        public string Trabajo
        {
            get
            {
                return ObtenerTipo().ToString();
            }
        }

        public string NombreCompleto
        {
            get { return Nombre + " " + Apellido; }
        }

        public EnumTipoTrabajo ObtenerTipo()
        {
            var tipo = this.GetType();
            if (tipo == typeof(Cocinero))
            {
                return EnumTipoTrabajo.Cocinero;
            }
            else if (tipo == typeof(Mesero))
            {
                return EnumTipoTrabajo.Mesero;
            }
            else if (tipo == typeof(Encargado))
            {
                return EnumTipoTrabajo.Encargado;
            }
            throw new System.Exception("Tipo de persona inválido");
        }
    }
}
