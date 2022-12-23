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
        public Task<ServiceResponse<Product>> AddNewProduct(Product product) => ProductDAO.Instance.AddNewProduct(product);

        public Task<ServiceResponse<string>> DeleteProduct(int id) => ProductDAO.Instance.DeleteProduct(id);

        public Task<ServiceResponse<ProductDTO>> GetProductById(int id) => ProductDAO.Instance.GetProductById(id);

        public Task<ServiceResponse<IEnumerable<ProductDTO>>> GetProducts() => ProductDAO.Instance.GetProducts();
    }
}
