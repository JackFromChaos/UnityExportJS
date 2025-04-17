using System;
using System.Collections.Generic;
using UnityEngine;
using ThreeJsExporter.Models;

namespace ThreeJsExporter.Converters
{
    public class GroupConverter : IUnityObjectConverter
    {
        public bool CanConvert(Transform transform)
        {
            return true;
        }

        public ThreeJsObjectBase Convert(Transform transform)
        {
            return ThreeJsObject.CreateGroup(transform);
        }
    }
}