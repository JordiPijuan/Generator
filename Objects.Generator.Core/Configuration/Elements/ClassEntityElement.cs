namespace Objects.Generator.Core.Configuration.Elements
{
    using System.Configuration;

    public sealed partial class ClassEntityElement : ConfigurationElement
    {

        //-- Properties
        [ConfigurationProperty("id", IsRequired = true)]
        public long Id
        {
            get { return ((long)(this["id"])); }
            set { this["id"] = value; }
        }

        [ConfigurationProperty("partial", IsRequired = true)]
        public bool Partial
        {
            get { return ((bool)(this["partial"])); }
            set { this["partial"] = value; }
        }

        [ConfigurationProperty("sealed", IsRequired = true)]
        public bool Sealed
        {
            get { return ((bool)(this["sealed"])); }
            set { this["sealed"] = value; }
        }

        [ConfigurationProperty("isEnum", IsRequired = true)]
        public bool IsEnum
        {
            get { return ((bool)(this["isEnum"])); }
            set { this["isEnum"] = value; }
        }

        [ConfigurationProperty("fieldsEnum", IsRequired = true)]
        public bool FieldsEnum
        {
            get { return ((bool)(this["fieldsEnum"])); }
            set { this["fieldsEnum"] = value; }
        }

        [ConfigurationProperty("withMetadata", IsRequired = false)]
        public bool WithMetadata
        {
            get { return ((bool)(this["withMetadata"])); }
            set { this["withMetadata"] = value; }
        }

        [ConfigurationProperty("addConstructorWithParameters", IsRequired = false)]
        public bool AddConstructorWithParameters
        {
            get { return ((bool)(this["addConstructorWithParameters"])); }
            set { this["addConstructorWithParameters"] = value; }
        }

        [ConfigurationProperty("sufix", IsRequired = false)]
        public string Sufix
        {
            get { return ((string)(this["sufix"])); }
            set { this["sufix"] = value; }
        }

        //-- Elements
        [ConfigurationProperty("ClassMetadata", IsRequired = false)]
        public ClassMetadataElement ClassMetadata
        {
            get { return ((ClassMetadataElement)(this["ClassMetadata"])); }
        }

        [ConfigurationProperty("Constructor", IsRequired = false)]
        public ConstructorElement Constructor
        {
            get { return ((ConstructorElement)(this["Constructor"])); }
        }

        [ConfigurationProperty("Enumerations", IsRequired = false)]
        public EnumerationsElement Enumerations
        {
            get { return ((EnumerationsElement)(this["Enumerations"])); }
        }

    }

}
