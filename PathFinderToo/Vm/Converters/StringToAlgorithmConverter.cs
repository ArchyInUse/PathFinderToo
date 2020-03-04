using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using PathFinderToo.Logic;
using System.Windows.Controls;
using PathFinderToo.Logic.Algorithms;

namespace PathFinderToo.Vm.Converters
{
    public class StringToAlgorithmConverter : IValueConverter
    {
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = (ComboBoxItem)value;

            switch (item.Content)
            {
                case "None":
                    return AlgorithmType.None;
                case "A*":
                    return AlgorithmType.AStar;
                case "Djikstra's":
                    return AlgorithmType.Djikstras;
                default:
                    return AlgorithmType.None;
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = (AlgorithmType)value;
            switch (item)
            {
                case AlgorithmType.None:
                    return "None";
                case AlgorithmType.AStar:
                    return "A*";
                case AlgorithmType.Djikstras:
                    return "Djikstra's";
                default:
                    return "error";
            }
        }
    }
}
