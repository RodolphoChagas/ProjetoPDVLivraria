using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ProjetoPDVModelos
{
    [PetaPoco.TableName("FecCaixaPDV")]
    public class FecCaixa
    {
        public int id { get; set; }
        public int coduser { get; set; }
        public string nomeuser { get; set; }
        public DateTime data { get; set; }
        public decimal val_dinheirof { get; set; }
        public decimal val_debitof { get; set; }
        public decimal val_creditof { get; set; }
        public decimal val_outrosf { get; set; }
        public decimal val_dinheirouser { get; set; }
        public decimal val_debitouser { get; set; }
        public decimal val_creditouser { get; set; }
        public decimal val_outrosuser { get; set; }
        public string observacao { get; set; }
        public decimal diferencaDinheiro { get; set; }
        public decimal diferencaCredito { get; set; }
        public decimal diferencaDebito { get; set; }
        public decimal diferencaOutros { get; set; }
        public int loja { get; set; }

        public decimal Devolucao { get; set; }

        public FecCaixa() { }

    }
}
