using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetoPDVModelos
{
    public class Produto_Loja
    {
        public int codpro { get; set; }
        public int codloja { get; set; }
        public int desconto { get; set; }
        public int site { get; set; }
        public int estatus { get; set; }

        public Produto_Loja() 
        {
        }
    }
}
