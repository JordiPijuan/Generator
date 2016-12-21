namespace Objects.Generator.Core.Configuration
{
    using Enumerations;
    using Exceptions;
    using Localizacion;
    using System;
    using System.Configuration;

    public sealed class GeneratorObjectsManager
    {

        private static GeneratorObjectsSection _config;

        static GeneratorObjectsManager()
        {
            try
            {
                _config = ((GeneratorObjectsSection)(ConfigurationManager.GetSection(ConfigurationKeys.Section.Key)));
            }
            catch (System.Exception ex)
            {
                throw new GeneratorObjectsException(
                    GeneratorObjectsError.SectionNotCorrect,
                    string.Format(
                        CoreMessages.MsgErrorCode,
                        (long)GeneratorObjectsError.SectionNotCorrect,
                        string.Concat(ExceptionsMessages.MsgSectionNotCorrect, Environment.NewLine, ex.Message)
                    )
                );
            }
        }

        private GeneratorObjectsManager()
        {
        }

        public static GeneratorObjectsSection Config
        {
            get { return _config; }
        }

    }

}
