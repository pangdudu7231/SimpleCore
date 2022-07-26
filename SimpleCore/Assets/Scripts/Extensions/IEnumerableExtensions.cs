using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleCore.Extensions
{
    /// <summary>
    /// IEnumerable 的扩展类。
    /// </summary>
    public static class IEnumerableExtensions
    {
        #region public static functions

        /// <summary>
        /// 判断 IEnumerable 是否为 Null 或者空。
        /// </summary>
        /// <param name="source"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return source == null || !source.Any();
        }

        /// <summary>
        /// 循环执行action事件。
        /// </summary>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <exception cref="NullReferenceException"></exception>
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            if (source == null) throw new NullReferenceException();

            foreach (var item in source) action.Invoke(item);
        }

        #endregion
    }
}