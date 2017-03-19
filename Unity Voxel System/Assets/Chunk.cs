/* Chunk.cs  
 * (c) 2017 Ritoban Roy-Chowdhury. All rights reserved 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    public const int CHUNK_SIZE = 128;

    public MeshData MeshData;
    public WorldManager World;
    public WorldPos WorldPos;
    // FIXME: I do not like having the data layer store information about the unity layer, but it would make storing the world much more complex
    //          I am storing a separate WorldPos Chunk Dictionary (even though the chunks already have a reference to their worldpos), 
    //         and making a 3-way dictionary would be hard
    public GameObject gameObject;

    public Voxel[,,] Voxels;
    protected MeshFilter meshFilter;
    protected MeshCollider meshCollider;
    

    public Chunk(WorldManager world, GameObject gameObject, WorldPos WorldPos)
    {
        #region Assigning Member Fields
        this.World = world;
        this.MeshData = new MeshData();
        this.gameObject = gameObject;
        this.meshFilter = gameObject.GetComponent<MeshFilter>();
        this.meshCollider = gameObject.GetComponent<MeshCollider>();
        this.WorldPos = WorldPos;
        #endregion

        #region Voxel Array
        Voxels = new Voxel[CHUNK_SIZE, CHUNK_SIZE, CHUNK_SIZE];
        #endregion
    }

    public MeshData CalculateMeshData()
    {
        // Go through each block and make it generate its mesh. Pass it the MeshData for this chunk to add to
        for (int x = 0; x < CHUNK_SIZE; x++)
        {
            for (int y = 0; y < CHUNK_SIZE; y++)
            {
                for (int z = 0; z < CHUNK_SIZE; z++)
                {
                    Voxel voxel = GetVoxel(x, y, z);
                    voxel.CalculateMeshData(MeshData);
                }
            }
        }
        return MeshData;
    }


    public void RenderChunk()
    {
        Mesh mesh = MeshData.CreateMesh();
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

    #region Utility Methods
    /// <summary>
    /// Returns a voxel from the voxels array. If not in this chunk, asks the world for the correct one
    /// </summary>
    /// <param name="x">The x coordinate in chunk space</param>
    /// <param name="y">The y coordinate in chunk space</param>
    /// <param name="z">The z coordinate in chunk space</param>
    /// <returns></returns>
    public Voxel GetVoxel(int x, int y, int z)
    {
        if (InRange(x) && InRange(y) && InRange(z))
            return Voxels[x, y, z];
        // We need to overload the addition operator!
        return World.GetVoxel(WorldPos.X + x, WorldPos.Y + y, WorldPos.Z + z);
    }

    /// <summary>
    /// Works similarly to the Get voxel function. Sets the value in the appropriate chunks voxels array
    /// </summary>
    /// <param name="x">The x coordinate in chunk space</param>
    /// <param name="y">The y coordinate in chunk space</param>
    /// <param name="z">The z coordinate in chunk space</param>
    /// <param name="voxel">The Voxel to set it to. Should be a new Voxel</param>
    public void SetVoxel(int x, int y, int z, Voxel voxel)
    {
        if (InRange(x) && InRange(y) && InRange(z))
        {
            Voxels[x, y, z] = voxel;
            return;
        }
        World.SetVoxel(x + WorldPos.X, y + WorldPos.Y, z + WorldPos.Z, voxel);
    }

    protected bool InRange(int value)
    {
        return value >= 0 && value < CHUNK_SIZE;
    }
    #endregion
}
