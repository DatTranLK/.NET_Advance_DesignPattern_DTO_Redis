﻿using BusinessObject.Dtos;
using BusinessObject.Models;
using DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IProductRepository
    {
        Task<ServiceResponse<IEnumerable<ProductDTO>>> GetProducts();
    }
}
