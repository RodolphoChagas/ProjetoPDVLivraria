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
    public partial class frmListaProduto : Form
    {
        List<Produto> lstProduto = new List<Produto>();
        ProdutoDao pDao = new ProdutoDao();

        public Produto produto;

        int iDisponivel,
            iIndisponivel,
            iPrevenda,
            iBloqueado;




        public frmListaProduto()
        {
            InitializeComponent();
        }

        private void frmListaProduto_Load(object sender, EventArgs e)
        {
            cboLocalizar.SelectedIndex = 0;

            
            if (Usuario.getInstance.loja.Equals(0))
            {
                cklstStatus.SetItemChecked(0, true);
            }
            else
            {
                cklstStatus.Enabled = false;
            }


            cboLoja.SelectedIndex = Usuario.getInstance.loja;


            BuscaProduto();
        }



        private void BuscaProduto()
        {

            try
            {

                lstvwProduto.Items.Clear();
                lstProduto.Clear();


                lblCount.Text = "Itens encontrados: (0)";



                iDisponivel = 0;
                iIndisponivel = 0;
                iPrevenda = 0;
                iBloqueado = 0;

                if (cklstStatus.GetItemChecked(0))
                    iDisponivel = 1;
                if (cklstStatus.GetItemChecked(1))
                    iIndisponivel = 1;
                if (cklstStatus.GetItemChecked(2))
                    iPrevenda = 1;
                if (cklstStatus.GetItemChecked(3))
                    iBloqueado = 1;

                if (cboLocalizar.Text.Equals("Descrição"))
                {
                    if (cboLoja.Text.Equals("Livraria"))
                    {
                        lstProduto = pDao.getLst_Produto_findDescricao(txtDescricao.Text.Trim(), iDisponivel, iIndisponivel, iPrevenda, iBloqueado);
                    }
                    else if (cboLoja.Text.Equals("Cafeteria"))
                        lstProduto = pDao.getLst_Produto_Cafeteria_findDescricao(txtDescricao.Text.Trim(), 0);

                }
                else if (cboLocalizar.Text.Equals("ISBN/EAN"))
                {
                    lstProduto = pDao.getLst_Produto_findISBN(txtDescricao.Text.Trim(), iDisponivel, iIndisponivel, iPrevenda, iBloqueado);
                }



                foreach (Produto p in lstProduto)
                {

                    p.subGrupo = pDao.getSubGrupo(p.codgrupo, p.codsubGrupo);


                    ListViewItem ls = new ListViewItem(p.codpro.ToString());

                    ls.SubItems.Add(p.descricao);
                    ls.SubItems.Add(p.subGrupo.descSub);
                    ls.SubItems.Add(p.estoque.ToString("000"));
                    ls.SubItems.Add(p.prcvenda.ToString("0.00"));

                    lstvwProduto.Items.Add(ls);
                }


                lblCount.Text = "Itens encontrados: (" + lstvwProduto.Items.Count + ")";

            }
            catch (Exception)
            {
                
                throw;
            }
        
        
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lstvwProduto.FocusedItem == null)
            {
                return;
            }


            frmProduto frm = new frmProduto((lstProduto[lstvwProduto.FocusedItem.Index]));
            frm.ShowDialog();

        }
    }
}
