using BusinessObject.Dtos;
using CRUDAdvanceWithDesignPatternAndRedis.Queries;
using DataAccessObject;
using MediatR;
using Repository;
using System.Threading;
using System.Threading.Tasks;

namespace CRUDAdvanceWithDesignPatternAndRedis.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ServiceResponse<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ServiceResponse<ProductDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await _productRepository.GetProductById(request.Id);
            return res;
        }
    }
}
