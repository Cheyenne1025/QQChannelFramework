using System;
namespace QQChannelFramework.Tools
{
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
        public static DateTime GetDateTime(int timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = ((long)timeStamp * 10000000);
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime targetDt = dtStart.Add(toNow);
            return targetDt;
        }
    }
}

