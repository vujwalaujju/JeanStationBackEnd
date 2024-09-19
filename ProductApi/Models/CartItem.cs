using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        //email property added by anil on 19th sep 
        public string Email {  get; set; }

    }
}
