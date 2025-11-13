namespace QuanLyPhongKhachSan.Staff
{
    partial class frmThemKH
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.guna2HtmlLabel8 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtTamTinh = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnInHoaDon = new Guna.UI2.WinForms.Guna2Button();
            this.dtpNgayNhan = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.dtpNgayTraDuKien = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.txtSDT = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2HtmlLabel7 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtCCCD = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2HtmlLabel6 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtTenKH = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2HtmlLabel5 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnHoanThanh = new Guna.UI2.WinForms.Guna2Button();
            this.btnHoaDon2 = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // guna2HtmlLabel8
            // 
            this.guna2HtmlLabel8.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel8.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel8.Location = new System.Drawing.Point(8, 98);
            this.guna2HtmlLabel8.Name = "guna2HtmlLabel8";
            this.guna2HtmlLabel8.Size = new System.Drawing.Size(76, 24);
            this.guna2HtmlLabel8.TabIndex = 55;
            this.guna2HtmlLabel8.Text = "Tạm tính: ";
            // 
            // txtTamTinh
            // 
            this.txtTamTinh.BorderRadius = 5;
            this.txtTamTinh.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTamTinh.DefaultText = "";
            this.txtTamTinh.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTamTinh.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTamTinh.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTamTinh.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTamTinh.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTamTinh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTamTinh.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTamTinh.Location = new System.Drawing.Point(104, 98);
            this.txtTamTinh.Name = "txtTamTinh";
            this.txtTamTinh.PlaceholderText = "";
            this.txtTamTinh.ReadOnly = true;
            this.txtTamTinh.SelectedText = "";
            this.txtTamTinh.Size = new System.Drawing.Size(99, 23);
            this.txtTamTinh.TabIndex = 54;
            // 
            // btnInHoaDon
            // 
            this.btnInHoaDon.BorderRadius = 10;
            this.btnInHoaDon.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnInHoaDon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnInHoaDon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnInHoaDon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnInHoaDon.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInHoaDon.ForeColor = System.Drawing.Color.White;
            this.btnInHoaDon.Location = new System.Drawing.Point(307, 95);
            this.btnInHoaDon.Name = "btnInHoaDon";
            this.btnInHoaDon.Size = new System.Drawing.Size(92, 36);
            this.btnInHoaDon.TabIndex = 53;
            this.btnInHoaDon.Text = "Hóa đơn 1";
            this.btnInHoaDon.Click += new System.EventHandler(this.btnInHoaDon_Click);
            // 
            // dtpNgayNhan
            // 
            this.dtpNgayNhan.BorderRadius = 10;
            this.dtpNgayNhan.Checked = true;
            this.dtpNgayNhan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNgayNhan.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpNgayNhan.Location = new System.Drawing.Point(295, 11);
            this.dtpNgayNhan.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpNgayNhan.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpNgayNhan.Name = "dtpNgayNhan";
            this.dtpNgayNhan.Size = new System.Drawing.Size(201, 36);
            this.dtpNgayNhan.TabIndex = 52;
            this.dtpNgayNhan.Value = new System.DateTime(2025, 9, 7, 12, 29, 11, 39);
            // 
            // dtpNgayTraDuKien
            // 
            this.dtpNgayTraDuKien.BorderRadius = 10;
            this.dtpNgayTraDuKien.Checked = true;
            this.dtpNgayTraDuKien.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNgayTraDuKien.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpNgayTraDuKien.Location = new System.Drawing.Point(295, 53);
            this.dtpNgayTraDuKien.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpNgayTraDuKien.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpNgayTraDuKien.Name = "dtpNgayTraDuKien";
            this.dtpNgayTraDuKien.Size = new System.Drawing.Size(201, 36);
            this.dtpNgayTraDuKien.TabIndex = 51;
            this.dtpNgayTraDuKien.Value = new System.DateTime(2025, 9, 7, 12, 29, 11, 39);
            // 
            // txtSDT
            // 
            this.txtSDT.BorderRadius = 5;
            this.txtSDT.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSDT.DefaultText = "";
            this.txtSDT.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSDT.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSDT.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSDT.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSDT.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSDT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSDT.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSDT.Location = new System.Drawing.Point(104, 71);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.PlaceholderText = "";
            this.txtSDT.SelectedText = "";
            this.txtSDT.Size = new System.Drawing.Size(185, 18);
            this.txtSDT.TabIndex = 49;
            // 
            // guna2HtmlLabel7
            // 
            this.guna2HtmlLabel7.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel7.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel7.Location = new System.Drawing.Point(8, 70);
            this.guna2HtmlLabel7.Name = "guna2HtmlLabel7";
            this.guna2HtmlLabel7.Size = new System.Drawing.Size(26, 19);
            this.guna2HtmlLabel7.TabIndex = 48;
            this.guna2HtmlLabel7.Text = "SĐT";
            // 
            // txtCCCD
            // 
            this.txtCCCD.BorderRadius = 5;
            this.txtCCCD.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCCCD.DefaultText = "";
            this.txtCCCD.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtCCCD.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtCCCD.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCCCD.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCCCD.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCCCD.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCCCD.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCCCD.Location = new System.Drawing.Point(104, 41);
            this.txtCCCD.Name = "txtCCCD";
            this.txtCCCD.PlaceholderText = "";
            this.txtCCCD.SelectedText = "";
            this.txtCCCD.Size = new System.Drawing.Size(185, 18);
            this.txtCCCD.TabIndex = 47;
            // 
            // guna2HtmlLabel6
            // 
            this.guna2HtmlLabel6.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel6.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel6.Location = new System.Drawing.Point(8, 40);
            this.guna2HtmlLabel6.Name = "guna2HtmlLabel6";
            this.guna2HtmlLabel6.Size = new System.Drawing.Size(41, 19);
            this.guna2HtmlLabel6.TabIndex = 46;
            this.guna2HtmlLabel6.Text = "CMND";
            // 
            // txtTenKH
            // 
            this.txtTenKH.BorderRadius = 5;
            this.txtTenKH.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenKH.DefaultText = "";
            this.txtTenKH.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTenKH.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTenKH.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenKH.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenKH.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenKH.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTenKH.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenKH.Location = new System.Drawing.Point(104, 12);
            this.txtTenKH.Name = "txtTenKH";
            this.txtTenKH.PlaceholderText = "";
            this.txtTenKH.SelectedText = "";
            this.txtTenKH.Size = new System.Drawing.Size(185, 18);
            this.txtTenKH.TabIndex = 45;
            // 
            // guna2HtmlLabel5
            // 
            this.guna2HtmlLabel5.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel5.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel5.Location = new System.Drawing.Point(8, 11);
            this.guna2HtmlLabel5.Name = "guna2HtmlLabel5";
            this.guna2HtmlLabel5.Size = new System.Drawing.Size(91, 19);
            this.guna2HtmlLabel5.TabIndex = 44;
            this.guna2HtmlLabel5.Text = "Tên khách hàng";
            // 
            // btnHoanThanh
            // 
            this.btnHoanThanh.BorderRadius = 10;
            this.btnHoanThanh.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnHoanThanh.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnHoanThanh.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnHoanThanh.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnHoanThanh.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHoanThanh.ForeColor = System.Drawing.Color.White;
            this.btnHoanThanh.Location = new System.Drawing.Point(209, 95);
            this.btnHoanThanh.Name = "btnHoanThanh";
            this.btnHoanThanh.Size = new System.Drawing.Size(92, 36);
            this.btnHoanThanh.TabIndex = 56;
            this.btnHoanThanh.Text = "Hoàn thành";
            this.btnHoanThanh.Click += new System.EventHandler(this.btnHoanThanh_Click);
            // 
            // btnHoaDon2
            // 
            this.btnHoaDon2.BorderRadius = 10;
            this.btnHoaDon2.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnHoaDon2.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnHoaDon2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnHoaDon2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnHoaDon2.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHoaDon2.ForeColor = System.Drawing.Color.White;
            this.btnHoaDon2.Location = new System.Drawing.Point(405, 95);
            this.btnHoaDon2.Name = "btnHoaDon2";
            this.btnHoaDon2.Size = new System.Drawing.Size(92, 36);
            this.btnHoaDon2.TabIndex = 57;
            this.btnHoaDon2.Text = "Hóa đơn 2";
            this.btnHoaDon2.Click += new System.EventHandler(this.btnHoaDon2_Click);
            // 
            // frmThemKH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 149);
            this.Controls.Add(this.btnHoaDon2);
            this.Controls.Add(this.btnHoanThanh);
            this.Controls.Add(this.guna2HtmlLabel8);
            this.Controls.Add(this.txtTamTinh);
            this.Controls.Add(this.btnInHoaDon);
            this.Controls.Add(this.dtpNgayNhan);
            this.Controls.Add(this.dtpNgayTraDuKien);
            this.Controls.Add(this.txtSDT);
            this.Controls.Add(this.guna2HtmlLabel7);
            this.Controls.Add(this.txtCCCD);
            this.Controls.Add(this.guna2HtmlLabel6);
            this.Controls.Add(this.txtTenKH);
            this.Controls.Add(this.guna2HtmlLabel5);
            this.Name = "frmThemKH";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form thêm khách hàng cho nhiều phòng";
            this.Load += new System.EventHandler(this.frmThemKH_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel8;
        private Guna.UI2.WinForms.Guna2TextBox txtTamTinh;
        private Guna.UI2.WinForms.Guna2Button btnInHoaDon;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNgayNhan;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNgayTraDuKien;
        private Guna.UI2.WinForms.Guna2TextBox txtSDT;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel7;
        private Guna.UI2.WinForms.Guna2TextBox txtCCCD;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel6;
        private Guna.UI2.WinForms.Guna2TextBox txtTenKH;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel5;
        private Guna.UI2.WinForms.Guna2Button btnHoanThanh;
        private Guna.UI2.WinForms.Guna2Button btnHoaDon2;
    }
}