namespace Objects.Generator.Core.Configuration
{
    using System.Configuration;
    using Objects.Generator.Core.Configuration.Collections;
    using Objects.Generator.Core.Configuration.Elements;

    public sealed partial class GeneratorObjectsSection : ConfigurationSection
    {

        //-- Elements
        [ConfigurationPropertyAttribute("GeneratorConnections", IsRequired = true)]
        [ConfigurationCollectionAttribute(typeof(GeneratorConnectionElement), AddItemName = "GeneratorConnection")]
        public GeneratorConnectionsElementCollection GeneratorConnections
        {
            get { return ((GeneratorConnectionsElementCollection)(this["GeneratorConnections"])); }
        }

        [ConfigurationPropertyAttribute("Namespaces", IsRequired = true)]
        [ConfigurationCollectionAttribute(typeof(NamespaceElement), AddItemName = "Namespace")]
        public NameSpacesElementCollection Namespaces
        {
            get { return ((NameSpacesElementCollection)(this["Namespaces"])); }
        }

    }

}
