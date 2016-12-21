namespace Objects.Generator.Core.Configuration.Collections
{
    using System.Configuration;
    using Objects.Generator.Core.Configuration.Elements;

    public sealed class PublicMethodsElementCollection : ConfigurationElementCollection
    {

        public PublicMethodElement this[int i]
        {
            get { return ((PublicMethodElement)(BaseGet(i))); }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new PublicMethodElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((PublicMethodElement)(element)).Id;
        }

    }

}
