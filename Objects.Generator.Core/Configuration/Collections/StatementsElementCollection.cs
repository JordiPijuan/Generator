namespace Objects.Generator.Core.Configuration.Collections
{
    using System.Configuration;
    using Objects.Generator.Core.Configuration.Elements;

    public sealed partial class StatementsElementCollection : ConfigurationElementCollection
    {

        public StatementElement this[int i]
        {
            get { return ((StatementElement)(this.BaseGet(i))); }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new StatementElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((StatementElement)(element)).Id;
        }

    }

}
