using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tptarjeta
{
    public class ColectivoInterurbano : Colectivo
    {
        private const decimal tarifaInterurbana = 2500;

        public override decimal Tarifa => tarifaInterurbana;
    }
}
