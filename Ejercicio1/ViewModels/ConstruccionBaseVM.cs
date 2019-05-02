using System.ComponentModel.DataAnnotations;

namespace Administracion.ViewModels
{
    public abstract class ConstruccionBaseVM : Models.Entidad
    {
        [Required]
        [StringLength(50, ErrorMessage ="Dirección supera la longitud permitida(50 caracteres)")]
        public string Direccion { get; set; }
    }
}