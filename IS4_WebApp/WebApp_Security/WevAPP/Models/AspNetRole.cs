using System;
using System.Collections.Generic;

namespace WevAPP.Models;

public partial class AspNetRole
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; } = new List<AspNetRoleClaim>();

    public ICollection<AspNetUser> Users { get; set; } = new List<AspNetUser>();
}
