using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

      
            private readonly ProductContext _context;

            public CartController(ProductContext context)
            {
                _context = context;
            }

            // GET: api/Cart
            [HttpGet]
            public async Task<ActionResult<IEnumerable<CartItem>>> GetCartItem()
            {
                return await _context.CartItem.ToListAsync();
            }

            // GET: api/Cart/5
            [HttpGet("{email}")]
            public async Task<ActionResult<List<CartItem>>> GetCartItem(string email)
            {
                var cartItem = await _context.CartItem.Where(c=>c.Email == email).ToListAsync();

                if (cartItem == null)
                {
                    return NotFound();
                }

                return cartItem;
            }

            // PUT: api/Cart/UpdateQuantity/5
            [HttpPut("UpdateQuantity/{id}")]
            public async Task<IActionResult> UpdateQuantity(int id, [FromBody] Payload quantity)
            {
                var cartItem = await _context.CartItem.FirstOrDefaultAsync(item => item.Id == id);

                if (cartItem == null)
                {
                    return NotFound();
                }

                // Update the quantity and total price
                cartItem.Quantity = quantity.quantity;
                cartItem.TotalPrice = cartItem.Price * quantity.quantity;

                _context.Entry(cartItem).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!CartItemExists(id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }


                var updatedCartItems = await _context.CartItem.ToListAsync();
                return Ok(updatedCartItems);
            }

            // POST: api/Cart
            [HttpPost]
            public async Task<ActionResult<CartItem>> PostCartItem(CartItem cartItem)
            {

                var existingItem = await _context.CartItem
           .FirstOrDefaultAsync(item => item.Size == cartItem.Size
                                     && item.Color == cartItem.Color
                                     && item.Image == cartItem.Image
                                     //added by anil on sep19th
                                     && item.Email == cartItem.Email);

                // Calculate the total price

                if (existingItem != null)
                {
                    existingItem.Quantity = existingItem.Quantity + 1;
                    existingItem.TotalPrice = existingItem.Quantity * cartItem.Price;
                    _context.CartItem.Update(existingItem);
                    await _context.SaveChangesAsync();

                    var newItems = await _context.CartItem.ToListAsync();
                    return Ok(newItems);


                }

                else
                {
                    cartItem.TotalPrice = cartItem.Quantity * cartItem.Price;

                    _context.CartItem.Add(cartItem);
                    await _context.SaveChangesAsync();

                    var updatedCartItems = await _context.CartItem.ToListAsync();
                    return Ok(updatedCartItems);
                }
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateCartItem(int id, CartItem updatedCartItem)
            {
                if (id != updatedCartItem.Id)
                {
                    return BadRequest();
                }

                var cartItem = await _context.CartItem.FindAsync(id);
                if (cartItem == null)
                {
                    return NotFound();
                }

                // Update the quantity and total price
                cartItem.Quantity = updatedCartItem.Quantity;
                cartItem.TotalPrice = cartItem.Quantity * cartItem.Price;

                _context.Entry(cartItem).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // if (!CartItemExists(id))
                    //{
                    // return NotFound();
                    // }
                    // else
                    // {
                    //   throw;
                    // }
                }

                var updatedCartItems = await _context.CartItem.ToListAsync();
                return Ok(updatedCartItems);
            }

            private bool CartItemExists(int id)
            {
                return _context.CartItem.Any(e => e.Id == id);
            }


            // DELETE: api/Cart/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteCartItem(int id)
            {
                var cartItem = await _context.CartItem.FirstOrDefaultAsync(ci => ci.Id == id);
                if (cartItem == null)
                {
                    return NotFound();
                }

                _context.CartItem.Remove(cartItem);
                await _context.SaveChangesAsync();

                var updatedCartItems = await _context.CartItem.ToListAsync();
                return Ok(updatedCartItems);
            }


        }
    }
