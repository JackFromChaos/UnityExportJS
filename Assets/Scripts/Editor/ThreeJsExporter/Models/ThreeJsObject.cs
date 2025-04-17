using Newtonsoft.Json;
using UnityEngine;

namespace ThreeJsExporter.Models
{
    [System.Serializable]
    public class ThreeJsObject : ThreeJsObjectBase
    {
        public const string DefaultMaterialId="default";

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string geometry { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string material { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object color { get; set; }
        
        
        public ThreeJsObject() : base()
        {
        }
        
        public ThreeJsObject(string type, string name = null) : base(type, name)
        {
        }
        
        public static ThreeJsObject CreateScene()
        {
            return new ThreeJsObject("Scene", "Scene")
            {
                matrix = new float[] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 },
                position = new float[] { 0, 0, 0 },
                rotation = new float[] { 0, 0, 0 },
                scale = new float[] { 1, 1, 1 },
            };
        }
        
        public static ThreeJsObject CreateGroup(Transform transform)
        {
            var group = new ThreeJsObject("Group", transform.name);
            group.matrix = TransformUtils.ConvertTransformToThreeJsMatrix(transform);
            TransformUtils.SetThreeJsTransform(transform, group);
            return group;
        }
        
        public static ThreeJsObject CreateMesh(Transform transform, string geometryId)
        {
            var mesh = new ThreeJsObject("Mesh", transform.name);
            mesh.matrix = TransformUtils.ConvertTransformToThreeJsMatrix(transform);
            TransformUtils.SetThreeJsTransform(transform, mesh);
            mesh.geometry = geometryId;
            mesh.material = DefaultMaterialId;
            return mesh;
        }
        
    }
}