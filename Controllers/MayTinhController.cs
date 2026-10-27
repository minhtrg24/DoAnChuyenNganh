
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoAnChuyenNganh.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnChuyenNganh.Controllers
{
    public class MayTinhController : Controller
    {
        private readonly tiem_netContext _context;

        public MayTinhController(tiem_netContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách máy
        public async Task<IActionResult> Index()
        {
            var danhSachMay = await _context.Computers
                .Include(m => m.Zone)
                .OrderBy(m => m.ComputerCode)
                .ToListAsync();

            return View(danhSachMay);
        }

        // Hiển thị giao diện thêm máy
        [HttpGet]
        public async Task<IActionResult> Them()
        {
            ViewBag.KhuVucs = await _context.ComputerZones
                .OrderBy(k => k.ZoneName)
                .ToListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Them(Computer may)
        {

            if (string.IsNullOrWhiteSpace(may.ComputerCode))
                ModelState.AddModelError("ComputerCode", "Vui lòng nhập mã máy.");

            if (await _context.Computers.AnyAsync(
                m => m.ComputerCode == may.ComputerCode))
                ModelState.AddModelError("ComputerCode", "Mã máy đã tồn tại.");

            if (may.HourlyRate <= 0)
                ModelState.AddModelError("HourlyRate", "Giá giờ phải lớn hơn 0.");

            if (!await _context.ComputerZones.AnyAsync(
                k => k.ZoneId == may.ZoneId))
                ModelState.AddModelError("ZoneId", "Vui lòng chọn khu vực.");
            may.Status = "AVAILABLE";
            ModelState.Remove("Status");
            // Bỏ qua kiểm tra thuộc tính điều hướng do EF Core tạo
            ModelState.Remove("Zone");
            ModelState.Remove("GamingSession");
            ModelState.Remove("MaintenanceLogs");

            if (!ModelState.IsValid)
            {
                ViewBag.KhuVucs = await _context.ComputerZones
                    .OrderBy(k => k.ZoneName)
                    .ToListAsync();

                return View(may);
            }

            may.Status = "AVAILABLE";
            may.CreatedAt = DateTime.Now;
            may.UpdatedAt = DateTime.Now;

            _context.Computers.Add(may);
            await _context.SaveChangesAsync();

            TempData["ThongBao"] = "Thêm máy tính thành công!";
            return RedirectToAction(nameof(Index));

        }
        // Hiển thị giao diện sửa máy
        [HttpGet]
        public async Task<IActionResult> Sua(long id)
        {
            var may = await _context.Computers.FindAsync(id);

            if (may == null)
                return NotFound();

            ViewBag.KhuVucs = await _context.ComputerZones
                .OrderBy(k => k.ZoneName)
                .ToListAsync();

            return View(may);
        }

        // Lưu thông tin máy đã sửa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sua(long id, Computer may)
        {
            var mayCu = await _context.Computers.FindAsync(id);

            if (mayCu == null)
                return NotFound();

            // Không yêu cầu nhập lại các trường hệ thống và thuộc tính điều hướng
            ModelState.Remove("Status");
            ModelState.Remove("Zone");
            ModelState.Remove("GamingSession");
            ModelState.Remove("MaintenanceLogs");

            if (string.IsNullOrWhiteSpace(may.ComputerCode))
                ModelState.AddModelError("ComputerCode", "Vui lòng nhập mã máy.");

            if (may.HourlyRate <= 0)
                ModelState.AddModelError("HourlyRate", "Giá giờ phải lớn hơn 0.");

            if (!await _context.ComputerZones.AnyAsync(k => k.ZoneId == may.ZoneId))
                ModelState.AddModelError("ZoneId", "Vui lòng chọn khu vực.");

            if (await _context.Computers.AnyAsync(
                m => m.ComputerCode == may.ComputerCode
                     && m.ComputerId != id))
            {
                ModelState.AddModelError("ComputerCode", "Mã máy đã tồn tại.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.KhuVucs = await _context.ComputerZones
                    .OrderBy(k => k.ZoneName)
                    .ToListAsync();

                return View(may);
            }

            // Chỉ cập nhật các thông tin được phép sửa
            mayCu.ComputerCode = may.ComputerCode;
            mayCu.ZoneId = may.ZoneId;
            mayCu.ComputerType = may.ComputerType;
            mayCu.HourlyRate = may.HourlyRate;
            mayCu.IpAddress = may.IpAddress;
            mayCu.Specifications = may.Specifications;
            mayCu.Notes = may.Notes;
            mayCu.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            TempData["ThongBao"] = "Sửa thông tin máy tính thành công!";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoa(long id)
        {
            var may = await _context.Computers.FindAsync(id);

            if (may == null)
            {
                return NotFound();
            }

            _context.Computers.Remove(may);
            await _context.SaveChangesAsync();

            TempData["ThongBao"] = "Xóa máy tính thành công!";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CapNhatTrangThai(long id, string trangThai)
        {
            var may = await _context.Computers.FindAsync(id);

            if (may == null)
            {
                return NotFound();
            }

            if (trangThai != "AVAILABLE" &&
                trangThai != "IN_USE" &&
                trangThai != "MAINTENANCE")
            {
                return BadRequest("Trạng thái không hợp lệ.");
            }

            may.Status = trangThai;
            may.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            TempData["ThongBao"] = "Cập nhật trạng thái thành công!";

            return RedirectToAction(nameof(Index));
        }


    }
}
