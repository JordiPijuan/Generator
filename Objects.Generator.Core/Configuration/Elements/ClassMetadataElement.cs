namespace Objects.Generator.Core.Configuration.Elements
{
    using System.Configuration;
    using Objects.Generator.Core.Configuration.Collections;

    public sealed partial class ClassMetadataElement : ConfigurationElement
    {

        //-- Properties
        [ConfigurationProperty("id", IsRequired = true)]
        public long Id
        {
            get { return ((long)(this["id"])); }
            set { this["id"] = value; }
        }

        [ConfigurationProperty("sufix", IsRequired = true)]
        public string Sufix
        {
            get { return ((string)(this["sufix"])); }
            set { this["sufix"] = value; }
        }

        //-- Elements
        [ConfigurationProperty("Attributes", IsRequired = false)]
        [ConfigurationCollection(typeof(AttributeElement), AddItemName = "Attribute")]
        public AttributesElementCollection Attributes
        {
            get { return ((AttributesElementCollection)(this["Attributes"])); }
        }

    }

}
