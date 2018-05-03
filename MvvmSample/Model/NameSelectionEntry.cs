using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace MvvmSample.Model
{
    // ObservableObject needed to raise property changed event
    public class NameSelectionEntry
    {
        private static Int32 IteratingId { get; set; }
        public Boolean IsSelected { get; set; }
        public Int32 ID { get; set; }
        public String Name { get; set; }

        static NameSelectionEntry()
        {
            IteratingId = 0;
        }
        public NameSelectionEntry()
        {
            ID = IteratingId++;
            Name = "";
            IsSelected = false;
        }
        public NameSelectionEntry(String name)
        {
            ID = IteratingId++;
            this.Name = name;
            this.IsSelected = false;
        }
        public NameSelectionEntry(String name, Boolean isSelected)
        {
            ID = IteratingId++;
            this.Name = name;
            this.IsSelected = isSelected;
        }
        public override String ToString()
        {
            return ID.ToString("D4") + " " + (IsSelected?"(Y)":"(N)") + Name;
        }
    }
}
