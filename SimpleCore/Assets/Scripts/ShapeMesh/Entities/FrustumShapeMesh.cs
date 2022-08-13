using System;
using UnityEngine;

namespace SimpleCore.ShapeMeshes
{
    /// <summary>
    ///     圆锥台图形 mesh类。
    /// </summary>
    public sealed class FrustumShapeMesh : BaseShapeMesh
    {
        #region private members

        private readonly float _height; //圆锥台的高度
        private readonly float _topRadius; //圆锥台的顶面半径
        private readonly float _bottomRadius; //圆锥台的底面半径
        private readonly bool _isDoubleSide; //mesh是否是双面的

        private readonly int _circleSideCount; //圆边切割的数量。

        #endregion

        #region ctor

        /// <summary>
        ///     构造函数。
        /// </summary>
        /// <param name="height"></param>
        /// <param name="topRadius"></param>
        /// <param name="bottomRadius"></param>
        /// <param name="isDoubleSide"></param>
        /// <param name="meshName"></param>
        /// <param name="meshPivot"></param>
        public FrustumShapeMesh(float height, float topRadius, float bottomRadius, bool isDoubleSide, string meshName,
            MeshPivot meshPivot) : base(meshName, meshPivot)
        {
            _height = height;
            _topRadius = topRadius;
            _bottomRadius = bottomRadius;
            _isDoubleSide = isDoubleSide;

            _circleSideCount = ShapeMeshUtility.GetCircleSideCount(Mathf.Max(_topRadius, _bottomRadius));
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
                MeshPivot.Left => new Vector3(-(_topRadius + _bottomRadius), 0, 0) * 0.5f,
                MeshPivot.Right => new Vector3(_topRadius + _bottomRadius, 0, 0) * 0.5f,
                MeshPivot.Front => new Vector3(0, 0, _topRadius + _bottomRadius) * 0.5f,
                MeshPivot.Back => new Vector3(0, 0, -(_topRadius + _bottomRadius)) * 0.5f,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        protected override int GetArrayLen()
        {
            //底面顶点(圆心点 + 圆弧点)、顶面(圆心点 + 圆弧点)、侧面(圆弧点 * 2 + 最后两个点和开始两个点重合)
            var arrayLen = (_circleSideCount + 1) * 2 * 2;
            if (_isDoubleSide) arrayLen *= 2;
            return arrayLen;
        }

        protected override int GetTriArrayLen()
        {
            //底面三角面、顶面三角面、侧面三角面
            var triArrayLen = _circleSideCount * 3 * 2 * 2;
            if (_isDoubleSide) triArrayLen *= 2;
            return triArrayLen;
        }

        protected override Vector3[] GetVertices(int arrayLen, Vector3 vertexOffset)
        {
            return _isDoubleSide
                ? GetDoubleSideVertices(_height, _topRadius, _bottomRadius, arrayLen, vertexOffset, _circleSideCount)
                : GetSingleSideVertices(_height, _topRadius, _bottomRadius, arrayLen, vertexOffset, _circleSideCount);
        }

        protected override Vector3[] GetNormals(int arrayLen)
        {
            return _isDoubleSide
                ? GetDoubleSideNormals(arrayLen, _circleSideCount)
                : GetSingleSideNormals(arrayLen, _circleSideCount);
        }

        protected override int[] GetTriangles(int triArrayLen)
        {
            return _isDoubleSide
                ? GetDoubleSideTriangles(triArrayLen, _circleSideCount)
                : GetSingleSideTriangles(triArrayLen, _circleSideCount);
        }

        protected override Vector2[] GetUVs(int arrayLen)
        {
            return _isDoubleSide
                ? GetDoubleSideUVs(arrayLen, _circleSideCount)
                : GetSingleSideUVs(arrayLen, _circleSideCount);
        }

        #endregion

        #region static functions

        /// <summary>
        ///     获得圆锥台单面的顶点数组。
        /// </summary>
        /// <param name="height"></param>
        /// <param name="topRadius"></param>
        /// <param name="bottomRadius"></param>
        /// <param name="arrayLen"></param>
        /// <param name="vertexOffset"></param>
        /// <param name="circleSideCount"></param>
        /// <returns></returns>
        private static Vector3[] GetSingleSideVertices(float height, float topRadius, float bottomRadius, int arrayLen,
            Vector3 vertexOffset, int circleSideCount)
        {
            var curIndex = 0; //当前的索引
            var vertices = new Vector3[arrayLen];
            var topY = height * 0.5f;
            var bottomY = -height * 0.5f;
            ApplyCircleVertices(topY, bottomY); //底面和顶面
            ApplySideVertices(topY, bottomY); //侧面
            return vertices;

            //应用上下圆面的顶点
            void ApplyCircleVertices(float p_topY, float p_bottomY)
            {
                //底面
                vertices[curIndex++] = new Vector3(0, p_bottomY, 0) - vertexOffset;
                for (var i = 0; i < circleSideCount; i++)
                {
                    var rad = i * Mathf.PI * 2 / circleSideCount; //弧度
                    var cos = Mathf.Cos(rad) * bottomRadius;
                    var sin = Mathf.Sin(rad) * bottomRadius;
                    vertices[curIndex++] = new Vector3(cos, p_bottomY, sin) - vertexOffset;
                }

                //顶面
                vertices[curIndex++] = new Vector3(0, p_topY, 0) - vertexOffset;
                for (var i = 0; i < circleSideCount; i++)
                {
                    var rad = i * Mathf.PI * 2 / circleSideCount; //弧度
                    var cos = Mathf.Cos(rad) * topRadius;
                    var sin = Mathf.Sin(rad) * topRadius;
                    vertices[curIndex++] = new Vector3(cos, p_topY, sin) - vertexOffset;
                }
            }

            //应用侧面的顶点
            void ApplySideVertices(float p_topY, float p_bottomY)
            {
                for (var i = 0; i <= circleSideCount; i++)
                {
                    var rad = i * Mathf.PI * 2 / circleSideCount; //弧度
                    var botCos = Mathf.Cos(rad) * bottomRadius;
                    var botSin = Mathf.Sin(rad) * bottomRadius;
                    var topCos = Mathf.Cos(rad) * topRadius;
                    var topSin = Mathf.Sin(rad) * topRadius;
                    vertices[curIndex++] = new Vector3(botCos, p_bottomY, botSin) - vertexOffset;
                    vertices[curIndex++] = new Vector3(topCos, p_topY, topSin) - vertexOffset;
                }
            }
        }

        /// <summary>
        ///     获得圆锥台双面的顶点数组。
        /// </summary>
        /// <param name="height"></param>
        /// <param name="topRadius"></param>
        /// <param name="bottomRadius"></param>
        /// <param name="arrayLen"></param>
        /// <param name="vertexOffset"></param>
        /// <param name="circleSideCount"></param>
        /// <returns></returns>
        private static Vector3[] GetDoubleSideVertices(float height, float topRadius, float bottomRadius, int arrayLen,
            Vector3 vertexOffset, int circleSideCount)
        {
            var curIndex = 0; //当前的索引
            var vertices = new Vector3[arrayLen];
            var topY = height * 0.5f;
            var bottomY = -height * 0.5f;
            ApplyCircleVertices(topY, bottomY); //底面和顶面
            ApplySideVertices(topY, bottomY); //侧面
            return vertices;

            //应用上下圆面的顶点
            void ApplyCircleVertices(float p_topY, float p_bottomY)
            {
                //底面
                vertices[curIndex++] = new Vector3(0, p_bottomY, 0) - vertexOffset;
                for (var i = 0; i < circleSideCount; i++)
                {
                    var rad = i * Mathf.PI * 2 / circleSideCount; //弧度
                    var cos = Mathf.Cos(rad) * bottomRadius;
                    var sin = Mathf.Sin(rad) * bottomRadius;
                    vertices[curIndex++] = new Vector3(cos, p_bottomY, sin) - vertexOffset;
                }

                vertices[curIndex++] = new Vector3(0, p_bottomY, 0) - vertexOffset;
                for (var i = 0; i < circleSideCount; i++)
                {
                    var rad = (circleSideCount - i - 1) * Mathf.PI * 2 / circleSideCount; //弧度
                    var cos = Mathf.Cos(rad) * bottomRadius;
                    var sin = Mathf.Sin(rad) * bottomRadius;
                    vertices[curIndex++] = new Vector3(cos, p_bottomY, sin) - vertexOffset;
                }

                //顶面
                vertices[curIndex++] = new Vector3(0, p_topY, 0) - vertexOffset;
                for (var i = 0; i < circleSideCount; i++)
                {
                    var rad = i * Mathf.PI * 2 / circleSideCount; //弧度
                    var cos = Mathf.Cos(rad) * topRadius;
                    var sin = Mathf.Sin(rad) * topRadius;
                    vertices[curIndex++] = new Vector3(cos, p_topY, sin) - vertexOffset;
                }

                vertices[curIndex++] = new Vector3(0, p_topY, 0) - vertexOffset;
                for (var i = 0; i < circleSideCount; i++)
                {
                    var rad = (circleSideCount - i - 1) * Mathf.PI * 2 / circleSideCount; //弧度
                    var cos = Mathf.Cos(rad) * topRadius;
                    var sin = Mathf.Sin(rad) * topRadius;
                    vertices[curIndex++] = new Vector3(cos, p_topY, sin) - vertexOffset;
                }
            }

            //应用侧面的顶点
            void ApplySideVertices(float p_topY, float p_bottomY)
            {
                for (var i = 0; i <= circleSideCount; i++)
                {
                    var rad = i * Mathf.PI * 2 / circleSideCount; //弧度
                    var botCos = Mathf.Cos(rad) * bottomRadius;
                    var botSin = Mathf.Sin(rad) * bottomRadius;
                    var topCos = Mathf.Cos(rad) * topRadius;
                    var topSin = Mathf.Sin(rad) * topRadius;
                    vertices[curIndex++] = new Vector3(botCos, p_bottomY, botSin) - vertexOffset;
                    vertices[curIndex++] = new Vector3(topCos, p_topY, topSin) - vertexOffset;
                }

                for (var i = 0; i <= circleSideCount; i++)
                {
                    var rad = (circleSideCount - i) * Mathf.PI * 2 / circleSideCount; //弧度
                    var botCos = Mathf.Cos(rad) * bottomRadius;
                    var botSin = Mathf.Sin(rad) * bottomRadius;
                    var topCos = Mathf.Cos(rad) * topRadius;
                    var topSin = Mathf.Sin(rad) * topRadius;
                    vertices[curIndex++] = new Vector3(botCos, p_bottomY, botSin) - vertexOffset;
                    vertices[curIndex++] = new Vector3(topCos, p_topY, topSin) - vertexOffset;
                }
            }
        }

        /// <summary>
        ///     获得圆锥台单面的法线数组。
        /// </summary>
        /// <param name="arrayLen"></param>
        /// <param name="circleSideCount"></param>
        /// <returns></returns>
        private static Vector3[] GetSingleSideNormals(int arrayLen, int circleSideCount)
        {
            var curIndex = 0; //当前索引
            var normals = new Vector3[arrayLen];
            ApplyCircleNormals(); //底面和顶面
            ApplySideNormals(); //侧面
            return normals;

            //应用上下圆面的法线数组
            void ApplyCircleNormals()
            {
                for (var i = 0; i <= circleSideCount; i++) normals[curIndex++] = Vector3.down;
                for (var i = 0; i <= circleSideCount; i++) normals[curIndex++] = Vector3.up;
            }

            //应用侧面的法线数组
            void ApplySideNormals()
            {
                for (var i = 0; i <= circleSideCount; i++)
                {
                    var rad = i * Mathf.PI * 2 / circleSideCount;
                    var cos = Mathf.Cos(rad);
                    var sin = Mathf.Sin(rad);
                    normals[curIndex++] = new Vector3(cos, 0, sin);
                    normals[curIndex++] = new Vector3(cos, 0, sin);
                }
            }
        }

        /// <summary>
        ///     获得圆锥台双面的法线数组。
        /// </summary>
        /// <param name="arrayLen"></param>
        /// <param name="circleSideCount"></param>
        /// <returns></returns>
        private static Vector3[] GetDoubleSideNormals(int arrayLen, int circleSideCount)
        {
            var curIndex = 0; //当前索引
            var normals = new Vector3[arrayLen];
            ApplyCircleNormals(); //底面和顶面
            ApplySideNormals(); //侧面
            return normals;

            //应用上下圆面的法线数组
            void ApplyCircleNormals()
            {
                for (var i = 0; i <= circleSideCount; i++) normals[curIndex++] = Vector3.down;
                for (var i = 0; i <= circleSideCount; i++) normals[curIndex++] = Vector3.up;

                for (var i = 0; i <= circleSideCount; i++) normals[curIndex++] = Vector3.up;
                for (var i = 0; i <= circleSideCount; i++) normals[curIndex++] = Vector3.down;
            }

            //应用侧面的法线数组
            void ApplySideNormals()
            {
                for (var i = 0; i <= circleSideCount; i++)
                {
                    var rad = i * Mathf.PI * 2 / circleSideCount;
                    var cos = Mathf.Cos(rad);
                    var sin = Mathf.Sin(rad);
                    normals[curIndex++] = new Vector3(cos, 0, sin);
                    normals[curIndex++] = new Vector3(cos, 0, sin);
                }

                for (var i = 0; i <= circleSideCount; i++)
                {
                    var rad = (circleSideCount - i) * Mathf.PI * 2 / circleSideCount;
                    var cos = Mathf.Cos(rad);
                    var sin = Mathf.Sin(rad);
                    normals[curIndex++] = new Vector3(cos, 0, sin);
                    normals[curIndex++] = new Vector3(cos, 0, sin);
                }
            }
        }

        /// <summary>
        ///     获得圆锥台单面的三角面数组。
        /// </summary>
        /// <param name="triArrayLen"></param>
        /// <param name="circleSideCount"></param>
        /// <returns></returns>
        private static int[] GetSingleSideTriangles(int triArrayLen, int circleSideCount)
        {
            var curIndex = 0; //当前索引
            var triangles = new int[triArrayLen];
            var startIndex = 0; //开始的索引
            ApplyCircleTriangles(startIndex); //底面和顶面
            startIndex += (circleSideCount + 1) * 2;
            ApplySideTriangles(startIndex); //侧面
            return triangles;

            //应用上下圆的三角面顶点的索引
            void ApplyCircleTriangles(int p_startIndex)
            {
                //底面
                for (var i = p_startIndex + 1; i < p_startIndex + circleSideCount; i++)
                {
                    triangles[curIndex++] = p_startIndex;
                    triangles[curIndex++] = i;
                    triangles[curIndex++] = i + 1;
                }

                triangles[curIndex++] = p_startIndex;
                triangles[curIndex++] = p_startIndex + circleSideCount;
                triangles[curIndex++] = p_startIndex + 1;

                //顶面
                p_startIndex += circleSideCount + 1;
                for (var i = p_startIndex + 1; i < p_startIndex + circleSideCount; i++)
                {
                    triangles[curIndex++] = p_startIndex;
                    triangles[curIndex++] = i + 1;
                    triangles[curIndex++] = i;
                }

                triangles[curIndex++] = p_startIndex;
                triangles[curIndex++] = p_startIndex + 1;
                triangles[curIndex++] = p_startIndex + circleSideCount;
            }

            //应用侧面的三角面顶点的索引
            void ApplySideTriangles(int p_startIndex)
            {
                for (var i = 0; i < circleSideCount; i++, p_startIndex += 2)
                {
                    triangles[curIndex++] = p_startIndex;
                    triangles[curIndex++] = p_startIndex + 1;
                    triangles[curIndex++] = p_startIndex + 3;

                    triangles[curIndex++] = p_startIndex + 3;
                    triangles[curIndex++] = p_startIndex + 2;
                    triangles[curIndex++] = p_startIndex;
                }
            }
        }

        /// <summary>
        ///     获得圆锥台双面的三角面数组。
        /// </summary>
        /// <param name="triArrayLen"></param>
        /// <param name="circleSideCount"></param>
        /// <returns></returns>
        private static int[] GetDoubleSideTriangles(int triArrayLen, int circleSideCount)
        {
            var curIndex = 0; //当前索引
            var triangles = new int[triArrayLen];
            var startIndex = 0; //开始的索引
            ApplyCircleTriangles(startIndex); //底面和顶面
            startIndex += (circleSideCount + 1) * 2 * 2;
            ApplySideTriangles(startIndex); //侧面
            return triangles;

            //应用上下圆的三角面顶点的索引
            void ApplyCircleTriangles(int p_startIndex)
            {
                //底面
                for (var i = p_startIndex + 1; i < p_startIndex + circleSideCount; i++)
                {
                    triangles[curIndex++] = p_startIndex;
                    triangles[curIndex++] = i;
                    triangles[curIndex++] = i + 1;
                }

                triangles[curIndex++] = p_startIndex;
                triangles[curIndex++] = p_startIndex + circleSideCount;
                triangles[curIndex++] = p_startIndex + 1;

                p_startIndex += circleSideCount + 1;
                for (var i = p_startIndex + 1; i < p_startIndex + circleSideCount; i++)
                {
                    triangles[curIndex++] = p_startIndex;
                    triangles[curIndex++] = i;
                    triangles[curIndex++] = i + 1;
                }

                triangles[curIndex++] = p_startIndex;
                triangles[curIndex++] = p_startIndex + circleSideCount;
                triangles[curIndex++] = p_startIndex + 1;


                //顶面
                p_startIndex += circleSideCount + 1;
                for (var i = p_startIndex + 1; i < p_startIndex + circleSideCount; i++)
                {
                    triangles[curIndex++] = p_startIndex;
                    triangles[curIndex++] = i + 1;
                    triangles[curIndex++] = i;
                }

                triangles[curIndex++] = p_startIndex;
                triangles[curIndex++] = p_startIndex + 1;
                triangles[curIndex++] = p_startIndex + circleSideCount;

                p_startIndex += circleSideCount + 1;
                for (var i = p_startIndex + 1; i < p_startIndex + circleSideCount; i++)
                {
                    triangles[curIndex++] = p_startIndex;
                    triangles[curIndex++] = i + 1;
                    triangles[curIndex++] = i;
                }

                triangles[curIndex++] = p_startIndex;
                triangles[curIndex++] = p_startIndex + 1;
                triangles[curIndex++] = p_startIndex + circleSideCount;
            }

            //应用侧面的三角面顶点的索引
            void ApplySideTriangles(int p_startIndex)
            {
                for (var i = 0; i < circleSideCount; i++, p_startIndex += 2)
                {
                    triangles[curIndex++] = p_startIndex;
                    triangles[curIndex++] = p_startIndex + 1;
                    triangles[curIndex++] = p_startIndex + 3;

                    triangles[curIndex++] = p_startIndex + 3;
                    triangles[curIndex++] = p_startIndex + 2;
                    triangles[curIndex++] = p_startIndex;
                }

                p_startIndex += 2;
                for (var i = 0; i < circleSideCount; i++, p_startIndex += 2)
                {
                    triangles[curIndex++] = p_startIndex;
                    triangles[curIndex++] = p_startIndex + 1;
                    triangles[curIndex++] = p_startIndex + 3;

                    triangles[curIndex++] = p_startIndex + 3;
                    triangles[curIndex++] = p_startIndex + 2;
                    triangles[curIndex++] = p_startIndex;
                }
            }
        }

        /// <summary>
        ///     获得圆锥台单面的UV数组。
        /// </summary>
        /// <param name="arrayLen"></param>
        /// <param name="circleSideCount"></param>
        /// <returns></returns>
        private static Vector2[] GetSingleSideUVs(int arrayLen, int circleSideCount)
        {
            var curIndex = 0; //当前索引
            var uvs = new Vector2[arrayLen];
            ApplyCircleUVs(); //底面和顶面
            ApplySideUVs(); //侧面
            return uvs;

            //应用上下圆的uv数组
            void ApplyCircleUVs()
            {
                //底面
                uvs[curIndex++] = new Vector2(0.5f, 0.5f);
                for (var i = 0; i < circleSideCount; i++)
                {
                    var rad = i * Mathf.PI * 2 / circleSideCount; //弧度
                    var cos = Mathf.Cos(rad);
                    var sin = Mathf.Sin(rad);
                    uvs[curIndex++] = new Vector2(cos, sin) * 0.5f + new Vector2(0.5f, 0.5f);
                }

                //顶面
                uvs[curIndex++] = new Vector2(0.5f, 0.5f);
                for (var i = 0; i < circleSideCount; i++)
                {
                    var rad = i * Mathf.PI * 2 / circleSideCount; //弧度
                    var cos = Mathf.Cos(rad);
                    var sin = Mathf.Sin(rad);
                    uvs[curIndex++] = new Vector2(cos, sin) * 0.5f + new Vector2(0.5f, 0.5f);
                }
            }

            //应用侧面的uv数组
            void ApplySideUVs()
            {
                var value = 1.0f / circleSideCount;
                for (var i = 0; i <= circleSideCount; i++)
                {
                    var x = i * value;
                    uvs[curIndex++] = new Vector2(x, 0);
                    uvs[curIndex++] = new Vector2(x, 1);
                }
            }
        }

        /// <summary>
        ///     获得圆锥台双面的uv数组。
        /// </summary>
        /// <param name="arrayLen"></param>
        /// <param name="circleSideCount"></param>
        /// <returns></returns>
        private static Vector2[] GetDoubleSideUVs(int arrayLen, int circleSideCount)
        {
            var curIndex = 0; //当前索引
            var uvs = new Vector2[arrayLen];
            ApplyCircleUVs(); //底面和顶面
            ApplySideUVs(); //侧面
            return uvs;

            //应用上下圆的uv数组
            void ApplyCircleUVs()
            {
                //底面
                {
                    uvs[curIndex++] = new Vector2(0.5f, 0.5f);
                    for (var i = 0; i < circleSideCount; i++)
                    {
                        var rad = i * Mathf.PI * 2 / circleSideCount; //弧度
                        var cos = Mathf.Cos(rad);
                        var sin = Mathf.Sin(rad);
                        uvs[curIndex++] = new Vector2(cos, sin) * 0.5f + new Vector2(0.5f, 0.5f);
                    }

                    uvs[curIndex++] = new Vector2(0.5f, 0.5f);
                    for (var i = 0; i < circleSideCount; i++)
                    {
                        var rad = (circleSideCount - i - 1) * Mathf.PI * 2 / circleSideCount;
                        var cos = Mathf.Cos(rad);
                        var sin = Mathf.Sin(rad);
                        uvs[curIndex++] = new Vector2(cos, sin) * 0.5f + new Vector2(0.5f, 0.5f);
                    }
                }

                //顶面
                {
                    uvs[curIndex++] = new Vector2(0.5f, 0.5f);
                    for (var i = 0; i < circleSideCount; i++)
                    {
                        var rad = i * Mathf.PI * 2 / circleSideCount; //弧度
                        var cos = Mathf.Cos(rad);
                        var sin = Mathf.Sin(rad);
                        uvs[curIndex++] = new Vector2(cos, sin) * 0.5f + new Vector2(0.5f, 0.5f);
                    }

                    uvs[curIndex++] = new Vector2(0.5f, 0.5f);
                    for (var i = 0; i < circleSideCount; i++)
                    {
                        var rad = (circleSideCount - i - 1) * Mathf.PI * 2 / circleSideCount;
                        var cos = Mathf.Cos(rad);
                        var sin = Mathf.Sin(rad);
                        uvs[curIndex++] = new Vector2(cos, sin) * 0.5f + new Vector2(0.5f, 0.5f);
                    }
                }
            }

            //应用侧面的uv数组
            void ApplySideUVs()
            {
                var value = 1.0f / circleSideCount;
                for (var i = 0; i <= circleSideCount; i++)
                {
                    var x = i * value;
                    uvs[curIndex++] = new Vector2(x, 0);
                    uvs[curIndex++] = new Vector2(x, 1);
                }

                for (var i = circleSideCount; i >= 0; i--)
                {
                    var x = i * value;
                    uvs[curIndex++] = new Vector2(x, 0);
                    uvs[curIndex++] = new Vector2(x, 1);
                }
            }
        }

        #endregion
    }
}