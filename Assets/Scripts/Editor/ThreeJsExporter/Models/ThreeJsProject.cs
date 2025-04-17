namespace ThreeJsExporter.Models
{
    [System.Serializable]
    public class ThreeJsProject
    {
        public bool shadows { get; set; }
        public int shadowType { get; set; }
        public int toneMapping { get; set; }
        public float toneMappingExposure { get; set; }
        
        public ThreeJsProject()
        {
            shadows = true;
            shadowType = 1;
            toneMapping = 0;
            toneMappingExposure = 1;
        }
    }
}