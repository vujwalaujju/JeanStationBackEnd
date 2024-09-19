using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Services;
namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service = new ProductService();


        // GET: api/Product
       
        [HttpGet("{storeId}")]
        public ActionResult<IEnumerable<Product>> GetProductsByStoreId(int storeId)
        {
            var products = _service.GetProductById(storeId);
            return Ok(products);
        }
        [HttpGet("gender/{gender}")]
        public ActionResult<IEnumerable<Product>> GetProductsByGender(string gender)
        {
            var products = _service.GetProductsByGender(gender);
            return Ok(products);
        }
        [HttpGet("description/{description}")]
        public ActionResult<IEnumerable<Product>> GetProductsByDescription(string description)
        {
            var products = _service.GetProductsByDescription(description);
            return Ok(products);
        }

        // POST api/Product

        [HttpPost()]
        public IActionResult Post([FromBody] Product product)
        {
            try
            {
                if (_service.AddProduct(product))
                {
                    return CreatedAtAction(nameof(GetProductsByStoreId), new { storeId = product.StoreId }, product);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

            // PUT api/Product/5
            [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product p)
        {
            if (_service.UpdateProduct(id, p))
            {
                return Ok(p);
            }
            return BadRequest();
        }

        // DELETE api/Product/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_service.DeleteProduct(id))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
