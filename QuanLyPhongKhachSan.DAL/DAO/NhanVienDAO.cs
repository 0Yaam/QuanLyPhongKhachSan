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


        public int CapNhatThongTin(NhanVien nv)
        {
            const string sql = @"
UPDATE dbo.NhanVien
SET TenNV = @Ten, SDT = @SDT, CCCD = @CCCD, NgayThamGia = @Ngay
WHERE MaNV = @MaNV;";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Ten", (object)nv.TenNV ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SDT", (object)nv.SDT ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CCCD", (object)nv.CCCD ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Ngay", nv.NgayThamGia);
                cmd.Parameters.AddWithValue("@MaNV", nv.MaNV);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // DAL/DAO/NhanVienDAO.cs
        public List<NhanVienView> LayDanhSachNhanVien()
        {
            var list = new List<NhanVienView>();
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                const string sql = @"
SELECT 
    nv.MaNV,
    tk.MaTK,
    nv.TenNV,
    nv.SDT,
    nv.CCCD,
    RTRIM(tk.TenDangNhap) AS TenDangNhap,
    RTRIM(tk.MatKhau)     AS MatKhau,
    nv.NgayThamGia
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
                            MaNV = rd.GetInt32(0),
                            MaTK = rd.IsDBNull(1) ? (int?)null : rd.GetInt32(1),
                            Ten = rd.IsDBNull(2) ? "" : rd.GetString(2),
                            SDT = rd.IsDBNull(3) ? null : rd.GetString(3),
                            CCCD = rd.IsDBNull(4) ? null : rd.GetString(4),
                            TenTaiKhoan = rd.IsDBNull(5) ? null : rd.GetString(5),
                            MatKhau = rd.IsDBNull(6) ? null : rd.GetString(6),
                            NgayThamGia = rd.IsDBNull(7) ? DateTime.MinValue : rd.GetDateTime(7)
                        });
                    }
                }
            }
            return list;
        }


        public int Xoa(int maNV)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        // Xóa TK trước (nếu có)
                        using (var cmdTK = new SqlCommand("DELETE FROM dbo.TaiKhoan WHERE MaNV = @MaNV;", conn, tran))
                        {
                            cmdTK.Parameters.AddWithValue("@MaNV", maNV);
                            cmdTK.ExecuteNonQuery();
                        }
                        // Xóa NV
                        int rows;
                        using (var cmdNV = new SqlCommand("DELETE FROM dbo.NhanVien WHERE MaNV = @MaNV;", conn, tran))
                        {
                            cmdNV.Parameters.AddWithValue("@MaNV", maNV);
                            rows = cmdNV.ExecuteNonQuery();
                        }

                        tran.Commit();
                        return rows; // 1 nếu xóa thành công
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
