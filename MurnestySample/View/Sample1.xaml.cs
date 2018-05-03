using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MurnestySample.View
{
    /// <summary>
    /// Interaction logic for Sample1.xaml
    /// </summary>
    public partial class Sample1 : Window
    {
        private Int32 Iterator { get; set; } = 1;

        public Sample1()
        {
            InitializeComponent();

            // test
            EventTest();
            InitResourceDictionary();
        }

        #region event declaration
        private void EventTest()
        {
            // tie event in code rather than xaml design
            Button2.Click += new System.Windows.RoutedEventHandler(Button1_Click);
        }
        private void Button1_Click(Object sender, RoutedEventArgs e)
        {
            TextBlock1.Text = (Iterator++).ToString();
        }
        #endregion

        #region control property change
        private void Button2_Click(Object sender, RoutedEventArgs e)
        {
            Button2.Height = 100;
            if (Button2.Background == Brushes.LightGreen)
                Button2.Background = Brushes.LightPink;
            else if (Button2.Background == Brushes.LightPink)
                Button2.Background = (Brush)System.ComponentModel.TypeDescriptor.GetConverter(
                    typeof(Brush)).ConvertFromInvariantString("Blue");
            else
                Button2.Background = Brushes.LightGreen;
        }
        #endregion

        #region resource dictionary
        private Color Color1 { get; set; } = new Color { A = 255, R = 255, G = 0, B = 0 };
        private Color Color2 { get; set; } = new Color { A = 255, R = 0, G = 255, B = 0 };
        private Color Color3 { get; set; } = new Color { A = 255, R = 0, G = 0, B = 255 };
        private ResourceDictionary DList { get; set; }
        private Color CurrentColor { get; set; }
        private void InitResourceDictionary()
        {
            DList = new ResourceDictionary {{"c1", Color1}, {"c2", Color2}, {"c3", Color3}};

            CurrentColor = (Color)DList["c1"];
        }
        private void Button6_OnClick(Object sender, RoutedEventArgs e)
        {
            if(CurrentColor == (Color)DList["c1"])
                CurrentColor = (Color)DList["c2"];
            else if (CurrentColor == (Color)DList["c2"])
                CurrentColor = (Color)DList["c3"];
            else
                CurrentColor = (Color)DList["c1"];

            SolidColorBrush b = new SolidColorBrush {Color = CurrentColor };
            Button6.Background = b;
        }
        #endregion

        #region button
        private void OpenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TextBlock2.Text = "Invoked open command";
        }
        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TextBlock2.Text = "Invoked save command";
        }
        #endregion

    }
}
