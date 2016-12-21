namespace Objects.Generator.Core.Configuration.Collections
{
    using System.Configuration;
    using Objects.Generator.Core.Configuration.Elements;

    public sealed class NameSpacesElementCollection : ConfigurationElementCollection
    {

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return ((string)(this["name"])); }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("languaje", IsRequired = true)]
        public long Languaje
        {
            get { return ((long)(this["languaje"])); }
            set { this["languaje"] = value; }
        }

        [ConfigurationProperty("automatic", IsRequired = false, DefaultValue = true)]
        public bool Automatic
        {
            get { return ((bool)(this["automatic"])); }
            set { this["automatic"] = value; }
        }

        public NamespaceElement this[int i]
        {
            get { return ((NamespaceElement)(BaseGet(i))); }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new NamespaceElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((NamespaceElement)(element)).Id;
        }
    
    }

}
