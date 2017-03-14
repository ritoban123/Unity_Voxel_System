/* MeshData.cs  
 * (c) 2017 Ritoban Roy-Chowdhury. All rights reserved 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshData  
{
    public List<Vector3> Verts = new List<Vector3>();
    public List<int> Tris = new List<int>();
    public List<Vector2> Uvs = new List<Vector2>();

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = Verts.ToArray();
        mesh.triangles = Tris.ToArray();
        mesh.uv = Uvs.ToArray();
        mesh.RecalculateNormals();
        return mesh;
    }

    public void AddVertex(float x, float y, float z)
    {
        Verts.Add(new Vector3(x, y, z));
    }

    public void AddQuadTris()
    {
        List<int> tris = new List<int>();
        tris.AddRange(new int[] { Verts.Count - 4, Verts.Count - 3, Verts.Count - 2 });
        tris.AddRange(new int[] { Verts.Count - 4, Verts.Count - 2, Verts.Count - 1 });
        Tris.AddRange(tris);
    }
}
