using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMap : MonoBehaviour
{
    void Start()
    {
        byte[] mapData = File.ReadAllBytes(StaticData.MapPath);

        byte[] geometryHeader = {68, 65, 84, 65, 72, 77, 65, 80, 208, 7};
        byte[] geometryEnd = {68, 65, 84, 65, 84, 84, 89, 80, 210, 7};

        bool readingMode = false;

        int p = 0;

        List<byte> geometryData = new List<byte>();

        while (true)
        {
            if (!readingMode)
            {
                if (HeaderComparator(mapData, geometryHeader, p))
                {
                    readingMode = true;
                    
                    p += 27;
                }
            }

            else
            {
                if (HeaderComparator(mapData, geometryEnd, p))
                {
                    break;
                } 
                
                geometryData.Add(mapData[p]);
            }

            p++;
        }
        
        int dotCount = geometryData.Count;
        int dotSideCount = Convert.ToInt32(Math.Sqrt(dotCount));

        for (int i = 0; i < dotCount - dotSideCount - 1; i += StaticData.SkipFactor)
        {
            if ((i + 1) % dotSideCount != 0)
            {
                CreateTriangle(
                    new int[] {i % dotSideCount,                    geometryData[i],              i / dotSideCount},
                    new int[] {(i + 1) % dotSideCount,              geometryData[i+1],            i / dotSideCount},
                    new int[] {(i + dotSideCount) % dotSideCount,   geometryData[i+dotSideCount], i / dotSideCount + 1},
                    8f, true
                );

                CreateTriangle(
                    new int[] {(i + 1) % dotSideCount,                geometryData[i+1],              i / dotSideCount},
                    new int[] {(i + dotSideCount) % dotSideCount,     geometryData[i+dotSideCount],   i / dotSideCount + 1},
                    new int[] {(i + dotSideCount + 1) % dotSideCount, geometryData[i+dotSideCount+1], i / dotSideCount + 1},
                    8f, false
                );
            }
        }
    }

    private void CreateTriangle(int[] p1, int[] p2, int[] p3, float size, bool normalOrder)
    {
        GameObject triangle = new GameObject();

        triangle.AddComponent<MeshFilter>();
        triangle.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();

        mesh.vertices = new Vector3[]
        {
            new Vector3(p1[0], p1[1] / size, p1[2]),
            new Vector3(p2[0], p2[1] / size, p2[2]),
            new Vector3(p3[0], p3[1] / size, p3[2]),
        };

        string color;

        if (normalOrder)
        {
            mesh.triangles = new int[3] {2, 1, 0};

            color = "White";
        }

        else
        {
            mesh.triangles = new int[3] {0, 1, 2};

            color = "Green";
        }

        triangle.GetComponent<MeshFilter>().mesh = mesh;
        triangle.GetComponent<MeshRenderer>().material = Resources.Load($"Materials/{color}", typeof(Material)) as Material;
    }

    private bool HeaderComparator(byte[] mapData, byte[] headerData, int p)
    {
        for (int i = 0; i < headerData.Length; i++)
        {
            if (mapData[p+i] != headerData[i])
            {
                return false;
            }
        }

        return true;
    }
}
