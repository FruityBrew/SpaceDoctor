using System;
using System.Windows.Data;

namespace SpaceDoctor.ViewModel
{
    /// <summary>
    /// Конвертер даты в формат "dd.MM.yyyy"
    /// </summary>
    [ValueConversion(typeof(DateTime), typeof(string))]
    public class XDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                DateTime date = (DateTime)value;
                return date.ToString("dd.MM.yyyy");
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string strValue = value as string;
            DateTime resultDateTime;
            if (DateTime.TryParse(strValue, out resultDateTime))
            {
                return resultDateTime;
            }
            return null;
        }
    }
}
