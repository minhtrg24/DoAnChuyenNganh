
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoAnChuyenNganh.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnChuyenNganh.Controllers
{
    public class TongquanController : Controller
    {
        private readonly tiem_netContext _context;

        public TongquanController(tiem_netContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Tổng số máy
            ViewBag.TongSoMay =
                await _context.Computers.CountAsync();

            // Số máy đang chơi
            ViewBag.MayDangChoi =
                await _context.Computers.CountAsync(
                    x => x.Status == "IN_USE");

            // Số máy còn trống
            ViewBag.MayTrong =
                await _context.Computers.CountAsync(
                    x => x.Status == "AVAILABLE");

            // Doanh thu thanh toán trong ngày
            DateTime homNay = DateTime.Today;
            DateTime ngayMai = homNay.AddDays(1);

            ViewBag.DoanhThuHomNay =
                await _context.Payments
                    .Where(x => x.PaidAt >= homNay
                             && x.PaidAt < ngayMai)
                    .SumAsync(x => (decimal?)x.Amount) ?? 0m;

            return View();
        }
    }
}
