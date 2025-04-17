using UnityEngine;

namespace ThreeJsExporter.Converters
{
    public interface IUnityObjectConverter
    {
        bool CanConvert(Transform transform);
        SimpleSceneObject Convert(Transform transform);
    }
}