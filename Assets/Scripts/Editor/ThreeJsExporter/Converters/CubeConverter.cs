using UnityEngine;

namespace ThreeJsExporter.Converters
{
    public class CubeConverter : IUnityObjectConverter
    {
        public bool CanConvert(Transform transform)
        {
            MeshFilter meshFilter = transform.GetComponent<MeshFilter>();
            if (meshFilter != null && meshFilter.sharedMesh != null)
            {
                string meshName = meshFilter.sharedMesh.name;
                if (meshName == "Cube" || meshName.StartsWith("Cube "))
                    return true;
            }
            return false;
        }

        public SimpleSceneObject Convert(Transform transform)
        {
            return SimpleSceneObject.CreateFromTransform(transform, ObjectType.Box);
        }
    }
}