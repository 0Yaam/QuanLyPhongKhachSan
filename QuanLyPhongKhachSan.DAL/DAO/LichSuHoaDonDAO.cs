using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyPhongKhachSan.DAL.OL;

namespace QuanLyPhongKhachSan.DAL.DAO
{
    public class LichSuHoaDonDAO
    {
        private readonly string _connStr = Config.ConnectionString;

        public int Them(LichSuHoaDon x)
        {
            const string sql = @"
INSERT INTO LichSuHoaDon (MaHD, MaDat, ThoiGianIn, MaNV, SoPhong)
VALUES (@MaHD, @MaDat, @ThoiGianIn, @MaNV, @SoPhong);
SELECT CAST(SCOPE_IDENTITY() AS int);";

            try
            {
                using (var conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@MaHD", SqlDbType.Int).Value = (x.MaHD == 0 ? (object)DBNull.Value : x.MaHD);
                        cmd.Parameters.Add("@MaDat", SqlDbType.Int).Value = (x.MaDat == 0 ? (object)DBNull.Value : x.MaDat);
                        cmd.Parameters.Add("@ThoiGianIn", SqlDbType.DateTime2).Value = x.ThoiGianIn;
                        cmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = (x.MaNV == 0 ? (object)DBNull.Value : x.MaNV);
                        cmd.Parameters.Add("@SoPhong", SqlDbType.NVarChar).Value = (object)x.SoPhong ?? DBNull.Value;

                        var idObj = cmd.ExecuteScalar();
                        return (idObj == null || idObj == DBNull.Value) ? 0 : Convert.ToInt32(idObj);
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"SQL Error LichSuHoaDonDAO.Them: MaHD={x.MaHD}, MaDat={x.MaDat}, Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error LichSuHoaDonDAO.Them: {ex.Message}");
                throw;
            }
        }

        public List<LichSuHoaDon> LayDanhSach()
        {
            const string sql = @"
SELECT 
    l.Id, l.MaHD, l.MaDat, l.ThoiGianIn, l.MaNV, l.SoPhong,
    ISNULL(k.HoTen, '') AS TenKH,
    ISNULL(k.CCCD, '') AS CCCD,
    ISNULL(k.SDT, '') AS SDT,
    ISNULL(h.LoaiHoaDon, '') AS LoaiHoaDon
FROM LichSuHoaDon l
LEFT JOIN DatPhong dp ON dp.MaDat = l.MaDat
LEFT JOIN KhachHang k ON k.MaKH = dp.MaKH
LEFT JOIN HoaDon h ON h.MaHD = l.MaHD
ORDER BY l.ThoiGianIn DESC";

            var list = new List<LichSuHoaDon>();
            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    int ordId = rd.GetOrdinal("Id");
                    int ordMaHD = rd.GetOrdinal("MaHD");
                    int ordMaDat = rd.GetOrdinal("MaDat");
                    int ordThoiGianIn = rd.GetOrdinal("ThoiGianIn");
                    int ordMaNV = rd.GetOrdinal("MaNV");
                    int ordSoPhong = rd.GetOrdinal("SoPhong");
                    int ordTenKH = rd.GetOrdinal("TenKH");
                    int ordCCCD = rd.GetOrdinal("CCCD");
                    int ordSDT = rd.GetOrdinal("SDT");
                    int ordLoaiHoaDon = rd.GetOrdinal("LoaiHoaDon");

                    while (rd.Read())
                    {
                        list.Add(new LichSuHoaDon
                        {
                            Id = rd.GetInt32(ordId),
                            MaHD = rd.IsDBNull(ordMaHD) ? 0 : rd.GetInt32(ordMaHD),
                            MaDat = rd.IsDBNull(ordMaDat) ? 0 : rd.GetInt32(ordMaDat),
                            ThoiGianIn = rd.GetDateTime(ordThoiGianIn),
                            MaNV = rd.IsDBNull(ordMaNV) ? 0 : rd.GetInt32(ordMaNV),
                            SoPhong = rd.IsDBNull(ordSoPhong) ? string.Empty : rd.GetString(ordSoPhong),
                            TenKH = rd.IsDBNull(ordTenKH) ? string.Empty : rd.GetString(ordTenKH),
                            CCCD = rd.IsDBNull(ordCCCD) ? string.Empty : rd.GetString(ordCCCD),
                            SDT = rd.IsDBNull(ordSDT) ? string.Empty : rd.GetString(ordSDT),
                            LoaiHoaDon = rd.IsDBNull(ordLoaiHoaDon) ? string.Empty : rd.GetString(ordLoaiHoaDon)
                        });
                    }
                }
            }
            return list;
        }

        public int ThemNeuChuaCo(LichSuHoaDon x)
        {
            const string sqlSelect = @"
SELECT TOP 1 Id FROM LichSuHoaDon 
WHERE MaHD = @MaHD AND ThoiGianIn = @ThoiGianIn";

            const string sqlInsert = @"
INSERT INTO LichSuHoaDon (MaHD, MaDat, ThoiGianIn, MaNV, SoPhong)
VALUES (@MaHD, @MaDat, @ThoiGianIn, @MaNV, @SoPhong);
SELECT SCOPE_IDENTITY();";

            try
            {
                using (var conn = new SqlConnection(_connStr))
                using (var cmd = new SqlCommand(sqlSelect, conn))
                {
                    cmd.Parameters.Add("@MaHD", SqlDbType.Int).Value = (x.MaHD == 0 ? (object)DBNull.Value : x.MaHD);
                    cmd.Parameters.Add("@ThoiGianIn", SqlDbType.DateTime2).Value = x.ThoiGianIn;
                    conn.Open();

                    var existIdObj = cmd.ExecuteScalar();
                    if (existIdObj != null && existIdObj != DBNull.Value)
                        return Convert.ToInt32(existIdObj);
                }

                using (var conn = new SqlConnection(_connStr))
                using (var cmd = new SqlCommand(sqlInsert, conn))
                {
                    cmd.Parameters.Add("@MaHD", SqlDbType.Int).Value = (x.MaHD == 0 ? (object)DBNull.Value : x.MaHD);
                    cmd.Parameters.Add("@MaDat", SqlDbType.Int).Value = (x.MaDat == 0 ? (object)DBNull.Value : x.MaDat);
                    cmd.Parameters.Add("@ThoiGianIn", SqlDbType.DateTime2).Value = x.ThoiGianIn;
                    cmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = (x.MaNV == 0 ? (object)DBNull.Value : x.MaNV);
                    cmd.Parameters.Add("@SoPhong", SqlDbType.NVarChar).Value = (object)x.SoPhong ?? DBNull.Value;

                    conn.Open();
                    var obj = cmd.ExecuteScalar();
                    return (obj != null && int.TryParse(obj.ToString(), out int id)) ? id : 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error LichSuHoaDonDAO.ThemNeuChuaCo: {ex.Message}");
                return 0;
            }
        }
    }
}