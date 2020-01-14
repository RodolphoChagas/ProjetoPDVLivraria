using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;


namespace ProjetoTeste
{
    public class Teste_LogError: Exception
    {
        private DateTime dataError;
        private string messageError;
        private string sourceError;


        public Teste_LogError(Exception ex) 
        {

            dataError = DateTime.Now;
            messageError = ex.Message;
            sourceError = ex.HelpLink;

            Monta_ArquivoLog();
        
        }




        private void Monta_ArquivoLog()
        {
            try
            {


            }
            catch (Exception)
            {
            }
        }

 
    }
}
