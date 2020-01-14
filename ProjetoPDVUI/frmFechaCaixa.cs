using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjetoPDVModelos;
using ProjetoPDVDao;
using PetaPoco;


namespace ProjetoPDVUI
{
    public partial class frmFechaCaixa : Form
    {
        FecCaixaDao fecCaixaoDao = new FecCaixaDao();
        FecCaixa fecCaixa = new FecCaixa();
        TextBox txtControl = new TextBox();

        Produto produto = new Produto();
        Pedido pedido = new Pedido();
        List<PedidoItem> lstPeditoItem = new List<PedidoItem>();
        decimal total = 0;
        string msg = string.Empty;

        bool editLst = false;
        bool editando = false;

        bool ret = false;

        public frmFechaCaixa()
        {
            InitializeComponent();
        }

        private void frmFechaCaixa_Load(object sender, EventArgs e)
        {

            btnSalvar.Image = System.Drawing.Bitmap.FromFile(@"Imagens\disk.png");
            btnSair.Image = System.Drawing.Bitmap.FromFile(@"Imagens\cross.png");

            txtCodUser.Text = Usuario.getInstance.codUser.ToString();
            txtNomeUser.Text = Usuario.getInstance.nomeUser;

            txtDataAtual.Text = DateTime.Now.ToString();


            Inicia_Tab_FecCaixa();
            Inicia_Tab_PerdaDoDia();
        }

