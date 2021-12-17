using System;
namespace QQChannelFramework.Tools;

/// <summary>
/// 转换辅助类
/// </summary>
public static class ConvertHelper
{
    /// <summary>
    /// 16进制颜色值转换至10进制uint
    /// </summary>
    /// <param name="colorStr"></param>
    /// <returns></returns>
    public static uint GetHex(string colorStr)
    {
        var color = $"ff{colorStr.Replace("#", "")}";

        return uint.Parse(color, System.Globalization.NumberStyles.AllowHexSpecifier);
    }

    /// <summary>
    /// 时间戳转DetaTime
    /// </summary>
    /// <param name="timeStamp"></param>
    /// <returns></returns>
    public static DateTime GetDateTime(long timeStamp)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = (timeStamp * 10000000);
        TimeSpan toNow = new TimeSpan(lTime);
        DateTime targetDt = dtStart.Add(toNow);
        return targetDt;
    }

    /// <summary>
    /// 毫秒时间戳转DetaTime
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static DateTime GetDateTime(string time)
    {
        return new DateTime((Convert.ToInt64(time) * 10000) + 621355968000000000);
    }

    /// <summary>
    /// DetaTime转时间戳(毫秒)
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static string GetChinaTicks(DateTime dateTime)
    {
        DateTime startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1, 8, 0, 0, 0), TimeZoneInfo.Local);
        long t = (dateTime.Ticks - startTime.Ticks) / 10000;
        return t.ToString();
    }
}