namespace Objects.Generator.Core.Configuration.Collections
{
    using System.Configuration;
    using Objects.Generator.Core.Configuration.Elements;

    public sealed partial class ParametersElementCollection : ConfigurationElementCollection
    {

        public ParameterElement this[int i]
        {
            get { return ((ParameterElement)(this.BaseGet(i))); }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ParameterElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ParameterElement)(element)).Id;
        }

    }

}
