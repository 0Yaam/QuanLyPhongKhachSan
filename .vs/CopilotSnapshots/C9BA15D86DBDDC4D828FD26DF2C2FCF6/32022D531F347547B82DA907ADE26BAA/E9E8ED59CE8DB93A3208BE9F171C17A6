// AppEvents.cs
using System;

namespace QuanLyPhongKhachSan
{
    public static class AppEvents
    {
        public static event Action InvoiceLogged;

        public static void RaiseInvoiceLogged()
        {
            try { InvoiceLogged?.Invoke(); } catch { /* ignore */ }
        }
    }
}
