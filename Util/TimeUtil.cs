using System;

public static class TimeUtil
{
    public static string NowTime()
    {
        var hour = DateTime.Now.Hour;
        var minute = DateTime.Now.Minute;
        var second = DateTime.Now.Second;
        var year = DateTime.Now.Year;
        var month = DateTime.Now.Month;
        var day = DateTime.Now.Day;

        //格式化显示当前时间
        return string.Format("{0:D4}{1:D2}{2:D2}_" + "{3:D2}{4:D2}{5:D2}", year, month, day, hour, minute, second);
    }
}