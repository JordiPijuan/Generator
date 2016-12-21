namespace Objects.Generator.Core.Exceptions
{
    using System.Collections.Generic;
    using Objects.Generator.Core.Enumerations;
    using Objects.Generator.Core.Localizacion;

    public class GeneratorObjectsErrorDescription
    {

        public static GeneratorObjectsErrorDescription ErrorsDescription = null;
        public readonly Dictionary<GeneratorObjectsError, string> ErrorCollection = new Dictionary<GeneratorObjectsError, string>();

        private GeneratorObjectsErrorDescription()
        {

        }

        public static GeneratorObjectsErrorDescription GetInstance()
        {
            return ErrorsDescription ?? (ErrorsDescription = CreateInstance());
        }

        private static GeneratorObjectsErrorDescription CreateInstance()
        {
            var errorDescriptions = new GeneratorObjectsErrorDescription();

            BuildErrorDescriptionList(ref errorDescriptions);

            return errorDescriptions;
        }

        private static void BuildErrorDescriptionList(ref GeneratorObjectsErrorDescription errorsDescription)
        {

            errorsDescription.ErrorCollection.Add(
                GeneratorObjectsError.SectionNotCorrect,
                ExceptionsMessages.MsgSectionNotCorrect
            );

            errorsDescription.ErrorCollection.Add(
                GeneratorObjectsError.ConnectionNotActive,
                ExceptionsMessages.MsgConnectionNotActive
            );

            errorsDescription.ErrorCollection.Add(
                GeneratorObjectsError.ServerNotProvided,
                ExceptionsMessages.MsgServerNotProvided
            );

            errorsDescription.ErrorCollection.Add(
                GeneratorObjectsError.DatabaseNotProvided,
                ExceptionsMessages.MsgDatabaseNotProvided
            );

            errorsDescription.ErrorCollection.Add(
                GeneratorObjectsError.TimeoutNotProvided,
                ExceptionsMessages.MsgTimeoutNotProvided
            );

            errorsDescription.ErrorCollection.Add(
                GeneratorObjectsError.LanguajeNotProvided,
                ExceptionsMessages.MsgLanguajeNotProvided
            );

            errorsDescription.ErrorCollection.Add(
                GeneratorObjectsError.NamespaceNotProvider,
                ExceptionsMessages.MsgNamespaceNotProvider
            );

            errorsDescription.ErrorCollection.Add(
                GeneratorObjectsError.NamespaceTypeNotProvider,
                ExceptionsMessages.MsgNamespaceTypeNotProvider
            );

            errorsDescription.ErrorCollection.Add(
                GeneratorObjectsError.NamespaceDestinyNotProvided,
                ExceptionsMessages.MsgNamespaceDestinyNotProvided
            );

            errorsDescription.ErrorCollection.Add(
                GeneratorObjectsError.MetadataNameNotProvided,
                ExceptionsMessages.MsgMetadataNameNotProvided
            );

            errorsDescription.ErrorCollection.Add(
                GeneratorObjectsError.MetadataTypeNotProvided,
                ExceptionsMessages.MsgNamespaceTypeNotProvider
            );

            errorsDescription.ErrorCollection.Add(
                GeneratorObjectsError.ClassMetadataSufixNotProvided,
                ExceptionsMessages.MsgClassMetadataSufixNotProvided
            );

            errorsDescription.ErrorCollection.Add(
                GeneratorObjectsError.UsingNotProvided,
                ExceptionsMessages.MsgUsingNotProvided
            );

        }

    }

}
