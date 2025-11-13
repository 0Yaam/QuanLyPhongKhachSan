using System;
using System.Data;
using QuanLyPhongKhachSan.DAL.DAO;
using QuanLyPhongKhachSan.BLL.Services;

namespace QuanLyPhongKhachSan.BLL.Services
{
    public class ThongKeNhanVienService
    {
        private readonly ThongKeNhanVienDAO _dao = new ThongKeNhanVienDAO();
        private const decimal CommissionRate = 0.03m; // chỉnh nếu cần

        public DataTable LayDanhSachNhanVien() => _dao.LayDanhSachNhanVien();

        public (DataTable daily, int total, decimal revenue, decimal commission)
            GetStats(DateTime from, DateTime to, int? maNV)
        {
            var daily = _dao.ThongKeKhachTheoNgay(from, to, maNV);
            var (total, revenue) = _dao.TongHop(from, to, maNV);
            var commission = Math.Round(revenue * CommissionRate, 0);
            return (daily, total, revenue, commission);
        }
    }
}
