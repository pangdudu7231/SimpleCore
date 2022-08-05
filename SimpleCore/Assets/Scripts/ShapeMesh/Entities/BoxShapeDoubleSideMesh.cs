using UnityEngine;

namespace SimpleCore.ShapeMeshes
{
    /// <summary>
    ///     立方体图形双面 mesh类。
    /// </summary>
    public sealed class BoxShapeDoubleSideMesh : BoxShapeMesh
    {
        #region ctor

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="xSize"></param>
        /// <param name="ySize"></param>
        /// <param name="zSize"></param>
        /// <param name="meshName"></param>
        /// <param name="meshPivot"></param>
        public BoxShapeDoubleSideMesh(float xSize, float ySize, float zSize, string meshName, MeshPivot meshPivot) :
            base(xSize, ySize, zSize, meshName, meshPivot)
        {
        }

        #endregion

        #region override functions

        protected override Vector3[] GetVertices(Vector3[] points)
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

        protected override Vector3[] GetNormals()
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

        protected override int[] GetTriangles()
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

        protected override Vector2[] GetUVs()
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