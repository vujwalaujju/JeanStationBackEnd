namespace ProductApi.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Product>Products { get; set; }


    }
}
