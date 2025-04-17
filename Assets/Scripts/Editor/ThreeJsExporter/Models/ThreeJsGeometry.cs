namespace ThreeJsExporter.Models
{
    [System.Serializable]
    public class ThreeJsGeometry
    {
        public string uuid { get; set; }
        public string type { get; set; }
        public float width { get; set; }
        public float height { get; set; }
        public float depth { get; set; }
        public int widthSegments { get; set; }
        public int heightSegments { get; set; }
        public int depthSegments { get; set; }
        
        public ThreeJsGeometry()
        {
            uuid = System.Guid.NewGuid().ToString();
        }
        
        public static ThreeJsGeometry CreateBoxGeometry(string uuid = null, float width = 1, float height = 1, float depth = 1)
        {
            return new ThreeJsGeometry
            {
                uuid = uuid ?? System.Guid.NewGuid().ToString(),
                type = "BoxGeometry",
                width = width,
                height = height,
                depth = depth,
                widthSegments = 1,
                heightSegments = 1,
                depthSegments = 1
            };
        }
    }
}