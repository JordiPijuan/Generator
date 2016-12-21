namespace Objects.Generator.Core.Configuration.Collections
{
    using System.Configuration;
    using Objects.Generator.Core.Configuration.Elements;

    public sealed class ImportsElementCollection : ConfigurationElementCollection
    {

        public ImportElement this[int i]
        {
            get { return ((ImportElement)(BaseGet(i))); }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ImportElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ImportElement)(element)).Id;
        }

    }

}
