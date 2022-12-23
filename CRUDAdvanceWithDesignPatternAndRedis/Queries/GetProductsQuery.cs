using BusinessObject.Dtos;
using BusinessObject.Models;
using DataAccessObject;
using MediatR;
using System.Collections.Generic;

namespace CRUDAdvanceWithDesignPatternAndRedis.Queries
{
    public class GetProductsQuery : IRequest<ServiceResponse<IEnumerable<ProductDTO>>>
    {
    }
}
