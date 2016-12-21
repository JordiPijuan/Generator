namespace Objects.Generator.Core.Configuration.Elements
{
    using System.Configuration;
    using Objects.Generator.Core.Configuration.Collections;

    public sealed partial class PrivateMethodElement : BaseElement
    {

        //-- Properties
        [ConfigurationProperty("id", IsRequired = true)]
        public long Id
        {
            get { return ((long)(this["id"])); }
            set { this["id"] = value; }
        }

        [ConfigurationProperty("returnType", IsRequired = true)]
        public string ReturnType
        {
            get { return ((string)(this["returnType"])); }
            set { this["returnType"] = value; }
        }

        //-- Elements
        [ConfigurationProperty("ExceptionMethod", IsRequired = false)]
        public ExceptionMethodElement ExceptionMethod
        {
            get { return ((ExceptionMethodElement)(this["ExceptionMethod"])); }
        }

        [ConfigurationProperty("Parameters", IsRequired = false)]
        [ConfigurationCollection(typeof(ParameterElement), AddItemName = "Parameter")]
        public ParametersElementCollection Parameters
        {
            get { return ((ParametersElementCollection)(this["Parameters"])); }
        }

        [ConfigurationProperty("Statements", IsRequired = false)]
        [ConfigurationCollection(typeof(StatementElement), AddItemName = "Statement")]
        public StatementsElementCollection Statements
        {
            get { return ((StatementsElementCollection)(this["Statements"])); }
        }

    }

}
