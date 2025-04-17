using Newtonsoft.Json;
using UnityEngine;

namespace ThreeJsExporter.Models
{
    [System.Serializable]
    public class ThreeJsCameraData
    {
        public ThreeJsMetadata metadata { get; set; }
        [JsonProperty("object")]
        public ThreeJsObjectBase objectData { get; set; }
        
        public ThreeJsCameraData()
        {
            metadata = new ThreeJsMetadata();
        }
        
        public static ThreeJsCameraData FromCamera(Camera camera)
        {
            if (camera == null)
                return null;
                
            var cameraData = new ThreeJsCameraData();            
            cameraData.objectData = ThreeJsPerspectiveCamera.FromCamera(camera);
            return cameraData;
        }
    }
}