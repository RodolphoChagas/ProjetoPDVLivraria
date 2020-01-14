namespace ProjetoPDVUI
{
    partial class frmGravaXml
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
            this.cmdLocalizar = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCodNat = new System.Windows.Forms.Label();
            this.Label18 = new System.Windows.Forms.Label();
            this.lblValDoc = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.lblDatDoc = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.lblNFiscal = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.lblUF = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.lblCidade = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.lblEndereco = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.lblRazaoSocial = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtNumDoc = new System.Windows.Forms.TextBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.TxtNumProtocolo = new System.Windows.Forms.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.TxtChaveNFe = new System.Windows.Forms.TextBox();
            this.cboProcurar = new System.Windows.Forms.ComboBox();
            this.cmdSair = new System.Windows.Forms.Button();
            this.lblArquivo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdAplicar = new System.Windows.Forms.Button();
            this.cmdAbrir = new System.Windows.Forms.Button();
            this.lblArquivoName = new System.Windows.Forms.Label();
            this.txtArquivoName = new System.Windows.Forms.TextBox();
            this.rdNFCe = new System.Windows.Forms.RadioButton();
            this.rdNFe = new System.Windows.Forms.RadioButton();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdLocalizar
            // 
            this.cmdLocalizar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdLocalizar.Location = new System.Drawing.Point(348, 7);
            this.cmdLocalizar.Name = "cmdLocalizar";
            this.cmdLocalizar.Size = new System.Drawing.Size(120, 24);
            this.cmdLocalizar.TabIndex = 132;
            this.cmdLocalizar.Text = "&Localizar";
            this.cmdLocalizar.UseVisualStyleBackColor = true;
            this.cmdLocalizar.Click += new System.EventHandler(this.cmdLocalizar_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.lblCodNat);
            this.GroupBox1.Controls.Add(this.Label18);
            this.GroupBox1.Controls.Add(this.lblValDoc);
            this.GroupBox1.Controls.Add(this.Label16);
            this.GroupBox1.Controls.Add(this.lblDatDoc);
            this.GroupBox1.Controls.Add(this.Label15);
            this.GroupBox1.Controls.Add(this.lblNFiscal);
            this.GroupBox1.Controls.Add(this.Label14);
            this.GroupBox1.Controls.Add(this.Label7);
            this.GroupBox1.Controls.Add(this.lblUF);
            this.GroupBox1.Controls.Add(this.Label10);
            this.GroupBox1.Controls.Add(this.lblCidade);
            this.GroupBox1.Controls.Add(this.Label11);
            this.GroupBox1.Controls.Add(this.lblEndereco);
            this.GroupBox1.Controls.Add(this.Label9);
            this.GroupBox1.Controls.Add(this.lblRazaoSocial);
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.ForeColor = System.Drawing.Color.Black;
            this.GroupBox1.Location = new System.Drawing.Point(8, 40);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(460, 132);
            this.GroupBox1.TabIndex = 131;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Dados da NF";
            // 
            // lblCodNat
            // 
            this.lblCodNat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCodNat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodNat.Location = new System.Drawing.Point(288, 20);
            this.lblCodNat.Name = "lblCodNat";
            this.lblCodNat.Size = new System.Drawing.Size(40, 20);
            this.lblCodNat.TabIndex = 107;
            this.lblCodNat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label18.Location = new System.Drawing.Point(240, 24);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(46, 13);
            this.Label18.TabIndex = 106;
            this.Label18.Text = "CodNat:";
            // 
            // lblValDoc
            // 
            this.lblValDoc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblValDoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblValDoc.Location = new System.Drawing.Point(372, 20);
            this.lblValDoc.Name = "lblValDoc";
            this.lblValDoc.Size = new System.Drawing.Size(80, 20);
            this.lblValDoc.TabIndex = 105;
            this.lblValDoc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label16.Location = new System.Drawing.Point(336, 24);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(34, 13);
            this.Label16.TabIndex = 104;
            this.Label16.Text = "Valor:";
            // 
            // lblDatDoc
            // 
            this.lblDatDoc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDatDoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblDatDoc.Location = new System.Drawing.Point(156, 20);
            this.lblDatDoc.Name = "lblDatDoc";
            this.lblDatDoc.Size = new System.Drawing.Size(80, 20);
            this.lblDatDoc.TabIndex = 103;
            this.lblDatDoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label15.Location = new System.Drawing.Point(120, 24);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(33, 13);
            this.Label15.TabIndex = 102;
            this.Label15.Text = "Data:";
            // 
            // lblNFiscal
            // 
            this.lblNFiscal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNFiscal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblNFiscal.Location = new System.Drawing.Point(52, 20);
            this.lblNFiscal.Name = "lblNFiscal";
            this.lblNFiscal.Size = new System.Drawing.Size(60, 20);
            this.lblNFiscal.TabIndex = 101;
            this.lblNFiscal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label14.Location = new System.Drawing.Point(8, 24);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(45, 13);
            this.Label14.TabIndex = 100;
            this.Label14.Text = "NFiscal:";
            // 
            // Label7
            // 
            this.Label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label7.Location = new System.Drawing.Point(0, 47);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(460, 2);
            this.Label7.TabIndex = 99;
            // 
            // lblUF
            // 
            this.lblUF.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblUF.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblUF.Location = new System.Drawing.Point(416, 104);
            this.lblUF.Name = "lblUF";
            this.lblUF.Size = new System.Drawing.Size(36, 20);
            this.lblUF.TabIndex = 98;
            this.lblUF.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label10.Location = new System.Drawing.Point(392, 108);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(24, 13);
            this.Label10.TabIndex = 97;
            this.Label10.Text = "UF:";
            // 
            // lblCidade
            // 
            this.lblCidade.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCidade.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCidade.Location = new System.Drawing.Point(64, 104);
            this.lblCidade.Name = "lblCidade";
            this.lblCidade.Size = new System.Drawing.Size(180, 20);
            this.lblCidade.TabIndex = 96;
            this.lblCidade.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label11.Location = new System.Drawing.Point(8, 108);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(43, 13);
            this.Label11.TabIndex = 95;
            this.Label11.Text = "Cidade:";
            // 
            // lblEndereco
            // 
            this.lblEndereco.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEndereco.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblEndereco.Location = new System.Drawing.Point(64, 80);
            this.lblEndereco.Name = "lblEndereco";
            this.lblEndereco.Size = new System.Drawing.Size(388, 20);
            this.lblEndereco.TabIndex = 94;
            this.lblEndereco.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label9.Location = new System.Drawing.Point(8, 84);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(56, 13);
            this.Label9.TabIndex = 93;
            this.Label9.Text = "Endereço:";
            // 
            // lblRazaoSocial
            // 
            this.lblRazaoSocial.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRazaoSocial.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblRazaoSocial.Location = new System.Drawing.Point(64, 56);
            this.lblRazaoSocial.Name = "lblRazaoSocial";
            this.lblRazaoSocial.Size = new System.Drawing.Size(388, 20);
            this.lblRazaoSocial.TabIndex = 92;
            this.lblRazaoSocial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label2.Location = new System.Drawing.Point(8, 60);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(42, 13);
            this.Label2.TabIndex = 91;
            this.Label2.Text = "Cliente:";
            // 
            // txtNumDoc
            // 
            this.txtNumDoc.BackColor = System.Drawing.Color.White;
            this.txtNumDoc.Location = new System.Drawing.Point(131, 7);
            this.txtNumDoc.MaxLength = 8;
            this.txtNumDoc.Name = "txtNumDoc";
            this.txtNumDoc.Size = new System.Drawing.Size(76, 20);
            this.txtNumDoc.TabIndex = 130;
            this.txtNumDoc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label12.Location = new System.Drawing.Point(296, 178);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(107, 13);
            this.Label12.TabIndex = 134;
            this.Label12.Text = "Número do Protocolo";
            // 
            // TxtNumProtocolo
            // 
            this.TxtNumProtocolo.BackColor = System.Drawing.SystemColors.Control;
            this.TxtNumProtocolo.Location = new System.Drawing.Point(296, 194);
            this.TxtNumProtocolo.Name = "TxtNumProtocolo";
            this.TxtNumProtocolo.ReadOnly = true;
            this.TxtNumProtocolo.Size = new System.Drawing.Size(172, 20);
            this.TxtNumProtocolo.TabIndex = 136;
            this.TxtNumProtocolo.TabStop = false;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label8.Location = new System.Drawing.Point(8, 179);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(128, 13);
            this.Label8.TabIndex = 133;
            this.Label8.Text = "Chave de acesso da NFe";
            // 
            // TxtChaveNFe
            // 
            this.TxtChaveNFe.BackColor = System.Drawing.SystemColors.Control;
            this.TxtChaveNFe.Location = new System.Drawing.Point(8, 194);
            this.TxtChaveNFe.Name = "TxtChaveNFe";
            this.TxtChaveNFe.ReadOnly = true;
            this.TxtChaveNFe.Size = new System.Drawing.Size(276, 20);
            this.TxtChaveNFe.TabIndex = 135;
            this.TxtChaveNFe.TabStop = false;
            // 
            // cboProcurar
            // 
            this.cboProcurar.DisplayMember = "0";
            this.cboProcurar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProcurar.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboProcurar.FormattingEnabled = true;
            this.cboProcurar.Items.AddRange(new object[] {
            "N° da Nota Fiscal",
            "N° do Pedido"});
            this.cboProcurar.Location = new System.Drawing.Point(8, 7);
            this.cboProcurar.Name = "cboProcurar";
            this.cboProcurar.Size = new System.Drawing.Size(117, 21);
            this.cboProcurar.TabIndex = 137;
            // 
            // cmdSair
            // 
            this.cmdSair.Location = new System.Drawing.Point(388, 297);
            this.cmdSair.Name = "cmdSair";
            this.cmdSair.Size = new System.Drawing.Size(80, 29);
            this.cmdSair.TabIndex = 142;
            this.cmdSair.Text = "&Sair";
            this.cmdSair.UseVisualStyleBackColor = true;
            this.cmdSair.Click += new System.EventHandler(this.cmdSair_Click);
            // 
            // lblArquivo
            // 
            this.lblArquivo.BackColor = System.Drawing.Color.White;
            this.lblArquivo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblArquivo.ForeColor = System.Drawing.Color.Green;
            this.lblArquivo.Location = new System.Drawing.Point(8, 249);
            this.lblArquivo.Name = "lblArquivo";
            this.lblArquivo.Size = new System.Drawing.Size(416, 28);
            this.lblArquivo.TabIndex = 140;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 229);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 139;
            this.label1.Text = "Arquivo XML ";
            // 
            // cmdAplicar
            // 
            this.cmdAplicar.Enabled = false;
            this.cmdAplicar.Location = new System.Drawing.Point(296, 297);
            this.cmdAplicar.Name = "cmdAplicar";
            this.cmdAplicar.Size = new System.Drawing.Size(80, 29);
            this.cmdAplicar.TabIndex = 138;
            this.cmdAplicar.Text = "&Salvar";
            this.cmdAplicar.UseVisualStyleBackColor = true;
            this.cmdAplicar.Click += new System.EventHandler(this.cmdAplicar_Click);
            // 
            // cmdAbrir
            // 
            this.cmdAbrir.Enabled = false;
            this.cmdAbrir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAbrir.Location = new System.Drawing.Point(428, 249);
            this.cmdAbrir.Name = "cmdAbrir";
            this.cmdAbrir.Size = new System.Drawing.Size(40, 28);
            this.cmdAbrir.TabIndex = 143;
            this.cmdAbrir.Text = "...";
            this.cmdAbrir.UseVisualStyleBackColor = true;
            this.cmdAbrir.Click += new System.EventHandler(this.cmdAbrir_Click);
            // 
            // lblArquivoName
            // 
            this.lblArquivoName.AutoSize = true;
            this.lblArquivoName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblArquivoName.Location = new System.Drawing.Point(8, 286);
            this.lblArquivoName.Name = "lblArquivoName";
            this.lblArquivoName.Size = new System.Drawing.Size(0, 13);
            this.lblArquivoName.TabIndex = 144;
            this.lblArquivoName.Visible = false;
            // 
            // txtArquivoName
            // 
            this.txtArquivoName.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtArquivoName.Location = new System.Drawing.Point(89, 226);
            this.txtArquivoName.Name = "txtArquivoName";
            this.txtArquivoName.Size = new System.Drawing.Size(13, 20);
            this.txtArquivoName.TabIndex = 145;
            this.txtArquivoName.Visible = false;
            // 
            // rdNFCe
            // 
            this.rdNFCe.AutoSize = true;
            this.rdNFCe.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdNFCe.ForeColor = System.Drawing.Color.Maroon;
            this.rdNFCe.Location = new System.Drawing.Point(268, 8);
            this.rdNFCe.Name = "rdNFCe";
            this.rdNFCe.Size = new System.Drawing.Size(59, 20);
            this.rdNFCe.TabIndex = 167;
            this.rdNFCe.Text = "NFC-e";
            this.rdNFCe.UseVisualStyleBackColor = true;
            // 
            // rdNFe
            // 
            this.rdNFe.AutoSize = true;
            this.rdNFe.Checked = true;
            this.rdNFe.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdNFe.ForeColor = System.Drawing.Color.Maroon;
            this.rdNFe.Location = new System.Drawing.Point(216, 8);
            this.rdNFe.Name = "rdNFe";
            this.rdNFe.Size = new System.Drawing.Size(51, 20);
            this.rdNFe.TabIndex = 166;
            this.rdNFe.TabStop = true;
            this.rdNFe.Text = "NF-e";
            this.rdNFe.UseVisualStyleBackColor = true;
            // 
            // frmGravaXml
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 331);
            this.Controls.Add(this.rdNFCe);
            this.Controls.Add(this.rdNFe);
            this.Controls.Add(this.txtArquivoName);
            this.Controls.Add(this.lblArquivoName);
            this.Controls.Add(this.cmdAbrir);
            this.Controls.Add(this.cmdSair);
            this.Controls.Add(this.lblArquivo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdAplicar);
            this.Controls.Add(this.cboProcurar);
            this.Controls.Add(this.cmdLocalizar);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.txtNumDoc);
            this.Controls.Add(this.Label12);
            this.Controls.Add(this.TxtNumProtocolo);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.TxtChaveNFe);
            this.Name = "frmGravaXml";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Grava arquivo XML";
            this.Load += new System.EventHandler(this.frmGravaXml_Load);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdLocalizar;
        internal System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.Label lblCodNat;
        private System.Windows.Forms.Label Label18;
        private System.Windows.Forms.Label lblValDoc;
        private System.Windows.Forms.Label Label16;
        private System.Windows.Forms.Label lblDatDoc;
        private System.Windows.Forms.Label Label15;
        private System.Windows.Forms.Label lblNFiscal;
        private System.Windows.Forms.Label Label14;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label lblUF;
        private System.Windows.Forms.Label Label10;
        private System.Windows.Forms.Label lblCidade;
        private System.Windows.Forms.Label Label11;
        private System.Windows.Forms.Label lblEndereco;
        private System.Windows.Forms.Label Label9;
        private System.Windows.Forms.Label lblRazaoSocial;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.TextBox txtNumDoc;
        private System.Windows.Forms.Label Label12;
        private System.Windows.Forms.TextBox TxtNumProtocolo;
        private System.Windows.Forms.Label Label8;
        private System.Windows.Forms.TextBox TxtChaveNFe;
        internal System.Windows.Forms.ComboBox cboProcurar;
        internal System.Windows.Forms.Button cmdSair;
        internal System.Windows.Forms.Label lblArquivo;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button cmdAplicar;
        internal System.Windows.Forms.Button cmdAbrir;
        private System.Windows.Forms.Label lblArquivoName;
        private System.Windows.Forms.TextBox txtArquivoName;
        private System.Windows.Forms.RadioButton rdNFCe;
        private System.Windows.Forms.RadioButton rdNFe;
    }
}