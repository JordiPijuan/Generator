namespace Objects.Generator.Core.Decorators
{
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using Objects.Generator.Core.Configuration.Elements;
    using Objects.Generator.Core.Contracts;
    using Objects.Generator.Core.Entities;
    using Objects.Generator.Core.Enumerations;
    using Objects.Generator.Core.Managers;

    public class EntityGenerator : Generator
    {

        #region · Properties

        private Table TargetTable { get; set; }
        private NamespaceElement ElementNamespace { get; set; }
        readonly CodeDomManager _manager;
        private CodeTypeDeclaration _targetClass;
        private CodeTypeDeclaration _targetMetaData;

        #endregion Properties

        public EntityGenerator(
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
            var listFields = TargetTable.FieldsList.ToList();
            var nameSpace = _manager.AddNamespace(ElementNamespace.Name);

            if (ElementNamespace.GeneratorOptions.ClassEntity.Partial)
                _targetClass = _manager.AddPartialClass(TargetTable.Name);
            else if (ElementNamespace.GeneratorOptions.ClassEntity.Sealed)
                _targetClass = _manager.AddSealedPublicClass(TargetTable.Name);
            else
                _targetClass = _manager.AddClass(TargetTable.Name);

            if (ElementNamespace.GeneratorOptions.ClassEntity.FieldsEnum)
            {
                _targetClass.Members.Add(
                    _manager.AddEnum(
                        TargetTable,
                        ElementNamespace.GeneratorOptions.ClassEntity.Enumerations.AddAttribute
                        )
                    );
            }

            var cons = _manager.AddConstructorDefault();

            if (ElementNamespace.GeneratorOptions.ClassEntity.AddConstructorWithParameters)
            {
                cons.StartDirectives.Add(
                    _manager.AddRegionStart(
                        ElementNamespace.GeneratorOptions.ClassEntity.Constructor.StartRegion
                        )
                    );

                _targetClass.Members.Add(cons);

                GetConstructorWithParameters(listFields);
            }
            else
                _targetClass.Members.Add(cons);

            GetPrivateFields(listFields);
            GetPublicProperties(listFields);

            if (ElementNamespace.Imports.Count > 0)
            {
                ElementNamespace.Imports
                    .Cast<ImportElement>()
                    .ToList()
                    .FindAll(u => u.Enabled)
                    .ForEach(i => nameSpace.Imports.Add(_manager.AddUsing(i.Name)));
            }

            nameSpace.Types.Add(_targetClass);

            return nameSpace;
        }

        public CodeNamespace GenerateMetadata()
        {
            var nameSpaceMetadata = _manager.AddNamespace(ElementNamespace.Name);

            if (ElementNamespace.Imports.Count > 0)
            {
                ElementNamespace.Imports
                    .Cast<ImportElement>()
                    .ToList()
                    .FindAll(u => u.Enabled)
                    .ForEach(i => nameSpaceMetadata.Imports.Add(_manager.AddUsing(i.Name)));
            }

            GetMetadataClass(nameSpaceMetadata);

            return nameSpaceMetadata;
        }

        #region · Private methods

        private void GetConstructorWithParameters(IReadOnlyCollection<Field> fieldsList)
        {
            var constructor = _manager.AddConstructorDefault();

            fieldsList
                .ToList()
                .ForEach(
                    param =>
                    {
                        constructor.Parameters.Add(
                            _manager.AddParameter(
                                CodeStringManager.GetFieldType(param.FieldType),
                                CodeStringManager.GetCamelCaseIdentifier(param.Name)
                                )
                            );
                        constructor.Statements.Add(_manager.AddSnippetExpresion(param.Name));
                    }
                );

            constructor.ChainedConstructorArgs.Add(new CodeSnippetExpression());
            constructor.EndDirectives.Add(_manager.AddRegionEnd());

            _targetClass.Members.Add(constructor);
        }

        private void GetMetadataClass(CodeNamespace _namespace)
        {
            _targetMetaData = _manager.AddPartialClass(TargetTable.Name);
            var targetName = string.Format(ElementNamespace.GeneratorOptions.ClassEntity.ClassMetadata.Sufix, TargetTable.Name);
            var targetSealed = _manager.AddSealedClass(targetName);

            _targetMetaData.CustomAttributes.Add(
                _manager.AddAttributeWithArgument(
                    ObjectType.MetadataType.ToString(),
                    _manager.AddAttributeArgumentTypeof(string.Concat(ElementNamespace.Name, ".", targetName))
                    )
                );

            TargetTable.FieldsList
                .ToList()
                .ForEach(field =>
                {
                    var member = _manager.AddPublicProperty(
                        field.Name,
                        CodeStringManager.GetFieldType(field.FieldType)
                        );

                    if(ElementNamespace.GeneratorOptions.ClassEntity.ClassMetadata.Attributes
                        .Cast<AttributeElement>()
                        .ToList()
                        .FindAll(m => m.Enabled)
                        .Any())
                    {
                        ElementNamespace.GeneratorOptions.ClassEntity.ClassMetadata.Attributes
                            .Cast<AttributeElement>()
                            .ToList()
                            .FindAll(m => m.Enabled)
                            .ForEach(att =>
                            {
                                if (string.IsNullOrWhiteSpace(att.Value))
                                {
                                    if (field.Name.Contains("Id") || field.Name.Contains("ID"))
                                        member.CustomAttributes.Add(_manager.AddAttribute(att.Name));
                                }
                                else
                                    member.CustomAttributes.Add(
                                        _manager.AddAttributeWithArgument(
                                            att.Name,
                                            _manager.AddAttributeArgument(
                                                att.Value,
                                                field.Name
                                                )
                                            )
                                        );
                            });
                    }

                    targetSealed.Members.Add(member);
                });

            _targetMetaData.Members.Add(targetSealed);

            _namespace.Types.Add(_targetMetaData);
        }

        private void GetPrivateFields(IReadOnlyCollection<Field> fieldsList)
        {
            int count = 0;
            //-- private properties
            foreach (var field in fieldsList)
            {
                var member = _manager.AddField(
                        CodeStringManager.GetPrivateFieldName(
                            field.Name,
                            ElementNamespace.GeneratorOptions.PrivateProperties.PrefixPrivateFields
                            ),
                        CodeStringManager.GetFieldType(field.FieldType)
                    );

                if (count == 0)
                    member.StartDirectives.Add(
                        _manager.AddRegionStart(
                            ElementNamespace.GeneratorOptions.PrivateProperties.StartRegion
                            )
                        );

                if (count == fieldsList.Count - 1)
                    member.EndDirectives.Add(
                        _manager.AddRegionEnd(
                            ElementNamespace.GeneratorOptions.PrivateProperties.EndRegion
                            )
                        );

                _targetClass.Members.Add(member);

                count++;
            }
        }

        private void GetPublicProperties(IReadOnlyCollection<Field> fieldsList)
        {
            int count = 0;
            //-- public properties
            foreach (var field in fieldsList)
            {
                var member = _manager.AddProperty(
                        field.Name,
                        CodeStringManager.GetPrivateFieldName(
                            field.Name,
                            ElementNamespace.GeneratorOptions.PrivateProperties.PrefixPrivateFields
                            ),
                        CodeStringManager.GetFieldType(field.FieldType),
                        ElementNamespace.GeneratorOptions.PublicProperties.HasGet,
                        ElementNamespace.GeneratorOptions.PublicProperties.HasSet
                    );

                if(ElementNamespace.GeneratorOptions.PublicProperties.Attributes.Count > 0)
                {
                    ElementNamespace.GeneratorOptions.PublicProperties.Attributes
                        .Cast<AttributeElement>()
                        .ToList()
                        .FindAll(a => a.Enabled)
                        .ForEach( att => 
                        {
                            member.CustomAttributes.Add(
                                _manager.AddAttributeWithArgument(
                                    att.Name,
                                    _manager.AddAttributeArgument(
                                        string.Format(att.MessageProperty, field.Name),
                                        att.Name
                                        )
                                    )
                                );
                        });
                }

                if (count == 0)
                    member.StartDirectives.Add(
                        _manager.AddRegionStart(
                            ElementNamespace.GeneratorOptions.PublicProperties.StartRegion
                            )
                        );

                if (count == fieldsList.Count - 1)
                    member.EndDirectives.Add(
                        _manager.AddRegionEnd(
                            ElementNamespace.GeneratorOptions.PublicProperties.EndRegion
                            )
                        );

                _targetClass.Members.Add(member);

                count++;
            }
        }

        #endregion Private methods

    }

}
