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

        /// <summary>
        ///     生成圆锥体图形 mesh组件。
        /// </summary>
        /// <param name="height"></param>
        /// <param name="radius"></param>
        /// <param name="isDoubleSide"></param>
        /// <param name="meshName"></param>
        /// <param name="meshPivot"></param>
        /// <returns></returns>
        public static Mesh GenerateConeShapeMesh(float height, float radius, bool isDoubleSide = true,
            string meshName = "ConeMesh", MeshPivot meshPivot = MeshPivot.Center)
        {
            var shapeMesh = new ConeShapeMesh(height, radius, isDoubleSide, meshName, meshPivot);
            return shapeMesh.GenerateMesh();
        }

        /// <summary>
        ///     生成圆锥台图形 mesh组件。
        /// </summary>
        /// <param name="height"></param>
        /// <param name="topRadius"></param>
        /// <param name="bottomRadius"></param>
        /// <param name="isDoubleSide"></param>
        /// <param name="meshName"></param>
        /// <param name="meshPivot"></param>
        /// <returns></returns>
        public static Mesh GenerateFrustumShapeMesh(float height, float topRadius, float bottomRadius,
            bool isDoubleSide = true, string meshName = "FrustumMesh", MeshPivot meshPivot = MeshPivot.Center)
        {
            var shapeMesh = new FrustumShapeMesh(height, topRadius, bottomRadius, isDoubleSide, meshName, meshPivot);
            return shapeMesh.GenerateMesh();
        }

        /// <summary>
        ///     生成球图形 mesh组件。
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="isDoubleSide"></param>
        /// <param name="meshName"></param>
        /// <param name="meshPivot"></param>
        /// <returns></returns>
        public static Mesh GenerateSphereShapeMesh(float radius, bool isDoubleSide = true,
            string meshName = "SphereMesh", MeshPivot meshPivot = MeshPivot.Center)
        {
            var shapeMesh = new SphereShapeMesh(radius, isDoubleSide, meshName, meshPivot);
            return shapeMesh.GenerateMesh();
        }

        #endregion
    }
}