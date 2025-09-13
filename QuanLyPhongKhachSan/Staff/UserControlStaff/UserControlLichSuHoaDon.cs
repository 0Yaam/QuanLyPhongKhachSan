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
                    x.TenKH,
                    x.CCCD,
                    x.SDT,
                    x.SoPhong,
                    ThoiGianIn = x.ThoiGianIn.ToString("dd/MM/yyyy HH:mm"),
                    x.LoaiHoaDon
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

            // nếu bạn đã kéo thả 5 cột trước đó: TenKhachHang, CCCD, SDT, ThoiGianIn, LoaiHoaDon
            // -> thêm 1 cột SoPhong vào vị trí sau SDT:
            if (!dgvLichSu.Columns.Contains("SoPhong"))
            {
                var col = new DataGridViewTextBoxColumn
                {
                    Name = "SoPhong",
                    HeaderText = "Số phòng",
                    DataPropertyName = "SoPhong",
                    Width = 80
                };
                // chèn sau SDT
                int idx = dgvLichSu.Columns.Contains("SDT") ? dgvLichSu.Columns["SDT"].Index + 1 : dgvLichSu.Columns.Count;
                dgvLichSu.Columns.Insert(idx, col);
            }

            // map các cột sẵn có
            dgvLichSu.Columns["TenKhachHang"].DataPropertyName = "TenKH";
            dgvLichSu.Columns["CCCD"].DataPropertyName = "CCCD";
            dgvLichSu.Columns["SDT"].DataPropertyName = "SDT";
            dgvLichSu.Columns["SoPhong"].DataPropertyName = "SoPhong";     // NEW
            dgvLichSu.Columns["ThoiGianIn"].DataPropertyName = "ThoiGianIn";
            dgvLichSu.Columns["LoaiHoaDon"].DataPropertyName = "LoaiHoaDon";

            RefreshData();
        }

     

    }
}
