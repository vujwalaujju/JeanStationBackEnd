using ProductApi.Models;

namespace ProductApi.Services
{
    public interface IProductService
    {
        bool AddProduct(Product product);
        bool DeleteProduct(int code);
        List<Product> GetProductById(int id);
        List<Product> GetProductsByDescription(string description);
        List<Product> GetProductsByGender(string gender);
        bool UpdateProduct(int id, Product p);
    }
}