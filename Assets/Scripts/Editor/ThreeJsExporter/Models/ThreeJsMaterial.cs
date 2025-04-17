using UnityEngine;

namespace ThreeJsExporter.Models
{
    [System.Serializable]
    public class ThreeJsMaterial
    {
        public string uuid { get; set; }
        public string type { get; set; }
        public string name { get; set; } 
        public object color { get; set; } 
        public float roughness { get; set; }
        public float metalness { get; set; }
        public int emissive { get; set; }
        public object[] envMapRotation { get; set; }
        public float envMapIntensity { get; set; }
        public int blendColor { get; set; }
        
        public ThreeJsMaterial()
        {
            uuid = System.Guid.NewGuid().ToString();
            envMapRotation = new object[] { 0, 0, 0, "XYZ" };
        }
        
        public static ThreeJsMaterial CreateStandardMaterial(string uuid = null, string name = "Default Material", Color? color = null)
        {
            Color materialColor = color ?? Color.white;
            int colorValue = (int)(materialColor.r * 255) << 16 | (int)(materialColor.g * 255) << 8 | (int)(materialColor.b * 255);
            
            return new ThreeJsMaterial
            {
                uuid = uuid ?? System.Guid.NewGuid().ToString(),
                type = "MeshStandardMaterial",
                name = name,
                color = colorValue,
                roughness = 0.5f,
                metalness = 0.0f,
                emissive = 0,
                envMapRotation = new object[] { 0, 0, 0, "XYZ" },
                envMapIntensity = 1,
                blendColor = 0
            };
        }
        
        public static ThreeJsMaterial FromUnityMaterial(Material material, string uuid = null)
        {
            if (material == null)
                return CreateStandardMaterial();
                
            Color color = material.color;
            int colorValue = (int)(color.r * 255) << 16 | (int)(color.g * 255) << 8 | (int)(color.b * 255);
            
            float roughness = 0.5f;
            float metalness = 0.0f;
            
            if (material.HasProperty("_Glossiness"))
                roughness = 1 - material.GetFloat("_Glossiness");
                
            if (material.HasProperty("_Metallic"))
                metalness = material.GetFloat("_Metallic");
                
            return new ThreeJsMaterial
            {
                uuid = uuid ?? System.Guid.NewGuid().ToString(),
                type = "MeshStandardMaterial",
                name = material.name,
                color = colorValue,
                roughness = roughness,
                metalness = metalness,
                emissive = 0,
                envMapRotation = new object[] { 0, 0, 0, "XYZ" },
                envMapIntensity = 1,
                blendColor = 0
            };
        }
    }
}