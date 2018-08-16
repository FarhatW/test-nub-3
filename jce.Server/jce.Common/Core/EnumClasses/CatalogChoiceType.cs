using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jce.Common.Core.EnumClasses
{
    public class CatalogChoiceType : EnumerationClass
    {
        protected CatalogChoiceType(int id, string name)
            : base(id, name)
        {
        }

        public static CatalogChoiceType UniqueChoice { get;  } = new CatalogChoiceType(1, "Choix Unique");
        public static CatalogChoiceType MultipleChoice { get;  } = new CatalogChoiceType(2, "Choix Multiples");
        public static CatalogChoiceType CumulatedProductChoice { get;  } = new CatalogChoiceType(3, "Choix Cumulés");

        public static IEnumerable<CatalogChoiceType> List()
        {
            return new[] { UniqueChoice, MultipleChoice, CumulatedProductChoice };
        }

        public static CatalogChoiceType FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new ArgumentException($"Possible values for CatalogChoiceType: {String.Join(",", List().Select(s => s.Name))}");
            }
            return state;
        }

        public static CatalogChoiceType From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new ArgumentException($"Possible values for CatalogChoiceType: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
