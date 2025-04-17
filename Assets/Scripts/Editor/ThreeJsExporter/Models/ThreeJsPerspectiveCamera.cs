using UnityEngine;

namespace ThreeJsExporter.Models
{
    [System.Serializable]
    public class ThreeJsPerspectiveCamera : ThreeJsCamera
    {
        public float fov { get; set; } = 50f;
        public float aspect { get; set; } = 1.77778f; // 16:9
        public float focus { get; set; } = 10f;
        public float filmGauge { get; set; } = 35f;
        public float filmOffset { get; set; } = 0f;
        
        public ThreeJsPerspectiveCamera() : base("PerspectiveCamera")
        {
            fov = 50f;
            aspect = 1.77778f; // 16:9
            focus = 10f;
            filmGauge = 35f;
            filmOffset = 0f;
        }
        
        public static ThreeJsPerspectiveCamera FromCamera(Camera camera)
        {
            if (camera == null || camera.orthographic)
                return null;
                
            var cam = new ThreeJsPerspectiveCamera();
            cam.name = camera.name;
            cam.matrix = TransformUtils.ConvertTransformToThreeJsMatrix(camera.transform);
            TransformUtils.SetThreeJsTransform(camera.transform, cam);
            cam.fov = camera.fieldOfView;
            cam.near = camera.nearClipPlane;
            cam.far = camera.farClipPlane;
            cam.aspect = camera.aspect;
            
            return cam;
        }
    }
}