using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ProjetoPDVDao;
using ProjetoPDVModelos;
//using Ionic.Zip;
using System.IO;
using System.Xml;
using System.Drawing;
using ProjetoPDVServico;
using ProjetoPDVUtil;
using PetaPoco;
using System.Text;

namespace ProjetoPDVUI
{
    public partial class frmMovimento : Form
    {

        List<Pedido> lstp = new List<Pedido>();
        PedidoDao pd = new PedidoDao();
        string strStatNFCe = string.Empty;

        decimal val_Dinheiro = 0;
        decimal val_CCredito = 0;
        decimal val_CDebito = 0;
        decimal val_Outros = 0;

        //TipoPagamento tipopgto = new TipoPagamento();

        FolderBrowserDialog folder = new FolderBrowserDialog();
        XML xml = new XML();
        string caminho;


        public frmMovimento()
        {
            InitializeComponent();
        }

        private void frmMovimento_Load(object sender, EventArgs e)
        {

            //cboProcurar.SelectedIndex = 0;

            dtInicial.Checked = true;
            dtFinal.Checked = true;

            dtInicial.Value = DateTime.Now;
            dtFinal.Value = DateTime.Now;


            cboLoja.SelectedIndex = 0;

            cboProcurar.Items.Add("Data da Nota Fiscal");
            cboProcurar.Items.Add("N° da Nota Fiscal");
            cboProcurar.Items.Add("N° do Pedido");
            cboProcurar.SelectedIndex = 0;

            cboStatus.Items.Add("Todos");
            cboStatus.Items.Add("Pendentes");
            cboStatus.Items.Add("Autorizadas");
            cboStatus.Items.Add("Canceladas");
            cboStatus.Items.Add("Inutilizadas");
            cboStatus.SelectedIndex = 0;

            Inicializa_cboFormaPagamento();
            Inicializa_cboUsuario();
        }

