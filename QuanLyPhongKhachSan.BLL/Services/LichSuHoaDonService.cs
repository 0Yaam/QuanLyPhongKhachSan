using System;
using System.Collections.Generic;
using QuanLyPhongKhachSan.DAL.DAO;
using QuanLyPhongKhachSan.DAL.OL;

namespace QuanLyPhongKhachSan.BLL.Services
{
    public class LichSuHoaDonService
    {
        private readonly LichSuHoaDonDAO _dao = new LichSuHoaDonDAO();

        public int Them(LichSuHoaDon x) => _dao.Them(x);

        public List<LichSuHoaDon> LayDanhSach() => _dao.LayDanhSach();


    }

}
