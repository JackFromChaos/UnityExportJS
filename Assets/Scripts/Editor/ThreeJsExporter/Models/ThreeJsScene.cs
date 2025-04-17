using System.Collections.Generic;
using Newtonsoft.Json;

namespace ThreeJsExporter.Models
{
    [System.Serializable]
    public class ThreeJsScene
    {
        public ThreeJsMetadata metadata { get; set; } = new ThreeJsMetadata
        {
            version = 4.6f,
            type = "Object",
            generator = "Unity ThreeJs Exporter"
        };
        public ThreeJsProject project { get; set; }
        public ThreeJsCameraData camera { get; set; }
        public ThreeJsSceneData scene { get; set; }
        public Dictionary<string, object> scripts { get; set; } = new Dictionary<string, object>();
        public ThreeJsHistory history { get; set; } = new ThreeJsHistory();
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public Dictionary<string, object> environment { get; set; } = new Dictionary<string, object>();
        
        public ThreeJsScene()
        {
            project = new ThreeJsProject
            {
                shadows = true,
                shadowType = 1,
                toneMapping = 0,
                toneMappingExposure = 1
            };
            
            history = new ThreeJsHistory();
        }
        
        public static ThreeJsScene CreateDefault()
        {
            return new ThreeJsScene();
        }
        
        public static JsonSerializerSettings GetSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.Indented
            };
        }
    }
}