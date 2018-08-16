using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace jce.Common.Resources.Batch
{
    public class BatchListResource
    {
        public ICollection<BatchResource> Batches { get; set; }
        public int AddedBatchCount { get; set; }
        public int NotAddedBatchCount { get; set; }
        public List<string> DuplicatedRefList { get; set; }
        public List<KeyValuePair<string, string>> NonExistentProducts { get; set; }

        public BatchListResource()
        {
            DuplicatedRefList = new List<string>();
            Batches = new Collection<BatchResource>();
            NonExistentProducts = new List<KeyValuePair<string, string>>();
        }
    }
}
