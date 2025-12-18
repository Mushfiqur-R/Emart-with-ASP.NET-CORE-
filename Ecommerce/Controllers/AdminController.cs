using Ecommerce.Data;
using Ecommerce.DTOs;
using Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
            private readonly EcommerceDBContext _dbContext;
            public AdminController(EcommerceDBContext context) { 
                   _dbContext= context;
        }

        [HttpGet("catagory")]
        public IActionResult getall()
        {
            var allcatagory = _dbContext.Catagories.ToList();
            return Ok(allcatagory);
        }

        //[HttpGet("products")]
        //public IActionResult allproduct()
        //{
        //    var allcatagory = _dbContext.Products.Include(p => p.Catagory).ToList();
        //    return Ok(allcatagory);
        //}

        [HttpGet("getproducts")]
        public IActionResult getproducts()
        {
            var allcatagory = _dbContext.Products.Include(p => p.Catagory).Select(p=> new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity,
                CatagoryName=p.Catagory.Name

            });
            return Ok(allcatagory);
        }

        [HttpPost("createproduct")]
        public IActionResult createproduct(ProductDto product)
        {
            var catagory = _dbContext.Catagories.FirstOrDefault(c => c.Name == product.CatagoryName);
            if (catagory == null)
            {
                return BadRequest($"Catagory {product.Name} is not available");
            }

            var newproduct = new Product
            {
               
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                CatagoryId = catagory.Id,
            };
            _dbContext.Products.Add(newproduct);
            _dbContext.SaveChanges();

            return Ok(new
            {
                message = "Product creation successfully done.",
               
            });
        }



    }
}
