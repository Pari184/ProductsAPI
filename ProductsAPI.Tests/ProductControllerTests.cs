using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductsAPI.Controllers;
using ProductsAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAPI.Tests
{
    [TestClass]
    public class ProductControllerTests
    {
        private Mock<IProductsRepository> _productRepoMock;
        private Mock<ILogger<ProductsController>> _loggerMock;
        private ProductsController _controller;
        public ProductControllerTests()
        {
            _productRepoMock = new Mock<IProductsRepository>();
            _loggerMock = new Mock<ILogger<ProductsController>>();
        }
        [TestMethod]
        public async Task GetProducts_ReturnOk()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Brand = "Brand 1", Price = 10.0m },
                new Product { Id = 2, Name = "Product 2", Brand = "Brand 2", Price = 20.0m }
            };

            _productRepoMock.Setup(repo => repo.GetAllProducts()).Returns(products);

            _controller = new ProductsController(_productRepoMock.Object, _loggerMock.Object);

            var result = await _controller.GetProducts();
            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public async Task Get_Product_ThrowException()
        {
            _productRepoMock.Setup(repo => repo.GetAllProducts()).Throws(new Exception());
            _controller = new ProductsController(_productRepoMock.Object, _loggerMock.Object);

            var result = await _controller.GetProducts();
            var obj = result as ObjectResult;

            Assert.AreEqual(400, obj.StatusCode);
        }
        [TestMethod]
        public async Task Get_Product_ById_ReturnOk()
        {
            var product = new Product { Id = 1, Name = "Product 1", Brand = "Brand 1", Price = 10.0m };
            _productRepoMock.Setup(repo => repo.GetProduct(It.IsAny<int>())).Returns(product);

            _controller = new ProductsController(_productRepoMock.Object, _loggerMock.Object);

            var result = await _controller.GetProduct(1);
            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj.StatusCode);
        }
        [TestMethod]
        public async Task Post_Product_ReturnOk()
        {
            var product = new Product { Id = 4, Name = "Product 3", Brand = "Brand 3", Price = 30.30m };
            _productRepoMock.Setup(repo => repo.PostProduct(It.IsAny<Product>())).Returns(product);
            _controller = new ProductsController(_productRepoMock.Object, _loggerMock.Object);

            var result = await _controller.PostProduct(product);
            var obj = result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }
        [TestMethod]
        public async Task Put_Product_ReturnOk()
        {
            int productId = 5;
            var product = new Product { Id = 5, Name = "Product new3", Brand = "Brand new3", Price = 30.00m };
            _productRepoMock.Setup(repo => repo.PutProduct(It.IsAny<int>(), It.IsAny<Product>())).Returns(product);
            _controller = new ProductsController(_productRepoMock.Object, _loggerMock.Object);

            var result = await _controller.PutProduct(productId, product);
            var obj = result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }
        [TestMethod]
        public async Task Delete_Product_ReturnOk()
        {
            int productId = 5;
            _productRepoMock.Setup(repo => repo.DeleteProduct(It.IsAny<int>())).Returns(true);
            _controller = new ProductsController(_productRepoMock.Object, _loggerMock.Object);

            var result = await _controller.DeleteProduct(productId);
            var obj = result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }
    }
}
