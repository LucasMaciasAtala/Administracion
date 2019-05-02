using System.Collections.Generic;

namespace Administracion.ViewModels
{
    public class EmpleadosVM
    {
        public int IdResto { get; set; }

        public string NombreResto { get; set; }

        public bool ManagerValido { get; set; }

        public List<CheckModel> Contratados { get; set; }

        public List<CheckModel> AContratar { get; set; }

        public EmpleadosVM()
        {
            Contratados = new List<CheckModel>();
            AContratar = new List<CheckModel>();
        }
    }
}