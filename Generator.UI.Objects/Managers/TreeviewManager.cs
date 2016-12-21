namespace Generator.UI.Objects.Managers
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using global::Objects.Generator.Core.Entities;

    public class TreeviewManager
    {
        const char pathSeparator = '\\';

        public static void GetGeneratorCode(
            Dictionary<string, List<KeyValuePair<string, string>>> codeCollection,
            ref TreeView tree
            )
        {
            TreeView myTreeView = new TreeView();
            tree.Nodes.Clear();

            foreach (var code in codeCollection)
            {
                TreeNode parent = new TreeNode();
                parent.Text = code.Key;

                foreach (var type in code.Value)
                {
                    var info = new NodeFile()
                    {
                        FilePath = type.Key,
                        FileText = type.Value
                    };

                    TreeNode child = new TreeNode()
                    {
                        Text = type.Key.Substring(type.Key.LastIndexOf(pathSeparator) + 1),
                        Tag = info
                    };

                    parent.Nodes.Add(child);
                }

                tree.Nodes.Add(parent);
            }
        }

    }
}
