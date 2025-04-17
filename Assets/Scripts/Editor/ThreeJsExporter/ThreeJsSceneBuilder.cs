using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using ThreeJsExporter.Models;
using ThreeJsExporter.Converters;

namespace ThreeJsExporter
{
    public class ThreeJsSceneBuilder
    {
        private readonly List<IUnityObjectConverter> _converters;
        private readonly Dictionary<string, ThreeJsGeometry> _geometries = new Dictionary<string, ThreeJsGeometry>();
        private readonly Dictionary<string, ThreeJsMaterial> _materials = new Dictionary<string, ThreeJsMaterial>();

        const string DefaultMaterialId = "default";

        public ThreeJsSceneBuilder()
        {
            _converters = new List<IUnityObjectConverter>
            {
                new PerspectiveCameraConverter(),
                new CubeConverter(),
                new GroupConverter() 
            };
            
            // Добавляем стандартный материал
            _materials[DefaultMaterialId] = ThreeJsMaterial.CreateStandardMaterial(DefaultMaterialId);
        }

        public ThreeJsScene BuildScene()
        {
            var scene = ThreeJsScene.CreateDefault();

            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                scene.camera = ThreeJsCameraData.FromCamera(mainCamera);
            }

            scene.scene = new ThreeJsSceneData
            {
                objectData = CreateSceneObject()
            };

            // Инициализируем базовую геометрию куба
            EnsureCubeGeometryExists();
            
            // Добавляем все геометрии и материалы в сцену
            scene.scene.geometries.AddRange(_geometries.Values);
            scene.scene.materials.AddRange(_materials.Values);

            return scene;
        }

        private void EnsureCubeGeometryExists()
        {
            string geoId = "cube-geometry";
            if (!_geometries.ContainsKey(geoId))
            {
                _geometries[geoId] = ThreeJsGeometry.CreateBoxGeometry(geoId);
            }
        }

        private ThreeJsObject CreateSceneObject()
        {
            var sceneObject = ThreeJsObject.CreateScene();

            Scene currentScene = SceneManager.GetActiveScene();
            GameObject[] rootObjects = currentScene.GetRootGameObjects();

            foreach (GameObject rootObject in rootObjects)
            {
                ProcessGameObject(rootObject.transform, sceneObject.children);
            }

            return sceneObject;
        }

        private void ProcessGameObject(Transform transform, List<ThreeJsObjectBase> parentChildren)
        {
            IUnityObjectConverter converter = _converters.FirstOrDefault(c => c.CanConvert(transform));
            
            if (converter == null)
                return;

            ThreeJsObjectBase threeJsObject = converter.Convert(transform);

            if ((threeJsObject.type == "Group" || threeJsObject.type == "Scene") && transform.childCount > 0)
            {
                foreach (Transform child in transform)
                {
                    ProcessGameObject(child, threeJsObject.children);
                }
            }

            if (threeJsObject.children.Count > 0 || transform.parent == null || threeJsObject.type != "Group")
            {
                parentChildren.Add(threeJsObject);
            }
        }
    }
}