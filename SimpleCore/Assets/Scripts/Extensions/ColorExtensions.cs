using System;
using UnityEngine;

namespace SimpleCore.Extensions
{
    /// <summary>
    ///     Unity Color 的扩展类。
    /// </summary>
    public static class ColorExtensions
    {
        #region public static functions

        /// <summary>
        ///     向Color中的参数赋值。
        /// </summary>
        /// <param name="color"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        /// <returns>返回赋值完成后的Color值。</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Color SetValue(this Color color, float? r = null, float? g = null, float? b = null,
            float? a = null)
        {
            if (r == null && g == null && b == null && a == null) throw new ArgumentNullException();

            return SetValueInternal(color, r, g, b, a);
        }

        /// <summary>
        ///     向Color32中的参数赋值。
        /// </summary>
        /// <param name="color32"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        /// <returns>返回赋值完成后的Color32值。</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Color32 SetValue(this Color32 color32, byte? r = null, byte? g = null, byte? b = null,
            byte? a = null)
        {
            if (r == null && g == null && b == null && a == null) throw new ArgumentNullException();

            return SetValueInternal(color32, r, g, b, a);
        }

        #endregion

        #region private static internal function

        /// <summary>
        ///     向Color中的参数赋值。
        /// </summary>
        /// <param name="color"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        /// <returns>返回赋值完成后的Color值。</returns>
        private static Color SetValueInternal(Color color, float? r = null, float? g = null, float? b = null,
            float? a = null)
        {
            if (r.HasValue) color.r = r.Value;
            if (g.HasValue) color.g = g.Value;
            if (b.HasValue) color.b = b.Value;
            if (a.HasValue) color.a = a.Value;
            return color;
        }

        /// <summary>
        ///     向Color32中的参数赋值。
        /// </summary>
        /// <param name="color32"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        /// <returns>返回赋值完成后的Color32值。</returns>
        private static Color32 SetValueInternal(Color32 color32, byte? r = null, byte? g = null, byte? b = null,
            byte? a = null)
        {
            if (r.HasValue) color32.r = r.Value;
            if (g.HasValue) color32.g = g.Value;
            if (b.HasValue) color32.b = b.Value;
            if (a.HasValue) color32.a = a.Value;
            return color32;
        }

        #endregion
    }
}