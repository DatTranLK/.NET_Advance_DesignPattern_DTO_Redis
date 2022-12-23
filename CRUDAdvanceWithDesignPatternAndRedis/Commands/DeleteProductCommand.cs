using DataAccessObject;
using MediatR;

namespace CRUDAdvanceWithDesignPatternAndRedis.Commands
{
    public class DeleteProductCommand : IRequest<ServiceResponse<string>>
    {
        public int Id { get; set; }
        public DeleteProductCommand(int id)
        {
            Id = id;
        }
    }
}
