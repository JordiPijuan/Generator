using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Objects.Generator.Core.Entities;

namespace Generator.UI.Objects.Forms
{
    public partial class SelectForm : Form
    {

        List<Table> TableList { get; set; }
        Dictionary<string, List<KeyValuePair<string, string>>> CodeCollection { get; set; }

        public SelectForm(List<Table> tableList, Dictionary<string, List<KeyValuePair<string, string>>> codeCollection)
        {
            InitializeComponent();
            TableList = tableList;
            CodeCollection = codeCollection;
            LoadTables();
        }

        private void lvTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = GetIndex(lvTables);

            if(lvTables.SelectedItems.Count > 0)
            {
                lvFields.Items.Clear();

                if(index >= 0)
                    LoadFields(lvTables.SelectedItems[0].Text);
            }
        }

        private void chkAllTables_CheckedChanged(object sender, EventArgs e)
        {
            var bCheck = ((CheckBox)sender).Checked;

            foreach(ListViewItem oItem in lvTables.Items)
                oItem.Checked = bCheck;

            TableList
                .ToList()
                .ForEach(tab =>
                {
                    tab.IsSelect = bCheck;
                    tab.FieldsList.ToList().ForEach(cam => cam.IsSelect = bCheck);
                });
        }

        private void lvTables_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            TableList.ElementAt(e.Item.Index).IsSelect = e.Item.Checked;
        }

        private void LoadTables()
        {
            TableList.ToList().ForEach(tab => lvTables.Items.Add(new ListViewItem(tab.Name)));
        }

        private void LoadFields(string table)
        {
            var targetTable = TableList.First(t => t.Name == table);

            targetTable.FieldsList
                .ToList()
                .ForEach(f =>
                {
                    var item = new ListViewItem(f.Name)
                    {
                        Checked = f.IsSelect
                    };

                    item.SubItems.Add(f.FieldType);

                    lvFields.Items.Add(item);
                });
        }

        private int GetIndex(ListView listView)
        {
            var list = listView.SelectedItems;
            var index = -1;

            foreach(ListViewItem item in list)
            {
                index = item.Index;

                if(index >= 0) break;
            }

            return index;
        }

        private void chkFields_CheckedChanged(object sender, EventArgs e)
        {
            var nIndex = GetIndex(lvTables);
            var bCheck = ((CheckBox)sender).Checked;

            foreach(ListViewItem oItem in lvFields.Items)
            {
                oItem.Checked = bCheck;
            }

            TableList.ElementAt(nIndex).FieldsList.ToList().ForEach(cam => cam.IsSelect = bCheck);
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            new Preview(CodeCollection) { MdiParent = this.MdiParent }.Show();
            this.Close();
        }

    }

}
