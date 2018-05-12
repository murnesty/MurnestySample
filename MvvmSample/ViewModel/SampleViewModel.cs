using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using MvvmSample.Model;
using MvvmSample.Reader;

namespace MvvmSample.ViewModel
{
    public class SampleViewModel : ViewModelBase
    {
        public SampleViewModel()
        {
            // binding & event
            Text1 = "Test";
            MouseEnterTextBlockCommand = new RelayCommand(MouseEnterTextBlockMethod);
            MouseLeaveTextBlockCommand = new RelayCommand(MouseLeaveTextBlockMethod);
            ButtonEnterCommand = new RelayCommand<String>(ButtonEnterMethod);
            ButtonLeaveCommand = new RelayCommand<String>(ButtonLeaveMethod);
            Text1Brush = null;

            // data grid
            ReadNameCommand = new RelayCommand(ReadNameMethod);

            // control
            LoadNameEntryCommand = new RelayCommand(LoadNameEntryMethod);
        }

        #region binding & event
        private String _text1; public String Text1 { get { return _text1; } set { Set(() => Text1, ref _text1, value); } }
        private Brush _text1Brush; public Brush Text1Brush { get { return _text1Brush; } set { Set(() => Text1Brush, ref _text1Brush, value); } }
        public ICommand MouseEnterTextBlockCommand { get; set; }
        public ICommand MouseLeaveTextBlockCommand { get; set; }
        public ICommand ButtonEnterCommand { get; set; }
        public ICommand ButtonLeaveCommand { get; set; }

        private void MouseEnterTextBlockMethod()
        {
            Text1 = "Mouse Enter";
        }
        private void MouseLeaveTextBlockMethod()
        {
            Text1 = "Mouse Leave";
        }
        private void ButtonEnterMethod(String arg)
        {
            try
            {
                Text1 = "Mouse Enter with " + arg;
                Text1Brush = (Brush)System.ComponentModel.TypeDescriptor.GetConverter(
                    typeof(Brush)).ConvertFromInvariantString(arg);
            }
            catch { }
        }
        private void ButtonLeaveMethod(String arg)
        {
            Text1 = "Mouse Leave with " + arg;
        }
        #endregion

        #region datagrid
        private ObservableCollection<NameSelectionEntry> _nameEntries;
        public ObservableCollection<NameSelectionEntry> NameEntries
        {
            get { return _nameEntries; }
            set { Set(() => NameEntries, ref _nameEntries, value); }
        }
        private ListCollectionView _nameView;
        public ListCollectionView NameView
        {
            get { return _nameView; }
            set { Set(() => NameView, ref _nameView, value); }
        }
        public ICommand ReadNameCommand { get; set; }
        private NameSelectionEntry _selectedNameEntry;
        public NameSelectionEntry SelectedNameEntry
        {
            get { return _selectedNameEntry; }
            set
            {
                Set(() => SelectedNameEntry, ref _selectedNameEntry, value);
                UpdateSelectedNameEntry();
            }
        }
        private String _selectedName;
        public String SelectedName
        {
            get { return _selectedName; }
            set { Set(() => SelectedName, ref _selectedName, value); }
        }
        private Int32 _selectedId;
        public Int32 SelectedId
        {
            get { return _selectedId; }
            set { Set(() => SelectedId, ref _selectedId, value); }
        }
        private Boolean _selectedSelected;
        public Boolean SelectedSelected
        {
            get { return _selectedSelected; }
            set { Set(() => SelectedSelected, ref _selectedSelected, value); }
        }

        private void ReadNameMethod()
        {
            NameEntries = new ObservableCollection<NameSelectionEntry>(NameReader.GetNameEntries());
            if (NameEntries == null)
                return;
            NameView = CollectionViewSource.GetDefaultView(NameEntries) as ListCollectionView;
            for (int i = 0; i < NameEntries.Count; i++)
            {
                NameSelectionEntry entry = NameEntries[i] as NameSelectionEntry;
                if (entry == null)
                    continue;

                entry.PropertyChanged += NameEntryPropertyChanged;
            }
        }
        private void NameEntryPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateSelectedNameEntry();
        }
        private void UpdateSelectedNameEntry()
        {
            SelectedSelected = SelectedNameEntry.IsSelected;
            SelectedId = SelectedNameEntry.ID;
            SelectedName = SelectedNameEntry.Name;
        }
        #endregion

        #region control
        private NameSelectionEntry _nameEntry;
        public NameSelectionEntry NameEntry
        {
            get { return _nameEntry; }
            set { Set(() => NameEntry, ref _nameEntry, value); }
        }
        public ICommand LoadNameEntryCommand { get; set; }
        private void LoadNameEntryMethod()
        {
            NameEntry = new NameSelectionEntry
            {
                Name = "Kanasai",
                IsSelected = true
            };
        }
        #endregion
    }
}
