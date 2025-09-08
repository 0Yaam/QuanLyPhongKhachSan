// QuanLyPhongKhachSan.BLL.Services.HoaDonService.cs
using System.Collections.Generic;
using QuanLyPhongKhachSan.DAL.DAO;
using QuanLyPhongKhachSan.DAL.OL;

namespace QuanLyPhongKhachSan.BLL.Services
{
    public class HoaDonService
    {
        private readonly HoaDonDAO _dao = new HoaDonDAO();

        public int TaoHoaDon(HoaDon hd) => _dao.ThemVaLayMa(hd);

        public List<HoaDon> LayDanhSach() => _dao.LayDanhSach();
    }
}
