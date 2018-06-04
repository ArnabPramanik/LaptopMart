using LaptopMart.ApplicationDb;
using LaptopMart.Implementations;
using LaptopMart.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
namespace LaptopMart.Tests.DALTests
{
    public class ProductRepositoryTest
    {
        private ProductRepository _repository;


        [TestInitialize]
        public void TestInitialize()
        {
            var mockProducts = new Mock<DbSet<Product>>();
            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.ProductTable).Returns(mockProducts.Object);
            _repository = new ProductRepository(mockContext.Object);
        }
    }

}
