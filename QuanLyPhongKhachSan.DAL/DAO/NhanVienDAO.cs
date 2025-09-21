using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyPhongKhachSan.DAL.OL;
using static QuanLyPhongKhachSan.DAL.Config;

namespace QuanLyPhongKhachSan.DAL.DAO
{
    public class NhanVienDAO
    {
        private readonly string _cs = Config.ConnectionString;

        /// <summary>Thêm nhân viên, trả về MaNV (IDENTITY) hoặc -1 nếu lỗi.</summary>
        public int ThemTraMa(NhanVien nv)
        {
            const string sql = @"
INSERT INTO NhanVien (TenNV, CCCD, SDT, ChucVu)
VALUES (@TenNV, @CCCD, @SDT, @ChucVu);
SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@TenNV", SqlDbType.NVarChar, 100).Value = (object)nv.TenNV ?? DBNull.Value;
                cmd.Parameters.Add("@CCCD", SqlDbType.VarChar, 15).Value = (object)nv.CCCD ?? DBNull.Value;
                cmd.Parameters.Add("@SDT", SqlDbType.VarChar, 15).Value = (object)nv.SDT ?? DBNull.Value;
                cmd.Parameters.Add("@ChucVu", SqlDbType.NVarChar, 50).Value = (object)nv.ChucVu ?? DBNull.Value;

                conn.Open();
                var obj = cmd.ExecuteScalar();
                return (obj != null && obj != DBNull.Value) ? Convert.ToInt32(obj) : -1;
            }
        }
    }
}
