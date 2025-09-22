using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyPhongKhachSan.DAL.DAO
{
    public class NhatKyDAO
    {
        private readonly string _cs = Config.ConnectionString;

        public int Them(NhatKyHeThong x)
        {
            const string sql = @"
INSERT INTO dbo.NhatKyHeThong
(MaNV, TenDangNhap, ThoiGian, HanhDong, DoiTuong, KhoaChinh, MoTa, KetQua, Loi, TenMay, DiaChiIP, DuLieuCu, DuLieuMoi)
OUTPUT INSERTED.Id
VALUES (@MaNV, @User, SYSDATETIME(), @HanhDong, @DoiTuong, @KhoaChinh, @MoTa, @KetQua, @Loi, @TenMay, @IP, @Cu, @Moi);";

            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaNV", (object)x.MaNV ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@User", (object)x.TenDangNhap ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@HanhDong", (object)x.HanhDong ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DoiTuong", (object)x.DoiTuong ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@KhoaChinh", (object)x.KhoaChinh ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MoTa", (object)x.MoTa ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@KetQua", x.KetQua);
                cmd.Parameters.AddWithValue("@Loi", (object)x.Loi ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TenMay", (object)x.TenMay ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IP", (object)x.DiaChiIP ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Cu", (object)x.DuLieuCu ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Moi", (object)x.DuLieuMoi ?? DBNull.Value);

                conn.Open();
                var obj = cmd.ExecuteScalar();
                return (obj == null || obj == DBNull.Value) ? 0 : System.Convert.ToInt32(obj);
            }
        }

        public List<NhatKyHeThong> LayDanhSach()
        {
            var list = new List<NhatKyHeThong>();
            const string sql = @"SELECT TOP 500 *
                                 FROM dbo.NhatKyHeThong
                                 ORDER BY ThoiGian DESC, Id DESC";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new NhatKyHeThong
                        {
                            Id = rd.GetInt32(rd.GetOrdinal("Id")),
                            MaNV = rd.IsDBNull(rd.GetOrdinal("MaNV")) ? (int?)null : rd.GetInt32(rd.GetOrdinal("MaNV")),
                            TenDangNhap = rd.IsDBNull(rd.GetOrdinal("TenDangNhap")) ? null : rd.GetString(rd.GetOrdinal("TenDangNhap")),
                            ThoiGian = rd.GetDateTime(rd.GetOrdinal("ThoiGian")),
                            HanhDong = rd.IsDBNull(rd.GetOrdinal("HanhDong")) ? null : rd.GetString(rd.GetOrdinal("HanhDong")),
                            DoiTuong = rd.IsDBNull(rd.GetOrdinal("DoiTuong")) ? null : rd.GetString(rd.GetOrdinal("DoiTuong")),
                            KhoaChinh = rd.IsDBNull(rd.GetOrdinal("KhoaChinh")) ? null : rd.GetString(rd.GetOrdinal("KhoaChinh")),
                            MoTa = rd.IsDBNull(rd.GetOrdinal("MoTa")) ? null : rd.GetString(rd.GetOrdinal("MoTa")),
                            KetQua = !rd.IsDBNull(rd.GetOrdinal("KetQua")) && rd.GetBoolean(rd.GetOrdinal("KetQua")),
                            Loi = rd.IsDBNull(rd.GetOrdinal("Loi")) ? null : rd.GetString(rd.GetOrdinal("Loi")),
                            TenMay = rd.IsDBNull(rd.GetOrdinal("TenMay")) ? null : rd.GetString(rd.GetOrdinal("TenMay")),
                            DiaChiIP = rd.IsDBNull(rd.GetOrdinal("DiaChiIP")) ? null : rd.GetString(rd.GetOrdinal("DiaChiIP")),
                            DuLieuCu = rd.IsDBNull(rd.GetOrdinal("DuLieuCu")) ? null : rd.GetString(rd.GetOrdinal("DuLieuCu")),
                            DuLieuMoi = rd.IsDBNull(rd.GetOrdinal("DuLieuMoi")) ? null : rd.GetString(rd.GetOrdinal("DuLieuMoi"))
                        });
                    }
                }
            }
            return list;
        }

        public NhatKyHeThong LayTheoId(int id)
        {
            const string sql = "SELECT * FROM dbo.NhatKyHeThong WHERE Id=@id";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        return new NhatKyHeThong
                        {
                            Id = rd.GetInt32(rd.GetOrdinal("Id")),
                            MaNV = rd.IsDBNull(rd.GetOrdinal("MaNV")) ? (int?)null : rd.GetInt32(rd.GetOrdinal("MaNV")),
                            TenDangNhap = rd.IsDBNull(rd.GetOrdinal("TenDangNhap")) ? null : rd.GetString(rd.GetOrdinal("TenDangNhap")),
                            ThoiGian = rd.GetDateTime(rd.GetOrdinal("ThoiGian")),
                            HanhDong = rd.IsDBNull(rd.GetOrdinal("HanhDong")) ? null : rd.GetString(rd.GetOrdinal("HanhDong")),
                            DoiTuong = rd.IsDBNull(rd.GetOrdinal("DoiTuong")) ? null : rd.GetString(rd.GetOrdinal("DoiTuong")),
                            KhoaChinh = rd.IsDBNull(rd.GetOrdinal("KhoaChinh")) ? null : rd.GetString(rd.GetOrdinal("KhoaChinh")),
                            MoTa = rd.IsDBNull(rd.GetOrdinal("MoTa")) ? null : rd.GetString(rd.GetOrdinal("MoTa")),
                            KetQua = !rd.IsDBNull(rd.GetOrdinal("KetQua")) && rd.GetBoolean(rd.GetOrdinal("KetQua")),
                            Loi = rd.IsDBNull(rd.GetOrdinal("Loi")) ? null : rd.GetString(rd.GetOrdinal("Loi")),
                            TenMay = rd.IsDBNull(rd.GetOrdinal("TenMay")) ? null : rd.GetString(rd.GetOrdinal("TenMay")),
                            DiaChiIP = rd.IsDBNull(rd.GetOrdinal("DiaChiIP")) ? null : rd.GetString(rd.GetOrdinal("DiaChiIP")),
                            DuLieuCu = rd.IsDBNull(rd.GetOrdinal("DuLieuCu")) ? null : rd.GetString(rd.GetOrdinal("DuLieuCu")),
                            DuLieuMoi = rd.IsDBNull(rd.GetOrdinal("DuLieuMoi")) ? null : rd.GetString(rd.GetOrdinal("DuLieuMoi"))
                        };
                    }
                }
            }
            return null;
        }
    }
}
