namespace ProjetoPDVUI
{
    partial class frmMovimento
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
            this.components = new System.ComponentModel.Container();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.lbl = new System.Windows.Forms.Label();
            this.lblPendente = new System.Windows.Forms.Label();
            this.cboUsuario = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cboLoja = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboFormaPagamento = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtProcurar = new System.Windows.Forms.TextBox();
            this.dtFinal = new System.Windows.Forms.DateTimePicker();
            this.Label2 = new System.Windows.Forms.Label();
            this.dtInicial = new System.Windows.Forms.DateTimePicker();
            this.Label3 = new System.Windows.Forms.Label();
            this.cboProcurar = new System.Windows.Forms.ComboBox();
            this.cmdLocalizar = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.lstvwPedidos = new System.Windows.Forms.ListView();
            this.NumDoc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NFiscal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DataDigitacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DataNFiscal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Operacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValorNFe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FormaPagamento = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Chave = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Protocolo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Loja = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.cmdExportaXML = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape4 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.rectangleShape1 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.rectangleShape3 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.rectangleShape2 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.Label12 = new System.Windows.Forms.Label();
            this.TxtNumProtocolo = new System.Windows.Forms.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.TxtChaveNFe = new System.Windows.Forms.TextBox();
            this.lstItens = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblAutorizado = new System.Windows.Forms.Label();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.btnEmitir = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.lblVal_Dinheiro = new System.Windows.Forms.Label();
            this.lblVal_CDebito = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblVal_CCredito = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblval_Total = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblVal_Outros = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtErro = new System.Windows.Forms.TextBox();
            this.btnFecharErro = new System.Windows.Forms.Button();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this.lbl);
            this.Panel1.Controls.Add(this.lblPendente);
            this.Panel1.Controls.Add(this.cboUsuario);
            this.Panel1.Controls.Add(this.label15);
            this.Panel1.Controls.Add(this.cboLoja);
            this.Panel1.Controls.Add(this.label9);
            this.Panel1.Controls.Add(this.cboStatus);
            this.Panel1.Controls.Add(this.label10);
            this.Panel1.Controls.Add(this.cboFormaPagamento);
            this.Panel1.Controls.Add(this.label7);
            this.Panel1.Controls.Add(this.txtProcurar);
            this.Panel1.Controls.Add(this.dtFinal);
            this.Panel1.Controls.Add(this.Label2);
            this.Panel1.Controls.Add(this.dtInicial);
            this.Panel1.Controls.Add(this.Label3);
            this.Panel1.Controls.Add(this.cboProcurar);
            this.Panel1.Controls.Add(this.cmdLocalizar);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(1148, 102);
            this.Panel1.TabIndex = 11;
            this.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel1_Paint);
            // 
            // lbl
            // 
            this.lbl.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lbl.Font = new System.Drawing.Font("TypoUpright BT", 26.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl.ForeColor = System.Drawing.Color.Red;
            this.lbl.Location = new System.Drawing.Point(1123, 68);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(21, 38);
            this.lbl.TabIndex = 26;
            this.lbl.Text = "!";
            this.lbl.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lbl.Visible = false;
            // 
            // lblPendente
            // 
            this.lblPendente.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblPendente.Font = new System.Drawing.Font("Verdana", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendente.ForeColor = System.Drawing.Color.Red;
            this.lblPendente.Location = new System.Drawing.Point(951, 74);
            this.lblPendente.Name = "lblPendente";
            this.lblPendente.Size = new System.Drawing.Size(178, 25);
            this.lblPendente.TabIndex = 25;
            this.lblPendente.Text = "Pendentes ()";
            this.lblPendente.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lblPendente.Visible = false;
            // 
            // cboUsuario
            // 
            this.cboUsuario.DisplayMember = "0";
            this.cboUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUsuario.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboUsuario.FormattingEnabled = true;
            this.cboUsuario.Location = new System.Drawing.Point(285, 65);
            this.cboUsuario.Name = "cboUsuario";
            this.cboUsuario.Size = new System.Drawing.Size(98, 21);
            this.cboUsuario.TabIndex = 24;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(222, 65);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(73, 19);
            this.label15.TabIndex = 23;
            this.label15.Text = "Usuário:";
            // 
            // cboLoja
            // 
            this.cboLoja.DisplayMember = "0";
            this.cboLoja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoja.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboLoja.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLoja.FormattingEnabled = true;
            this.cboLoja.Items.AddRange(new object[] {
            "Todos",
            "Livraria",
            "Cafeteria"});
            this.cboLoja.Location = new System.Drawing.Point(76, 63);
            this.cboLoja.Name = "cboLoja";
            this.cboLoja.Size = new System.Drawing.Size(140, 23);
            this.cboLoja.TabIndex = 22;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(6, 65);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 19);
            this.label9.TabIndex = 21;
            this.label9.Text = "Loja:";
            // 
            // cboStatus
            // 
            this.cboStatus.DisplayMember = "0";
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Location = new System.Drawing.Point(285, 36);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(98, 21);
            this.cboStatus.TabIndex = 20;
            this.cboStatus.SelectedIndexChanged += new System.EventHandler(this.cboStatus_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(222, 40);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 19);
            this.label10.TabIndex = 19;
            this.label10.Text = "Status:";
            // 
            // cboFormaPagamento
            // 
            this.cboFormaPagamento.DisplayMember = "0";
            this.cboFormaPagamento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFormaPagamento.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboFormaPagamento.FormattingEnabled = true;
            this.cboFormaPagamento.Location = new System.Drawing.Point(108, 36);
            this.cboFormaPagamento.Name = "cboFormaPagamento";
            this.cboFormaPagamento.Size = new System.Drawing.Size(108, 21);
            this.cboFormaPagamento.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(6, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(151, 19);
            this.label7.TabIndex = 17;
            this.label7.Text = "Forma de pt.º: ";
            // 
            // txtProcurar
            // 
            this.txtProcurar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProcurar.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtProcurar.Location = new System.Drawing.Point(227, 10);
            this.txtProcurar.MaxLength = 100;
            this.txtProcurar.Name = "txtProcurar";
            this.txtProcurar.Size = new System.Drawing.Size(156, 20);
            this.txtProcurar.TabIndex = 16;
            // 
            // dtFinal
            // 
            this.dtFinal.Checked = false;
            this.dtFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFinal.Location = new System.Drawing.Point(605, 9);
            this.dtFinal.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.dtFinal.Name = "dtFinal";
            this.dtFinal.ShowCheckBox = true;
            this.dtFinal.Size = new System.Drawing.Size(103, 20);
            this.dtFinal.TabIndex = 15;
            this.dtFinal.Value = new System.DateTime(2010, 5, 4, 0, 0, 0, 0);
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.Color.Black;
            this.Label2.Location = new System.Drawing.Point(569, 12);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(33, 16);
            this.Label2.TabIndex = 14;
            this.Label2.Text = "até";
            // 
            // dtInicial
            // 
            this.dtInicial.Checked = false;
            this.dtInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtInicial.Location = new System.Drawing.Point(460, 9);
            this.dtInicial.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.dtInicial.Name = "dtInicial";
            this.dtInicial.ShowCheckBox = true;
            this.dtInicial.Size = new System.Drawing.Size(103, 20);
            this.dtInicial.TabIndex = 13;
            this.dtInicial.Value = new System.DateTime(2010, 5, 4, 0, 0, 0, 0);
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.Color.Black;
            this.Label3.Location = new System.Drawing.Point(396, 12);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(63, 16);
            this.Label3.TabIndex = 12;
            this.Label3.Text = "Período de:";
            // 
            // cboProcurar
            // 
            this.cboProcurar.DisplayMember = "0";
            this.cboProcurar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProcurar.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboProcurar.FormattingEnabled = true;
            this.cboProcurar.Location = new System.Drawing.Point(76, 9);
            this.cboProcurar.Name = "cboProcurar";
            this.cboProcurar.Size = new System.Drawing.Size(140, 21);
            this.cboProcurar.TabIndex = 9;
            this.cboProcurar.SelectedIndexChanged += new System.EventHandler(this.cboProcurar_SelectedIndexChanged);
            // 
            // cmdLocalizar
            // 
            this.cmdLocalizar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cmdLocalizar.BackColor = System.Drawing.SystemColors.Control;
            this.cmdLocalizar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLocalizar.ForeColor = System.Drawing.Color.Black;
            this.cmdLocalizar.Location = new System.Drawing.Point(1036, 8);
            this.cmdLocalizar.Name = "cmdLocalizar";
            this.cmdLocalizar.Size = new System.Drawing.Size(107, 24);
            this.cmdLocalizar.TabIndex = 8;
            this.cmdLocalizar.Text = "&Localizar";
            this.cmdLocalizar.UseVisualStyleBackColor = false;
            this.cmdLocalizar.Click += new System.EventHandler(this.cmdLocalizar_Click);
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.Color.Black;
            this.Label1.Location = new System.Drawing.Point(6, 11);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(76, 19);
            this.Label1.TabIndex = 10;
            this.Label1.Text = "Procurar por:";
            // 
            // lstvwPedidos
            // 
            this.lstvwPedidos.BackColor = System.Drawing.Color.White;
            this.lstvwPedidos.CheckBoxes = true;
            this.lstvwPedidos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NumDoc,
            this.NFiscal,
            this.DataDigitacao,
            this.DataNFiscal,
            this.Operacao,
            this.ValorNFe,
            this.FormaPagamento,
            this.Status,
            this.Chave,
            this.Protocolo,
            this.Loja});
            this.lstvwPedidos.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstvwPedidos.FullRowSelect = true;
            this.lstvwPedidos.GridLines = true;
            this.lstvwPedidos.HideSelection = false;
            this.lstvwPedidos.Location = new System.Drawing.Point(0, 150);
            this.lstvwPedidos.MultiSelect = false;
            this.lstvwPedidos.Name = "lstvwPedidos";
            this.lstvwPedidos.Size = new System.Drawing.Size(1144, 364);
            this.lstvwPedidos.TabIndex = 147;
            this.lstvwPedidos.UseCompatibleStateImageBehavior = false;
            this.lstvwPedidos.View = System.Windows.Forms.View.Details;
            this.lstvwPedidos.SelectedIndexChanged += new System.EventHandler(this.lstPedidos_SelectedIndexChanged);
            // 
            // NumDoc
            // 
            this.NumDoc.Text = "Pedido";
            this.NumDoc.Width = 70;
            // 
            // NFiscal
            // 
            this.NFiscal.Text = "NFiscal";
            this.NFiscal.Width = 63;
            // 
            // DataDigitacao
            // 
            this.DataDigitacao.Text = "Data da Digitação";
            this.DataDigitacao.Width = 160;
            // 
            // DataNFiscal
            // 
            this.DataNFiscal.Text = "Data da Nota Fiscal";
            this.DataNFiscal.Width = 160;
            // 
            // Operacao
            // 
            this.Operacao.Text = "Natureza da Operação";
            this.Operacao.Width = 170;
            // 
            // ValorNFe
            // 
            this.ValorNFe.Text = "ValorNFe";
            this.ValorNFe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ValorNFe.Width = 69;
            // 
            // FormaPagamento
            // 
            this.FormaPagamento.Text = "Forma de pagamento";
            this.FormaPagamento.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FormaPagamento.Width = 120;
            // 
            // Status
            // 
            this.Status.Text = "Status";
            this.Status.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Status.Width = 180;
            // 
            // Chave
            // 
            this.Chave.Text = "Chave";
            this.Chave.Width = 0;
            // 
            // Protocolo
            // 
            this.Protocolo.Text = "Protocolo";
            this.Protocolo.Width = 0;
            // 
            // Loja
            // 
            this.Loja.Text = "Loja";
            this.Loja.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Loja.Width = 132;
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // cmdExportaXML
            // 
            this.cmdExportaXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExportaXML.Enabled = false;
            this.cmdExportaXML.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExportaXML.Location = new System.Drawing.Point(1037, 120);
            this.cmdExportaXML.Name = "cmdExportaXML";
            this.cmdExportaXML.Size = new System.Drawing.Size(107, 24);
            this.cmdExportaXML.TabIndex = 151;
            this.cmdExportaXML.Text = "&Gerar DANFE";
            this.cmdExportaXML.UseVisualStyleBackColor = true;
            this.cmdExportaXML.Click += new System.EventHandler(this.cmdExportaXML_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Gray;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(365, 529);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 16);
            this.label5.TabIndex = 165;
            this.label5.Text = "DADOS DA EMISSÃO";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape4,
            this.rectangleShape1,
            this.rectangleShape3,
            this.rectangleShape2});
            this.shapeContainer1.Size = new System.Drawing.Size(1148, 712);
            this.shapeContainer1.TabIndex = 176;
            this.shapeContainer1.TabStop = false;
            // 
            // rectangleShape4
            // 
            this.rectangleShape4.BackColor = System.Drawing.Color.Gray;
            this.rectangleShape4.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.rectangleShape4.BorderColor = System.Drawing.Color.Gray;
            this.rectangleShape4.CornerRadius = 1;
            this.rectangleShape4.FillGradientColor = System.Drawing.Color.Gray;
            this.rectangleShape4.Location = new System.Drawing.Point(857, 523);
            this.rectangleShape4.Name = "rectangleShape4";
            this.rectangleShape4.Size = new System.Drawing.Size(286, 28);
            // 
            // rectangleShape1
            // 
            this.rectangleShape1.BorderColor = System.Drawing.SystemColors.AppWorkspace;
            this.rectangleShape1.Location = new System.Drawing.Point(856, 522);
            this.rectangleShape1.Name = "rectangleShape1";
            this.rectangleShape1.Size = new System.Drawing.Size(287, 183);
            // 
            // rectangleShape3
            // 
            this.rectangleShape3.BackColor = System.Drawing.Color.Gray;
            this.rectangleShape3.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.rectangleShape3.BorderColor = System.Drawing.Color.Gray;
            this.rectangleShape3.CornerRadius = 1;
            this.rectangleShape3.FillGradientColor = System.Drawing.Color.Gray;
            this.rectangleShape3.Location = new System.Drawing.Point(2, 522);
            this.rectangleShape3.Name = "rectangleShape3";
            this.rectangleShape3.Size = new System.Drawing.Size(840, 28);
            // 
            // rectangleShape2
            // 
            this.rectangleShape2.BackColor = System.Drawing.Color.Gray;
            this.rectangleShape2.BorderColor = System.Drawing.SystemColors.AppWorkspace;
            this.rectangleShape2.Location = new System.Drawing.Point(1, 521);
            this.rectangleShape2.Name = "rectangleShape2";
            this.rectangleShape2.Size = new System.Drawing.Size(842, 184);
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label12.Location = new System.Drawing.Point(500, 562);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(107, 13);
            this.Label12.TabIndex = 181;
            this.Label12.Text = "Número do Protocolo";
            // 
            // TxtNumProtocolo
            // 
            this.TxtNumProtocolo.BackColor = System.Drawing.Color.White;
            this.TxtNumProtocolo.ForeColor = System.Drawing.Color.MidnightBlue;
            this.TxtNumProtocolo.Location = new System.Drawing.Point(503, 579);
            this.TxtNumProtocolo.Name = "TxtNumProtocolo";
            this.TxtNumProtocolo.ReadOnly = true;
            this.TxtNumProtocolo.Size = new System.Drawing.Size(332, 20);
            this.TxtNumProtocolo.TabIndex = 180;
            this.TxtNumProtocolo.TabStop = false;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.ForeColor = System.Drawing.Color.Black;
            this.Label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label8.Location = new System.Drawing.Point(10, 562);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(91, 13);
            this.Label8.TabIndex = 179;
            this.Label8.Text = "Chave de Acesso";
            // 
            // TxtChaveNFe
            // 
            this.TxtChaveNFe.BackColor = System.Drawing.Color.White;
            this.TxtChaveNFe.ForeColor = System.Drawing.Color.MidnightBlue;
            this.TxtChaveNFe.Location = new System.Drawing.Point(13, 579);
            this.TxtChaveNFe.MaxLength = 50;
            this.TxtChaveNFe.Name = "TxtChaveNFe";
            this.TxtChaveNFe.ReadOnly = true;
            this.TxtChaveNFe.Size = new System.Drawing.Size(469, 20);
            this.TxtChaveNFe.TabIndex = 178;
            this.TxtChaveNFe.TabStop = false;
            // 
            // lstItens
            // 
            this.lstItens.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstItens.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader18,
            this.columnHeader16,
            this.columnHeader17});
            this.lstItens.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstItens.FullRowSelect = true;
            this.lstItens.GridLines = true;
            this.lstItens.HideSelection = false;
            this.lstItens.Location = new System.Drawing.Point(10, 605);
            this.lstItens.MultiSelect = false;
            this.lstItens.Name = "lstItens";
            this.lstItens.Size = new System.Drawing.Size(825, 94);
            this.lstItens.TabIndex = 177;
            this.lstItens.UseCompatibleStateImageBehavior = false;
            this.lstItens.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Código";
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Descrição";
            this.columnHeader14.Width = 365;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Qtd.";
            this.columnHeader15.Width = 50;
            // 
            // columnHeader18
            // 
            this.columnHeader18.DisplayIndex = 5;
            this.columnHeader18.Text = "Valor";
            this.columnHeader18.Width = 75;
            // 
            // columnHeader16
            // 
            this.columnHeader16.DisplayIndex = 3;
            this.columnHeader16.Text = "Desc.";
            this.columnHeader16.Width = 70;
            // 
            // columnHeader17
            // 
            this.columnHeader17.DisplayIndex = 4;
            this.columnHeader17.Text = "Preço";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(3, 120);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 24);
            this.button1.TabIndex = 182;
            this.button1.Text = "&Sair";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.OliveDrab;
            this.label4.Location = new System.Drawing.Point(862, 553);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 23);
            this.label4.TabIndex = 183;
            this.label4.Text = "Autorizadas";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Brown;
            this.label6.Location = new System.Drawing.Point(862, 573);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 23);
            this.label6.TabIndex = 184;
            this.label6.Text = "Canceladas";
            // 
            // lblAutorizado
            // 
            this.lblAutorizado.AutoSize = true;
            this.lblAutorizado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAutorizado.Location = new System.Drawing.Point(1112, 559);
            this.lblAutorizado.Name = "lblAutorizado";
            this.lblAutorizado.Size = new System.Drawing.Size(29, 16);
            this.lblAutorizado.TabIndex = 187;
            this.lblAutorizado.Text = "000";
            // 
            // lblCancelado
            // 
            this.lblCancelado.AutoSize = true;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(1112, 579);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(29, 16);
            this.lblCancelado.TabIndex = 188;
            this.lblCancelado.Text = "000";
            // 
            // btnEmitir
            // 
            this.btnEmitir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEmitir.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmitir.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.btnEmitir.Location = new System.Drawing.Point(924, 120);
            this.btnEmitir.Name = "btnEmitir";
            this.btnEmitir.Size = new System.Drawing.Size(107, 24);
            this.btnEmitir.TabIndex = 191;
            this.btnEmitir.Text = "&Emitir NFC-e";
            this.btnEmitir.UseVisualStyleBackColor = true;
            this.btnEmitir.Visible = false;
            this.btnEmitir.Click += new System.EventHandler(this.btnEmitir_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(863, 638);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(119, 16);
            this.label11.TabIndex = 192;
            this.label11.Text = "Cartão de Crédito: ";
            // 
            // lblVal_Dinheiro
            // 
            this.lblVal_Dinheiro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVal_Dinheiro.Location = new System.Drawing.Point(1085, 603);
            this.lblVal_Dinheiro.Name = "lblVal_Dinheiro";
            this.lblVal_Dinheiro.Size = new System.Drawing.Size(56, 16);
            this.lblVal_Dinheiro.TabIndex = 193;
            this.lblVal_Dinheiro.Text = "00,00";
            this.lblVal_Dinheiro.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblVal_CDebito
            // 
            this.lblVal_CDebito.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVal_CDebito.Location = new System.Drawing.Point(1082, 620);
            this.lblVal_CDebito.Name = "lblVal_CDebito";
            this.lblVal_CDebito.Size = new System.Drawing.Size(59, 16);
            this.lblVal_CDebito.TabIndex = 195;
            this.lblVal_CDebito.Text = "00,00";
            this.lblVal_CDebito.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(863, 620);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(116, 16);
            this.label14.TabIndex = 194;
            this.label14.Text = "Cartão de Débito: ";
            // 
            // lblVal_CCredito
            // 
            this.lblVal_CCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVal_CCredito.Location = new System.Drawing.Point(1079, 638);
            this.lblVal_CCredito.Name = "lblVal_CCredito";
            this.lblVal_CCredito.Size = new System.Drawing.Size(62, 16);
            this.lblVal_CCredito.TabIndex = 197;
            this.lblVal_CCredito.Text = "00,00";
            this.lblVal_CCredito.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(863, 603);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(61, 16);
            this.label16.TabIndex = 196;
            this.label16.Text = "Dinheiro:";
            // 
            // lblval_Total
            // 
            this.lblval_Total.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblval_Total.Location = new System.Drawing.Point(1076, 684);
            this.lblval_Total.Name = "lblval_Total";
            this.lblval_Total.Size = new System.Drawing.Size(65, 16);
            this.lblval_Total.TabIndex = 199;
            this.lblval_Total.Text = "00,00";
            this.lblval_Total.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(863, 682);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(51, 18);
            this.label18.TabIndex = 198;
            this.label18.Text = "Total:";
            // 
            // lblVal_Outros
            // 
            this.lblVal_Outros.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVal_Outros.Location = new System.Drawing.Point(1079, 656);
            this.lblVal_Outros.Name = "lblVal_Outros";
            this.lblVal_Outros.Size = new System.Drawing.Size(62, 16);
            this.lblVal_Outros.TabIndex = 201;
            this.lblVal_Outros.Text = "00,00";
            this.lblVal_Outros.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(863, 656);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(50, 16);
            this.label13.TabIndex = 200;
            this.label13.Text = "Outros:";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtErro
            // 
            this.txtErro.Location = new System.Drawing.Point(352, 278);
            this.txtErro.Multiline = true;
            this.txtErro.Name = "txtErro";
            this.txtErro.Size = new System.Drawing.Size(438, 140);
            this.txtErro.TabIndex = 202;
            this.txtErro.Visible = false;
            // 
            // btnFecharErro
            // 
            this.btnFecharErro.Location = new System.Drawing.Point(772, 278);
            this.btnFecharErro.Name = "btnFecharErro";
            this.btnFecharErro.Size = new System.Drawing.Size(18, 18);
            this.btnFecharErro.TabIndex = 203;
            this.btnFecharErro.Text = "X";
            this.btnFecharErro.UseVisualStyleBackColor = true;
            this.btnFecharErro.Visible = false;
            this.btnFecharErro.Click += new System.EventHandler(this.btnFecharErro_Click);
            // 
            // frmMovimento
            // 
            this.AcceptButton = this.cmdLocalizar;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(1148, 712);
            this.Controls.Add(this.btnFecharErro);
            this.Controls.Add(this.txtErro);
            this.Controls.Add(this.lblVal_Outros);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lblval_Total);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.lblVal_CCredito);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.lblVal_CDebito);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lblVal_Dinheiro);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnEmitir);
            this.Controls.Add(this.lblCancelado);
            this.Controls.Add(this.lblAutorizado);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Label12);
            this.Controls.Add(this.TxtNumProtocolo);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.TxtChaveNFe);
            this.Controls.Add(this.lstItens);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmdExportaXML);
            this.Controls.Add(this.lstvwPedidos);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmMovimento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Movimentação de Notas Fiscais";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMovimento_FormClosed);
            this.Load += new System.EventHandler(this.frmMovimento_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMovimento_KeyDown);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.DateTimePicker dtFinal;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.DateTimePicker dtInicial;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.ComboBox cboProcurar;
        internal System.Windows.Forms.Button cmdLocalizar;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.ColumnHeader NumDoc;
        internal System.Windows.Forms.ColumnHeader NFiscal;
        internal System.Windows.Forms.ColumnHeader Operacao;
        internal System.Windows.Forms.ColumnHeader DataNFiscal;
        internal System.Windows.Forms.ColumnHeader ValorNFe;
        private System.Windows.Forms.ListView lstvwPedidos;
        internal System.Windows.Forms.TextBox txtProcurar;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private System.Windows.Forms.Button cmdExportaXML;
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.ColumnHeader FormaPagamento;
        private System.Windows.Forms.ColumnHeader Chave;
        private System.Windows.Forms.ColumnHeader Protocolo;
        private System.Windows.Forms.Label label5;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape2;
        private System.Windows.Forms.Label Label12;
        private System.Windows.Forms.TextBox TxtNumProtocolo;
        private System.Windows.Forms.Label Label8;
        private System.Windows.Forms.TextBox TxtChaveNFe;
        private System.Windows.Forms.ListView lstItens;
        internal System.Windows.Forms.ColumnHeader columnHeader7;
        internal System.Windows.Forms.ColumnHeader columnHeader14;
        internal System.Windows.Forms.ColumnHeader columnHeader15;
        internal System.Windows.Forms.ColumnHeader columnHeader18;
        internal System.Windows.Forms.ColumnHeader columnHeader16;
        internal System.Windows.Forms.ColumnHeader columnHeader17;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape3;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape4;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblAutorizado;
        private System.Windows.Forms.Label lblCancelado;
        internal System.Windows.Forms.ComboBox cboStatus;
        internal System.Windows.Forms.Label label10;
        internal System.Windows.Forms.ComboBox cboFormaPagamento;
        internal System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnEmitir;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblVal_Dinheiro;
        private System.Windows.Forms.Label lblVal_CDebito;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblVal_CCredito;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblval_Total;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblVal_Outros;
        private System.Windows.Forms.Label label13;
        internal System.Windows.Forms.ColumnHeader DataDigitacao;
        internal System.Windows.Forms.ComboBox cboUsuario;
        internal System.Windows.Forms.Label label15;
        internal System.Windows.Forms.ComboBox cboLoja;
        internal System.Windows.Forms.Label label9;
        private System.Windows.Forms.ColumnHeader Loja;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label lblPendente;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox txtErro;
        private System.Windows.Forms.Button btnFecharErro;
    }
}