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
                
                var data = await _dbContext.Products.Include(x => x.Category).OrderByDescending(x => x.ProductId).ToListAsync();
                if (data != null)
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()
                                                                    .ForMember(dto => dto.CategoryName,
                                                                    act => act.MapFrom(obj => obj.Category.CategoryName)));
                    var mapper = new Mapper(config);
                    var dataDTO = mapper.Map<IEnumerable<ProductDTO>>(data);
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
        public async Task<ServiceResponse<ProductDTO>> GetProductById(int id)
        {
            try
            {
                var res = await _dbContext.Products
                    .Include(x => x.Category)
                    .FirstOrDefaultAsync(x => x.ProductId == id);
                if (res != null)
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()
                                                                    .ForMember(dto => dto.CategoryName, act => act.MapFrom(obj => obj.Category.CategoryName)));

                    var mapper = new Mapper(config);
                    var resDTO = mapper.Map<ProductDTO>(res);
                    return new ServiceResponse<ProductDTO>
                    { 
                        Data = resDTO,
                        Success = true,
                        Message = "Successfully",
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<ProductDTO>
                { 
                    Success = true,
                    Message = "Not Found",
                    StatusCode = 404
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<ServiceResponse<string>> DeleteProduct(int id)
        {
            try
            {
                var pro = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);
                if (pro != null)
                { 
                    _dbContext.Products.Remove(pro);
                    await _dbContext.SaveChangesAsync();
                    return new ServiceResponse<string>
                    {
                        Success = true,
                        Message = "Successfully",
                        StatusCode = 204
                    };
                }
                return new ServiceResponse<string>
                {
                    Success = true,
                    Message = "Not Found",
                    StatusCode = 404
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<ServiceResponse<Product>> AddNewProduct(Product product)
        {
            try
            {
                _dbContext.ChangeTracker.Clear();
                var data = await _dbContext.Products.AddAsync(product);
                await _dbContext.SaveChangesAsync();
                
                return new ServiceResponse<Product>
                { 
                    Data = data.Entity,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 201
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
