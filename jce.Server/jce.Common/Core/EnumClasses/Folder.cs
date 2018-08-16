using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jce.Common.Core.EnumClasses
{
    public class Folder : EnumerationClass
    {
        public List<string> AcceptedMimes { get; set; }
        public int MaxFileSize { get; set; }


        public static Folder FilesFolder = new Folder(1, "Fichiers", FilesFolderMimes(), 2000);
        public static Folder PicturesFolder = new Folder(1, "Images", PicturesFolderMimes(), 300);

        public Folder()
        {

        }


        protected Folder(int id, string name, List<string> acceptedMimes, int maxFileSize)
            : base(id, name)
        {
            AcceptedMimes = acceptedMimes;
            MaxFileSize = maxFileSize;
        }

        public static IEnumerable<Folder> List()
        {
            return new[] { FilesFolder, PicturesFolder };
        }

        public static List<string> FilesFolderMimes()
        {
            return new List<string>
            {
                "application/pdf"
            };
        }

        public static List<string> PicturesFolderMimes()
        {
            return new List<string>
            {
              "image/gif",
              "image/jpeg",
              "image/pjpeg",
              "image/png",
            };
        }

        public static Folder FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new ArgumentException($"Possible values for Folder: {String.Join(",", List().Select(s => s.Name))}");
            }
            return state;
        }

        public static Folder From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new ArgumentException($"Possible values for Folder: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
