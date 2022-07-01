using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PB_Semester_Project
{
    public static class Utility
    {
        public static void initializeComboBox(String query, ComboBox combo)
        {
            var table = DataBaseProcessor.extractTable(query);
            foreach (DataRow row in table.Rows)
                combo.Items.Add(row[0]);
        }

    }
}
