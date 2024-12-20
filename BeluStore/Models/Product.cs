﻿using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;

namespace BeluStore.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int? CategoryId { get; set; }

    public int? SupplierId { get; set; }

    public decimal Price { get; set; }

    public string? ProductImage { get; set; }

    public int? QuantityInStock { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Supplier? Supplier { get; set; }

    public ImageSource ProductImageSource
    {
        get
        {
            if (string.IsNullOrEmpty(ProductImage) || !File.Exists(ProductImage))
                return null;
            return new BitmapImage(new Uri(ProductImage));
        }
    }
}
