namespace Objects.Generator.Core.Decorators
{
    using System.CodeDom;
    using System.Linq;
    using Objects.Generator.Core.Configuration;
    using Objects.Generator.Core.Configuration.Elements;
    using Objects.Generator.Core.Contracts;
    using Objects.Generator.Core.Entities;
    using Objects.Generator.Core.Enumerations;
    using Objects.Generator.Core.Managers;

    public class GenericRepositoryCollectionGenerator : Generator
    {

        #region · Properties

        internal Table TargetTable { get; set; }
        internal NamespaceElement ElementNamespace { get; set; }
        private static GeneratorObjectsSection Config { get { return GeneratorObjectsManager.Config; } }
        readonly CodeDomManager _manager;

        #endregion Properties

        public GenericRepositoryCollectionGenerator(
            NamespaceElement name,
            Table targetTable
            )
        {
            ElementNamespace = name;
            TargetTable = targetTable;
            _manager = new CodeDomManager(ElementNamespace);
        }

        public override CodeNamespace Generate()
        {
            var nameSpace = _manager.AddNamespace(ElementNamespace.Name);
            var targetClass = _manager.AddGenericRepositoryClass(
                TargetTable.Name, 
                GeneratedType.Repository.ToString()
                );

            ElementNamespace.GeneratorOptions.PublicMethods
                .Cast<PublicMethodElement>()
                .ToList()
                .FindAll(m => m.Enabled)
                .ForEach(method =>
                {
                    var targetMethod = _manager.AddPublicOverrideTypeMethod(
                        method.Name,
                        Config.Namespaces.Languaje == 1
                            ? string.Format(method.ReturnType, TargetTable.Name)
                            : string.Format(method.ReturnType.Replace("<{0}>", "(Of {0})"), TargetTable.Name)
                        );

                    method.Parameters
                        .Cast<ParameterElement>()
                        .ToList()
                        .FindAll(m => m.Enabled)
                        .ForEach(param => targetMethod.Parameters.Add(_manager.AddParameter(TargetTable.Name, param.Name)));

                    if(method.Statements.Count > 0)
                        method.Statements
                            .Cast<StatementElement>()
                            .ToList()
                            .FindAll(m => m.Enabled)
                            .ForEach( statement => targetMethod.Statements.Add(_manager.AddThrowException(statement.Name)));
                    else
                        targetMethod.Statements.Add(_manager.AddThrowException(method.ExceptionMethod.TypeException));

                    targetClass.Members.Add(targetMethod);
                });

            if (ElementNamespace.Imports.Count > 0)
            {
                ElementNamespace.Imports
                    .Cast<ImportElement>()
                    .ToList()
                    .FindAll(m => m.Enabled)
                    .ForEach(i => nameSpace.Imports.Add(_manager.AddUsing(i.Name)));
            }

            nameSpace.Types.Add(targetClass);

            return nameSpace;
        }

    }

}
