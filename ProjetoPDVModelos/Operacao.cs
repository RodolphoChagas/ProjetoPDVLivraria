using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetoPDVModelos
{
    public class Operacao
    {
        public int codoperacao { get; set; }
        public string nome { get; set; }
        public int intinic { get; set; }
        public int intervalo { get; set; }
        public int numpagto { get; set; }
        public int cns { get; set; }
        public int prm { get; set; }
        public int ava { get; set; }
        public int ent { get; set; }
        public int tro { get; set; }
        public int dif { get; set; }
        public int STQ { get; set; }
        public int vnd { get; set; }
        public int cffe { get; set; }
        public int cfde { get; set; }
        public string descfiscal { get; set; }
        public string devolucao { get; set; }
        public string TV { get; set; }
        public string nf { get; set; }

        //Construtores
        public Operacao() { 
        }

        public Operacao(int codOperacao)
        {
            this.codoperacao = codOperacao;
        }


    }
}
