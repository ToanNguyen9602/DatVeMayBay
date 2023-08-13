using System;
using System.Collections.Generic;

namespace DatVeMayBayApi.Models;

public partial class Sanbay
{
    public int Masanbay { get; set; }

    public string Tensanbay { get; set; } = null!;

    public virtual ICollection<Chitietchuyenbay> Chitietchuyenbays { get; set; } = new List<Chitietchuyenbay>();

    public virtual ICollection<Chuyenbay> ChuyenbayMasbdenNavigations { get; set; } = new List<Chuyenbay>();

    public virtual ICollection<Chuyenbay> ChuyenbayMasbdiNavigations { get; set; } = new List<Chuyenbay>();
}
