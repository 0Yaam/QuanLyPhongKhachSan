using System;
using System.Data;
using QuanLyPhongKhachSan.DAL.DAO;

namespace QuanLyPhongKhachSan.BLL.Services
{
    public class ThongKeService
    {
        private readonly ThongKeDAO _dao = new ThongKeDAO();

        // Overload cũ (nếu nơi khác đang dùng)
        public (DataTable dailyStats, int totalCustomers, decimal commission)
            GetCustomerStatistics(DateTime from, DateTime to)
            => _dao.GetCustomerStatistics(from, to, null);

        // Overload mới có lọc theo nhân viên
        public (DataTable dailyStats, int totalCustomers, decimal commission)
            GetCustomerStatistics(DateTime from, DateTime to, int? maNV)
            => _dao.GetCustomerStatistics(from, to, maNV);
    }
}
