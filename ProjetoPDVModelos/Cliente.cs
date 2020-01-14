using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ProjetoPDVModelos
{
    public class Cliente
    {
        public int codcli { get; set; }
        public string firma { get; set; }
        public Endereco end { get; set; }
        public int tipcli { get; set; }
        public string cgc { get; set; }
        public string inscest { get; set; }
        public string cpf { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
    }
}
