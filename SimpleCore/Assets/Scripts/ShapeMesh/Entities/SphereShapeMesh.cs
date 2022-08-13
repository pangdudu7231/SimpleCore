using System;
using UnityEngine;

namespace SimpleCore.ShapeMeshes
{
    /// <summary>
    ///     球图形 mesh类。
    /// </summary>
    public sealed class SphereShapeMesh : BaseShapeMesh
    {
        private const int LONGITUDE = 24 * 2; //球经线的数量
        private const int LATITUDE = 16 * 2; //球纬线的数量

        #region private members

        private readonly float _radius; //球的半径
        private readonly bool _isDoubleSide; //mesh是否是双面的

        #endregion

        #region ctor

        /// <summary>
        ///     构造函数。
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="isDoubleSide"></param>
        /// <param name="meshName"></param>
        /// <param name="meshPivot"></param>
        public SphereShapeMesh(float radius, bool isDoubleSide, string meshName, MeshPivot meshPivot) : base(meshName,
            meshPivot)
        {
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
                MeshPivot.Top => new Vector3(0, _radius, 0),
                MeshPivot.Bottom => new Vector3(0, -_radius, 0),
                MeshPivot.Left => new Vector3(-_radius, 0, 0),
                MeshPivot.Right => new Vector3(_radius, 0, 0),
                MeshPivot.Front => new Vector3(0, 0, _radius),
                MeshPivot.Back => new Vector3(0, 0, -_radius),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        protected override int GetArrayLen()
        {
            var arrayLen = (LONGITUDE + 1) * (LATITUDE + 1);
            if (_isDoubleSide) arrayLen *= 2;
            return arrayLen;
        }

        protected override int GetTriArrayLen()
        {
            var triArrayLen = LONGITUDE * LATITUDE * 3 * 2;
            if (_isDoubleSide) triArrayLen *= 2;
            return triArrayLen;
        }

        protected override Vector3[] GetVertices(int arrayLen, Vector3 vertexOffset)
        {
            return _isDoubleSide
                ? GetDoubleSideVertices(_radius, arrayLen, vertexOffset)
                : GetSingleSideVertices(_radius, arrayLen, vertexOffset);
        }

        protected override Vector3[] GetNormals(int arrayLen)
        {
            return _isDoubleSide ? GetDoubleSideNormals(arrayLen) : GetSingleSideNormals(arrayLen);
        }

        protected override int[] GetTriangles(int triArrayLen)
        {
            return _isDoubleSide ? GetDoubleSideTriangles(triArrayLen) : GetSingleSideTriangles(triArrayLen);
        }

        protected override Vector2[] GetUVs(int arrayLen)
        {
            return _isDoubleSide ? GetDoubleSideUVs(arrayLen) : GetSingleSideUVs(arrayLen);
        }

        #endregion

        #region static functions

        /// <summary>
        ///     获得球单面的顶点数组。
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="arrayLen"></param>
        /// <param name="vertexOffset"></param>
        /// <returns></returns>
        private static Vector3[] GetSingleSideVertices(float radius, int arrayLen, Vector3 vertexOffset)
        {
            var curIndex = 0; //当前索引
            var vertices = new Vector3[arrayLen];
            ApplyVertices();
            return vertices;

            //应用点
            void ApplyVertices()
            {
                for (var lat = 0; lat <= LATITUDE; lat++)
                {
                    //经线角度的取值范围是0~180度
                    var latRad = lat * Mathf.PI / LATITUDE;
                    var latCos = Mathf.Cos(latRad);
                    var latSin = Mathf.Sin(latRad);
                    for (var lon = 0; lon <= LONGITUDE; lon++)
                    {
                        //纬线角度的取值范围是0~360度
                        var lonRad = lon * Mathf.PI * 2 / LONGITUDE;
                        var lonCos = Mathf.Cos(lonRad);
                        var lonSin = Mathf.Sin(lonRad);
                        vertices[curIndex++] = new Vector3(latSin * lonCos, latCos, latSin * lonSin) * radius -
                                               vertexOffset;
                    }
                }
            }
        }

        /// <summary>
        ///     获得球双面的顶点数组。
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="arrayLen"></param>
        /// <param name="vertexOffset"></param>
        /// <returns></returns>
        private static Vector3[] GetDoubleSideVertices(float radius, int arrayLen, Vector3 vertexOffset)
        {
            var curIndex = 0; //当前索引
            var vertices = new Vector3[arrayLen];
            ApplyVertices();
            return vertices;

            //应用点
            void ApplyVertices()
            {
                //正面
                for (var lat = 0; lat <= LATITUDE; lat++)
                {
                    //经线角度的取值范围是0~180度
                    var latRad = lat * Mathf.PI / LATITUDE;
                    var latCos = Mathf.Cos(latRad);
                    var latSin = Mathf.Sin(latRad);
                    for (var lon = 0; lon <= LONGITUDE; lon++)
                    {
                        //纬线角度的取值范围是0~360度
                        //每一圈的最后一个点和第一个点重合(设置UV的坐标)
                        var lonRad = lon * Mathf.PI * 2 / LONGITUDE;
                        var lonCos = Mathf.Cos(lonRad);
                        var lonSin = Mathf.Sin(lonRad);
                        vertices[curIndex++] = new Vector3(latSin * lonCos, latCos, latSin * lonSin) * radius -
                                               vertexOffset;
                    }
                }

                //反面
                for (var lat = 0; lat <= LATITUDE; lat++)
                {
                    //经线角度的取值范围是0~180度
                    var latRad = (LATITUDE - lat) * Mathf.PI / LATITUDE;
                    var latCos = Mathf.Cos(latRad);
                    var latSin = Mathf.Sin(latRad);
                    for (var lon = 0; lon <= LONGITUDE; lon++)
                    {
                        //纬线角度的取值范围是0~360度
                        var lonRad = lon * Mathf.PI * 2 / LONGITUDE;
                        var lonCos = Mathf.Cos(lonRad);
                        var lonSin = Mathf.Sin(lonRad);
                        vertices[curIndex++] = new Vector3(latSin * lonCos, latCos, latSin * lonSin) * radius -
                                               vertexOffset;
                    }
                }
            }
        }

        /// <summary>
        ///     获得球单面的法线数组。
        /// </summary>
        /// <param name="arrayLen"></param>
        /// <returns></returns>
        private static Vector3[] GetSingleSideNormals(int arrayLen)
        {
            var curIndex = 0; //当前索引
            var normals = new Vector3[arrayLen];
            ApplyNormals();
            return normals;

            //应用法线数组
            void ApplyNormals()
            {
                for (var lat = 0; lat <= LATITUDE; lat++)
                {
                    //经线角度的取值范围是0~180度
                    var latRad = lat * Mathf.PI / LATITUDE;
                    var latCos = Mathf.Cos(latRad);
                    var latSin = Mathf.Sin(latRad);
                    for (var lon = 0; lon <= LONGITUDE; lon++)
                    {
                        //纬线角度的取值范围是0~360度
                        var lonRad = lon * Mathf.PI * 2 / LONGITUDE;
                        var lonCos = Mathf.Cos(lonRad);
                        var lonSin = Mathf.Sin(lonRad);
                        normals[curIndex++] = new Vector3(latSin * lonCos, latCos, latSin * lonSin).normalized;
                    }
                }
            }
        }

        /// <summary>
        ///     获得球双面的法线数组。
        /// </summary>
        /// <param name="arrayLen"></param>
        /// <returns></returns>
        private static Vector3[] GetDoubleSideNormals(int arrayLen)
        {
            var curIndex = 0; //当前索引
            var normals = new Vector3[arrayLen];
            ApplyNormals();
            return normals;

            //应用法线数组
            void ApplyNormals()
            {
                //正面
                for (var lat = 0; lat <= LATITUDE; lat++)
                {
                    //经线角度的取值范围是0~180度
                    var latRad = lat * Mathf.PI / LATITUDE;
                    var latCos = Mathf.Cos(latRad);
                    var latSin = Mathf.Sin(latRad);
                    for (var lon = 0; lon <= LONGITUDE; lon++)
                    {
                        //纬线角度的取值范围是0~360度
                        var lonRad = lon * Mathf.PI * 2 / LONGITUDE;
                        var lonCos = Mathf.Cos(lonRad);
                        var lonSin = Mathf.Sin(lonRad);
                        normals[curIndex++] = new Vector3(latSin * lonCos, latCos, latSin * lonSin).normalized;
                    }
                }

                //反面
                for (var lat = 0; lat <= LATITUDE; lat++)
                {
                    //经线角度的取值范围是0~180度
                    var latRad = (LATITUDE - lat) * Mathf.PI / LATITUDE;
                    var latCos = Mathf.Cos(latRad);
                    var latSin = Mathf.Sin(latRad);
                    for (var lon = 0; lon <= LONGITUDE; lon++)
                    {
                        //纬线角度的取值范围是0~360度
                        var lonRad = lon * Mathf.PI * 2 / LONGITUDE;
                        var lonCos = Mathf.Cos(lonRad);
                        var lonSin = Mathf.Sin(lonRad);
                        normals[curIndex++] = new Vector3(latSin * lonCos, latCos, latSin * lonSin).normalized;
                    }
                }
            }
        }

        /// <summary>
        ///     获得球单面的三角面数组。
        /// </summary>
        /// <param name="triArrayLen"></param>
        /// <returns></returns>
        private static int[] GetSingleSideTriangles(int triArrayLen)
        {
            var curIndex = 0; //当前索引
            var triangles = new int[triArrayLen];
            ApplyTriangles();
            return triangles;

            //应用三角面
            void ApplyTriangles()
            {
                for (var lat = 0; lat < LATITUDE; lat++)
                for (var lon = 0; lon < LONGITUDE; lon++)
                {
                    var current = lat * (LONGITUDE + 1) + lon;
                    var next = current + LONGITUDE + 1;

                    triangles[curIndex++] = current;
                    triangles[curIndex++] = current + 1;
                    triangles[curIndex++] = next;

                    triangles[curIndex++] = next;
                    triangles[curIndex++] = current + 1;
                    triangles[curIndex++] = next + 1;
                }
            }
        }

        /// <summary>
        ///     获得球双面的三角面数组。
        /// </summary>
        /// <param name="triArrayLen"></param>
        /// <returns></returns>
        private static int[] GetDoubleSideTriangles(int triArrayLen)
        {
            var curIndex = 0; //当前索引
            var triangles = new int[triArrayLen];
            ApplyTriangles();
            return triangles;

            //应用三角面
            void ApplyTriangles()
            {
                //正面
                for (var lat = 0; lat < LATITUDE; lat++)
                for (var lon = 0; lon < LONGITUDE; lon++)
                {
                    var current = lat * (LONGITUDE + 1) + lon;
                    var next = current + LONGITUDE + 1;

                    triangles[curIndex++] = current;
                    triangles[curIndex++] = current + 1;
                    triangles[curIndex++] = next;

                    triangles[curIndex++] = next;
                    triangles[curIndex++] = current + 1;
                    triangles[curIndex++] = next + 1;
                }

                //反面
                const int startIndex = (LONGITUDE + 1) * (LATITUDE + 1);
                for (var lat = 0; lat < LATITUDE; lat++)
                for (var lon = 0; lon < LONGITUDE; lon++)
                {
                    var current = lat * (LONGITUDE + 1) + lon + startIndex;
                    var next = current + LONGITUDE + 1;

                    triangles[curIndex++] = current;
                    triangles[curIndex++] = current + 1;
                    triangles[curIndex++] = next;

                    triangles[curIndex++] = next;
                    triangles[curIndex++] = current + 1;
                    triangles[curIndex++] = next + 1;
                }
            }
        }

        /// <summary>
        ///     获得球单面的UV数组。
        /// </summary>
        /// <param name="arrayLen"></param>
        /// <returns></returns>
        private static Vector2[] GetSingleSideUVs(int arrayLen)
        {
            var curIndex = 0; //当前索引
            var uvs = new Vector2[arrayLen];
            ApplyUVs();
            return uvs;

            //应用UV数组
            void ApplyUVs()
            {
                for (var lat = 0; lat <= LATITUDE; lat++)
                for (var lon = 0; lon <= LONGITUDE; lon++)
                    uvs[curIndex++] = new Vector2((float) lon / LONGITUDE, 1f - (float) lat / LATITUDE);
            }
        }

        /// <summary>
        ///     获得球双面的UV数组。
        /// </summary>
        /// <param name="arrayLen"></param>
        /// <returns></returns>
        private static Vector2[] GetDoubleSideUVs(int arrayLen)
        {
            var curIndex = 0; //当前索引
            var uvs = new Vector2[arrayLen];
            ApplyUVs();
            return uvs;

            //应用UV数组
            void ApplyUVs()
            {
                //正面
                for (var lat = 0; lat <= LATITUDE; lat++)
                for (var lon = 0; lon <= LONGITUDE; lon++)
                    uvs[curIndex++] = new Vector2((float) lon / LONGITUDE, 1f - (float) lat / LATITUDE);

                //反面
                for (var lat = 0; lat <= LATITUDE; lat++)
                for (var lon = 0; lon <= LONGITUDE; lon++)
                    uvs[curIndex++] = new Vector2((float) (LONGITUDE - lon) / LONGITUDE,
                        1f - (float) (LATITUDE - lat) / LATITUDE);
            }
        }

        #endregion
    }
}