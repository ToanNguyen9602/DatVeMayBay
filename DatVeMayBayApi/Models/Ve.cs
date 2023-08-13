using System;
using System.Collections.Generic;

namespace DatVeMayBayApi.Models;

public partial class Ve
{
    public int Mave { get; set; }

    public int Mahk { get; set; }

    public int Macb { get; set; }

    public int Soghe { get; set; }

    public int Loaighe { get; set; }

    public int Giaghe { get; set; }

    public virtual Chuyenbay MacbNavigation { get; set; } = null!;

    public virtual Hanhkhach MahkNavigation { get; set; } = null!;
}
