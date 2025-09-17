namespace QuanLyPhongKhachSan.Staff.UserControlStaff
{
    partial class UserControlLichSuHoaDon
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rdSoPhong = new Guna.UI2.WinForms.Guna2RadioButton();
            this.rdCCCD = new Guna.UI2.WinForms.Guna2RadioButton();
            this.rdTen = new Guna.UI2.WinForms.Guna2RadioButton();
            this.rdSDT = new Guna.UI2.WinForms.Guna2RadioButton();
            this.txtTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.dgvLichSu = new Guna.UI2.WinForms.Guna2DataGridView();
            this.dtpNgay = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.TenKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCCD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoPhong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThoiGianIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoaiHoaDon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rdSoPhong);
            this.groupBox4.Controls.Add(this.rdCCCD);
            this.groupBox4.Controls.Add(this.rdTen);
            this.groupBox4.Controls.Add(this.rdSDT);
            this.groupBox4.Location = new System.Drawing.Point(458, 9);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(337, 49);
            this.groupBox4.TabIndex = 25;
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
            this.txtTimKiem.Location = new System.Drawing.Point(31, 19);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "";
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(418, 31);
            this.txtTimKiem.TabIndex = 24;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // dgvLichSu
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvLichSu.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLichSu.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLichSu.ColumnHeadersHeight = 15;
            this.dgvLichSu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvLichSu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TenKH,
            this.CCCD,
            this.SDT,
            this.SoPhong,
            this.ThoiGianIn,
            this.LoaiHoaDon});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLichSu.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLichSu.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvLichSu.Location = new System.Drawing.Point(20, 74);
            this.dgvLichSu.Name = "dgvLichSu";
            this.dgvLichSu.RowHeadersVisible = false;
            this.dgvLichSu.Size = new System.Drawing.Size(1038, 518);
            this.dgvLichSu.TabIndex = 26;
            this.dgvLichSu.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvLichSu.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvLichSu.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvLichSu.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvLichSu.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvLichSu.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvLichSu.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvLichSu.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvLichSu.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvLichSu.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvLichSu.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvLichSu.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvLichSu.ThemeStyle.HeaderStyle.Height = 15;
            this.dgvLichSu.ThemeStyle.ReadOnly = false;
            this.dgvLichSu.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvLichSu.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvLichSu.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvLichSu.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvLichSu.ThemeStyle.RowsStyle.Height = 22;
            this.dgvLichSu.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvLichSu.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // dtpNgay
            // 
            this.dtpNgay.BorderRadius = 5;
            this.dtpNgay.Checked = true;
            this.dtpNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNgay.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpNgay.Location = new System.Drawing.Point(807, 19);
            this.dtpNgay.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpNgay.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpNgay.Name = "dtpNgay";
            this.dtpNgay.Size = new System.Drawing.Size(225, 36);
            this.dtpNgay.TabIndex = 27;
            this.dtpNgay.Value = new System.DateTime(2025, 9, 10, 21, 4, 4, 463);
            // 
            // TenKH
            // 
            this.TenKH.DataPropertyName = "TenKH";
            this.TenKH.HeaderText = "Tên Khách Hàng";
            this.TenKH.Name = "TenKH";
            // 
            // CCCD
            // 
            this.CCCD.DataPropertyName = "CCCD";
            this.CCCD.HeaderText = "Căn cước công dân/Passport";
            this.CCCD.Name = "CCCD";
            // 
            // SDT
            // 
            this.SDT.DataPropertyName = "SDT";
            this.SDT.HeaderText = "Số điện thoại";
            this.SDT.Name = "SDT";
            // 
            // SoPhong
            // 
            this.SoPhong.HeaderText = "Số phòng";
            this.SoPhong.Name = "SoPhong";
            // 
            // ThoiGianIn
            // 
            this.ThoiGianIn.DataPropertyName = "ThoiGianIn";
            this.ThoiGianIn.HeaderText = "Thời gian in";
            this.ThoiGianIn.Name = "ThoiGianIn";
            // 
            // LoaiHoaDon
            // 
            this.LoaiHoaDon.DataPropertyName = "LoaiHoaDon";
            this.LoaiHoaDon.HeaderText = "Loại hóa đơn";
            this.LoaiHoaDon.Name = "LoaiHoaDon";
            // 
            // UserControlLichSuHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtpNgay);
            this.Controls.Add(this.dgvLichSu);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.txtTimKiem);
            this.Name = "UserControlLichSuHoaDon";
            this.Size = new System.Drawing.Size(1084, 614);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private Guna.UI2.WinForms.Guna2RadioButton rdSoPhong;
        private Guna.UI2.WinForms.Guna2RadioButton rdCCCD;
        private Guna.UI2.WinForms.Guna2RadioButton rdTen;
        private Guna.UI2.WinForms.Guna2RadioButton rdSDT;
        private Guna.UI2.WinForms.Guna2TextBox txtTimKiem;
        private Guna.UI2.WinForms.Guna2DataGridView dgvLichSu;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNgay;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCCD;
        private System.Windows.Forms.DataGridViewTextBoxColumn SDT;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoPhong;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThoiGianIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoaiHoaDon;
    }
}
