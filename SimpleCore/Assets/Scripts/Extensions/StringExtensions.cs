using System;

namespace SimpleCore.Extensions
{
    /// <summary>
    ///     String 的扩展类。
    /// </summary>
    public static class StringExtensions
    {
        #region public static functions

        /// <summary>
        ///     判断字符串是否为 Null 或者空字符串。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        ///     判断字符串是否为 Null 或者只包含空格符。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        ///     判断字符串是否包含 Value。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public static bool Contains(this string str, string value,
            StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            return str.IndexOf(value, comparisonType) >= 0;
        }

        #endregion
    }
}