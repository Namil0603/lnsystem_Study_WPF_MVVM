using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace lnsystem_Study03_ICD_ItemTemplate_.Converters
{
    public class BooleanToGridColumnConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isSentByMe)
            {
                return isSentByMe ? 2 : 0;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}