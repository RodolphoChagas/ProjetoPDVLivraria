using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ProjetoPDVModelos
{
    public class XML
    {
        public int numdoc { get; set; }
        public DateTime data { get; set; }
        public string arquivoXML { get; set; }
        public string Modelo { get; set; }
        public string statNFCe { get; set; }


        public XML(){}

        public XML(int numdoc, string arquivoxml, DateTime data)
        {
            this.numdoc = numdoc;
            this.arquivoXML = arquivoxml;
            this.data = data;
        }

        public bool GeraPDF(string xml, string caminho)
        {

            if (string.IsNullOrEmpty(xml) || string.IsNullOrEmpty(caminho))
                return false;

            try
            {
                string nomeArq = xml.Substring(xml.IndexOf("<chNFe>") + 7, 44);
                nomeArq += ".pdf";

                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("http://www.webdanfe.com.br/danfe/GeraDanfe.php");

                string postData = "arquivoXml=" + xml;

                byte[] postBytes = Encoding.UTF8.GetBytes(postData);

                Request.Method = "POST";
                Request.ContentType = "application/x-www-form-urlencoded";
                Request.ContentLength = postBytes.Length;

                Stream requestStream = Request.GetRequestStream();

                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();

                HttpWebResponse response = (HttpWebResponse)Request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine("Resposta do Servidor: " + response.StatusCode.ToString());

                    var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8"));

                    FileStream writeStream = new FileStream(caminho + @"\" + nomeArq, FileMode.Create, FileAccess.Write);

                    ReadWriteStream(stream.BaseStream, writeStream);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return true;
        }



        private void ReadWriteStream(Stream readStream, FileStream writeStream)
        {
            try
            {
                int Length = 256;
                Byte[] buffer = new Byte[Length];
                int bytesRead = readStream.Read(buffer, 0, Length);
                // write the required bytes
                while (bytesRead > 0)
                {
                    writeStream.Write(buffer, 0, bytesRead);
                    bytesRead = readStream.Read(buffer, 0, Length);
                }
                readStream.Close();
                writeStream.Close();

            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
