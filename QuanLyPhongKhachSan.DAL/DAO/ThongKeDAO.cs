using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyPhongKhachSan.DAL.DAO
{
    public class ThongKeDAO
    {
        private readonly string connectionString = Config.ConnectionString;

        public DataTable GetDailyCustomerStats(DateTime fromDate, DateTime toDate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"
                        SELECT CAST(NgayThamGia AS DATE) AS Ngay, COUNT(*) AS SoKhach
                        FROM KhachHang
                        WHERE NgayThamGia IS NOT NULL
                          AND NgayThamGia BETWEEN @FromDate AND @ToDate
                        GROUP BY CAST(NgayThamGia AS DATE)
                        ORDER BY Ngay";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@FromDate", fromDate);
                        cmd.Parameters.AddWithValue("@ToDate", toDate);
                        DataTable dt = new DataTable();
                        using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                        {
                            ad.Fill(dt);
                        }
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi GetDailyCustomerStats: {ex.Message}");
                throw new Exception($"Lỗi khi lấy thống kê theo ngày: {ex.Message}");
            }
        }

        public int GetCustomerCountInMonth(DateTime firstDay, DateTime lastDay)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"
                        SELECT COUNT(*)
                        FROM KhachHang
                        WHERE NgayThamGia IS NOT NULL
                          AND NgayThamGia BETWEEN @FirstDay AND @LastDay";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstDay", firstDay);
                        cmd.Parameters.AddWithValue("@LastDay", lastDay);
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi GetCustomerCountInMonth: {ex.Message}");
                throw new Exception($"Lỗi khi lấy số lượng khách trong tháng: {ex.Message}");
            }
        }
    }
}