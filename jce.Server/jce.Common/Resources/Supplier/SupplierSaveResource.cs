using jce.Common.Core;

namespace jce.Common.Resources.Supplier
{
    public class SupplierSaveResource : ResourceEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsEnabled { get; set; }
        public string SupplierRef { get; set; }
    }
}
