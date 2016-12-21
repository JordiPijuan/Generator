using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Generator.UI.Objects.Managers
{
    public class ListViewManager
    {

        public static void CreateColumns(ListView listView, IEnumerable<string> listColumns)
        {
            listView.Columns.Clear();

            foreach(var column in listColumns)
            {
                var values = column.Split(';');

                listView.Columns.Add(values[0], Convert.ToInt32(values[1]));
            }
        }

    }

}
