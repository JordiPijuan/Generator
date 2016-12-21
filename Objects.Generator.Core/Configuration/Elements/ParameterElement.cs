namespace Objects.Generator.Core.Configuration.Elements
{
    using System.Configuration;

    public sealed partial class ParameterElement : BaseElement
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

    }

}
