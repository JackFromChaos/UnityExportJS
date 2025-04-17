using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

namespace ThreeJsExporter
{
    public class ThreeJsExporter : EditorWindow
    {
        [MenuItem("Tools/Export to HTML5")]
        public static void ExportToHTML5()
        {
            string path = EditorUtility.SaveFilePanel(
                "Save HTML5 Scene",
                "",
                SceneManager.GetActiveScene().name + ".html",
                "html");

            if (string.IsNullOrEmpty(path))
                return;

            var sceneBuilder = new ThreeJsSceneBuilder();
            var scene = sceneBuilder.BuildScene();

            string json = JsonConvert.SerializeObject(scene, SimpleScene.GetSerializerSettings());
            
            TextAsset templateAsset = Resources.Load<TextAsset>("ThreeJsExporter/scene-viewer");
            
            if (templateAsset == null)
            {
                Debug.LogError("Template file not found in Resources/ThreeJsExporter/scene-viewer.html");
                return;
            }
            
            string template = templateAsset.text;
            string html = template.Replace("{{SCENE_DATA}}", json);
            
            using (StreamWriter writer = new StreamWriter(path, false, new UTF8Encoding(false)))
            {
                writer.Write(html);
            }
            
            Debug.Log($"HTML5 scene exported to: {path}");
            
            if (EditorUtility.DisplayDialog("Export Complete", 
                $"HTML5 scene has been exported to:\n{path}\n\nWould you like to open it now?", 
                "Open in Browser", "Close"))
            {
                Application.OpenURL("file://" + path);
            }
        }
    }
}