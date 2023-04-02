using System.Globalization;

namespace PersianDateTests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        var pc = new PersianCalendar();
        var z = pc.ToDateTime(1, 1, 1, 0, 0, 0, 0);
        for (uint i = 0; i <= (DateTime.MaxValue - z).Days; i++)
        {
            int y, m, d;
            PersianDate.PersianDate.yearMonthDay(i, out y, out m, out d);
            var td = pc.AddDays(z, (int)i);
            int yy = pc.GetYear(td), mm = pc.GetMonth(td), dd = pc.GetDayOfMonth(td);
            if (yy != y || dd != d || mm != m || PersianDate.PersianDate.days(yy, mm, dd) != i ||
                !PersianDate.PersianDate.IsValid(y, m, d))
                Assert.Fail("this failed: {0}: {1}/{2}/{3},  {4}", i, y, m, d, PersianDate.PersianDate.days(y, m, d));
        }
    }

    [TestMethod]
    public void DayOfYearTests()
    {
        var pc = new PersianCalendar();
        var z = pc.ToDateTime(1, 1, 1, 0, 0, 0, 0);
        for (uint i = 0; i <= (DateTime.MaxValue - z).Days; i++)
        {
            var pd = new PersianDate.PersianDate(i);
            if (pc.GetDayOfYear(pd.ToDateTime()) != pd.DayOfYear)
                Assert.Fail("this failed: {0}: {1}, {2}, {3}", i, pd.ToString(), pd.DayOfYear,
                    pc.GetDayOfYear(pd.ToDateTime()));
        }
    }

    [TestMethod]
    public void DayOfWeekTests()
    {
        var pc = new PersianCalendar();

        var l = new List<PersianDate.PersianDate>();
        for (var pd = PersianDate.PersianDate.MinValue;
             pd <= new PersianDate.PersianDate(DateTime.MaxValue);
             pd = pd.AddDays(1))
        {
            var d = pc.ToDateTime(pd.Year, pd.Month, pd.Day, 0, 0, 0, 0);
            if (d.DayOfWeek != pd.DayOfWeek)
                l.Add(pd);
        }

        Assert.IsTrue(l.Count == 0);
    }

    [TestMethod]
    public void ParseTests()
    {
        for (var pd = PersianDate.PersianDate.MinValue;
             pd <= new PersianDate.PersianDate(10000, 1, 1);
             pd = pd.AddDays(1)) Assert.AreEqual(pd, PersianDate.PersianDate.Parse(pd.ToString()));

        Assert.IsFalse(PersianDate.PersianDate.TryParse("12/11/432", out _));
        Assert.IsFalse(PersianDate.PersianDate.TryParse("1390/1/0", out _));
        Assert.IsFalse(PersianDate.PersianDate.TryParse("1344/8/31", out _));
        Assert.IsFalse(PersianDate.PersianDate.TryParse("0/12/29", out _));
    }


    [TestMethod]
    public void ToStringTests()
    {
        var result = "";
        var today = DateTime.Today;
        var persianToday = PersianDate.PersianDate.Today;

        result += "\r\nToString: " + persianToday.ToDateTime();
        result += "\r\nToShortDateString: " + persianToday.ToDateTime().ToShortDateString();
        result += "\r\nToLongDateString: " + persianToday.ToDateTime().ToLongDateString();
        result += "\r\n---";
        result += "\r\nToString: " + new PersianDate.PersianDate(today);
        result += "\r\nToLongDateString: " + new PersianDate.PersianDate(today).ToLongDateString();
        result += "\r\nToLongDateString: " + persianToday.ToLongDateString();
        result += "\r\nMonthAsPersianMonth: " + new PersianDate.PersianDate(today).MonthAsPersianMonth;

        Assert.Inconclusive(result);
    }
}