using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ProjetoPDVModelos;
using ProjetoPDVDao;
using PetaPoco;
using System.IO;
using System.Xml;
using ProjetoPDVServico;
using ProjetoPDVUI;
using ProjetoPDVUtil;

namespace ProjetoPDVUI
{
    public partial class frmSelecionaPagamento : Form
    {

        Pedido pedido = new Pedido();


        private string urlQRCode;


        Cliente cliente = new Cliente();
        ClienteDao clienteDao = new ClienteDao();
        PedidoDao pedidoDao = new PedidoDao();
        TipoPagamentoDAO tipopgtoDao = new TipoPagamentoDAO();
        TipoPagamento tipopgto = new TipoPagamento();
        Boleta boleta = new Boleta();

        int iDesconto = 0;

        Button button_tipopgto = new Button();
        frmCaixa frmCaixa;
        int iRetorno;

        public frmSelecionaPagamento(Pedido p, frmCaixa instanciaForm)
        {
            InitializeComponent();

            //Inicializando as imagens dos botoes
            btnVisa.Image = System.Drawing.Bitmap.FromFile(@"Imagens\visa3.png");
            btnMasterCard.Image = System.Drawing.Bitmap.FromFile(@"Imagens\mastercard3.png");
            btnELO.Image = System.Drawing.Bitmap.FromFile(@"Imagens\elo.png");
            btnDiners.Image = System.Drawing.Bitmap.FromFile(@"Imagens\diners.png");
            btnAmericanExpress.Image = System.Drawing.Bitmap.FromFile(@"Imagens\american2.png");
            btnVisaEletron.Image = System.Drawing.Bitmap.FromFile(@"Imagens\visaelectron2.png");
            btnRedeShop.Image = System.Drawing.Bitmap.FromFile(@"Imagens\maestro3.png");
            btnDinheiro.Image = System.Drawing.Bitmap.FromFile(@"Imagens\dinheiro.png");
            btnFinalizar.Image = System.Drawing.Bitmap.FromFile(@"Imagens\accept.png");
            btnCancelar.Image = System.Drawing.Bitmap.FromFile(@"Imagens\cancel.png");

            //Iniciando as variaveis passadas por parametros
            frmCaixa = instanciaForm;
            pedido = p;
            //----------------------------------------------

            foreach (Control control in this.panel1.Controls)
            {
                control.MouseDown += ControlsPanel_MouseDown;
            }
        }


        private void cboConvenio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboConvenio.SelectedIndex.Equals(cboConvenio.Items.Count - 1))
            {
                iDesconto = 0;

                lblDesconto.Text = "0.00";
                lblTotalFatura.Text = pedido.valdoc.ToString("0.00");

                txtValorPago.Text = lblTotalFatura.Text;
                lblTroco.Text = "0,00";
            }
            else
            {
                //int i = cboConvenio.SelectedValue;

                iDesconto = 5;//(new ConvenioDao()).getDesconto(5);
                Aplica_DescontoaVista();
            }

