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
using System.Windows.Media.Imaging;

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

            // image list
            Img1ButtonColor = Brushes.Gray;
            Img2ButtonColor = Brushes.Gray;
            Img3ButtonColor = Brushes.Gray;
            Img4ButtonColor = Brushes.Gray;
            ImgSelectorCommand = new RelayCommand<String>(ImgSelectorMethod);
            ImgFlipperCommand = new RelayCommand<String>(ImgFlipperMethod);
            ImgButtonEnterCommand = new RelayCommand<String>(ImgButtonEnterMethod);
            ImgButtonLeaveCommand = new RelayCommand(ImgButtonLeaveMethod);
            SelectImage(0);

            Init();
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

        #region image list
        private Brush _img1ButtonColor;
        public Brush Img1ButtonColor
        {
            get => _img1ButtonColor;
            set => Set(() => Img1ButtonColor, ref _img1ButtonColor, value);
        }
        private Brush _img2ButtonColor;
        public Brush Img2ButtonColor
        {
            get => _img2ButtonColor;
            set => Set(() => Img2ButtonColor, ref _img2ButtonColor, value);
        }
        private Brush _img3ButtonColor;
        public Brush Img3ButtonColor
        {
            get => _img3ButtonColor;
            set => Set(() => Img3ButtonColor, ref _img3ButtonColor, value);
        }
        private Brush _img4ButtonColor;
        public Brush Img4ButtonColor
        {
            get => _img4ButtonColor;
            set => Set(() => Img4ButtonColor, ref _img4ButtonColor, value);
        }

        private Int32 CurrImgIndex { get; set; } = 0;
        private ImageSource _imageSrc;
        public ImageSource ImageSrc
        {
            get => _imageSrc;
            set => Set(() => ImageSrc, ref _imageSrc, value);
        }
        private String[] ImgList =
        {
            @"pack://application:,,,/Resource/apple.jpg",
            @"pack://application:,,,/Resource/airballons.jpg",
            @"pack://application:,,,/Resource/bird.jpg",
            @"pack://application:,,,/Resource/pencils.jpg"
        };
        public ICommand ImgSelectorCommand { get; set; }
        private void ImgSelectorMethod(String arg)
        {
            if (!Int32.TryParse(arg, out Int32 index))
                return;
            SelectImage(index);
        }
        public ICommand ImgFlipperCommand { get; set; }
        private void ImgFlipperMethod(String arg)
        {
            switch (arg)
            {
                case "Prev":
                    if (--CurrImgIndex < 0)
                        CurrImgIndex = 0;
                    break;

                case "Next":
                    if (++CurrImgIndex >= ImgList.Length)
                        CurrImgIndex = ImgList.Length - 1;
                    break;
            }
            SelectImage(CurrImgIndex);
        }
        public ICommand ImgButtonEnterCommand { get; set; }
        private void ImgButtonEnterMethod(String arg)
        {
            if (!Int32.TryParse(arg, out Int32 index))
                return;
            SelectImage(index);
        }
        public ICommand ImgButtonLeaveCommand { get; set; }
        private void ImgButtonLeaveMethod()
        {
            SelectImage(CurrImgIndex);
        }
        private void SelectImage(Int32 index)
        {
            if (index > ImgList.Length || index < 0)
                return;
            CurrImgIndex = index;
            ImageSrc = new BitmapImage(new Uri(ImgList[index], UriKind.RelativeOrAbsolute));

            Img1ButtonColor = Brushes.Gray;
            Img2ButtonColor = Brushes.Gray;
            Img3ButtonColor = Brushes.Gray;
            Img4ButtonColor = Brushes.Gray;
            switch (index)
            {
                case 0: Img1ButtonColor = Brushes.LightBlue; break;
                case 1: Img2ButtonColor = Brushes.LightBlue; break;
                case 2: Img3ButtonColor = Brushes.LightBlue; break;
                case 3: Img4ButtonColor = Brushes.LightBlue; break;
            }
        }
        #endregion

        #region item control
        public class ButtonEntry
        {
            public String ButtonText { get; set; }
            public String ButtonClickCommandParameter { get; set; }
            public String ButtonMouseEnterCommandParameter { get; set; }

            public ICommand ButtonClickCommand { get; set; }
            public ICommand ButtonMouseEnterCommand { get; set; }
            public ICommand ButtonMouseLeaveCommand { get; set; }
        }
        public List<ButtonEntry> ItemControlSrc { get; set; }
        private void Init()
        {
            ItemControlSrc = new List<ButtonEntry>
            {
                new ButtonEntry { ButtonText="1", ButtonClickCommand = new RelayCommand<String>(ImgSelectorMethod), ButtonClickCommandParameter="0", ButtonMouseEnterCommand = new RelayCommand<String>(ImgButtonEnterMethod), ButtonMouseLeaveCommand = new RelayCommand(ImgButtonLeaveMethod), ButtonMouseEnterCommandParameter = "0"}, 
                new ButtonEntry { ButtonText="2", ButtonClickCommand = new RelayCommand<String>(ImgSelectorMethod), ButtonClickCommandParameter="1", ButtonMouseEnterCommand = new RelayCommand<String>(ImgButtonEnterMethod), ButtonMouseLeaveCommand = new RelayCommand(ImgButtonLeaveMethod), ButtonMouseEnterCommandParameter = "1"}, 
                new ButtonEntry { ButtonText="3", ButtonClickCommand = new RelayCommand<String>(ImgSelectorMethod), ButtonClickCommandParameter="2", ButtonMouseEnterCommand = new RelayCommand<String>(ImgButtonEnterMethod), ButtonMouseLeaveCommand = new RelayCommand(ImgButtonLeaveMethod), ButtonMouseEnterCommandParameter = "2"}, 
                new ButtonEntry { ButtonText="4", ButtonClickCommand = new RelayCommand<String>(ImgSelectorMethod), ButtonClickCommandParameter="3", ButtonMouseEnterCommand = new RelayCommand<String>(ImgButtonEnterMethod), ButtonMouseLeaveCommand = new RelayCommand(ImgButtonLeaveMethod), ButtonMouseEnterCommandParameter = "3"},  
            };
        }
        #endregion
    }
}
