using BusinessObject.Dtos;
using BusinessObject.Models;
using CRUDAdvanceWithDesignPatternAndRedis.Commands;
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
        /// <summary>
        /// GetProductById dùng redis hơi ngu nhỉ
        /// </summary>
        /// <remark>
        /// GetProductById dùng redis hơi ngu nhỉ
        /// </remark>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<ProductDTO>>> GetProductById(int id)
        {
            try
            {
                /*var cacheData = _cacheService.GetData<ServiceResponse<ProductDTO>>("product");
                if (cacheData != null)
                    return StatusCode((int)cacheData.StatusCode, cacheData);*/
                var query = new GetProductByIdQuery(id);
                var cacheData = await _mediator.Send(query);
                //set expiry time
                /*var expiryTime = DateTimeOffset.Now.AddSeconds(30);
                _cacheService.SetData<ServiceResponse<ProductDTO>>("product", cacheData, expiryTime);*/
                return StatusCode((int)cacheData.StatusCode, cacheData);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<string>>> DeleteProduct(int id)
        {
            try
            {
                var queryFind = new GetProductByIdQuery(id);
                var query = new DeleteProductCommand(id);
                var resFind = await _mediator.Send(queryFind);
                if (resFind.Data != null)
                { 
                    var res = await _mediator.Send(query);
                    _cacheService.RemoveData("product");
                    return StatusCode((int)res.StatusCode, res);
                }
                return NotFound();
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Product>>> AddProduct([FromBody] AddProductCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                //set expire time

                var expiryTime = DateTimeOffset.Now.AddSeconds(30);
                _cacheService.SetData<ServiceResponse<Product>>("product", result, expiryTime);
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
