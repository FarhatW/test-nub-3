using FluentAssertions;
using jce.BackOffice;
using jce.BackOffice.Controllers;
using jce.Common.Entites;
using jce.Common.Entites.JceDbContext;
using jce.Common.Extentions;
using jce.Common.Resources;
using jce.Common.Resources.Good;
using jce.Common.Resources.Product;
using jce.DataAccess.Core.dbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using RestEase;
using Shouldly;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestJCE.IntegrationTests.Tests
{
    public class GoodsControllerIntegrationTests 
    {
        private readonly JceDbContext _context;
        private readonly HttpClient _client;

        public GoodsControllerIntegrationTests()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Test")
                .UseStartup<TestStartupLocalDb>();

            var server = new TestServer(builder);

            _context = server.Host.Services.GetService(typeof(JceDbContext)) as JceDbContext;
            _client = server.CreateClient();

            SeedGoods();
        }


        [Fact]
        public void SeedGoods()
        {
            var newProduct1 = new Product
            {
                Id = 4,
                Title = "TestProduct",
                CreatedBy = "Test1",
                CreatedOn = DateTime.Now,
                Details = "TestTest",
                GoodDepartmentId = 1,
                File = "/test.pdf",
                IndexId = "A",
                IsBasicProduct = true,
                IsDiscountable = true,
                IsDisplayedOnJCE = true,
                IsEnabled = true,
                OriginId = 1,
                PintelSheetId = 1,
                ProductTypeId = 1,
                Price = 19.99,
                RefPintel = "1234",
                Season = "2018",
                SupplierId = 1,
                UpdatedBy = "",
                UpdatedOn = DateTime.Now
            };

            var newProduct2 = new Product
            {
                Id = 2,
                Title = "TestProduct2",
                CreatedBy = "Test2",
                CreatedOn = DateTime.Now,
                Details = "TestTest",
                GoodDepartmentId = 1,
                File = "/test.pdf",
                IndexId = "C",
                IsBasicProduct = true,
                IsDiscountable = true,
                IsDisplayedOnJCE = true,
                IsEnabled = true,
                OriginId = 1,
                PintelSheetId = 1,
                ProductTypeId = 1,
                Price = 19.99,
                RefPintel = "4567",
                Season = "2018",
                SupplierId = 1,
                UpdatedBy = "",
                UpdatedOn = DateTime.Now
            };

            var newProduct3 = new Product
            {
                Id = 3,
                Title = "TestProduct3",
                CreatedBy = "Test3",
                CreatedOn = DateTime.Now,
                Details = "TestTest",
                GoodDepartmentId = 1,
                File = "/test.pdf",
                IndexId = "B",
                IsBasicProduct = true,
                IsDiscountable = true,
                IsDisplayedOnJCE = true,
                IsEnabled = true,
                OriginId = 1,
                PintelSheetId = 1,
                ProductTypeId = 1,
                Price = 19.99,
                RefPintel = "4321",
                Season = "2018",
                SupplierId = 1,
                UpdatedBy = "",
                UpdatedOn = DateTime.Now
            };

            var newProduct4 = new Product
            {
                Id = 5,
                Title = "Ftest",
                CreatedBy = "Test1",
                CreatedOn = DateTime.Now,
                Details = "TestTest",
                GoodDepartmentId = 1,
                File = "/test.pdf",
                IndexId = "A",
                IsBasicProduct = true,
                IsDiscountable = true,
                IsDisplayedOnJCE = true,
                IsEnabled = true,
                OriginId = 1,
                PintelSheetId = 1,
                ProductTypeId = 1,
                Price = 19.99,
                RefPintel = "1234",
                Season = "2018",
                SupplierId = 1,
                UpdatedBy = "",
                UpdatedOn = DateTime.Now
            };


            _context.Products.AddRange(newProduct1, newProduct2, newProduct3, newProduct4);
            _context.SaveChanges();

        }

        [Fact]
        public void DeleteDatabase()
        {
            foreach(var entity in _context.Products)
            {
                _context.Products.Remove(entity);
            }

            _context.SaveChanges();
        }


        [Fact]
        public async Task GoodsGetAll()
        {
            var response = await _client.GetAsync($"/api/goods?&pagesize=10&page=1");
            response.EnsureSuccessStatusCode();
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var result = await response.Content.ReadAsStringAsync();
            var goods = JsonConvert.DeserializeObject<QueryResult<GoodResource>>(result);

            goods.Items.Count().ShouldBe(4);
            goods.TotalItems.ShouldBe(4);

            DeleteDatabase();

        }

        [Fact]
        public async Task Good_Get_Specific()
        {
            var response = await _client.GetAsync($"/api/goods/4");
            response.EnsureSuccessStatusCode();
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var result = await response.Content.ReadAsStringAsync();
            var good = JsonConvert.DeserializeObject<GoodResource>(result);

            good.Id.ShouldBe(4);

            DeleteDatabase();

        }

        [Fact]
        public async Task ProductAddValid()
        {
            var newProduct = new ProductSaveResource
            {
                Title = "TestProduct4555",
                CreatedBy = "Test45",
                CreatedOn = DateTime.Now,
                Details = "TestTestsss",
                GoodDepartmentId = 1,
                IsBatch =  false,
                IndexId = "C",
                IsBasicProduct = true,
                IsDiscountable = false,
                IsDisplayedOnJCE = true,
                IsEnabled = true,
                OriginId = 1,
                PintelSheetId = 1,
                ProductTypeId = 1,
                Price = 19.99,
                RefPintel = "6498",
                Season = "2018",
                SupplierId = 1,
                UpdatedBy = "",
                UpdatedOn = DateTime.Now
            };

            string stringData = JsonConvert.SerializeObject(newProduct);
            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"/api/products/", contentData);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            response.EnsureSuccessStatusCode();
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var result = await response.Content.ReadAsStringAsync();
            var good = JsonConvert.DeserializeObject<ProductResource>(result);

            good.Title.ShouldBe("TestProduct4555");

            DeleteDatabase();

        }

        [Fact]
        public async Task EditProductValid()
        {
            var editedProduct = new ProductSaveResource
            {
                Id = 4,
                Title = "TestProductModified",
                CreatedBy = "Test1",
                CreatedOn = DateTime.Now,
                Details = "TestTest",
                GoodDepartmentId = 1,
                IndexId = "A",
                IsBasicProduct = true,
                IsDiscountable = true,
                IsDisplayedOnJCE = true,
                IsEnabled = true,
                OriginId = 1,
                PintelSheetId = 1,
                ProductTypeId = 1,
                Price = 19.99,
                RefPintel = "1234",
                Season = "2018",
                SupplierId = 1,
                UpdatedBy = "",
                UpdatedOn = DateTime.Now
            };

            string stringData = JsonConvert.SerializeObject(editedProduct);
            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"/api/products/4", contentData);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            response.EnsureSuccessStatusCode();
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var result = await response.Content.ReadAsStringAsync();
            var good = JsonConvert.DeserializeObject<ProductResource>(result);

            good.Title.ShouldBe("TestProductModified");

            DeleteDatabase();

        }
    }
}
