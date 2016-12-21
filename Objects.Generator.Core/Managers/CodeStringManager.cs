namespace Objects.Generator.Core.Managers
{
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Text;
    using Objects.Generator.Core.Enumerations;

    internal class CodeStringManager
    {

        private static readonly Dictionary<string, string> SqlDataType = 
            new Dictionary<string, string>() {
                {"text", "System.String"},
                {"varchar", "System.String"},
                {"char", "System.Char"},
                {"ntext", "System.String"},
                {"nvarchar", "System.String"},
                {"nchar", "System.Char"},
                {"time", "System.TimeSpan"},
                {"timestamp", "System.Byte"},
                {"datetime", "System.String"},
                {"smalldatetime", "System.String"},
                {"date", "System.String"},
                {"bigint", "System.Int64"},
                {"int", "System.Int32"},
                {"smallint", "System.Int16"},
                {"tinyint", "System.Int16"},
                {"decimal", "System.Decimal"},
                {"numeric", "System.Double"},
                {"money", "System.Decimal"},
                {"smallmoney", "System.Decimal"},
                {"float", "System.Single"},
                {"real", "System.Single"},
                {"table", "System.Object"},
                {"uniqueidentifier", "System.String"},
                {"bit", "System.Boolean"},
                {"image", "System.Byte"},
                {"binary", "System.Byte"},
                {"varbinary","System.Byte"},
                {"rowversion", "System.Byte"},
                {"sql_variant", "System.Object"},
                {"xml", "System.Xml"}
        };

        internal static void CreateCodeFile(
            string filename,
            CodeNamespace codeNamespace,
            int outputCode = 1
            )
        {
            using(TextWriter textWriter = new StreamWriter(filename))
            {
                var codeProvider = CodeDomProvider.CreateProvider(((OutputExtension)outputCode).ToString());

                codeProvider.GenerateCodeFromNamespace(codeNamespace, textWriter, GetOptions());
            }

            File.WriteAllText(filename, File.ReadAllText(filename).Replace("};", "}"));
        }

        internal static string GenerateCode(
            CodeNamespace codeNamespace,
            int outputCode = 1
            )
        {
            var codeProvider = CodeDomProvider.CreateProvider(((OutputExtension)outputCode).ToString());
            var sbCode = new StringBuilder();
            var writter = new StringWriter(sbCode);

            codeProvider.GenerateCodeFromNamespace(codeNamespace, writter, GetOptions());

            return sbCode.ToString().Replace("};", "}");
        }

        internal static string GetCamelCaseIdentifier(string identifier)
        {
            return char.ToLower(identifier[0]) + identifier.Substring(1);
        }

        internal static string GetPascalCaseIdentifier(
            string identifier,
            string suffix = ""
            )
        {
            var builder = new StringBuilder(identifier.Length);
            var isAtSeparator = true;

            foreach(char ch in identifier)
            {
                //If the current character is a separator, set the flag and ignore the character
                if(!char.IsLetterOrDigit(ch)) isAtSeparator = true;
                //If flag set, uppercase current character and set flag to false
                else if(isAtSeparator)
                {
                    builder.Append(char.ToUpper(ch));
                    isAtSeparator = false;
                }
                else builder.Append(ch);
            }

            if(!string.IsNullOrEmpty(suffix)) builder.Append(suffix);

            return builder.ToString();
        }

        internal static string GetFieldType(string field)
        {
            string value;

            SqlDataType.TryGetValue(field, out value);

            return value;
        }

        internal static string GetTypeName(SqlDbType type)
        {
            var result = string.Empty;

            switch(type)
            {
                case SqlDbType.SmallInt:
                case SqlDbType.TinyInt:
                    result = "System.Int16";
                    break;
                case SqlDbType.Int:
                    result = "System.Int32";
                    break;
                case SqlDbType.BigInt:
                    result = "System.Int64";
                    break;
                case SqlDbType.Bit:
                    result = "System.Boolean";
                    break;
                case SqlDbType.Real:
                case SqlDbType.Float:
                    result = "System.Single";
                    break;
                case SqlDbType.Decimal:
                case SqlDbType.Money:
                case SqlDbType.SmallMoney:
                    result = "System.Decimal";
                    break;
                case SqlDbType.DateTime:
                case SqlDbType.SmallDateTime:
                    result = "DateTime";
                    break;
                case SqlDbType.UniqueIdentifier:
                    result = "Guid";
                    break;
                default:
                    result = "System.String";
                    break;
            }

            return result;
        }

        internal static string GetPrivateFieldName(
            string field, 
            string prefix = "{0}"
            )
        {
            return string.Format(prefix, GetCamelCaseIdentifier(field));
        }

        private static CodeGeneratorOptions GetOptions()
        {
            return new CodeGeneratorOptions
            {
                BracingStyle = "C",
                IndentString = "\t",
                BlankLinesBetweenMembers = true,
                VerbatimOrder = true,
                ElseOnClosing = true
            };
        }

    }

}
