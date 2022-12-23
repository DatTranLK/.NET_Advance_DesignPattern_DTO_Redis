using BusinessObject.Dtos;
using BusinessObject.Models;
using CRUDAdvanceWithDesignPatternAndRedis.Queries;
using DataAccessObject;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDAdvanceWithDesignPatternAndRedis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICacheService _cacheService;

        public ProductController(IMediator mediator, ICacheService cacheService)
        {
            _mediator = mediator;
            _cacheService = cacheService;
        }
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<ProductDTO>>>> GetProducts()
        {
            try
            {
                
                var cacheData = _cacheService.GetData<ServiceResponse<IEnumerable<ProductDTO>>>("products");
                if (cacheData != null && cacheData.Data.Count() > 0)
                    return Ok(cacheData);
                // using mediator and cqrs 
                var query = new GetProductsQuery();
                cacheData = await _mediator.Send(query);
                //set expiry time
                var expiryTime = DateTimeOffset.Now.AddSeconds(30);
                _cacheService.SetData<ServiceResponse<IEnumerable<ProductDTO>>>("products", cacheData, expiryTime);
                return Ok(cacheData);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
