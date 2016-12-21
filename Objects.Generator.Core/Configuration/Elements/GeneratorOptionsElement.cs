namespace Objects.Generator.Core.Configuration.Elements
{
    using System.Configuration;
    using Objects.Generator.Core.Configuration.Collections;

    public sealed partial class GeneratorOptionsElement : ConfigurationElement
    {

        //-- Properties
        [ConfigurationProperty("blankLinesBetweenMembers", IsRequired = true)]
        public bool BlankLinesBetweenMembers
        {
            get { return ((bool)(this["blankLinesBetweenMembers"])); }
            set { this["blankLinesBetweenMembers"] = value; }
        }

        [ConfigurationProperty("withImplementation", IsRequired = true)]
        public bool WithImplementation
        {
            get { return ((bool)(this["withImplementation"])); }
            set { this["withImplementation"] = value; }
        }

        //-- Elements
        [ConfigurationProperty("ClassEntity", IsRequired = true)]
        public ClassEntityElement ClassEntity
        {
            get { return ((ClassEntityElement)(this["ClassEntity"])); }
        }

        [ConfigurationProperty("PrivateProperties", IsRequired = false)]
        public PrivatePropertiesElement PrivateProperties
        {
            get { return ((PrivatePropertiesElement)(this["PrivateProperties"])); }
        }

        [ConfigurationProperty("PublicProperties", IsRequired = false)]
        public PublicPropertiesElement PublicProperties
        {
            get { return ((PublicPropertiesElement)(this["PublicProperties"])); }
        }

        [ConfigurationProperty("PublicMethods", IsRequired = false)]
        [ConfigurationCollection(typeof(PublicMethodElement), AddItemName = "PublicMethod")]
        public PublicMethodsElementCollection PublicMethods
        {
            get { return ((PublicMethodsElementCollection)(this["PublicMethods"])); }
        }

        [ConfigurationProperty("PrivateMethods", IsRequired = false)]
        [ConfigurationCollection(typeof(PrivateMethodElement), AddItemName = "PrivateMethod")]
        public PrivateMethodsElementCollection PrivateMethods
        {
            get { return ((PrivateMethodsElementCollection)(this["PrivateMethods"])); }
        }

    }

}
