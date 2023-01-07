using System;
using System.Collections.Generic;

namespace MojeFilmyAPI.Models;

public partial class Filmy
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Director { get; set; } = null!;

    public decimal Year { get; set; }

    public decimal Rate { get; set; }
}
