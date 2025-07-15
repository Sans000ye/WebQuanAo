using System;
using System.Collections.Generic;

namespace ClothingWebsite.Server.Models.Domains;

public partial class LoaiSanPham
{
    public string Loai { get; set; } = null!;

    public int MaLoai { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}