        private void Inicializa_cboUsuario()
        {
            try
            {
                List<Usuario> lstUsuario = (new UsuarioDao()).getlst_Usuarios();

                lstUsuario.Add(new Usuario(0, "Todos"));

                cboUsuario.DataSource = lstUsuario;
                cboUsuario.DisplayMember = "NomeUser";
                cboUsuario.ValueMember = "CodUser";

                cboUsuario.SelectedValue = Usuario.getInstance.codUser;
            }
            catch (Exception)
            {
                MessageBox.Show("Houve um erro ao listar os Usuários..", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Inicializa_cboFormaPagamento()
        {
            try
            {
                List<TipoPagamento> lstTipoPgto = (new TipoPagamentoDAO()).getlstTipoPagamento();

                lstTipoPgto.Add(new TipoPagamento(0, "Todos"));

                cboFormaPagamento.DataSource = lstTipoPgto;
                cboFormaPagamento.DisplayMember = "descTipoPgto";
                cboFormaPagamento.ValueMember = "codTipoPgto";


                cboFormaPagamento.SelectedValue = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Houve um erro ao listar as formas de pagamento..", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdLocalizar_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            lstvwPedidos.Items.Clear();
            lstItens.Items.Clear();
            lstp.Clear();

            lblAutorizado.Text = "000";
            lblCancelado.Text = "000";


            if (cboProcurar.SelectedIndex != 0 & txtProcurar.Text == string.Empty)
            {
                MessageBox.Show("Digite o (" + cboProcurar.Text + ") por favor.", "Mensagem - Busca", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Cursor = Cursors.Default;
                return;
            }

            if (cboProcurar.SelectedIndex == 0 & (dtInicial.Checked == false || dtFinal.Checked == false))
            {
                MessageBox.Show("Entre com a (" + cboProcurar.Text + ") por favor.", "Mensagem - Busca", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Cursor = Cursors.Default;
                return;
            }

            string strWhere = string.Empty;

            try
            {
                switch (cboProcurar.Text)
                {
                    case "Data da Nota Fiscal":

                        if (cboFormaPagamento.Text != "Todos")
                        {
                            strWhere = "TipoPgto = " + cboFormaPagamento.SelectedValue;
                        }

                        if (cboStatus.Text != "Todos")
                        {
                            if (strWhere != string.Empty)
                            {
                                strWhere += " And StatNFCe = " + strStatNFCe;
                            }
                            else
                                strWhere += "StatNFCe = " + strStatNFCe;
                        }


                        lstp = pd.getlstPedidos(cboLoja.Text, Convert.ToInt32(cboUsuario.SelectedValue), string.Format("{0:yyyy-MM-dd 00:00:00}", dtInicial.Value), string.Format("{0:yyyy-MM-dd 23:59:59}", dtFinal.Value), strWhere);
                        

                        break;


                    case "N° da Nota Fiscal":
                        lstp.Add(pd.getPedido(txtProcurar.Text));
                        break;
                    case "N° do Pedido":
                        lstp.Add(pd.getPedido(Convert.ToInt32(txtProcurar.Text)));
                        break;
                }


                if (lstp.Count == 0)
                {
                    MessageBox.Show("Pedido(s) não encontrado(s)!", "Mensagem - Busca", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lstp.Clear();
                    lstItens.Items.Clear();
                    cmdExportaXML.Enabled = false;
                    TxtChaveNFe.Text = string.Empty;
                    TxtNumProtocolo.Text = string.Empty;
                    this.Cursor = Cursors.Default;
                    return;
                }

                if (lstp[0] == null)
                {
                    MessageBox.Show("Pedido(s) não encontrado(s)!", "Mensagem - Busca", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lstp.Clear();
                    lstItens.Items.Clear();
                    cmdExportaXML.Enabled = false;
                    TxtChaveNFe.Text = string.Empty;
                    TxtNumProtocolo.Text = string.Empty;
                    this.Cursor = Cursors.Default;
                    return;
                }


                Lista_Pedidos();
                cmdExportaXML.Enabled = true;
            }
            
            catch (Exception ex)
            {
                Log_Exception.Monta_ArquivoLog(ex);

                MessageBox.Show("Ocorreu um erro inesperado: " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Cursor = Cursors.Default;
        }


        private void Calcula_Totais(Pedido p)
        {

            //Dinheiro
            if (p.tipoPgto.codTipoPgto.Equals(9))
            {
                val_Dinheiro += p.valdoc;
            }
            else if (p.tipoPgto.codFormaPgtoNFCe.Equals(3))
            {
                val_CCredito += p.valdoc;
            }
            else if (p.tipoPgto.codFormaPgtoNFCe.Equals(4))
            {
                val_CDebito += p.valdoc;
            }
            else
            {
                val_Outros += p.valdoc;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (lblPendente.Visible == true)
            {
                lblPendente.Visible = false;
                lbl.Visible = false;
            }
            else
            {
                lblPendente.Visible = true;
                lbl.Visible = true;
            }
        }


        private void Lista_Pedidos()
        {
            try
            {
                ProdutoDao pDao = new ProdutoDao();
                Produto_LojaDao plDao = new Produto_LojaDao();
                Produto_Loja ploja = new Produto_Loja();

                int iPendentes = 0;

                val_Dinheiro = 0;
                val_CCredito = 0;
                val_CDebito = 0;
                val_Outros = 0;

                timer1.Enabled = false;
                lblPendente.Text = "";
                lbl.Text = "";

                foreach (Pedido p in lstp)
                {
                    p.operacao = (new OperacaoDao()).getOperacaoPedido(p.numdoc);
                    p.lstPedidoItem = (new PedidoItemDao()).getlst_Itens(p.numdoc);
                    p.tipoPgto = (new TipoPagamentoDAO()).getTipoPagamento(p.numdoc.ToString());
                    p.cliente = (new ClienteDao()).getClientePedido(p.numdoc);
                    p.codcli = p.cliente.codcli;
                    p.xml = (new XMLDao()).getXML_NFe(p.numdoc);



                    foreach (PedidoItem pedidoitem in p.lstPedidoItem)
                    {
                        pedidoitem.produto = pDao.getProduto(pedidoitem.codpro);

                        if (pedidoitem.produto.codgrupo.Equals(0))
                        {
                            ploja = plDao.getProduto_Loja(pedidoitem.produto.codpro);
                        }
                        else
                        {
                            ploja.codpro = pedidoitem.produto.codpro;
                            ploja.desconto = 0;
                            ploja.estatus = 0;
                            ploja.site = 1;
                        }

                        pedidoitem.produto.produto_loja = ploja;
                        ploja = new Produto_Loja();

                        pedidoitem.produto.subGrupo = pDao.getSubGrupo(pedidoitem.produto.codgrupo, pedidoitem.produto.codsubGrupo);
                    }

                    ListViewItem ls = new ListViewItem(p.numdoc.ToString());
                    ls.SubItems.Add(p.nfiscal.ToString());
                    ls.SubItems.Add(p.datadigitacao.ToString());
                    ls.SubItems.Add(p.datanfiscal.ToString());
                    ls.SubItems.Add(p.operacao.nome);
                    ls.SubItems.Add(p.valdoc.ToString("0.00"));
                    ls.SubItems.Add(p.tipoPgto.descTipoPgto);


                    if (p.statNFCe == null || p.statNFCe.Trim().Equals("0"))
                    {
                        ls.SubItems.Add("NFC-e Pendente");

                        lblPendente.Text = "Pendentes (" + (iPendentes += 1) + ")";
                        lbl.Text = "!";
                        lblPendente.Visible = true;
                        lbl.Visible = true;
                        timer1.Enabled = true;
                    }
                    else if (p.statNFCe.Trim().Equals("102"))
                    {
                        ls.SubItems.Add("NFC-e Inutilizada");
                        Colore_itemListView(ls, Color.Silver);
                    }
                    else if (p.statNFCe.Trim().Equals("100"))
                    {
                        ls.SubItems.Add("NFC-e Autorizada");
                        Colore_itemListView(ls, Color.OliveDrab);

                        lblAutorizado.Text = (Convert.ToInt16(lblAutorizado.Text) + 1).ToString("000");
                    }
                    else if (p.statNFCe.Trim().Equals("135"))
                    {
                        ls.SubItems.Add("NFC-e Cancelada");
                        Colore_itemListView(ls,Color.Brown);

                        lblCancelado.Text = (Convert.ToInt16(lblCancelado.Text) + 1).ToString("000");
                    }

                    ls.SubItems.Add(p.chave);
                    ls.SubItems.Add(p.protocolo);
                    ls.SubItems.Add(p.codvendedor == 104 ? "LIVRARIA":"CAFETERIA");
                    ls.UseItemStyleForSubItems = false;


                    if (ls.SubItems[10].Text == "CAFETERIA")
                        ls.SubItems[10].ForeColor = System.Drawing.Color.SaddleBrown;
                    else
                        ls.SubItems[10].ForeColor = System.Drawing.Color.Chocolate;


                    Calcula_Totais(p);
                    lstvwPedidos.Items.Add(ls);
                }

                lblVal_Dinheiro.Text = val_Dinheiro.ToString("0.00");
                lblVal_CDebito.Text = val_CDebito.ToString("0.00");
                lblVal_CCredito.Text = val_CCredito.ToString("0.00");
                lblVal_Outros.Text = val_Outros.ToString("0.00");

                lblval_Total.Text = (val_Dinheiro + val_CDebito + val_CCredito + val_Outros).ToString("0.00");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Colore_itemListView(ListViewItem item, Color cor)
        {
            item.SubItems[0].ForeColor = cor;
            item.SubItems[1].ForeColor = cor;
            item.SubItems[2].ForeColor = cor;
            item.SubItems[3].ForeColor = cor;
            item.SubItems[4].ForeColor = cor;
            item.SubItems[5].ForeColor = cor;
            item.SubItems[6].ForeColor = cor;
            item.SubItems[7].ForeColor = cor;
        }


        private void cboProcurar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboProcurar.SelectedIndex == 0)
            {
                txtProcurar.Enabled = false;
                dtInicial.Enabled = true;
                dtFinal.Enabled = true;
                cboFormaPagamento.Enabled = true;
            }
            else
            {
                txtProcurar.Enabled = true;
                dtInicial.Enabled = false;
                dtFinal.Enabled = false;
                cboFormaPagamento.Enabled = false;
                //cboFormaPagamento.SelectedIndex = 0;
            }
        }


        private void lstPedidos_SelectedIndexChanged(object sender, EventArgs e)
        {

            lstItens.Items.Clear();
            TxtChaveNFe.Text = string.Empty;
            TxtNumProtocolo.Text = string.Empty;


            if (lstvwPedidos.FocusedItem == null)
            {
                return;
            }

            try
            {
                var item = lstvwPedidos.FocusedItem;

                TxtChaveNFe.Text = item.SubItems[8].Text;
                TxtNumProtocolo.Text = item.SubItems[9].Text;

                foreach (PedidoItem pditem in lstp[item.Index].lstPedidoItem)
                {
                    ListViewItem ls = new ListViewItem(pditem.codpro.ToString());
                    ls.SubItems.Add(pditem.produto.descricao);
                    ls.SubItems.Add(pditem.qtditens.ToString());
                    ls.SubItems.Add(pditem.valitens.ToString("0.00"));
                    ls.SubItems.Add(pditem.dscitens.ToString());
                    ls.SubItems.Add(pditem.prcitens.ToString("0.00"));

                    lstItens.Items.Add(ls);
                }
            }
            catch (Exception ex)
            {
                Log_Exception.Monta_ArquivoLog(ex);

                MessageBox.Show("Erro: " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void frmMovimento_FormClosed(object sender, FormClosedEventArgs e)
        {
            //instancia_MDI.pictureBox1.Visible = true;
        }

        private void cmdExportaXML_Click(object sender, EventArgs e)
        {

            try
            {

                if (lstvwPedidos.FocusedItem == null)
                {
                    return;
                }

                Verifica_Status_Impressora();


                //lstp[lstvwPedidos.FocusedItem.Index].

                if (lstp[lstvwPedidos.FocusedItem.Index].statNFCe.Equals("100"))
                {
                    //ImpressoraBema.GeraDANFE_NFCe(lstp[lstvwPedidos.FocusedItem.Index]);
                }
                else if (lstp[lstvwPedidos.FocusedItem.Index].statNFCe.Equals("0"))
                {
                    ImpressoraBema.GeraDANFE_Cupom(lstp[lstvwPedidos.FocusedItem.Index]);
                }
                else
                {
                    MessageBox.Show("Apenas pedidos emitidos ou pendentes podem gerar DANFE!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro inesperado ao gerar o DANFE, tente novamente.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Verifica_Status_Impressora()
        {
            MP2032.ConfiguraModeloImpressora(7); //Bematech MP-4200 TH
            MP2032.IniciaPorta("USB");

            int iRetorno;

            iRetorno = MP2032.Le_Status();
            if (iRetorno == 0)
            {
                MessageBox.Show("Erro ao se comunicar com a Impressora Bematech MP-4200 TH, verifique por favor.", "** ATENÇÃO **", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (iRetorno == 5)
            {
                MessageBox.Show("Impressora com pouco papel, verifique por favor.", "** ATENÇÃO **", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (iRetorno == 9)
            {
                MessageBox.Show("Impressora com a tampa aberta, verifique por favor.", "** ATENÇÃO **", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (iRetorno == 32)
            {
                MessageBox.Show("Impressora sem papel, verifique por favor.", "** ATENÇÃO **", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Unload_Form();
        }

        private void frmMovimento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                Unload_Form();
            }
            else if (e.KeyData == Keys.F12)
            {
                btnEmitir.Visible = btnEmitir.Visible == true ? false : true;
            }
        }

        private void Unload_Form()
        {
            lstp = null;
            pd = null;
            xml = null;

            this.Close();
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboStatus.Text == "Autorizadas")
            {
                strStatNFCe = "100";
            }
            else if (cboStatus.Text == "Canceladas")
            {
                strStatNFCe = "135";
            }
            else if (cboStatus.Text == "Inutilizadas")
            {
                strStatNFCe = "102";
            }
            else if (cboStatus.Text == "Pendentes")
            {
                strStatNFCe = "0";
            }
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnEmitir_Click(object sender, EventArgs e)
        {

            if (lstvwPedidos.FocusedItem == null)
            {
                return;
            }

            if (lstp[lstvwPedidos.FocusedItem.Index].datadigitacao.Date < DateTime.Now.Date)
            {
                lstp[lstvwPedidos.FocusedItem.Index].datanfiscal = DateTime.Now;
            }


            if (lstp[lstvwPedidos.FocusedItem.Index].nfiscal.Equals("0") || lstp[lstvwPedidos.FocusedItem.Index].nfiscal.Equals(""))
            {

                var db = new Database("stringConexao");

                try
                {

                    db.BeginTransaction();


                    lstp[lstvwPedidos.FocusedItem.Index].nfiscal = ((new ControleNFiscalDao()).getNumNFiscal() + 1).ToString();


                    db.Update("Update Controle Set NFiscal_NFCe=" + Convert.ToInt32(lstp[lstvwPedidos.FocusedItem.Index].nfiscal.Trim()) + " Where ChvControle = 1");
                    db.Update("Update Movdb Set nfiscal = '" + lstp[lstvwPedidos.FocusedItem.Index].nfiscal.Trim() + "' Where NumDoc=" + lstp[lstvwPedidos.FocusedItem.Index].numdoc);

                    db.CompleteTransaction();

                    Controle.getInstance.ultima_NFCe = Convert.ToInt32(lstp[lstvwPedidos.FocusedItem.Index].nfiscal);
                }
                catch (Exception ex)
                {
                    //RollBack
                    db.AbortTransaction();

                    Log_Exception.Monta_ArquivoLog(ex);

                    MessageBox.Show("Houve um erro de conexão com o Banco de Dados." + Environment.NewLine + "Tente Novamente.." + Environment.NewLine + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


            }


            foreach (var ls in lstvwPedidos.CheckedItems)
            {

                var l = (ListViewItem)ls;


                if (Gera_NFCe(lstp[l.Index]))
                {
                    //MessageBox.Show("NFC-e Emitida com Sucesso!", "Emissão Nota Fiscal Eletrônica", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }

            }


            MessageBox.Show("NFC-e Emitida com Sucesso!", "Emissão Nota Fiscal Eletrônica", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cmdLocalizar_Click(sender, e);

            //if (Gera_NFCe(lstp[lstvwPedidos.FocusedItem.Index]))
            //{
            //    MessageBox.Show("NFC-e Emitida com Sucesso!", "Emissão Nota Fiscal Eletrônica", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    cmdLocalizar_Click(sender, e);
            //}


        }


        // ========================================================================
        private bool Gera_NFCe(Pedido pedido)
        {

            var msg = new StringBuilder();

            XmlDocument xmlNFe = new XmlDocument();
            XmlDocument xmlNFe_Assinado = new XmlDocument();

            GerarXML gerarXml = new GerarXML();
            AssinarXML assinarXml = new AssinarXML();
            ValidarXML validarXml = new ValidarXML();
            TransmitirXML transmitirXml = new TransmitirXML();
            Email email = new Email();
            XMLDao xmlDao = new XMLDao();

            StreamWriter Grava;

            string retValidar;
            string strProc;
            string strXmlProcNfe;
            string retTransmitir;
            string cStatus_LoteProcessado;
            string cStatus_Autorizado;

            int nPosI;
            int nPosF;

            try
            {

                retTransmitir = string.Empty;
                retValidar = string.Empty;

                cStatus_LoteProcessado = string.Empty;
                cStatus_Autorizado = string.Empty;

                try
                {

                    // Gerando o XML
                    xmlNFe = (gerarXml.NFe(pedido));

                    //MensagemSistema("Arquivo Gerado ...", Color.OliveDrab);

                    // Assinando o XML
                    xmlNFe_Assinado = assinarXml.AssinaXML(xmlNFe.InnerXml, "infNFe", Certificado.getInstance.oCertificado);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("** Erro ao ASSINAR XML NFC-e, tente novamente **" + Environment.NewLine + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }


                try
                {
                    // Validando o XML
                    retValidar = validarXml.Valida(xmlNFe_Assinado, "NFe");

                    //Inserindo a URL QRCode no xml já assinado
                    xmlNFe_Assinado.LoadXml(xmlNFe_Assinado.InnerXml.Replace("</infNFe>", "</infNFe><infNFeSupl><qrCode><![CDATA[" +
                    gerarXml.Gera_Url_QRCode(xmlNFe_Assinado, pedido) + "]]></qrCode><urlChave>http://www4.fazenda.rj.gov.br/consultaNFCe/QRCode</urlChave></infNFeSupl>"));
                }
                catch (Exception ex)
                {
                    //Log_Exception.Monta_ArquivoLog(ex);
                    MessageBox.Show("** Erro ao VALIDAR XML NFC-e **" + Environment.NewLine + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (retValidar == string.Empty)
                {
                    try
                    {
                        //MensagemSistema("Enviando a NFC-e", Color.OliveDrab);

                        // Recebendo o XML de retorno da transmissão
                        retTransmitir = transmitirXml.XML_NFCe4(xmlNFe_Assinado, pedido.nfiscal, Certificado.getInstance.oCertificado);

                        if (retTransmitir.Substring(0, 4) != "Erro")
                        {
                            XmlDocument xmlRetorno = new XmlDocument();
                            xmlRetorno.LoadXml(retTransmitir);

                            // Lote processado
                            if (xmlRetorno.GetElementsByTagName("cStat")[0].InnerText == "104")
                            {
                                // Autorizado
                                if (xmlRetorno.GetElementsByTagName("cStat")[1].InnerText == "100")
                                {
                                    try
                                    {
                                        //MensagemSistema("Autorizado o uso da NFC-e", Color.OliveDrab);

                                        pedido.chave = xmlRetorno.GetElementsByTagName("chNFe")[0].InnerText;
                                        pedido.protocolo = xmlRetorno.GetElementsByTagName("nProt")[0].InnerText;

                                        // Separar somente o conteúdo a partir da tag <protNFe> até </protNFe>
                                        nPosI = retTransmitir.IndexOf("<protNFe");
                                        nPosF = retTransmitir.Length - (nPosI + 13);
                                        strProc = retTransmitir.Substring(nPosI, nPosF);

                                        // XML pronto para salvar
                                        strXmlProcNfe = @"<?xml version=""1.0"" encoding=""utf-8"" ?><nfeProc xmlns=""http://www.portalfiscal.inf.br/nfe"" versao=""4.0"">" + xmlNFe_Assinado.InnerXml + strProc + "</nfeProc>";

                                        pedido.xml = new XML
                                        {
                                            numdoc = pedido.numdoc,
                                            arquivoXML = strXmlProcNfe,
                                            data = DateTime.Now,
                                            Modelo = pedido.modelo,
                                            statNFCe = "100"
                                        };


                                        //Salva arquivo XML no Banco SQL (NFe)
                                        if (xmlDao.Grava_XML(pedido.xml))
                                        {
                                            // Atualizando o pedido com a Chave, Protocolo, e statNFCe
                                            if ((new PedidoDao()).Update_ChaveProtocolo_condDoc_StatNFCe(pedido.numdoc, pedido.chave, pedido.protocolo, pedido.xml.statNFCe))
                                            {
                                                if (GerarXML.str_Ambiente == "1")
                                                {
                                                    if (!string.IsNullOrEmpty(Controle.getInstance.caminho_XMLAutorizado))
                                                    {
                                                        //Salvando o arquivo XML na pasta
                                                        Grava = File.CreateText(Controle.getInstance.caminho_XMLAutorizado.Remove(Controle.getInstance.caminho_XMLAutorizado.Length - 1) + DateTime.Now.Month + @"\" + pedido.chave + "-procNfe.xml");
                                                        Grava.Write(pedido.xml.arquivoXML);
                                                        Grava.Close();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("** NOTA EMITIDA **, mas houve um erro inesperado" + Environment.NewLine + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return false;
                                    }
                                }
                                else
                                {
                                    //MessageBox.Show("Erro ao Transmitir(004) XML NFC-e" + Environment.NewLine + xmlRetorno.GetElementsByTagName("xMotivo")[1].InnerText, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    
                                    msg.Append("** NOTA EMITIDA **, mas houve um erro inesperado");
                                    msg.Append(Environment.NewLine);
                                    msg.Append(xmlRetorno.GetElementsByTagName("xMotivo")[1].InnerText);

                                    txtErro.Visible = true;
                                    btnEmitir.Visible = true;

                                    txtErro.Text = msg.ToString();

                                    msg.Clear();

                                    return false;
                                }
                            }
                            else
                            {
                                //MessageBox.Show("Erro ao Transmitir(003) XML NFC-e" + Environment.NewLine + xmlRetorno.GetElementsByTagName("xMotivo")[0].InnerText, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                msg.Append("** Erro ao Transmitir(003) XML NFC-e**");
                                msg.Append(Environment.NewLine);
                                msg.Append(xmlRetorno.GetElementsByTagName("xMotivo")[0].InnerText);

                                txtErro.Visible = true;
                                btnEmitir.Visible = true;

                                txtErro.Text = msg.ToString();

                                msg.Clear();


                                return false;
                            }
                        }
                        else
                        {
                            //MessageBox.Show("Erro ao Transmitir(002) XML NFC-e" + Environment.NewLine + retTransmitir, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            msg.Append("** Erro ao Transmitir(002) XML NFC-e **");
                            msg.Append(Environment.NewLine);
                            msg.Append(retTransmitir);

                            txtErro.Visible = true;
                            btnEmitir.Visible = true;

                            txtErro.Text = msg.ToString();

                            msg.Clear();


                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao Transmitir(001) XML NFC-e" + Environment.NewLine + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Erro ao validar XML NFC-e" + Environment.NewLine + retValidar, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log_Exception.Monta_ArquivoLog(ex);
                MessageBox.Show("Ocorreu um erro inesperado, informe ao administrador do sistema!" + Environment.NewLine + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btnFecharErro_Click(object sender, EventArgs e)
        {
            txtErro.Visible = false;
            btnFecharErro.Visible = false;  
        }
    }
}