using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyPhongKhachSan.Bar
{
    public partial class UserControlThongKe : UserControl
    {
        private readonly string _connStr =
            @"Data Source=DESKTOP-LIASEBH;Initial Catalog=QuanLyPhongKhachSan;Integrated Security=True";

        public UserControlThongKe()
        {
            InitializeComponent();
        }

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

        private void RefreshStats()
        {
            // Chuẩn hoá khoảng ngày: từ 00:00 của Từ Ngày đến 23:59:59.999 của Đến Ngày
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
                using (var con = new SqlConnection(_connStr))
                {
                    con.Open();

                    var cmdDaily = new SqlCommand(@"
                        SELECT CAST(NgayThamGia AS date) AS Ngay, COUNT(*) AS SoKhach
                        FROM   KhachHang
                        WHERE  NgayThamGia IS NOT NULL
                           AND NgayThamGia BETWEEN @from AND @to
                        GROUP BY CAST(NgayThamGia AS date)
                        ORDER BY Ngay;", con);
                    cmdDaily.Parameters.AddWithValue("@from", from);
                    cmdDaily.Parameters.AddWithValue("@to", to);

                    var daily = new DataTable();
                    using (var ad = new SqlDataAdapter(cmdDaily)) ad.Fill(daily);

                    int tongTrongKhoang = 0;
                    foreach (DataRow dr in daily.Rows) tongTrongKhoang += Convert.ToInt32(dr["SoKhach"]);
                    txtSoLuongKhach.Text = tongTrongKhoang.ToString("#,0");

                    var now = DateTime.Today;
                    var firstDay = new DateTime(now.Year, now.Month, 1);
                    var lastDay = firstDay.AddMonths(1).AddTicks(-1);

                    var cmdMonth = new SqlCommand(@"
                        SELECT COUNT(*) 
                        FROM KhachHang
                        WHERE NgayThamGia IS NOT NULL
                          AND NgayThamGia BETWEEN @mfrom AND @mto;", con);
                    cmdMonth.Parameters.AddWithValue("@mfrom", firstDay);
                    cmdMonth.Parameters.AddWithValue("@mto", lastDay);

                    int countInMonth = Convert.ToInt32(cmdMonth.ExecuteScalar());
                    int threshold = 50;
                    int over = Math.Max(0, countInMonth - threshold);
                    decimal commission = over * 5000m;
                    txtTongTienThuong.Text = commission.ToString("#,0");


                    RenderChart(daily, from.Date, to.Date);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thống kê: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RenderChart(DataTable daily, DateTime from, DateTime to)
        {
            var series = chrThongKe.Series[0];
            series.Points.Clear();

            // Tạo dictionary ngày→số khách từ DataTable
            var dict = daily.AsEnumerable()
                            .ToDictionary(
                                r => r.Field<DateTime>("Ngay").Date,
                                r => Convert.ToInt32(r["SoKhach"])
                            );

            for (var d = from; d <= to; d = d.AddDays(1))
            {
                int val = dict.TryGetValue(d, out var v) ? v : 0;
                var p = series.Points.AddXY(d, val);
                // Hiển thị nhãn chỉ khi >0 cho gọn
                if (val == 0) series.Points[p].IsValueShownAsLabel = false;
            }

            if (chrThongKe.Titles.Count > 0)
                chrThongKe.Titles[0].Text = $"Thống kê khách ({from:dd/MM/yyyy} - {to:dd/MM/yyyy})";
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            // “Từ ngày đến ngày, cứ trừ ra 30 ngày tính từ hiện tại”
            dtpDenNgay.Value = DateTime.Today;
            dtpTuNgay.Value = DateTime.Today.AddDays(-30);
            RefreshStats();
        }
    }
}
