using UnityEngine;

namespace SimpleCore.ShapeMeshes
{
    /// <summary>
    ///     图形 mesh 的工厂类。
    /// </summary>
    public static class ShapeMeshFactory
    {
        #region public static functions

        /// <summary>
        ///     生成平面图形 mesh组件。
        /// </summary>
        /// <param name="xSize"></param>
        /// <param name="zSize"></param>
        /// <param name="meshName"></param>
        /// <param name="meshPivot"></param>
        /// <returns></returns>
        public static Mesh GeneratePlaneShapeMesh(float xSize, float zSize, string meshName = "PlaneMesh",
            MeshPivot meshPivot = MeshPivot.Center)
        {
            var shapeMesh = new PlaneShapeMesh(xSize, zSize, meshName, meshPivot);
            return shapeMesh.GenerateMesh();
        }

        /// <summary>
        ///     生成立方体图形 mesh组件。
        /// </summary>
        /// <param name="xSize"></param>
        /// <param name="ySize"></param>
        /// <param name="zSize"></param>
        /// <param name="isDoubleSide"></param>
        /// <param name="meshName"></param>
        /// <param name="meshPivot"></param>
        /// <returns></returns>
        public static Mesh GenerateBoxShapeMesh(float xSize, float ySize, float zSize, bool isDoubleSide = true,
            string meshName = "BoxMesh", MeshPivot meshPivot = MeshPivot.Center)
        {
            var shapeMesh = new BoxShapeMesh(xSize, ySize, zSize, isDoubleSide, meshName, meshPivot);
            return shapeMesh.GenerateMesh();
        }

        /// <summary>
        ///     生成圆柱体图形 mesh组件。
        /// </summary>
        /// <param name="height"></param>
        /// <param name="radius"></param>
        /// <param name="isDoubleSide"></param>
        /// <param name="meshName"></param>
        /// <param name="meshPivot"></param>
        /// <returns></returns>
        public static Mesh GenerateCylinderShapeMesh(float height, float radius, bool isDoubleSide = true,
            string meshName = "CylinderMesh", MeshPivot meshPivot = MeshPivot.Center)
        {
            var shapeMesh = new CylinderShapeMesh(height, radius, isDoubleSide, meshName, meshPivot);
            return shapeMesh.GenerateMesh();
        }

        #endregion
    }
}