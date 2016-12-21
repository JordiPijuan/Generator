namespace Objects.Generator.Core.Configuration.Elements
{
    using System.Configuration;
    using Objects.Generator.Core.Configuration.Collections;

    public sealed partial class NamespaceElement : BaseElement
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

        [ConfigurationProperty("destinyPath", IsRequired = true)]
        public string DestinyPath
        {
            get { return ((string)(this["destinyPath"])); }
            set { this["destinyPath"] = value; }
        }

        //-- Elements
        [ConfigurationProperty("GeneratorOptions", IsRequired = true)]
        public GeneratorOptionsElement GeneratorOptions
        {
            get {  return ((GeneratorOptionsElement)(this["GeneratorOptions"])); }
        }

        [ConfigurationProperty("Imports", IsRequired = false)]
        [ConfigurationCollection(typeof(ImportElement), AddItemName = "Import")]
        public ImportsElementCollection Imports
        {
            get { return ((ImportsElementCollection)(this["Imports"])); }
        }

    }

}
