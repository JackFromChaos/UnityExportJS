using UnityEngine;
using ThreeJsExporter.Models;

namespace ThreeJsExporter.Converters
{
    public interface IUnityObjectConverter
    {
        bool CanConvert(Transform transform);
        ThreeJsObjectBase Convert(Transform transform);
    }
}