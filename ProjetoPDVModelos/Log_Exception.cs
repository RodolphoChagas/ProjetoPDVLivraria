using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace ProjetoPDVModelos
{
    public class Log_Exception
    {
        private static int idNomeArquivo;
        private static string arquivo;
        private static string metodo;
        private static string linha;

        /// <summary>
        /// Monta um arquivo(txt) e salva na pasta onde o projeto foi instalado com os detalhes do erro.
        /// </summary>
        public static void Monta_ArquivoLog(Exception erro) 
        {
            try
            {
                StackTrace trace = new StackTrace(erro, true);

                idNomeArquivo = trace.GetFrame(trace.FrameCount - 1).GetFileName().LastIndexOf("\\") + 1;
                arquivo = trace.GetFrame(trace.FrameCount - 1).GetFileName().Substring(idNomeArquivo).ToString();
                metodo = trace.GetFrame(trace.FrameCount - 1).GetMethod().Name;
                linha = trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber().ToString();

                using (StreamWriter sw = new StreamWriter(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + @"\Log_Erro.txt", true))
                {
                    sw.WriteLine("Data: " + DateTime.Now.ToShortDateString());
                    sw.WriteLine("Hora: " + DateTime.Now.ToShortTimeString());
                    sw.WriteLine("Descrição do erro: " + erro.Message);
                    sw.WriteLine("Origem: " + arquivo);
                    sw.WriteLine("Metodo: " + metodo);
                    sw.WriteLine("Linha: " + linha);
                    //sw.WriteLine("Stack Trace: " + erro.StackTrace);
                    sw.WriteLine("---------------------------------------------------");
                    sw.Flush();
                }
            }
            catch (Exception)
            {
                //throw;
                //return;
            }
        }        
    }
}
