using QuanLyPhongKhachSan.BLL.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyPhongKhachSan.Staff.UserControlStaff
{
    public partial class UserControlLichSuHoaDon : UserControl
    {
        private readonly LichSuHoaDonService _svc = new LichSuHoaDonService();

        public UserControlLichSuHoaDon()
        {
            InitializeComponent();
            this.Load += UserControlLichSuHoaDon_Load;

            AppEvents.InvoiceLogged += OnInvoiceLogged;
            this.Disposed += (s, e) => AppEvents.InvoiceLogged -= OnInvoiceLogged;
            txtTimKiem.PlaceholderText = "Tìm kiếm...";
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
                System.Diagnostics.Debug.WriteLine($"RefreshData: Loaded {list?.Count ?? 0} records");

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

                int tenKhIndex = dgvLichSu.Columns.Contains("TenKH") ? dgvLichSu.Columns["TenKH"].Index : -1;
                for (int i = dgvLichSu.Columns.Count - 1; i >= 0; i--)
                {
                    string colName = dgvLichSu.Columns[i].Name;
                    if ((colName == "SoPhong" || colName == "SoPhong1" || colName == "RoomNumber") && i == tenKhIndex + 1)
                    {
                        System.Diagnostics.Debug.WriteLine($"Xóa cột dư thừa sau DataSource: {colName} tại index {i}");
                        dgvLichSu.Columns.RemoveAt(i);
                    }
                }

                System.Diagnostics.Debug.WriteLine("Danh sách cột sau DataSource: " + string.Join(", ", dgvLichSu.Columns.Cast<DataGridViewColumn>().Select(c => $"{c.Name} (Index: {c.Index})")));
                dgvLichSu.Refresh();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error RefreshData: {ex.Message}");
                MessageBox.Show("Lỗi tải lịch sử hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UserControlLichSuHoaDon_Load(object sender, EventArgs e)
        {
            try
            {
                dgvLichSu.AutoGenerateColumns = false;

                if (dgvLichSu.Columns["MaHD"] == null) dgvLichSu.Columns.Add("MaHD", "Mã hóa đơn");
                if (dgvLichSu.Columns["TenKH"] == null) dgvLichSu.Columns.Add("TenKH", "Tên KH");
                if (dgvLichSu.Columns["CCCD"] == null) dgvLichSu.Columns.Add("CCCD", "CCCD");
                if (dgvLichSu.Columns["SDT"] == null) dgvLichSu.Columns.Add("SDT", "SĐT");
                if (dgvLichSu.Columns["LoaiHoaDon"] == null) dgvLichSu.Columns.Add("LoaiHoaDon", "Loại hóa đơn");
                if (dgvLichSu.Columns["SoPhong"] == null) dgvLichSu.Columns.Add("SoPhong", "Số phòng");
                if (dgvLichSu.Columns["ThoiGianIn"] == null) dgvLichSu.Columns.Add("ThoiGianIn", "Thời gian");

                dgvLichSu.Columns["MaHD"].DataPropertyName = "MaHD";
                dgvLichSu.Columns["TenKH"].DataPropertyName = "TenKH";
                dgvLichSu.Columns["CCCD"].DataPropertyName = "CCCD";
                dgvLichSu.Columns["SDT"].DataPropertyName = "SDT";
                dgvLichSu.Columns["LoaiHoaDon"].DataPropertyName = "LoaiHoaDon";
                dgvLichSu.Columns["SoPhong"].DataPropertyName = "SoPhong";
                dgvLichSu.Columns["ThoiGianIn"].DataPropertyName = "ThoiGianIn";

                dgvLichSu.Columns["MaHD"].DisplayIndex = 0;
                dgvLichSu.Columns["TenKH"].DisplayIndex = 1;
                dgvLichSu.Columns["CCCD"].DisplayIndex = 2;
                dgvLichSu.Columns["SDT"].DisplayIndex = 3;
                dgvLichSu.Columns["LoaiHoaDon"].DisplayIndex = 4;
                dgvLichSu.Columns["SoPhong"].DisplayIndex = 5;
                dgvLichSu.Columns["ThoiGianIn"].DisplayIndex = 6;

                System.Diagnostics.Debug.WriteLine("Danh sách cột ban đầu: " + string.Join(", ", dgvLichSu.Columns.Cast<DataGridViewColumn>().Select(c => $"{c.Name} (Index: {c.Index})")));

                int tenKhIndex = dgvLichSu.Columns.Contains("TenKH") ? dgvLichSu.Columns["TenKH"].Index : -1;
                for (int i = dgvLichSu.Columns.Count - 1; i >= 0; i--)
                {
                    string colName = dgvLichSu.Columns[i].Name;
                    if ((colName == "SoPhong" || colName == "SoPhong1" || colName == "RoomNumber") && i == tenKhIndex + 1)
                    {
                        System.Diagnostics.Debug.WriteLine($"Xóa cột dư thừa: {colName} tại index {i}");
                        dgvLichSu.Columns.RemoveAt(i);
                    }
                }

                System.Diagnostics.Debug.WriteLine("Danh sách cột sau xử lý: " + string.Join(", ", dgvLichSu.Columns.Cast<DataGridViewColumn>().Select(c => $"{c.Name} (Index: {c.Index})")));

                rdSoPhong.Checked = true; 
                RefreshData();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi UserControlLichSuHoaDon_Load: {ex.Message}");
                MessageBox.Show("Lỗi tải lịch sử hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string tuKhoa = txtTimKiem.Text.Trim();
                var list = _svc.LayDanhSach();

                if (string.IsNullOrEmpty(tuKhoa))
                {
                    RefreshData();
                    System.Diagnostics.Debug.WriteLine("Tìm kiếm rỗng, load toàn bộ danh sách");
                    return;
                }

                var filteredList = list;
                if (rdSoPhong.Checked)
                {
                    filteredList = list.Where(x => (x.SoPhong.ToString() ?? "").Contains(tuKhoa)).ToList();
                    System.Diagnostics.Debug.WriteLine($"Tìm kiếm theo số phòng: {tuKhoa}, Kết quả: {filteredList.Count} bản ghi");
                }
                else if (rdTen.Checked)
                {
                    filteredList = list.Where(x => (x.TenKH ?? "").IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                    System.Diagnostics.Debug.WriteLine($"Tìm kiếm theo tên: {tuKhoa}, kết quả: {filteredList.Count} bản ghi");
                }
                else if (rdCCCD.Checked)
                {
                    filteredList = list.Where(x => (x.CCCD ?? "").Contains(tuKhoa)).ToList();
                    System.Diagnostics.Debug.WriteLine($"Tìm kiếm theo CCCD: {tuKhoa}, kết quả: {filteredList.Count} bản ghi");
                }
                else if (rdSDT.Checked)
                {
                    filteredList = list.Where(x => (x.SDT ?? "").Contains(tuKhoa)).ToList();
                    System.Diagnostics.Debug.WriteLine($"Tìm kiếm theo SĐT: {tuKhoa}, kết quả: {filteredList.Count} bản ghi");
                }

                var view = filteredList.Select(x => new
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

                // Xóa cột SoPhong dư thừa
                int tenKhIndex = dgvLichSu.Columns.Contains("TenKH") ? dgvLichSu.Columns["TenKH"].Index : -1;
                for (int i = dgvLichSu.Columns.Count - 1; i >= 0; i--)
                {
                    string colName = dgvLichSu.Columns[i].Name;
                    if ((colName == "SoPhong" || colName == "SoPhong1" || colName == "RoomNumber") && i == tenKhIndex + 1)
                    {
                        System.Diagnostics.Debug.WriteLine($"Xóa cột dư thừa sau tìm kiếm: {colName} tại index {i}");
                        dgvLichSu.Columns.RemoveAt(i);
                    }
                }

                dgvLichSu.Refresh();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi txtTimKiem_TextChanged: {ex.Message}");
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rdSoPhong_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSoPhong.Checked)
            {
                txtTimKiem.Clear();
                txtTimKiem_TextChanged(sender, e);
            }
        }

        private void rdTen_CheckedChanged(object sender, EventArgs e)
        {
            if (rdTen.Checked)
            {
                txtTimKiem.Clear();
                txtTimKiem_TextChanged(sender, e);
            }
        }

        private void rdCCCD_CheckedChanged(object sender, EventArgs e)
        {
            if (rdCCCD.Checked)
            {
                txtTimKiem.Clear();
                txtTimKiem_TextChanged(sender, e);
            }
        }

        private void rdSDT_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSDT.Checked)
            {
                txtTimKiem.Clear();
                txtTimKiem_TextChanged(sender, e);
            }
        }
    }
}