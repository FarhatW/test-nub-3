namespace jce.Common.Resources.UserProfile
{
   public class JceProfileQueryResource : FilterResource
    {
        public int? UserType { get; set; }
        public bool? NoPageSize { get; set; }
        public string Search { get; set; }
    }
}
