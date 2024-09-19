namespace ProductApi.Models
{
    public class StockProduct
    {
       public int Id { get; set; }
        
        public string Colour { get; set; }
        public int S { get; set; }
        public int M { get; set; }
        public int L { get; set; }
        public int Xl { get; set; }
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }

    }
}
