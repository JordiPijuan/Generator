namespace Objects.Generator.Core.Decorators
{
    using System.CodeDom;
    using System.Linq;
    using Objects.Generator.Core.Configuration;
    using Objects.Generator.Core.Configuration.Elements;
    using Objects.Generator.Core.Contracts;
    using Objects.Generator.Core.Enumerations;
    using Objects.Generator.Core.Managers;

    public class GenericRepositoryGenerator : Generator
    {

        #region · Properties

        internal bool IsInterface { get; set; }
        internal bool WithImplementation { get; set; }
        internal NamespaceElement ElementNamespace { get; set; }
        private static GeneratorObjectsSection Config { get { return GeneratorObjectsManager.Config; } }
        readonly CodeDomManager _manager;

        #endregion Properties

        public GenericRepositoryGenerator(
            NamespaceElement name,
            bool isInterface = false,
            bool withImplementation = false
            )
        {
            ElementNamespace = name;
            _manager = new CodeDomManager(ElementNamespace);
            IsInterface = isInterface;
            WithImplementation = withImplementation;
        }

        public override CodeNamespace Generate()
        {
            var nameSpace = _manager.AddNamespace(ElementNamespace.Name);
            var targetClass = IsInterface
                ? _manager.AddGenericInterface(GeneratedType.IRepository.ToString(), "T")
                : WithImplementation
                    ? _manager.AddGenericClass(GeneratedType.Repository.ToString(), "T", WithImplementation)
                    : _manager.AddGenericClass(GeneratedType.Repository.ToString(), "T");

            ElementNamespace.GeneratorOptions.PublicMethods
                .Cast<PublicMethodElement>()
                .ToList()
                .FindAll(m => m.Enabled)
                .ForEach(method =>
                {
                    var targetMethod = _manager.AddPublicVirtualTypeMethod(
                        method.Name,
                        Config.Namespaces.Languaje == 1
                            ? method.ReturnType
                            : method.ReturnType.Replace("<T>", "(Of T)")
                        );

                    method.Parameters
                        .Cast<ParameterElement>()
                        .ToList()
                        .FindAll(m => m.Enabled)
                        .ForEach(param => targetMethod.Parameters.Add(_manager.AddParameter(param.Type, param.Name)));
                    
                    if(method.Statements.Count > 0)
                        method.Statements
                            .Cast<StatementElement>()
                            .ToList()
                            .FindAll(m => m.Enabled)
                            .ForEach(statement => targetMethod.Statements.Add(_manager.AddThrowException(statement.Name)));
                    else
                        targetMethod.Statements.Add(_manager.AddThrowException(method.ExceptionMethod.TypeException));

                    targetClass.Members.Add(targetMethod);
                });

            if (ElementNamespace.Imports.Count > 0)
            {
                ElementNamespace.Imports
                    .Cast<ImportElement>()
                    .ToList()
                    .FindAll(i => i.Enabled)
                    .ForEach(i => nameSpace.Imports.Add(_manager.AddUsing(i.Name)));
            }

            nameSpace.Types.Add(targetClass);

            return nameSpace;
        }

    }

}
