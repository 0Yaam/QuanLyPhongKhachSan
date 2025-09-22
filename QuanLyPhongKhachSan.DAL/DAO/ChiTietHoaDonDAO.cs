using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyPhongKhachSan.DAL.DAO
{
    public class ChiTietHoaDonDAO
    {
        private readonly string connectionString = Config.ConnectionString;

        public int Them(ChiTietHoaDon cthd)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // KHÔNG insert ThanhTien vì là computed
                    const string sql = @"
INSERT INTO ChiTietHoaDon (MaHD, DanhMuc, SoLuong, DonGia)
OUTPUT INSERTED.MaCT
VALUES (@MaHD, @DanhMuc, @SoLuong, @DonGia);";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@MaHD", SqlDbType.Int).Value = cthd.MaHD;

                        cmd.Parameters.Add("@DanhMuc", SqlDbType.NVarChar, 200)
                           .Value = (object)(cthd.TenDichVu ?? "") ?? DBNull.Value;

                        cmd.Parameters.Add("@SoLuong", SqlDbType.Int).Value = cthd.SoLuong;

                        var pDonGia = cmd.Parameters.Add("@DonGia", SqlDbType.Decimal);
                        pDonGia.Precision = 18;
                        pDonGia.Scale = 2;
                        pDonGia.Value = cthd.Gia;

                        var idObj = cmd.ExecuteScalar();
                        return (idObj == null || idObj == DBNull.Value) ? 0 : Convert.ToInt32(idObj);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi ChiTietHoaDonDAO.Them: {ex.Message}");
                return 0;
            }
        }
        public List<ChiTietHoaDon> LayTheoMaHD(int maHD)
        {
            var list = new List<ChiTietHoaDon>();
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(
                @"SELECT 
              MaCT      AS MaCTHD,      -- PK thật là MaCT → alias về MaCTHD
              MaHD, 
              DanhMuc   AS TenDichVu,   -- DanhMuc → alias về TenDichVu
              SoLuong, 
              DonGia    AS Gia          -- DonGia → alias về Gia
          FROM ChiTietHoaDon 
          WHERE MaHD = @id
          ORDER BY MaCT;", conn))
            {
                cmd.Parameters.AddWithValue("@id", maHD);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new ChiTietHoaDon
                        {
                            MaCTHD = rd.GetInt32(rd.GetOrdinal("MaCTHD")),
                            MaHD = rd.GetInt32(rd.GetOrdinal("MaHD")),
                            TenDichVu = rd.IsDBNull(rd.GetOrdinal("TenDichVu")) ? "" : rd.GetString(rd.GetOrdinal("TenDichVu")),
                            SoLuong = rd.IsDBNull(rd.GetOrdinal("SoLuong")) ? 0 : rd.GetInt32(rd.GetOrdinal("SoLuong")),
                            Gia = rd.IsDBNull(rd.GetOrdinal("Gia")) ? 0m : rd.GetDecimal(rd.GetOrdinal("Gia"))
                        });
                    }
                }
            }
            return list;
        }

    }
}
