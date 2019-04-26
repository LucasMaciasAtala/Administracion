using System.ComponentModel.DataAnnotations;

namespace Ejercicio1.Models
{
    public abstract class PersonaBase : Entidad
    {
        [Required(ErrorMessage = "Por favor ingrese un nombre")]
        [StringLength(20, ErrorMessage = "El nombre puede contener solo {1} caracteres.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "Por favor ingrese un apellido")]
        [StringLength(20, ErrorMessage = "El apellido puede contener solo {1} caracteres.")]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }


        [Required(ErrorMessage = "Por favor ingrese un documento")]
        [Range(1000000, 99999999, ErrorMessage = "El documento debe encontrarse entre {1}  y {2}")]
        [Display(Name = "Documento")]
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
