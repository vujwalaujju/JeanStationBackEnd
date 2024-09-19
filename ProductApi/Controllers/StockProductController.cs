using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockProductController : ControllerBase
    {
        private readonly ProductContext _context;

        public StockProductController(ProductContext context)
        {
            _context = context;
        }

        // GET: api/StockProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockProduct>>> GetStockProducts()
        {
            return await _context.StockProducts.ToListAsync();
        }
        [HttpGet("ByProductId/{productId}")]
        public async Task<ActionResult<IEnumerable<StockProduct>>> GetStockProductsByProductId(int productId)
        {
            var stockProducts = await _context.StockProducts
                                              .Where(sp => sp.ProductId == productId)
                                              .ToListAsync();

            if (stockProducts == null || stockProducts.Count == 0)
            {
                return NotFound();
            }

            return stockProducts;
        }



        // GET: api/StockProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockProduct>> GetStockProduct(int id)
        {
            var stockProduct = await _context.StockProducts.FindAsync(id);

            if (stockProduct == null)
            {
                return NotFound();
            }

            return stockProduct;
        }

        // PUT: api/StockProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockProduct(int id, StockProduct stockProduct)
        {
            if (id != stockProduct.Id)
            {
                return BadRequest();
            }

            _context.Entry(stockProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StockProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StockProduct>> PostStockProduct(StockProduct stockProduct)
        {
            _context.StockProducts.Add(stockProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStockProduct", new { id = stockProduct.Id }, stockProduct);
        }

        // DELETE: api/StockProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockProduct(int id)
        {
            var stockProduct = await _context.StockProducts.FindAsync(id);
            if (stockProduct == null)
            {
                return NotFound();
            }

            _context.StockProducts.Remove(stockProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockProductExists(int id)
        {
            return _context.StockProducts.Any(e => e.Id == id);
        }
        [HttpPut("ByProductId/{productId}")]
        public async Task<IActionResult> UpdateStockProduct(int productId, StockProduct stockProduct)
        {
            if (productId != stockProduct.ProductId)
            {
                return BadRequest("ProductId in the route does not match the ProductId in the body.");
            }

            _context.Entry(stockProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.StockProducts.Any(e => e.ProductId == productId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpPost("ByProductId/{productId}")]
        public async Task<ActionResult<StockProduct>> CreateStockProduct(int productId, StockProduct stockProduct)
        {
            if (productId != stockProduct.ProductId)
            {
                return BadRequest("ProductId in the route does not match the ProductId in the body.");
            }

            _context.StockProducts.Add(stockProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStockProductsByProductId), new { productId = stockProduct.ProductId }, stockProduct);
        }

    }
}
