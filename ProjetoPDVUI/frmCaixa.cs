using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ProjetoPDVDao;
using ProjetoPDVModelos;
using ProjetoPDVServico;

namespace ProjetoPDVUI
{
    public partial class frmCaixa : Form
    {
        ProdutoDao pdao = new ProdutoDao();
        Produto_LojaDao plojaDao = new Produto_LojaDao();

        Pedido pedido = new Pedido();
        Produto produto = new Produto();

        List<PedidoItem> lstpedidoItem = new List<PedidoItem>();

        bool statusCaixa;
        int totalItens;
        decimal totalVenda;


        public frmCaixa()
        {
            InitializeComponent();
        }

        private void frmCaixa_Load(object sender, EventArgs e)
        {
            //Fechado
            statusCaixa = false;

            btnFinalizar.Image = System.Drawing.Bitmap.FromFile(@"Imagens\accept.png");
            btnCancelar.Image = System.Drawing.Bitmap.FromFile(@"Imagens\cancel.png");
            pictureBox1.Image = System.Drawing.Bitmap.FromFile(@"Imagens\Logo_Concursar 370x150.png");
        }

        private void Inicia_Venda()
        {
            label6.Text = "Informe o código, código de barras ou descrição do produto";
            label1.Text = "CAIXA EM VENDA";
            label1.ForeColor = Color.ForestGreen;
            txtBusca.Enabled = true;
            txtQuantidade.Enabled = true;
            btnCancelar.Enabled = true;
            btnFinalizar.Enabled = true;

            txtBusca.Focus();


            //Caixa em venda(ABERTO)
            statusCaixa = true;

            //Iniciando o Pedido
            pedido.numdoc = 0;
            pedido.conddoc = "A";
            pedido.coduser = Usuario.getInstance.codUser;
            //pedido.codvendedor = Usuario.getInstance.loja.Equals(0) ? 104 : 105; //104: Livraria   105: Cafeteria
            pedido.modelo = "65";
            pedido.serienfiscal = 1;
            pedido.valdoc = 0;
            pedido.nfiscal = "0";
            pedido.lstPedidoItem = new List<PedidoItem>();
        }

        private void Reinicia_Venda()
        {
            lblProdutoSel.Text = "Selecione um produto";
            lblDescricao.Text = string.Empty;
            txtBusca.Text = string.Empty;
            lblDescricao.Text = string.Empty;
            lblPrcVenda.Text = "0,00";
            lblDesconto.Text = "000";
            lblTotal.Text = "0,00";
            txtQuantidade.Text = "1";

            txtBusca.Focus();
        }

        public void Fecha_Caixa()
        {
            label6.Text = "Pressione F2 para abrir uma nova venda";
            label1.Text = "CAIXA LIVRE";
            label1.ForeColor = Color.DarkRed;
            //Zerando os textbox
            lblTotalItens.Text = "0";
            lblTotalVenda.Text = "0,00";

            //Limpando a lista de itens
            lstItens.Items.Clear();

            //Caixa Livre
            statusCaixa = false;

            txtBusca.Enabled = false;
            txtQuantidade.Enabled = false;
            btnCancelar.Enabled = false;
            btnFinalizar.Enabled = false;


            pedido = new Pedido();
            lstpedidoItem = new List<PedidoItem>();
        }

