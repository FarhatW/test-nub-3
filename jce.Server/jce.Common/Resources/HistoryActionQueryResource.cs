using System;
using System.Collections.Generic;
using System.Text;

namespace jce.Common.Resources
{
    public class HistoryActionQueryResource : FilterResource
    {
        public string UserId { get; set; }
        public string ActionName { get; set; }
        public string TableName { get; set; }

        public string Date { get; set; }

    }
}
