# PersianDate.Net7
WPF Persian Calendar and Date Picker Control For .net 7

# Introduction
The source code consists of three main projects: PersianDate, which is a type (structure actually) for holding values of the Persian calendar, PersianDateControls, which has two WPF controls for Persian calendar: PersianCalendar and PersianDatePicker (these controls are pretty much like the ones found in the WPF control library: Calendar and DatePicker; these two controls use the type PersianDate to work with values of the Persian calendar), and the third project is a simple demo that demonstrates using these controls.

# About the Persian Calendar
The Persian calendar is a sonar calendar, like Gregorian calendar, but there are some differences. One is that the origins are different, and the Persian calendar's origin is about 621 years after Gregorian calendar's; another one is that Persian calendar's first day of year is March 21; and probably the most important one is that the average length of a Persian calendar year is different from that of a Gregorian calendar: the Persian calendar has 8 leap years (that is a year that has an extra day than normal years) in each 33 years, whereas the Gregorian calendar has 8 leap years in each 32 years. This little difference means that Persian dates cannot be calculated directly from Gregorian dates.

# The PersianDate Structure
The PersianDate structure stores dates of the Persian calendar. It is somehow similar to the DateTime structure in the .NET Class Library, except that PersianDate only stores the date, and not the time. This structure has only one field, which stores the number of days passed since the first day of the first year of the Persian calendar(1/1/1):

# How to Use the Code
If you just want to use PersianDate, you can either add its project to your solution, or build it and reference the assembly, or download the demo and reference PersianDate.dll.
If you want to use the WPF controls, you should either add both projects (PersianDate and PersianDateControls) to your solution, or build them both and reference them both, or download the demo and reference PersianDate.dll and PersianDateControls.dll.
