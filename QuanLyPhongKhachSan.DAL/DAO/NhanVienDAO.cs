// DAL/DAO/NhanVienDAO.cs
using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QuanLyPhongKhachSan.DAL.DAO
{
    public class NhanVienDAO
    {
        private readonly string _cs = Config.ConnectionString;

        // Đã dùng trong NhanSuService
        public int Them(NhanVien nv, SqlConnection conn, SqlTransaction tran)
        {
            const string sql = @"
INSERT INTO NhanVien (TenNV, CCCD, SDT, ChucVu, NgayThamGia)
OUTPUT INSERTED.MaNV
VALUES (@Ten, @CCCD, @SDT, @ChucVu, @NgayThamGia);";

            using (var cmd = new SqlCommand(sql, conn, tran))
            {
                cmd.Parameters.AddWithValue("@Ten", nv.TenNV);
                cmd.Parameters.AddWithValue("@CCCD", (object)nv.CCCD ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SDT", (object)nv.SDT ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ChucVu", (object)nv.ChucVu ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayThamGia", nv.NgayThamGia);

                var obj = cmd.ExecuteScalar();
                return (obj != null && obj != DBNull.Value) ? Convert.ToInt32(obj) : 0;
            }
        }

        // DÙNG ĐỂ LOAD LÊN DGV
        public List<NhanVienView> LayDanhSachNhanVien()
        {
            var list = new List<NhanVienView>();
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                const string sql = @"
SELECT 
    nv.TenNV                                  AS Ten,
    nv.SDT                                     AS SDT,
    nv.CCCD                                    AS CCCD,
    RTRIM(tk.TenDangNhap)                      AS TenTaiKhoan,
    RTRIM(tk.MatKhau)                          AS MatKhau,
    nv.NgayThamGia                             AS NgayThamGia
FROM dbo.NhanVien nv
LEFT JOIN dbo.TaiKhoan tk ON tk.MaNV = nv.MaNV
ORDER BY nv.TenNV;";

                using (var cmd = new SqlCommand(sql, conn))
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new NhanVienView
                        {
                            Ten = rd.IsDBNull(0) ? "" : rd.GetString(0),
                            SDT = rd.IsDBNull(1) ? null : rd.GetString(1),
                            CCCD = rd.IsDBNull(2) ? null : rd.GetString(2),
                            TenTaiKhoan = rd.IsDBNull(3) ? null : rd.GetString(3),
                            MatKhau = rd.IsDBNull(4) ? null : rd.GetString(4),
                            NgayThamGia = rd.IsDBNull(5) ? DateTime.MinValue : rd.GetDateTime(5)
                        });
                    }
                }
            }
            return list;
        }

        
    }
}
