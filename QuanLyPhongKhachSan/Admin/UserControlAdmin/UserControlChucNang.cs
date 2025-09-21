using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLyPhongKhachSan.DAL.DAO;
using QuanLyPhongKhachSan.DAL.OL;

namespace QuanLyPhongKhachSan.Login.UserControlAdmin
{
    public partial class UserControlChucNang : UserControl
    {
        private readonly LoaiPhongDAO _loaiDAO = new LoaiPhongDAO();
        private List<LoaiPhong> _loais = new List<LoaiPhong>();

        // Đang sửa loại nào (0 = không ở chế độ sửa)
        private int _editingMaLoai = 0;

        public UserControlChucNang()
        {
            InitializeComponent();

            txtLoaiPhong.PlaceholderText = "Thêm/Sửa loại phòng";

            this.Load -= UserControlChucNang_Load;
            this.Load += UserControlChucNang_Load;

            cbbLoaiPhong.SelectedIndexChanged -= cbbLoaiPhong_SelectedIndexChanged;
            cbbLoaiPhong.SelectedIndexChanged += cbbLoaiPhong_SelectedIndexChanged;

            btnHoanThanh.Click -= btnHoanThanh_Click;
            btnHoanThanh.Click += btnHoanThanh_Click;

            cmsXoa.Click -= cmsXoa_Click;
            cmsXoa.Click += cmsXoa_Click;

            // Bắt click trên lưới để đổ ngược dữ liệu sang các control
            dgvLoaiPhong.CellClick -= dgvLoaiPhong_CellClick;
            dgvLoaiPhong.CellClick += dgvLoaiPhong_CellClick;

            // numeric
            nudSuaGiaTien.Minimum = 0; nudSuaGiaTien.Maximum = 1000000000; nudSuaGiaTien.DecimalPlaces = 0;
            nudThemGiaTien.Minimum = 0; nudThemGiaTien.Maximum = 1000000000; nudThemGiaTien.DecimalPlaces = 0;

            dgvLoaiPhong.AutoGenerateColumns = false; // dùng cột đã set DataPropertyName trong Designer
        }

        private void UserControlChucNang_Load(object sender, EventArgs e)
        {
            try { ReloadAll(); }
            catch (Exception ex) { MessageBox.Show("Lỗi tải dữ liệu loại phòng: " + ex.Message); }
        }

        private void ReloadAll()
        {
            _editingMaLoai = 0; // reset chế độ sửa
            _loais = _loaiDAO.LayDanhSach() ?? new List<LoaiPhong>();
            BindComboLoaiPhong();
            LoadGridLoaiPhong();
            SyncPriceFromCombo();
        }

        private void BindComboLoaiPhong()
        {
            var data = _loais.OrderBy(x => x.TenLoaiPhong, StringComparer.CurrentCultureIgnoreCase).ToList();
            cbbLoaiPhong.DataSource = null;
            cbbLoaiPhong.DisplayMember = "TenLoaiPhong";
            cbbLoaiPhong.ValueMember = "MaLoaiPhong";
            cbbLoaiPhong.DataSource = data;
        }

        private void LoadGridLoaiPhong()
        {
            var raw = _loaiDAO.LayDanhSach() ?? new List<LoaiPhong>();
            var data = raw.Select(x => new
            {
                LoaiPhong = x.TenLoaiPhong ?? string.Empty,
                GiaPhong = x.GiaPhong
            }).ToList();

            dgvLoaiPhong.AutoGenerateColumns = false;
            dgvLoaiPhong.DataSource = null;
            dgvLoaiPhong.DataSource = data;

            if (dgvLoaiPhong.Columns.Contains("GiaPhong"))
            {
                dgvLoaiPhong.Columns["GiaPhong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLoaiPhong.Columns["GiaPhong"].DefaultCellStyle.Format = "N0";
            }
        }

        private void SyncPriceFromCombo()
        {
            if (cbbLoaiPhong.SelectedValue is int maLoai)
            {
                var sel = _loais.FirstOrDefault(x => x.MaLoaiPhong == maLoai);
                if (sel != null)
                {
                    var gia = Math.Max(0, sel.GiaPhong);
                    nudSuaGiaTien.Value = ClampToRange(gia, nudSuaGiaTien);
                    return;
                }
            }
            nudSuaGiaTien.Value = 0;
        }

        private static decimal ClampToRange(decimal v, Guna.UI2.WinForms.Guna2NumericUpDown nud)
        {
            if (v < nud.Minimum) return nud.Minimum;
            if (v > nud.Maximum) return nud.Maximum;
            return v;
        }



        private void cbbLoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            // đổi lựa chọn combobox → đồng bộ giá sửa
            SyncPriceFromCombo();
        }

