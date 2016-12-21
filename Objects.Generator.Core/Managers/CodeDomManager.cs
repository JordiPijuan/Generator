namespace Objects.Generator.Core.Managers
{
    using System.CodeDom;
    using System.Reflection;
    using Objects.Generator.Core.Configuration;
    using Objects.Generator.Core.Configuration.Elements;
    using Objects.Generator.Core.Entities;
    using Objects.Generator.Core.Localizacion;

    public class CodeDomManager
    {

        #region · Properties class

        CodeCompileUnit _targetUnit;
        CodeTypeDeclaration _targetClass;
        private static GeneratorObjectsSection Config { get { return GeneratorObjectsManager.Config; } }
        private NamespaceElement ElementNamespace { get; set; }

        #endregion Properties class

        public CodeDomManager(NamespaceElement targetNamespace)
        {
            _targetUnit = new CodeCompileUnit();
            ElementNamespace = targetNamespace;
        }

        public CodeDomManager(
            string targetClass, 
            NamespaceElement targetNamespace
            )
            : this(targetNamespace)
        {
            _targetClass = AddClass(targetClass);
        }

        #region · Members

        internal CodeTypeDeclaration AddClass(string name)
        {
            return new CodeTypeDeclaration(name)
            {
                IsClass = true,
                TypeAttributes = TypeAttributes.Public
            };
        }

        internal CodeTypeDeclaration AddPartialClass(string name)
        {
            return new CodeTypeDeclaration(name)
            {
                IsClass = true,
                IsPartial = true,
                TypeAttributes = TypeAttributes.Public
            };
        }

        internal CodeTypeDeclaration AddSealedClass(string name)
        {
            return new CodeTypeDeclaration(name)
            {
                IsClass = true,
                TypeAttributes = TypeAttributes.Sealed
            };
        }

        internal CodeTypeDeclaration AddSealedPublicClass(string name)
        {
            return new CodeTypeDeclaration(name)
            {
                IsClass = true,
                TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed
            };
        }

        internal CodeTypeDeclaration AddInterface(string name)
        {
            return new CodeTypeDeclaration(name)
            {
                IsInterface = true,
                TypeAttributes = TypeAttributes.Public
            };
        }

        internal CodeTypeDeclaration AddGenericInterface(
            string name,
            string type
            )
        {
            var targetClass = new CodeTypeDeclaration { Name = name, IsInterface = true };

            var parameter = new CodeTypeParameter { Name = type };
            parameter.Constraints.Add(Config.Namespaces.Languaje == 1 ? " class" : " Class");

            targetClass.TypeParameters.Add(parameter);

            return targetClass;
        }

        internal CodeTypeDeclaration AddGenericRepositoryClass(
            string name,
            string sufix
            )
        {
            var targetClass = new CodeTypeDeclaration { Name = string.Concat(name, sufix) };

            targetClass.BaseTypes.Add(
                new CodeTypeReference(
                    sufix,
                    new CodeTypeReference(name)
                    )
                );

            return targetClass;
        }

        internal CodeTypeDeclaration AddGenericClass(
            string name,
            string type,
            bool withImplementation = false
            )
        {
            var targetClass = new CodeTypeDeclaration { Name = name };

            if (withImplementation)
                targetClass.BaseTypes.Add(AddTypeReference(string.Concat("I", name), type));

            var parameter = new CodeTypeParameter { Name = type };
            parameter.Constraints.Add(Config.Namespaces.Languaje == 1 ? " class" : " Class");

            targetClass.TypeParameters.Add(parameter);

            return targetClass;
        }

        internal CodeTypeReference AddTypeReference(
            string name,
            string sufix
            )
        {
            return new CodeTypeReference(
                    name,
                    new CodeTypeReference(sufix)
                );
        }

        #endregion Members

        #region · Constructors

        internal CodeConstructor AddConstructorDefault()
        {
            return new CodeConstructor { Attributes = MemberAttributes.Public };
        }

        internal CodeConstructor AddConstructorWithParameters(
            string type,
            string name
            )
        {
            var constructor = AddConstructorDefault();

            constructor.Parameters.Add(AddParameter(type, name));

            return constructor;
        }

        internal CodeParameterDeclarationExpression AddParameter(
            string type,
            string name
            )
        {
            return new CodeParameterDeclarationExpression(type, name);
        }

        #endregion Constructors

        internal CodeVariableReferenceExpression AddVariableReference(string name)
        {
            return new CodeVariableReferenceExpression(name);
        }

        #region · Regions

        internal CodeRegionDirective AddRegionStart(string name)
        {
            return new CodeRegionDirective(CodeRegionMode.Start, name);
        }

        internal CodeRegionDirective AddRegionEnd(string name = "")
        {
            return new CodeRegionDirective(CodeRegionMode.End, name);
        }

        #endregion Regions

        internal CodeNamespace AddNamespace(string targetNamespace)
        {
            return new CodeNamespace(targetNamespace);
        }

        #region · Enums

        internal CodeTypeDeclaration AddEnum(string name)
        {
            return new CodeTypeDeclaration(name)
            {
                IsEnum = true,
                TypeAttributes = TypeAttributes.Public
            };
        }

        internal CodeTypeDeclaration AddEnum(
            Table table,
            bool attribute = false
            )
        {
            var enumName = string.Format(ElementNamespace.GeneratorOptions.ClassEntity.Enumerations.EndEnums, table.Name);
            var type = new CodeTypeDeclaration(enumName) { IsEnum = true };

            foreach(var field in table.FieldsList)
            {
                var member = new CodeMemberField(enumName, field.Name);

                // Adds the description attribute
                if(attribute)
                    member.CustomAttributes.Add(
                        new CodeAttributeDeclaration(
                            ElementNamespace.GeneratorOptions.ClassEntity.Enumerations.EnumAttribute,
                            new CodeAttributeArgument(
                                new CodePrimitiveExpression(string.Format(ElementNamespace.GeneratorOptions.ClassEntity.Enumerations.Message, field.Name, table.Name))
                                )
                            )
                        );

                type.Members.Add(member);
            }

            return type;
        }

        #endregion Enums

        #region · Imports

        internal void AddUsing(
            ref CodeNamespace targetNamespace,
            string targetUsing
            )
        {
            targetNamespace.Imports.Add(new CodeNamespaceImport(targetUsing));
        }

        internal CodeNamespaceImport AddUsing(string targetUsing)
        {
            return new CodeNamespaceImport(targetUsing);
        }

        #endregion Imports

        #region · Comments

        internal void AddCommentField(
            ref CodeMemberField field,
            string text
            )
        {
            field.Comments.Add(
                new CodeCommentStatement(string.Format(ElementNamespace.GeneratorOptions.PublicProperties.Attributes[0].MessageProperty, text))
                );
        }

        internal CodeCommentStatement AddComment(string text)
        {
            return new CodeCommentStatement(string.Format(ElementNamespace.GeneratorOptions.PublicProperties.Attributes[0].MessageProperty, text));
        }

        internal CodeCommentStatement AddCommentXmlDoc(string text)
        {
            return new CodeCommentStatement(text, true);
        }

        internal void AddCommentProperty(
            ref CodeMemberProperty property,
            string text
            )
        {
            property.Comments.Add(
                new CodeCommentStatement(string.Format(ElementNamespace.GeneratorOptions.PublicProperties.Attributes[0].MessageProperty, text))
                );
        }

        #endregion Comments

        #region · Properties

        internal void AddProperty(
            string name,
            string namePrivate,
            string type
            )
        {
            // Declare the property.
            var property = new CodeMemberProperty
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                Name = name,
                HasGet = true,
                HasSet = true,
                Type = new CodeTypeReference(type)
            };

            AddCommentProperty(ref property, name);

            _targetClass.Members.Add(property);
        }

        internal CodeMemberProperty AddProperty(
            string name,
            string namePrivate,
            string type,
            bool hasGet,
            bool hasSet
            )
        {
            // Declare the property.
            var property = new CodeMemberProperty
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                Name = name,
                HasGet = hasGet,
                HasSet = hasSet,
                Type = new CodeTypeReference(type)
            };

            if(hasGet)
                property.GetStatements.Add(AddGet(namePrivate));

            if(hasSet)
                property.SetStatements.Add(AddSet(namePrivate));

            return property;
        }

        internal CodeMemberField AddPublicProperty(
            string name,
            string type
            )
        {
            return new CodeMemberField
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                Name = string.Concat(name, Config.Namespaces.Languaje == 1 ? CoreMessages.MsgPropertyGetSet : string.Empty),
                Type = new CodeTypeReference(type)
            };
        }

        internal CodeMemberField AddField(
            string name,
            string type
            )
        {
            return new CodeMemberField
            {
                Attributes = MemberAttributes.Private,
                Name = name,
                Type = new CodeTypeReference(type)
            };
        }


        internal void AddGet(
            ref CodeMemberProperty property,
            string name
            )
        {
            property.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(),
                        name
                        )
                    )
                );
        }

        internal CodeMethodReturnStatement AddGet(string name)
        {
            return new CodeMethodReturnStatement(
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(),
                    name
                    )
                );
        }

        internal void AddSet(
            ref CodeMemberProperty property,
            string name
            )
        {
            property.SetStatements.Add(
                new CodeAssignStatement(
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(),
                        name
                        ),
                    new CodePropertySetValueReferenceExpression()
                    )
                );
        }

        internal CodeAssignStatement AddSet(string name)
        {
            return new CodeAssignStatement(
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(),
                        name
                        ),
                    new CodePropertySetValueReferenceExpression()
                );
        }

        #endregion Properties

        #region · Attributes

        internal CodeAttributeDeclaration AddAttribute(string attribute)
        {
            return new CodeAttributeDeclaration(attribute);
        }

        internal CodeAttributeDeclaration AddAttributeWithArgument(
            string attribute,
            CodeAttributeArgument argument
            )
        {
            return new CodeAttributeDeclaration(attribute, argument);
        }

        internal CodeAttributeArgument AddAttributeArgument(
            string attribute,
            string argument
            )
        {
            return new CodeAttributeArgument(new CodePrimitiveExpression(string.Format(attribute, argument)));
        }

        internal CodeAttributeArgument AddAttributeArgumentTypeof(string type)
        {
            return new CodeAttributeArgument(
                new CodeSnippetExpression(
                    string.Format(CoreMessages.MsgTypeof, type)
                    )
                );
        }

        #endregion Attributes

        internal CodeEntryPointMethod AddEntryPointMethod(string name)
        {
            var methodMain = new CodeEntryPointMethod();

            return methodMain;
        }

        internal CodeVariableDeclarationStatement AddVariableDeclaration(
            string name, 
            string type
            )
        {
            return new CodeVariableDeclarationStatement(
                type,
                name,
                new CodeObjectCreateExpression(type)
                );
        }

        #region · Methods

        internal CodeMemberMethod AddPublicMethod(
            string name,
            string returnType
            )
        {
            var method1 = new CodeMemberMethod()
            {
                Name = name,
                ReturnType = new CodeTypeReference(returnType),
                Attributes = MemberAttributes.Public | MemberAttributes.Override
            };

            //TODO -- Seleccionar del config
            method1.Statements.Add(AddThrowException("NotImplementedException"));

            return method1;
        }

        internal CodeMemberMethod AddPublicTypeMethod(
            string name,
            string returnType
            )
        {
            var method = new CodeMemberMethod()
            {
                Name = name,
                ReturnType = new CodeTypeReference(returnType),
                Attributes = MemberAttributes.Public | MemberAttributes.Final
            };

            method.Statements.Add(AddThrowException("NotImplementedException"));

            return method;
        }

        internal CodeMemberMethod AddPublicVirtualTypeMethod(
            string name,
            string returnType
            )
        {
            var method = new CodeMemberMethod()
            {
                Name = name,
                ReturnType = string.IsNullOrWhiteSpace(returnType) ? null : new CodeTypeReference(returnType),
                Attributes = MemberAttributes.Public
            };

            //method.Statements.Add(AddThrowException("NotImplementedException"));

            return method;
        }

        internal CodeMemberMethod AddPublicOverrideTypeMethod(
            string name,
            string returnType
            )
        {
            var method = new CodeMemberMethod()
            {
                Name = name,
                ReturnType = string.IsNullOrWhiteSpace(returnType) ? null : new CodeTypeReference(returnType),
                Attributes = MemberAttributes.Public | MemberAttributes.Override
            };

            //method1.TypeParameters.Add(new CodeTypeParameter(type));
            //method.Statements.Add(AddThrowException("NotImplementedException"));

            return method;
        }

        #endregion Methods

        #region · Exceptions

        internal CodeThrowExceptionStatement AddThrowExceptionWithMessage(
            string typeException,
            string message
            )
        {
            return new CodeThrowExceptionStatement(
                    new CodeObjectCreateExpression(
                            typeException,
                            new CodePrimitiveExpression(message)
                            )
                        );
        }

        internal CodeThrowExceptionStatement AddThrowException(string typeException)
        {
            return new CodeThrowExceptionStatement(
                    new CodeObjectCreateExpression(
                            typeException
                            )
                        );
        }

        #endregion Exceptions

        internal CodeSnippetExpression AddSnippetExpresion(string name)
        {
            return new CodeSnippetExpression(
                string.Format(CoreMessages.MsgEntityThis,
                    CodeStringManager.GetPrivateFieldName(
                        name,
                        ElementNamespace.GeneratorOptions.PrivateProperties.PrefixPrivateFields
                    ),
                    CodeStringManager.GetCamelCaseIdentifier(name)
                    )
                );
        }

    }

}
