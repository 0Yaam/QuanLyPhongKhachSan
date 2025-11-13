using QuanLyPhongKhachSan.Admin;
using QuanLyPhongKhachSan.BLL.Services;
using QuanLyPhongKhachSan.DAL.DAO;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyPhongKhachSan.Login.UserControlAdmin
{
    // Class để lưu trữ dữ liệu cho ComboBox
   

    public partial class UserControlLichSuThayDoi : UserControl
    {
        private readonly NhatKyService _logSvc = new NhatKyService();
        private readonly NhanVienDAO _nvDao = new NhanVienDAO();

        public UserControlLichSuThayDoi()
        {
            InitializeComponent();
            this.Load += UserControlNhatKy_Load;
        }

        private void UserControlNhatKy_Load(object sender, EventArgs e)
        {
            if (dgvLog == null)
            {
                MessageBox.Show("Không tìm thấy DataGridView có tên 'dgvLog'. Hãy mở Designer và đổi Name thành 'dgvLog'.",
                                "Thiếu control", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // Thiết lập grid
            dgvLog.AutoGenerateColumns = false;
            dgvLog.ReadOnly = true;
            dgvLog.AllowUserToAddRows = false;
            dgvLog.AllowUserToDeleteRows = false;
            dgvLog.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLog.MultiSelect = false;

            // Clear cột cũ để tránh trùng/ẩn
            dgvLog.Columns.Clear();

            // Cột ẩn Id để mở chi tiết
            var colId = new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Visible = false
            };
            dgvLog.Columns.Add(colId);

            // Cột Tên nhân viên
            var colTenNV = new DataGridViewTextBoxColumn
            {
                Name = "TenNV",
                HeaderText = "Tên nhân viên",
                DataPropertyName = "TenNV",
                FillWeight = 35
            };
            dgvLog.Columns.Add(colTenNV);

            // Cột Loại thay đổi
            var colLoai = new DataGridViewTextBoxColumn
            {
                Name = "Loai",
                HeaderText = "Loại thay đổi",
                DataPropertyName = "Loai",
                FillWeight = 35
            };
            dgvLog.Columns.Add(colLoai);

            // Cột Thời gian
            var colThoiGian = new DataGridViewTextBoxColumn
            {
                Name = "ThoiGian",
                HeaderText = "Thời gian",
                DataPropertyName = "ThoiGian",
                FillWeight = 30
            };
            dgvLog.Columns.Add(colThoiGian);

            // Đăng ký sự kiện
            dgvLog.CellDoubleClick -= dgvLog_CellDoubleClick;
            dgvLog.CellDoubleClick += dgvLog_CellDoubleClick;
            cbbTenNhanVien.SelectedIndexChanged += cbbTenNhanVien_SelectedIndexChanged;
            dtpTuNgay.ValueChanged += dtpTuNgay_ValueChanged;
            dtpDenNgay.ValueChanged += dtpDenNgay_ValueChanged;
            chkLocTheoNgay.CheckedChanged += chkLocTheoNgay_CheckedChanged;

            // Tải danh sách nhân viên vào ComboBox
            LoadNhanVienComboBox();

            // Đặt giá trị mặc định cho DateTimePicker
            dtpTuNgay.Value = DateTime.Today.AddDays(-30);
            dtpDenNgay.Value = DateTime.Today;

            RefreshData();
        }

        private void LoadNhanVienComboBox()
        {
            try
            {
                var nhanViens = _nvDao.LayDanhSachNhanVien();
                cbbTenNhanVien.Items.Clear();
                cbbTenNhanVien.Items.Add(new NhanVienComboItem(null, "Tất cả")); // Thêm tùy chọn "Tất cả"
                foreach (var nv in nhanViens)
                {
                    cbbTenNhanVien.Items.Add(new NhanVienComboItem(nv.MaNV, nv.Ten ?? nv.TenTaiKhoan ?? ""));
                }
                cbbTenNhanVien.DisplayMember = "Ten";
                cbbTenNhanVien.ValueMember = "MaNV";
                cbbTenNhanVien.SelectedIndex = 0; // Mặc định chọn "Tất cả"
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[UserControlLichSuThayDoi] Lỗi LoadNhanVienComboBox: {ex.Message}");
                MessageBox.Show("Lỗi tải danh sách nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RefreshData()
        {
            try
            {
                var logs = _logSvc.LayDanhSach() ?? new System.Collections.Generic.List<QuanLyPhongKhachSan.DAL.OL.NhatKyHeThong>();
                Debug.WriteLine($"[UserControlLichSuThayDoi] Tổng log: {logs.Count}");

                // Lọc theo nhân viên
                if (cbbTenNhanVien.SelectedIndex > 0 && cbbTenNhanVien.SelectedItem is NhanVienComboItem selectedNV)
                {
                    int? maNV = selectedNV.MaNV;
                    logs = logs.Where(l => l.MaNV == maNV).ToList();
                }

                // Lọc theo khoảng thời gian nếu chkLocTheoNgay được check
                if (chkLocTheoNgay.Checked)
                {
                    DateTime tuNgay = dtpTuNgay.Value.Date;
                    DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1);
                    logs = logs.Where(l => l.ThoiGian >= tuNgay && l.ThoiGian <= denNgay).ToList();
                }

                // Map MaNV -> Tên NV
                var dictNV = _nvDao.LayDanhSachNhanVien()
                                   .GroupBy(x => x.MaNV)
                                   .ToDictionary(g => g.Key, g => g.First().Ten ?? "");

                // View: 4 field (Id ẩn, TenNV, Loai, ThoiGian)
                var view = logs.Select(l => new
                {
                    Id = l.Id,
                    TenNV = (l.MaNV.HasValue && dictNV.ContainsKey(l.MaNV.Value))
                                ? dictNV[l.MaNV.Value]
                                : (l.TenDangNhap ?? ""),
                    Loai = $"{(l.HanhDong ?? "").Trim()} / {(l.DoiTuong ?? "").Trim()}",
                    ThoiGian = l.ThoiGian.ToString("dd/MM/yyyy HH:mm:ss")
                }).ToList();

                dgvLog.DataSource = null;
                dgvLog.DataSource = view;
                dgvLog.Refresh();

                if (view.Count > 0)
                {
                    dgvLog.ClearSelection();
                    dgvLog.Rows[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[UserControlLichSuThayDoi] Lỗi RefreshData: {ex.Message}");
                MessageBox.Show("Lỗi tải nhật ký: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvLog_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvLog.CurrentRow == null) return;
            var row = dgvLog.Rows[e.RowIndex];

            int id = 0;
            try
            {
                id = Convert.ToInt32(row.Cells["Id"].Value ?? 0);
            }
            catch { id = 0; }

            if (id <= 0)
            {
                MessageBox.Show("Không xác định được Id nhật ký.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var frm = new frmChiTietNhatKy(id))
            {
                frm.ShowDialog(this);
            }
        }

        private void cbbTenNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {
            if (dtpTuNgay.Value > dtpDenNgay.Value)
            {
                dtpDenNgay.Value = dtpTuNgay.Value;
            }
            if (chkLocTheoNgay.Checked)
            {
                RefreshData();
            }
        }

        private void dtpDenNgay_ValueChanged(object sender, EventArgs e)
        {
            if (dtpDenNgay.Value < dtpTuNgay.Value)
            {
                dtpTuNgay.Value = dtpDenNgay.Value;
            }
            if (chkLocTheoNgay.Checked)
            {
                RefreshData();
            }
        }

        private void chkLocTheoNgay_CheckedChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
        private void dgvLog_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        public class NhanVienComboItem
        {
            public int? MaNV { get; set; }
            public string Ten { get; set; }

            public NhanVienComboItem(int? maNV, string ten)
            {
                MaNV = maNV;
                Ten = ten;
            }

            public override string ToString() => Ten;
        }

        private void cmsXoa_Click_1(object sender, EventArgs e)
        {
            if (dgvLog.CurrentRow == null || dgvLog.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một bản ghi nhật ký để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dgvLog.CurrentRow;
            int id = 0;
            try
            {
                id = Convert.ToInt32(row.Cells["Id"].Value ?? 0);
            }
            catch
            {
                MessageBox.Show("Không xác định được Id nhật ký.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (id <= 0)
            {
                MessageBox.Show("Không xác định được Id nhật ký.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi nhật ký này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            try
            {
                var dao = new NhatKyDAO();
                dao.Xoa(id);
                RefreshData();
                MessageBox.Show("Xóa bản ghi nhật ký thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[UserControlLichSuThayDoi] Lỗi xóa nhật ký: {ex.Message}");
                MessageBox.Show("Lỗi khi xóa nhật ký: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}