namespace QuanLyPhongKhachSan.Login.UserControlAdmin
{
    partial class UserControlThongKeChung
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dtpTuNgay = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.cbbNhanVien = new System.Windows.Forms.ComboBox();
            this.chrThongKe = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDenNgay = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.txtSoLuongKhach = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtDoanhThu = new Guna.UI2.WinForms.Guna2TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chrThongKe)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.BorderRadius = 5;
            this.dtpTuNgay.Checked = true;
            this.dtpTuNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(117, 9);
            this.dtpTuNgay.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTuNgay.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(114, 33);
            this.dtpTuNgay.TabIndex = 6;
            this.dtpTuNgay.Value = new System.DateTime(2025, 9, 7, 10, 9, 39, 154);
            // 
            // cbbNhanVien
            // 
            this.cbbNhanVien.FormattingEnabled = true;
            this.cbbNhanVien.Location = new System.Drawing.Point(23, 14);
            this.cbbNhanVien.Name = "cbbNhanVien";
            this.cbbNhanVien.Size = new System.Drawing.Size(86, 21);
            this.cbbNhanVien.TabIndex = 8;
            this.cbbNhanVien.Text = "Nhân viên";
            // 
            // chrThongKe
            // 
            chartArea1.Name = "ChartArea1";
            this.chrThongKe.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chrThongKe.Legends.Add(legend1);
            this.chrThongKe.Location = new System.Drawing.Point(23, 61);
            this.chrThongKe.Name = "chrThongKe";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chrThongKe.Series.Add(series1);
            this.chrThongKe.Size = new System.Drawing.Size(559, 280);
            this.chrThongKe.TabIndex = 9;
            this.chrThongKe.Text = "chart1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(369, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Tổng lượng khách";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.BorderRadius = 5;
            this.dtpDenNgay.Checked = true;
            this.dtpDenNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(237, 9);
            this.dtpDenNgay.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpDenNgay.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(114, 33);
            this.dtpDenNgay.TabIndex = 11;
            this.dtpDenNgay.Value = new System.DateTime(2025, 9, 7, 10, 9, 39, 154);
            // 
            // txtSoLuongKhach
            // 
            this.txtSoLuongKhach.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSoLuongKhach.DefaultText = "";
            this.txtSoLuongKhach.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSoLuongKhach.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSoLuongKhach.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSoLuongKhach.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSoLuongKhach.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSoLuongKhach.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSoLuongKhach.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSoLuongKhach.Location = new System.Drawing.Point(485, 14);
            this.txtSoLuongKhach.Name = "txtSoLuongKhach";
            this.txtSoLuongKhach.PlaceholderText = "";
            this.txtSoLuongKhach.SelectedText = "";
            this.txtSoLuongKhach.Size = new System.Drawing.Size(88, 27);
            this.txtSoLuongKhach.TabIndex = 12;
            // 
            // txtDoanhThu
            // 
            this.txtDoanhThu.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDoanhThu.DefaultText = "";
            this.txtDoanhThu.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtDoanhThu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtDoanhThu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDoanhThu.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDoanhThu.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDoanhThu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDoanhThu.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDoanhThu.Location = new System.Drawing.Point(453, 328);
            this.txtDoanhThu.Name = "txtDoanhThu";
            this.txtDoanhThu.PlaceholderText = "";
            this.txtDoanhThu.SelectedText = "";
            this.txtDoanhThu.Size = new System.Drawing.Size(129, 27);
            this.txtDoanhThu.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(351, 335);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 17);
            this.label3.TabIndex = 15;
            this.label3.Text = "Tổng doanh thu";
            // 
            // UserControlThongKeChung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtDoanhThu);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSoLuongKhach);
            this.Controls.Add(this.dtpDenNgay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chrThongKe);
            this.Controls.Add(this.cbbNhanVien);
            this.Controls.Add(this.dtpTuNgay);
            this.Name = "UserControlThongKeChung";
            this.Size = new System.Drawing.Size(612, 358);
            ((System.ComponentModel.ISupportInitialize)(this.chrThongKe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpTuNgay;
        private System.Windows.Forms.ComboBox cbbNhanVien;
        private System.Windows.Forms.DataVisualization.Charting.Chart chrThongKe;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpDenNgay;
        private Guna.UI2.WinForms.Guna2TextBox txtSoLuongKhach;
        private Guna.UI2.WinForms.Guna2TextBox txtDoanhThu;
        private System.Windows.Forms.Label label3;
    }
}
