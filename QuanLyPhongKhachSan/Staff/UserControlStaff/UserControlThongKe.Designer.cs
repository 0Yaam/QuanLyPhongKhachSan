namespace QuanLyPhongKhachSan.Bar
{
    partial class UserControlThongKe
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dtpTuNgay = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.chrThongKe = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtSoLuongKhach = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtTongTienThuong = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.dtpDenNgay = new Guna.UI2.WinForms.Guna2DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.chrThongKe)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.BorderRadius = 5;
            this.dtpTuNgay.Checked = true;
            this.dtpTuNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(135, 25);
            this.dtpTuNgay.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTuNgay.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(130, 36);
            this.dtpTuNgay.TabIndex = 31;
            this.dtpTuNgay.Value = new System.DateTime(2025, 9, 10, 21, 4, 4, 463);
            // 
            // chrThongKe
            // 
            chartArea4.Name = "ChartArea1";
            this.chrThongKe.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chrThongKe.Legends.Add(legend4);
            this.chrThongKe.Location = new System.Drawing.Point(29, 86);
            this.chrThongKe.Name = "chrThongKe";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chrThongKe.Series.Add(series4);
            this.chrThongKe.Size = new System.Drawing.Size(993, 520);
            this.chrThongKe.TabIndex = 36;
            this.chrThongKe.Text = "chart1";
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(60, 31);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(69, 24);
            this.guna2HtmlLabel1.TabIndex = 38;
            this.guna2HtmlLabel1.Text = "Từ ngày:";
            // 
            // guna2HtmlLabel3
            // 
            this.guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel3.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel3.Location = new System.Drawing.Point(514, 34);
            this.guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            this.guna2HtmlLabel3.Size = new System.Drawing.Size(123, 24);
            this.guna2HtmlLabel3.TabIndex = 40;
            this.guna2HtmlLabel3.Text = "Số lượng khách:";
            // 
            // txtSoLuongKhach
            // 
            this.txtSoLuongKhach.BorderRadius = 5;
            this.txtSoLuongKhach.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSoLuongKhach.DefaultText = "";
            this.txtSoLuongKhach.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSoLuongKhach.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSoLuongKhach.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSoLuongKhach.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSoLuongKhach.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSoLuongKhach.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSoLuongKhach.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSoLuongKhach.Location = new System.Drawing.Point(643, 33);
            this.txtSoLuongKhach.Name = "txtSoLuongKhach";
            this.txtSoLuongKhach.PlaceholderForeColor = System.Drawing.Color.Black;
            this.txtSoLuongKhach.PlaceholderText = "";
            this.txtSoLuongKhach.ReadOnly = true;
            this.txtSoLuongKhach.SelectedText = "";
            this.txtSoLuongKhach.Size = new System.Drawing.Size(100, 28);
            this.txtSoLuongKhach.TabIndex = 41;
            // 
            // txtTongTienThuong
            // 
            this.txtTongTienThuong.BorderRadius = 5;
            this.txtTongTienThuong.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTongTienThuong.DefaultText = "";
            this.txtTongTienThuong.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTongTienThuong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTongTienThuong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTongTienThuong.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTongTienThuong.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTongTienThuong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTongTienThuong.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTongTienThuong.Location = new System.Drawing.Point(905, 31);
            this.txtTongTienThuong.Name = "txtTongTienThuong";
            this.txtTongTienThuong.PlaceholderText = "";
            this.txtTongTienThuong.ReadOnly = true;
            this.txtTongTienThuong.SelectedText = "";
            this.txtTongTienThuong.Size = new System.Drawing.Size(117, 28);
            this.txtTongTienThuong.TabIndex = 42;
            // 
            // guna2HtmlLabel4
            // 
            this.guna2HtmlLabel4.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel4.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel4.Location = new System.Drawing.Point(755, 31);
            this.guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            this.guna2HtmlLabel4.Size = new System.Drawing.Size(144, 28);
            this.guna2HtmlLabel4.TabIndex = 43;
            this.guna2HtmlLabel4.Text = "Tổng hoa hồng:";
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(280, 31);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(78, 24);
            this.guna2HtmlLabel2.TabIndex = 45;
            this.guna2HtmlLabel2.Text = "Đến ngày:";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.BorderRadius = 5;
            this.dtpDenNgay.Checked = true;
            this.dtpDenNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(364, 25);
            this.dtpDenNgay.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpDenNgay.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(130, 36);
            this.dtpDenNgay.TabIndex = 44;
            this.dtpDenNgay.Value = new System.DateTime(2025, 9, 10, 21, 4, 4, 463);
            // 
            // UserControlThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.guna2HtmlLabel2);
            this.Controls.Add(this.dtpDenNgay);
            this.Controls.Add(this.guna2HtmlLabel4);
            this.Controls.Add(this.txtTongTienThuong);
            this.Controls.Add(this.txtSoLuongKhach);
            this.Controls.Add(this.guna2HtmlLabel3);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Controls.Add(this.chrThongKe);
            this.Controls.Add(this.dtpTuNgay);
            this.Name = "UserControlThongKe";
            this.Size = new System.Drawing.Size(1113, 627);
            this.Load += new System.EventHandler(this.UserControlThongKe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chrThongKe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2DateTimePicker dtpTuNgay;
        private System.Windows.Forms.DataVisualization.Charting.Chart chrThongKe;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2TextBox txtSoLuongKhach;
        private Guna.UI2.WinForms.Guna2TextBox txtTongTienThuong;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpDenNgay;
    }
}
