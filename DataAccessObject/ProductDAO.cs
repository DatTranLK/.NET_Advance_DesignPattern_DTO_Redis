using AutoMapper;
using BusinessObject.Dtos;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class ProductDAO
    {
       private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        FStoreDBContext _dbContext = new FStoreDBContext();

        public ProductDAO()
        {

        }

        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }
        public async Task<ServiceResponse<IEnumerable<ProductDTO>>> GetProducts()
        {
            try
            {
                /*var cacheData = _cacheService.GetData<IEnumerable<ProductDTO>>("products");
                if (cacheData != null && cacheData.Count() > 0)
                    return new ServiceResponse<IEnumerable<ProductDTO>>
                    {
                        Data = cacheData,
                        Success = true,
                        Message = "Successfully",
                        StatusCode = 200
                    };*/
                var data = await _dbContext.Products.Include(x => x.Category).OrderByDescending(x => x.ProductId).ToListAsync();
                if (data != null)
                {
                    //using automapper
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()
                                                                    .ForMember(dto => dto.CategoryName,
                                                                    act => act.MapFrom(obj => obj.Category.CategoryName)));

                    var mapper = new Mapper(config);
                    var dataDTO = mapper.Map<IEnumerable<ProductDTO>>(data);

                    // continue caching
                    /*cacheData = dataDTO;*/
                    //set expiry time
                    /*var expiryTime = DateTimeOffset.Now.AddSeconds(30);
                    _cacheService.SetData<IEnumerable<ProductDTO>>("products", cacheData, expiryTime);*/
                    return new ServiceResponse<IEnumerable<ProductDTO>>
                    {
                        Data = dataDTO,
                        Success = true,
                        Message = "Successfully",
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<ProductDTO>>
                {
                    Success = true,
                    Message = "No rows",
                    StatusCode = 200
                };

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
