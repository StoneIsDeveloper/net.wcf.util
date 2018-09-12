using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImport
{
    public class ProductDataBase
    {
        public void SaveRecords(List<Product> list)
        {
            DataTable tb = new DataTable();
            tb.Columns.Add("Id", typeof(int));
            tb.Columns.Add("Name", typeof(string));
            tb.Columns.Add("Price", typeof(decimal));

            foreach (Product record in list)
            {
                DataRow row = tb.NewRow();
                tb.Rows.Add(row);
                row["Id"] = record.Id;
                row["Name"] = record.Name;
                row["Price"] = record.Price;
            }

            DataImportHelper.BulkInsert(tb, "[dbo].[Products]");
        }

    }
}
