namespace CRMReportes
{
    partial class ReciboPago
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReciboPago));
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.dsPago = new Telerik.Reporting.SqlDataSource();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(2.2999999523162842D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.pictureBox1});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            this.pageHeaderSection1.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pageHeaderSection1.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(1.4999998807907105D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(20.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "RECIBO DE PAGO";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.pictureBox1.MimeType = "image/png";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.39989972114563D), Telerik.Reporting.Drawing.Unit.Cm(1.4997997283935547D));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            this.pictureBox1.Value = ((object)(resources.GetObject("pictureBox1.Value")));
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(3.38582706451416D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox25,
            this.textBox24,
            this.textBox19,
            this.textBox18,
            this.textBox17,
            this.textBox16,
            this.textBox15,
            this.textBox14,
            this.textBox9,
            this.textBox8,
            this.textBox7,
            this.textBox6,
            this.textBox3,
            this.textBox2,
            this.textBox4,
            this.textBox26,
            this.textBox5,
            this.textBox10,
            this.textBox20});
            this.detail.Name = "detail";
            // 
            // textBox25
            // 
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.19999989867210388D));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7072219848632812D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox25.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(219)))), ((int)(((byte)(219)))));
            this.textBox25.Style.BorderColor.Default = System.Drawing.Color.Gray;
            this.textBox25.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox25.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox25.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox25.Value = "Num Pago :";
            // 
            // textBox24
            // 
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.7869443893432617D), Telerik.Reporting.Drawing.Unit.Cm(0.19999989867210388D));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.9000000953674316D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox24.Style.BorderColor.Bottom = System.Drawing.Color.Gray;
            this.textBox24.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox24.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox24.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox24.Value = "= Fields.IdPago";
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.299999237060547D), Telerik.Reporting.Drawing.Unit.Cm(1.7938116788864136D));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.1998982429504395D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox19.Style.BorderColor.Bottom = System.Drawing.Color.Gray;
            this.textBox19.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox19.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox19.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox19.Value = "= Fields.Paterno";
            // 
            // textBox18
            // 
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.299999237060547D), Telerik.Reporting.Drawing.Unit.Cm(2.3053393363952637D));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.1998982429504395D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox18.Style.BorderColor.Bottom = System.Drawing.Color.Gray;
            this.textBox18.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox18.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox18.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox18.Value = "= Fields.Materno";
            // 
            // textBox17
            // 
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.40000057220459D), Telerik.Reporting.Drawing.Unit.Cm(1.7938116788864136D));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7072219848632812D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox17.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(219)))), ((int)(((byte)(219)))));
            this.textBox17.Style.BorderColor.Default = System.Drawing.Color.Gray;
            this.textBox17.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox17.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox17.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox17.Value = "Paterno :";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.40000057220459D), Telerik.Reporting.Drawing.Unit.Cm(2.3053393363952637D));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7072219848632812D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox16.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(219)))), ((int)(((byte)(219)))));
            this.textBox16.Style.BorderColor.Default = System.Drawing.Color.Gray;
            this.textBox16.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox16.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox16.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox16.Value = "Materno :";
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.40000057220459D), Telerik.Reporting.Drawing.Unit.Cm(1.2936112880706787D));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7072219848632812D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox15.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(219)))), ((int)(((byte)(219)))));
            this.textBox15.Style.BorderColor.Default = System.Drawing.Color.Gray;
            this.textBox15.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox15.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox15.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox15.Value = "Nombre :";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.299999237060547D), Telerik.Reporting.Drawing.Unit.Cm(1.2936112880706787D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.1998982429504395D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox14.Style.BorderColor.Bottom = System.Drawing.Color.Gray;
            this.textBox14.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox14.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox14.Value = "= Fields.Nombre";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.7869443893432617D), Telerik.Reporting.Drawing.Unit.Cm(1.29361093044281D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.9000000953674316D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox9.Style.BorderColor.Bottom = System.Drawing.Color.Gray;
            this.textBox9.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox9.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox9.Value = "= Fields.Desarrollo";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(1.29361093044281D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7072219848632812D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox8.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(219)))), ((int)(((byte)(219)))));
            this.textBox8.Style.BorderColor.Default = System.Drawing.Color.Gray;
            this.textBox8.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox8.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = "Desarrollo :";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(2.3053393363952637D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7072219848632812D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox7.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(219)))), ((int)(((byte)(219)))));
            this.textBox7.Style.BorderColor.Default = System.Drawing.Color.Gray;
            this.textBox7.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.Value = "Código :";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(1.7938116788864136D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7072219848632812D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox6.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(219)))), ((int)(((byte)(219)))));
            this.textBox6.Style.BorderColor.Default = System.Drawing.Color.Gray;
            this.textBox6.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "Propiedad :";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.7869443893432617D), Telerik.Reporting.Drawing.Unit.Cm(2.3053393363952637D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.9000000953674316D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox3.Style.BorderColor.Bottom = System.Drawing.Color.Gray;
            this.textBox3.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox3.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "= Fields.Codigo";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.7869443893432617D), Telerik.Reporting.Drawing.Unit.Cm(1.7938116788864136D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.9000000953674316D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox2.Style.BorderColor.Bottom = System.Drawing.Color.Gray;
            this.textBox2.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox2.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "= Fields.Titulo";
            // 
            // textBox4
            // 
            this.textBox4.Format = "{0:C2}";
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.707521915435791D), Telerik.Reporting.Drawing.Unit.Cm(3.7999999523162842D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.9000000953674316D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox4.Style.BorderColor.Bottom = System.Drawing.Color.Gray;
            this.textBox4.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox4.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.Value = "= Fields.Importe";
            // 
            // textBox26
            // 
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(3.7999999523162842D));
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7072219848632812D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox26.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(219)))), ((int)(((byte)(219)))));
            this.textBox26.Style.BorderColor.Default = System.Drawing.Color.Gray;
            this.textBox26.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox26.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox26.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox26.Value = "Importe :";
            // 
            // textBox5
            // 
            this.textBox5.Format = "{0:d}";
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.299999237060547D), Telerik.Reporting.Drawing.Unit.Cm(0.19999963045120239D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.1998982429504395D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox5.Style.BorderColor.Bottom = System.Drawing.Color.Gray;
            this.textBox5.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox5.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox5.Value = "= Fields.FechaPago.Date.Date";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.40000057220459D), Telerik.Reporting.Drawing.Unit.Cm(0.19999989867210388D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7072219848632812D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox10.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(219)))), ((int)(((byte)(219)))));
            this.textBox10.Style.BorderColor.Default = System.Drawing.Color.Gray;
            this.textBox10.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox10.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox10.Value = "Fecha Pago :";
            // 
            // textBox20
            // 
            this.textBox20.Format = "{0:C2}";
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.707521915435791D), Telerik.Reporting.Drawing.Unit.Cm(4.3002004623413086D));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(16.99247932434082D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox20.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox20.Value = "= \"( SON \" + Fields.ImporteConLetra + \" MN )\"";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.51999998092651367D);
            this.pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox13,
            this.textBox12,
            this.textBox11});
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.1383333206176758D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.9000000953674316D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.Value = "Quantium Desarrollos";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.599900245666504D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.9000000953674316D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.Value = "= \"Página \" + PageNumber + \" de \" + PageCount";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.0999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox11.Style.Color = System.Drawing.Color.Silver;
            this.textBox11.Style.Font.Name = "Arial";
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.Value = "= \"Fecha Impresión : \" + Now()";
            // 
            // dsPago
            // 
            this.dsPago.CommandTimeout = 900;
            this.dsPago.ConnectionString = "CRMReportes.Properties.Settings.CRMINMOBILIARIO";
            this.dsPago.Name = "dsPago";
            this.dsPago.Parameters.AddRange(new Telerik.Reporting.SqlDataSourceParameter[] {
            new Telerik.Reporting.SqlDataSourceParameter("@IdAmortizacion", System.Data.DbType.Int32, "= Parameters.IdAmortizacion.Value")});
            this.dsPago.SelectCommand = resources.GetString("dsPago.SelectCommand");
            // 
            // ReciboPago
            // 
            this.DataSource = this.dsPago;
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1});
            this.Name = "ReciboPago";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            reportParameter1.Name = "IdAmortizacion";
            reportParameter1.Text = "IdAmortizacion";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter1.Visible = true;
            this.ReportParameters.Add(reportParameter1);
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1});
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(20.5D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox11;
        public Telerik.Reporting.SqlDataSource dsPago;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox20;
    }
}