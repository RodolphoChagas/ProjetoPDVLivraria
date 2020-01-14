using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoPDVModelos;
using System.IO;


namespace ProjetoPDVDao
{
    public class XMLDao
    {

        public XML getXML_NFe(int numDoc)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<XML>("SELECT top 1 * FROM XML_NFe WHERE NumDoc=@0", numDoc);
            }
            catch (Exception)
            {
                throw;
            }

        }

        /*
        /// <summary>
        /// Retorna uma lista com todos os arquivos XML do pedido passado por parametro.
        /// </summary>
        public List<string> getlistXML(int numDoc) 
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).Query<string>("SELECT arquivoXML FROM XML_NFe WHERE NumDoc=" + numDoc).ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return (new PetaPoco.Database("stringConexao")).Query<string>("SELECT arquivoXML FROM XML_NFe WHERE NumDoc=" + numDoc).ToList();
        }
         * */

        public string getArquivoXML(int numDoc)
        {
            string arquivo = string.Empty;

            try
            {
                arquivo = (new PetaPoco.Database("stringConexao")).SingleOrDefault<string>("SELECT arquivoXML FROM XML_NFe WHERE NumDoc=" + numDoc);
            }
            catch (Exception)
            {
                throw;
            }

            return arquivo;
        }
        
        /// <summary>
        /// Salva o arquivo de XML na tabela designada.
        /// <para>Valores para o parametro 'tipoNFe'</para>
        /// <para>"NFe" - Nota Fiscal Eletronica.</para>
        /// <para>"Canc" - Nota de Cancelamento.</para>
        /// <para>"CCe" - Nota de Carta de Correção.</para>
        /// <para>"Inut" - Nota de Inutilização.</para>
        /// </summary>
        public bool Grava_XML(XML xml)
        {
            try
            {

                if ((new PetaPoco.Database("stringConexao")).Insert("XML_NFe", "idXML", false, xml) != null)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log_Exception.Monta_ArquivoLog(ex);

                return false;
            }

            return true;
        }
    }
}