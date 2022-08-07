using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleCore.ShapeMeshes
{
    /// <summary>
    ///     立方体图形 mesh类。
    /// </summary>
    public class BoxShapeMesh : BaseShapeMesh
    {
        #region private members

        private readonly float _xSize, _ySize, _zSize; //图形在X轴、Y轴和Z轴方向的尺寸
        private readonly bool _isDoubleSide; //是否是双面的mesh

        #endregion

        #region ctor

        /// <summary>
        ///     构造函数。
        /// </summary>
        /// <param name="xSize"></param>
        /// <param name="ySize"></param>
        /// <param name="zSize"></param>
        /// <param name="isDoubleSide"></param>
        /// <param name="meshName"></param>
        /// <param name="meshPivot"></param>
        public BoxShapeMesh(float xSize, float ySize, float zSize, bool isDoubleSide, string meshName,
            MeshPivot meshPivot) : base(meshName, meshPivot)
        {
            _xSize = xSize;
            _ySize = ySize;
            _zSize = zSize;
            _isDoubleSide = isDoubleSide;
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

        protected override int GetArrayLen()
        {
            return _isDoubleSide ? 4 * 6 * 2 : 4 * 6;
        }

        protected override int GetTriArrayLen()
        {
            return _isDoubleSide ? 6 * 3 * 2 * 2 : 6 * 3 * 2;
        }

        protected override Vector3[] GetVertices(int arrayLen, Vector3 vertexOffset)
        {
            var points = GetBoxShapePoints(_xSize, _ySize, _zSize, vertexOffset);
            return _isDoubleSide ? GetDoubleSideVertices(points) : GetSingleSideVertices(points);
        }

        protected override Vector3[] GetNormals(int arrayLen)
        {
            return _isDoubleSide ? GetDoubleSideNormals() : GetSingleSideNormals();
        }

        protected override int[] GetTriangles(int triArrayLen)
        {
            return _isDoubleSide ? GetDoubleSideTriangles() : GetSingleSideTriangles();
        }

        protected override Vector2[] GetUVs(int arrayLen)
        {
            return _isDoubleSide ? GetDoubleSideUVs() : GetSingleSideUVs();
        }

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

        /// <summary>
        ///     获得立方体图形单面mesh的顶点数组。
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private static Vector3[] GetSingleSideVertices(IReadOnlyList<Vector3> points)
        {
            return new[]
            {
                //下
                points[1], points[2], points[3], points[0],
                //上
                points[4], points[5], points[6], points[7],
                //左
                points[3], points[5], points[4], points[0],
                //右
                points[1], points[7], points[6], points[2],
                //前
                points[2], points[6], points[5], points[3],
                //后
                points[0], points[4], points[7], points[1]
            };
        }

        /// <summary>
        ///     获得立方体图形双面mesh的顶点数组
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private static Vector3[] GetDoubleSideVertices(IReadOnlyList<Vector3> points)
        {
            return new[]
            {
                //下
                points[1], points[2], points[3], points[0],
                points[0], points[3], points[2], points[1],
                //上
                points[4], points[5], points[6], points[7],
                points[7], points[6], points[5], points[4],
                //左
                points[3], points[5], points[4], points[0],
                points[0], points[4], points[5], points[3],
                //右
                points[1], points[7], points[6], points[2],
                points[2], points[6], points[7], points[1],
                //前
                points[2], points[6], points[5], points[3],
                points[3], points[5], points[6], points[2],
                //后
                points[0], points[4], points[7], points[1],
                points[1], points[7], points[4], points[0]
            };
        }

        /// <summary>
        ///     获得立方体图形单面mesh的法线数组。
        /// </summary>
        /// <returns></returns>
        private static Vector3[] GetSingleSideNormals()
        {
            return new[]
            {
                //下
                Vector3.down, Vector3.down, Vector3.down, Vector3.down,
                //上
                Vector3.up, Vector3.up, Vector3.up, Vector3.up,
                //左
                Vector3.left, Vector3.left, Vector3.left, Vector3.left,
                //右
                Vector3.right, Vector3.right, Vector3.right, Vector3.right,
                //前
                Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward,
                //后
                Vector3.back, Vector3.back, Vector3.back, Vector3.back
            };
        }

        /// <summary>
        ///     获得立方体图形双面mesh的法线数组。
        /// </summary>
        /// <returns></returns>
        private static Vector3[] GetDoubleSideNormals()
        {
            return new[]
            {
                //下
                Vector3.down, Vector3.down, Vector3.down, Vector3.down,
                Vector3.up, Vector3.up, Vector3.up, Vector3.up,
                //上
                Vector3.up, Vector3.up, Vector3.up, Vector3.up,
                Vector3.down, Vector3.down, Vector3.down, Vector3.down,
                //左
                Vector3.left, Vector3.left, Vector3.left, Vector3.left,
                Vector3.right, Vector3.right, Vector3.right, Vector3.right,
                //右
                Vector3.right, Vector3.right, Vector3.right, Vector3.right,
                Vector3.left, Vector3.left, Vector3.left, Vector3.left,
                //前
                Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward,
                Vector3.back, Vector3.back, Vector3.back, Vector3.back,
                //后
                Vector3.back, Vector3.back, Vector3.back, Vector3.back,
                Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward
            };
        }

        /// <summary>
        ///     获得立方体图形单面mesh的三角面顶点索引。
        /// </summary>
        /// <returns></returns>
        private static int[] GetSingleSideTriangles()
        {
            var triangles = new int[6 * 3 * 2]; //六个面，每个面两个三角面
            var triIndex = 0; //三角面的索引
            for (var i = 0; i < 6; i++) //顺序为：下面、上面、左面、右面、前面、后面
            {
                //四个点组成两个三角面
                var verIndex = 0 + 4 * i; //顶点的索引
                triangles[triIndex++] = verIndex;
                triangles[triIndex++] = verIndex + 1;
                triangles[triIndex++] = verIndex + 2;
                triangles[triIndex++] = verIndex + 2;
                triangles[triIndex++] = verIndex + 3;
                triangles[triIndex++] = verIndex;
            }

            return triangles;
        }

        /// <summary>
        ///     获得立方体图形双面mesh的三角面顶点索引。
        /// </summary>
        /// <returns></returns>
        private static int[] GetDoubleSideTriangles()
        {
            var triangles = new int[6 * 3 * 2 * 2]; //六个面，每个面两个三角面，双面渲染
            var triIndex = 0; //三角面的索引
            for (var i = 0; i < 6; i++) //顺序为：下面、上面、左面、右面、前面、后面
            {
                //八个点组成双面渲染的三角面
                var verIndex = 0 + 8 * i; //顶点的索引
                triangles[triIndex++] = verIndex;
                triangles[triIndex++] = verIndex + 1;
                triangles[triIndex++] = verIndex + 2;
                triangles[triIndex++] = verIndex + 2;
                triangles[triIndex++] = verIndex + 3;
                triangles[triIndex++] = verIndex;

                triangles[triIndex++] = verIndex + 4;
                triangles[triIndex++] = verIndex + 5;
                triangles[triIndex++] = verIndex + 6;
                triangles[triIndex++] = verIndex + 6;
                triangles[triIndex++] = verIndex + 7;
                triangles[triIndex++] = verIndex + 4;
            }

            return triangles;
        }

        /// <summary>
        ///     获得立方体图形单面mesh的uv数组。
        /// </summary>
        /// <returns></returns>
        private static Vector2[] GetSingleSideUVs()
        {
            var uvs = new Vector2[6 * 4];
            var uvIndex = 0;
            for (var i = 0; i < 6; i++)
            {
                uvs[uvIndex++] = new Vector2(0, 0);
                uvs[uvIndex++] = new Vector2(0, 1);
                uvs[uvIndex++] = new Vector2(1, 1);
                uvs[uvIndex++] = new Vector2(1, 0);
            }

            return uvs;
        }

        /// <summary>
        ///     获得立方体图形双面mesh的uv数组。
        /// </summary>
        /// <returns></returns>
        private static Vector2[] GetDoubleSideUVs()
        {
            var uvs = new Vector2[6 * 4 * 2];
            var uvIndex = 0;
            for (var i = 0; i < 12; i++)
            {
                uvs[uvIndex++] = new Vector2(0, 0);
                uvs[uvIndex++] = new Vector2(0, 1);
                uvs[uvIndex++] = new Vector2(1, 1);
                uvs[uvIndex++] = new Vector2(1, 0);
            }

            return uvs;
        }

        #endregion
    }
}