namespace Objects.Generator.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Objects.Generator.Core.Configuration;
    using Objects.Generator.Core.Configuration.Elements;
    using Objects.Generator.Core.Decorators;
    using Objects.Generator.Core.Entities;
    using Objects.Generator.Core.Enumerations;
    using Objects.Generator.Core.Managers;

    public class GeneratorService
    {

        #region · Properties

        private static GeneratorObjectsSection Config { get { return GeneratorObjectsManager.Config; } }
        private NamespaceElement ElementNamespace { get; set; }
        private List<KeyValuePair<string, string>> Collection { get; set; }
        const string tab = "\t";
        const string spaces = "    ";

        #endregion Properties

        public GeneratorService(NamespaceElement _namespace)
        {
            ElementNamespace = _namespace;
        }

        public void GetData(out List<KeyValuePair<string, string>> collection, out List<Table> tables)
        {
            var codeCollection = new List<KeyValuePair<string, string>>();
            var listTables = new SqlManager().GetTableList();

            tables = new List<Table>();
            collection = new List<KeyValuePair<string, string>>();

            if (listTables.Any())
            {
                Generate(listTables, out codeCollection);

                tables = listTables;
                collection = codeCollection;
            }
        }

        private void Generate(List<Table> tableList, out List<KeyValuePair<string, string>> collection)
        {
            var generateType = (NamespaceType)Enum.Parse(typeof(NamespaceType), ElementNamespace.Type);
            var codeCollection = new List<KeyValuePair<string, string>>();

            switch (generateType)
            {
                case NamespaceType.Entity:
                    GenerateEntities(tableList);
                    codeCollection.AddRange(GetTextEntities(tableList));
                    break;
                case NamespaceType.GenericRepositoryCollection:
                    GenerateGenericRepositoryCollection(tableList);
                    codeCollection.AddRange(GetTextGenericRepositoryCollection(tableList));
                    break;
                case NamespaceType.GenericRepository:
                    GenerateGenericRepository();
                    codeCollection.Add(GetTextGenericRepository());
                    break;
                case NamespaceType.GenericInterface:
                    GenerateGenericInterface();
                    codeCollection.Add(GetTextGenericInterface());
                    break;
            }

            collection = codeCollection;
        }

        private void GenerateEntities(List<Table> tableList)
        {
            tableList.ForEach(t =>
            {
                CodeStringManager.CreateCodeFile(
                    string.Format(ElementNamespace.DestinyPath, Application.StartupPath, t.Name, ((OutputExtension)Config.Namespaces.Languaje).ToString()),
                    new EntityGenerator(ElementNamespace, t).Generate(),
                    (int)Config.Namespaces.Languaje
                );

                if (ElementNamespace.GeneratorOptions.ClassEntity.WithMetadata)
                {
                    var name = string.Format(ElementNamespace.GeneratorOptions.ClassEntity.ClassMetadata.Sufix, t.Name);

                    CodeStringManager.CreateCodeFile(
                        string.Format(ElementNamespace.DestinyPath, Application.StartupPath, name, ((OutputExtension)Config.Namespaces.Languaje).ToString()),
                        new EntityGenerator(ElementNamespace, t).GenerateMetadata(),
                        (int)Config.Namespaces.Languaje
                    );
                }
            });
        }

        private void GenerateGenericRepositoryCollection(List<Table> tableList)
        {
            tableList.ForEach(t =>
                CodeStringManager.CreateCodeFile(
                    string.Format(ElementNamespace.DestinyPath, Application.StartupPath, string.Format(ElementNamespace.GeneratorOptions.ClassEntity.Sufix, t.Name), ((OutputExtension)Config.Namespaces.Languaje).ToString()),
                    new GenericRepositoryCollectionGenerator(ElementNamespace, t).Generate(),
                    (int)Config.Namespaces.Languaje
                )
            );
        }

        private void GenerateGenericRepository()
        {
            CodeStringManager.CreateCodeFile(
                string.Format(ElementNamespace.DestinyPath, Application.StartupPath, GeneratedType.Repository.ToString(), ((OutputExtension)Config.Namespaces.Languaje).ToString()),
                new GenericRepositoryGenerator(ElementNamespace, false, ElementNamespace.GeneratorOptions.WithImplementation).Generate(),
                (int)Config.Namespaces.Languaje
            );
        }

        private void GenerateGenericInterface()
        {
            CodeStringManager.CreateCodeFile(
                string.Format(ElementNamespace.DestinyPath, Application.StartupPath, GeneratedType.IRepository.ToString(), ((OutputExtension)Config.Namespaces.Languaje).ToString()),
                new GenericRepositoryGenerator(ElementNamespace, true).Generate(),
                (int)Config.Namespaces.Languaje
            );
        }

        private List<KeyValuePair<string, string>> GetTextEntities(List<Table> tableList)
        {
            var listEntities = new List<KeyValuePair<string, string>>();

            tableList.ForEach(table =>
            {
                var pair = 
                    new KeyValuePair<string, string>(
                        string.Format(ElementNamespace.DestinyPath, Application.StartupPath, table.Name, ((OutputExtension)Config.Namespaces.Languaje).ToString()), 
                        CodeStringManager.GenerateCode(
                            new EntityGenerator(ElementNamespace, table).Generate(),
                            (int)Config.Namespaces.Languaje
                        )
                        .Replace(tab, spaces)
                    );

                listEntities.Add(pair);

                if (ElementNamespace.GeneratorOptions.ClassEntity.WithMetadata)
                {
                    var name = string.Format(ElementNamespace.GeneratorOptions.ClassEntity.ClassMetadata.Sufix, table.Name);
                    var pairMetaData =
                        new KeyValuePair<string, string>(
                            string.Format(ElementNamespace.DestinyPath, Application.StartupPath, name, ((OutputExtension)Config.Namespaces.Languaje).ToString()),
                            CodeStringManager.GenerateCode(
                                new EntityGenerator(ElementNamespace, table).GenerateMetadata(),
                                (int)Config.Namespaces.Languaje
                            )
                            .Replace(tab, spaces)
                        );

                    listEntities.Add(pairMetaData);
                }
            });

            return listEntities;
        }

        private List<KeyValuePair<string, string>> GetTextGenericRepositoryCollection(List<Table> tableList)
        {
            var listRepositories = new List<KeyValuePair<string, string>>();

            tableList.ForEach(t =>
            {
                var pair =
                    new KeyValuePair<string, string>(
                        string.Format(ElementNamespace.DestinyPath, Application.StartupPath, string.Format(ElementNamespace.GeneratorOptions.ClassEntity.Sufix, t.Name), ((OutputExtension)Config.Namespaces.Languaje).ToString()),
                        CodeStringManager.GenerateCode(
                            new GenericRepositoryCollectionGenerator(ElementNamespace, t).Generate(),
                            (int)Config.Namespaces.Languaje
                        )
                        .Replace(tab, spaces)
                    );

                listRepositories.Add(pair);
            });

            return listRepositories;
        }

        private KeyValuePair<string, string> GetTextGenericRepository()
        {
            var textRepository = 
                new KeyValuePair<string, string>(
                    string.Format(ElementNamespace.DestinyPath, Application.StartupPath, GeneratedType.Repository.ToString(), ((OutputExtension)Config.Namespaces.Languaje).ToString()),
                    CodeStringManager.GenerateCode(
                        new GenericRepositoryGenerator(
                            ElementNamespace,
                            false,
                            ElementNamespace.GeneratorOptions.WithImplementation
                            ).Generate(),
                            (int)Config.Namespaces.Languaje
                        )
                        .Replace(tab, spaces)
                    );

            return textRepository;
        }

        private KeyValuePair<string, string> GetTextGenericInterface()
        {
            var textInterface =
                new KeyValuePair<string, string>(
                    string.Format(ElementNamespace.DestinyPath, Application.StartupPath, GeneratedType.IRepository.ToString(), ((OutputExtension)Config.Namespaces.Languaje).ToString()),                    
                    CodeStringManager.GenerateCode(
                        new GenericRepositoryGenerator(
                            ElementNamespace,
                            true
                            ).Generate(),
                            (int)Config.Namespaces.Languaje
                        )
                        .Replace(tab, spaces)
                    );

            return textInterface;
        }

    }

}
