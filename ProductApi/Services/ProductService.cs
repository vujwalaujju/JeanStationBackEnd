using ProductApi.Models;

namespace ProductApi.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductContext _context = new ProductContext();


        public bool AddProduct(Product product)
        {
            var storeExists = _context.Stores.Any(s => s.Id == product.StoreId);
            if (!storeExists)
            {
                throw new Exception("StoreId does not exist.");
            }

            _context.Products.Add(product);
            _context.SaveChanges();
            return true;
        }
        public bool UpdateProduct(int id, Product p)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                product.Name = p.Name;
                product.Price = p.Price;
                product.Image = p.Image;
                product.Description = p.Description;
                product.Gender = p.Gender;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool DeleteProduct(int code)
        {
            var product = _context.Products.Find(code);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public List<Product> GetProductById(int id)
        {
            return _context.Products.Where(p => p.StoreId == id).ToList();

        }
        public List<Product> GetProductsByGender(string gender)
        {
            return _context.Products.Where(p => p.Gender == gender).ToList();
        }

        public List<Product> GetProductsByDescription(string description)
        {
            return _context.Products.Where(p => p.Description.Contains(description) || p.Name.Contains(description)).ToList();
        }
    }
}
