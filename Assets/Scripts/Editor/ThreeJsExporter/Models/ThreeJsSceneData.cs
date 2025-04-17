using System.Collections.Generic;
using Newtonsoft.Json;

namespace ThreeJsExporter.Models
{
    [System.Serializable]
    public class ThreeJsSceneData
    {
        public ThreeJsMetadata metadata { get; set; }
        public List<ThreeJsGeometry> geometries { get; set; }
        public List<ThreeJsMaterial> materials { get; set; }
        [JsonProperty("object")]
        public ThreeJsObject objectData { get; set; }
        
        public ThreeJsSceneData()
        {
            metadata = new ThreeJsMetadata();
            geometries = new List<ThreeJsGeometry>();
            materials = new List<ThreeJsMaterial>();
        }
    }
}