using jce.Common.Core;
using jce.Common.Resources.Product;

namespace jce.Common.Resources.Command
{
    public class CommandChildProductResource : ResourceEntity
    {
        public int CommandId { get; set; }
        public int ChildId { get; set; }
        public int ProductId { get; set; }
        public int OvertakeCommandChild { get; set; }

        public virtual CommandResource Command { get; set; }
        public virtual ChildResource Child { get; set; }
        public virtual ProductResource Product { get; set; }
    }
}
