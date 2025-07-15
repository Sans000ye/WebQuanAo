using ClothingWebsite.Server.Models;
using ClothingWebsite.Server.Models.Converter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClothingWebsite.Server.Data;
using ClothingWebsite.Server.DTOs;
using ClothingWebsite.Server.Models.Domains;


namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly QuanAoContext _context;

        public AuthController(QuanAoContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginRegisterRequest req)
        {
            if (await _context.TaiKhoans.AnyAsync(u => u.Username == req.Username))
                return BadRequest(new { message = "Username đã tồn tại" });

            var lastAccount = await _context.TaiKhoans
                .OrderByDescending(t => t.MaTaiKhoan)
                .FirstOrDefaultAsync();

            int nextNumber = 1;
            if (lastAccount != null && lastAccount.MaTaiKhoan.StartsWith("TK"))
            {
                string numberPart = lastAccount.MaTaiKhoan.Substring(2);
                if (int.TryParse(numberPart, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }
            string newMaTaiKhoan = $"TK{nextNumber:D3}";
            var user = new TaiKhoan
            {
                MaTaiKhoan = newMaTaiKhoan,
                Username = req.Username,
                Password = req.Password,
                QuyenTaiKhoan = 2
            };

            user.Password = user.Hashed_Password;
            _context.TaiKhoans.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Đăng ký thành công", MaTaiKhoan = newMaTaiKhoan });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRegisterRequest req)
        {
            var hasher = new HashPassword();
            string hashedPassword = hasher.HashSHA256(req.Password);

            var user = await _context.TaiKhoans
                .FirstOrDefaultAsync(u => u.Username == req.Username && u.Password == hashedPassword);
            if (user == null)
                return Unauthorized(new { message = "Tài khoản hoặc mật khẩu không đúng" });
            return Ok(new { message = "Đăng nhập thành công" });
        }
    }
}