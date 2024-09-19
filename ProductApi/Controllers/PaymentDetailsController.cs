using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailsController : ControllerBase
    {
        private readonly ProductContext _context;

        public PaymentDetailsController(ProductContext context)
        {
            _context = context;
        }

        // GET: api/PaymentDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDetails>>> GetPaymentDetails()
        {
            return await _context.PaymentDetails.ToListAsync();
        }

        // GET: api/PaymentDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetails>> GetPaymentDetails(int id)
        {
            var paymentDetails = await _context.PaymentDetails.FindAsync(id);

            if (paymentDetails == null)
            {
                return NotFound();
            }

            return paymentDetails;
        }

        // GET: api/PaymentDetails/byMethod/{paymentMethod}
        [HttpGet("byMethod/{paymentMethod}")]
        public async Task<ActionResult<IEnumerable<PaymentDetails>>> GetPaymentDetailsByMethod(string paymentMethod)
        {
            var paymentDetails = await _context.PaymentDetails
                .Where(pd => pd.PaymentMethod == paymentMethod)
                .ToListAsync();

            if (paymentDetails == null || !paymentDetails.Any())
            {
                return NotFound();
            }

            return paymentDetails;
        }

        // PUT: api/PaymentDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentDetails(int id, PaymentDetails paymentDetails)
        {
            if (id != paymentDetails.PaymentId)
            {
                return BadRequest();
            }

            _context.Entry(paymentDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentDetailsExists(id))
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

        // POST: api/PaymentDetails
        [HttpPost]
        public async Task<ActionResult<PaymentDetails>> PostPaymentDetails([FromBody] PaymentDetails paymentDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate fields based on payment method
            switch (paymentDetails.PaymentMethod)
            {
                case "CreditCard":
                    if (string.IsNullOrEmpty(paymentDetails.CardNumber) ||
                        string.IsNullOrEmpty(paymentDetails.CardName) ||
                        string.IsNullOrEmpty(paymentDetails.ExpiryDate) ||
                        string.IsNullOrEmpty(paymentDetails.CVV))
                    {
                        return BadRequest("Credit card details are required.");
                    }
                    break;
                case "PayPal":
                    if (string.IsNullOrEmpty(paymentDetails.PayPalEmail))
                    {
                        return BadRequest("PayPal email is required.");
                    }
                    break;
                case "NetBanking":
                    if (string.IsNullOrEmpty(paymentDetails.BankName) ||
                        string.IsNullOrEmpty(paymentDetails.AccountNumber) ||
                        string.IsNullOrEmpty(paymentDetails.IFSCCode))
                    {
                        return BadRequest("Net banking details are required.");
                    }
                    break;
                default:
                    return BadRequest("Invalid payment method.");
            }

            _context.PaymentDetails.Add(paymentDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPaymentDetails), new { id = paymentDetails.PaymentId }, paymentDetails);
        }

        // DELETE: api/PaymentDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentDetails(int id)
        {
            var paymentDetails = await _context.PaymentDetails.FindAsync(id);
            if (paymentDetails == null)
            {
                return NotFound();
            }

            _context.PaymentDetails.Remove(paymentDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentDetailsExists(int id)
        {
            return _context.PaymentDetails.Any(e => e.PaymentId == id);
        }

    }
}
