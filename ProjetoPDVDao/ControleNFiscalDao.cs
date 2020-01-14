using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoPDVModelos;

namespace ProjetoPDVDao
{
    public class ControleNFiscalDao
    {
        ~ControleNFiscalDao() { }


        public int GeraNovo_NumeroNFiscal() 
        {
            
            var db = new PetaPoco.Database("stringConexao");
            int iNFiscal = 0;

            try
            {
            
                db.BeginTransaction();


                //Buscando o numero da ultima nota fiscal emitida e gerando um novo numero
                iNFiscal = (this.getNumNFiscal() + 1);

                //Atualizando o numero da nota fiscal no banco de dados
                db.Update("Update Controle Set NFiscal_NFCe=" + iNFiscal + " Where ChvControle = 1");


                Controle.getInstance.ultima_NFCe = Convert.ToInt32(iNFiscal);
                
                
                db.CompleteTransaction();
            }
            catch (Exception)
            {
                db.AbortTransaction();
                return 0;
            }

            return iNFiscal;
        }
        

        public Controle getControle()
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<Controle>("select * from Controle where ChvControle = 1");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int getNumNFiscal()
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).ExecuteScalar<int>("select NFiscal_NFCe from Controle where ChvControle = 1");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Atualiza a coluna 'NFiscal_NFCe' da tabela 'Controle' com a ultima nfiscal emitida.
        /// </summary>
        public void UpdateNFiscal(int nfiscal)
        {
            try
            {
                if ((new PetaPoco.Database("stringConexao")).Update("Update Controle Set NFiscal_NFCe=" + nfiscal + " Where ChvControle = 1") != 1)
                    throw new Exception("");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Atualiza a tabela 'Controle' com a query passada por parametro.
        /// </summary>
        public bool Update(string query)
        {
            try
            {
                if ((new PetaPoco.Database("stringConexao")).Update(query) != 1)
                    return false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

            return true;
        }
    }
}
