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

                if (chkLocTheoNgay.Checked)
                {
                    DateTime tuNgay = dtpTuNgay.Value.Date;
                    DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1); // Bao gồm cả ngày cuối
                    list = list.Where(x => x.ThoiGianIn >= tuNgay && x.ThoiGianIn <= denNgay).ToList();
                    System.Diagnostics.Debug.WriteLine($"Lọc theo ngày từ {tuNgay:dd/MM/yyyy} đến {denNgay:dd/MM/yyyy}: {list.Count} bản ghi");
                }

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

                // Xóa cột SoPhong dư thừa (nếu có)
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

                // Đảm bảo các cột đã được thêm trong Designer
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

                dtpTuNgay.Value = DateTime.Today.AddDays(-7); // Mặc định 7 ngày trước
                dtpDenNgay.Value = DateTime.Today; // Mặc định hôm nay
                chkLocTheoNgay.Checked = false; // Mặc định không lọc theo ngày
                rdSoPhong.Checked = true; // Mặc định tìm kiếm theo số phòng

                dtpTuNgay.ValueChanged += dtpTuNgay_ValueChanged;
                dtpDenNgay.ValueChanged += dtpDenNgay_ValueChanged;
                chkLocTheoNgay.CheckedChanged += chkLocTheoNgay_CheckedChanged;

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
                System.Diagnostics.Debug.WriteLine($"Tìm kiếm với từ khóa: {tuKhoa}, Tổng số bản ghi: {list?.Count ?? 0}");

                if (chkLocTheoNgay.Checked)
                {
                    DateTime tuNgay = dtpTuNgay.Value.Date;
                    DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1); // Bao gồm cả ngày cuối
                    list = list.Where(x => x.ThoiGianIn >= tuNgay && x.ThoiGianIn <= denNgay).ToList();
                    System.Diagnostics.Debug.WriteLine($"Lọc theo ngày từ {tuNgay:dd/MM/yyyy} đến {denNgay:dd/MM/yyyy}: {list.Count} bản ghi");
                }

                var filteredList = list;
                if (!string.IsNullOrEmpty(tuKhoa))
                {
                    if (rdSoPhong.Checked)
                    {
                        filteredList = list.Where(x => (x.SoPhong.ToString() ?? "").Contains(tuKhoa)).ToList();
                        System.Diagnostics.Debug.WriteLine($"Tìm kiếm theo số phòng: {tuKhoa}, Kết quả: {filteredList.Count} bản ghi");
                    }
                    else if (rdTen.Checked)
                    {
                        filteredList = list.Where(x => (x.TenKH ?? "").IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                        System.Diagnostics.Debug.WriteLine($"Tìm kiếm theo tên: {tuKhoa}, Kết quả: {filteredList.Count} bản ghi");
                    }
                    else if (rdCCCD.Checked)
                    {
                        filteredList = list.Where(x => (x.CCCD ?? "").Contains(tuKhoa)).ToList();
                        System.Diagnostics.Debug.WriteLine($"Tìm kiếm theo CCCD: {tuKhoa}, Kết quả: {filteredList.Count} bản ghi");
                    }
                    else if (rdSDT.Checked)
                    {
                        filteredList = list.Where(x => (x.SDT ?? "").Contains(tuKhoa)).ToList();
                        System.Diagnostics.Debug.WriteLine($"Tìm kiếm theo SĐT: {tuKhoa}, Kết quả: {filteredList.Count} bản ghi");
                    }
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

                System.Diagnostics.Debug.WriteLine("Danh sách cột sau tìm kiếm: " + string.Join(", ", dgvLichSu.Columns.Cast<DataGridViewColumn>().Select(c => $"{c.Name} (Index: {c.Index})")));
                dgvLichSu.Refresh();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi txtTimKiem_TextChanged: {ex.Message}");
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {
            if (chkLocTheoNgay.Checked)
            {
                System.Diagnostics.Debug.WriteLine($"dtpTuNgay thay đổi: {dtpTuNgay.Value:dd/MM/yyyy}");
                txtTimKiem_TextChanged(sender, e);
            }
        }

        private void dtpDenNgay_ValueChanged(object sender, EventArgs e)
        {
            if (chkLocTheoNgay.Checked)
            {
                System.Diagnostics.Debug.WriteLine($"dtpDenNgay thay đổi: {dtpDenNgay.Value:dd/MM/yyyy}");
                txtTimKiem_TextChanged(sender, e);
            }
        }

        private void chkLocTheoNgay_CheckedChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"chkLocTheoNgay thay đổi: {chkLocTheoNgay.Checked}");
            txtTimKiem_TextChanged(sender, e);
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