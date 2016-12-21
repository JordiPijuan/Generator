namespace Objects.Generator.Core.Configuration.Elements
{
    using System.Configuration;

    public sealed partial class EnumerationsElement : ConfigurationElement
    {

        //-- Properties
        [ConfigurationProperty("id", IsRequired = true)]
        public long Id
        {
            get { return ((long)(this["id"])); }
            set { this["id"] = value; }
        }

        [ConfigurationProperty("enumAttribute", IsRequired = true)]
        public string EnumAttribute
        {
            get { return ((string)(this["enumAttribute"])); }
            set { this["enumAttribute"] = value; }
        }

        [ConfigurationProperty("endEnums", IsRequired = true)]
        public string EndEnums
        {
            get { return ((string)(this["endEnums"])); }
            set { this["endEnums"] = value; }
        }

        [ConfigurationProperty("addAttribute", IsRequired = true)]
        public bool AddAttribute
        {
            get { return ((bool)(this["addAttribute"])); }
            set { this["addAttribute"] = value; }
        }

        [ConfigurationProperty("message", IsRequired = false)]
        public string Message
        {
            get { return ((string)(this["message"])); }
            set { this["message"] = value; }
        }

    }

}
