using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI.Data
{
    public interface IProductsRepository
    {
        List<Product> GetAllProducts();
        Product GetProduct(int Id);
        Product PostProduct(Product product);
        Product PutProduct(int id, Product product);
        bool DeleteProduct(int Id);
    }
}
