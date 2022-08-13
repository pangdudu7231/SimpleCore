using UnityEngine;

namespace SimpleCore.ShapeMeshes.Example
{
    public class ConeShapeMeshDemo : MonoBehaviour
    {
        #region public members

        public MeshPivot meshPivot = MeshPivot.Center;

        #endregion

        #region unity functions

        private void Start()
        {
            var tempGo = new GameObject("ConeShape");
            var meshFilter = tempGo.AddComponent<MeshFilter>();
            meshFilter.mesh = ShapeMeshFactory.GenerateConeShapeMesh(5, 2, meshPivot: meshPivot);
            tempGo.AddComponent<MeshRenderer>().material = Resources.Load<Material>("ShapeMesh/Material");
            tempGo.AddComponent<MeshDisplay>();
        }

        #endregion
    }
}