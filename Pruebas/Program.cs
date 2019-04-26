using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pruebas
{
    class Program
    {
        public void Calcular(Func<int> lala)
        {

        }

        static void Main(string[] args)
        {
            var program = new Program();


            program.Calcular(delegate ()
            {
                return 0;
            });

            program.Calcular(() => 0);
        }

        public int MiMetodo()
        {
            return 0;
        }
    }
}
