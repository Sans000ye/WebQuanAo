using ClothingWebsite.Server.DTOs;
using System;
using System.Collections.Generic;

namespace ClothingWebsite.Server.Models.Domains;

public partial class TaiKhoan
{
    public string MaTaiKhoan { get; set; } = null!;
    public string? Username { get; set; }
    public string? Password { get; set; }
    public int? QuyenTaiKhoan { get; set; }

    public virtual ICollection<SanPhamKhachHang> SanPhamKhachHangs { get; set; } = new List<SanPhamKhachHang>();

    public string Hashed_Password
    {
        get
        {
            var hasher = new HashPassword();
            return hasher.HashSHA256(Password);
        }
        set
        {
            Password = value;
        }
    }
}
