using System;
using System.Collections.Generic;

namespace Ecommerce.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Price { get; set; }

    public int Quantity { get; set; }

    public int CatagoryId { get; set; }

    public virtual Catagory Catagory { get; set; } = null!;
}
