namespace ThreeJsExporter.Models
{
    [System.Serializable]
    public class ThreeJsMetadata
    {
        public float version { get; set; }
        public string type { get; set; }
        public string generator { get; set; }
        
        public ThreeJsMetadata()
        {
            version = 4.6f;
            type = "Object";
            generator = "Unity ThreeJs Exporter";
        }
    }
}