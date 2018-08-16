using jce.Common.Entites;
using jce.Common.Entites.JceDbContext;
using jce.Common.Resources.Product;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TestJCE.IntegrationTests.Goods
{
    public class TestDataGenerator : IEnumerable<object[]>
    {
        public static IEnumerable<object[]> GetGoodsFromDataGenerator()
        {
            yield return new object[]
            {
                new Product
                {
                        Id = 1,
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
                        RefPintel = "4444",
                        Season = "2018",
                        SupplierId = 1,
                        UpdatedBy = "",
                        UpdatedOn = DateTime.Now
                    },
                    new Product
                    {
                        Id = 2,
                        Title = "TestProduct2",
                        CreatedBy = "Test2",
                        CreatedOn = DateTime.Now,
                        Details = "TestTest2",
                        GoodDepartmentId = 2,
                        File = "/test.pdf",
                        IndexId = "A",
                        IsBasicProduct = true,
                        IsDiscountable = true,
                        IsDisplayedOnJCE = true,
                        IsEnabled = true,
                        OriginId = 1,
                        PintelSheetId = 1,
                        ProductTypeId = 1,
                        Price = 99.99,
                        RefPintel = "5544",
                        Season = "2018",
                        SupplierId = 1,
                        UpdatedBy = "",
                        UpdatedOn = DateTime.Now
                    }
            };

            yield return new object[]
            {
                new Product
                {
                        Id = 3,
                        Title = "ATest",
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
                        RefPintel = "4444",
                        Season = "2018",
                        SupplierId = 1,
                        UpdatedBy = "",
                        UpdatedOn = DateTime.Now
                    },
                    new Product
                    {
                        Id = 4,
                        Title = "BTest",
                        CreatedBy = "Test2",
                        CreatedOn = DateTime.Now,
                        Details = "TestTest2",
                        GoodDepartmentId = 2,
                        File = "/test.pdf",
                        IndexId = "A",
                        IsBasicProduct = true,
                        IsDiscountable = true,
                        IsDisplayedOnJCE = true,
                        IsEnabled = true,
                        OriginId = 1,
                        PintelSheetId = 1,
                        ProductTypeId = 1,
                        Price = 99.99,
                        RefPintel = "5544",
                        Season = "2018",
                        SupplierId = 1,
                        UpdatedBy = "",
                        UpdatedOn = DateTime.Now
                    }
            };
        }

        public static IEnumerable<object[]> GetGoodsResourceFromDataGenerator()
        {
            yield return new object[]
            {
                new ProductResource
                {
                        Id = 1,
                        Title = "TestProduct",
                        CreatedBy = "Test1",
                        CreatedOn = DateTime.Now,
                        Details = "TestTest",
                        GoodDepartmentId = 1,
                        IndexId = "A",
                        IsBasicProduct = true,
                        IsDiscountable = true,
                        IsDisplayedOnJCE = true,
                        IsBatch = false,
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
                    },
                    new ProductResource
                    {
                        Id = 2,
                        Title = "TestProduct2",
                        CreatedBy = "Test2",
                        CreatedOn = DateTime.Now,
                        Details = "TestTest2",
                        GoodDepartmentId = 2,
                        IndexId = "A",
                        IsBasicProduct = true,
                        IsDiscountable = true,
                        IsBatch = false,

                        IsDisplayedOnJCE = true,
                        IsEnabled = true,
                        OriginId = 1,
                        PintelSheetId = 1,
                        ProductTypeId = 1,
                        Price = 99.99,
                        RefPintel = "5544",
                        Season = "2018",
                        SupplierId = 1,
                        UpdatedBy = "",
                        UpdatedOn = DateTime.Now
                    }
            };

            yield return new object[]
            {
                new ProductResource
                {
                        Id = 3,
                        Title = "ATest",
                        CreatedBy = "Test1",
                        CreatedOn = DateTime.Now,
                        Details = "TestTest",
                        GoodDepartmentId = 1,
                        IsBatch = false,

                        IndexId = "A",
                        IsBasicProduct = true,
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
                    },
                    new ProductResource
                    {
                        Id = 4,
                        Title = "BTest",
                        CreatedBy = "Test2",
                        CreatedOn = DateTime.Now,
                        Details = "TestTest2",
                        GoodDepartmentId = 2,
                        IndexId = "A",
                        IsBasicProduct = true,
                        IsDiscountable = true,
                        IsDisplayedOnJCE = true,
                        IsBatch = false,
                        IsEnabled = true,
                        OriginId = 1,
                        PintelSheetId = 1,
                        ProductTypeId = 1,
                        Price = 99.99,
                        RefPintel = "5544",
                        Season = "2018",
                        SupplierId = 1,
                        UpdatedBy = "",
                        UpdatedOn = DateTime.Now
                    }
            };
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
