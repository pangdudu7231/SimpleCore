using UnityEngine;

namespace SimpleCore.Extensions
{
    /// <summary>
    ///     Unity Matrix4x4 的扩展类。
    /// </summary>
    public static class Matrix4x4Extensions
    {
        #region public static functions

        /// <summary>
        ///     从矩阵数据中获取 Position 坐标信息。
        /// </summary>
        /// <param name="matrix4X4"></param>
        /// <returns></returns>
        public static Vector3 GetPosition(this Matrix4x4 matrix4X4)
        {
            return new Vector3(matrix4X4.m03, matrix4X4.m13, matrix4X4.m23);
        }

        /// <summary>
        ///     从矩阵数据中获取 Rotation 旋转四元数信息。
        /// </summary>
        /// <param name="matrix4X4"></param>
        /// <returns></returns>
        public static Quaternion GetRotation(this Matrix4x4 matrix4X4)
        {
            return Quaternion.LookRotation(matrix4X4.GetColumn(2), matrix4X4.GetColumn(1));
        }

        /// <summary>
        ///     从矩阵数据中获取 Scale 缩放信息。
        /// </summary>
        /// <param name="matrix4X4"></param>
        /// <returns></returns>
        public static Vector3 GetScale(this Matrix4x4 matrix4X4)
        {
            var x = new Vector4(matrix4X4.m00, matrix4X4.m10, matrix4X4.m20, matrix4X4.m30).magnitude;
            var y = new Vector4(matrix4X4.m01, matrix4X4.m11, matrix4X4.m21, matrix4X4.m31).magnitude;
            var z = new Vector4(matrix4X4.m02, matrix4X4.m12, matrix4X4.m22, matrix4X4.m32).magnitude;
            return new Vector3(x, y, z);
        }

        #endregion
    }
}