using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MurnestySample.Converter
{
    public class ButtonColorConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            String colorText = value as String;
            if (colorText == null)
                return null;
            return (Brush)System.ComponentModel.TypeDescriptor.GetConverter(
                typeof(Brush)).ConvertFromInvariantString(colorText);
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
