using Ejercicio1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ejercicio1.ViewModels
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