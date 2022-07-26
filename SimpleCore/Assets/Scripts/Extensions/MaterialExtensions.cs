using System;
using UnityEngine;

namespace SimpleCore.Extensions
{
    /// <summary>
    ///     Unity Material 的扩展类。
    /// </summary>
    public static class MaterialExtensions
    {
        #region public static functions

        /// <summary>
        ///     设置 Material 的主颜色值。
        /// </summary>
        /// <param name="material"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetColor(this Material material, float? r = null, float? g = null, float? b = null,
            float? a = null)
        {
            if (r == null && g == null && b == null && a == null) throw new ArgumentNullException();

            SetColorInternal(material, r, g, b, a);
        }

        /// <summary>
        ///     根据颜色参数的名称设置 Material 的颜色值。
        /// </summary>
        /// <param name="material"></param>
        /// <param name="name"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetColorWithName(this Material material, string name, float? r = null, float? g = null,
            float? b = null, float? a = null)
        {
            if (r == null && g == null && b == null && a == null) throw new ArgumentNullException();

            SetColorWithNameInternal(material, name, r, g, b, a);
        }

        /// <summary>
        ///     根据颜色参数的ID设置 Material 的颜色值。
        /// </summary>
        /// <param name="material"></param>
        /// <param name="nameID"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        public static void SetColorWithNameID(this Material material, int nameID, float? r = null, float? g = null,
            float? b = null, float? a = null)
        {
            if (r == null && g == null && b == null && a == null) throw new ArgumentNullException();

            SetColorWithNameIDInternal(material, nameID, r, g, b, a);
        }

        #endregion

        #region private static internal functions

        /// <summary>
        ///     设置 Material 的主颜色值。
        /// </summary>
        /// <param name="material"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        private static void SetColorInternal(Material material, float? r = null, float? g = null, float? b = null,
            float? a = null)
        {
            var color = material.color;
            if (r.HasValue) color.r = r.Value;
            if (g.HasValue) color.g = g.Value;
            if (b.HasValue) color.b = b.Value;
            if (a.HasValue) color.a = a.Value;
            material.color = color;
        }

        /// <summary>
        ///     根据颜色参数的名称设置 Material 的颜色值。
        /// </summary>
        /// <param name="material"></param>
        /// <param name="name"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        private static void SetColorWithNameInternal(Material material, string name, float? r = null,
            float? g = null, float? b = null, float? a = null)
        {
            var color = material.GetColor(name);
            if (r.HasValue) color.r = r.Value;
            if (g.HasValue) color.g = g.Value;
            if (b.HasValue) color.b = b.Value;
            if (a.HasValue) color.a = a.Value;
            material.SetColor(name, color);
        }

        /// <summary>
        ///     根据颜色参数的ID设置 Material 的颜色值。
        /// </summary>
        /// <param name="material"></param>
        /// <param name="nameID"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        private static void SetColorWithNameIDInternal(Material material, int nameID, float? r = null, float? g = null,
            float? b = null, float? a = null)
        {
            var color = material.GetColor(nameID);
            if (r.HasValue) color.r = r.Value;
            if (g.HasValue) color.g = g.Value;
            if (b.HasValue) color.b = b.Value;
            if (a.HasValue) color.a = a.Value;
            material.SetColor(nameID, color);
        }

        #endregion
    }
}