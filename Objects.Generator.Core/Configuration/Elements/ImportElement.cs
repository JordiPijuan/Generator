namespace Objects.Generator.Core.Configuration.Elements
{
    using System.Configuration;

    public sealed class ImportElement : BaseElement
    {

        //-- Properties
        [ConfigurationProperty("id", IsRequired = true)]
        public long Id
        {
            get { return ((long)(this["id"])); }
            set { this["id"] = value; }
        }

    }

}
