using QuanLyPhongKhachSan.BLL.Services;
using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Collections.Generic;


namespace QuanLyPhongKhachSan.Bar
{
    public partial class UserControlDanhSachKhachHang : UserControl
    {
        private readonly KhachHangService _svc = new KhachHangService();

        private bool _userSorted = false;

        public UserControlDanhSachKhachHang()
        {
            InitializeComponent();
            txtTimKiem.PlaceholderText = "Tìm kiếm...";
            this.Load += UserControlDanhSachKhachHang_Load;
        }

        private void UserControlDanhSachKhachHang_Load(object sender, EventArgs e)
        {
            try
            {
                dgvDSKH.AutoGenerateColumns = false;

                var colTen = dgvDSKH.Columns["TenKH"];
                var colCCCD = dgvDSKH.Columns["CCCD"];
                var colSDT = dgvDSKH.Columns["SDT"];
                var colNgay = dgvDSKH.Columns["NgayThamGia"];

                if (colTen != null) colTen.DataPropertyName = "HoTen";
                if (colCCCD != null) colCCCD.DataPropertyName = "CCCD";
                if (colSDT != null) colSDT.DataPropertyName = "SDT";
                if (colNgay != null)
                {
                    colNgay.DataPropertyName = "NgayThamGia";
                    colNgay.DefaultCellStyle.Format = "dd/MM/yyyy";
                    colNgay.DefaultCellStyle.NullValue = "";
                }

                foreach (DataGridViewColumn c in dgvDSKH.Columns)
                    c.SortMode = DataGridViewColumnSortMode.NotSortable;


                dtpTuNgay.Value = DateTime.Today.AddDays(-7);
                dtpDenNgay.Value = DateTime.Today;
                chkLocTheoNgay.Checked = false; 

                dtpTuNgay.ValueChanged += dtpTuNgay_ValueChanged;
                dtpDenNgay.ValueChanged += dtpDenNgay_ValueChanged;
                chkLocTheoNgay.CheckedChanged += chkLocTheoNgay_CheckedChanged;
                RefreshData();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi UserControlDanhSachKhachHang_Load: {ex.Message}");
                MessageBox.Show("Lỗi tải danh sách khách hàng: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1); 
                    list = list
                        .Where(x => x.NgayThamGia.HasValue &&
                                    x.NgayThamGia.Value >= tuNgay &&
                                    x.NgayThamGia.Value <= denNgay)
                        .ToList();
                }

                string tuKhoa = txtTimKiem.Text.Trim();
                if (!string.IsNullOrEmpty(tuKhoa))
                {
                    if (rdSTen.Checked)
                        list = list.Where(x => (x.HoTen ?? "").IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                    else if (rdSCCCD.Checked)
                        list = list.Where(x => (x.CCCD ?? "").Contains(tuKhoa)).ToList();
                    else if (rdSSDT.Checked)
                        list = list.Where(x => (x.SDT ?? "").Contains(tuKhoa)).ToList();
                }

                if (_userSorted)
                {
                    if (rdTen.Checked)
                    {
                        list = (rdTang.Checked
                            ? list.OrderBy(x => x.HoTen, StringComparer.CurrentCultureIgnoreCase)
                            : list.OrderByDescending(x => x.HoTen, StringComparer.CurrentCultureIgnoreCase)).ToList();
                    }
                    else if (rdCCCD.Checked)
                    {
                        list = (rdTang.Checked ? list.OrderBy(x => x.CCCD) : list.OrderByDescending(x => x.CCCD)).ToList();
                    }
                    else if (rdSDT.Checked)
                    {
                        list = (rdTang.Checked ? list.OrderBy(x => x.SDT) : list.OrderByDescending(x => x.SDT)).ToList();
                    }
                }
                else
                {
                    // MẶC ĐỊNH: SẮP XẾP THEO NGÀY THAM GIA GIẢM DẦN (mới nhất trước)
                    list = list
                        .OrderByDescending(x => x.NgayThamGia ?? DateTime.MinValue)
                        .ToList();
                }

                var view = list.Select(x => new
                {
                    HoTen = x.HoTen,
                    CCCD = x.CCCD,
                    SDT = x.SDT,
                    NgayThamGia = x.NgayThamGia
                }).ToList();

                dgvDSKH.DataSource = null;
                dgvDSKH.DataSource = view;
                dgvDSKH.Refresh();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error RefreshData: {ex.Message}");
                MessageBox.Show("Lỗi tải danh sách khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // === SEARCH ===
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try { RefreshData(); }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi txtTimKiem_TextChanged: {ex.Message}");
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void rdSTen_CheckedChanged(object sender, EventArgs e) { if (rdSTen.Checked) RefreshData(); }
        private void rdSCCCD_CheckedChanged(object sender, EventArgs e) { if (rdSCCCD.Checked) RefreshData(); }
        private void rdSSDT_CheckedChanged(object sender, EventArgs e) { if (rdSSDT.Checked) RefreshData(); }

        // === SORT (ghi đè mặc định) ===
        private void rdTen_CheckedChanged(object sender, EventArgs e)
        {
            if (rdTen.Checked) { _userSorted = true; RefreshData(); }
        }
        private void rdCCCD_CheckedChanged(object sender, EventArgs e)
        {
            if (rdCCCD.Checked) { _userSorted = true; RefreshData(); }
        }
        private void rdSDT_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSDT.Checked) { _userSorted = true; RefreshData(); }
        }
        private void rdTang_CheckedChanged(object sender, EventArgs e)
        {
            if (rdTang.Checked && _userSorted) RefreshData();
        }
        private void rdGiam_CheckedChanged(object sender, EventArgs e)
        {
            if (rdGiam.Checked && _userSorted) RefreshData();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            _userSorted = false;
            rdTen.Checked = rdCCCD.Checked = rdSDT.Checked = false;
            // rdTang/rdGiam không quan trọng khi _userSorted=false
            RefreshData();
        }

        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {
            if (chkLocTheoNgay.Checked) RefreshData();
        }
        private void dtpDenNgay_ValueChanged(object sender, EventArgs e)
        {
            if (chkLocTheoNgay.Checked) RefreshData();
        }
        private void chkLocTheoNgay_CheckedChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void btnChonTatCa_Click(object sender, EventArgs e)
        {
            dgvDSKH.SelectAll();
        }

        private void btnReset_Click_1(object sender, EventArgs e)
        {
            rdSDT.Checked = false;
            rdCCCD.Checked = false;
            rdTen.Checked = false;
            rdGiam.Checked = false;
            rdTang.Checked = false;
            RefreshData();
        }
        private List<(string HoTen, string CCCD, string SDT, string NgayThamGia)> GetSelectedRowsForExport()
        {
            var rows = new List<(string, string, string, string)>();

            var source = dgvDSKH.SelectedRows.Count > 0 ? dgvDSKH.SelectedRows.Cast<DataGridViewRow>()
                                                        : dgvDSKH.Rows.Cast<DataGridViewRow>();

            var ordered = source.OrderBy(r => r.Index);

            foreach (var r in ordered)
            {
                if (r.IsNewRow) continue;

                string hoTen = Convert.ToString(r.Cells["HoTen"].Value ?? r.Cells.Cast<DataGridViewCell>()
                                             .FirstOrDefault(c => (c.OwningColumn?.DataPropertyName ?? "") == "HoTen")?.Value ?? "");
                string cccd = Convert.ToString(r.Cells["CCCD"].Value ?? "");
                string sdt = Convert.ToString(r.Cells["SDT"].Value ?? "");
                string ngay = "";
                var cellNgay = r.Cells["NgayThamGia"];
                if (cellNgay != null && cellNgay.Value != null)
                {
                    if (cellNgay.Value is DateTime dt) ngay = dt.ToString("dd/MM/yyyy");
                    else ngay = cellNgay.Value.ToString();
                }

                rows.Add((hoTen, cccd, sdt, ngay));
            }

            return rows;
        }
        private void WriteCsv(string path, List<(string HoTen, string CCCD, string SDT, string NgayThamGia)> rows)
        {
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            using (var writer = new StreamWriter(fs, new UTF8Encoding(encoderShouldEmitUTF8Identifier: true)))
            {
                // Header
                writer.WriteLine("Họ tên,CCCD,SĐT,Ngày tham gia");

                foreach (var row in rows)
                {
                    string Esc(string s)
                    {
                        if (s == null) return "";
                        bool needQuote = s.Contains(",") || s.Contains("\n") || s.Contains("\r") || s.Contains("\"");
                        s = s.Replace("\"", "\"\"");
                        return needQuote ? $"\"{s}\"" : s;
                    }

                    writer.WriteLine($"{Esc(row.HoTen)},{Esc(row.CCCD)},{Esc(row.SDT)},{Esc(row.NgayThamGia)}");
                }
            }
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            try
            {
                var rows = GetSelectedRowsForExport();
                if (rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var sfd = new SaveFileDialog()
                {
                    Title = "Xuất CSV",
                    Filter = "CSV file (*.csv)|*.csv",
                    FileName = "KhachHang.csv",
                    OverwritePrompt = true
                })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        WriteCsv(sfd.FileName, rows);
                        MessageBox.Show("Xuất CSV thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất CSV: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
