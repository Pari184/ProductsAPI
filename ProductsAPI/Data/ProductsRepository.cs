using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI.Data
{
    public class ProductsRepository: IProductsRepository
    {
        private readonly ProductContext _context;
        public ProductsRepository(ProductContext context)
        {
            _context = context;
        }
        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }
        public Product GetProduct(int Id)
        {
            var product = _context.Products.Find(Id);
            return product;
        }
        public Product PostProduct(Product product)
        {
            var existingProduct = _context.Products.FirstOrDefault(p => p.Name == product.Name && p.Brand == product.Brand);
            if (existingProduct == null)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return product;
            }
            else return null;
            
        }
        public Product PutProduct(int id, Product product)
        {
            var productToEdit = _context.Products.Where(x => x.Id == id).FirstOrDefault();           
            productToEdit.Name = product.Name;
            productToEdit.Brand = product.Brand;
            productToEdit.Price = product.Price;
            
            _context.SaveChanges();
            return productToEdit;
        }
        public bool DeleteProduct(int Id)
        {
            var productToDelete = _context.Products.Where(x => x.Id == Id).FirstOrDefault();
            _context.Products.Remove(productToDelete);
            _context.SaveChanges();
            return true;
        }
    }
}
