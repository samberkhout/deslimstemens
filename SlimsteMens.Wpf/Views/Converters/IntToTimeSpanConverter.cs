using System;
using System.Globalization;
using System.Windows.Data;

namespace SlimsteMens.Wpf.Views.Converters;

public class IntToTimeSpanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object? parameter, CultureInfo culture)
        => value is int i ? TimeSpan.FromSeconds(i) : TimeSpan.Zero;

    public object ConvertBack(object value, Type targetType, object? parameter, CultureInfo culture)
        => value is TimeSpan ts ? (int)ts.TotalSeconds : 0;
}
