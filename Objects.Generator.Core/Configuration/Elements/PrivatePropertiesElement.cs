namespace Objects.Generator.Core.Configuration.Elements
{
    using System.Configuration;

    public sealed partial class PrivatePropertiesElement : ConfigurationElement
    {

        //-- Properties
        [ConfigurationProperty("id", IsRequired = true, IsKey = true)]
        public long Id
        {
            get { return ((long)(this["id"])); }
            set { this["id"] = value; }
        }

        [ConfigurationProperty("startRegion", IsRequired = true)]
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

        [ConfigurationProperty("prefixPrivateFields", IsRequired = false)]
        public string PrefixPrivateFields
        {
            get { return ((string)(this["prefixPrivateFields"])); }
            set { this["prefixPrivateFields"] = value; }
        }

    }

}
