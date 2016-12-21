namespace Objects.Generator.Core.Configuration.Collections
{
    using System.Configuration;
    using Objects.Generator.Core.Configuration.Elements;

    public sealed partial class AttributesElementCollection : ConfigurationElementCollection
    {

        public AttributeElement this[int i]
        {
            get { return ((AttributeElement)(this.BaseGet(i))); }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new AttributeElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AttributeElement)(element)).Id;
        }
                        
    }

}