        // ===== CLICK LƯỚI: đưa dữ liệu sang cả 2 nhóm control để sửa nhanh =====
        private void dgvLoaiPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvLoaiPhong.Rows[e.RowIndex];

            var ten = row.Cells["LoaiPhong"]?.Value?.ToString() ?? "";
            var giaObj = row.Cells["GiaPhong"]?.Value;
            decimal gia = 0;
            decimal.TryParse(Convert.ToString(giaObj), out gia);

            // Tìm entity thực từ danh sách để lấy MaLoaiPhong
            var loai = _loais.FirstOrDefault(x =>
                string.Equals(x.TenLoaiPhong, ten, StringComparison.CurrentCultureIgnoreCase));

            if (loai == null)
            {
                _editingMaLoai = 0;
                return;
            }

            _editingMaLoai = loai.MaLoaiPhong;

            // Đồng bộ về combobox + nud sửa giá
            cbbLoaiPhong.SelectedValue = loai.MaLoaiPhong;
            nudSuaGiaTien.Value = ClampToRange(gia, nudSuaGiaTien);

            // Đồng bộ qua khu "thêm" để dùng cho đổi tên + đổi giá
            txtLoaiPhong.Text = loai.TenLoaiPhong;
            nudThemGiaTien.Value = ClampToRange(gia, nudThemGiaTien);
        }

