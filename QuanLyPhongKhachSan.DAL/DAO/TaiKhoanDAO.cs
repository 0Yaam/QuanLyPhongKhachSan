using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyPhongKhachSan.DAL.DAO
{
    public class TaiKhoanDAO
    {
        private string connectionString = Config.ConnectionString;

        public TaiKhoan KiemTraDangNhap(string tenDangNhap, string matKhau)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT MaTK, TenDangNhap, MatKhau, Quyen FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        return new TaiKhoan
                        {
                            MaTK = reader.GetInt32(0),
                            TenDangNhap = reader.GetString(1),
                            MatKhau = reader.GetString(2),
                            Quyen = reader.GetInt32(3) // Lấy int thay vì string
                        };
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi kiểm tra đăng nhập: " + ex.Message);
                return null;
            }
        }

        public List<TaiKhoan> LayDanhSachTaiKhoan()
        {
            List<TaiKhoan> danhSach = new List<TaiKhoan>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT MaTK, TenDangNhap, MatKhau, Quyen FROM TaiKhoan";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        danhSach.Add(new TaiKhoan
                        {
                            MaTK = reader.GetInt32(0),
                            TenDangNhap = reader.GetString(1),
                            MatKhau = reader.GetString(2),
                            Quyen = reader.GetInt32(3) // Lấy int
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách: " + ex.Message);
            }
            return danhSach;
        }

        public bool CapNhatMatKhau(int maTK, string matKhauMoi)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "UPDATE TaiKhoan SET MatKhau = @MatKhauMoi WHERE MaTK = @MaTK";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MatKhauMoi", matKhauMoi);
                    cmd.Parameters.AddWithValue("@MaTK", maTK);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật mật khẩu: " + ex.Message);
                return false;
            }
        }

        public bool ThemTaiKhoan(TaiKhoan taiKhoan)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO TaiKhoan (TenDangNhap, MatKhau, Quyen) VALUES (@TenDangNhap, @MatKhau, @Quyen)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@TenDangNhap", taiKhoan.TenDangNhap);
                    cmd.Parameters.AddWithValue("@MatKhau", taiKhoan.MatKhau);
                    cmd.Parameters.AddWithValue("@Quyen", taiKhoan.Quyen); // Sử dụng int
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm tài khoản: " + ex.Message);
                return false;
            }
        }

        public bool XoaTaiKhoan(int maTK)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "DELETE FROM TaiKhoan WHERE MaTK = @MaTK";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaTK", maTK);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xóa tài khoản: " + ex.Message);
                return false;
            }
        }
        public bool TenDangNhapDaTonTai(string username)
        {
            const string sql = "SELECT 1 FROM TaiKhoan WHERE TenDangNhap = @u";
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@u", SqlDbType.Char, 50).Value = username ?? string.Empty;
                conn.Open();
                var obj = cmd.ExecuteScalar();
                return obj != null;
            }
        }

        /// <summary>Thêm tài khoản. Trả về true nếu OK.</summary>
        public bool Them(string tenDangNhap, string matKhau, int quyen, int? maNV)
        {
            const string sql = @"
INSERT INTO TaiKhoan (TenDangNhap, MatKhau, Quyen, MaNV)
VALUES (@u, @p, @q, @manv);";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@u", SqlDbType.Char, 50).Value = tenDangNhap ?? string.Empty;
                cmd.Parameters.Add("@p", SqlDbType.Char, 50).Value = matKhau ?? string.Empty;
                cmd.Parameters.Add("@q", SqlDbType.Int).Value = quyen;
                if (maNV.HasValue)
                    cmd.Parameters.Add("@manv", SqlDbType.Int).Value = maNV.Value;
                else
                    cmd.Parameters.Add("@manv", SqlDbType.Int).Value = DBNull.Value;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }


    }
}