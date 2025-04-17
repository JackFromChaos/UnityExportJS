using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using ThreeJsExporter.Converters;

namespace ThreeJsExporter
{
    public class ThreeJsSceneBuilder
    {
        private readonly List<IUnityObjectConverter> _converters;

        public ThreeJsSceneBuilder()
        {
            _converters = new List<IUnityObjectConverter>
        {
            new PerspectiveCameraConverter(),
            new CubeConverter()
        };
        }

        public SimpleScene BuildScene()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            var sceneRoot = new SimpleSceneObject
            {
                id = "scene",
                name = currentScene.name,
                type = ObjectType.Scene,
                position = new float[] { 0, 0, 0 },
                rotation = new float[] { 0, 0, 0 },
                scale = new float[] { 1, 1, 1 }
            };

            var objectMap = new Dictionary<Transform, SimpleSceneObject>();

            foreach (Transform transform in UnityEngine.Object.FindObjectsOfType<Transform>())
            {
                var converter = _converters.FirstOrDefault(c => c.CanConvert(transform));
                if (converter != null)
                {
                    objectMap[transform] = converter.Convert(transform);
                }
            }

            var allTransforms = objectMap.Keys.ToList();
            foreach (var transform in allTransforms)
            {
                var parent = transform.parent;
                while (parent != null && !objectMap.ContainsKey(parent))
                {
                    objectMap[parent] = SimpleSceneObject.CreateFromTransform(parent, ObjectType.Group);
                    parent = parent.parent;
                }
            }

            var addedObjects = new HashSet<string>();

            foreach (var entry in objectMap)
            {
                var transform = entry.Key;
                var obj = entry.Value;

                if (transform.parent == null)
                {
                    if (addedObjects.Add(obj.id)) 
                    {
                        sceneRoot.children.Add(obj);
                    }
                }
                else if (objectMap.TryGetValue(transform.parent, out var parentObj))
                {
                    if (addedObjects.Add(obj.id))
                    {
                        parentObj.children.Add(obj);
                    }
                }
            }

            var scene = new SimpleScene
            {
                scene = sceneRoot
            };

            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                scene.camera = SimpleSceneObject.CreateCamera(mainCamera);
            }

            return scene;
        }
    }
}