using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;


namespace ProjetoPDVModelos
{
    [TableName("MovItens")]
    public class PedidoItem
    {
        public int numdoc { get; set; }
        [Ignore]
        public Pedido pedido { get; set; }

        public int codpro { get; set; }
        [Ignore]
        public Produto produto { get; set; }
        
        public int qtditens { get; set; }
        public double dscitens { get; set; }
        public decimal prcitens { get; set; }
        public decimal valitens { get; set; }

        [Ignore]
        public decimal valDesc { get; set; }


        //[Ignore]
        //public int qtditens_Acumulado { get; set; }




        public PedidoItem(Pedido p, Produto produto, int codpro, int qtditens, double dscitens, decimal valdesc, decimal prcitens, decimal valitens)
        {
            this.pedido = p;
            this.produto = produto;
            this.codpro = codpro;
            this.qtditens = qtditens;
            this.dscitens = dscitens;
            this.valDesc = valdesc;
            this.prcitens = prcitens;
            this.valitens = valitens;
        }

        //Construtores
        public PedidoItem()
        { 
        }
    }
}
