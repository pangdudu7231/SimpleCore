using System;
using UnityEngine;

namespace SimpleCore.ShapeMeshes
{
    /// <summary>
    ///     平面图形 mesh类。
    /// </summary>
    public sealed class PlaneShapeMesh : BaseShapeMesh
    {
        #region private members

        private readonly float _xSize, _zSize; //图形在X轴和Z轴方向的尺寸

        #endregion

        #region ctor

        /// <summary>
        ///     构造函数。
        /// </summary>
        /// <param name="xSize"></param>
        /// <param name="zSize"></param>
        /// <param name="meshName"></param>
        /// <param name="meshPivot"></param>
        public PlaneShapeMesh(float xSize, float zSize, string meshName, MeshPivot meshPivot) : base(meshName,
            meshPivot)
        {
            _xSize = xSize;
            _zSize = zSize;
        }

        #endregion

        #region override functions

        protected override Vector3 GetVertexOffset()
        {
            return _meshPivot switch
            {
                MeshPivot.Center => Vector3.zero,
                MeshPivot.Top => Vector3.zero,
                MeshPivot.Bottom => Vector3.zero,
                MeshPivot.Left => new Vector3(-_xSize, 0, 0) * 0.5f,
                MeshPivot.Right => new Vector3(_xSize, 0, 0) * 0.5f,
                MeshPivot.Front => new Vector3(0, 0, _zSize) * 0.5f,
                MeshPivot.Back => new Vector3(0, 0, -_zSize) * 0.5f,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        protected override int GetArrayLen()
        {
            return 8;
        }

        protected override int GetTriArrayLen()
        {
            return 12;
        }

        protected override Vector3[] GetVertices(int arrayLen, Vector3 vertexOffset)
        {
            var points = GetPlaneShapePoints(_xSize, _zSize, vertexOffset);
            return new[]
            {
                points[0], points[1], points[2], points[3],
                points[3], points[2], points[1], points[0]
            };
        }

        protected override Vector3[] GetNormals(int arrayLen)
        {
            return new[]
            {
                Vector3.up, Vector3.up, Vector3.up, Vector3.up,
                Vector3.down, Vector3.down, Vector3.down, Vector3.down
            };
        }

        protected override int[] GetTriangles(int triArrayLen)
        {
            return new[]
            {
                0, 1, 2, 2, 3, 0,
                4, 5, 6, 6, 7, 4
            };
        }

        protected override Vector2[] GetUVs(int arrayLen)
        {
            return new[]
            {
                new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0),
                new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0)
            };
        }

        #endregion

        #region static functions

        /// <summary>
        ///     获得平面图形的顶点数组。
        /// </summary>
        /// <param name="xSize"></param>
        /// <param name="zSize"></param>
        /// <param name="vertexOffset"></param>
        /// <returns></returns>
        private static Vector3[] GetPlaneShapePoints(float xSize, float zSize, Vector3 vertexOffset)
        {
            var p0 = new Vector3(-xSize, 0, -zSize) * 0.5f - vertexOffset; //左下
            var p1 = new Vector3(-xSize, 0, zSize) * 0.5f - vertexOffset; //左上
            var p2 = new Vector3(xSize, 0, zSize) * 0.5f - vertexOffset; //右上
            var p3 = new Vector3(xSize, 0, -zSize) * 0.5f - vertexOffset; //右下
            return new[] {p0, p1, p2, p3};
        }

        #endregion
    }
}