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
        //protected readonly bool _isDoubleSide; //mesh 是否是双面的

        protected Mesh _unityMesh; //生成的 mesh 组件

        #endregion

        #region public properties

        /// <summary>
        ///     获取生成的 mesh 组件
        /// </summary>
        public Mesh UnityMesh => _unityMesh ??= GenerateMesh();

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

        #region protected functions

        /// <summary>
        ///     生成 mesh 组件
        /// </summary>
        /// <returns></returns>
        protected Mesh GenerateMesh()
        {
            var mesh = new Mesh {name = _meshName};
            var vertexOffset = GetVertexOffset();
            mesh.vertices = GetVertices(vertexOffset);
            mesh.normals = GetNormals();
            mesh.triangles = GetTriangles();
            mesh.uv = GetUVs();
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
        /// 获得顶点的数据集合。
        /// </summary>
        /// <param name="vertexOffset"></param>
        /// <returns></returns>
        protected abstract Vector3[] GetVertices(Vector3 vertexOffset);

        /// <summary>
        ///     获得法线方向的数据集合。
        /// </summary>
        /// <returns></returns>
        protected abstract Vector3[] GetNormals();

        /// <summary>
        ///     获得三角面顶点的索引。
        /// </summary>
        /// <returns></returns>
        protected abstract int[] GetTriangles();

        /// <summary>
        ///     获得UV坐标的数据集合。
        /// </summary>
        /// <returns></returns>
        protected abstract Vector2[] GetUVs();

        #endregion
    }
}