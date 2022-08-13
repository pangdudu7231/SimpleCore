using UnityEngine;

namespace SimpleCore.ShapeMeshes.Example
{
    public class FrustumShapeMeshDemo : MonoBehaviour
    {
        #region public members

        public MeshPivot meshPivot = MeshPivot.Center;

        #endregion

        #region unity functions

        private void Start()
        {
            var tempGo = new GameObject("FrustumShape");
            var meshFilter = tempGo.AddComponent<MeshFilter>();
            meshFilter.mesh = ShapeMeshFactory.GenerateFrustumShapeMesh(5, 2, 3, meshPivot: meshPivot);
            tempGo.AddComponent<MeshRenderer>().material = Resources.Load<Material>("ShapeMesh/Material");
            tempGo.AddComponent<MeshDisplay>();
        }

        #endregion
    }
}