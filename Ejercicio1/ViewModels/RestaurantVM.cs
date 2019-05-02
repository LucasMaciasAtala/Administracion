using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Administracion.ViewModels
{
    public class RestaurantVM : ConstruccionBaseVM
    {
        private const string _nombreLength = "50";
        public List<PersonaBase> Empleados { get; set; }

        [DisplayFormat(NullDisplayText = "Sin Encargado")]
        public string ManagerNombreCompleto { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Nombre supera la longitud permitida (" + _nombreLength + ")")]
        public string Nombre { get; set; }

        public int IdEncargado { get; set; }

        public Encargado ModeloEncargado { get; set; }//Para <th>

        public RestaurantVM(Restaurant resto)
        {
            Direccion = resto.Direccion;
            Empleados = resto.Empleados;

            if (resto.Manager != null)
            {
                ManagerNombreCompleto = resto.Manager.NombreCompleto;
                IdEncargado = resto.Manager.Id;
            }

            Nombre = resto.Nombre;
            Id = resto.Id;
        }

        public Restaurant ObtenerRestaurantSinEncargado()
        {
            var resto = new Restaurant();
            resto.Id = Id;
            resto.Nombre = Nombre;
            resto.Direccion = Direccion;
            resto.Empleados = Empleados;
            return resto;
        }
    }
}