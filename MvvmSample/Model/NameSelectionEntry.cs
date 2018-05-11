using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace MvvmSample.Model
{
    // ObservableObject needed to raise property changed event
    public class NameSelectionEntry : ObservableObject
    {
        private static Int32 IteratingId { get; set; }
        private Boolean _isSelected;
        public Boolean IsSelected
        {
            get { return _isSelected; }
            set { Set(() => IsSelected, ref _isSelected, value); }
        }
        private Int32 _id;
        public Int32 ID
        {
            get { return _id; }
            set { Set(() => ID, ref _id, value); }
        }
        private String _name;
        public String Name
        {
            get { return _name; }
            set { Set(() => Name, ref _name, value); }
        }

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
