using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using ThreeJsExporter.Models;

namespace ThreeJsExporter
{
    public class ThreeJsExporter : EditorWindow
    {
        [MenuItem("Tools/Export to HTML5/Export to Three.js JSON")]
        public static void ExportToThreeJs()
        {
            string path = EditorUtility.SaveFilePanel(
                "Save Three.js Scene",
                "",
                "scene.json",
                "json");

            if (string.IsNullOrEmpty(path))
                return;

            var sceneBuilder = new ThreeJsSceneBuilder();
            var scene = sceneBuilder.BuildScene();

            string json = JsonConvert.SerializeObject(scene, ThreeJsScene.GetSerializerSettings());

            using (StreamWriter writer = new StreamWriter(path, false, new UTF8Encoding(false)))
            {
                writer.Write(json);
            }

            Debug.Log($"Scene exported to: {path}");
        }
        
        [MenuItem("Tools/Export to HTML5/Export to HTML5")]
        public static void ExportToHTML5()
        {
            string path = EditorUtility.SaveFilePanel(
                "Save HTML5 Scene",
                "",
                "scene.html",
                "html");

            if (string.IsNullOrEmpty(path))
                return;

            var sceneBuilder = new ThreeJsSceneBuilder();
            var scene = sceneBuilder.BuildScene();

            string json = JsonConvert.SerializeObject(scene, ThreeJsScene.GetSerializerSettings());
            string templatePath = Path.Combine(Application.dataPath, "Scripts/Editor/ThreeJsExporter/Templates/viewer-template.html");
            
            if (!File.Exists(templatePath))
            {
                Debug.LogError($"Template file not found at path: {templatePath}");
                return;
            }
            
            string template = File.ReadAllText(templatePath);
            string html = template.Replace("{{SCENE_DATA}}", json);
            
            using (StreamWriter writer = new StreamWriter(path, false, new UTF8Encoding(false)))
            {
                writer.Write(html);
            }
            
            Debug.Log($"HTML5 scene exported to: {path}");
            
            // Открываем файл в браузере
            if (EditorUtility.DisplayDialog("Export Complete", 
                $"HTML5 scene has been exported to:\n{path}\n\nWould you like to open it now?", 
                "Open in Browser", "Close"))
            {
                Application.OpenURL("file://" + path);
            }
        }
    }
}