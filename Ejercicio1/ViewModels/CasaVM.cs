using Models;
using System.ComponentModel.DataAnnotations;

namespace Administracion.ViewModels
{
    public class CasaVM : ConstruccionBaseVM
    {
        [Required]
        [StringLength(200, ErrorMessage = "Descripción supera la longitu permitida (200 caracertes)")]
        public string Descripcion { get; set; }

        [DisplayFormat(NullDisplayText = "Sin Propietario")]
        public string NombreCompletoPropietario { get; set; }

        public CasaVM(Casa casa)
        {
            Descripcion = casa.Descripcion;
            Direccion = casa.Direccion;
            Id = casa.Id;

            if (casa.Propietario != null)
            {
                NombreCompletoPropietario = casa.Propietario.NombreCompleto;
            }
        }
    }
}