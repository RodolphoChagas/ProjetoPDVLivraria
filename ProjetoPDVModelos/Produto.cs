using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace ProjetoPDVModelos
{
    public class Produto
    {
        public int codpro { get; set; }
        public string descricao { get; set; }
        public int peso { get; set; }
        public int estoque { get; set; }
        public string isbn { get; set; }
        public decimal prcvenda { get; set; }
        public decimal prccusto { get; set; }
        public Produto_Loja produto_loja { get; set; }

        [Column("tipoproduto")]
        public int codgrupo { get; set; }
        public int codsubGrupo { get; set; }
        public ProdutoSubGrupo subGrupo { get; set; }

        //Construtores
        public Produto() { }

    }
}