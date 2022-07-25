using System;
using UnityEngine.UI;

namespace SimpleCore.Extensions
{
    /// <summary>
    ///     Unity Graphic的扩展类。
    /// </summary>
    public static class GraphicExtensions
    {
        #region public static functions

        /// <summary>
        ///     设置 Graphic 组件的颜色值。
        /// </summary>
        /// <param name="graphic"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetColor(this Graphic graphic, float? r = null, float? g = null, float? b = null,
            float? a = null)
        {
            if (r == null && g == null && b == null && a == null) throw new ArgumentNullException();

            SetColorInternal(graphic, r, g, b, a);
        }

        #endregion

        #region private static internal functions

        /// <summary>
        ///     设置 Graphic 组件的颜色值。
        /// </summary>
        /// <param name="graphic"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        private static void SetColorInternal(Graphic graphic, float? r = null, float? g = null, float? b = null,
            float? a = null)
        {
            var color = graphic.color;
            if (r.HasValue) color.r = r.Value;
            if (g.HasValue) color.g = g.Value;
            if (b.HasValue) color.b = b.Value;
            if (a.HasValue) color.a = a.Value;
            graphic.color = color;
        }

        #endregion
    }
}