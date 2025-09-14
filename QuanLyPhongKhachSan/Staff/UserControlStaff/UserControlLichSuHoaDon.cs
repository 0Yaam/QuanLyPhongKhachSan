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
            System.Diagnostics.Debug.WriteLine("OnInvoiceLogged triggered at " + DateTime.Now);
            if (IsHandleCreated && InvokeRequired)
                BeginInvoke(new Action(RefreshData));
            else
                RefreshData();
        }

        public void RefreshData()
        {
            try
            {
                var list = _svc.LayDanhSach();
                System.Diagnostics.Debug.WriteLine($"RefreshData: Loaded {list.Count} records");
                var view = list.Select(x => new
                {
                    MaHD = x.MaHD,
                    TenKH = x.TenKH,
                    CCCD = x.CCCD,
                    SDT = x.SDT,
                    LoaiHoaDon = x.LoaiHoaDon,
                    SoPhong = x.SoPhong,
                    ThoiGianIn = x.ThoiGianIn.ToString("dd/MM/yyyy HH:mm")
                }).ToList();

                dgvLichSu.DataSource = null;
                dgvLichSu.DataSource = view;
                dgvLichSu.Refresh();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error RefreshData: {ex.Message}");
                MessageBox.Show("Lỗi tải lịch sử hóa đơn: " + ex.Message);
            }
        }

        private void UserControlLichSuHoaDon_Load(object sender, EventArgs e)
        {
            dgvLichSu.AutoGenerateColumns = false;

            // Đảm bảo các cột đã được thêm trong Designer
            if (dgvLichSu.Columns["MaHD"] == null) dgvLichSu.Columns.Add("MaHD", "Mã HĐ");
            if (dgvLichSu.Columns["TenKH"] == null) dgvLichSu.Columns.Add("TenKH", "Tên KH");
            if (dgvLichSu.Columns["CCCD"] == null) dgvLichSu.Columns.Add("CCCD", "CCCD");
            if (dgvLichSu.Columns["SDT"] == null) dgvLichSu.Columns.Add("SDT", "SĐT");
            if (dgvLichSu.Columns["LoaiHoaDon"] == null) dgvLichSu.Columns.Add("LoaiHoaDon", "Loại HĐ");
            if (dgvLichSu.Columns["SoPhong"] == null) dgvLichSu.Columns.Add("SoPhong", "Số Phòng");
            if (dgvLichSu.Columns["ThoiGianIn"] == null) dgvLichSu.Columns.Add("ThoiGianIn", "Thời gian");

            // Map các cột
            dgvLichSu.Columns["MaHD"].DataPropertyName = "MaHD";
            dgvLichSu.Columns["TenKH"].DataPropertyName = "TenKH";
            dgvLichSu.Columns["CCCD"].DataPropertyName = "CCCD";
            dgvLichSu.Columns["SDT"].DataPropertyName = "SDT";
            dgvLichSu.Columns["LoaiHoaDon"].DataPropertyName = "LoaiHoaDon";
            dgvLichSu.Columns["SoPhong"].DataPropertyName = "SoPhong";
            dgvLichSu.Columns["ThoiGianIn"].DataPropertyName = "ThoiGianIn";

            // Điều chỉnh thứ tự cột (tùy chỉnh theo ý muốn)
            dgvLichSu.Columns["MaHD"].DisplayIndex = 0;
            dgvLichSu.Columns["TenKH"].DisplayIndex = 1;
            dgvLichSu.Columns["CCCD"].DisplayIndex = 2;
            dgvLichSu.Columns["SDT"].DisplayIndex = 3;
            dgvLichSu.Columns["LoaiHoaDon"].DisplayIndex = 4;
            dgvLichSu.Columns["SoPhong"].DisplayIndex = 5;
            dgvLichSu.Columns["ThoiGianIn"].DisplayIndex = 6;

            RefreshData();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
        }

        private void rdSoPhong_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void rdSDT_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void rdCCCD_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void rdTen_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}