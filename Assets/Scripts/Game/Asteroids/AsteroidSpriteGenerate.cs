using System.Collections.Generic;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
public class AsteroidSpriteGenerate : MonoBehaviour
{
    public Material material;

    public void Generate()
    {
        var mesh = new Mesh();


        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        for (int i = 0; i < 6; i++)
        {
            var angle = i * (2 * Mathf.PI / i);
            var vertex = new Vector3();
            vertex.x = Mathf.Cos(angle);
            vertex.y = Mathf.Sin(angle);

            vertex *= Random.Range(0.5f, 2f);
            vertices.Add(vertex);
        }

        for (int index = 0; index < vertices.Count-3; index++)
        {
            var vector3 = vertices[index];
            triangles.Add(index);
            triangles.Add(index+1);
            triangles.Add(index+2);
        }

        triangles.Add(vertices.Count-2);
        triangles.Add(vertices.Count-1);
        triangles.Add(0);

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.Optimize();
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }

    void Start()
    {
        Generate();
    }

}
