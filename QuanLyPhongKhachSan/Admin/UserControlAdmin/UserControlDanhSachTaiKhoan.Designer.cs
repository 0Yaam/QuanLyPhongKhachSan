namespace QuanLyPhongKhachSan.Login.UserControlAdmin
{
    partial class UserControlDanhSachTaiKhoan
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtpDenNgay = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdSDT = new Guna.UI2.WinForms.Guna2RadioButton();
            this.rdCCCD = new Guna.UI2.WinForms.Guna2RadioButton();
            this.rdTen = new Guna.UI2.WinForms.Guna2RadioButton();
            this.txtTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.guna2RadioButton5 = new Guna.UI2.WinForms.Guna2RadioButton();
            this.guna2RadioButton4 = new Guna.UI2.WinForms.Guna2RadioButton();
            this.guna2RadioButton3 = new Guna.UI2.WinForms.Guna2RadioButton();
            this.guna2RadioButton2 = new Guna.UI2.WinForms.Guna2RadioButton();
            this.guna2RadioButton1 = new Guna.UI2.WinForms.Guna2RadioButton();
            this.dtpTuNgay = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.dgvDanhSachTaiKhoan = new Guna.UI2.WinForms.Guna2DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsReset = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsXoa = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachTaiKhoan)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.BorderRadius = 5;
            this.dtpDenNgay.Checked = true;
            this.dtpDenNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpDenNgay.Location = new System.Drawing.Point(188, 11);
            this.dtpDenNgay.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpDenNgay.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(152, 30);
            this.dtpDenNgay.TabIndex = 11;
            this.dtpDenNgay.Value = new System.DateTime(2025, 9, 7, 10, 9, 39, 154);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdSDT);
            this.groupBox3.Controls.Add(this.rdCCCD);
            this.groupBox3.Controls.Add(this.rdTen);
            this.groupBox3.Location = new System.Drawing.Point(21, 47);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(319, 49);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tìm kiếm theo";
            // 
            // rdSDT
            // 
            this.rdSDT.AutoSize = true;
            this.rdSDT.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rdSDT.CheckedState.BorderThickness = 0;
            this.rdSDT.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rdSDT.CheckedState.InnerColor = System.Drawing.Color.White;
            this.rdSDT.CheckedState.InnerOffset = -4;
            this.rdSDT.Location = new System.Drawing.Point(203, 19);
            this.rdSDT.Name = "rdSDT";
            this.rdSDT.Size = new System.Drawing.Size(88, 17);
            this.rdSDT.TabIndex = 7;
            this.rdSDT.Text = "Số điện thoại";
            this.rdSDT.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.rdSDT.UncheckedState.BorderThickness = 2;
            this.rdSDT.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.rdSDT.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            // 
            // rdCCCD
            // 
            this.rdCCCD.AutoSize = true;
            this.rdCCCD.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rdCCCD.CheckedState.BorderThickness = 0;
            this.rdCCCD.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rdCCCD.CheckedState.InnerColor = System.Drawing.Color.White;
            this.rdCCCD.CheckedState.InnerOffset = -4;
            this.rdCCCD.Location = new System.Drawing.Point(104, 19);
            this.rdCCCD.Name = "rdCCCD";
            this.rdCCCD.Size = new System.Drawing.Size(54, 17);
            this.rdCCCD.TabIndex = 7;
            this.rdCCCD.Text = "CCCD";
            this.rdCCCD.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.rdCCCD.UncheckedState.BorderThickness = 2;
            this.rdCCCD.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.rdCCCD.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            // 
            // rdTen
            // 
            this.rdTen.AutoSize = true;
            this.rdTen.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rdTen.CheckedState.BorderThickness = 0;
            this.rdTen.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rdTen.CheckedState.InnerColor = System.Drawing.Color.White;
            this.rdTen.CheckedState.InnerOffset = -4;
            this.rdTen.Location = new System.Drawing.Point(15, 19);
            this.rdTen.Name = "rdTen";
            this.rdTen.Size = new System.Drawing.Size(44, 17);
            this.rdTen.TabIndex = 7;
            this.rdTen.Text = "Tên";
            this.rdTen.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.rdTen.UncheckedState.BorderThickness = 2;
            this.rdTen.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.rdTen.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BorderRadius = 12;
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.DefaultText = "";
            this.txtTimKiem.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTimKiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTimKiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiem.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTimKiem.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Location = new System.Drawing.Point(21, 99);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "";
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(237, 31);
            this.txtTimKiem.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.guna2RadioButton3);
            this.groupBox1.Controls.Add(this.guna2RadioButton2);
            this.groupBox1.Controls.Add(this.guna2RadioButton1);
            this.groupBox1.Location = new System.Drawing.Point(379, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 119);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sắp xếp theo";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.guna2RadioButton5);
            this.groupBox2.Controls.Add(this.guna2RadioButton4);
            this.groupBox2.Location = new System.Drawing.Point(106, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(88, 90);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // guna2RadioButton5
            // 
            this.guna2RadioButton5.AutoSize = true;
            this.guna2RadioButton5.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2RadioButton5.CheckedState.BorderThickness = 0;
            this.guna2RadioButton5.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2RadioButton5.CheckedState.InnerColor = System.Drawing.Color.White;
            this.guna2RadioButton5.CheckedState.InnerOffset = -4;
            this.guna2RadioButton5.Location = new System.Drawing.Point(6, 53);
            this.guna2RadioButton5.Name = "guna2RadioButton5";
            this.guna2RadioButton5.Size = new System.Drawing.Size(70, 17);
            this.guna2RadioButton5.TabIndex = 8;
            this.guna2RadioButton5.Text = "Giảm dần";
            this.guna2RadioButton5.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.guna2RadioButton5.UncheckedState.BorderThickness = 2;
            this.guna2RadioButton5.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.guna2RadioButton5.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            // 
            // guna2RadioButton4
            // 
            this.guna2RadioButton4.AutoSize = true;
            this.guna2RadioButton4.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2RadioButton4.CheckedState.BorderThickness = 0;
            this.guna2RadioButton4.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2RadioButton4.CheckedState.InnerColor = System.Drawing.Color.White;
            this.guna2RadioButton4.CheckedState.InnerOffset = -4;
            this.guna2RadioButton4.Location = new System.Drawing.Point(6, 21);
            this.guna2RadioButton4.Name = "guna2RadioButton4";
            this.guna2RadioButton4.Size = new System.Drawing.Size(71, 17);
            this.guna2RadioButton4.TabIndex = 7;
            this.guna2RadioButton4.Text = "Tăng dần";
            this.guna2RadioButton4.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.guna2RadioButton4.UncheckedState.BorderThickness = 2;
            this.guna2RadioButton4.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.guna2RadioButton4.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            // 
            // guna2RadioButton3
            // 
            this.guna2RadioButton3.AutoSize = true;
            this.guna2RadioButton3.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2RadioButton3.CheckedState.BorderThickness = 0;
            this.guna2RadioButton3.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2RadioButton3.CheckedState.InnerColor = System.Drawing.Color.White;
            this.guna2RadioButton3.CheckedState.InnerOffset = -4;
            this.guna2RadioButton3.Location = new System.Drawing.Point(16, 82);
            this.guna2RadioButton3.Name = "guna2RadioButton3";
            this.guna2RadioButton3.Size = new System.Drawing.Size(88, 17);
            this.guna2RadioButton3.TabIndex = 5;
            this.guna2RadioButton3.Text = "Số điện thoại";
            this.guna2RadioButton3.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.guna2RadioButton3.UncheckedState.BorderThickness = 2;
            this.guna2RadioButton3.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.guna2RadioButton3.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            // 
            // guna2RadioButton2
            // 
            this.guna2RadioButton2.AutoSize = true;
            this.guna2RadioButton2.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2RadioButton2.CheckedState.BorderThickness = 0;
            this.guna2RadioButton2.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2RadioButton2.CheckedState.InnerColor = System.Drawing.Color.White;
            this.guna2RadioButton2.CheckedState.InnerOffset = -4;
            this.guna2RadioButton2.Location = new System.Drawing.Point(16, 54);
            this.guna2RadioButton2.Name = "guna2RadioButton2";
            this.guna2RadioButton2.Size = new System.Drawing.Size(54, 17);
            this.guna2RadioButton2.TabIndex = 4;
            this.guna2RadioButton2.Text = "CCCD";
            this.guna2RadioButton2.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.guna2RadioButton2.UncheckedState.BorderThickness = 2;
            this.guna2RadioButton2.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.guna2RadioButton2.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            // 
            // guna2RadioButton1
            // 
            this.guna2RadioButton1.AutoSize = true;
            this.guna2RadioButton1.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2RadioButton1.CheckedState.BorderThickness = 0;
            this.guna2RadioButton1.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2RadioButton1.CheckedState.InnerColor = System.Drawing.Color.White;
            this.guna2RadioButton1.CheckedState.InnerOffset = -4;
            this.guna2RadioButton1.Location = new System.Drawing.Point(16, 27);
            this.guna2RadioButton1.Name = "guna2RadioButton1";
            this.guna2RadioButton1.Size = new System.Drawing.Size(44, 17);
            this.guna2RadioButton1.TabIndex = 3;
            this.guna2RadioButton1.Text = "Tên";
            this.guna2RadioButton1.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.guna2RadioButton1.UncheckedState.BorderThickness = 2;
            this.guna2RadioButton1.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.guna2RadioButton1.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.BorderRadius = 5;
            this.dtpTuNgay.Checked = true;
            this.dtpTuNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpTuNgay.Location = new System.Drawing.Point(21, 11);
            this.dtpTuNgay.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTuNgay.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(152, 30);
            this.dtpTuNgay.TabIndex = 13;
            this.dtpTuNgay.Value = new System.DateTime(2025, 9, 7, 10, 9, 39, 154);
            // 
            // dgvDanhSachTaiKhoan
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvDanhSachTaiKhoan.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDanhSachTaiKhoan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDanhSachTaiKhoan.ColumnHeadersHeight = 28;
            this.dgvDanhSachTaiKhoan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvDanhSachTaiKhoan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.dgvDanhSachTaiKhoan.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDanhSachTaiKhoan.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDanhSachTaiKhoan.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDanhSachTaiKhoan.Location = new System.Drawing.Point(21, 145);
            this.dgvDanhSachTaiKhoan.Name = "dgvDanhSachTaiKhoan";
            this.dgvDanhSachTaiKhoan.RowHeadersVisible = false;
            this.dgvDanhSachTaiKhoan.Size = new System.Drawing.Size(558, 200);
            this.dgvDanhSachTaiKhoan.TabIndex = 14;
            this.dgvDanhSachTaiKhoan.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvDanhSachTaiKhoan.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvDanhSachTaiKhoan.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvDanhSachTaiKhoan.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvDanhSachTaiKhoan.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvDanhSachTaiKhoan.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvDanhSachTaiKhoan.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDanhSachTaiKhoan.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvDanhSachTaiKhoan.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvDanhSachTaiKhoan.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDanhSachTaiKhoan.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvDanhSachTaiKhoan.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvDanhSachTaiKhoan.ThemeStyle.HeaderStyle.Height = 28;
            this.dgvDanhSachTaiKhoan.ThemeStyle.ReadOnly = false;
            this.dgvDanhSachTaiKhoan.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvDanhSachTaiKhoan.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvDanhSachTaiKhoan.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDanhSachTaiKhoan.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvDanhSachTaiKhoan.ThemeStyle.RowsStyle.Height = 22;
            this.dgvDanhSachTaiKhoan.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDanhSachTaiKhoan.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Ten";
            this.Column1.HeaderText = "Tên";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "SDT";
            this.Column2.HeaderText = "Số điện thoại";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "CCCD";
            this.Column3.HeaderText = "CCCD";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "TenTaiKhoan";
            this.Column4.HeaderText = "Tên tài khoản";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "MatKhau";
            this.Column5.HeaderText = "Mật khẩu";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Ngày tham gia";
            this.Column6.Name = "Column6";
            // 
            // btnThem
            // 
            this.btnThem.BorderRadius = 5;
            this.btnThem.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThem.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(272, 102);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(68, 28);
            this.btnThem.TabIndex = 15;
            this.btnThem.Text = "Thêm";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsReset,
            this.cmsXoa});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(156, 48);
            // 
            // cmsReset
            // 
            this.cmsReset.Name = "cmsReset";
            this.cmsReset.Size = new System.Drawing.Size(155, 22);
            this.cmsReset.Text = "Reset mật khẩu";
            // 
            // cmsXoa
            // 
            this.cmsXoa.Name = "cmsXoa";
            this.cmsXoa.Size = new System.Drawing.Size(155, 22);
            this.cmsXoa.Text = "Xóa";
            // 
            // UserControlDanhSachTaiKhoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.dgvDanhSachTaiKhoan);
            this.Controls.Add(this.dtpTuNgay);
            this.Controls.Add(this.dtpDenNgay);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.groupBox1);
            this.Name = "UserControlDanhSachTaiKhoan";
            this.Size = new System.Drawing.Size(612, 358);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachTaiKhoan)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2DateTimePicker dtpDenNgay;
        private System.Windows.Forms.GroupBox groupBox3;
        private Guna.UI2.WinForms.Guna2RadioButton rdSDT;
        private Guna.UI2.WinForms.Guna2RadioButton rdCCCD;
        private Guna.UI2.WinForms.Guna2RadioButton rdTen;
        private Guna.UI2.WinForms.Guna2TextBox txtTimKiem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Guna.UI2.WinForms.Guna2RadioButton guna2RadioButton5;
        private Guna.UI2.WinForms.Guna2RadioButton guna2RadioButton4;
        private Guna.UI2.WinForms.Guna2RadioButton guna2RadioButton3;
        private Guna.UI2.WinForms.Guna2RadioButton guna2RadioButton2;
        private Guna.UI2.WinForms.Guna2RadioButton guna2RadioButton1;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpTuNgay;
        private Guna.UI2.WinForms.Guna2DataGridView dgvDanhSachTaiKhoan;
        private Guna.UI2.WinForms.Guna2Button btnThem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cmsReset;
        private System.Windows.Forms.ToolStripMenuItem cmsXoa;
    }
}
