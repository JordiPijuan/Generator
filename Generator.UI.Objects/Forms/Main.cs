using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Generator.UI.Objects.Managers;
using Objects.Generator.Core.Configuration;
using Objects.Generator.Core.Configuration.Elements;
using Objects.Generator.Core.Entities;
using Objects.Generator.Core.Services;

namespace Generator.UI.Objects.Forms
{

    public partial class Main : Form
    {

        public Dictionary<string, List<KeyValuePair<string, string>>> CodeCollection { get; set; }
        public List<Table> TableCollection { get; set; }

        public Main()
        {
            try
            {
                InitializeComponent();
                InitializeService();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        private void InitializeService()
        {
            try
            {
                CodeCollection = new Dictionary<string, List<KeyValuePair<string, string>>>();
                TableCollection = new List<Table>();
                var collection = new List<KeyValuePair<string, string>>();
                var tables = new List<Table>();

                GeneratorObjectsManager.Config.Namespaces
                    .Cast<NamespaceElement>()
                    .ToList()
                    .FindAll(n => n.Enabled)
                    .ForEach(
                        ns =>
                        {
                            var service = new GeneratorService(ns);
                            service.GetData(out collection, out tables);

                            CodeCollection.Add(ns.Type, collection);

                            if(tables.Any() && !TableCollection.Any())
                                TableCollection.AddRange(tables);
                        });

                if (GeneratorObjectsManager.Config.Namespaces.Automatic)
                    new Preview(CodeCollection) {MdiParent = this}.Show();
                else 
                    new SelectForm(TableCollection, CodeCollection) {MdiParent = this}.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Preview(CodeCollection) { MdiParent = this }.Show();
        }

        private void selectObjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SelectForm(TableCollection, CodeCollection) { MdiParent = this }.Show();
        }
    }

}
