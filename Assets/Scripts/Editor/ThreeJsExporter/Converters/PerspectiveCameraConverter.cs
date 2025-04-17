using UnityEngine;

namespace ThreeJsExporter.Converters
{
    public class PerspectiveCameraConverter : IUnityObjectConverter
    {
        public bool CanConvert(Transform transform)
        {
            Camera camera = transform.GetComponent<Camera>();
            return camera != null && !camera.orthographic;
        }

        public SimpleSceneObject Convert(Transform transform)
        {
            Camera camera = transform.GetComponent<Camera>();
            return SimpleSceneObject.CreateCamera(camera);
        }
    }
}