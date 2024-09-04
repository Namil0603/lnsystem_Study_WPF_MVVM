using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace lnsystem_Study03_ICD_ItemTemplate_.Converters
{
    public class BooleanToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isLocal)
            {
                return isLocal ? Brushes.LightBlue : Brushes.LightGray;
            }
            return Brushes.LightGray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}