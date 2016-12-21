namespace Objects.Generator.Core.Configuration.Elements
{
    using System.Configuration;

    public sealed partial class ExceptionMethodElement : ConfigurationElement
    {

        //-- Properties
        [ConfigurationProperty("typeException", IsRequired = true)]
        public string TypeException
        {
            get { return ((string)(this["typeException"])); }
            set { this["typeException"] = value; }
        }

        [ConfigurationProperty("message", IsRequired = false)]
        public string Message
        {
            get { return ((string)(this["message"])); }
            set { this["message"] = value; }
        }

    }

}
