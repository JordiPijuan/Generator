using System.Configuration;

namespace Objects.Generator.Core.Configuration.Elements
{
    public sealed partial class ConstructorElement : ConfigurationElement
    {

        //-- Properties
        [ConfigurationProperty("id", IsRequired = true)]
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

    }
}
