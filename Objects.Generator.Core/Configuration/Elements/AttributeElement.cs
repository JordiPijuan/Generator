namespace Objects.Generator.Core.Configuration.Elements
{
    using System.Configuration;

    public sealed partial class AttributeElement : BaseElement
    {

        //-- Properties
        [ConfigurationProperty("id", IsRequired = true)]
        public long Id
        {
            get { return ((long)(this["id"])); }
            set { this["id"] = value; }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return ((string)(this["type"])); }
            set { this["type"] = value; }
        }

        [ConfigurationProperty("value", IsRequired = false)]
        public string Value
        {
            get { return ((string)(this["value"])); }
            set { this["value"] = value; }
        }
                            
        [ConfigurationProperty("messageProperty", IsRequired = false)]
        public string MessageProperty
        {
            get { return ((string)(this["messageProperty"])); }
            set { this["messageProperty"] = value; }
        }

    }

}
