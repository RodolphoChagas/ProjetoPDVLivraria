﻿//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Este código-fonte foi gerado automaticamente por Microsoft.VSDesigner, Versão 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace ProjetoPDVServico.NFCeAutorizacao4H {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="NFeAutorizacao4Soap", Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4")]
    public partial class NFeAutorizacao4 : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback nfeAutorizacaoLoteOperationCompleted;
        
        private System.Threading.SendOrPostCallback nfeAutorizacaoLoteZipOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public NFeAutorizacao4() {
            this.Url = global::ProjetoPDVServico.Properties.Settings.Default.ProjetoPDVServico_NFCeAutorizacao4H_NFeAutorizacao4;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event nfeAutorizacaoLoteCompletedEventHandler nfeAutorizacaoLoteCompleted;
        
        /// <remarks/>
        public event nfeAutorizacaoLoteZipCompletedEventHandler nfeAutorizacaoLoteZipCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4/nfeAutorizacaoLote", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("nfeResultMsg", Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4", IsNullable=true)]
        public System.Xml.XmlNode nfeAutorizacaoLote([System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4")] System.Xml.XmlNode nfeDadosMsg) {
            object[] results = this.Invoke("nfeAutorizacaoLote", new object[] {
                        nfeDadosMsg});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void nfeAutorizacaoLoteAsync(System.Xml.XmlNode nfeDadosMsg) {
            this.nfeAutorizacaoLoteAsync(nfeDadosMsg, null);
        }
        
        /// <remarks/>
        public void nfeAutorizacaoLoteAsync(System.Xml.XmlNode nfeDadosMsg, object userState) {
            if ((this.nfeAutorizacaoLoteOperationCompleted == null)) {
                this.nfeAutorizacaoLoteOperationCompleted = new System.Threading.SendOrPostCallback(this.OnnfeAutorizacaoLoteOperationCompleted);
            }
            this.InvokeAsync("nfeAutorizacaoLote", new object[] {
                        nfeDadosMsg}, this.nfeAutorizacaoLoteOperationCompleted, userState);
        }
        
        private void OnnfeAutorizacaoLoteOperationCompleted(object arg) {
            if ((this.nfeAutorizacaoLoteCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.nfeAutorizacaoLoteCompleted(this, new nfeAutorizacaoLoteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4/nfeAutorizacaoLoteZip", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("nfeResultMsg", Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4", IsNullable=true)]
        public System.Xml.XmlNode nfeAutorizacaoLoteZip([System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4")] string nfeDadosMsgZip) {
            object[] results = this.Invoke("nfeAutorizacaoLoteZip", new object[] {
                        nfeDadosMsgZip});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void nfeAutorizacaoLoteZipAsync(string nfeDadosMsgZip) {
            this.nfeAutorizacaoLoteZipAsync(nfeDadosMsgZip, null);
        }
        
        /// <remarks/>
        public void nfeAutorizacaoLoteZipAsync(string nfeDadosMsgZip, object userState) {
            if ((this.nfeAutorizacaoLoteZipOperationCompleted == null)) {
                this.nfeAutorizacaoLoteZipOperationCompleted = new System.Threading.SendOrPostCallback(this.OnnfeAutorizacaoLoteZipOperationCompleted);
            }
            this.InvokeAsync("nfeAutorizacaoLoteZip", new object[] {
                        nfeDadosMsgZip}, this.nfeAutorizacaoLoteZipOperationCompleted, userState);
        }
        
        private void OnnfeAutorizacaoLoteZipOperationCompleted(object arg) {
            if ((this.nfeAutorizacaoLoteZipCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.nfeAutorizacaoLoteZipCompleted(this, new nfeAutorizacaoLoteZipCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    public delegate void nfeAutorizacaoLoteCompletedEventHandler(object sender, nfeAutorizacaoLoteCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class nfeAutorizacaoLoteCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal nfeAutorizacaoLoteCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    public delegate void nfeAutorizacaoLoteZipCompletedEventHandler(object sender, nfeAutorizacaoLoteZipCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class nfeAutorizacaoLoteZipCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal nfeAutorizacaoLoteZipCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591