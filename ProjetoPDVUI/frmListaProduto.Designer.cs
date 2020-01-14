namespace ProjetoPDVUI
{
    partial class frmListaProduto
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
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cboLocalizar = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cklstStatus = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboLoja = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstvwProduto
            // 
            this.lstvwProduto.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1,
            this.ColumnHeader3,
            this.ColumnHeader4,
            this.ColumnHeader5,
            this.columnHeader7});
            this.lstvwProduto.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstvwProduto.FullRowSelect = true;
            this.lstvwProduto.GridLines = true;
            this.lstvwProduto.HideSelection = false;
            this.lstvwProduto.Location = new System.Drawing.Point(4, 107);
            this.lstvwProduto.MultiSelect = false;
            this.lstvwProduto.Name = "lstvwProduto";
            this.lstvwProduto.Size = new System.Drawing.Size(649, 493);
            this.lstvwProduto.TabIndex = 3;
            this.lstvwProduto.UseCompatibleStateImageBehavior = false;
            this.lstvwProduto.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "CodPro";
            // 
            // ColumnHeader3
            // 
            this.ColumnHeader3.Text = "Descrição";
            this.ColumnHeader3.Width = 340;
            // 
            // ColumnHeader4
            // 
            this.ColumnHeader4.Text = "SubGrupo";
            this.ColumnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ColumnHeader4.Width = 100;
            // 
            // ColumnHeader5
            // 
            this.ColumnHeader5.Text = "Estoque";
            this.ColumnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Valor";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cboLocalizar
            // 
            this.cboLocalizar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocalizar.FormattingEnabled = true;
            this.cboLocalizar.Items.AddRange(new object[] {
            "Descrição",
            "ISBN/EAN"});
            this.cboLocalizar.Location = new System.Drawing.Point(4, 25);
            this.cboLocalizar.Name = "cboLocalizar";
            this.cboLocalizar.Size = new System.Drawing.Size(108, 21);
            this.cboLocalizar.TabIndex = 166;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 14);
            this.label5.TabIndex = 165;
            this.label5.Text = "Localizar por";
            // 
            // txtDescricao
            // 
            this.txtDescricao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescricao.Location = new System.Drawing.Point(125, 25);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(439, 20);
            this.txtDescricao.TabIndex = 163;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(122, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 14);
            this.label2.TabIndex = 164;
            this.label2.Text = "Digite a pesquisa aqui";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(4, 616);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 23);
            this.button1.TabIndex = 167;
            this.button1.Text = "&Editar produto";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(514, 616);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(139, 23);
            this.button2.TabIndex = 168;
            this.button2.Text = "&Cadastrar Novo produto";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cklstStatus
            // 
            this.cklstStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cklstStatus.CheckOnClick = true;
            this.cklstStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cklstStatus.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.cklstStatus.FormattingEnabled = true;
            this.cklstStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cklstStatus.Items.AddRange(new object[] {
            "Disponível",
            "Indispinível",
            "Pré-Venda",
            "Bloqueado"});
            this.cklstStatus.Location = new System.Drawing.Point(125, 70);
            this.cklstStatus.Name = "cklstStatus";
            this.cklstStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cklstStatus.Size = new System.Drawing.Size(97, 18);
            this.cklstStatus.TabIndex = 170;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(122, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 14);
            this.label4.TabIndex = 169;
            this.label4.Text = "Status";
            // 
            // cboLoja
            // 
            this.cboLoja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoja.FormattingEnabled = true;
            this.cboLoja.Items.AddRange(new object[] {
            "Livraria",
            "Cafeteria"});
            this.cboLoja.Location = new System.Drawing.Point(4, 67);
            this.cboLoja.Name = "cboLoja";
            this.cboLoja.Size = new System.Drawing.Size(108, 21);
            this.cboLoja.TabIndex = 172;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 14);
            this.label1.TabIndex = 171;
            this.label1.Text = "Loja";
            // 
            // lblCount
            // 
            this.lblCount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblCount.Location = new System.Drawing.Point(440, 70);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(213, 23);
            this.lblCount.TabIndex = 173;
            this.lblCount.Text = "1 itens encontrados";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmListaProduto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(658, 643);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.cboLoja);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cklstStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cboLocalizar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstvwProduto);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmListaProduto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmListaProduto";
            this.Load += new System.EventHandler(this.frmListaProduto_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstvwProduto;
        internal System.Windows.Forms.ColumnHeader ColumnHeader1;
        internal System.Windows.Forms.ColumnHeader ColumnHeader3;
        internal System.Windows.Forms.ColumnHeader ColumnHeader4;
        internal System.Windows.Forms.ColumnHeader ColumnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ComboBox cboLocalizar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckedListBox cklstStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboLoja;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCount;
    }
}