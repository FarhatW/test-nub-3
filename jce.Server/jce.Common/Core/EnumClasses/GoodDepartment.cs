using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jce.Common.Core.EnumClasses
{
    public class GoodDepartment : EnumerationClass
    {
        public static GoodDepartment U1 { get; } = new GoodDepartment(1, "Premier Âge");
        public static GoodDepartment U2 { get; } = new GoodDepartment(2, "Bois");
        public static GoodDepartment U3 { get; } = new GoodDepartment(3, "Jeux Educatifs");
        public static GoodDepartment U4 { get; } = new GoodDepartment(4, "Filles");
        public static GoodDepartment U5 { get; } = new GoodDepartment(5, "Garçons");
        public static GoodDepartment U6 { get; } = new GoodDepartment(6, "Musique");
        public static GoodDepartment U7 { get; } = new GoodDepartment(7, "Jeux de société");
        public static GoodDepartment U8 { get; } = new GoodDepartment(8, "Création et découverte");
        public static GoodDepartment U9 { get; } = new GoodDepartment(9, "Multimédia");
        public static GoodDepartment U10 { get; } = new GoodDepartment(10, "Sport");
        public static GoodDepartment U11 { get; } = new GoodDepartment(11, "Cadeaux");


        public GoodDepartment(int id, string name)
            : base(id, name)
        {
            
        }

        private GoodDepartment()
        {

        }

        public static IEnumerable<GoodDepartment> List()
        {
            return new[] { U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11 };
        }

        public static GoodDepartment FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new ArgumentException($"Possible values for ToyDepartment: {String.Join(",", List().Select(s => s.Name))}");
            }
            return state;
        }

        public static GoodDepartment From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new ArgumentException($"Possible values for ToyDepartment: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

    }


}
