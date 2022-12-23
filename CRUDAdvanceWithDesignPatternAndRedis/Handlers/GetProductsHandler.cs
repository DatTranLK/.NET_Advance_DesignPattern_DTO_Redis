using BusinessObject.Dtos;
using BusinessObject.Models;
using CRUDAdvanceWithDesignPatternAndRedis.Queries;
using DataAccessObject;
using MediatR;
using Repository;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CRUDAdvanceWithDesignPatternAndRedis.Handlers
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, ServiceResponse<IEnumerable<ProductDTO>>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ServiceResponse<IEnumerable<ProductDTO>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var res = await _productRepository.GetProducts();
            return res;
        }
    }
}
