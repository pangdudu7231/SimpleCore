using UnityEngine;

namespace SimpleCore.ShapeMeshes
{
    /// <summary>
    /// 图形 mesh的工具类。
    /// </summary>
    public static class ShapeMeshUtility
    {
        #region public functions

        /// <summary>
        /// 获得以 radius为半径的圆，切割圆弧长度为 arcLen的数量。
        /// 获得值的取值范围为3~2000
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="arcLen"></param>
        /// <returns></returns>
        public static int GetCircleSideCount(float radius, float arcLen = 0.1f)
        {
            //取值范围为3~2000
            const int MIN_SIDE_COUNT = 3, MAX_SIDE_COUNT = 2000;
            
            var sideCount = Mathf.RoundToInt(Mathf.PI * 2 * radius / arcLen);
            sideCount = Mathf.Clamp(sideCount, MIN_SIDE_COUNT, MAX_SIDE_COUNT);
            return sideCount;
        }

        #endregion
    }
}