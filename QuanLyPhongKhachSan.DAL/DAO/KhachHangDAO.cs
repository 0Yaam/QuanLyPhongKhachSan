using System;
using System.Data.SqlClient;
using QuanLyPhongKhachSan.DAL.OL;

namespace QuanLyPhongKhachSan.DAL.DAO
{
    public class KhachHangDAO
    {
        private string connectionString = Config.ConnectionString;

        public int ThemKhachHang(KhachHang khachHang)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO KhachHang (HoTen, CCCD, SDT) VALUES (@HoTen, @CCCD, @SDT); SELECT CAST(SCOPE_IDENTITY() AS int)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@HoTen", khachHang.HoTen ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CCCD", khachHang.CCCD ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@SDT", khachHang.SDT ?? (object)DBNull.Value);
                    return (int)cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm khách hàng: " + ex.Message);
                return -1;
            }
        }

        public int CapNhatKhachHang(KhachHang khachHang)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"
                        UPDATE KhachHang 
                        SET HoTen = @HoTen, CCCD = @CCCD, SDT = @SDT 
                        WHERE MaKH = @MaKH";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaKH", khachHang.MaKH);
                    cmd.Parameters.AddWithValue("@HoTen", khachHang.HoTen ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CCCD", khachHang.CCCD ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@SDT", khachHang.SDT ?? (object)DBNull.Value);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0 ? khachHang.MaKH : -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật khách hàng: " + ex.Message);
                return -1;
            }
        }

        public int KiemTraTonTai(string cccd, string sdt)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT MaKH FROM KhachHang WHERE CCCD = @CCCD OR SDT = @SDT";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@CCCD", cccd ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@SDT", sdt ?? (object)DBNull.Value);
                    var result = cmd.ExecuteScalar();
                    return result != null && result != DBNull.Value ? Convert.ToInt32(result) : -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi kiểm tra tồn tại: " + ex.Message);
                return -1;
            }
        }
    }
}