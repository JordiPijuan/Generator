namespace Objects.Generator.Core.Configuration.Elements
{
    using System.Configuration;
    using Objects.Generator.Core.Configuration.Collections;

    public sealed partial class PublicPropertiesElement : ConfigurationElement 
    {

        //-- Properties
        [ConfigurationProperty("id", IsRequired = true)]
        public long Id
        {
            get { return ((long)(this["id"])); }
            set { this["id"] = value; }
        }
        
        [ConfigurationProperty("startRegion", IsRequired = false)]
        public string StartRegion 
        {
            get { return ((string)(this["startRegion"])); }
            set { this["startRegion"] = value; }
        }
                
        [ConfigurationProperty("endRegion", IsRequired = false)]
        public string EndRegion 
        {
            get { return ((string)(this["endRegion"])); }
            set { this["endRegion"] = value; }
        }
                
        [ConfigurationProperty("hasGet", IsRequired = true)]
        public bool HasGet 
        {
            get { return ((bool)(this["hasGet"])); }
            set { this["hasGet"] = value; }
        }
                
        [ConfigurationProperty("hasSet", IsRequired = true)]
        public bool HasSet 
        {
            get { return ((bool)(this["hasSet"])); }
            set { this["hasSet"] = value; }
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
