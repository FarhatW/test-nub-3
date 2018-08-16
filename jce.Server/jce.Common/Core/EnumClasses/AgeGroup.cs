using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jce.Common.Core;

namespace jce.Common.Core.EnumClasses
{
    public class AgeGroup : EnumerationClass
    {
        public DateTime DateMin { get; set; }
        public DateTime DateMax { get; set; }

        public static AgeGroup ZeroToOneGroup { get; } = new AgeGroup(1, "0 à 1 An", SetDateMin(1), SetDateMax(1));
        public static AgeGroup TwoToThreeGroup { get; } = new AgeGroup(2, "2 à 3 Ans", SetDateMin(2), SetDateMax(2));
        public static AgeGroup FourToFiveGroup  = new AgeGroup(3, "4 à 5 Ans", SetDateMin(3), SetDateMax(3));
        public static AgeGroup SixToSevenGroup  = new AgeGroup(4, "6 à 7 Ans", SetDateMin(4), SetDateMax(4));
        public static AgeGroup EightToNineGroup  = new AgeGroup(5, "8 à 9 Ans", SetDateMin(5), SetDateMax(5));
        public static AgeGroup TenToElevenGroup = new AgeGroup(6, "10 à 11 Ans", SetDateMin(6), SetDateMax(6));
        public static AgeGroup MoreThanElevenGroup  = new AgeGroup(7, "Pour les grands", SetDateMin(7), SetDateMax(7));

        public AgeGroup() { }

        protected AgeGroup(int id, string name, DateTime dateMin, DateTime dateMax)
            :base(id, name)
        {
            DateMin = dateMin;
            DateMax = dateMax;
        }

        public static IEnumerable<AgeGroup> List()
        {
            return new[] { ZeroToOneGroup, TwoToThreeGroup, FourToFiveGroup, SixToSevenGroup, EightToNineGroup, TenToElevenGroup, MoreThanElevenGroup };
        }

        public static DateTime SetDateMax (int idGroup)
        {
            var dateKeyValuePair = new Dictionary<int, int>()
            {
                { 1, 0 }, { 2, 2 }, { 3, 4}, { 4, 6 }, { 5, 8} , { 6, 10}, { 7, 12}  
            };

            var year = (DateTime.Now.AddYears(-(dateKeyValuePair.FirstOrDefault(v => v.Key == idGroup).Value))).Year;
            var dateMax = new DateTime(year, 12, 31, 23, 59, 59);

            return dateMax;
        }

        public static DateTime SetDateMin(int idGroup)
        {
            var dateKeyValuePair = new Dictionary<int, int>()
            {
                { 1, 1 }, { 2, 4 }, { 3, 5 }, { 4, 7 }, { 5, 9 } , { 6, 11 }, { 7, 18 }
            };

            var year = (DateTime.Now.AddYears(- (dateKeyValuePair.FirstOrDefault(v => v.Key == idGroup).Value))).Year;
            var dateMin = new DateTime(year, 1, 1, 0, 0, 1);

            return dateMin;
        }

        public static int SetDateToInt(DateTime date)
        {
            int year = int.Parse((date.ToString("yyyy MMMM dd").Substring(0,4)));

            return year;
        }

        public static AgeGroup FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new ArgumentException($"Possible values for AgeGroup: {String.Join(",", List().Select(s => s.Name))}");
            }
            return state;
        }

        public static AgeGroup From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new ArgumentException($"Possible values for AgeGroup: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
