using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Text;

namespace ProjetoPDVModelos
{
    public class Certificado
    {

        private static Certificado _instancia;
        private X509Certificate2 _oCertificado;
        private string _sSubject;
        private DateTime _dValidadeInicial;
        private DateTime _dValidadeFinal;


        public X509Certificate2 oCertificado
        {
            get { return _oCertificado; }
            set { _oCertificado = value; }
        }

        public string sSubject
        {
            get { return _sSubject; }
            set { _sSubject = value; }
        }

        public DateTime dValidadeInicial
        {
            get { return _dValidadeInicial; }
            set { _dValidadeInicial = value; }
        }

        public DateTime dValidadeFinal
        {
            get { return _dValidadeFinal; }
            set { _dValidadeFinal = value; }
        }



        private Certificado() 
        {
        }


        public static Certificado getInstance
        {
            get 
            { 
                if(_instancia == null)
                {
                    return _instancia = new Certificado();
                }
                return _instancia;
            }
        }

        public void Seleciona_Certificado()
        {
            try
            {
                //_oCertificado = new X509Certificate2();
                //X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
                //store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

                //X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
                //X509Certificate2Collection collection1 = (X509Certificate2Collection)(collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false));
                //X509Certificate2Collection collection2 = (X509Certificate2Collection)(collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false));

                oCertificado = new X509Certificate2(@"\\SERVER\Dados\Sistemas\LAC\Certificados\Backup Certificado digital.pfx", "contrati", X509KeyStorageFlags.Exportable);


                //_oCertificado = store.Certificates[0];
                _sSubject = oCertificado.Subject;
                _dValidadeInicial = oCertificado.NotBefore;
                _dValidadeFinal = oCertificado.NotAfter;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Altera_Certificado(X509Certificate2 cert)
        {
            try
            {
                _oCertificado = cert;
                _sSubject = cert.Subject;
                _dValidadeInicial = cert.NotBefore;
                _dValidadeFinal = cert.NotAfter;
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
