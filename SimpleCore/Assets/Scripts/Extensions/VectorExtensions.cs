using System;
using UnityEngine;

namespace SimpleCore.Extensions
{
    /// <summary>
    ///     Unity Vector 的扩展类。
    /// </summary>
    public static class VectorExtensions
    {
        #region public static functions

        /// <summary>
        ///     设置 Vector2 的值，并返回一个新的 Vector2 结构体。
        /// </summary>
        /// <param name="v"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Vector2 SetValue(this Vector2 v, float? x = null, float? y = null)
        {
            if (x == null && y == null) throw new ArgumentNullException();

            return SetValueInternal(v, x, y);
        }

        /// <summary>
        ///     设置 Vector2Int 的值，并返回一个新的 Vector2Int 结构体。
        /// </summary>
        /// <param name="v"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Vector2Int SetValue(this Vector2Int v, int? x = null, int? y = null)
        {
            if (x == null && y == null) throw new ArgumentNullException();

            return SetValueInternal(v, x, y);
        }

        /// <summary>
        ///     设置 Vector3 的值，并返回一个新的 Vector3 结构体。
        /// </summary>
        /// <param name="v"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Vector3 SetValue(this Vector3 v, float? x = null, float? y = null, float? z = null)
        {
            if (x == null && y == null && z == null) throw new ArgumentNullException();

            return SetValueInternal(v, x, y, z);
        }

        /// <summary>
        ///     设置 Vector3Int 的值，并返回一个新的 Vector3Int 结构体。
        /// </summary>
        /// <param name="v"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Vector3Int SetValue(this Vector3Int v, int? x = null, int? y = null, int? z = null)
        {
            if (x == null && y == null && z == null) throw new ArgumentNullException();

            return SetValueInternal(v, x, y, z);
        }

        /// <summary>
        ///     比较两个 Vector2 是否非常接近。
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool Approximately(this Vector2 v1, Vector2 v2)
        {
            if (!Mathf.Approximately(v1.x, v2.x)) return false;
            if (!Mathf.Approximately(v1.y, v2.y)) return false;
            return true;
        }

        /// <summary>
        ///     比较两个 Vector3 是否非常接近。
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool Approximately(this Vector3 v1, Vector3 v2)
        {
            if (!Mathf.Approximately(v1.x, v2.x)) return false;
            if (!Mathf.Approximately(v1.y, v2.y)) return false;
            if (!Mathf.Approximately(v1.z, v2.z)) return false;
            return true;
        }

        /// <summary>
        ///     比较两个 Vector4 是否非常接近。
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool Approximately(this Vector4 v1, Vector4 v2)
        {
            if (!Mathf.Approximately(v1.x, v2.x)) return false;
            if (!Mathf.Approximately(v1.y, v2.y)) return false;
            if (!Mathf.Approximately(v1.z, v2.z)) return false;
            if (!Mathf.Approximately(v1.w, v2.w)) return false;
            return true;
        }

        #endregion

        #region private static internal functions

        /// <summary>
        ///     设置 Vector2 的值，并返回一个新的 Vector2 结构体。
        /// </summary>
        /// <param name="v"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static Vector2 SetValueInternal(Vector2 v, float? x = null, float? y = null)
        {
            if (x.HasValue) v.x = x.Value;
            if (y.HasValue) v.y = y.Value;
            return v;
        }

        /// <summary>
        ///     设置 Vector2Int 的值，并返回一个新的 Vector2Int 结构体。
        /// </summary>
        /// <param name="v"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static Vector2Int SetValueInternal(Vector2Int v, int? x = null, int? y = null)
        {
            if (x.HasValue) v.x = x.Value;
            if (y.HasValue) v.y = y.Value;
            return v;
        }

        /// <summary>
        ///     设置 Vector3 的值，并返回一个新的 Vector3 结构体。
        /// </summary>
        /// <param name="v"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        private static Vector3 SetValueInternal(Vector3 v, float? x = null, float? y = null, float? z = null)
        {
            if (x.HasValue) v.x = x.Value;
            if (y.HasValue) v.y = y.Value;
            if (z.HasValue) v.z = z.Value;
            return v;
        }

        /// <summary>
        ///     设置 Vector3Int 的值，并返回一个新的 Vector3Int 结构体。
        /// </summary>
        /// <param name="v"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        private static Vector3Int SetValueInternal(Vector3Int v, int? x = null, int? y = null, int? z = null)
        {
            if (x.HasValue) v.x = x.Value;
            if (y.HasValue) v.y = y.Value;
            if (z.HasValue) v.z = z.Value;
            return v;
        }

        #endregion
    }
}