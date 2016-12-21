namespace Objects.Generator.Core.Configuration.Elements
{
    using System.Configuration;

    public sealed partial class ConectionChainElement : ConfigurationElement
    {

        //-- Properties
        [ConfigurationProperty("connectionString", IsRequired = true)]
        public string ConnectionString
        {
            get { return ((string)(this["connectionString"])); }
            set { this["connectionString"] = value; }
        }

    }

}
