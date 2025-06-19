namespace SAPB1.SLFramework.Abstractions.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ServiceLayerResourcePathAttribute : Attribute
    {
        public string ResourcePath { get; }

        public ServiceLayerResourcePathAttribute(string resourcePath)
        {
            ResourcePath = resourcePath;
        }
    }
}
