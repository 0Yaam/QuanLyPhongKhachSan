using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

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
        public int Them(TaiKhoan tk)
        {
            const string sql = @"
INSERT INTO TaiKhoan (TenDangNhap, MatKhau, Quyen, MaNV)
OUTPUT INSERTED.MaTK
VALUES (@TenDangNhap, @MatKhau, @Quyen, @MaNV);";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@TenDangNhap", tk.TenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", tk.MatKhau);
                cmd.Parameters.AddWithValue("@Quyen", tk.Quyen);
                cmd.Parameters.AddWithValue("@MaNV", tk.MaNV);

                conn.Open();
                var obj = cmd.ExecuteScalar();
                return (obj != null && obj != DBNull.Value) ? Convert.ToInt32(obj) : 0;
            }
        }


        public int ThemTaiKhoan(TaiKhoan tk)
        {
            const string sql = @"
INSERT INTO TaiKhoan (TenDangNhap, MatKhau, Quyen, MaNV)
VALUES (@User, @Pass, @Quyen, @MaNV);
SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (var conn = new SqlConnection(Config.ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@User", tk.TenDangNhap.Trim());
                cmd.Parameters.AddWithValue("@Pass", tk.MatKhau); // TODO: hash
                cmd.Parameters.AddWithValue("@Quyen", tk.Quyen);
                // Nếu MaNV optional, cho phép null:
                if (tk.MaNV > 0) cmd.Parameters.AddWithValue("@MaNV", tk.MaNV);
                else cmd.Parameters.AddWithValue("@MaNV", DBNull.Value);

                conn.Open();
                var id = cmd.ExecuteScalar();
                return id != null ? (int)id : 0;
            }
        }

        public int DemTheoTenDangNhap(string ten, SqlConnection conn, SqlTransaction tran)
        {
            using (var cmd = new SqlCommand(
                "SELECT COUNT(1) FROM dbo.TaiKhoan WHERE TenDangNhap = @u", conn, tran))
            {
                cmd.Parameters.AddWithValue("@u", ten);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void Them(TaiKhoan tk, SqlConnection conn, SqlTransaction tran)
        {
            using (var cmd = new SqlCommand(@"
INSERT INTO dbo.TaiKhoan (TenDangNhap, MatKhau, Quyen, MaNV)
VALUES (@U, @P, @Q, @MaNV);", conn, tran))
            {
                cmd.Parameters.AddWithValue("@U", tk.TenDangNhap);
                cmd.Parameters.AddWithValue("@P", tk.MatKhau);
                cmd.Parameters.AddWithValue("@Q", tk.Quyen);
                cmd.Parameters.AddWithValue("@MaNV", (object)tk.MaNV ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }


        public int CapNhatTaiKhoan(int maTK, string tenDangNhap, string matKhau)
        {
            const string sql = @"
UPDATE dbo.TaiKhoan
SET TenDangNhap = @U, MatKhau = @P
WHERE MaTK = @MaTK;";
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@U", (object)(tenDangNhap ?? string.Empty));
                cmd.Parameters.AddWithValue("@P", (object)(matKhau ?? string.Empty));
                cmd.Parameters.AddWithValue("@MaTK", maTK);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public int ResetMatKhau(int maTK, string newPass = "123")
        {
            const string sql = "UPDATE dbo.TaiKhoan SET MatKhau = @P WHERE MaTK = @MaTK;";
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@P", newPass);
                cmd.Parameters.AddWithValue("@MaTK", maTK);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public int XoaTheoMaNV(int maNV)
        {
            const string sql = "DELETE FROM dbo.TaiKhoan WHERE MaNV = @MaNV;";
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaNV", maNV);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public int CapNhatMatKhauByIds(IEnumerable<int> maTKs, string newPass)
        {
            if (maTKs == null) return 0;
            var ids = maTKs.Distinct().ToList();
            if (ids.Count == 0) return 0;

            const string sql = "UPDATE dbo.TaiKhoan SET MatKhau = @p WHERE MaTK = @id";

            using (var conn = new SqlConnection(Config.ConnectionString))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        int total = 0;
                        foreach (var id in ids)
                        {
                            using (var cmd = new SqlCommand(sql, conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@p", newPass);
                                cmd.Parameters.AddWithValue("@id", id);
                                total += cmd.ExecuteNonQuery();
                            }
                        }
                        tran.Commit();
                        return total; // số bản ghi cập nhật
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }








    }
}