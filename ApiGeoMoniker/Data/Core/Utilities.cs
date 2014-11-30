using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Data.Core
{
    public class Utilities
    {
        public Utilities()
        {

        }

        public void QuitNullValueOfTable(ref DataTable t)
        {
            foreach (DataRow row in t.Rows)
            {
                for (int i = 0; i < t.Columns.Count; i++)
                {
                    var test = row[i].GetType();
                    if (row[i] == DBNull.Value)
                        row[i] = "";
                }
            }
        }
    }
}
