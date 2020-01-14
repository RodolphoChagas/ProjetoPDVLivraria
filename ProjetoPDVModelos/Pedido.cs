using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace ProjetoPDVModelos
{
    [TableName("Movdb")]
    [PrimaryKey("NumDoc")]
    public class Pedido
    {

        public int numdoc { get;  set; }

        public int codcli { get; set; }
        [Ignore]
        public Cliente cliente { get; set; }

        public int codoperacao { get; set; }
        [Ignore]
        public Operacao operacao { get; set; }
        
        [Ignore]
        public XML xml { get; set; }

        [Column("tipopgto")]
        public int codtipopgto { get; set; }
        [Ignore]
        public TipoPagamento tipoPgto { get; set; }

        public int codvendedor { get; set; }
        public int coduser { get; set; }
        public string nfiscal { get; set; }
        public string conddoc { get; set; }
        public decimal valdoc { get; set; }
        public double valfrete { get; set; }
        public string cndfrete { get; set; }
        public DateTime datadigitacao { get; set; }
        public DateTime datavencimento { get; set; }
        public DateTime datanfiscal { get; set; }
        public decimal dscdoc { get; set; }
        public decimal valdsc { get; set; }
        public double peso { get; set; }
        public int volume { get; set; }
        public string modelo { get; set; }
        public int serienfiscal { get; set; }
        public string chave { get; set; }
        public string protocolo { get; set; }

        public int codtransp { get; set; }

        public int cartao_codautorizacao { get; set; }
        public string statNFCe { get; set; }
        public int codcon { get; set; }
        
        [ResultColumn]
        public List<PedidoItem> lstPedidoItem { get; set; }


    }
}   