            Focus_Textbox(txtValorPago);
        }

        private void Inicializa_cboConvenio()
        {

            List<Convenio> lstConvenio = (new ConvenioDao()).getlstConvenio();

            lstConvenio.Add(new Convenio(0, "0% - Desconto"));

            cboConvenio.DataSource = lstConvenio;
            cboConvenio.DisplayMember = "desccon";
            cboConvenio.ValueMember = "codcon";


            cboConvenio.SelectedValue = 0;
            //cboConvenio.SelectedText = "0% - Desconto";
            //cboConvenio.SelectedIndex = cboConvenio.Items.Count-1;
        }

        private void frmSelecionaPagamento_Load(object sender, EventArgs e)
        {

            txtCodAutorizacao.Select();
            decimal ValPedido = pedido.valdoc;


            try
            {
                lblTotalFatura.Text = ValPedido.ToString("0.00");
                lblSubTotal.Text = ValPedido.ToString("0.00");

                txtValorPago.Text = ValPedido.ToString("0.00");
            }
            catch (Exception ex)
            {
                Log_Exception.Monta_ArquivoLog(ex);
                MessageBox.Show("Erro : " + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MensagemSistema("Ocorreu um erro inesperado ao iniciar a forma de pagamento", Color.Brown);
            }

            Inicializa_cboConvenio();
        }


        private void MensagemSistema(string mensagem, Color cor)
        {
            rsMensagem.BackColor = cor;
            lblMensagem.BackColor = cor;
            lblMensagem.Text = mensagem;
        }

        private void ControlsPanel_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                (sender as Button).BackColor = Color.Transparent;
            }

        }

        private void ControlsPanel_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                (sender as Button).BackColor = Color.Orange;
            }
        }


        private void ControlsPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (tipopgto.codTipoPgto != 0)
                foreach (Button btn in this.panel1.Controls)
                {
                    btn.BackColor = Color.Transparent;
                }

            if (sender is Button)
            {
                try
                {
                    button_tipopgto = (sender as Button);

                    tipopgto = tipopgtoDao.getTipoPagamento(Convert.ToInt32(button_tipopgto.Tag));


                    lblCodAutorizacao.Visible = tipopgto.codFormaPgtoNFCe != 1 ? true : false;
                    txtCodAutorizacao.Visible = tipopgto.codFormaPgtoNFCe != 1 ? true : false;


                    button_tipopgto.BackColor = Color.Orange;


                    if (tipopgto.maximum_number_of_plots.Equals(1))
                    {
                        //ckDesconto.Enabled = true;
                        //ckDesconto.CheckState = CheckState.Checked;
                        /*
                        if (Usuario.getInstance.loja.Equals(0))
                            cboConvenio.SelectedIndex = 0; //desconto a vista
                        else
                        */

                        cboConvenio.SelectedIndex = cboConvenio.Items.Count - 1;


                        //Aplica_DescontoaVista();
                    }
                    else
                    {
                        cboConvenio.SelectedIndex = cboConvenio.Items.Count - 1;
                        //ckDesconto.Enabled = false;
                        //ckDesconto.CheckState = CheckState.Unchecked;
                    }

                    if (Convert.ToDecimal(lblTotalFatura.Text) > tipopgto.minimum_value_for_plots)
                    {
                        cboParcelas.Enabled = true;
                        Preenche_CboParcelas(tipopgto.minimum_value_for_plots, tipopgto.maximum_number_of_plots);
                        //Preenche_CboParcelas(tipopgto.maximum_number_of_plots);
                    }
                    else
                    {
                        cboParcelas.Enabled = false;
                        Preenche_CboParcelas(Convert.ToDecimal(lblTotalFatura.Text), 1);
                        //Preenche_CboParcelas(1);
                    }

                    Focus_Textbox(txtValorPago);
                }
                catch (Exception ex)
                {
                    Log_Exception.Monta_ArquivoLog(ex);
                    MessageBox.Show("Erro : " + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MensagemSistema("Ocorreu um erro inesperado ao iniciar a forma de pagamento", Color.Brown);
                }
            }
        }

        private void Preenche_CboParcelas(decimal minValue, int maxParcelas)
        {
            cboParcelas.Items.Clear();
            
            int numParcelas = 0;

            if (maxParcelas != 1)
                numParcelas = Convert.ToInt32(Math.Truncate((Convert.ToDecimal(lblTotalFatura.Text) / minValue)));
            else
                numParcelas = 1;

            for (int i = 1; i <= (numParcelas <= maxParcelas ? numParcelas : maxParcelas); i++)
            {
                cboParcelas.Items.Add(i);
            }

            cboParcelas.SelectedIndex = 0;
        }

        private void frmSelecionaPagamento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F5)
            {
                Finalizar(false);
            }
            else if (e.KeyData == Keys.F8)
            {
                Unload_Form();
            }
        }

        private void Verifica_Status_Impressora()
        {
            MP2032.ConfiguraModeloImpressora(7); // Bematech MP-4200 TH
            MP2032.IniciaPorta("USB");

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
                MP2032.FechaPorta();
                return;
            }
            else if (iRetorno == 32)
            {
                MessageBox.Show("Impressora sem papel, verifique por favor.", "** ATENÇÃO **", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Aplica_DescontoaVista()
        {
            try
            {
                decimal valDesc = 0;
                decimal valTotal = 0;
                decimal valTotalDsc = 0;

                if (rbDinheiro.Checked)
                {
                    valTotalDsc += Convert.ToDecimal(txtDescDinheiro.Text.Trim());
                    valTotal += (Convert.ToDecimal(lblSubTotal.Text) - valTotalDsc);
                }
                else
                {
                    foreach (PedidoItem pItem in pedido.lstPedidoItem)
                    {
                        //Rateando o desconto da nota entre os itens do pedido
                        valDesc = 0;
                        valDesc = ((pItem.valitens * iDesconto) / 100);
                        valDesc = decimal.Round(valDesc, 2);

                        valTotalDsc += valDesc;
                        valTotal += (pItem.valitens - valDesc);
                    }
                }


                lblDesconto.Text = valTotalDsc.ToString("0.00");
                lblTotalFatura.Text = valTotal.ToString("0.00");

                txtValorPago.Text = lblTotalFatura.Text;
                lblTroco.Text = "0,00";
            }
            catch (Exception ex)
            {
                MensagemSistema("Erro inesperado ao aplicar o desconto à vista, tente novamente" + Environment.NewLine + ex.Message, Color.Brown);
                MessageBox.Show("Erro: " + ex.Message, "Mensagem de erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Finalizar(bool isImprimir)
        {

            if (txtValorPago.Text.Trim() == string.Empty)
            {
                MensagemSistema("Digite o Valor Pago pelo cliente por favor!", Color.Brown);
                return;
            }

            if (Convert.ToDecimal(txtValorPago.Text) < Convert.ToDecimal(lblTotalFatura.Text))
            {
                MensagemSistema("Valor Pago está menor que o total da Fatura, verifique por favor!", Color.Brown);
                return;
            }

            if (tipopgto.codTipoPgto == 0)
            {
                MensagemSistema("Selecione a forma de pagamento por favor", Color.Brown);
                return;
            }

            if (string.IsNullOrEmpty(txtClienteCpf.Text.Trim()) && (!string.IsNullOrEmpty(txtClienteNome.Text.Trim()) || !string.IsNullOrEmpty(txtClienteCelular.Text.Trim()) || !string.IsNullOrEmpty(txtClienteEmail.Text.Trim())))
            {
                MensagemSistema("O CPF é obrigatório na identificação do cliente", Color.Brown);
                return;
            }


            Inicializa_Cliente();

            if(isImprimir)
                Verifica_Status_Impressora();


            DialogResult retMsgBox = MessageBox.Show("Confirma gerar a NFC-e ?", "Finalizando pagamento", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            
            if (retMsgBox == DialogResult.Cancel) // Cancelando a finalização do pagamento
            {
                return;
            }



            var db = new Database("stringConexao");

            try
            {
                // Iniciando a transação
                db.BeginTransaction();

                pedido.datadigitacao = DateTime.Now;
                pedido.datanfiscal = DateTime.Now;
                pedido.datavencimento = DateTime.Now;
                pedido.codtransp = 4; // Frete grátis
                pedido.codtipopgto = tipopgto.codTipoPgto;
                pedido.tipoPgto = tipopgto;
                pedido.operacao = retorna_Operacao_fiscal(Convert.ToInt32(cboParcelas.Text));
                pedido.codoperacao = pedido.operacao.codoperacao;
                pedido.statNFCe = "0";
                //pedido.codcon = 0;
                //pedido.dscdoc = Convert.ToDecimal(lblDesconto.Text);
                pedido.valdsc = Convert.ToDecimal(lblDesconto.Text);
                pedido.valdoc = Convert.ToDecimal(lblTotalFatura.Text);
                pedido.cartao_codautorizacao = Convert.ToInt32(txtCodAutorizacao.Text.Trim().Equals("") ? "0" : txtCodAutorizacao.Text.Trim());
                pedido.conddoc = "F";


                //if(retMsgBox == DialogResult.Yes)
                //    pedido.nfiscal = ((new ControleNFiscalDao()).getNumNFiscal() + 1).ToString();
                if (retMsgBox == DialogResult.Yes)
                {
                    pedido.nfiscal = ((new ControleNFiscalDao()).getNumNFiscal() + 1).ToString();
                    db.Update("Update Controle Set NFiscal_NFCe=" + Convert.ToInt32(pedido.nfiscal.Trim()) + " Where ChvControle = 1");
                    Controle.getInstance.ultima_NFCe = Convert.ToInt32(pedido.nfiscal);
                }


                // Inserindo o pedido na tabela Movdb
                pedido.numdoc = Convert.ToInt32(db.Insert(pedido));

                // Atualizando o estoque dos Produtos
                decimal valDesc;
                ProdutoDao produtodao = new ProdutoDao();


                var i = 0;

                foreach (PedidoItem pItem in pedido.lstPedidoItem)
                {

                    if (i.Equals(0) && rbDinheiro.Checked)
                    {
                        // Rateando o desconto da nota entre os produtos
                        valDesc = Convert.ToDecimal(lblDesconto.Text);
                        valDesc = decimal.Round(valDesc, 2);

                        pItem.valitens -= valDesc; // aplicando o desconto
                        pItem.valDesc += valDesc; // totalizando os descontos
                    }
                    i++;


                    // Se houver desconto na nota, ratear esse desconto entre os produtos
                    if (cboConvenio.SelectedIndex != cboConvenio.Items.Count - 1 || !rbDinheiro.Checked)
                    {
                        // Rateando o desconto da nota entre os produtos
                        valDesc = 0;
                        valDesc = ((pItem.valitens * iDesconto) / 100);
                        valDesc = decimal.Round(valDesc, 2);

                        pItem.valitens -= valDesc; // aplicando o desconto
                        pItem.valDesc += valDesc; // totalizando os descontos
                    }



                    //pItem.valitens = decimal.Round(pItem.valitens,2);
                    //throw new Exception("");

                    //produtodao.AtualizaEstoque(pItem.codpro, pItem.qtditens, pItem.valitens, pedido.operacao);
                    db.Update("UPDATE produto " +
                                "SET Estoque = Estoque + " + pItem.qtditens * pedido.operacao.STQ +
                                ",QtdCns = QtdCns + " + pItem.qtditens * pedido.operacao.cns +
                                ",QtdPrm = QtdPrm + " + pItem.qtditens * pedido.operacao.prm +
                                ",QtdAva = QtdAva + " + pItem.qtditens * pedido.operacao.ava +
                                ",QtdEnt = QtdEnt + " + pItem.qtditens * pedido.operacao.ent +
                                ",QtdVnd = QtdVnd + " + pItem.qtditens * pedido.operacao.vnd +
                                ",QtdTro = QtdTro + " + pItem.qtditens * pedido.operacao.tro +
                                ",QtdDif = QtdDif + " + pItem.qtditens * pedido.operacao.dif +
                                ",ValorVnd = ValorVnd + " + (pItem.valitens * pedido.operacao.vnd).ToString().Replace(",", ".") +
                                " WHERE CodPro = " + pItem.codpro);
                }

                //Inserindo os Itens na tabela MOVITENS
                db.Insert_lstPedidos(pedido.numdoc, pedido.lstPedidoItem);


                //Gerando pagamento
                if (!Gera_Boleta(db))
                    throw new Exception("");


                //Commit
                db.CompleteTransaction();
            }
            catch (Exception ex)
            {
                //RollBack
                db.AbortTransaction();

                Log_Exception.Monta_ArquivoLog(ex);

                MensagemSistema("Erro ao finalizar Pedido", Color.Brown);
                MessageBox.Show("Houve um erro de conexão com o Banco de Dados." + Environment.NewLine + "Tente Novamente.." + Environment.NewLine + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            if(retMsgBox == DialogResult.Yes)
            {

                if (isImprimir)
                {

                    //Gerando o XML de Nota Fiscal Eletronica(NFC-e)
                    if (Gera_NFCe())
                    {
                        try
                        {
                            //Verificando se existe troco
                            if (lblTroco.Text != "0,00")
                            {
                                //Imprimindo a DANFE NFC-e
                                ImpressoraBema.GeraDANFE_NFCe(pedido, urlQRCode, Convert.ToDecimal(txtValorPago.Text));
                            }
                            else
                                ImpressoraBema.GeraDANFE_NFCe(pedido, urlQRCode);


                            MessageBox.Show("NFC-e emitida com sucesso!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        catch (Exception)
                        {
                            MessageBox.Show("** NOTA EMITIDA **" + Environment.NewLine + "Mas houve um erro inesperado ao se comunicar com a IMPRESSOSA BEMATECH", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("O pedido foi finalizado, mas houve um problema ao gerar a NOTA FISCAL!" + Environment.NewLine + "Deseja imprimir um comprovante ?", "Mensagem", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            try
                            {
                                //Verificando se existe troco
                                if (lblTroco.Text != "0,00")
                                {
                                    //Imprimindo a DANFE NFC-e
                                    ImpressoraBema.GeraDANFE_Cupom(pedido, Convert.ToDecimal(txtValorPago.Text));
                                }
                                else
                                    ImpressoraBema.GeraDANFE_Cupom(pedido);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("O PEDIDO foi finalizado" + Environment.NewLine + "Mas houve um erro inesperado ao se comunicar com a IMPRESSOSA BEMATECH", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    //Gerando o XML de Nota Fiscal Eletronica(NFC-e)
                    if (Gera_NFCe())
                    {
                        MessageBox.Show("NFC-e emitida com sucesso!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("O pedido foi finalizado, mas houve um problema ao gerar a NOTA FISCAL!", "Mensagem NFC-e", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                this.Close();
                frmCaixa.Fecha_Caixa();
            }
            else if(retMsgBox == DialogResult.No)
            {
                try
                {
                    //Verificando se existe troco
                    if (lblTroco.Text != "0,00")
                    {
                        //Imprimindo a DANFE NFC-e
                        ImpressoraBema.GeraDANFE_Cupom(pedido, Convert.ToDecimal(txtValorPago.Text));
                    }
                    else
                        ImpressoraBema.GeraDANFE_Cupom(pedido);

                    MessageBox.Show("PEDIDO FINALIZADO COM SUCESSO!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("O PEDIDO foi finalizado" + Environment.NewLine + "Mas houve um erro inesperado ao se comunicar com a IMPRESSOSA BEMATECH", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                this.Close();
                frmCaixa.Fecha_Caixa();
            }
        }





        private bool Gera_Boleta(Database db1)
        {

            //var db1 = new Database("stringConexao");

            try
            {

                if (pedido.codoperacao == 0)
                    return false;

                //Abrindo a transação
                db1.BeginTransaction();


                BoletaDao bdao = new BoletaDao();
                Boleta boleta = new Boleta(pedido.numdoc, pedido.codcli, 1, 0);
                pedido.operacao = (new OperacaoDao()).getOperacao(pedido.codoperacao);

                decimal v = pedido.valdoc;
                int n = pedido.operacao.numpagto;

                if (pedido.operacao.vnd == 1)
                {
                    for (int numPagto = 1; numPagto <= pedido.operacao.numpagto; numPagto++)
                    {

                        if (numPagto == 1)
                        {
                            pedido.datavencimento = pedido.datavencimento.AddDays(pedido.operacao.intinic);
                        }
                        else
                        {
                            pedido.datavencimento = pedido.datavencimento.AddDays(pedido.operacao.intervalo);
                        }


                        if (n > 1)
                        {
                            if (numPagto == 1)
                            {
                                v = Math.Round((pedido.valdoc / n), 2);
                            }

                            if (numPagto == n)
                            {
                                v = (pedido.valdoc - (v * (n - 1)));
                            }
                        }

                        boleta.numpgto = numPagto;
                        boleta.datavenc = pedido.datavencimento;
                        boleta.valor = v;
                        boleta.valorpago = v;


                        if (bdao.VerificaBoleta(pedido.numdoc, numPagto))
                        {
                            //bdao.UpdateBoleta(boleta);
                            db1.Update(boleta);
                        }
                        else
                        {
                            //bdao.InsertBoleta(boleta);
                            boleta.condicao = 1;
                            db1.Insert(boleta);
                        }
                    }

                    boleta = null;
                    bdao = null;
                }

                db1.CompleteTransaction();

                return true;
            }
            catch (Exception)
            {
                db1.AbortTransaction();
                return false;
            }
        }


        private void txtValorPago_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtValorPago.Text == string.Empty)
                    return;

                if (Convert.ToDecimal(txtValorPago.Text) < 0)
                {
                    MensagemSistema("Digite o valor corretamente", Color.Brown);
                }

                if (txtValorPago.Text != string.Empty)
                    lblTroco.Text = (Convert.ToDecimal(txtValorPago.Text) - Convert.ToDecimal(lblTotalFatura.Text)).ToString("0.00");
            }
            catch (Exception ex)
            {
                MensagemSistema("Campo (Valor Pago) digitado incorretamente", Color.Brown);
                MessageBox.Show("Erro : " + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            Finalizar(false);
        }

        private void Unload_Form()
        {
            if (MessageBox.Show("Confirma cancelar a operação ?", "Cancelamento", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                pedido = null;
                tipopgto = null;
                boleta = null;

                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Unload_Form();
        }

        private Operacao retorna_Operacao_fiscal(int numParcelas)
        {
            OperacaoDao opDao = new OperacaoDao();

            int iCodOperacao = 0;


            if (pedido.tipoPgto.tipo.Equals(0))
            {
                return opDao.getOperacao(100); // VENDA A VISTA
            }

            switch (numParcelas)
            {
                case 1:
                    iCodOperacao = 130; // VENDA - Cartao 1x
                    break;
                case 2:
                    iCodOperacao = 102; // VENDA - Cartao 2x
                    break;
                case 3:
                    iCodOperacao = 103; // VENDA - Cartao 3x
                    break;
                case 4:
                    iCodOperacao = 141; // VENDA - Cartao 4x
                    break;
                case 5:
                    iCodOperacao = 142; // VENDA - Cartao 5x
                    break;
                case 6:
                    iCodOperacao = 143; // VENDA - Cartao 6x
                    break;
                case 7:
                    iCodOperacao = 144; // VENDA - Cartao 7x
                    break;
                case 8:
                    iCodOperacao = 145; // VENDA - Cartao 8x
                    break;
                case 9:
                    iCodOperacao = 146; // VENDA - Cartao 9x
                    break;
                case 10:
                    iCodOperacao = 147; // VENDA - Cartao 10x
                    break;
            }

            return opDao.getOperacao(iCodOperacao);
        }

        private void txtValorPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
                MessageBox.Show("este campo aceita somente numero e virgula");
            }
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
                MessageBox.Show("este campo aceita somente uma virgula");
            }
        }

        private void Focus_Textbox(TextBox textbox)
        {
            textbox.Focus();
            textbox.SelectAll();
        }

        private void txtValorPago_Validated(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(txtValorPago.Text) < Convert.ToDecimal(lblTotalFatura.Text))
                {
                    MensagemSistema("O valor pago pelo cliente não pode ser menor que o total da fatura.", Color.Brown);
                    Focus_Textbox(txtValorPago);
                    return;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Erro inesperado, digite novamente.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lstTipoPgto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Focus_Textbox(txtValorPago);
            }
        }

        private void txtValorPago_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Tab)
            {
                if (!cboParcelas.Enabled)
                {
                    txtClienteCpf.Focus();
                }
                else
                {
                    cboParcelas.Focus();
                }
            }
        }

        private void txtValorPago_Click(object sender, EventArgs e)
        {
            txtValorPago.SelectAll();
        }

        private void cboParcelas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Tab)
            {
                txtClienteCpf.Focus();
            }
        }


        private void txtClienteCpf_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Finalizar(true);
            }
        }

        private void txtClienteCpf_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtClienteCpf.Text.Trim()))
            {
                txtClienteNome.Text = string.Empty;
                txtClienteEmail.Text = string.Empty;
                txtClienteCelular.Text = string.Empty;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Inicializa_Cliente();

            //// Validando o CPF
            //if (!Validacao.ValidaCPF(txtClienteCpf.Text.Trim()))
            //{
            //    MensagemSistema("CPF inválido!", Color.Brown);
            //    return;
            //}

            //// Buscando o cliente pelo CPF
            //cliente = clienteDao.getCliente_CPF(txtClienteCpf.Text);

            //if (cliente == null)
            //{
            //    MensagemSistema("Cliente não encontrado", Color.Goldenrod);
            //}
            //else
            //{
            //    MensagemSistema("Cliente já cadastrado", Color.Goldenrod);

            //    pedido.cliente = cliente;
            //    pedido.codcli = cliente.codcli;

            //    txtClienteNome.Text = string.IsNullOrEmpty(pedido.cliente.firma) ? "" : pedido.cliente.firma;
            //    txtClienteEmail.Text = string.IsNullOrEmpty(pedido.cliente.firma) ? "" : pedido.cliente.email;
            //    txtClienteCelular.Text = string.IsNullOrEmpty(pedido.cliente.firma) ? "" : pedido.cliente.telefone;
            //}
        }

        private void Inicializa_Cliente()
        {
            try
            {
                // CONSUMIDOR NAO IDENTIFICADO
                if (txtClienteCpf.Text.Trim().Equals(""))
                {
                    pedido.cliente = clienteDao.getCliente_CONSUMIDOR_NAO_IDENTIFICADO();
                    pedido.codcli = pedido.cliente.codcli;
                    return;
                }


                if(rbPessoaFisica.Checked)
                {
                    // Validando o CPF
                    if (!Validacao.ValidaCPF(txtClienteCpf.Text.Trim()))
                    {
                        MensagemSistema("CPF inválido!", Color.Brown);
                        return;
                    }

                    // Verificando se o cliente já existe
                    cliente = clienteDao.getCliente_CPF(txtClienteCpf.Text.Trim());
                }
                else if (rbPessoaJuridica.Checked)
                {
                    // Validando o CNPJ
                    if (!Validacao.IsCnpj(txtClienteCpf.Text.Trim()))
                    {
                        MensagemSistema("CNPJ inválido!", Color.Brown);
                        return;
                    }

                    // Verificando se o cliente já existe
                    cliente = clienteDao.getCliente_CNPJ(txtClienteCpf.Text.Trim());
                }


                // Se o cliente não existir cadastra com os dados informados
                if (cliente == null)
                {
                    // Inserindo no banco e retornando o ID
                    pedido.codcli = rbPessoaFisica.Checked ? Convert.ToInt32(clienteDao.InsertClienteFisica(txtClienteCpf.Text.Trim(), txtClienteNome.Text.Trim(), txtClienteEmail.Text.Trim(), txtClienteCelular.Text.Trim())) : Convert.ToInt32(clienteDao.InsertClienteJuridica(txtClienteCpf.Text.Trim(), txtClienteNome.Text.Trim(), txtClienteEmail.Text.Trim(), txtClienteCelular.Text.Trim()));
                    pedido.cliente = clienteDao.getCliente(pedido.codcli.ToString());
                }
                else
                {
                    MensagemSistema("Cliente já cadastrado", Color.Goldenrod);

                    pedido.cliente = cliente;
                    pedido.codcli = cliente.codcli;

                    txtClienteNome.Text = string.IsNullOrEmpty(pedido.cliente.firma) ? "" : pedido.cliente.firma;
                    txtClienteEmail.Text = string.IsNullOrEmpty(pedido.cliente.firma) ? "" : pedido.cliente.email;
                    txtClienteCelular.Text = string.IsNullOrEmpty(pedido.cliente.firma) ? "" : pedido.cliente.telefone;
                }
            }
            catch (Exception)
            {
                MensagemSistema("Ocorreu um erro inesperado ao selecionar o cliente, tente novamente.", Color.Brown);
                return;
            }
        }


        // ========================================================================
        private bool Gera_NFCe()
        {

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

                    MensagemSistema("Arquivo Gerado ...", Color.OliveDrab);

                    // Assinando o XML
                    xmlNFe_Assinado = assinarXml.AssinaXML(xmlNFe.InnerXml, "infNFe", Certificado.getInstance.oCertificado);
                }
                catch (Exception ex)
                {
                    Log_Exception.Monta_ArquivoLog(ex);
                    MensagemSistema("** Erro ao ASSINAR XML NFC-e, tente novamente **", Color.Brown);
                    MessageBox.Show("Erro: " + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                
                MensagemSistema("Arquivo Assinado ...", Color.OliveDrab);



                if (GerarXML.str_Ambiente == "2")
                {
                    //Salvando o arquivo XML na pasta
                    Grava = File.CreateText(@"C:\Users\Admin\Desktop\ASSINADO.xml");
                    Grava.Write(xmlNFe_Assinado.InnerXml);
                    Grava.Close();
                }
                

                try
                {
                    // Validando o XML
                    retValidar = validarXml.Valida(xmlNFe_Assinado, "NFe");

                    urlQRCode = gerarXml.Gera_Url_QRCode(xmlNFe_Assinado, pedido);

                    //Inserindo a URL QRCode no xml já assinado
                    xmlNFe_Assinado.LoadXml(xmlNFe_Assinado.InnerXml.Replace("</infNFe>", "</infNFe><infNFeSupl><qrCode><![CDATA[" + urlQRCode + "]]></qrCode><urlChave>http://www4.fazenda.rj.gov.br/consultaNFCe/QRCode</urlChave></infNFeSupl>"));
                }
                catch (Exception ex)
                {
                    Log_Exception.Monta_ArquivoLog(ex);

                    MensagemSistema("** Erro ao VALIDAR XML NFC-e **", Color.Brown);
                    MessageBox.Show("Erro: " + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (retValidar == string.Empty)
                {
                    try
                    {
                        MensagemSistema("Enviando a NFC-e", Color.OliveDrab);

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

                                        MensagemSistema("Autorizado o uso da NFC-e", Color.OliveDrab);

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


                                        if (GerarXML.str_Ambiente == "2")
                                        {
                                            //Salvando o arquivo XML na pasta
                                            Grava = File.CreateText(@"C:\Users\Admin\Desktop\EMITIDO.xml");
                                            Grava.Write(pedido.xml.arquivoXML);
                                            Grava.Close();
                                        }



                                        //Salva arquivo XML no Banco SQL (NFe)
                                        if (xmlDao.Grava_XML(pedido.xml))
                                        {
                                            // Atualizando o pedido com a Chave, Protocolo, e statNFCe
                                            if (pedidoDao.Update_ChaveProtocolo_condDoc_StatNFCe(pedido.numdoc, pedido.chave, pedido.protocolo, pedido.xml.statNFCe))
                                            {
                                                if(GerarXML.str_Ambiente == "1")
                                                {
                                                    if (!string.IsNullOrEmpty(Controle.getInstance.caminho_XMLAutorizado))
                                                    {
                                                        //Salvando o arquivo XML na pasta
                                                        Grava = File.CreateText(Controle.getInstance.caminho_XMLAutorizado.Remove(Controle.getInstance.caminho_XMLAutorizado.Length-1) + DateTime.Now.Month + @"\" + pedido.chave + "-procNfe.xml");
                                                        Grava.Write(pedido.xml.arquivoXML);
                                                        Grava.Close();   
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Log_Exception.Monta_ArquivoLog(ex);
                                        MensagemSistema("** NOTA EMITIDA **, mas houve um erro inesperado", Color.Brown);
                                        MessageBox.Show("Erro: " + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return false;
                                    }
                                }
                                else
                                {
                                    MensagemSistema("Erro ao Transmitir(004) XML NFC-e para SEFAZ", Color.Brown);
                                    MessageBox.Show("Erro: " + xmlRetorno.GetElementsByTagName("xMotivo")[1].InnerText, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return false;
                                }
                            }
                            else
                            {
                                MensagemSistema("Erro ao Transmitir(003) XML NFC-e para SEFAZ", Color.Brown);
                                MessageBox.Show("Erro: " + xmlRetorno.GetElementsByTagName("xMotivo")[0].InnerText, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                        else
                        {
                            MensagemSistema("Erro ao Transmitir(002) XML NFC-e para SEFAZ", Color.Brown);
                            MessageBox.Show("Erro: " + retTransmitir, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Log_Exception.Monta_ArquivoLog(ex);

                        MensagemSistema("Erro ao Transmitir(001) XML NFC-e para SEFAZ", Color.Brown);
                        MessageBox.Show("Erro: " + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MensagemSistema("Erro ao validar XML NFC-e", Color.Brown);
                    MessageBox.Show("Erro XML Shema: " + retValidar, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log_Exception.Monta_ArquivoLog(ex);
                MensagemSistema("Ocorreu um erro inesperado, informe ao administrador do sistema!", Color.Brown);
                MessageBox.Show("Erro : " + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }



        private void rbConvenio_CheckedChanged(object sender, EventArgs e)
        {
            cboConvenio.Visible = true;
            txtDescDinheiro.Visible = false;
            txtDescPorcentagem.Visible = false;
        }

        private void rbDinheiro_CheckedChanged(object sender, EventArgs e)
        {
            iDesconto = 0;
            cboConvenio.SelectedValue = 0;
            lblDesconto.Text = "0.00";
            lblTotalFatura.Text = pedido.valdoc.ToString("0.00");
            txtValorPago.Text = lblTotalFatura.Text;
            lblTroco.Text = "0,00";

            cboConvenio.Visible = false;
            txtDescDinheiro.Visible = true;
            txtDescPorcentagem.Visible = false;
            Focus_Textbox(txtDescDinheiro);
        }

        private void rbPorcentagem_CheckedChanged(object sender, EventArgs e)
        {
            iDesconto = 0;
            cboConvenio.SelectedValue = 0;
            lblDesconto.Text = "0.00";
            lblTotalFatura.Text = pedido.valdoc.ToString("0.00");
            txtValorPago.Text = lblTotalFatura.Text;
            lblTroco.Text = "0,00";

            cboConvenio.Visible = false;
            txtDescDinheiro.Visible = false;
            txtDescPorcentagem.Visible = true;
            Focus_Textbox(txtDescPorcentagem);
        }

        private void txtDescDinheiro_TextChanged(object sender, EventArgs e)
        {
            if (txtDescDinheiro.Text.Equals("0"))
            {
                iDesconto = 0;

                lblDesconto.Text = "0.00";
                lblTotalFatura.Text = pedido.valdoc.ToString("0.00");

                txtValorPago.Text = lblTotalFatura.Text;
                lblTroco.Text = "0,00";
            }
            else
            {
                try
                {
                    FormataTextbox.TextBoxMoeda(ref txtDescDinheiro);

                    iDesconto = Convert.ToInt16(txtDescPorcentagem.Text.Trim());
                    Aplica_DescontoaVista();
                }
                catch (Exception)
                {
                    MessageBox.Show(@"Valor Recebido digitado incorretamente!");
                }
            }
        }

        private void txtDescPorcentagem_TextChanged(object sender, EventArgs e)
        {
            if (txtDescPorcentagem.Text.Trim().Equals(string.Empty))
                txtDescPorcentagem.Text = "0";

            if (txtDescPorcentagem.Text.Equals("0"))
            {
                iDesconto = 0;

                lblDesconto.Text = "0.00";
                lblTotalFatura.Text = pedido.valdoc.ToString("0.00");

                txtValorPago.Text = lblTotalFatura.Text;
                lblTroco.Text = "0,00";
            }
            else
            {
                iDesconto = Convert.ToInt16(txtDescPorcentagem.Text.Trim());
                Aplica_DescontoaVista();
            }
        }

        private void txtDescDinheiro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Tab)
            {
                Focus_Textbox(txtValorPago);
            }
        }

        private void txtDescDinheiro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
                MessageBox.Show("este campo aceita somente numero e virgula");
            }
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
                MessageBox.Show("este campo aceita somente uma virgula");
            }
        }

        private void txtDescPorcentagem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Tab)
            {
                Focus_Textbox(txtValorPago);
            }
        }

        private void txtDescPorcentagem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("este campo aceita somente numero");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Finalizar(true);
        }

        private void txtDescDinheiro_Click(object sender, EventArgs e)
        {
            txtDescDinheiro.SelectionStart = txtDescDinheiro.Text.Length;

            if (txtDescDinheiro.Text == "0,00")
                txtDescDinheiro.SelectAll();
        }
    }
}