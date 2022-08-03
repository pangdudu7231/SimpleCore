using UnityEngine;

namespace SimpleCore.ShapeMeshes.Example
{
    /// <summary>
    ///     显示 mesh 的法线和切线的脚本。
    /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    public class MeshDisplay : MonoBehaviour
    {
        #region public members

        public bool showNormals = true; //是否显示法线
        public bool showTangents; //是否显示切线
        public float displayLength = 1.0f; //显示线的长度
        public Color normalColor = Color.blue; //法线线条的颜色
        public Color tangentColor = Color.green; //切线线条的颜色

        #endregion

        #region unity functions

        private void OnDrawGizmosSelected()
        {
            var mesh = GetComponent<MeshFilter>().sharedMesh;
            if (mesh == null)
            {
                Debug.LogError("Find mesh failure.");
                return;
            }

            var vertices = mesh.vertices; //mesh的顶点数组
            var normals = mesh.normals; //mesh的法线数组
            var tangents = mesh.tangents; //mesh的切线数组
            var canShowNormals = showNormals && normals.Length == vertices.Length; //可以显示法线
            var canShowTangents = showTangents && tangents.Length == vertices.Length; //可以显示切线

            foreach (var index in mesh.triangles) //遍历
            {
                //将顶点局部坐标的位置转换到世界坐标的位置（受物体比例的影响）
                var vertex = transform.TransformPoint(vertices[index]);

                if (canShowNormals)
                {
                    //将方向x、y、z从局部空间转换到世界空间
                    //此操作不受变换的比例或位置的影响，返回的向量和初始向量具有相同的长度
                    var normal = transform.TransformDirection(normals[index]);
                    Gizmos.color = normalColor;
                    Gizmos.DrawLine(vertex, vertex + normal * displayLength);
                }

                if (canShowTangents)
                {
                    //将方向x、y、z从局部空间转换到世界空间
                    //此操作不受变换的比例或位置的影响，返回的向量和初始向量具有相同的长度
                    var tangent = transform.TransformDirection(tangents[index]);
                    Gizmos.color = tangentColor;
                    Gizmos.DrawLine(vertex, vertex + tangent * displayLength);
                }
            }
        }

        #endregion
    }
}