using BusinessObject.Dtos;
using DataAccessObject;
using MediatR;

namespace CRUDAdvanceWithDesignPatternAndRedis.Queries
{
    public class GetProductByIdQuery :IRequest<ServiceResponse<ProductDTO>>
    {
        public int Id { get; set; }
        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }
}
