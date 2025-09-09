using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Data.SqlClient;

namespace QuanLyPhongKhachSan.DAL.DAO
{
    public class KhachHangDAO
    {
        public int KiemTraTonTai(string cccd, string sdt)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Config.ConnectionString))
                {
                    conn.Open();
                    string sql = "SELECT MaKH FROM KhachHang WHERE CCCD = @CCCD OR SDT = @SDT";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@CCCD", (object)cccd ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SDT", sdt);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi kiểm tra tồn tại khách hàng: {ex.Message}");
            }
        }

        public int ThemKhachHang(KhachHang khachHang)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Config.ConnectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO KhachHang (HoTen, CCCD, SDT) OUTPUT INSERTED.MaKH VALUES (@HoTen, @CCCD, @SDT)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@HoTen", khachHang.HoTen);
                    cmd.Parameters.AddWithValue("@CCCD", (object)khachHang.CCCD ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SDT", khachHang.SDT);
                    return (int)cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm khách hàng: {ex.Message}");
            }
        }

        public int CapNhatKhachHang(KhachHang khachHang)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Config.ConnectionString))
                {
                    conn.Open();
                    string sql = "UPDATE KhachHang SET HoTen = @HoTen, CCCD = @CCCD, SDT = @SDT WHERE MaKH = @MaKH";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@HoTen", khachHang.HoTen);
                    cmd.Parameters.AddWithValue("@CCCD", (object)khachHang.CCCD ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SDT", khachHang.SDT);
                    cmd.Parameters.AddWithValue("@MaKH", khachHang.MaKH);
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0 ? khachHang.MaKH : -1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật khách hàng: {ex.Message}");
            }
        }

        public KhachHang LayKhachHangTheoMaKH(int maKH)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Config.ConnectionString))
                {
                    conn.Open();
                    string sql = "SELECT MaKH, HoTen, CCCD, SDT FROM KhachHang WHERE MaKH = @MaKH";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaKH", maKH);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new KhachHang
                            {
                                MaKH = reader.GetInt32(0),
                                HoTen = reader.GetString(1),
                                CCCD = reader.IsDBNull(2) ? null : reader.GetString(2),
                                SDT = reader.GetString(3)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy khách hàng (MaKH={maKH}): {ex.Message}");
            }
        }
    }
}