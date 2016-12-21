namespace Objects.Generator.Core.Configuration.Elements
{
    using System.Configuration;

    public class BaseElement : ConfigurationElement
    {

        [ConfigurationProperty("name", IsRequired = true)]
        [StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string Name
        {
            get { return ((string)(this["name"])); }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("enabled", IsRequired = false)]
        public bool Enabled
        {
            get { return ((bool)(this["enabled"])); }
            set { this["enabled"] = value; }
        }

    }

}
