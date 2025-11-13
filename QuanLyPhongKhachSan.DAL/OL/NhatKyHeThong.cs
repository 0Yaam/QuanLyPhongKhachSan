namespace QuanLyPhongKhachSan.DAL.OL
{
    public class NhatKyHeThong
    {
        public int Id { get; set; }
        public int? MaNV { get; set; }
        public string TenDangNhap { get; set; }
        public System.DateTime ThoiGian { get; set; }
        public string HanhDong { get; set; }       // Thêm / Xoá / Sửa / In
        public string DoiTuong { get; set; }       // Phong / KhachHang / HoaDon
        public string KhoaChinh { get; set; }      // "MaPhong=12" hay "Ids=1,2,3"
        public string MoTa { get; set; }           // mô tả thân thiện
        public bool KetQua { get; set; }           // true = thành công
        public string Loi { get; set; }            // thông báo lỗi (nếu có)
        public string TenMay { get; set; }
        public string DiaChiIP { get; set; }
        public string DuLieuCu { get; set; }       // JSON/text optional
        public string DuLieuMoi { get; set; }      // JSON/text optional
    }
}
