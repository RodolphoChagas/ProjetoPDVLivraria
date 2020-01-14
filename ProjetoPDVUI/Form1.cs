using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjetoPDVUtil;
using ProjetoPDVModelos;
using ProjetoPDVDao;

namespace ProjetoPDVUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            string x = string.Empty;

            Pedido p = (new PedidoDao()).getPedido(206217);
            p.lstPedidoItem = (new PedidoItemDao()).getlst_Itens(p.numdoc);

            ProdutoDao pd = new ProdutoDao();

            foreach (PedidoItem pitem in p.lstPedidoItem)
            {
                pitem.produto = pd.getProduto(pitem.codpro);
            }

            for (int i = 0; i <= p.lstPedidoItem.Count - 1; i++)
            {

                //x = p.lstPedidoItem[i].prcitens.ToString().Replace(".", ",").PadLeft(6, ' ');
                x = p.lstPedidoItem[i].prcitens.ToString("######0.00").Replace(".", ",").PadLeft(10, ' ');

            }


            return;

            //string str = "Rua Alexandre Moura, 51, Gragoatá, Niterói - RJ" + Environment.NewLine;
            //string str2 = "dd";

            //lbl2.Text = str.Length.ToString();

            //txt.Text = StringUtil.PadBoth(str, 50);
            //txt.Text += StringUtil.PadBoth(str2, 50);
            

            //lbl.Text = txt.Text.Length.ToString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
