using System;
using System.Linq;
using System.Windows.Forms;
using QuanLyPhongKhachSan.BLL.Services;

namespace QuanLyPhongKhachSan.Staff.UserControlStaff
{
    public partial class UserControlLichSuHoaDon : UserControl
    {
        private readonly LichSuHoaDonService _svc = new LichSuHoaDonService();

        public UserControlLichSuHoaDon()
        {
            InitializeComponent();
            this.Load += UserControlLichSuHoaDon_Load;

            // Đăng ký event global
            AppEvents.InvoiceLogged += OnInvoiceLogged;

            // Hủy đăng ký khi control dispose (tránh leak)
            this.Disposed += (s, e) => AppEvents.InvoiceLogged -= OnInvoiceLogged;
        }

        private void OnInvoiceLogged()
        {
            if (IsHandleCreated && InvokeRequired)
                BeginInvoke(new Action(RefreshData));
            else
                RefreshData();
        }

        private void UserControlLichSuHoaDon_Load(object sender, EventArgs e)
        {
            dgvLichSu.AutoGenerateColumns = false;

            // Map cột -> property đúng với object bạn bind
            dgvLichSu.Columns["TenKhachHang"].DataPropertyName = "TenKH";
            dgvLichSu.Columns["CCCD"].DataPropertyName = "CCCD";
            dgvLichSu.Columns["SDT"].DataPropertyName = "SDT";
            dgvLichSu.Columns["ThoiGianIn"].DataPropertyName = "ThoiGianIn";
            dgvLichSu.Columns["LoaiHoaDon"].DataPropertyName = "LoaiHoaDon";

            RefreshData();
        }

        public void RefreshData()
        {
            try
            {
                var list = _svc.LayDanhSach();
                var view = list.Select(x => new
                {
                    x.TenKH,
                    x.CCCD,
                    x.SDT,
                    ThoiGianIn = x.ThoiGianIn.ToString("dd/MM/yyyy HH:mm"),
                    x.LoaiHoaDon
                }).ToList();

                dgvLichSu.DataSource = null;
                dgvLichSu.DataSource = view;
                dgvLichSu.Refresh();
            }
            catch (Exception ex)
            {
                // Dùng ex để khỏi warning "declared but never used"
                System.Diagnostics.Debug.WriteLine("RefreshData error: " + ex);
                MessageBox.Show("Lỗi tải lịch sử hóa đơn: " + ex.Message);
            }
        }
    }
}
