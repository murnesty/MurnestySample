using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace MvvmSample.ViewModel
{
    public class SampleViewModel : ViewModelBase
    {
        private String _text1; public String Text1 { get { return _text1; } set { Set(() => Text1, ref _text1, value); } }
        private Brush _text1Brush; public Brush Text1Brush { get { return _text1Brush; } set { Set(() => Text1Brush, ref _text1Brush, value); } }
        public ICommand MouseEnterTextBlockCommand { get; set; }
        public ICommand MouseLeaveTextBlockCommand { get; set; }
        public ICommand ButtonEnterCommand { get; set; }
        public ICommand ButtonLeaveCommand { get; set; }

        public SampleViewModel()
        {
            Text1 = "Test";
            MouseEnterTextBlockCommand = new RelayCommand(MouseEnterTextBlockMethod);
            MouseLeaveTextBlockCommand = new RelayCommand(MouseLeaveTextBlockMethod);
            ButtonEnterCommand = new RelayCommand<String>(ButtonEnterMethod);
            ButtonLeaveCommand = new RelayCommand<String>(ButtonLeaveMethod);
            Text1Brush = null;
        }

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
            Text1 = "Mouse Enter with " + arg;
            Text1Brush = (Brush)System.ComponentModel.TypeDescriptor.GetConverter(
                    typeof(Brush)).ConvertFromInvariantString(arg);
        }
        private void ButtonLeaveMethod(String arg)
        {
            Text1 = "Mouse Leave with " + arg;
        }
    }
}
