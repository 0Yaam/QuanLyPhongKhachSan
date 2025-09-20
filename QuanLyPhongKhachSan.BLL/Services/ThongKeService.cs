using QuanLyPhongKhachSan.DAL.DAO;
using System;
using System.Data;

namespace QuanLyPhongKhachSan.BLL.Services
{
    public class ThongKeService
    {
        private readonly ThongKeDAO _dao = new ThongKeDAO();

        public (DataTable DailyStats, int TotalCustomers, decimal Commission) GetCustomerStatistics(DateTime fromDate, DateTime toDate)
        {
            try
            {
                DataTable dailyStats = _dao.GetDailyCustomerStats(fromDate, toDate);

                // Tính tổng số khách trong khoảng thời gian
                int totalCustomers = 0;
                foreach (DataRow row in dailyStats.Rows)
                {
                    totalCustomers += Convert.ToInt32(row["SoKhach"]);
                }

                // Tính hoa hồng dựa trên số khách trong tháng hiện tại
                DateTime now = DateTime.Today;
                DateTime firstDay = new DateTime(now.Year, now.Month, 1);
                DateTime lastDay = firstDay.AddMonths(1).AddTicks(-1);
                int countInMonth = _dao.GetCustomerCountInMonth(firstDay, lastDay);
                int threshold = 50;
                int overThreshold = Math.Max(0, countInMonth - threshold);
                decimal commission = overThreshold * 5000m;

                return (dailyStats, totalCustomers, commission);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi GetCustomerStatistics: {ex.Message}");
                throw new Exception($"Lỗi khi lấy thống kê khách hàng: {ex.Message}");
            }
        }
    }
}