namespace Reliance.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ParseConfigAttribute : Attribute
    {
        public string PartName { get; set; } = "";
        public string LineName { get; set; } = "";
    }
}
