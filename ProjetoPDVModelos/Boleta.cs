using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetoPDVModelos
{
    public class Boleta
    {
        public int numdoc { get; set; }
        public int codcli { get; set; }
        public int numpgto { get; set; }
        public int numseq { get; set; }
        public decimal valor { get; set; }
        public decimal valorpago { get; set; }
        public DateTime datavenc { get; set; }
        public int numboleta { get; set; }
        public int condicao { get; set; }
        
        

        //Construtores
        public Boleta() {
        }

        public Boleta(int numdoc, int codcli, int numseq, int numboleta)
        {
            this.numdoc = numdoc;
            this.codcli = codcli;
            this.numseq = numseq;
            this.numboleta = numboleta;
        }

        public Boleta(int numdoc, int codcli)
        {
            this.numdoc = numdoc;
            this.codcli = codcli;
        }

    }
}
