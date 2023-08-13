using System;
using System.Collections.Generic;

namespace DatVeMayBayApi.Models;

public partial class ChuyenbayDto
{
    public int Macb { get; set; }

    public string? Tencb { get; set; } = null!;

    public int? Masbdi { get; set; }

    public int? Masbden { get; set; }

    public DateTime? Ngaydi { get; set; }

    public int? Gheloai1 { get; set; }

    public int? Giagheloai1 { get; set; }

    public int? Gheloai2 { get; set; }

    public int? Giagheloai2 { get; set; }

}
