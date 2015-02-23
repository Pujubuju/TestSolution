﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShoppingStore.Domain.Entities;

namespace OnlineShoppingStore.Domain.Abstract
{
    public interface IProductRepository
    {

        IEnumerable<Product> Products { get; } 

    }
}
