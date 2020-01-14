using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetoPDVModelos
{
    public class Emitente
    {
        private static Emitente _instancia;

        private string _cnpj { get; set; }
        private string _inscest { get; set; }
        private string _nome { get; set; }
        private string _nomefantasia { get; set; }
        private Endereco _endereco { get; set; }


        public static Emitente getInstance
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new Emitente();
                }
                return _instancia;
            }
        }

        public string cnpj 
        {
            get { return _cnpj; }
            set 
            {
                if (_cnpj == null)
                    _cnpj = value;
            }
        }

        public string inscest
        {
            get { return _inscest; }

            set 
            {
                if (_inscest == null)
                    _inscest = value;
            }
        }

        public string nome
        {
            get { return _nome; }
            set 
            {
                if (_nome == null)
                    _nome = value;
            }
        }

        public string nomefantasia
        {
            get { return _nomefantasia; }
            set
            {
                if (_nomefantasia == null)
                    _nomefantasia = value;
            }
        }

        public Endereco endereco 
        {
            get { return _endereco; }
            set 
            {
                if (_endereco == null)
                    _endereco = value;
            }
        }
                       
    }
}