        /// <summary>
        /// - Nếu _editingMaLoai > 0  => đang SỬA loại: đổi tên (txtLoaiPhong) + đổi giá (ưu tiên nudThemGiaTien, nếu =0 thì lấy nudSuaGiaTien).
        /// - Nếu _editingMaLoai = 0  => như cũ:
        ///     + txtLoaiPhong có text  => THÊM MỚI với giá từ nudThemGiaTien.
        ///     + txtLoaiPhong trống    => CẬP NHẬT GIÁ cho loại đang chọn ở combobox bằng nudSuaGiaTien.
        /// </summary>
        private void btnHoanThanh_Click(object sender, EventArgs e)
        {
            try
            {
                if (_editingMaLoai > 0)
                {
                    // ===== CHẾ ĐỘ SỬA (đã click một dòng) =====
                    string tenMoi = (txtLoaiPhong.Text ?? "").Trim();
                    if (string.IsNullOrWhiteSpace(tenMoi))
                    {
                        MessageBox.Show("Vui lòng nhập tên loại phòng.");
                        return;
                    }

                    // lấy giá mới: ưu tiên nudThemGiaTien (đang dùng cho rename), nếu =0 thì dùng nudSuaGiaTien
                    decimal giaMoi = nudThemGiaTien.Value > 0 ? nudThemGiaTien.Value : nudSuaGiaTien.Value;
                    if (giaMoi <= 0)
                    {
                        MessageBox.Show("Vui lòng nhập giá > 0.");
                        return;
                    }

                    // Kiểm tra trùng tên với loại khác
                    bool nameClash = _loais.Any(x =>
                        x.MaLoaiPhong != _editingMaLoai &&
                        string.Equals(x.TenLoaiPhong, tenMoi, StringComparison.OrdinalIgnoreCase));
                    if (nameClash)
                    {
                        MessageBox.Show("Tên loại phòng đã tồn tại. Vui lòng chọn tên khác.");
                        return;
                    }

                    var ent = new LoaiPhong
                    {
                        MaLoaiPhong = _editingMaLoai,
                        TenLoaiPhong = tenMoi,
                        GiaPhong = giaMoi
                    };

                    int rows = _loaiDAO.CapNhat(ent);
                    if (rows <= 0)
                    {
                        MessageBox.Show("Cập nhật loại phòng thất bại.");
                        return;
                    }

                    var keepId = _editingMaLoai;
                    ReloadAll();
                    cbbLoaiPhong.SelectedValue = keepId;

                    // Xoá chế độ sửa (nếu muốn giữ thì comment 3 dòng dưới)
                    _editingMaLoai = 0;
                    txtLoaiPhong.Clear();
                    nudThemGiaTien.Value = 0;
                    return;
                }

                // ===== KHÔNG Ở CHẾ ĐỘ SỬA: HÀNH VI CŨ =====
                var isAdding = !string.IsNullOrWhiteSpace(txtLoaiPhong.Text);

                if (isAdding)
                {
                    // THÊM MỚI
                    string ten = txtLoaiPhong.Text.Trim();
                    if (string.IsNullOrWhiteSpace(ten))
                    {
                        MessageBox.Show("Vui lòng nhập tên loại phòng.");
                        return;
                    }

                    decimal gia = nudThemGiaTien.Value;
                    if (gia <= 0)
                    {
                        MessageBox.Show("Vui lòng nhập giá tiền (nud Thêm giá tiền) > 0 trước khi bấm Hoàn thành.");
                        return;
                    }

                    bool exists = _loais.Any(x => string.Equals(x.TenLoaiPhong, ten, StringComparison.OrdinalIgnoreCase));
                    if (exists)
                    {
                        var ans = MessageBox.Show(
                            "Tên loại phòng đã tồn tại. Bạn có muốn cập nhật giá cho loại này?",
                            "Trùng tên", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (ans == DialogResult.Yes)
                        {
                            var loai = _loais.First(x => string.Equals(x.TenLoaiPhong, ten, StringComparison.OrdinalIgnoreCase));
                            loai.GiaPhong = gia;
                            var rows2 = _loaiDAO.CapNhat(loai);
                            if (rows2 <= 0)
                            {
                                MessageBox.Show("Cập nhật giá thất bại.");
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        var loai = new LoaiPhong { TenLoaiPhong = ten, GiaPhong = gia };
                        int id = _loaiDAO.Them(loai);
                        if (id <= 0)
                        {
                            MessageBox.Show("Thêm loại phòng thất bại.");
                            return;
                        }
                    }

                    txtLoaiPhong.Clear();
                    nudThemGiaTien.Value = 0;
                    ReloadAll();
                    cbbLoaiPhong.SelectedItem = _loais.FirstOrDefault(x =>
                        string.Equals(x.TenLoaiPhong, ten, StringComparison.OrdinalIgnoreCase));
                }
                else
                {
                    // CẬP NHẬT GIÁ CHO LOẠI ĐANG CHỌN Ở COMBO
                    if (cbbLoaiPhong.SelectedItem == null)
                    {
                        MessageBox.Show("Vui lòng chọn loại phòng để cập nhật giá.");
                        return;
                    }
                    var sel = (LoaiPhong)cbbLoaiPhong.SelectedItem;
                    decimal giaMoi = nudSuaGiaTien.Value;
                    if (giaMoi <= 0)
                    {
                        MessageBox.Show("Giá mới phải > 0.");
                        return;
                    }

                    sel.GiaPhong = giaMoi;
                    int rows = _loaiDAO.CapNhat(sel);
                    if (rows <= 0)
                    {
                        MessageBox.Show("Cập nhật giá thất bại.");
                        return;
                    }

                    ReloadAll();
                    cbbLoaiPhong.SelectedValue = sel.MaLoaiPhong;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xử lý: " + ex.Message);
            }
        }

        private void cmsXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string tenChon = null;

                if (dgvLoaiPhong.CurrentRow != null && dgvLoaiPhong.CurrentRow.Index >= 0)
                {
                    var row = dgvLoaiPhong.CurrentRow;
                    object val = row.Cells["LoaiPhong"]?.Value;
                    tenChon = val?.ToString();
                }

                if (string.IsNullOrWhiteSpace(tenChon) && cbbLoaiPhong.SelectedItem is LoaiPhong loaiSel)
                    tenChon = loaiSel.TenLoaiPhong;

                if (string.IsNullOrWhiteSpace(tenChon))
                {
                    MessageBox.Show("Chưa chọn loại phòng để xóa.");
                    return;
                }

                var loai = _loais.FirstOrDefault(x =>
                    string.Equals(x.TenLoaiPhong, tenChon, StringComparison.OrdinalIgnoreCase));
                if (loai == null)
                {
                    MessageBox.Show("Không tìm thấy loại phòng tương ứng.");
                    return;
                }

                int countPhong = _loaiDAO.DemSoPhongDangDung(loai.MaLoaiPhong);
                if (countPhong > 0)
                {
                    MessageBox.Show(
                        $"Không thể xóa vì vẫn còn {countPhong} phòng đang sử dụng loại \"{loai.TenLoaiPhong}\".\n" +
                        "Vui lòng đổi loại của các phòng đó trước khi xóa.",
                        "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show($"Xóa loại phòng \"{loai.TenLoaiPhong}\"?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                int rows;
                try { rows = _loaiDAO.Xoa(loai.MaLoaiPhong); }
                catch (InvalidOperationException ex) // ném từ DAO khi dính FK
                {
                    MessageBox.Show(ex.Message, "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (rows <= 0)
                {
                    MessageBox.Show("Không có bản ghi nào bị xóa (có thể loại này đã không còn).");
                    ReloadAll();
                    return;
                }

                ReloadAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa: " + ex.Message);
            }
        }

        // ===== Handlers trống nếu Designer đã gán =====
        private void txtLoaiPhong_TextChanged(object sender, EventArgs e) { }
        private void txtTrangThai_TextChanged(object sender, EventArgs e) { }
        private void dgvTrangThai_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void dgvLoaiPhong_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void nudThemGiaTien_ValueChanged(object sender, EventArgs e) { }
        private void nudGiaTien_ValueChanged(object sender, EventArgs e) { }
    }
}
