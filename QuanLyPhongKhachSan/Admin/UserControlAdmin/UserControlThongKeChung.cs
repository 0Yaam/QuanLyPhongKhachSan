using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using QuanLyPhongKhachSan.BLL.Services;

namespace QuanLyPhongKhachSan.Login.UserControlAdmin
{
    public partial class UserControlThongKeChung : UserControl
    {
        private readonly ThongKeNhanVienService _empSvc = new ThongKeNhanVienService();

        public UserControlThongKeChung()
        {
            InitializeComponent();
            // Bảo đảm load được gọi
            this.Load += UserControlThongKeChung_Load;
        }

        private void UserControlThongKeChung_Load(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra control tồn tại để tránh NullReference nếu tên khác trong Designer
                if (dtpDenNgay != null) dtpDenNgay.Value = DateTime.Today;
                if (dtpTuNgay != null) dtpTuNgay.Value = DateTime.Today.AddDays(-30);

                LoadNhanVien();
                HookEvents();
                SetupChartIfNeeded();
                RefreshStats();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo thống kê: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HookEvents()
        {
            if (dtpTuNgay != null) dtpTuNgay.ValueChanged += (_s, _e) => RefreshStats();
            if (dtpDenNgay != null) dtpDenNgay.ValueChanged += (_s, _e) => RefreshStats();
            if (cbbNhanVien != null) cbbNhanVien.SelectedIndexChanged += (_s, _e) => RefreshStats();
        }

        /// <summary>
        /// Nạp combobox nhân viên (thêm dòng "Tất cả nhân viên")
        /// </summary>
        private void LoadNhanVien()
        {
            if (cbbNhanVien == null) return;

            var dt = _empSvc.LayDanhSachNhanVien();
            if (dt == null) return;

            var row = dt.NewRow();
            row["MaNV"] = 0;
            row["TenHienThi"] = "Tất cả nhân viên";
            dt.Rows.InsertAt(row, 0);

            cbbNhanVien.DataSource = dt;
            cbbNhanVien.DisplayMember = "TenHienThi";
            cbbNhanVien.ValueMember = "MaNV";
        }

        /// <summary>
        /// Thiết lập biểu đồ 1 lần (nếu chưa có)
        /// </summary>
        private void SetupChartIfNeeded()
        {
            if (chrThongKe == null) return;

            chrThongKe.Series.Clear();
            chrThongKe.ChartAreas.Clear();
            chrThongKe.Titles.Clear();

            var area = new ChartArea("area");
            area.AxisX.LabelStyle.Format = "dd/MM";
            area.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            area.AxisY.Title = "Số khách";
            chrThongKe.ChartAreas.Add(area);

            var series = new Series("Số khách theo ngày")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.Date,
                IsValueShownAsLabel = true
            };
            chrThongKe.Series.Add(series);

            chrThongKe.Titles.Add("Thống kê");
        }

        /// <summary>
        /// Tải dữ liệu và đổ lên UI
        /// </summary>
        private void RefreshStats()
        {
            if (dtpTuNgay == null || dtpDenNgay == null) return;

            var from = dtpTuNgay.Value.Date;
            var to = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1);
            if (from > to)
            {
                MessageBox.Show("Từ ngày phải ≤ Đến ngày.", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? maNV = null;
            if (cbbNhanVien != null && cbbNhanVien.SelectedValue is int v && v > 0)
                maNV = v;

            try
            {
                var (daily, total, revenue, commission) = _empSvc.GetStats(from, to, maNV);

                if (txtSoLuongKhach != null)
                    txtSoLuongKhach.Text = total.ToString("#,0");

                if (txtDoanhThu != null)
                    txtDoanhThu.Text = revenue.ToString("#,0") + " VNĐ";

                RenderChart(daily, from, to);

                if (chrThongKe != null && chrThongKe.Titles.Count > 0)
                {
                    string who = (cbbNhanVien != null ? cbbNhanVien.Text : "Tất cả");
                    chrThongKe.Titles[0].Text = $"Thống kê: {who} ({from:dd/MM/yyyy} - {to:dd/MM/yyyy})";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thống kê: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Vẽ biểu đồ từ DataTable daily (cột Ngay (DateTime), SoKhach (int))
        /// </summary>
        private void RenderChart(DataTable daily, DateTime from, DateTime to)
        {
            if (chrThongKe == null || chrThongKe.Series.Count == 0) return;

            var series = chrThongKe.Series[0];
            series.Points.Clear();

            var dict = daily.AsEnumerable()
                .ToDictionary(
                    r => r.Field<DateTime>("Ngay").Date,
                    r => Convert.ToInt32(r["SoKhach"])
                );

            for (var d = from.Date; d <= to.Date; d = d.AddDays(1))
            {
                int val = dict.TryGetValue(d, out var v) ? v : 0;
                var idx = series.Points.AddXY(d, val);
                if (val == 0) series.Points[idx].IsValueShownAsLabel = false;
            }
        }

        // ==== Các handler trống sẵn của bạn, giữ nguyên nếu Designer đã gắn ====
        private void cbbNhanVien_SelectedIndexChanged(object sender, EventArgs e) { }
        private void dtpTuNgay_ValueChanged(object sender, EventArgs e) { }
        private void chart1_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e) { }
        private void txtSoLuongKhach_TextChanged(object sender, EventArgs e) { }
        private void txtTongTienThuong_TextChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
    }
}
