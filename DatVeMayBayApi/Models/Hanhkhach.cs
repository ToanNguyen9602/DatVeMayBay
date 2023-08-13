using System;
using System.Collections.Generic;

namespace DatVeMayBayApi.Models;

public partial class Hanhkhach
{
    public int Mahk { get; set; }

    public string Hoten { get; set; } = null!;

    public int Cmnd { get; set; }

    public DateTime Ngaysinh { get; set; }

    public virtual ICollection<Ve> Ves { get; set; } = new List<Ve>();
}
