namespace SAPB1.SLFramework.Abstractions.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class ValidValueAttribute(string value, string description) : Attribute
    {
        public string Value { get; } = value;
        public string Description { get; } = description;
    }
}
