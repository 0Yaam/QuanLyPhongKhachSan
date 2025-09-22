using Guna.UI2.WinForms;
using QuanLyPhongKhachSan.BLL.Services;
using QuanLyPhongKhachSan.Common;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyPhongKhachSan.Bar
{
    public partial class UserControlThongKe : UserControl
    {
        private readonly ThongKeService _service = new ThongKeService();

        public UserControlThongKe() => InitializeComponent();

        private void UserControlThongKe_Load(object sender, EventArgs e)
        {
            dtpDenNgay.Value = DateTime.Today;
            dtpTuNgay.Value = DateTime.Today.AddDays(-30);
            dtpTuNgay.ValueChanged += (_s, _e) => RefreshStats();
            dtpDenNgay.ValueChanged += (_s, _e) => RefreshStats();
            SetupChart();
            RefreshStats();
        }

        private void SetupChart()
        {
            chrThongKe.Series.Clear();
            chrThongKe.ChartAreas.Clear();
            chrThongKe.Titles.Clear();

            var area = new ChartArea("area");
            area.AxisX.LabelStyle.Format = "dd/MM";
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            area.AxisY.Title = "Số khách";
            chrThongKe.ChartAreas.Add(area);

            var series = new Series("Số khách theo ngày") { ChartType = SeriesChartType.Column, XValueType = ChartValueType.Date, IsValueShownAsLabel = true };
            chrThongKe.Series.Add(series);
            chrThongKe.Titles.Add("Thống kê khách");
        }

        private void RefreshStats()
        {
            var from = dtpTuNgay.Value.Date;
            var to = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1);
            if (from > to) return;

            int? maNV = CurrentUser.MaNV; // CHỈ theo nhân viên hiện tại

            var (dailyStats, totalCustomers, commission) =
                _service.GetCustomerStatistics(from, to, maNV);

            txtSoLuongKhach.Text = totalCustomers.ToString("#,0");
            txtTongTienThuong.Text = commission.ToString("#,0") + " VNĐ";
            RenderChart(dailyStats, from, to);
        }

        private void RenderChart(DataTable daily, DateTime from, DateTime to)
        {
            var series = chrThongKe.Series[0];
            series.Points.Clear();

            var dict = daily.AsEnumerable()
                .ToDictionary(r => r.Field<DateTime>("Ngay").Date,
                              r => Convert.ToInt32(r["SoKhach"]));

            for (var d = from.Date; d <= to.Date; d = d.AddDays(1))
            {
                int val = dict.TryGetValue(d, out var v) ? v : 0;
                var idx = series.Points.AddXY(d, val);
                if (val == 0) series.Points[idx].IsValueShownAsLabel = false;
            }

            if (chrThongKe.Titles.Count > 0)
                chrThongKe.Titles[0].Text = $"Thống kê khách ({from:dd/MM/yyyy} - {to:dd/MM/yyyy})";
        }

        // Auto-refresh khi in HĐ
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            AppEvents.InvoiceLogged += OnInvoiceLogged;
        }
        protected override void OnHandleDestroyed(EventArgs e)
        {
            AppEvents.InvoiceLogged -= OnInvoiceLogged;
            base.OnHandleDestroyed(e);
        }
        private void OnInvoiceLogged()
        {
            if (!IsHandleCreated) return;
            BeginInvoke((Action)RefreshStats);
        }

        public void RefreshNow() => RefreshStats();
    }
}
