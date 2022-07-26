using UnityEngine;

namespace SimpleCore.Extensions
{
    /// <summary>
    ///     Unity Object 的扩展类。
    /// </summary>
    public static class UnityObjectExtensions
    {
        #region public static functions

        /// <summary>
        ///     安全地销毁 Object 对象。(在销毁之前判空)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="t"></param>
        public static void DestroySafely(this Object obj, float t = 0.0f)
        {
            if (obj == null) return;

            Object.Destroy(obj, t);
        }

        /// <summary>
        ///     安全地立即销毁 Object 对象。(在销毁之前判空)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="allowDestroyingAssets"></param>
        public static void DestroyImmediateSafely(this Object obj, bool allowDestroyingAssets = false)
        {
            if (obj == null) return;

            Object.DestroyImmediate(obj, allowDestroyingAssets);
        }

        #endregion
    }
}