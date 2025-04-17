using System.Collections.Generic;
using UnityEngine;

namespace ThreeJsExporter.Models
{
    [System.Serializable]
    public class ThreeJsObjectBase
    {
        public string uuid { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public int layers { get; set; }
        public float[] matrix { get; set; }
        public float[] up { get; set; }
        
        public float[] position { get; set; }
        public float[] rotation { get; set; }
        public float[] scale { get; set; }
        public string rotationOrder { get; set; } = "XYZ";

        public List<ThreeJsObjectBase> children { get; set; } = new List<ThreeJsObjectBase>();

        public ThreeJsObjectBase()
        {
            uuid = System.Guid.NewGuid().ToString();
            layers = 1;
            up = new float[] { 0, 1, 0 };
            rotationOrder = "XYZ";
            matrix = new float[] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 };
        }
        
        public ThreeJsObjectBase(string type, string name = null) : this()
        {
            this.type = type;
            this.name = name ?? type;
        }
        
        public static ThreeJsObjectBase FromTransform(Transform transform, string type)
        {
            var obj = new ThreeJsObjectBase(type, transform.name);
            obj.matrix = TransformUtils.ConvertTransformToThreeJsMatrix(transform);
            TransformUtils.SetThreeJsTransform(transform, obj);
            return obj;
        }
    }
}