using System;
using System.Collections.Generic;


namespace Administracion.ViewModels
{
    public class CheckModel
    {
        public int IdEmpleado { get; set; }
        public int Documento { get; set; }
        public string NombreCompleto { get; set; }
        public string Trabajo { get; set; }
        public bool Checkeado { get; set; }
    }
}