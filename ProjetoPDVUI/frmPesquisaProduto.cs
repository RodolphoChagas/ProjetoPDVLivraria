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

namespace ProjetoPDVUI
{
    public partial class frmPesquisaProduto : Form
    {

        List<Produto> lstProduto = new List<Produto>();
        ProdutoDao pDao = new ProdutoDao();

        public Produto produto;

        int iDisponivel,
            iIndisponivel,
            iPrevenda,
            iBloqueado;




        public frmPesquisaProduto()
        {
            InitializeComponent();
        }

        private void btnRetornar_Click(object sender, EventArgs e)
        {
            if (lstvwProduto.Items.Count > 0)
            {
                if (lstvwProduto.SelectedItems[0].Selected)
                {
                    produto = lstProduto[lstvwProduto.SelectedItems[0].Index];

                    btnSair_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Selecione o produto por favor.");
                }
            }
            else
            {
                MessageBox.Show("Digite o produto que deseja retornar.");
                txtDescricao.Focus();
            }
        }

        private void txtDescricao_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lstvwProduto.Items.Clear();
                lstProduto.Clear();


                if (txtDescricao.Text.Trim() == string.Empty)
                    return;


                //                if (Usuario.getInstance.loja.Equals(0))
                //                {

                iDisponivel = 0;
                iIndisponivel = 0;
                iPrevenda = 0;
                iBloqueado = 0;

                if (cboStatus.SelectedIndex.Equals(0))
                    iDisponivel = 1;
                if (cboStatus.SelectedIndex.Equals(1))
                    iIndisponivel = 1;
                if (cboStatus.SelectedIndex.Equals(2))
                    iPrevenda = 1;
                if (cboStatus.SelectedIndex.Equals(3))
                    iBloqueado = 1;

                
                if (cboLocalizar.Text.Equals("Descrição"))
                {
                    if (cboLoja.Text.Equals("Livraria"))
                    {
                        lstProduto = pDao.getLst_Produto_findDescricao(txtDescricao.Text.Trim(), iDisponivel, iIndisponivel, iPrevenda, iBloqueado);
                    }
                    else if (cboLoja.Text.Equals("Cafeteria"))
                        lstProduto = pDao.getLst_Produto_Cafeteria_findDescricao(txtDescricao.Text.Trim(), (cboStatus.SelectedIndex > 1 ? 1 : cboStatus.SelectedIndex));

                }
                else if (cboLocalizar.Text.Equals("ISBN/EAN"))
                {
                    lstProduto = pDao.getLst_Produto_findISBN(txtDescricao.Text.Trim(), iDisponivel, iIndisponivel, iPrevenda, iBloqueado);
                }


                foreach (Produto p in lstProduto)
                {
                    ListViewItem ls = new ListViewItem(p.codpro.ToString());

                    ls.SubItems.Add(p.descricao);
                    ls.SubItems.Add("pendente");
                    ls.SubItems.Add(p.estoque.ToString("000"));
                    ls.SubItems.Add(p.prcvenda.ToString("0.00"));

                    lstvwProduto.Items.Add(ls);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Erro inesperado ao se comunicar com o banco de dados", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw;
            }
        }

        private void txtDescricao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (this.lstvwProduto.Items.Count > 0)
                {
                    this.lstvwProduto.Focus();
                    this.lstvwProduto.Items[0].Selected = true;
                }
            }
        }

        private void lstvwProduto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstvwProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnRetornar.Focus();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            lstProduto = null;
            pDao = null;
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            btnSair_Click(sender, e);
        }

        private void frmPesquisaProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                produto = null;
                btnSair_Click(sender, e);
            }
        }

        private void frmPesquisaProduto_Load(object sender, EventArgs e)
        {
            //if (Usuario.getInstance.loja.Equals(0))
            //{
            //    cklstStatus.SetItemChecked(0, true);
            //}
            //else
            //{
            //    cklstStatus.SetItemChecked(0, true);
            //    cklstStatus.Enabled = false;
            //}

            cboStatus.SelectedIndex = 0;

            cboLocalizar.SelectedIndex = 0;
            cboLoja.SelectedIndex = Usuario.getInstance.loja;
        }

        private void rectangleShape1_Click(object sender, EventArgs e)
        {

        }
    }
}
