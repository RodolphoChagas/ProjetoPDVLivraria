using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetoPDVModelos
{
    public class Convenio
    {
        public int codcon { get; set; }
        public string desccon { get; set; }
        public int desconto { get; set; }
        public int status { get; set; }


        public Convenio() { }

        public Convenio(int codcon, string desccon)
        {
            this.codcon = codcon;
            this.desccon = desccon;
        }
    }
}
