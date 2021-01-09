/* Copyrighted by Oguzcan Adabuk Chicago, IL, 2018*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour {

    public Texture2D heightmap;
    public Material marsMaterial;
    public int edge = 256;
    public float heightScale = 20f;
    public float widthScale = 10f;
    
    Color black = new Color(0, 0, 0);

    void Start () {
        for(int i = 0; i < heightmap.height; i += edge)
        {
            for (int j = 0; j < heightmap.width; j += edge)
            {
                BuildMapPart(i, j, edge);
            }
        }
    }

    public void BuildMapPart(int x, int y, int e)
    {
        GameObject mapPart = new GameObject();
        mapPart.AddComponent<MeshFilter>();
        mapPart.AddComponent<MeshRenderer>();
        mapPart.name = "Map_Part_" + x + "_" + y;

        Color[] mapPix = heightmap.GetPixels(x, y, e, e);

        List<Vector3> vertexPositions = new List<Vector3>();
        List<Vector2> uvPositions = new List<Vector2>();
        List<Vector3> normalPositions = new List<Vector3>();
        List<int> triangles = new List<int>();

        int mapSize = e * e;
        int n = 0;

        for (int i = 0; i < e; i++)
        {
            for (int j = 0; j < e; j++)
            {
                Vector3 pos = new Vector3(
                    (y + i ) * widthScale,
                    GetHeight(mapPix[n]) * heightScale,
                    (x + j) * widthScale
                );
                vertexPositions.Add(pos);
                uvPositions.Add(new Vector2(i, j));
                normalPositions.Add(new Vector3(5, 0, 0));
                n++;
            }
        }

        for (int k = 0; k + e + 1 < mapSize; k++)
        {
            if((k + 1) % e != 0)
            {
                triangles.Add(k);
                triangles.Add(k + 1);
                triangles.Add(k + 1 + e);

                triangles.Add(k + 1 + e);
                triangles.Add(k + e);
                triangles.Add(k);
            }
        }

        Mesh mesh = new Mesh();
        MeshFilter mf = mapPart.GetComponent<MeshFilter>();
        MeshRenderer mr = mapPart.GetComponent<MeshRenderer>();

        mesh.vertices = vertexPositions.ToArray();
        mesh.uv = uvPositions.ToArray();
        mesh.normals = normalPositions.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        mf.mesh = mesh;
        mr.material = marsMaterial;
    }
    
    public float GetHeight(Color c)
    {
        float distance = 
            Mathf.Abs(c.r - black.r) +
            Mathf.Abs(c.g - black.g) +
            Mathf.Abs(c.b - black.b);
        return distance;
    }  
}

