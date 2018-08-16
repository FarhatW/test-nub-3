using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jce.Common.Core.EnumClasses
{
    public class CatalogType : EnumerationClass
    {

        protected CatalogType(int id, string name)
            : base(id, name)
        {
        }

        public static CatalogType MiniCat { get; } = new CatalogType(1, "Mini Catalogue");
        public static CatalogType Letters { get; } = new CatalogType(2, "Lettres");
        public static CatalogType PintelSheets { get; } = new CatalogType(3, "Fiches de Collectivités");

        public static IEnumerable<CatalogType> List()
        {
            return new[] { MiniCat, Letters, PintelSheets };
        }

        public static CatalogType FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new ArgumentException($"Possible values for CatalogType: {String.Join(",", List().Select(s => s.Name))}");
            }
            return state;
        }

        public static CatalogType From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new ArgumentException($"Possible values for CatalogType: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }


    }

}
