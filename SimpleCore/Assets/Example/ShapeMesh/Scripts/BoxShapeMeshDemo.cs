﻿using UnityEngine;

namespace SimpleCore.ShapeMeshes.Example
{
    public class BoxShapeMeshDemo : MonoBehaviour
    {
        #region public members

        public MeshPivot meshPivot = MeshPivot.Center;

        #endregion
        
        #region unity functions

        private void Start()
        {
            var shapeMesh = new BoxShapeMesh(2, 2, 2, meshPivot: meshPivot);

            var tempGo = new GameObject("BoxShape");
            var meshFilter = tempGo.AddComponent<MeshFilter>();
            meshFilter.mesh = shapeMesh.UnityMesh;
            tempGo.AddComponent<MeshRenderer>().material = Resources.Load<Material>("ShapeMesh/Material");
            tempGo.AddComponent<MeshDisplay>();
        }

        #endregion
    }
}