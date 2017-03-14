/* WorldManager.cs  
 * (c) 2017 Ritoban Roy-Chowdhury. All rights reserved 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour 
{
    public GameObject ChunkPrefab;

    Chunk chunk;
    GameObject chunkObject;


	private void Start () 
	{
        chunk = new Chunk(this);
        chunkObject = Instantiate(ChunkPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        chunk.GetMesh(); 
        RenderChunks();

	}
    

    protected void RenderChunks()
    {
        MeshFilter mf = chunkObject.GetComponent <MeshFilter > ();
        mf.mesh = chunk.MeshData.CreateMesh();
    }
}
