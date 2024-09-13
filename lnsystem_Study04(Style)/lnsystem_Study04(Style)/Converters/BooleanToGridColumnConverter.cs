using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace lnsystem_Study04_Style_.Converters
{
    /// <summary>
    /// Boolean 값을 Grid의 Column 인덱스로 변환하는 컨버터입니다.
    /// </summary>
    public class BooleanToGridColumnConverter : IValueConverter
    {
        #region IValueConverter 구현

        /// <summary>
        /// Boolean 값을 Grid의 Column 인덱스로 변환합니다.
        /// </summary>
        /// <param name="value">변환할 값</param>
        /// <param name="targetType">대상 형식</param>
        /// <param name="parameter">추가 매개변수</param>
        /// <param name="culture">문화 정보</param>
        /// <returns>변환된 Column 인덱스</returns>
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isLocal)
            {
                return isLocal ? 2 : 0;
            }
            return 0;
        }

        /// <summary>
        /// Grid의 Column 인덱스를 Boolean으로 변환하는 것은 지원되지 않습니다.
        /// </summary>
        /// <param name="value">변환할 값</param>
        /// <param name="targetType">대상 형식</param>
        /// <param name="parameter">추가 매개변수</param>
        /// <param name="culture">문화 정보</param>
        /// <returns>지원되지 않음</returns>
        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            MessageBox.Show("Grid의 Column 인덱스를 Boolean으로 변환하는 것은 지원되지 않습니다.", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
            return DependencyProperty.UnsetValue;
        }

        #endregion
    }
}