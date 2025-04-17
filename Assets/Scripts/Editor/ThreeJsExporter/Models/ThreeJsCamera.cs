namespace ThreeJsExporter.Models
{
    [System.Serializable]
    public class ThreeJsCamera : ThreeJsObjectBase
    {
        public float zoom { get; set; } = 1;
        public float near { get; set; } = 0.1f;
        public float far { get; set; } = 2000f;
        
        public ThreeJsCamera() : base()
        {
            zoom = 1;
            near = 0.1f;
            far = 2000f;
        }
        
        public ThreeJsCamera(string type, string name = null) : base(type, name)
        {
            zoom = 1;
            near = 0.1f;
            far = 2000f;
        }
    }
}