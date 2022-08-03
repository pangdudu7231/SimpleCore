using UnityEngine;

namespace SimpleCore.ShapeMeshes.Example
{
    public class PlaneShapeMeshDemo : MonoBehaviour
    {
        #region public members

        public MeshPivot meshPivot = MeshPivot.Center;

        #endregion
        
        #region unity functions

        private void Start()
        {
            var shapeMesh = new PlaneShapeMesh(5, 5, meshPivot: meshPivot);

            var tempGo = new GameObject("PlaneShape");
            var meshFilter = tempGo.AddComponent<MeshFilter>();
            meshFilter.mesh = shapeMesh.UnityMesh;
            tempGo.AddComponent<MeshRenderer>().material = Resources.Load<Material>("ShapeMesh/Material");
            tempGo.AddComponent<MeshDisplay>();
        }

        #endregion
    }
}