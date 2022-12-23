using BusinessObject.Dtos;
using BusinessObject.Models;
using CRUDAdvanceWithDesignPatternAndRedis.Commands;
using DataAccessObject;
using MediatR;
using Repository;
using System.Threading;
using System.Threading.Tasks;

namespace CRUDAdvanceWithDesignPatternAndRedis.Handlers
{
    public class AddProductHandler : IRequestHandler<AddProductCommand, ServiceResponse<Product>>
    {
        private readonly IProductRepository _productRepository;

        public AddProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ServiceResponse<Product>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var res = await _productRepository.AddNewProduct(request.Product);
            return res;
        }
    }
}
