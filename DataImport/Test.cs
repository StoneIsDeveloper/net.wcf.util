using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImport
{
    public class Test
    {
        public void Run()
        {
            Product[] models = new Product[] {
               new Product{Id =1, Name = "aaa",Price = 1M},
               new Product{Id =2, Name = "BBB",Price = 2M}
            };
            var list = models.ToList();
            new ProductDataBase().SaveRecords(list);
        }
    }
}
