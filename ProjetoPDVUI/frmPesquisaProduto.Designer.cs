namespace ProjetoPDVUI
{
    partial class frmPesquisaProduto
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstvwProduto = new System.Windows.Forms.ListView();
            this.codpro = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.descricao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grupo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.estoque = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboLoja = new System.Windows.Forms.ComboBox();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.btnRetornar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape1 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboLocalizar = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lstvwProduto
            // 
            this.lstvwProduto.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.codpro,
            this.descricao,
            this.grupo,
            this.estoque,
            this.valor});
            this.lstvwProduto.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstvwProduto.FullRowSelect = true;
            this.lstvwProduto.GridLines = true;
            this.lstvwProduto.HideSelection = false;
            this.lstvwProduto.Location = new System.Drawing.Point(5, 111);
            this.lstvwProduto.MultiSelect = false;
            this.lstvwProduto.Name = "lstvwProduto";
            this.lstvwProduto.Size = new System.Drawing.Size(642, 223);
            this.lstvwProduto.TabIndex = 2;
            this.lstvwProduto.UseCompatibleStateImageBehavior = false;
            this.lstvwProduto.View = System.Windows.Forms.View.Details;
            this.lstvwProduto.SelectedIndexChanged += new System.EventHandler(this.lstvwProduto_SelectedIndexChanged);
            this.lstvwProduto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstvwProduto_KeyDown);
            // 
            // codpro
            // 
            this.codpro.Text = "CodPro";
            // 
            // descricao
            // 
            this.descricao.Text = "Descrição";
            this.descricao.Width = 340;
            // 
            // grupo
            // 
            this.grupo.Text = "Grupo";
            this.grupo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.grupo.Width = 100;
            // 
            // estoque
            // 
            this.estoque.Text = "Estoque";
            this.estoque.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // valor
            // 
            this.valor.Text = "Valor";
            this.valor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 14);
            this.label1.TabIndex = 150;
            this.label1.Text = "Loja";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(122, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 14);
            this.label2.TabIndex = 151;
            this.label2.Text = "Digite a pesquisa aqui";
            // 
            // cboLoja
            // 
            this.cboLoja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoja.FormattingEnabled = true;
            this.cboLoja.Items.AddRange(new object[] {
            "Livraria",
            "Cafeteria"});
            this.cboLoja.Location = new System.Drawing.Point(11, 73);
            this.cboLoja.Name = "cboLoja";
            this.cboLoja.Size = new System.Drawing.Size(108, 21);
            this.cboLoja.TabIndex = 152;
            // 
            // txtDescricao
            // 
            this.txtDescricao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescricao.Location = new System.Drawing.Point(125, 28);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(439, 20);
            this.txtDescricao.TabIndex = 1;
            this.txtDescricao.TextChanged += new System.EventHandler(this.txtDescricao_TextChanged);
            this.txtDescricao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescricao_KeyDown);
            // 
            // btnRetornar
            // 
            this.btnRetornar.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnRetornar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRetornar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRetornar.Location = new System.Drawing.Point(447, 342);
            this.btnRetornar.Name = "btnRetornar";
            this.btnRetornar.Size = new System.Drawing.Size(117, 23);
            this.btnRetornar.TabIndex = 3;
            this.btnRetornar.Text = "&Retornar produto";
            this.btnRetornar.UseVisualStyleBackColor = false;
            this.btnRetornar.Click += new System.EventHandler(this.btnRetornar_Click);
            // 
            // btnSair
            // 
            this.btnSair.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSair.Location = new System.Drawing.Point(570, 342);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 155;
            this.btnSair.Text = "&Sair";
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(650, 372);
            this.shapeContainer1.TabIndex = 156;
            this.shapeContainer1.TabStop = false;
            // 
            // rectangleShape1
            // 
            this.rectangleShape1.BackColor = System.Drawing.Color.SeaShell;
            this.rectangleShape1.Location = new System.Drawing.Point(0, 0);
            this.rectangleShape1.Name = "rectangleShape1";
            this.rectangleShape1.Size = new System.Drawing.Size(649, 371);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(632, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 16);
            this.label3.TabIndex = 157;
            this.label3.Text = "X";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(122, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 14);
            this.label4.TabIndex = 158;
            this.label4.Text = "Status";
            // 
            // cboLocalizar
            // 
            this.cboLocalizar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocalizar.FormattingEnabled = true;
            this.cboLocalizar.Items.AddRange(new object[] {
            "Descrição",
            "ISBN/EAN"});
            this.cboLocalizar.Location = new System.Drawing.Point(11, 28);
            this.cboLocalizar.Name = "cboLocalizar";
            this.cboLocalizar.Size = new System.Drawing.Size(108, 21);
            this.cboLocalizar.TabIndex = 162;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 14);
            this.label5.TabIndex = 161;
            this.label5.Text = "Localizar por";
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Items.AddRange(new object[] {
            "Disponível",
            "Indisponível",
            "Pré-Venda",
            "Bloqueado"});
            this.cboStatus.Location = new System.Drawing.Point(125, 73);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(108, 21);
            this.cboStatus.TabIndex = 163;
            // 
            // frmPesquisaProduto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(650, 372);
            this.Controls.Add(this.cboStatus);
            this.Controls.Add(this.cboLocalizar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnRetornar);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.cboLoja);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstvwProduto);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmPesquisaProduto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pesquisa Produto";
            this.Load += new System.EventHandler(this.frmPesquisaProduto_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPesquisaProduto_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstvwProduto;
        internal System.Windows.Forms.ColumnHeader codpro;
        internal System.Windows.Forms.ColumnHeader descricao;
        internal System.Windows.Forms.ColumnHeader grupo;
        internal System.Windows.Forms.ColumnHeader estoque;
        private System.Windows.Forms.ColumnHeader valor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboLoja;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Button btnRetornar;
        private System.Windows.Forms.Button btnSair;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboLocalizar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboStatus;

    }
}