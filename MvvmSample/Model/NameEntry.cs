using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmSample.Model
{
    public class NameEntry
    {
        private static Int32 IteratingId { get; set; }
        public Int32 ID { get; set; }
        public String Name { get; set; }

        static NameEntry()
        {
            IteratingId = 0;
        }
        public NameEntry()
        {
            ID = IteratingId++;
            Name = "";
        }
        public NameEntry(String name)
        {
            this.Name = name;
        }
    }
}
