﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class OrderItem : BaseEntity
    {
        public string OrderId { get; set; }

        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public int ProductQuantity { get; set; }

        public string ProductImage { get; set; }


    }
}
