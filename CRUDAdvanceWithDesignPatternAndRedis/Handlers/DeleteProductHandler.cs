using CRUDAdvanceWithDesignPatternAndRedis.Commands;
using DataAccessObject;
using MediatR;
using Repository;
using System.Threading;
using System.Threading.Tasks;

namespace CRUDAdvanceWithDesignPatternAndRedis.Handlers
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, ServiceResponse<string>>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ServiceResponse<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var res = await _productRepository.DeleteProduct(request.Id);
            return res;
        }
    }
}
