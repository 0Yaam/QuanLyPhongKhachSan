using System.Collections.Generic;
using QuanLyPhongKhachSan.DAL.DAO;
using QuanLyPhongKhachSan.DAL.OL;

namespace QuanLyPhongKhachSan.BLL.Services
{
    public class NhatKyService
    {
        private readonly NhatKyDAO _dao = new NhatKyDAO();
        public int Them(NhatKyHeThong x) => _dao.Them(x);
        public List<NhatKyHeThong> LayDanhSach() => _dao.LayDanhSach();
        public NhatKyHeThong LayTheoId(int id) => _dao.LayTheoId(id);
    }
}
