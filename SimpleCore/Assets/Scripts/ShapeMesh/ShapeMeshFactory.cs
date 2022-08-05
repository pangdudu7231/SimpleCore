using UnityEngine;

namespace SimpleCore.ShapeMeshes
{
    /// <summary>
    /// 图形 mesh 的工厂类。
    /// </summary>
    public static class ShapeMeshFactory
    {
        #region public static functions

        /// <summary>
        /// 生成平面图形 mesh组件。
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
        /// 生成立方体图形 mesh组件。
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
            BoxShapeMesh shapeMesh = isDoubleSide
                ? new BoxShapeDoubleSideMesh(xSize, ySize, zSize, meshName, meshPivot)
                : new BoxShapeSingleSideMesh(xSize, ySize, zSize, meshName, meshPivot);
            return shapeMesh.GenerateMesh();
        }

        #endregion
    }
}