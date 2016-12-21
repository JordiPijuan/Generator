namespace Objects.Generator.Core.Configuration.Collections
{
    using System.Configuration;
    using Objects.Generator.Core.Configuration.Elements;

    public sealed class GeneratorConnectionsElementCollection : ConfigurationElementCollection
    {

        public GeneratorConnectionElement this[int i]
        {
            get { return ((GeneratorConnectionElement)(BaseGet(i))); }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new GeneratorConnectionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((GeneratorConnectionElement)(element)).Id;
        }

    }

}
