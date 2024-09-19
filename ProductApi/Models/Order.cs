namespace ProductApi.Models
{
    public class Order
    {
        public int? Id { get; set; }
        public string Email { get; set; }
        public double GrossAmount {  get; set; }
        public DateTime? OrderDate { get; set; }= DateTime.Now;
        public virtual ICollection<OrderItems>? OrderItems { get; set; }
    }
}
