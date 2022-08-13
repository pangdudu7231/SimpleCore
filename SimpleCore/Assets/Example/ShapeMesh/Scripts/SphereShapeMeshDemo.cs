using UnityEngine;

namespace SimpleCore.ShapeMeshes.Example
{
    public class SphereShapeMeshDemo : MonoBehaviour
    {
        #region public members

        public MeshPivot meshPivot = MeshPivot.Center;

        #endregion

        #region unity functions

        private void Start()
        {
            var tempGo = new GameObject("SphereShape");
            var meshFilter = tempGo.AddComponent<MeshFilter>();
            meshFilter.mesh = ShapeMeshFactory.GenerateSphereShapeMesh(5, meshPivot: meshPivot);
            tempGo.AddComponent<MeshRenderer>().material = Resources.Load<Material>("ShapeMesh/Material");
            tempGo.AddComponent<MeshDisplay>();
        }

        #endregion
    }
}