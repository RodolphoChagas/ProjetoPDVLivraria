using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjetoPDVModelos;

namespace ProjetoPDVUI
{
    public partial class frmProduto : Form
    {

        Produto p = new Produto();

        public frmProduto()
        {
            InitializeComponent();
        }

        public frmProduto(Produto produto)
        {
            InitializeComponent();

            p = produto;

            try
            {
                txtCodPro.Text = p.codpro.ToString();
                txtDescricao.Text = p.descricao;
                txtPrcVenda.Text = p.prcvenda.ToString("0.00");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
