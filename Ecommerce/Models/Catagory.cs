using System;
using System.Collections.Generic;

namespace Ecommerce.Models;

public partial class Catagory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
