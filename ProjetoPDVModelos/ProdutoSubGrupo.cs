using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetoPDVModelos
{
    public class ProdutoSubGrupo
    {
        public int codSub { get; set; }
        public int codGrupo { get; set; }
        public string descSub { get; set; }
        public string ncm { get; set; }
        public string cest { get; set; }
        public int st { get; set; }
        public decimal picms { get; set; }
        public decimal pis { get; set; }
        public decimal cofins { get; set; }
        public decimal csll { get; set; }
        public decimal irpj { get; set; }

    }
}   