using System;
using System.Collections.Generic;

namespace DatVeMayBayApi.Models;

public partial class Chitietchuyenbay
{
    public int Mact { get; set; }

    public int Macb { get; set; }

    public int Masb { get; set; }

    public TimeSpan? Thoigiandung { get; set; }

    public string? Mota { get; set; }

    public virtual Chuyenbay MacbNavigation { get; set; } = null!;

    public virtual Sanbay MasbNavigation { get; set; } = null!;
}
