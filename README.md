# UnityExportJS


## Description
This tool allows you to export Unity scenes to HTML5 format using Three.js. The exporter preserves object hierarchy, position, rotation, and scale of cubes, as well as camera settings.

## How to Use
1. Import the package into your Unity project
2. In the Unity editor, select "Tools > Export to HTML5" from the menu
3. Choose a path to save the HTML file
4. After export, you can open the HTML file in any modern browser

## Architecture

### Converter System
The project uses an extensible converter architecture to transform various Unity object types into Three.js format:

- `IUnityObjectConverter` - interface for all converters
- `CubeConverter` - converts cubes
- `PerspectiveCameraConverter` - converts cameras

This architecture makes it easy to add support for new object types by implementing new converters.

### JSON Format Specification
The exporter creates a JSON structure that represents the scene in a format compatible with Three.js:

```json
{
  "scene": {
    "id": "scene",
    "name": "Scene",
    "type": "Scene",
    "position": [0, 0, 0],
    "rotation": [0, 0, 0],
    "scale": [1, 1, 1],
    "children": [
      {
        "id": "123456",
        "name": "Cube",
        "type": "Box",
        "position": [x, y, z],
        "rotation": [x, y, z],
        "scale": [x, y, z],
        "children": []
      }
    ]
  },
  "camera": {
    "id": "789012",
    "name": "Main Camera",
    "type": "PerspectiveCamera",
    "position": [x, y, z],
    "rotation": [x, y, z],
    "scale": [1, 1, 1],
    "fov": 60,
    "near": 0.3,
    "far": 1000,
    "aspect": 1.777
  }
}
```

### HTML Template
The exporter uses an HTML template (`scene-viewer.html`) that contains the necessary JavaScript code to display the exported scene using Three.js. The template is loaded from the Resources/ThreeJsExporter folder.

## Limitations
The current version of the exporter has the following limitations:
- Only cubes and cameras are supported
- Materials, textures, lighting, and other effects are not exported
- Animations are not supported

## Extending Functionality
To add support for new object types:
1. Create a new class implementing the `IUnityObjectConverter` interface
2. Implement the `CanConvert` and `Convert` methods
3. Add the new converter to the list in `ThreeJsSceneBuilder.cs`