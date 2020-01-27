using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using PathFinderToo.Logic;

namespace PathFinderToo.Vm.Converters
{
    public class VisualTypeToFillConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(!(value is VisualSquareType))
            {
                Debug.WriteLine($"Trying to convert {value} of type {value.GetType()} to {targetType} unsuccessfully.");
                return null;
            }

            return ((VisualSquareType)value).GetColor();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
