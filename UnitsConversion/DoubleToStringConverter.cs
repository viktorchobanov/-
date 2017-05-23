using System;
using System.Windows.Data;

namespace UnitsConversion
{
  [ValueConversion(typeof(double), typeof(string))]
  public class DoubleToStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType,
      object parameter, System.Globalization.CultureInfo culture)
    {
      return string.Format("{0:F3}", (double) value);
    }

    public object ConvertBack(object value, Type targetType,
      object parameter, System.Globalization.CultureInfo culture)
    {
      double d;
      if (double.TryParse((string)value, out d))
        return d;
      return 0.0;
    }
  }
}