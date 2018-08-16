using FluentAssertions;
using jce.BackOffice.Controllers;
using jce.BusinessLayer.IManagers;
using jce.Common.Resources;
using jce.Common.Resources.Product;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestJCE.UnitTests.Goods
{
    public class ProductsControllerUnitTests
    {

        Mock<IProductManager> mockProductManager = new Mock<IProductManager>();

        [Fact]
        public async Task ProductUpdateKO()
        {

            var mockProductManager = new Mock<IProductManager>();

            mockProductManager.Setup(mgm => mgm.Update(1, new ProductSaveResource())).ReturnsAsync(() => new ProductResource
            {
            });

            var controller = new ProductController(mockProductManager.Object);
            var result = await controller.Update(1, new ProductSaveResource());
            var okResult = result.Should().BeOfType<NotFoundResult>().Subject;
            okResult.StatusCode.ShouldBe(404);
        }

        [Fact]
        public void ProductAddModelStateKO()
        {
            var newProduct = new ProductSaveResource
            {
                Title = "TestProduct",
                CreatedBy = "Test1",
                CreatedOn = DateTime.Now,
                Details = "TestTest",
                GoodDepartmentId = 1,
                IndexId = "A",
                IsBasicProduct = true,
                IsBatch = false,
                IsDiscountable = true,
                IsDisplayedOnJCE = true,
                IsEnabled = true,
                OriginId = 1,
                PintelSheetId = 1,
                ProductTypeId = 1,
                Price = 19.99,
                Season = "2018",
                SupplierId = 1,
                UpdatedBy = "",
                UpdatedOn = DateTime.Now
            };

            var controller = new ProductController(mockProductManager.Object);
            var context = new ValidationContext(newProduct);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(newProduct, context, results, true);

            isModelStateValid.ShouldBe(false);
        }

        [Fact]
        public async Task ProductAddOK()
        {
            var newProduct = new ProductSaveResource
            {
                Title = "TestProduct",
                CreatedBy = "Test1",
                CreatedOn = DateTime.Now,
                Details = "TestTest",
                GoodDepartmentId = 1,
                IndexId = "A",
                IsBasicProduct = true,
                IsBatch = false,
                IsDiscountable = true,
                IsDisplayedOnJCE = true,
                IsEnabled = true,
                OriginId = 1,
                PintelSheetId = 1,
                ProductTypeId = 1,
                Price = 19.99,
                RefPintel = null,
                Season = "2018",
                SupplierId = 1,
                UpdatedBy = "",
                UpdatedOn = DateTime.Now
            };

            mockProductManager.Setup(mgm => mgm.Add(newProduct)).ReturnsAsync(new ProductResource
            {
                Id = 1,
                Title = "TestProduct",
                CreatedBy = "Test1",
                CreatedOn = DateTime.Now,
                Details = "TestTest",
                GoodDepartmentId = 1,
                IndexId = "A",
                IsBasicProduct = true,
                IsBatch = false,
                IsDiscountable = true,
                IsDisplayedOnJCE = true,
                IsEnabled = true,
                OriginId = 1,
                PintelSheetId = 1,
                ProductTypeId = 1,
                Price = 19.99,
                RefPintel = "4444",
                Season = "2018",
                SupplierId = 1,
                UpdatedBy = "",
                UpdatedOn = DateTime.Now
            });

            var controller = new ProductController(mockProductManager.Object);
            var result = await controller.Add(newProduct);
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var product = okResult.Value.Should().BeAssignableTo<ProductResource>().Subject;

            product.Title.Should().Be("TestProduct");
        }

        [Fact]
        public async Task ProductUpdateOK()
        {
            var newProduct = new ProductSaveResource
            {
                Id = 1,
                Title = "TestProduct2",
                CreatedBy = "Test1",
                CreatedOn = DateTime.Now,
                Details = "TestTest",
                GoodDepartmentId = 1,
                IndexId = "A",
                IsBasicProduct = true,
                IsBatch = false,
                IsDiscountable = true,
                IsDisplayedOnJCE = true,
                IsEnabled = true,
                OriginId = 1,
                PintelSheetId = 1,
                ProductTypeId = 1,
                Price = 19.99,
                RefPintel = "4444",
                Season = "2018",
                SupplierId = 1,
                UpdatedBy = "",
                UpdatedOn = DateTime.Now
            };

            var mockProductManager = new Mock<IProductManager>();
            mockProductManager.Setup(mgm => mgm.Update(1, newProduct)).ReturnsAsync(() => new ProductResource
            {
                Id = 1,
                Title = "TestProduct2",
                CreatedBy = "Test1",
                CreatedOn = DateTime.Now,
                Details = "TestTest",
                GoodDepartmentId = 1,
                IndexId = "A",
                IsBasicProduct = true,
                IsBatch = false,
                IsDiscountable = true,
                IsDisplayedOnJCE = true,
                IsEnabled = true,
                OriginId = 1,
                PintelSheetId = 1,
                ProductTypeId = 1,
                Price = 19.99,
                RefPintel = "4444",
                Season = "2018",
                SupplierId = 1,
                UpdatedBy = "",
                UpdatedOn = DateTime.Now
            });

            var controller = new ProductController(mockProductManager.Object);
            var result = await controller.Update(1, newProduct);
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var product = okResult.Value.Should().BeAssignableTo<ProductResource>().Subject;

            product.Title.Should().Be("TestProduct2");
            product.Id.Should().Be(1);
        }
      
    }
}
