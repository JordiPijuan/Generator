namespace Objects.Generator.Core.Configuration.Elements
{
    using System;
    using System.Configuration;

    public sealed partial class GeneratorConnectionElement : BaseElement
    {

        //-- Properties
        [ConfigurationProperty("id", IsRequired = true, IsKey = true)]
        [LongValidator(MinValue = 0, MaxValue = 10)]
        public long Id
        {
            get { return ((long)(this["id"])); }
            set { this["id"] = value; }
        }

        [ConfigurationProperty("provider", IsRequired = true)]
        [StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string Provider
        {
            get { return ((string)(this["provider"])); }
            set { this["provider"] = value; }
        }

        //-- Elements
        [ConfigurationProperty("ConectionChain", IsRequired = true)]
        public ConectionChainElement ConectionChain
        {
            get { return ((ConectionChainElement)(this["ConectionChain"])); }
        }

        [ConfigurationProperty("ConnectionOptions", IsRequired = true)]
        public ConnectionOptionsElement ConnectionOptions
        {
            get { return ((ConnectionOptionsElement)(this["ConnectionOptions"])); }
        }

    }

}