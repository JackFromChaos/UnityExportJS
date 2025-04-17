using UnityEngine;
using ThreeJsExporter.Models;

namespace ThreeJsExporter
{
    public static class TransformUtils
    {
        public static float[] ConvertTransformToThreeJsMatrix(Transform transform)
        {
            bool isRoot = transform.parent == null;
            Matrix4x4 unityMatrix;
            if (isRoot)
            {
                unityMatrix = Matrix4x4.TRS(
                    transform.position,
                    transform.rotation,
                    transform.lossyScale
                );
            }
            else
            {
                unityMatrix = Matrix4x4.TRS(
                    transform.localPosition,
                    transform.localRotation,
                    transform.localScale
                );
            }

            Matrix4x4 convertMatrix = Matrix4x4.identity;
            convertMatrix[2, 2] = -1;
            Matrix4x4 threeJsMatrix = convertMatrix * unityMatrix * convertMatrix;
            return new float[16] {
                threeJsMatrix[0, 0], threeJsMatrix[1, 0], threeJsMatrix[2, 0], threeJsMatrix[3, 0],
                threeJsMatrix[0, 1], threeJsMatrix[1, 1], threeJsMatrix[2, 1], threeJsMatrix[3, 1],
                threeJsMatrix[0, 2], threeJsMatrix[1, 2], threeJsMatrix[2, 2], threeJsMatrix[3, 2],
                threeJsMatrix[0, 3], threeJsMatrix[1, 3], threeJsMatrix[2, 3], threeJsMatrix[3, 3]
            };
        }

        public static void SetThreeJsTransform(Transform transform, ThreeJsObjectBase threeJsObject)
        {
            bool isRoot = transform.parent == null;
            if (isRoot)
            {
                Vector3 position = transform.position;
                threeJsObject.position = new float[] { position.x, position.y, -position.z };
                Vector3 eulerAngles = transform.eulerAngles;
                float xRot = -eulerAngles.x;
                float yRot = -eulerAngles.y;
                float zRot = eulerAngles.z;
                threeJsObject.rotation = new float[] {
                    xRot * Mathf.Deg2Rad,
                    yRot * Mathf.Deg2Rad,
                    zRot * Mathf.Deg2Rad
                };

                Vector3 scale = transform.lossyScale;
                threeJsObject.scale = new float[] { scale.x, scale.y, scale.z };
            }
            else
            {
                Vector3 localPosition = transform.localPosition;
                threeJsObject.position = new float[] { localPosition.x, localPosition.y, -localPosition.z };
                Vector3 localEulerAngles = transform.localEulerAngles;
                float xRot = -localEulerAngles.x;
                float yRot = -localEulerAngles.y;
                float zRot = localEulerAngles.z;
                threeJsObject.rotation = new float[] {
                    xRot * Mathf.Deg2Rad,
                    yRot * Mathf.Deg2Rad,
                    zRot * Mathf.Deg2Rad
                };
                Vector3 localScale = transform.localScale;
                threeJsObject.scale = new float[] { localScale.x, localScale.y, localScale.z };
            }

            threeJsObject.rotationOrder = "XYZ";
        }

        public static void GetThreeJsTransform(Transform transform, out float[] position, out float[] rotation, out float[] scale)
        {
            Vector3 pos = transform.position;
            position = new float[] { pos.x, pos.y, -pos.z };
            Vector3 eulerAngles = transform.eulerAngles;
            float xRot = -eulerAngles.x;
            float yRot = -eulerAngles.y;
            float zRot = eulerAngles.z;
            rotation = new float[] {
                xRot * Mathf.Deg2Rad,
                yRot * Mathf.Deg2Rad,
                zRot * Mathf.Deg2Rad
            };
            Vector3 scl = transform.lossyScale;
            scale = new float[] { scl.x, scl.y, scl.z };
        }
    }
}