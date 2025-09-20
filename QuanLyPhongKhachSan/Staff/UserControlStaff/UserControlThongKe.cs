using Guna.UI2.WinForms;
using QuanLyPhongKhachSan.BLL.Services;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyPhongKhachSan.Bar
{
    public partial class UserControlThongKe : UserControl
    {
        private readonly ThongKeService _service = new ThongKeService();

        public UserControlThongKe()
        {
            InitializeComponent();
        }

        private void UserControlThongKe_Load(object sender, EventArgs e)
        {
            try
            {
                dtpDenNgay.Value = DateTime.Today;
                dtpTuNgay.Value = DateTime.Today.AddDays(-30);
                dtpTuNgay.ValueChanged += (_s, _e) => RefreshStats();
                dtpDenNgay.ValueChanged += (_s, _e) => RefreshStats();
                SetupChart();
                RefreshStats();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi UserControlThongKe_Load: {ex.Message}");
                MessageBox.Show($"Lỗi khi tải thống kê: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupChart()
        {
            try
            {
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
                var series = new Series("Số khách theo ngày");
                series.ChartType = SeriesChartType.Column;
                series.XValueType = ChartValueType.Date;
                series.IsValueShownAsLabel = true;
                chrThongKe.Series.Add(series);

                chrThongKe.Titles.Add("Thống kê khách 30 ngày gần nhất");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi SetupChart: {ex.Message}");
                MessageBox.Show($"Lỗi khi thiết lập biểu đồ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshStats()
        {
            var from = dtpTuNgay.Value.Date;
            var to = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1);

            if (from > to)
            {
                MessageBox.Show("Từ ngày phải nhỏ hơn hoặc bằng Đến ngày.", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var (dailyStats, totalCustomers, commission) = _service.GetCustomerStatistics(from, to);
                txtSoLuongKhach.Text = totalCustomers.ToString("#,0");
                txtTongTienThuong.Text = commission.ToString("#,0") + " VNĐ";
                RenderChart(dailyStats, from, to);
                System.Diagnostics.Debug.WriteLine($"Thống kê: {totalCustomers} khách, Hoa hồng: {commission:N0} VNĐ, Từ ngày: {from:dd/MM/yyyy}, Đến ngày: {to:dd/MM/yyyy}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi RefreshStats: {ex.Message}");
                MessageBox.Show($"Lỗi khi tải thống kê: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RenderChart(DataTable daily, DateTime from, DateTime to)
        {
            try
            {
                var series = chrThongKe.Series[0];
                series.Points.Clear();

                var dict = daily.AsEnumerable()
                    .ToDictionary(
                        r => r.Field<DateTime>("Ngay").Date,
                        r => Convert.ToInt32(r["SoKhach"])
                    );

                for (var d = from; d <= to; d = d.AddDays(1))
                {
                    int val = dict.TryGetValue(d, out var v) ? v : 0;
                    var p = series.Points.AddXY(d, val);
                    if (val == 0) series.Points[p].IsValueShownAsLabel = false;
                }

                if (chrThongKe.Titles.Count > 0)
                    chrThongKe.Titles[0].Text = $"Thống kê khách ({from:dd/MM/yyyy} - {to:dd/MM/yyyy})";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi RenderChart: {ex.Message}");
                MessageBox.Show($"Lỗi khi vẽ biểu đồ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            dtpDenNgay.Value = DateTime.Today;
            dtpTuNgay.Value = DateTime.Today.AddDays(-30);
            RefreshStats();
        }

        private void chrThongKe_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Nút Thống Kê được nhấn");
            RefreshStats();
        }
    }
}