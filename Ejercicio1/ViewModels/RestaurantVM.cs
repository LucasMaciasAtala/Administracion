using Ejercicio1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ejercicio1.ViewModels
{
    public class RestaurantVM : ConstruccionBaseVM
    {
        private const string _nombreLength = "50";
        public List<PersonaBase> Empleados { get; set; }

        [DisplayFormat(NullDisplayText = "Sin Manager")]
        public string ManagerNombreCompleto { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Nombre supera la longitud permitida (" + _nombreLength + ")")]
        public string Nombre { get; set; }

        public RestaurantVM(Restaurant resto)
        {
            Direccion = resto.Direccion;
            Empleados = resto.Empleados;

            if (resto.Manager != null)
            {
                ManagerNombreCompleto = resto.Manager.NombreCompleto;
            }

            Nombre = resto.Nombre;
            Id = resto.Id;
        }
    }
}