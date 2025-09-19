namespace QuanLyPhongKhachSan.Staff.UserControlStaff
{
    partial class UserControlDatPhong
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
            this.rdPhongTrong = new System.Windows.Forms.RadioButton();
            this.rdPhongDaDat = new System.Windows.Forms.RadioButton();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2Panel4 = new Guna.UI2.WinForms.Guna2Panel();
            this.cbLoaiPhong = new System.Windows.Forms.ComboBox();
            this.txtSoPhong = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbLoai = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnReset = new Guna.UI2.WinForms.Guna2Button();
            this.rdTang = new System.Windows.Forms.RadioButton();
            this.rdGiam = new System.Windows.Forms.RadioButton();
            this.flpContain = new System.Windows.Forms.FlowLayoutPanel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.xóaThôngTinKháchHàngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rdSoPhong = new Guna.UI2.WinForms.Guna2RadioButton();
            this.rdCCCD = new Guna.UI2.WinForms.Guna2RadioButton();
            this.rdTen = new Guna.UI2.WinForms.Guna2RadioButton();
            this.rdSDT = new Guna.UI2.WinForms.Guna2RadioButton();
            this.txtTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.dtpNgayHienTai = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.btnThemKH = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.flpContain.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdPhongTrong
            // 
            this.rdPhongTrong.AutoSize = true;
            this.rdPhongTrong.Location = new System.Drawing.Point(6, 24);
            this.rdPhongTrong.Name = "rdPhongTrong";
            this.rdPhongTrong.Size = new System.Drawing.Size(102, 22);
            this.rdPhongTrong.TabIndex = 6;
            this.rdPhongTrong.TabStop = true;
            this.rdPhongTrong.Text = "Phòng trống";
            this.rdPhongTrong.UseVisualStyleBackColor = true;
            this.rdPhongTrong.CheckedChanged += new System.EventHandler(this.rdPhongTrong_CheckedChanged);
            // 
            // rdPhongDaDat
            // 
            this.rdPhongDaDat.AutoSize = true;
            this.rdPhongDaDat.Location = new System.Drawing.Point(6, 52);
            this.rdPhongDaDat.Name = "rdPhongDaDat";
            this.rdPhongDaDat.Size = new System.Drawing.Size(106, 22);
            this.rdPhongDaDat.TabIndex = 7;
            this.rdPhongDaDat.TabStop = true;
            this.rdPhongDaDat.Text = "Phòng đã đặt";
            this.rdPhongDaDat.UseVisualStyleBackColor = true;
            this.rdPhongDaDat.CheckedChanged += new System.EventHandler(this.rdPhongDaDat_CheckedChanged);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.BorderColor = System.Drawing.Color.Black;
            this.guna2Panel1.BorderRadius = 30;
            this.guna2Panel1.Controls.Add(this.label1);
            this.guna2Panel1.Controls.Add(this.guna2Panel4);
            this.guna2Panel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.guna2Panel1.Location = new System.Drawing.Point(20, 20);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(20);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(233, 114);
            this.guna2Panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Phòng 101";
            // 
            // guna2Panel4
            // 
            this.guna2Panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.guna2Panel4.Location = new System.Drawing.Point(0, 89);
            this.guna2Panel4.Name = "guna2Panel4";
            this.guna2Panel4.Size = new System.Drawing.Size(233, 25);
            this.guna2Panel4.TabIndex = 5;
            // 
            // cbLoaiPhong
            // 
            this.cbLoaiPhong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLoaiPhong.FormattingEnabled = true;
            this.cbLoaiPhong.Location = new System.Drawing.Point(906, 391);
            this.cbLoaiPhong.Name = "cbLoaiPhong";
            this.cbLoaiPhong.Size = new System.Drawing.Size(102, 21);
            this.cbLoaiPhong.TabIndex = 16;
            // 
            // txtSoPhong
            // 
            this.txtSoPhong.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSoPhong.DefaultText = "";
            this.txtSoPhong.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSoPhong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSoPhong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSoPhong.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSoPhong.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSoPhong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSoPhong.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSoPhong.Location = new System.Drawing.Point(906, 367);
            this.txtSoPhong.Name = "txtSoPhong";
            this.txtSoPhong.PlaceholderText = "";
            this.txtSoPhong.SelectedText = "";
            this.txtSoPhong.Size = new System.Drawing.Size(102, 18);
            this.txtSoPhong.TabIndex = 17;
            // 
            // btnThem
            // 
            this.btnThem.BorderRadius = 10;
            this.btnThem.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThem.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThem.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(917, 427);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(83, 38);
            this.btnThem.TabIndex = 12;
            this.btnThem.Text = "Thêm";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdPhongTrong);
            this.groupBox1.Controls.Add(this.rdPhongDaDat);
            this.groupBox1.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(900, 152);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(122, 90);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Loại phòng";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbLoai);
            this.groupBox2.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(900, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(119, 67);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Trạng thái";
            // 
            // cbLoai
            // 
            this.cbLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLoai.FormattingEnabled = true;
            this.cbLoai.Location = new System.Drawing.Point(17, 24);
            this.cbLoai.Name = "cbLoai";
            this.cbLoai.Size = new System.Drawing.Size(83, 26);
            this.cbLoai.TabIndex = 27;
            this.cbLoai.SelectedIndexChanged += new System.EventHandler(this.cbLoai_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdTang);
            this.groupBox3.Controls.Add(this.rdGiam);
            this.groupBox3.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(900, 257);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(122, 92);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Sắp xếp theo";
            // 
            // btnReset
            // 
            this.btnReset.BorderRadius = 10;
            this.btnReset.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnReset.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnReset.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnReset.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(976, 232);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(59, 33);
            this.btnReset.TabIndex = 8;
            this.btnReset.Text = "Reset";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // rdTang
            // 
            this.rdTang.AutoSize = true;
            this.rdTang.Location = new System.Drawing.Point(6, 24);
            this.rdTang.Name = "rdTang";
            this.rdTang.Size = new System.Drawing.Size(58, 22);
            this.rdTang.TabIndex = 6;
            this.rdTang.TabStop = true;
            this.rdTang.Text = "Tăng";
            this.rdTang.UseVisualStyleBackColor = true;
            this.rdTang.CheckedChanged += new System.EventHandler(this.rdTang_CheckedChanged);
            // 
            // rdGiam
            // 
            this.rdGiam.AutoSize = true;
            this.rdGiam.Location = new System.Drawing.Point(6, 52);
            this.rdGiam.Name = "rdGiam";
            this.rdGiam.Size = new System.Drawing.Size(60, 22);
            this.rdGiam.TabIndex = 7;
            this.rdGiam.TabStop = true;
            this.rdGiam.Text = "Giảm";
            this.rdGiam.UseVisualStyleBackColor = true;
            this.rdGiam.CheckedChanged += new System.EventHandler(this.rdGiam_CheckedChanged);
            // 
            // flpContain
            // 
            this.flpContain.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.flpContain.Controls.Add(this.guna2Panel1);
            this.flpContain.Location = new System.Drawing.Point(3, 68);
            this.flpContain.Name = "flpContain";
            this.flpContain.Size = new System.Drawing.Size(875, 540);
            this.flpContain.TabIndex = 8;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xóaThôngTinKháchHàngToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(212, 26);
            // 
            // xóaThôngTinKháchHàngToolStripMenuItem
            // 
            this.xóaThôngTinKháchHàngToolStripMenuItem.Name = "xóaThôngTinKháchHàngToolStripMenuItem";
            this.xóaThôngTinKháchHàngToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.xóaThôngTinKháchHàngToolStripMenuItem.Text = "Xóa thông tin khách hàng";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rdSoPhong);
            this.groupBox4.Controls.Add(this.rdCCCD);
            this.groupBox4.Controls.Add(this.rdTen);
            this.groupBox4.Controls.Add(this.rdSDT);
            this.groupBox4.Location = new System.Drawing.Point(472, 9);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(337, 49);
            this.groupBox4.TabIndex = 23;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Tìm kiếm theo";
            // 
            // rdSoPhong
            // 
            this.rdSoPhong.AutoSize = true;
            this.rdSoPhong.Checked = true;
            this.rdSoPhong.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rdSoPhong.CheckedState.BorderThickness = 0;
            this.rdSoPhong.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rdSoPhong.CheckedState.InnerColor = System.Drawing.Color.White;
            this.rdSoPhong.CheckedState.InnerOffset = -4;
            this.rdSoPhong.Location = new System.Drawing.Point(6, 19);
            this.rdSoPhong.Name = "rdSoPhong";
            this.rdSoPhong.Size = new System.Drawing.Size(71, 17);
            this.rdSoPhong.TabIndex = 8;
            this.rdSoPhong.TabStop = true;
            this.rdSoPhong.Text = "Số phòng";
            this.rdSoPhong.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.rdSoPhong.UncheckedState.BorderThickness = 2;
            this.rdSoPhong.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.rdSoPhong.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.rdSoPhong.CheckedChanged += new System.EventHandler(this.rdSoPhong_CheckedChanged);
            // 
            // rdCCCD
            // 
            this.rdCCCD.AutoSize = true;
            this.rdCCCD.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rdCCCD.CheckedState.BorderThickness = 0;
            this.rdCCCD.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rdCCCD.CheckedState.InnerColor = System.Drawing.Color.White;
            this.rdCCCD.CheckedState.InnerOffset = -4;
            this.rdCCCD.Location = new System.Drawing.Point(207, 19);
            this.rdCCCD.Name = "rdCCCD";
            this.rdCCCD.Size = new System.Drawing.Size(54, 17);
            this.rdCCCD.TabIndex = 7;
            this.rdCCCD.Text = "CCCD";
            this.rdCCCD.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.rdCCCD.UncheckedState.BorderThickness = 2;
            this.rdCCCD.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.rdCCCD.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.rdCCCD.CheckedChanged += new System.EventHandler(this.rdCCCD_CheckedChanged);
            // 
            // rdTen
            // 
            this.rdTen.AutoSize = true;
            this.rdTen.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rdTen.CheckedState.BorderThickness = 0;
            this.rdTen.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rdTen.CheckedState.InnerColor = System.Drawing.Color.White;
            this.rdTen.CheckedState.InnerOffset = -4;
            this.rdTen.Location = new System.Drawing.Point(287, 19);
            this.rdTen.Name = "rdTen";
            this.rdTen.Size = new System.Drawing.Size(44, 17);
            this.rdTen.TabIndex = 7;
            this.rdTen.Text = "Tên";
            this.rdTen.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.rdTen.UncheckedState.BorderThickness = 2;
            this.rdTen.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.rdTen.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.rdTen.CheckedChanged += new System.EventHandler(this.rdTen_CheckedChanged);
            // 
            // rdSDT
            // 
            this.rdSDT.AutoSize = true;
            this.rdSDT.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rdSDT.CheckedState.BorderThickness = 0;
            this.rdSDT.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rdSDT.CheckedState.InnerColor = System.Drawing.Color.White;
            this.rdSDT.CheckedState.InnerOffset = -4;
            this.rdSDT.Location = new System.Drawing.Point(93, 19);
            this.rdSDT.Name = "rdSDT";
            this.rdSDT.Size = new System.Drawing.Size(88, 17);
            this.rdSDT.TabIndex = 7;
            this.rdSDT.Text = "Số điện thoại";
            this.rdSDT.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.rdSDT.UncheckedState.BorderThickness = 2;
            this.rdSDT.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.rdSDT.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.rdSDT.CheckedChanged += new System.EventHandler(this.rdSDT_CheckedChanged);
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
            this.txtTimKiem.Location = new System.Drawing.Point(17, 19);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "";
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(443, 31);
            this.txtTimKiem.TabIndex = 22;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // dtpNgayHienTai
            // 
            this.dtpNgayHienTai.BorderRadius = 5;
            this.dtpNgayHienTai.Checked = true;
            this.dtpNgayHienTai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNgayHienTai.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpNgayHienTai.Location = new System.Drawing.Point(815, 17);
            this.dtpNgayHienTai.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpNgayHienTai.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpNgayHienTai.Name = "dtpNgayHienTai";
            this.dtpNgayHienTai.Size = new System.Drawing.Size(204, 39);
            this.dtpNgayHienTai.TabIndex = 24;
            this.dtpNgayHienTai.Value = new System.DateTime(2025, 9, 7, 12, 22, 27, 197);
            // 
            // btnThemKH
            // 
            this.btnThemKH.BorderRadius = 10;
            this.btnThemKH.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThemKH.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThemKH.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThemKH.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThemKH.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemKH.ForeColor = System.Drawing.Color.White;
            this.btnThemKH.Location = new System.Drawing.Point(909, 517);
            this.btnThemKH.Name = "btnThemKH";
            this.btnThemKH.Size = new System.Drawing.Size(99, 38);
            this.btnThemKH.TabIndex = 25;
            this.btnThemKH.Text = "Thêm khách hàng";
            // 
            // UserControlDatPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnThemKH);
            this.Controls.Add(this.dtpNgayHienTai);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.flpContain);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtSoPhong);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbLoaiPhong);
            this.Controls.Add(this.btnThem);
            this.Name = "UserControlDatPhong";
            this.Size = new System.Drawing.Size(1038, 632);
            this.Load += new System.EventHandler(this.UserControlDatPhong_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.flpContain.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RadioButton rdPhongTrong;
        private System.Windows.Forms.RadioButton rdPhongDaDat;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel4;
        private System.Windows.Forms.ComboBox cbLoaiPhong;
        private Guna.UI2.WinForms.Guna2TextBox txtSoPhong;
        private Guna.UI2.WinForms.Guna2Button btnThem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdTang;
        private System.Windows.Forms.RadioButton rdGiam;
        private System.Windows.Forms.FlowLayoutPanel flpContain;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem xóaThôngTinKháchHàngToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox4;
        private Guna.UI2.WinForms.Guna2RadioButton rdSDT;
        private Guna.UI2.WinForms.Guna2RadioButton rdCCCD;
        private Guna.UI2.WinForms.Guna2RadioButton rdTen;
        private Guna.UI2.WinForms.Guna2TextBox txtTimKiem;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNgayHienTai;
        private Guna.UI2.WinForms.Guna2Button btnThemKH;
        private System.Windows.Forms.ComboBox cbLoai;
        private Guna.UI2.WinForms.Guna2RadioButton rdSoPhong;
        private Guna.UI2.WinForms.Guna2Button btnReset;
    }
}
