using BusinessObject.Dtos;
using BusinessObject.Models;
using DataAccessObject;
using MediatR;

namespace CRUDAdvanceWithDesignPatternAndRedis.Commands
{
    public class AddProductCommand : IRequest<ServiceResponse<Product>>
    {
        public Product Product{ get; set; }
    }
}
