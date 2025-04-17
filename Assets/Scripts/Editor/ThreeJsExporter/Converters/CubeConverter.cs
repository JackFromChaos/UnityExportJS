using System;
using System.Collections.Generic;
using UnityEngine;
using ThreeJsExporter.Models;

namespace ThreeJsExporter.Converters
{
    public class CubeConverter : IUnityObjectConverter
    {
        public bool CanConvert(Transform transform)
        {
            MeshFilter meshFilter = transform.GetComponent<MeshFilter>();
            if (meshFilter != null && meshFilter.sharedMesh != null)
            {
                string meshName = meshFilter.sharedMesh.name;
                if (meshName == "Cube" || meshName.StartsWith("Cube "))
                    return true;
            }
            
            BoxCollider boxCollider = transform.GetComponent<BoxCollider>();
            if (boxCollider != null)
            {
                Vector3 size = boxCollider.size;
                float epsilon = 0.1f;
                
                if (Mathf.Abs(size.x - size.y) < epsilon && 
                    Mathf.Abs(size.y - size.z) < epsilon &&
                    Mathf.Abs(size.x - size.z) < epsilon)
                {
                    return true;
                }
            }
            
            return false;
        }

        public ThreeJsObjectBase Convert(Transform transform)
        {
            return ThreeJsObject.CreateMesh(transform, "cube-geometry");
        }
    }
}