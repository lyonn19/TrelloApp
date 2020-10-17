using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace TrelloApp.Converters
{
    public class LabelMaxLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string labelText = value as string;

            if (labelText == null)
                return value;

            const int maxLength = 20;

            if (labelText.Length > maxLength)
                return labelText.Substring(0, maxLength) + "...";
            else
                return labelText;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
