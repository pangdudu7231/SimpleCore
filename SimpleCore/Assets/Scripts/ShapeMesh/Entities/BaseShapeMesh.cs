using UnityEngine;

namespace SimpleCore.ShapeMeshes
{
    /// <summary>
    ///     图形 Mesh 的基类。
    /// </summary>
    public abstract class BaseShapeMesh
    {
        #region protected members

        protected readonly string _meshName; //mesh 的名称
        protected readonly MeshPivot _meshPivot; //图形 Mesh中心点的位置

        protected Mesh _unityMesh; //生成的 mesh 组件

        #endregion

        #region ctor

        /// <summary>
        ///     构造函数。
        /// </summary>
        /// <param name="meshName"></param>
        /// <param name="meshPivot"></param>
        protected BaseShapeMesh(string meshName, MeshPivot meshPivot)
        {
            _meshName = meshName;
            _meshPivot = meshPivot;
        }

        #endregion

        #region public  functions

        /// <summary>
        ///     生成 mesh 组件
        /// </summary>
        /// <returns></returns>
        public Mesh GenerateMesh()
        {
            var mesh = new Mesh {name = _meshName};
            var vertexOffset = GetVertexOffset();
            var arrayLen = GetArrayLen();
            var triArrayLen = GetTriArrayLen();
            mesh.vertices = GetVertices(arrayLen, vertexOffset);
            mesh.normals = GetNormals(arrayLen);
            mesh.triangles = GetTriangles(triArrayLen);
            mesh.uv = GetUVs(arrayLen);
            mesh.RecalculateTangents();
            mesh.RecalculateBounds();
            mesh.Optimize();
            return mesh;
        }

        #endregion

        #region abstract functions

        /// <summary>
        ///     根据 MeshPivot 获得顶点位置的偏移量。
        /// </summary>
        /// <returns></returns>
        protected abstract Vector3 GetVertexOffset();

        /// <summary>
        ///     获得数组的长度。
        ///     顶点数组、法线数组、切线数组、UV数组长度一致。
        /// </summary>
        /// <returns></returns>
        protected abstract int GetArrayLen();

        /// <summary>
        ///     获得三角面顶点索引的数组长度。
        /// </summary>
        /// <returns></returns>
        protected abstract int GetTriArrayLen();

        /// <summary>
        ///     获得顶点的数据集合。
        /// </summary>
        /// <param name="arrayLen"></param>
        /// <param name="vertexOffset"></param>
        /// <returns></returns>
        protected abstract Vector3[] GetVertices(int arrayLen, Vector3 vertexOffset);

        /// <summary>
        ///     获得法线方向的数据集合。
        /// </summary>
        /// <param name="arrayLen"></param>
        /// <returns></returns>
        protected abstract Vector3[] GetNormals(int arrayLen);

        /// <summary>
        ///     获得三角面顶点的索引。
        /// </summary>
        /// <param name="triArrayLen"></param>
        /// <returns></returns>
        protected abstract int[] GetTriangles(int triArrayLen);

        /// <summary>
        ///     获得UV坐标的数据集合。
        /// </summary>
        /// <param name="arrayLen"></param>
        /// <returns></returns>
        protected abstract Vector2[] GetUVs(int arrayLen);

        #endregion
    }
}