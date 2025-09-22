// Staff/Admin/frmChiTietNhatKy.cs
using QuanLyPhongKhachSan.BLL.Services;
using QuanLyPhongKhachSan.DAL.DAO;
using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public class frmChiTietNhatKy : Form
{
    private readonly int _id;
    private readonly NhatKyService _svc = new NhatKyService();
    private readonly NhanVienDAO _nvDao = new NhanVienDAO();

    // Control hiển thị HTML (không cần Designer)
    private WebBrowser web;

    public frmChiTietNhatKy(int id)
    {
        _id = id;
        InitializeComponent();      // tự định nghĩa ngay bên dưới
        this.Load += FrmChiTietNhatKy_Load;
    }

    // TỰ TẠO giao diện (thay cho Designer)
    private void InitializeComponent()
    {
        this.web = new WebBrowser
        {
            Dock = DockStyle.Fill,
            AllowWebBrowserDrop = false,
            IsWebBrowserContextMenuEnabled = true,
            ScriptErrorsSuppressed = true
        };

        this.Text = "Chi tiết nhật ký";
        this.StartPosition = FormStartPosition.CenterParent;
        this.Width = 900;
        this.Height = 700;

        this.Controls.Add(this.web);
    }

    // Encode HTML đơn giản (không cần thêm reference System.Web)
    private static string Encode(string s)
    {
        if (string.IsNullOrEmpty(s)) return "";
        return s.Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;");
    }

    private void FrmChiTietNhatKy_Load(object sender, EventArgs e)
    {
        var log = _svc.LayTheoId(_id);
        if (log == null)
        {
            MessageBox.Show("Không tìm thấy log.");
            Close();
            return;
        }

        // Lấy tên NV
        string tenNV = "";
        if (log.MaNV.HasValue)
        {
            var dictNV = _nvDao.LayDanhSachNhanVien()
                               .GroupBy(x => x.MaNV)
                               .ToDictionary(g => g.Key, g => g.First().Ten ?? "");
            dictNV.TryGetValue(log.MaNV.Value, out tenNV);
        }
        if (string.IsNullOrWhiteSpace(tenNV)) tenNV = log.TenDangNhap ?? "";

        string badge = log.KetQua ? "<span class='ok'>THÀNH CÔNG</span>" : "<span class='fail'>THẤT BẠI</span>";

        var sb = new StringBuilder();
        sb.Append(@"
<!doctype html>
<html><head><meta charset='utf-8'>
<style>
  body{font-family:Segoe UI,Roboto,Arial,sans-serif;margin:24px;background:#fafafa;color:#111}
  .card{background:#fff;border-radius:12px;box-shadow:0 2px 10px rgba(0,0,0,.08);padding:18px;margin-bottom:16px}
  h1{font-size:20px;margin:0 0 8px}
  .meta{display:grid;grid-template-columns:180px 1fr;row-gap:8px}
  .meta div{padding:6px 0;border-bottom:1px solid #eee}
  .label{color:#555}
  pre{white-space:pre-wrap;background:#0f172a;color:#e2e8f0;border-radius:8px;padding:12px;font-size:12.5px;line-height:1.45}
  .ok{background:#e6ffed;color:#067d3f;padding:2px 8px;border-radius:999px;font-weight:600}
  .fail{background:#ffeaea;color:#b00020;padding:2px 8px;border-radius:999px;font-weight:600}
  .small{color:#666;font-size:12px}
</style></head><body>");

        sb.Append("<div class='card'>");
        sb.Append("<h1>Chi tiết nhật ký</h1>");
        sb.Append("<div class='meta'>");
        sb.Append($"<div class='label'>Thời gian</div><div>{log.ThoiGian:dd/MM/yyyy HH:mm:ss}</div>");
        sb.Append($"<div class='label'>Nhân viên</div><div>{Encode(tenNV)}</div>");
        sb.Append($"<div class='label'>Loại thay đổi</div><div>{Encode(log.HanhDong)} / {Encode(log.DoiTuong)}</div>");
        sb.Append($"<div class='label'>Khóa chính</div><div>{Encode(log.KhoaChinh)}</div>");
        sb.Append($"<div class='label'>Mô tả</div><div>{Encode(log.MoTa)}</div>");
        sb.Append($"<div class='label'>Kết quả</div><div>{badge}</div>");
        if (!string.IsNullOrWhiteSpace(log.Loi))
            sb.Append($"<div class='label'>Lỗi</div><div style='color:#b00020'>{Encode(log.Loi)}</div>");
        sb.Append($"<div class='label'>Máy / IP</div><div class='small'>{Encode(log.TenMay)} / {Encode(log.DiaChiIP)}</div>");
        sb.Append("</div></div>");

        sb.Append("<div class='card'><h1>Dữ liệu cũ</h1><pre>");
        sb.Append(Encode(log.DuLieuCu));
        sb.Append("</pre></div>");

        sb.Append("<div class='card'><h1>Dữ liệu mới</h1><pre>");
        sb.Append(Encode(log.DuLieuMoi));
        sb.Append("</pre></div>");

        sb.Append("</body></html>");

        web.DocumentText = sb.ToString(); // ĐÃ có biến 'web'
    }
}
