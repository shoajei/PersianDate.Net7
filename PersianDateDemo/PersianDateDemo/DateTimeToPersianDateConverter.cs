using System;
using System.Globalization;
using System.Windows.Data;

namespace PersianDateDemo;

[ValueConversion(typeof(DateTime), typeof(PersianDate.PersianDate))]
public class DateTimeToPersianDateConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null) return null;
        var date = (DateTime)value;
        return new PersianDate.PersianDate(date);
    }

    public object? ConvertBack(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null) return null;
        var pDate = (PersianDate.PersianDate)value;
        return pDate.ToDateTime();
    }
}