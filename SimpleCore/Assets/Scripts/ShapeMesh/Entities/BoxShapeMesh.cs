using System;
using UnityEngine;

namespace SimpleCore.ShapeMeshes
{
    /// <summary>
    ///     立方体图形 mesh类。
    /// </summary>
    public abstract class BoxShapeMesh : BaseShapeMesh
    {
        #region private members

        private readonly float _xSize, _ySize, _zSize; //图形在X轴、Y轴和Z轴方向的尺寸

        #endregion

        #region ctor

        /// <summary>
        ///     构造函数。
        /// </summary>
        /// <param name="xSize"></param>
        /// <param name="ySize"></param>
        /// <param name="zSize"></param>
        /// <param name="meshName"></param>
        /// <param name="meshPivot"></param>
        protected BoxShapeMesh(float xSize, float ySize, float zSize, string meshName, MeshPivot meshPivot) : base(
            meshName, meshPivot)
        {
            _xSize = xSize;
            _ySize = ySize;
            _zSize = zSize;
        }

        #endregion

        #region override functions

        protected override Vector3 GetVertexOffset()
        {
            return _meshPivot switch
            {
                MeshPivot.Center => Vector3.zero,
                MeshPivot.Top => new Vector3(0, _ySize, 0) * 0.5f,
                MeshPivot.Bottom => new Vector3(0, -_ySize, 0) * 0.5f,
                MeshPivot.Left => new Vector3(-_xSize, 0, 0) * 0.5f,
                MeshPivot.Right => new Vector3(_xSize, 0, 0) * 0.5f,
                MeshPivot.Front => new Vector3(0, 0, _zSize) * 0.5f,
                MeshPivot.Back => new Vector3(0, 0, -_zSize) * 0.5f,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        protected override Vector3[] GetVertices(Vector3 vertexOffset)
        {
            var points = GetBoxShapePoints(_xSize, _ySize, _zSize, vertexOffset);
            return GetVertices(points);
        }

        /// <summary>
        ///     获得顶点的集合。
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        protected abstract Vector3[] GetVertices(Vector3[] points);

        #endregion

        #region static functions

        /// <summary>
        ///     获得立方体图形的顶点数组。
        /// </summary>
        /// <param name="xSize"></param>
        /// <param name="ySize"></param>
        /// <param name="zSize"></param>
        /// <param name="vertexOffset"></param>
        /// <returns></returns>
        private static Vector3[] GetBoxShapePoints(float xSize, float ySize, float zSize, Vector3 vertexOffset)
        {
            var p0 = new Vector3(-xSize * 0.5f, -ySize * 0.5f, -zSize * 0.5f) - vertexOffset; //左下
            var p1 = new Vector3(xSize * 0.5f, -ySize * 0.5f, -zSize * 0.5f) - vertexOffset; //右下
            var p2 = new Vector3(xSize * 0.5f, -ySize * 0.5f, zSize * 0.5f) - vertexOffset; //右上
            var p3 = new Vector3(-xSize * 0.5f, -ySize * 0.5f, zSize * 0.5f) - vertexOffset; //左上

            var p4 = new Vector3(-xSize * 0.5f, ySize * 0.5f, -zSize * 0.5f) - vertexOffset; //左下
            var p5 = new Vector3(-xSize * 0.5f, ySize * 0.5f, zSize * 0.5f) - vertexOffset; //左上
            var p6 = new Vector3(xSize * 0.5f, ySize * 0.5f, zSize * 0.5f) - vertexOffset; //右上
            var p7 = new Vector3(xSize * 0.5f, ySize * 0.5f, -zSize * 0.5f) - vertexOffset; //右下
            return new[]
            {
                p0, p1, p2, p3, p4, p5, p6, p7
            };
        }

        #endregion
    }
}