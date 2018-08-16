using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jce.BusinessLayer.IEnumManagers;
using jce.Common.Core;
using jce.Common.Core.EnumClasses;

namespace Managers
{
    public class LetterIndexManager : ILetterIndexManager
    {
        public List<LetterIndex> GetAll()
        {
            return LetterIndex.List().ToList();
        }

        public LetterIndex GetItemById(int id)
        {
            return LetterIndex.From(id);
        }

        public LetterIndex GetItemByName(string letter)
        {
            return LetterIndex.FromName(letter);
        }
    }
}
