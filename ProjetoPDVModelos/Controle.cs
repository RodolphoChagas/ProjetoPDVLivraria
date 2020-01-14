using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace ProjetoPDVModelos
{
    public class Controle
    {
        
        private int _ultima_NFCe;
        private string _csc_Producao;
        private string _csc_Homologacao;
        private string _caminho_XMLAutorizado;
        private string _caminho_XMLCancelado;
        private string _caminho_XMLInutilizado;
        private static Controle _instance;

        [Column("NFiscal_NFCe")]
        public int ultima_NFCe
        {
            get { return _ultima_NFCe; }
            set
            {
                    _ultima_NFCe = value;
            }
        }

        public string caminho_XMLAutorizado
        {
            get { return _caminho_XMLAutorizado; }
            set
            {
                _caminho_XMLAutorizado = value;
            }
        }
        public string caminho_XMLCancelado
        {
            get { return _caminho_XMLCancelado; }
            set
            {
                _caminho_XMLCancelado = value;
            }
        }
        public string caminho_XMLInutilizado
        {
            get { return _caminho_XMLInutilizado; }
            set
            {
                _caminho_XMLInutilizado = value;
            }
        }


        public string csc_Homologacao
        {
            get { return _csc_Homologacao; }
            set
            {
                //if (_csc_Homologacao == null)
                    _csc_Homologacao = value;
            }
        }


        public string csc_Producao
        {
            get { return _csc_Producao; }
            set
            {
                //if (_csc_Producao == null)
                    _csc_Producao = value;
            }
        }


        public static Controle getInstance
        {
            get
            {
                if (_instance == null)
                {
                    return _instance = new Controle();
                }
                return _instance;
            }
        }


   }
}
