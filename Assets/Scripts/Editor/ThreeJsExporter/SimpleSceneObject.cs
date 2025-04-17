// SimpleSceneObject.cs (поместить в корень)
using System.Collections.Generic;
using UnityEngine;

namespace ThreeJsExporter
{

    [System.Serializable]
    public enum ObjectType
    {
        Scene,
        Group,
        Box,
        PerspectiveCamera
    }

    [System.Serializable]
    public class SimpleSceneObject
    {
        public string id;
        public string name;
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public ObjectType type; public float[] position = new float[3];
        public float[] rotation = new float[3];
        public float[] scale = new float[3];
        public List<SimpleSceneObject> children = new List<SimpleSceneObject>();

        public float? fov;
        public float? near;
        public float? far;
        public float? aspect;

        public static SimpleSceneObject CreateFromTransform(Transform transform, ObjectType type)
        {
            var obj = new SimpleSceneObject
            {
                id = transform.GetInstanceID().ToString(),
                name = transform.name,
                type = type
            };

            // Преобразование координат Unity в Three.js
            Vector3 pos = transform.localPosition;
            obj.position = new float[] { pos.x, pos.y, -pos.z };

            Vector3 rot = transform.localEulerAngles;
            obj.rotation = new float[] {
                -rot.x * Mathf.Deg2Rad,
                -rot.y * Mathf.Deg2Rad,
                rot.z * Mathf.Deg2Rad
            };

            Vector3 scl = transform.localScale;
            obj.scale = new float[] { scl.x, scl.y, scl.z };

            return obj;
        }

        public static SimpleSceneObject CreateCamera(Camera camera)
        {
            var obj = CreateFromTransform(camera.transform, ObjectType.PerspectiveCamera);
            obj.fov = camera.fieldOfView;
            obj.near = camera.nearClipPlane;
            obj.far = camera.farClipPlane;
            obj.aspect = camera.aspect;
            return obj;
        }
    }

    // SimpleScene.cs
    [System.Serializable]
    public class SimpleScene
    {
        public SimpleSceneObject scene;
        public SimpleSceneObject camera;

        public static Newtonsoft.Json.JsonSerializerSettings GetSerializerSettings()
        {
            return new Newtonsoft.Json.JsonSerializerSettings
            {
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                Formatting = Newtonsoft.Json.Formatting.Indented
            };
        }
    }
}