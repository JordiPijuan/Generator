using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Generator.UI.Objects.Managers;
using Objects.Generator.Core.Configuration;
using Objects.Generator.Core.Entities;
using Objects.Generator.Core.Enumerations;
using Objects.Generator.Core.Managers;

namespace Generator.UI.Objects.Forms
{
    public partial class Preview : Form
    {

        private readonly Dictionary<string, List<KeyValuePair<string, string>>> CodeCollection;

        public Preview(Dictionary<string, List<KeyValuePair<string, string>>> codeCollection)
        {
            InitializeComponent();
            CodeCollection = codeCollection;
            lblCode.Text = string.Format(lblCode.Text, (OutputCode)GeneratorObjectsManager.Config.Namespaces.Languaje);
        }

        private void Preview_Load(object sender, EventArgs e)
        {
            TreeviewManager.GetGeneratorCode(CodeCollection, ref tvObjects);
        }

        private void tvObjects_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(e.Node.Tag != null)
                txtCode.Text = ((NodeFile)e.Node.Tag).FileText;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var file = (NodeFile)tvObjects.SelectedNode.Tag;

            file.FileText = txtCode.Text;

            CodeFileManager.CreateFile(file.FilePath, file.FileText);
        }

    }

}
