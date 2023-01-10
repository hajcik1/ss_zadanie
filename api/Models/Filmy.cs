using System;
using System.Collections.Generic;

namespace MojeFilmyAPI.Models;

public partial class Filmy
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Director { get; set; } = null!;

    public int? Year { get; set; }

    public float? Rate { get; set; }
}
