namespace QuanLyPhongKhachSan.Login.UserControlAdmin
{
    partial class UserControlLichSuThayDoi
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
            this.dtpTuNgay = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.cbbTenNhanVien = new System.Windows.Forms.ComboBox();
            this.dgvLog = new Guna.UI2.WinForms.Guna2DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsXoa = new System.Windows.Forms.ToolStripMenuItem();
            this.dtpDenNgay = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.chkLocTheoNgay = new Guna.UI2.WinForms.Guna2CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.BorderRadius = 5;
            this.dtpTuNgay.Checked = true;
            this.dtpTuNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(292, 17);
            this.dtpTuNgay.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTuNgay.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(124, 28);
            this.dtpTuNgay.TabIndex = 4;
            this.dtpTuNgay.Value = new System.DateTime(2025, 9, 12, 15, 33, 13, 654);
            this.dtpTuNgay.ValueChanged += new System.EventHandler(this.dtpTuNgay_ValueChanged);
            // 
            // cbbTenNhanVien
            // 
            this.cbbTenNhanVien.FormattingEnabled = true;
            this.cbbTenNhanVien.Location = new System.Drawing.Point(42, 17);
            this.cbbTenNhanVien.Name = "cbbTenNhanVien";
            this.cbbTenNhanVien.Size = new System.Drawing.Size(108, 21);
            this.cbbTenNhanVien.TabIndex = 5;
            this.cbbTenNhanVien.Text = "Tên nhân viên";
            this.cbbTenNhanVien.SelectedIndexChanged += new System.EventHandler(this.cbbTenNhanVien_SelectedIndexChanged);
            // 
            // dgvLog
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvLog.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLog.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLog.ColumnHeadersHeight = 15;
            this.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvLog.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLog.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLog.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvLog.Location = new System.Drawing.Point(20, 59);
            this.dgvLog.Name = "dgvLog";
            this.dgvLog.RowHeadersVisible = false;
            this.dgvLog.Size = new System.Drawing.Size(570, 271);
            this.dgvLog.TabIndex = 7;
            this.dgvLog.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvLog.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvLog.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvLog.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvLog.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvLog.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvLog.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvLog.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvLog.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvLog.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvLog.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvLog.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvLog.ThemeStyle.HeaderStyle.Height = 15;
            this.dgvLog.ThemeStyle.ReadOnly = false;
            this.dgvLog.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvLog.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvLog.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvLog.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvLog.ThemeStyle.RowsStyle.Height = 22;
            this.dgvLog.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvLog.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvLog.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLog_CellContentClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsXoa});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 48);
            // 
            // cmsXoa
            // 
            this.cmsXoa.Name = "cmsXoa";
            this.cmsXoa.Size = new System.Drawing.Size(180, 22);
            this.cmsXoa.Text = "Xóa";
            this.cmsXoa.Click += new System.EventHandler(this.cmsXoa_Click_1);
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.BorderRadius = 5;
            this.dtpDenNgay.Checked = true;
            this.dtpDenNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(435, 17);
            this.dtpDenNgay.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpDenNgay.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(124, 28);
            this.dtpDenNgay.TabIndex = 8;
            this.dtpDenNgay.Value = new System.DateTime(2025, 9, 12, 15, 33, 13, 654);
            this.dtpDenNgay.ValueChanged += new System.EventHandler(this.dtpDenNgay_ValueChanged);
            // 
            // chkLocTheoNgay
            // 
            this.chkLocTheoNgay.AutoSize = true;
            this.chkLocTheoNgay.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkLocTheoNgay.CheckedState.BorderRadius = 0;
            this.chkLocTheoNgay.CheckedState.BorderThickness = 0;
            this.chkLocTheoNgay.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkLocTheoNgay.Location = new System.Drawing.Point(565, 24);
            this.chkLocTheoNgay.Name = "chkLocTheoNgay";
            this.chkLocTheoNgay.Size = new System.Drawing.Size(15, 14);
            this.chkLocTheoNgay.TabIndex = 9;
            this.chkLocTheoNgay.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.chkLocTheoNgay.UncheckedState.BorderRadius = 0;
            this.chkLocTheoNgay.UncheckedState.BorderThickness = 0;
            this.chkLocTheoNgay.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.chkLocTheoNgay.CheckedChanged += new System.EventHandler(this.chkLocTheoNgay_CheckedChanged);
            // 
            // UserControlLichSuThayDoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkLocTheoNgay);
            this.Controls.Add(this.dtpDenNgay);
            this.Controls.Add(this.dgvLog);
            this.Controls.Add(this.cbbTenNhanVien);
            this.Controls.Add(this.dtpTuNgay);
            this.Name = "UserControlLichSuThayDoi";
            this.Size = new System.Drawing.Size(612, 358);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpTuNgay;
        private System.Windows.Forms.ComboBox cbbTenNhanVien;
        private Guna.UI2.WinForms.Guna2DataGridView dgvLog;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cmsXoa;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpDenNgay;
        private Guna.UI2.WinForms.Guna2CheckBox chkLocTheoNgay;
    }
}
