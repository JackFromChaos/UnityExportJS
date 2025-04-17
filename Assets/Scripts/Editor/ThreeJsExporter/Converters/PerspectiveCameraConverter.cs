using UnityEngine;
using System;
using System.Collections.Generic;
using ThreeJsExporter.Models;

namespace ThreeJsExporter.Converters
{
    public class PerspectiveCameraConverter : IUnityObjectConverter
    {
        public bool CanConvert(Transform transform)
        {
            Camera camera = transform.GetComponent<Camera>();
            return camera != null && !camera.orthographic;
        }

        public ThreeJsObjectBase Convert(Transform transform)
        {
            Camera camera = transform.GetComponent<Camera>();
            return ThreeJsPerspectiveCamera.FromCamera(camera);
        }
    }
}