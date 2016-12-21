namespace Objects.Generator.Core.Configuration.Collections
{
    using System.Configuration;
    using Objects.Generator.Core.Configuration.Elements;

    public sealed class PrivateMethodsElementCollection : ConfigurationElementCollection
    {

        public PrivateMethodElement this[int i]
        {
            get { return ((PrivateMethodElement)(BaseGet(i))); }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new PrivateMethodElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((PrivateMethodElement)(element)).Id;
        }

    }

}
