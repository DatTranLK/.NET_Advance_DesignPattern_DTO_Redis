using BusinessObject.Dtos;
using BusinessObject.Models;
using DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {

        public Task<ServiceResponse<IEnumerable<ProductDTO>>> GetProducts() => ProductDAO.Instance.GetProducts();
    }
}
