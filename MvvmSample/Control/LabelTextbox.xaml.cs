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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MvvmSample.Control
{
    /// <summary>
    /// Interaction logic for LabelTextbox.xaml
    /// </summary>
    public partial class LabelTextbox : UserControl
    {
        // create property
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(String),
                typeof(LabelTextbox), new PropertyMetadata(""));
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(Object),
                typeof(LabelTextbox), new PropertyMetadata(""));

        // property
        public String Label
        {
            get { return GetValue(LabelProperty) as String; }
            set { SetValue(LabelProperty, value); }
        }
        public Object Value
        {
            get { return GetValue(ValueProperty) as Object; }
            set { SetValue(ValueProperty, value); }
        }

        public LabelTextbox()
        {
            InitializeComponent();
        }
    }
}
