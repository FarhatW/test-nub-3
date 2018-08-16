using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jce.Common.Core.EnumClasses
{
    public class Origin : EnumerationClass
    {
        public static Origin FranceOrigin { get; } = new Origin(1, "France");
        public static Origin EuropeOrigin { get; } = new Origin(2, "Europe");
        public static Origin OtherOrigin { get; } = new Origin(3, "Autres");

        public Origin(int id, string name)
            : base(id, name)
        {
            
        }

        public static IEnumerable<Origin> List()
        {
            return new[] { FranceOrigin, EuropeOrigin, OtherOrigin };
        }

        public static Origin FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new ArgumentException($"Possible values for Origin: {String.Join(",", List().Select(s => s.Name))}");
            }
            return state;
        }

        public static Origin From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new ArgumentException($"Possible values for Origin: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
