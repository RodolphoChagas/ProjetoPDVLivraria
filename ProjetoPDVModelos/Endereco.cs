using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetoPDVModelos
{
    public class Endereco
    {
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string municipio { get; set; }
        public string cep { get; set; }
        public string cMun { get; set; }
        public string uf { get; set; }
        

        public Endereco() {
        }

    }
}