        private void Finaliza_Venda()
        {
            try
            {
                if (statusCaixa)
                {
                    if (lstItens.Items.Count != 0)
                    {
                        //Finalizando a venda e chamando o Form de Pagamento
                        if (MessageBox.Show("Confirma finalizar a venda ?", "Venda", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {

                            //Verifica_Status_Impressora();

                            pedido.valdoc = Convert.ToDecimal(lblTotalVenda.Text);
                            pedido.lstPedidoItem = lstpedidoItem;
                            pedido.codvendedor = 105;

                            foreach (PedidoItem pItem in lstpedidoItem)
                            {
                                if (pItem.produto.codgrupo.Equals(0))
                                {
                                    pedido.codvendedor = 104;
                                    break;
                                }
                            }


                            //passando o pedido como parametro
                            frmSelecionaPagamento frmSelPag = new frmSelecionaPagamento(pedido, this);
                            frmSelPag.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lista de itens vazia!");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Caixa fechado, aperte F2 para iniciar uma nova venda.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void frmCaixa_KeyDown(object sender, KeyEventArgs e)
        {



            if (e.KeyData == Keys.Escape)
            {
                if (!statusCaixa)
                {
                    Unload_Form();
                    return;
                }

                Reinicia_Venda();
            }
            else if (e.KeyData == Keys.F2)
            {
                if (!statusCaixa)
                {
                    Inicia_Venda();
                }
                else
                {
                    MessageBox.Show("O caixa já está aberto, finalize ou cancele para iniciar novamente!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if (e.KeyData == Keys.F5)
            {
                if (!statusCaixa)
                    return;

                try
                {
                    Finaliza_Venda();
                }
                catch (Exception ex)
                {
                    Log_Exception.Monta_ArquivoLog(ex);
                    MessageBox.Show("Tente novamente " + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if (e.KeyData == Keys.F8)
            {
                if (MessageBox.Show("Confirma fechar a aplicação ?", "Venda", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    Unload_Form();
                }
            }
            else if (e.KeyData == Keys.F4)
            {
                if (!statusCaixa)
                    return;

                frmPesquisaProduto frmPesquisa = new frmPesquisaProduto();
                frmPesquisa.ShowDialog();

                produto = frmPesquisa.produto;

                if (produto != null)
                {
                    Seleciona_Produto(produto);
                    txtQuantidade.Focus();
                }
            }
        }

        private void txtBusca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (txtBusca.Text.Trim().Length != 0)
                {

                    try
                    {

                        if (txtBusca.Text.Length > 10)
                        {
                            produto = pdao.getProduto(txtBusca.Text.Trim());
                        }
                        else if (txtBusca.Text.Length < 10)
                        {
                            produto = pdao.getProduto(Convert.ToInt32(txtBusca.Text.Trim()));
                        }

                        if (produto != null)
                        {

                            Seleciona_Produto(produto);

                            txtQuantidade.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Produto não encontrado, verifique por favor!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Reinicia_Venda();
                        }


                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Houve algum erro ao selecionar o produto digitado, verifique por favor!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Reinicia_Venda();
                    }
                }
                else
                {
                    MessageBox.Show("Informe o código, código de barras ou descrição do produto!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Reinicia_Venda();
                }
            }
        }


        private void Seleciona_Produto(Produto p)
        {
            decimal valDsc = 0;
            decimal valTotal = 0;

            // Informações sobre as aliquotas
            p.subGrupo = pdao.getSubGrupo(p.codgrupo, p.codsubGrupo);

            if (p.codgrupo.Equals(0))
            {
                p.produto_loja = plojaDao.getProduto_Loja(p.codpro);
            }
            else
            {
                Produto_Loja ploja = new Produto_Loja();
                ploja.codpro = p.codpro;
                ploja.desconto = 0;
                ploja.estatus = 0;
                ploja.site = 1;

                p.produto_loja = ploja;
            }



            lblProdutoSel.Text = "Produto selecionado";
            lblDescricao.Text = p.descricao;
            lblPrcVenda.Text = p.prcvenda.ToString("0.00");
            lblDesconto.Text = p.produto_loja.desconto.ToString();

            valDsc = decimal.Round((((p.prcvenda * p.produto_loja.desconto) / 100) * Convert.ToInt32(txtQuantidade.Text)), 2);
            txtValDesc.Text = valDsc.ToString("0.00");

            valTotal = decimal.Round(((p.prcvenda * Convert.ToInt32(txtQuantidade.Text)) - valDsc), 2);
            lblTotal.Text = valTotal.ToString("0.00");
            //lblTotal.Text = ((p.prcvenda * Convert.ToInt32(txtQuantidade.Text)) - ((p.prcvenda * p.produto_loja.desconto) / 100) * Convert.ToInt32(txtQuantidade.Text)).ToString("0.00");
        }


        private void txtQuantidade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                if (produto == null)
                {
                    MessageBox.Show("Pesquise um produto por favor.", "Caixa - Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (Convert.ToDecimal(lblTotal.Text) < 0)
                {
                    MessageBox.Show("Erro no valor total do produto, verifique por favor.", "Caixa - Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (txtQuantidade.Text.Trim() != "" && Convert.ToInt32(txtQuantidade.Text) > 0)
                {
                    AtualizaTotal_ItenseVenda(Convert.ToInt32(txtQuantidade.Text), Convert.ToDecimal(lblTotal.Text));

                    
                    var listItemView = lstItens.FindItemWithText(produto.codpro.ToString());
                    if (listItemView == null)
                    {
                        lstpedidoItem.Add(new PedidoItem(pedido, produto, produto.codpro, Convert.ToInt32(txtQuantidade.Text), Convert.ToDouble(lblDesconto.Text), Convert.ToDecimal(txtValDesc.Text), Convert.ToDecimal(lblPrcVenda.Text), Convert.ToDecimal(lblTotal.Text)));
                        Lista_Produto(produto, Convert.ToDecimal(txtValDesc.Text));
                    }
                    else
                    {
                        var quantidade = Convert.ToInt32(txtQuantidade.Text) + Convert.ToInt32(listItemView.SubItems[2].Text);
                        var total = Convert.ToDecimal(lblTotal.Text) + Convert.ToDecimal(listItemView.SubItems[5].Text);
                        var valorTotalDesconto = Convert.ToDecimal(txtValDesc.Text) + Convert.ToDecimal(listItemView.SubItems[7].Text);
                    
                        lstpedidoItem.Add(new PedidoItem(pedido, produto, produto.codpro, quantidade, Convert.ToDouble(lblDesconto.Text), valorTotalDesconto, Convert.ToDecimal(lblPrcVenda.Text), total));


                        lstItens.Items[listItemView.Index].SubItems[2].Text = quantidade.ToString();
                        lstItens.Items[listItemView.Index].SubItems[5].Text = total.ToString("0.00");
                        
                        //Lista_Produto(produto, valorTotalDesconto);
                    }

                    Reinicia_Venda();

                    produto = null;
                }
                else
                {
                    MessageBox.Show("Informe a quantidade por favor!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }


        private void Lista_Produto(Produto p, decimal valDesc, int quantidade = 0, decimal total = 0)
        {
            try
            {
                ListViewItem ls = new ListViewItem(p.codpro.ToString());


                if (quantidade == 0)
                    quantidade = Convert.ToInt32(txtQuantidade.Text);

                if (total == 0)
                    total = Convert.ToDecimal(lblTotal.Text);


                ls.SubItems.Add(p.descricao);
                //ls.SubItems.Add(txtQuantidade.Text);
                ls.SubItems.Add(quantidade.ToString());
                ls.SubItems.Add(p.prcvenda.ToString("0.00"));
                ls.SubItems.Add(p.produto_loja.desconto.ToString());
                //ls.SubItems.Add(Convert.ToDecimal(lblTotal.Text).ToString("0.00"));
                ls.SubItems.Add(total.ToString("0.00"));
                ls.SubItems.Add(total.ToString("0.00"));
                ls.SubItems.Add(valDesc.ToString("0.00"));

                lstItens.Items.Add(ls);

            }
            catch (Exception ex)
            {
                Log_Exception.Monta_ArquivoLog(ex);
                MessageBox.Show("Ocorreu um erro inesperado! " + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                AtualizaTotal_ItenseVenda(Convert.ToInt32(lstItens.SelectedItems[0].SubItems[2].Text) * -1, Convert.ToDecimal(lstItens.SelectedItems[0].SubItems[5].Text) * -1);

                lstpedidoItem.RemoveAll(delegate(PedidoItem p) { return p.codpro == Convert.ToInt32(lstItens.SelectedItems[0].SubItems[0].Text); });

                lstItens.SelectedItems[0].Remove();

            }
            catch (Exception ex)
            {
                Log_Exception.Monta_ArquivoLog(ex);

                MessageBox.Show("Ocorreu um erro inesperado, tente novamente... Erro : " + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtQuantidade_TextChanged(object sender, EventArgs e)
        {
            if (txtQuantidade.Text.Trim() != "" && Convert.ToInt32(txtQuantidade.Text.Trim()) > 0)
            {
                lblTotal.Text = (((produto.prcvenda * Convert.ToInt32(txtQuantidade.Text)) - ((produto.prcvenda * produto.produto_loja.desconto) / 100) * Convert.ToInt32(txtQuantidade.Text)).ToString("0.00"));
            }
            else
            {
                lblTotal.Text = "0,00";
            }
        }

        private void AtualizaTotal_ItenseVenda(int quantidade, decimal valor)
        {
            totalItens = Convert.ToInt32(lblTotalItens.Text);
            totalVenda = Convert.ToDecimal(lblTotalVenda.Text);

            totalItens += quantidade;
            totalVenda += valor;

            lblTotalItens.Text = totalItens.ToString();
            lblTotalVenda.Text = totalVenda.ToString();
        }



        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            try
            {
                Finaliza_Venda();
            }
            catch (Exception ex)
            {
                Log_Exception.Monta_ArquivoLog(ex);

                MessageBox.Show("Ocorreu um erro inesperado, tente novamente... Erro : " + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Unload_Form();
        }

        private void Unload_Form()
        {
            try
            {
                pdao = null;
                plojaDao = null;
                pedido = null;
                produto = null;
                lstpedidoItem = null;

                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                Log_Exception.Monta_ArquivoLog(ex);
                MessageBox.Show("Ocorreu um erro inesperado, tente novamente... Erro : " + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void txtBusca_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
