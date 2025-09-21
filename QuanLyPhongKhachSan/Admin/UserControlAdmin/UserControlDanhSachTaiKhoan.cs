using QuanLyPhongKhachSan.Admin;
using QuanLyPhongKhachSan.BLL.Services;
using QuanLyPhongKhachSan.DAL.DAO;
using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhongKhachSan.Login.UserControlAdmin
{
    public partial class UserControlDanhSachTaiKhoan : UserControl
    {
        private readonly NhanSuService nhansu = new NhanSuService();
        private readonly TaiKhoanDAO dao = new TaiKhoanDAO();
        private bool _suppressRowSave = false;
        private bool _userSorted = false;

        public UserControlDanhSachTaiKhoan()
        {
            InitializeComponent();
            this.Load += UserControlDanhSachTaiKhoan_Load;
            dgvDanhSachTaiKhoan.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            dgvDanhSachTaiKhoan.RowValidated += dgvDanhSachTaiKhoan_RowValidated;
            dgvDanhSachTaiKhoan.DataError += (s, e) => { /* tránh vỡ khi nhập sai định dạng ngày...*/ };
            txtTimKiem.PlaceholderText = "Tìm kiếm...";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (var frm = new frmThemNhanVien())
            {
                var dr = frm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    RefreshData();
                }
            }
        }

        private void UserControlDanhSachTaiKhoan_Load(object sender, EventArgs e)
        {
            try
            {
                dgvDanhSachTaiKhoan.AutoGenerateColumns = false;

                foreach (DataGridViewColumn c in dgvDanhSachTaiKhoan.Columns)
                    c.SortMode = DataGridViewColumnSortMode.NotSortable;

                // Gán sự kiện
              
                if (chkLocTheoNgay != null) chkLocTheoNgay.CheckedChanged += chkLocTheoNgay_CheckedChanged;

                // Thiết lập mặc định
                dtpTuNgay.Value = DateTime.Today.AddDays(-7);
                dtpDenNgay.Value = DateTime.Today;
                if (chkLocTheoNgay != null) chkLocTheoNgay.Checked = false;

                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshData()
        {
            try
            {
                var list = nhansu.LayDanhSachNhanVien();

                // Lọc theo ngày vào làm (NgayThamGia)
                if (chkLocTheoNgay != null && chkLocTheoNgay.Checked)
                {
                    DateTime tuNgay = dtpTuNgay.Value.Date;
                    DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1);
                    list = list.Where(x => x.NgayThamGia != DateTime.MinValue &&
                                           x.NgayThamGia >= tuNgay &&
                                           x.NgayThamGia <= denNgay)
                               .ToList();
                }

                // Tìm kiếm
                string tuKhoa = txtTimKiem.Text.Trim();
                if (!string.IsNullOrEmpty(tuKhoa))
                {
                    if (rdSTen.Checked)
                        list = list.Where(x => (x.Ten ?? "").IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                    else if (rdSCCCD.Checked)
                        list = list.Where(x => (x.CCCD ?? "").Contains(tuKhoa)).ToList();
                    else if (rdSSDT.Checked)
                        list = list.Where(x => (x.SDT ?? "").Contains(tuKhoa)).ToList();
                }

                // Sắp xếp
                if (_userSorted)
                {
                    if (rdTen.Checked)
                    {
                        list = (rdTang.Checked
                            ? list.OrderBy(x => x.Ten, StringComparer.CurrentCultureIgnoreCase)
                            : list.OrderByDescending(x => x.Ten, StringComparer.CurrentCultureIgnoreCase)).ToList();
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
                    list = list.OrderByDescending(x => x.NgayThamGia).ToList();
                }

                var view = list.Select(x => new
                {
                    x.MaNV,
                    x.MaTK,
                    x.Ten,
                    x.CCCD,
                    x.SDT,
                    x.TenTaiKhoan,
                    x.MatKhau,
                    x.NgayThamGia
                }).ToList();

                dgvDanhSachTaiKhoan.DataSource = null;
                dgvDanhSachTaiKhoan.DataSource = view;
                dgvDanhSachTaiKhoan.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDanhSachTaiKhoan_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (_suppressRowSave) return;
            if (e.RowIndex < 0) return;
            var row = dgvDanhSachTaiKhoan.Rows[e.RowIndex];
            var item = row.DataBoundItem as NhanVienView;
            if (item == null) return;

            try
            {
                if (item.MaNV <= 0) return;
                item.TenTaiKhoan = (item.TenTaiKhoan ?? "").Trim();
                item.MatKhau = (item.MatKhau ?? "").Trim();

                var ok = nhansu.CapNhatNhanVienVaTaiKhoan(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dòng: " + ex.Message);
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try { RefreshData(); }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rdSTen_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSTen.Checked) RefreshData();
        }

        private void rdSCCCD_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSCCCD.Checked) RefreshData();
        }

        private void rdSSDT_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSSDT.Checked) RefreshData();
        }

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

        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {
            if (chkLocTheoNgay != null && chkLocTheoNgay.Checked) RefreshData();
        }

        private void dtpDenNgay_ValueChanged(object sender, EventArgs e)
        {
            if (chkLocTheoNgay != null && chkLocTheoNgay.Checked) RefreshData();
        }

        private void chkLocTheoNgay_CheckedChanged(object sender, EventArgs e)
        {
            try { RefreshData(); }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lọc theo ngày: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<NhanVienView> GetSelectedItems()
        {
            return dgvDanhSachTaiKhoan.SelectedRows
                .Cast<DataGridViewRow>()
                .Select(r => r.DataBoundItem as NhanVienView)
                .Where(x => x != null)
                .ToList();
        }

        private void cmsXoa_Click(object sender, EventArgs e)
        {
            try
            {
                var items = GetSelectedItems();
                if (items.Count == 0)
                {
                    MessageBox.Show("Hãy chọn ít nhất một nhân viên để xóa.");
                    return;
                }

                var tenList = items.Select(x => x.Ten).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                var msg = (items.Count == 1)
                    ? $"Xóa nhân viên \"{tenList.FirstOrDefault() ?? ("MaNV=" + items.First().MaNV)}\"?"
                    : $"Xóa {items.Count} nhân viên đã chọn?\n{string.Join(", ", tenList)}";

                if (MessageBox.Show(msg, "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                var ids = items.Select(x => x.MaNV).Distinct().ToList();
                var done = nhansu.XoaNhanVienNhieu(ids);
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa: " + ex.Message);
            }
        }

        private void cmsReset_Click(object sender, EventArgs e)
        {
            try
            {
                var items = GetSelectedItems();
                if (items.Count == 0)
                {
                    MessageBox.Show("Hãy chọn ít nhất một dòng để reset mật khẩu.");
                    return;
                }

                var maTKs = items
                    .Where(x => x.MaTK.HasValue && x.MaTK.Value > 0)
                    .Select(x => x.MaTK.Value)
                    .Distinct()
                    .ToList();

                if (maTKs.Count == 0)
                {
                    MessageBox.Show("Các dòng được chọn không có tài khoản để reset.");
                    return;
                }

                if (MessageBox.Show(
                    $"Reset mật khẩu về '123' cho {maTKs.Count} tài khoản?",
                    "Xác nhận reset",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                _suppressRowSave = true;
                var affected = dao.CapNhatMatKhauByIds(maTKs, "123");
                RefreshData();
                MessageBox.Show($"Đã reset {affected} tài khoản về '123'.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi reset: " + ex.Message);
            }
            finally
            {
                _suppressRowSave = false;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            rdCCCD.Checked = false;
            rdSDT.Checked = false;
            rdTen.Checked = false;  
            rdTang.Checked = false;
            rdGiam.Checked = false;
        }
    }
}