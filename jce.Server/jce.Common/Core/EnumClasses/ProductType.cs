using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jce.Common.Core.EnumClasses
{
    public class ProductType : EnumerationClass
    {
        public static ProductType ProductTypeProduct { get; } = new ProductType(1, "Jouet");
        public static ProductType ProductTypeBook { get; } = new ProductType(2, "Livre");
        public static ProductType ProductTypeTicket { get; } = new ProductType(3, "Billet");

        public ProductType(int id, string name)
            : base(id, name)
        {

        }

        private ProductType()
        {

        }

        public static IEnumerable<ProductType> List()
        {
            return new[] { ProductTypeProduct, ProductTypeBook, ProductTypeTicket};
        }

        public static ProductType FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new ArgumentException($"Possible values for ToyType: {String.Join(",", List().Select(s => s.Name))}");
            }
            return state;
        }

        public static ProductType From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new ArgumentException($"Possible values for ToyType: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

    }

   
}
