using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace PersianDateControls;

[DefaultEvent("SelectedDateChanged")]
[DefaultProperty("SelectedDate")]
public partial class PersianDatePicker : UserControl
{
    public static readonly DependencyProperty SelectedDateProperty;
    public static readonly DependencyProperty DisplayDateProperty;
    public static readonly DependencyProperty DisplayDateStartProperty;
    public static readonly DependencyProperty DisplayDateEndProperty;
    public static readonly DependencyProperty TextProperty;

    //events

    public static readonly RoutedEvent SelectedDateChangedEvent;

    static PersianDatePicker()
    {
        var selectedDateMetadata = new PropertyMetadata(PersianDate.PersianDate.Today, selectedDateChanged);
        selectedDateMetadata.CoerceValueCallback = coerceDateToBeInRange;
        SelectedDateProperty =
            DependencyProperty.Register("SelectedDate", typeof(PersianDate.PersianDate), typeof(PersianDatePicker),
                selectedDateMetadata);

        var displayDateMetadata = new PropertyMetadata(PersianDate.PersianDate.Today);
        displayDateMetadata.CoerceValueCallback = coerceDateToBeInRange;
        DisplayDateProperty =
            DependencyProperty.Register("DisplayDate", typeof(PersianDate.PersianDate), typeof(PersianDatePicker),
                displayDateMetadata);

        var textMetadata = new PropertyMetadata(PersianDate.PersianDate.Today.ToString());
        TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(PersianDatePicker), textMetadata);

        var displayDateStartMetaData = new PropertyMetadata(new PersianDate.PersianDate());
        DisplayDateStartProperty =
            DependencyProperty.Register("DisplayDateStart", typeof(PersianDate.PersianDate), typeof(PersianDatePicker),
                displayDateStartMetaData);

        var displayDateEndMetaData = new PropertyMetadata(new PersianDate.PersianDate(10000, 1, 1));
        displayDateEndMetaData.CoerceValueCallback = coerceDisplayDateEnd;
        DisplayDateEndProperty =
            DependencyProperty.Register("DisplayDateEnd", typeof(PersianDate.PersianDate), typeof(PersianDatePicker),
                displayDateEndMetaData);

        SelectedDateChangedEvent =
            EventManager.RegisterRoutedEvent("SelectedDateChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                typeof(PersianDatePicker));
    }

    public PersianDatePicker()
    {
        InitializeComponent();
        setBindings();
        Text = SelectedDate.ToString();

        //this is for closing the popup when a date is selected using PersianCalendar
        foreach (var monthModeButton in persianCalendar.monthModeButtons)
            monthModeButton.Click += delegate { persianCalnedarPopup.IsOpen = false; };
    }

    [Category("Date Picker")]
    public PersianDate.PersianDate SelectedDate
    {
        get => (PersianDate.PersianDate)GetValue(SelectedDateProperty);
        set => SetValue(SelectedDateProperty, value);
    }

    /// <summary>
    ///     Gets or sets the date that is being displayed in the calendar.
    /// </summary>
    [Category("Date Picker")]
    public PersianDate.PersianDate DisplayDate
    {
        get => (PersianDate.PersianDate)GetValue(DisplayDateProperty);
        set => SetValue(DisplayDateProperty, value);
    }

    /// <summary>
    ///     the minimum date that is displayed, and can be selected
    /// </summary>
    [Category("Date Picker")]
    public PersianDate.PersianDate DisplayDateStart
    {
        get => (PersianDate.PersianDate)GetValue(DisplayDateStartProperty);
        set => SetValue(DisplayDateStartProperty, value);
    }


    /// <summary>
    ///     the maximum date that is displayed, and can be selected
    /// </summary>
    [Category("Date Picker")]
    public PersianDate.PersianDate DisplayDateEnd
    {
        get => (PersianDate.PersianDate)GetValue(DisplayDateEndProperty);
        set => SetValue(DisplayDateEndProperty, value);
    }


    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    ///     This readonly property gets the PersianCalendar object displayed when clicking the Calendar button.
    /// </summary>
    public PersianCalendar PersianCalendar => persianCalendar;

    public event RoutedEventHandler SelectedDateChanged
    {
        add => AddHandler(SelectedDateChangedEvent, value);
        remove => RemoveHandler(SelectedDateChangedEvent, value);
    }

    //property changed callbacks and value coercions
    private static object coerceDisplayDateEnd(DependencyObject d, object o)
    {
        var pdp = d as PersianDatePicker;
        var value = (PersianDate.PersianDate)o;
        if (value < pdp.DisplayDateStart) return pdp.DisplayDateStart;
        return o;
    }

    private static object coerceDateToBeInRange(DependencyObject d, object o)
    {
        var pdp = d as PersianDatePicker;
        var value = (PersianDate.PersianDate)o;
        if (value < pdp.DisplayDateStart) return pdp.DisplayDateStart;
        if (value > pdp.DisplayDateEnd) return pdp.DisplayDateEnd;
        return o;
    }

    private static void selectedDateChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
        var pdp = o as PersianDatePicker;
        pdp.Text = e.NewValue.ToString();
        pdp.RaiseEvent(new RoutedEventArgs(SelectedDateChangedEvent, pdp));
    }

    private void setBindings()
    {
        var selectedDateBinding = new Binding
        {
            Source = this,
            Path = new PropertyPath("SelectedDate"),
            Mode = BindingMode.TwoWay
        };
        persianCalendar.SetBinding(PersianCalendar.SelectedDateProperty, selectedDateBinding);

        var displayDateBinding = new Binding
        {
            Source = this,
            Path = new PropertyPath("DisplayDate"),
            Mode = BindingMode.TwoWay
        };
        persianCalendar.SetBinding(PersianCalendar.DisplayDateProperty, displayDateBinding);

        var textBinding = new Binding
        {
            Source = this,
            Path = new PropertyPath("Text"),
            Mode = BindingMode.TwoWay
        };
        dateTextBox.SetBinding(TextBox.TextProperty, textBinding);

        var displayDateStartBinding = new Binding
        {
            Source = persianCalendar,
            Path = new PropertyPath("DisplayDateStart"),
            Mode = BindingMode.TwoWay
        };
        SetBinding(DisplayDateStartProperty, displayDateStartBinding);

        var displayDateEndBinding = new Binding
        {
            Source = persianCalendar,
            Path = new PropertyPath("DisplayDateEnd"),
            Mode = BindingMode.TwoWay
        };
        SetBinding(DisplayDateEndProperty, displayDateEndBinding);
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        persianCalnedarPopup.IsOpen = true;
    }

    private void validateText()
    {
        PersianDate.PersianDate date;
        if (PersianDate.PersianDate.TryParse(dateTextBox.Text, out date))
        {
            SelectedDate = date;
            DisplayDate = date;
        }

        Text = SelectedDate.ToString();
    }

    private void dateTextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        validateText();
    }

    private void dateTextBox_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Return)
            validateText();
    }

    private void persianCalnedarPopup_Opened(object sender, EventArgs e)
    {
        persianCalendar.Focus();
    }
}