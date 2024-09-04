using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace lnsystem_Study03_ICD_ItemTemplate_.Converters
{
    public class BooleanToHorizontalAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isLocal)
            {
                return isLocal ? HorizontalAlignment.Right : HorizontalAlignment.Left;
            }
            return HorizontalAlignment.Right;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}