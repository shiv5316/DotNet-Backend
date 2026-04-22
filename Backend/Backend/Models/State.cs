using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class State
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? CountryId { get; set; }

    public virtual Country? Country { get; set; }
}
