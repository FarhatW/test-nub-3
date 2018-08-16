using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jce.Common.Core.EnumClasses
{
    public class LetterIndex : EnumerationClass
    {
        public string DisplayRange { get; set; }
        public double PriceRangeMin { get; set; }
        public double PriceRangeMax { get; set; }

        protected LetterIndex(int id, string name, string displayRange, double priceRangeMin, double priceRangeMax)
            : base(id, name)
        {
            DisplayRange = displayRange;
            PriceRangeMin = priceRangeMin;
            PriceRangeMax = priceRangeMax;
        }

        public static LetterIndex A { get; } = new LetterIndex(1, "A", "Jusqu'à 12 €", 0, 12);
        public static LetterIndex B { get; } = new LetterIndex(2, "B", "De 12,01 € à 15 €", 12.01, 15);
        public static LetterIndex C { get; } = new LetterIndex(3, "C", "De 15,01 € à 20 €", 15.01, 20);
        public static LetterIndex D { get; } = new LetterIndex(4, "D", "De 20,01 € à 25 €", 20.01, 25);
        public static LetterIndex E { get; } = new LetterIndex(5, "E", "De 25,01 € à 30 €", 25.01, 30);
        public static LetterIndex F { get; } = new LetterIndex(6, "F", "De 30,05 € à 40 €", 30.01, 40);
        public static LetterIndex G { get; } = new LetterIndex(7, "G", "De 40,01 € à 50 €", 40.01, 50);
        public static LetterIndex H { get; } = new LetterIndex(8, "H", "De 50,01 à 75 € ", 50.01, 75);
        public static LetterIndex I { get; } = new LetterIndex(9, "I", "De 75,01 € à 100 €",75.01, 100);
        public static LetterIndex J { get; } = new LetterIndex(10, "J", "Plus de 100, 01 €", 100.01, 50000);
        public static LetterIndex L { get; } = new LetterIndex(10, "L", "Lots masqués", 51000, 55000);

        public static IEnumerable<LetterIndex> List()
        {
            return new[] { A, B, C, D, E, F, G, H, I, J, L };
        }

        public static LetterIndex FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new ArgumentException($"Possible values for LetterIndex: {String.Join(",", List().Select(s => s.Name))}");
            }
            return state;
        }

        public static LetterIndex From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new ArgumentException($"Possible values for LetterIndex: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
