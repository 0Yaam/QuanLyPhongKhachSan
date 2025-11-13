using System;
using System.Linq;
using System.Net;
using QuanLyPhongKhachSan.BLL.Services;
using QuanLyPhongKhachSan.DAL.OL;

namespace QuanLyPhongKhachSan.UI.Helpers
{
    public static class AuditHelper
    {
        private static int? _currentMaNV;
        private static string _currentUsername;

        public static void SetCurrentUser(int? maNV, string username)
        {
            _currentMaNV = maNV;
            _currentUsername = username;
        }

        public static void Log(
            string hanhDong,
            string doiTuong,
            string khoaChinh,
            string moTa,
            bool ketQua = true,
            string loi = null,
            string duLieuCu = null,
            string duLieuMoi = null)
        {
            try
            {
                var svc = new NhatKyService();
                var host = Dns.GetHostName();
                string ip = "";
                try
                {
                    ip = Dns.GetHostAddresses(host)
                            .FirstOrDefault(a => a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?
                            .ToString() ?? "";
                }
                catch { }

                var x = new NhatKyHeThong
                {
                    MaNV = _currentMaNV,
                    TenDangNhap = _currentUsername,
                    HanhDong = hanhDong,
                    DoiTuong = doiTuong,
                    KhoaChinh = khoaChinh,
                    MoTa = moTa,
                    KetQua = ketQua,
                    Loi = loi,
                    TenMay = host,
                    DiaChiIP = ip,
                    DuLieuCu = duLieuCu,
                    DuLieuMoi = duLieuMoi,
                    ThoiGian = DateTime.Now
                };
                svc.Them(x);
            }
            catch
            {
                // nuốt lỗi logging để không phá flow ứng dụng
            }
        }

        public static void LogSuccess(string hanhDong, string doiTuong, string khoaChinh, string moTa,
            string duLieuCu = null, string duLieuMoi = null)
            => Log(hanhDong, doiTuong, khoaChinh, moTa, true, null, duLieuCu, duLieuMoi);

        public static void LogFail(string hanhDong, string doiTuong, string khoaChinh, string moTa, string loi)
            => Log(hanhDong, doiTuong, khoaChinh, moTa, false, loi, null, null);
    }
}