        private void Inicia_Tab_PerdaDoDia()
        {
            try
            {
                pedido = (new PedidoDao()).getPedido_do_Dia();

                if(pedido != null)
                {
                    lstPeditoItem = (new PedidoItemDao()).getlst_Itens(pedido.numdoc);
                    total = pedido.valdoc;

                    foreach (PedidoItem pitem in lstPeditoItem)
                    {
                        pitem.produto = (new ProdutoDao()).getProduto(pitem.codpro);
                        Lista_Produto(pitem.produto, pitem.qtditens);
                    }
                    
                    editando = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar os valores do ´Pedido do Dia´, informe imediatamnente ao administrador do sistema!", "Mensagem - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Inicia_Tab_FecCaixa()
        {
            try
            {
                //fecCaixa = fecCaixaoDao.getFecCaixa_do_Dia_F(Usuario.getInstance.loja);
                fecCaixa = fecCaixaoDao.getFecCaixa_do_Dia_F();

                //valor fixo
                txtVal_DinheiroF.Text = fecCaixa.val_dinheirof.ToString("0.00");
                txtVal_DebitoF.Text = fecCaixa.val_debitof.ToString("0.00");
                txtVal_CreditoF.Text = fecCaixa.val_creditof.ToString("0.00");
                txtVal_OutrosF.Text = fecCaixa.val_outrosf.ToString("0.00");

                //diferença
                txtVal_DinheiroDIF.Text = fecCaixa.val_dinheirof.ToString("0.00");
                txtVal_DebitoDIF.Text = fecCaixa.val_debitof.ToString("0.00");
                txtVal_CreditoDIF.Text = fecCaixa.val_creditof.ToString("0.00");
                txtVal_OutrosDIF.Text = fecCaixa.val_outrosf.ToString("0.00");

                txtDevolucao.Text = (fecCaixa.Devolucao * -1).ToString("0.00");

                //Total do Sistema abatendo a devolução
                txtTotalFixo.Text = ((fecCaixa.val_dinheirof + fecCaixa.val_debitof + fecCaixa.val_creditof + fecCaixa.val_outrosf) - fecCaixa.Devolucao).ToString("0.00");

                fecCaixa = null;

                fecCaixa = new FecCaixa();
                fecCaixa = fecCaixaoDao.getFecCaixa_do_Dia_User(Usuario.getInstance.loja);
                if (fecCaixa != null)
                {
                    txtVal_DinheiroUser.Text = fecCaixa.val_dinheirouser.ToString("0.00");
                    txtVal_DebitoUser.Text = fecCaixa.val_debitouser.ToString("0.00");
                    txtVal_CreditoUser.Text = fecCaixa.val_creditouser.ToString("0.00");
                    txtVal_OutrosUser.Text = fecCaixa.val_outrosuser.ToString("0.00");
                    //Total do usuário
                    txtTotalUsuario.Text = (fecCaixa.val_dinheirouser + fecCaixa.val_debitouser + fecCaixa.val_creditouser + fecCaixa.val_outrosuser).ToString("0.00");

                    txtObs.Text = fecCaixa.observacao;

                    ret = true;
                }

                //Total diferença
                txtTotalDiferenca.Text = (Convert.ToDecimal(txtTotalFixo.Text) - Convert.ToDecimal(txtTotalUsuario.Text)).ToString("0.00");
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao buscar os valores do ´Fechamento de Caixa´, informe imediatamnente ao administrador do sistema!", "Mensagem - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                //
                if (lstvwProduto.Items.Count == 0)
                {
                    if (MessageBox.Show("Confirma que não houve Perdas do Dia ?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                        return;
                }

                
                var fecCaixaSalvar = new FecCaixa();

                fecCaixaSalvar.data = DateTime.Now;
                fecCaixaSalvar.coduser = Usuario.getInstance.codUser;
                fecCaixaSalvar.nomeuser = Usuario.getInstance.nomeUser;

                // Valores informados pelo sistema
                fecCaixaSalvar.val_dinheirof = Convert.ToDecimal(txtVal_DinheiroF.Text.Trim());
                fecCaixaSalvar.val_debitof = Convert.ToDecimal(txtVal_DebitoF.Text.Trim());
                fecCaixaSalvar.val_creditof = Convert.ToDecimal(txtVal_CreditoF.Text.Trim());
                fecCaixaSalvar.val_outrosf = Convert.ToDecimal(txtVal_OutrosF.Text.Trim());
                //  Valores informados pelo Usuário
                fecCaixaSalvar.val_dinheirouser = Convert.ToDecimal(txtVal_DinheiroUser.Text.Trim());
                fecCaixaSalvar.val_debitouser = Convert.ToDecimal(txtVal_DebitoUser.Text.Trim());
                fecCaixaSalvar.val_creditouser = Convert.ToDecimal(txtVal_CreditoUser.Text.Trim());
                fecCaixaSalvar.val_outrosuser = Convert.ToDecimal(txtVal_OutrosUser.Text.Trim());
                // Diferenças
                fecCaixaSalvar.diferencaDinheiro = Convert.ToDecimal(txtVal_DinheiroDIF.Text.Trim());
                fecCaixaSalvar.diferencaDebito = Convert.ToDecimal(txtVal_DebitoDIF.Text.Trim());
                fecCaixaSalvar.diferencaCredito = Convert.ToDecimal(txtVal_CreditoDIF.Text.Trim());
                fecCaixaSalvar.diferencaOutros = Convert.ToDecimal(txtVal_OutrosDIF.Text.Trim());
                // Observação
                fecCaixaSalvar.observacao = txtObs.Text.Trim();

                if (Usuario.getInstance.loja.Equals(0))
                    fecCaixaSalvar.loja = 0;
                else
                    fecCaixaSalvar.loja = 1;


                if (ret)
                {
                    fecCaixaSalvar.id = fecCaixaoDao.get_id_FecCaixa_do_Dia_User(Usuario.getInstance.loja);
                    if (fecCaixaoDao.Update(fecCaixaSalvar))
                        //MessageBox.Show("Fechamento de caixa atualizado com sucesso!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        msg = "Fechamento de caixa atualizado com sucesso!";
                }
                else
                {
                    ret = fecCaixaoDao.InsertFecCaixa(fecCaixaSalvar);

                    if (ret)
                        msg = "Fechamento de caixa inserido com sucesso!";
                        //MessageBox.Show("Fechamento de caixa inserido com sucesso!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                fecCaixaSalvar = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Houve um erro inesperado ao salvar o fechamento de caixa no banco de dados" + Environment.NewLine + "Informe imediatamente ao administrador do sistema" + Environment.NewLine + "Erro: " + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (lstvwProduto.Items.Count > 0)
                FinalizaPerdasDoDia();

            EnviaRelatorioPorEmail();

            MessageBox.Show(msg, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void EnviaRelatorioPorEmail()
        {
            try
            {
                pBar1.Visible = true;
                pBar1.Minimum = 1;
                this.Cursor = Cursors.WaitCursor;


                Email mail = new Email();
                List<PedidoItem> lstPItem = new List<PedidoItem>();
                List<PedidoItem> lstPerdasItem = new List<PedidoItem>();

                ProdutoDao pdao = new ProdutoDao();

                pBar1.Maximum = lstPItem.Count;
                pBar1.Step = 1;


                lstPItem = (new PedidoItemDao()).getlst_Itens_AcumuladodoMes(DateTime.Now);
                for (int i = 0; i <= lstPItem.Count - 1; i++)
                {
                    lstPItem[i].produto = pdao.getProduto(lstPItem[i].codpro);
                }

                lstPerdasItem = (new PedidoItemDao()).getlst_Itens_PerdasdoDia(DateTime.Now);
                for (int i = 0; i <= lstPerdasItem.Count - 1; i++)
                {
                    lstPerdasItem[i].produto = pdao.getProduto(lstPerdasItem[i].codpro);
                }


                Movimentacao mov = new Movimentacao();
                mov = (new MovimentacaoDao()).getTotal_de_Vendas(DateTime.Now);

                // Enviando o email
                mail.Email_ConferenciaCaixa(lstPItem, lstPerdasItem, mov);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível enviar o relatório!" + Environment.NewLine + "Mensagem do erro: " + ex.Message, "Erro ao enviar Email", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                pBar1.Visible = false;
            }
        }

        private void frmFechaCaixa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtCodPro_DoubleClick(object sender, EventArgs e)
        {
            frmPesquisaProduto frmPesquisa = new frmPesquisaProduto();
            frmPesquisa.ShowDialog();

            produto = frmPesquisa.produto;

            if (produto != null)
            {
                Seleciona_Produto(produto);
                txtQuantidade.Focus();
            }
        }


        private void Seleciona_Produto(Produto p)
        {
            txtCodPro.Text = p.codpro.ToString();
            txtDescricao.Text = p.descricao;
            txtPrcVenda.Text = p.prcvenda.ToString("0.00");
            txtEstoque.Text = p.estoque.ToString();
        }

        private void Lista_Produto(Produto p, int quantidade)
        {
            try
            {
                ListViewItem ls = new ListViewItem(p.codpro.ToString());

                ls.SubItems.Add(quantidade.ToString());
                ls.SubItems.Add(p.prcvenda.ToString("0.00"));
                ls.SubItems.Add((p.prcvenda * quantidade).ToString("0.00"));
                ls.SubItems.Add(p.descricao);

                lstvwProduto.Items.Add(ls);

            }
            catch (Exception ex)
            {
                Log_Exception.Monta_ArquivoLog(ex);
                MessageBox.Show("Ocorreu um erro inesperado! " + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtQuantidade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Tab || e.KeyData == Keys.Enter) 
                btnGravar.Focus();
        }

        private void txtCodPro_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Enter)
            {
                produto = (new ProdutoDao()).getProduto(Convert.ToInt32(txtCodPro.Text));

                if (produto != null)
                {
                    Seleciona_Produto(produto);
                    txtQuantidade.Focus();
                }

            }
        }

        private void txtCodPro_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtCodPro.Text.Trim() != string.Empty)
            {
                limpaText();
            }
        }

        private void limpaText()
        {
            txtCodPro.Text = string.Empty;
            txtQuantidade.Text = string.Empty;
            txtPrcVenda.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            txtEstoque.Text = string.Empty;
        }

        private void txtQuantidade_Enter_1(object sender, EventArgs e)
        {
            if (txtCodPro.Text.Trim() != string.Empty)
            {
                produto = (new ProdutoDao()).getProduto(Convert.ToInt32(txtCodPro.Text));

                if (produto != null)
                {
                    Seleciona_Produto(produto);
                }
            }
        }

        private void btnGravar_Click_1(object sender, EventArgs e)
        {
            if (txtCodPro.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Selecione o produto.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            if (txtQuantidade.Text.Trim() == string.Empty || Convert.ToInt32(txtQuantidade.Text) == 0)
            {
                MessageBox.Show("Quantidade digitada inválida!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtQuantidade.Focus();
                return;
            }

            try
            {
                if (editLst)
                {
                    btnExcluir_Click(sender, e);
                }

                lstPeditoItem.Add(new PedidoItem(pedido, produto, produto.codpro, Convert.ToInt32(txtQuantidade.Text), 0, 0, Convert.ToDecimal(txtPrcVenda.Text), (Convert.ToDecimal(txtPrcVenda.Text) * Convert.ToInt32(txtQuantidade.Text))));

                Lista_Produto(produto, Convert.ToInt16(txtQuantidade.Text));
                total += Convert.ToDecimal(txtPrcVenda.Text) * Convert.ToInt32(txtQuantidade.Text);
                
                limpaText();
                editLst = false;

                txtCodPro.Focus();
            }
            catch (Exception)
            {
                limpaText();
                editLst = false;                
            }
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            if (lstvwProduto.FocusedItem == null)
            {
                return;
            }

            produto = (new ProdutoDao()).getProduto(lstPeditoItem[lstvwProduto.FocusedItem.Index].codpro);

            limpaText();
            Seleciona_Produto(produto);
            txtQuantidade.Focus();

            editLst = true;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {

                if (lstvwProduto.FocusedItem == null)
                {
                    return;
                }


                int codPro = Convert.ToInt32(lstvwProduto.SelectedItems[0].SubItems[0].Text);

                if (editando && !editLst)
                {
                    Database db = new Database("stringConexao");

                    try
                    {
                        db.BeginTransaction();

                        int qtd = 0;
                        qtd = db.SingleOrDefault<Int32>("Select Qtditens from movitens where numdoc = @0 and codpro = @1", pedido.numdoc, codPro);

                        if (qtd != 0)
                            if (!Atualiza_Estoque(db, codPro, qtd))
                                throw new Exception("");

                        db.CompleteTransaction();
                    }
                    catch (Exception ex)
                    {
                        db.AbortTransaction();
                        MessageBox.Show("Ocorreu um erro inesperado ao exclir esse item, tente novamente... Erro : " + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }


                total -= Convert.ToDecimal(lstvwProduto.SelectedItems[0].SubItems[3].Text);

                lstPeditoItem.RemoveAll(delegate(PedidoItem p) { return p.codpro == codPro; });

                lstvwProduto.SelectedItems[0].Remove();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro inesperado, tente novamente... Erro : " + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FinalizaPerdasDoDia()
        {

            if (lstvwProduto.Items.Count == 0)
                return;


            int iQtd;
            int iDif;
            var db = new Database("stringConexao");

            try
            {

                //Iniciando a transação
                db.BeginTransaction();


                //if (pedido == null)
                if (!editando)
                {
                    pedido = new Pedido();

                    pedido.datadigitacao = DateTime.Now;
                    pedido.datanfiscal = DateTime.Now;
                    pedido.datavencimento = DateTime.Now;
                    pedido.codoperacao = 905; // Operação: Perdas
                    pedido.nfiscal = "0";
                    pedido.codcli = 2;
                    pedido.coduser = Usuario.getInstance.codUser;
                    pedido.cndfrete = "FOB";
                    pedido.codvendedor = 105;
                    pedido.modelo = "65";
                    pedido.codtipopgto = 13;
                    pedido.codtransp = 1;
                    pedido.statNFCe = "0";
                    pedido.valdoc = total;
                    pedido.conddoc = "F";

                    pedido.lstPedidoItem = new List<PedidoItem>();
                    pedido.lstPedidoItem = lstPeditoItem;

                    //Inserindo o pedido na tabela Movdb
                    pedido.numdoc = Convert.ToInt32(db.Insert(pedido));
                    //pedido.numdoc = Convert.ToInt32(db.Insert("INSERT INTO Movdb (codcli,codvendedor,codoperacao,datadigitacao,valdoc,modelo,conddoc,codtransp,cndfrete,coduser,codloja,statnfce) VALUES(2,105,905,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + total.ToString("0.00").Replace(",", ".") + ",'65','F',1,'FOB'," + Usuario.getInstance.codUser + ",1,'0')"));

                    
                    foreach (PedidoItem pItem in pedido.lstPedidoItem)
                    {
                        if (!Atualiza_Estoque(db, pItem.codpro, (pItem.qtditens * -1)))
                            throw new Exception("");
                    }
                    

                    //Inserindo os Itens na tabela MOVITENS
                    db.Insert_lstPedidos(pedido.numdoc, pedido.lstPedidoItem);
                    
                    editando = true;
                }
                else
                {
                    foreach (PedidoItem pItem in pedido.lstPedidoItem)
                    {
                        iDif = 0;
                        iQtd = 0;

                        iQtd = db.SingleOrDefault<Int32>("Select Qtditens from movitens where numdoc = @0 and codpro = @1", pedido.numdoc, pItem.codpro);

                        if (iQtd != 0)
                        {
                            iDif = iQtd - pItem.qtditens;

                            if (iDif != 0)
                            {
                                if (!Atualiza_Estoque(db, pItem.codpro, iDif))
                                    throw new Exception("");
                            }
                        }
                    }

                    if (db.Execute("DELETE FROM MovItens WHERE NumDoc = " + pedido.numdoc) > 0)
                    {
                        db.Insert_lstPedidos(pedido.numdoc, lstPeditoItem);
                        db.Execute("UPDATE Movdb SET ValDoc = " + total.ToString("0.00").Replace(",", ".") + " WHERE NumDoc = " + pedido.numdoc);
                    }
                }


                //Commit
                db.CompleteTransaction();
                MessageBox.Show("Perdas do Dia inseridos com sucesso.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                //RollBack
                db.AbortTransaction();
                //editando = false;

                MessageBox.Show("Houve um erro inesperado ao finalizar o Pedido de Perda." + Environment.NewLine + "Tente Novamente.." + Environment.NewLine + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private bool Atualiza_Estoque(Database db1, int codPro, int quantidade) 
        {
            try
            {
                db1.BeginTransaction();

                db1.Update("UPDATE produto " +
                          "SET Estoque = Estoque + " + quantidade +
                          " WHERE CodPro = " + codPro);

                db1.CompleteTransaction();
            }
            catch (Exception)
            {
                db1.AbortTransaction();
                return false;
            }
            
            
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmFechaCaixa_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(pedido != null && lstvwProduto.Items.Count == 0)
            {
                Database db = new Database("stringConexao");

                try
                {
                    db.BeginTransaction();

                    db.Execute("DELETE FROM MovItens WHERE NumDoc = " + pedido.numdoc);
                    db.Execute("DELETE FROM Movdb WHERE NumDoc = " + pedido.numdoc);

                    db.CompleteTransaction();
                }
                catch (Exception ex)
                {
                    db.AbortTransaction();
                    MessageBox.Show("Houve um erro inesperado ao deletar o registro de ´Perdas do Dia´ do banco de dados." + Environment.NewLine + "Informe imediatamente ao administrador do sistema!" + Environment.NewLine + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtTotalUsuario_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Total diferença
                txtTotalDiferenca.Text = (Convert.ToDecimal(txtTotalFixo.Text) - Convert.ToDecimal(txtTotalUsuario.Text)).ToString("0.00");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Houve um erro inesperado " + ex.Message);
            }
        }

        private void txtVal_DinheiroUser_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal val_DinheiroUser = txtVal_DinheiroUser.Text.Trim().Equals("") ? 0 : Convert.ToDecimal(txtVal_DinheiroUser.Text.Trim());

                txtVal_DinheiroDIF.Text = (Convert.ToDecimal(txtVal_DinheiroF.Text) - val_DinheiroUser).ToString("0.00");
                txtTotalUsuario.Text = (val_DinheiroUser + Convert.ToDecimal(txtVal_DebitoUser.Text) + Convert.ToDecimal(txtVal_CreditoUser.Text) + Convert.ToDecimal(txtVal_OutrosUser.Text)).ToString("0.00");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void txtVal_DinheiroUser_KeyPress_1(object sender, KeyPressEventArgs e)
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

        private void txtVal_DinheiroUser_Leave(object sender, EventArgs e)
        {
            if (txtVal_DinheiroUser.Text.Trim().Equals(""))
            {
                txtVal_DinheiroUser.Text = "0,00";
            }
        }

        private void txtVal_DinheiroUser_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtVal_DinheiroUser.Text == "0,00")
                txtVal_DinheiroUser.Text = string.Empty;
        }

        private void txtVal_CreditoUser_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                decimal val_CreditoUser = txtVal_CreditoUser.Text.Trim().Equals("") ? 0 : Convert.ToDecimal(txtVal_CreditoUser.Text.Trim());

                txtVal_CreditoDIF.Text = (Convert.ToDecimal(txtVal_CreditoF.Text) - val_CreditoUser).ToString("0.00");
                txtTotalUsuario.Text = (val_CreditoUser + Convert.ToDecimal(txtVal_DebitoUser.Text) + Convert.ToDecimal(txtVal_DinheiroUser.Text) + Convert.ToDecimal(txtVal_OutrosUser.Text)).ToString("0.00");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void txtVal_CreditoUser_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (txtVal_CreditoUser.Text == "0,00")
                txtVal_CreditoUser.Text = string.Empty;
        }

        private void txtVal_CreditoUser_Leave(object sender, EventArgs e)
        {
            if (txtVal_CreditoUser.Text.Trim().Equals(""))
            {
                txtVal_CreditoUser.Text = "0,00";
            }
        }

        private void txtVal_DebitoUser_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                decimal val_DebitoUser = txtVal_DebitoUser.Text.Trim().Equals("") ? 0 : Convert.ToDecimal(txtVal_DebitoUser.Text.Trim());

                txtVal_DebitoDIF.Text = (Convert.ToDecimal(txtVal_DebitoF.Text) - val_DebitoUser).ToString("0.00");
                txtTotalUsuario.Text = (val_DebitoUser + Convert.ToDecimal(txtVal_CreditoUser.Text) + Convert.ToDecimal(txtVal_DinheiroUser.Text) + Convert.ToDecimal(txtVal_OutrosUser.Text)).ToString("0.00");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void txtVal_DebitoUser_Leave(object sender, EventArgs e)
        {
            if (txtVal_DebitoUser.Text.Trim().Equals(""))
            {
                txtVal_DebitoUser.Text = "0,00";
            }
        }

        private void txtVal_DebitoUser_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtVal_DebitoUser.Text == "0,00")
                txtVal_DebitoUser.Text = string.Empty;
        }

        private void txtVal_OutrosUser_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal val_OutrosUser = txtVal_OutrosUser.Text.Trim().Equals("") ? 0 : Convert.ToDecimal(txtVal_OutrosUser.Text.Trim());

                txtVal_OutrosDIF.Text = (Convert.ToDecimal(txtVal_OutrosF.Text) - val_OutrosUser).ToString("0.00");
                txtTotalUsuario.Text = (val_OutrosUser + Convert.ToDecimal(txtVal_CreditoUser.Text) + Convert.ToDecimal(txtVal_DinheiroUser.Text) + Convert.ToDecimal(txtVal_DebitoUser.Text)).ToString("0.00");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void txtVal_OutrosUser_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtVal_OutrosUser.Text == "0,00")
                txtVal_OutrosUser.Text = string.Empty;
        }

        private void txtVal_OutrosUser_Leave(object sender, EventArgs e)
        {
            if (txtVal_OutrosUser.Text.Trim().Equals(""))
            {
                txtVal_OutrosUser.Text = "0,00";
            }
        }

        private void txtTotalUsuario_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                //Total diferença
                txtTotalDiferenca.Text = (Convert.ToDecimal(txtTotalFixo.Text) - Convert.ToDecimal(txtTotalUsuario.Text)).ToString("0.00");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Houve um erro inesperado " + ex.Message);
            }
        }
    }
}
