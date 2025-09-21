// DAL/DAO/NhanVienDAO.cs
using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using static QuanLyPhongKhachSan.DAL.Config;

namespace QuanLyPhongKhachSan.DAL.DAO
{
    public class NhanVienDAO
    {
        private readonly string connectionString = Config.ConnectionString;

        // Dùng sẵn connection/transaction để BLL kiểm soát
        public int Them(NhanVien nv, SqlConnection conn, SqlTransaction tran)
        {
            using (var cmd = new SqlCommand(@"
INSERT INTO dbo.NhanVien (TenNV, CCCD, SDT, ChucVu, NgayThamGia)
VALUES (@TenNV, @CCCD, @SDT, @ChucVu, @NgayThamGia);
SELECT CAST(SCOPE_IDENTITY() AS INT);", conn, tran))
            {
                cmd.Parameters.AddWithValue("@TenNV", nv.TenNV);
                cmd.Parameters.AddWithValue("@CCCD", (object)nv.CCCD ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SDT", (object)nv.SDT ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ChucVu", nv.ChucVu ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayThamGia", nv.NgayThamGia);
                var id = cmd.ExecuteScalar();
                return (id == null) ? 0 : Convert.ToInt32(id);
            }
        }

        public List<NhanVienView> LayDanhSachNhanVien()
        {
            var list = new List<NhanVienView>();
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"
SELECT nv.TenNV, nv.SDT, nv.CCCD, tk.TenDangNhap, tk.MatKhau, nv.NgayThamGia
FROM NhanVien nv
INNER JOIN TaiKhoan tk ON nv.MaNV = tk.MaNV";

                using (var cmd = new SqlCommand(sql, conn))
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new NhanVienView
                        {
                            TenNV = rd.GetString(0),
                            SDT = rd.IsDBNull(1) ? null : rd.GetString(1),
                            CCCD = rd.IsDBNull(2) ? null : rd.GetString(2),
                            TenDangNhap = rd.GetString(3),
                            MatKhau = rd.GetString(4),
                            NgayThamGia = rd.GetDateTime(5)
                        });
                    }
                }
            }
            return list;
        }
    }

    // View model (dùng để bind DataGridView)
    public class NhanVienView
    {
        public string TenNV { get; set; }
        public string SDT { get; set; }
        public string CCCD { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public DateTime NgayThamGia { get; set; }
    }

}
