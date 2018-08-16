using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using jce.BusinessLayer.Core;
using jce.Common.Query;
using jce.Common.Resources;
using jce.Common.Resources.Product;

namespace jce.BusinessLayer.IManagers
{
    public interface IProductManager : IActionManager<ProductResource>
    {
        Task<ProductResource> GetItemByRefPintel(string refPintel);
        Task<ProductListResource> MultipleAdd(ProductSaveResource[] productSaveResourceArray);
        //Task<List<ProductResource>> GetListOfProduct(string[] refPintels);
    }
}
