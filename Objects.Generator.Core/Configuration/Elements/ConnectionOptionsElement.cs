namespace Objects.Generator.Core.Configuration.Elements
{
    using System.Configuration;

    public sealed partial class ConnectionOptionsElement : ConfigurationElement
    {

        //-- Properties
        [ConfigurationProperty("server", IsRequired = true)]
        public string Server
        {
            get { return ((string)(this["server"])); }
            set { this["server"] = value; }
        }

        [ConfigurationProperty("database", IsRequired = true)]
        public string Database
        {
            get { return ((string)(this["database"])); }
            set { this["database"] = value; }
        }

        [ConfigurationProperty("user", IsRequired = true)]
        public string User
        {
            get { return ((string)(this["user"])); }
            set { this["user"] = value; }
        }

        [ConfigurationProperty("password", IsRequired = true)]
        public string Password
        {
            get { return ((string)(this["password"])); }
            set { this["password"] = value; }
        }

        [ConfigurationProperty("timeout", IsRequired = true)]
        public long Timeout
        {
            get { return ((long)(this["timeout"])); }
            set { this["timeout"] = value; }
        }

    }

}
