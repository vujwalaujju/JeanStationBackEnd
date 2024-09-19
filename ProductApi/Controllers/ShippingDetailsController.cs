using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingDetailsController : ControllerBase
    {
        private readonly ProductContext _context;

        public ShippingDetailsController(ProductContext context)
        {
            _context = context;
        }

        // GET: api/ShippingDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShippingDetails>>> GetShippingDetails()
        {
            return await _context.ShippingDetails.ToListAsync();
        }

        // GET: api/ShippingDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShippingDetails>> GetShippingDetails(int id)
        {
            var shippingDetails = await _context.ShippingDetails.FindAsync(id);

            if (shippingDetails == null)
            {
                return NotFound();
            }

            return shippingDetails;
        }

        // PUT: api/ShippingDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShippingDetails(int id, ShippingDetails shippingDetails)
        {
            if (id != shippingDetails.SID)
            {
                return BadRequest();
            }

            _context.Entry(shippingDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShippingDetailsExists(id))
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

        // POST: api/ShippingDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShippingDetails>> PostShippingDetails(ShippingDetails shippingDetails)
        {
            _context.ShippingDetails.Add(shippingDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShippingDetails", new { id = shippingDetails.SID }, shippingDetails);
        }

        // DELETE: api/ShippingDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShippingDetails(int id)
        {
            var shippingDetails = await _context.ShippingDetails.FindAsync(id);
            if (shippingDetails == null)
            {
                return NotFound();
            }

            _context.ShippingDetails.Remove(shippingDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShippingDetailsExists(int id)
        {
            return _context.ShippingDetails.Any(e => e.SID == id);
        }

    }
}
