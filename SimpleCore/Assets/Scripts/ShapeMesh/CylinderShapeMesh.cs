using System;
using UnityEngine;

namespace SimpleCore.ShapeMeshes
{
    /// <summary>
    /// 圆柱体图形 mesh类。
    /// </summary>
    public sealed class CylinderShapeMesh : BaseShapeMesh
    {
        #region private members

        private readonly float _height;//圆柱体的高度
        private readonly float _radius;//圆柱体的底面半径
        private readonly bool _isDoubleSide; //mesh是否是双面的

        #endregion
        
        #region ctor

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="height"></param>
        /// <param name="radius"></param>
        /// <param name="isDoubleSide"></param>
        /// <param name="meshName"></param>
        /// <param name="meshPivot"></param>
        public CylinderShapeMesh(float height, float radius, bool isDoubleSide = true, string meshName = "CylinderMesh",
            MeshPivot meshPivot = MeshPivot.Center) : base(meshName, meshPivot)
        {
            _height = height;
            _radius = radius;
            _isDoubleSide = isDoubleSide;
        }
        
        #endregion

        #region override functions

        protected override Vector3 GetVertexOffset()
        {
            return _meshPivot switch
            {
                MeshPivot.Center => Vector3.zero,
                MeshPivot.Top => new Vector3(0, _height, 0) * 0.5f,
                MeshPivot.Bottom => new Vector3(0, -_height, 0) * 0.5f,
                MeshPivot.Left => new Vector3(-_radius, 0, 0) * 0.5f,
                MeshPivot.Right => new Vector3(_radius, 0, 0) * 0.5f,
                MeshPivot.Front => new Vector3(0, 0, _radius) * 0.5f,
                MeshPivot.Back => new Vector3(0, 0, -_radius) * 0.5f,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        protected override Vector3[] GetVertices(Vector3 vertexOffset)
        {
            var circularSideCount = ShapeMeshUtility.GetCircularSideCount(_radius);
            return _isDoubleSide
                ? GetDoubleSideVertices(_height, _radius, vertexOffset, circularSideCount)
                : GetSingleSideVertices(_height, _radius, vertexOffset, circularSideCount);
        }

        protected override Vector3[] GetNormals()
        {
            throw new NotImplementedException();
        }

        protected override int[] GetTriangles()
        {
            throw new NotImplementedException();
        }

        protected override Vector2[] GetUVs()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region static functions

        /// <summary>
        /// 获得圆柱体单面的顶点数组。
        /// </summary>
        /// <param name="height"></param>
        /// <param name="radius"></param>
        /// <param name="vertexOffset"></param>
        /// <param name="circularSideCount"></param>
        /// <returns></returns>
        private static Vector3[] GetSingleSideVertices(float height, float radius, Vector3 vertexOffset,
            int circularSideCount)
        {
            var curIndex = 0; //当前的索引
            //顶点数组的长度
            //底面顶点(圆心点 + 圆弧点)、顶面(圆心点 + 圆弧点)、侧面(圆弧点 * 2 + 最后两个点和开始两个点重合)
            var arrayLen = (circularSideCount + 1) * 2 + (circularSideCount + 1) * 2;
            var vertices = new Vector3[arrayLen];
            var topY = height * 0.5f;
            var bottomY = -height * 0.5f;
            ApplyCircleVertices(bottomY);//底面
            ApplyCircleVertices(topY);//顶面
            ApplySideVertices(topY, bottomY);//侧面
            return vertices;

            //应用上下圆面的顶点
            void ApplyCircleVertices(float p_y)
            {
                vertices[curIndex++] = new Vector3(0, p_y, 0) - vertexOffset;//圆心点
                for (var i = 0; i < circularSideCount; i++)
                {
                    var rad = i * Mathf.PI * 2 / circularSideCount;//弧度
                    var cos = Mathf.Cos(rad) * radius;
                    var sin = Mathf.Sin(rad) * radius;
                    vertices[curIndex++] = new Vector3(cos, p_y, sin) - vertexOffset;
                }
            }
            //应用侧面的顶点
            void ApplySideVertices(float p_topY,float p_bottomY)
            {
                for (var i = 0; i <= circularSideCount; i++)
                {
                    var rad = i * Mathf.PI * 2 / circularSideCount; //弧度
                    var cos = Mathf.Cos(rad) * radius;
                    var sin = Mathf.Sin(rad) * radius;
                    vertices[curIndex++] = new Vector3(cos, p_bottomY, sin) - vertexOffset;
                    vertices[curIndex++] = new Vector3(cos, p_topY, sin) - vertexOffset;
                }
            }
        }

        /// <summary>
        /// 获得圆柱体双面的顶点数组。
        /// </summary>
        /// <param name="height"></param>
        /// <param name="radius"></param>
        /// <param name="vertexOffset"></param>
        /// <param name="circularSideCount"></param>
        /// <returns></returns>
        private static Vector3[] GetDoubleSideVertices(float height, float radius, Vector3 vertexOffset,
            int circularSideCount)
        {
            var curIndex = 0; //当前的索引
            //顶点数组的长度
            //底面顶点(圆心点 + 圆弧点)、顶面(圆心点 + 圆弧点)、侧面(圆弧点 * 2 + 最后两个点和开始两个点重合)
            var arrayLen = (circularSideCount + 1) * 2 + (circularSideCount + 1) * 2;
            arrayLen *= 2; //双面顶点数量 *2
            var vertices = new Vector3[arrayLen];
            var topY = height * 0.5f;
            var bottomY = -height * 0.5f;
            ApplyCircleVertices(topY);//底面
            //TODO
            return vertices;

            //应用上下圆面的顶点
            void ApplyCircleVertices(float p_y)
            {
                vertices[curIndex++] = new Vector3(0, p_y, 0) - vertexOffset; //圆心点
                for (var i = 0; i < circularSideCount; i++)
                {
                    var rad = i * Mathf.PI * 2 / circularSideCount; //弧度
                    var cos = Mathf.Cos(rad) * radius;
                    var sin = Mathf.Sin(rad) * radius;
                    vertices[curIndex++] = new Vector3(cos, p_y, sin) - vertexOffset;
                }

                //双面渲染顶点
                vertices[curIndex++] = new Vector3(0, p_y, 0) - vertexOffset; //圆心点
                for (var i = 0; i < circularSideCount; i++)
                {
                    var rad = (circularSideCount - i - 1) * Mathf.PI * 2 / circularSideCount; //弧度
                    var cos = Mathf.Cos(rad) * radius;
                    var sin = Mathf.Sin(rad) * radius;
                    vertices[curIndex++] = new Vector3(cos, p_y, sin) - vertexOffset;
                }
            }
        }

        #endregion
    }
}