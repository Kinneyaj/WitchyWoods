using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneRender : MonoBehaviour
{
    [SerializeField]
    private int segments = 50;
    [SerializeField]
    private float innerRadius = 0.8f;
    [SerializeField]
    private float outerRadius = 1f;

    private Mesh mesh;

    private void Awake() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateTorus();
    }

    private void CreateTorus() {
        Vector3[] vertices = new Vector3[segments * 2];
        int[] triangles = new int[segments * 6];
        Vector2[] uvs = new Vector2[segments * 2];

        float segmentDegrees = 360f / segments;
        for (int i = 0; i < segments; i++) {
            float rad = Mathf.Deg2Rad * (i * segmentDegrees);
            float cos = Mathf.Cos(rad);
            float sin = Mathf.Sin(rad);

            vertices[i * 2] = new Vector2(cos * innerRadius, sin * innerRadius);
            vertices[i * 2 + 1] = new Vector2(cos * outerRadius, sin * outerRadius);

            uvs[i * 2] = new Vector2((float)i / segments, 0);
            uvs[i * 2 + 1] = new Vector2((float)i / segments, 1);

            if (i < segments - 1) {
                triangles[i * 6] = i * 2;
                triangles[i * 6 + 1] = i * 2 + 1;
                triangles[i * 6 + 2] = (i + 1) * 2;

                triangles[i * 6 + 3] = i * 2 + 1;
                triangles[i * 6 + 4] = (i + 1) * 2 + 1;
                triangles[i * 6 + 5] = (i + 1) * 2;
            } else {
                // last segment
                triangles[i * 6] = i * 2;
                triangles[i * 6 + 1] = i * 2 + 1;
                triangles[i * 6 + 2] = 0;

                triangles[i * 6 + 3] = i * 2 + 1;
                triangles[i * 6 + 4] = 1;
                triangles[i * 6 + 5] = 0;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }
}
