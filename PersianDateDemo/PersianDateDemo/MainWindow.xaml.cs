using System;
using System.Windows;
using System.Windows.Media;

namespace PersianDateDemo;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        datePicker.DisplayDateStart = calendar.DisplayDateStart =
            new PersianDate.PersianDate(1, 1, 1).ToDateTime();
        persianDatePicker.DisplayDateEnd =
            persianCalendar.DisplayDateEnd = new PersianDate.PersianDate(DateTime.MaxValue);
        datePicker.DisplayDateEnd = calendar.DisplayDateEnd = DateTime.MaxValue;

        var originalBackground = Background;
        try
        {
            Background = new SolidColorBrush(new Color { A = 128, R = 255, G = 255, B = 255 });
            //AeroGlassEffectUtility.ExtendGlass(this, -1, -1, -1, -1);
        }
        catch (Exception)
        {
            Background = originalBackground;
        }
    }
}