/* Chunk.cs  
 * (c) 2017 Ritoban Roy-Chowdhury. All rights reserved 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    public const int CHUNK_SIZE = 12;

    protected Voxel[,,] Voxels;
    public MeshData MeshData;
    public WorldManager world;

    public Chunk(WorldManager world)
    {
        #region Assigning Member Fields
        this.world = world;
        this.MeshData = new MeshData();
        #endregion

        #region Voxel Array
        Voxels = new Voxel[CHUNK_SIZE, CHUNK_SIZE, CHUNK_SIZE];
        for (int x = 0; x < CHUNK_SIZE; x++)
        {
            for (int y = 0; y < CHUNK_SIZE; y++)
            {
                for (int z = 0; z < CHUNK_SIZE; z++)
                {
                    Voxels[x, y, z] = new Voxel(x, y, z, this);
                }
            }
        }
        #endregion
    }

    public MeshData GetMesh()
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

    #region Utility Methods
    public Voxel GetVoxel(int x, int y, int z)
    {
        if (InRange(x) && InRange(y) && InRange(z))
            return Voxels[x, y, z];
        return null;
    }

    protected bool InRange(int value)
    {
        return value >= 0 && value < CHUNK_SIZE;
    }
    #endregion
}
