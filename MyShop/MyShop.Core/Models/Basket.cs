﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Basket : BaseEntity
    {
        // for Lazy Loading -- add virtual for EF
        public virtual ICollection<BasketItem> BasketItems { get; set; }

        public Basket()
        {
            BasketItems = new List<BasketItem>();
        }
    }
}
