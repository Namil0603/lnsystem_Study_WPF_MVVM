using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace lnsystem_Study03_ICD_ItemTemplate_.Converters
{
    /// <summary>
    /// Boolean 값을 Color로 변환하는 컨버터입니다.
    /// </summary>
    public class BooleanToColorConverter : IValueConverter
    {
        #region IValueConverter 구현

        /// <summary>
        /// Boolean 값을 Color로 변환합니다.
        /// </summary>
        /// <param name="value">변환할 값</param>
        /// <param name="targetType">대상 형식</param>
        /// <param name="parameter">추가 매개변수</param>
        /// <param name="culture">문화 정보</param>
        /// <returns>변환된 Color 값</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isLocal)
            {
                return isLocal ? Brushes.LightBlue : Brushes.LightGray;
            }
            return Brushes.LightGray;
        }

        /// <summary>
        /// Color 값을 Boolean으로 변환하는 것은 지원되지 않습니다.
        /// </summary>
        /// <param name="value">변환할 값</param>
        /// <param name="targetType">대상 형식</param>
        /// <param name="parameter">추가 매개변수</param>
        /// <param name="culture">문화 정보</param>
        /// <returns>지원되지 않음</returns>
        /// <exception cref="NotImplementedException">항상 발생</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}