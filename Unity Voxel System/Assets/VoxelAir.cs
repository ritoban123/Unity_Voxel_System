using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelAir : Voxel
{
    public VoxelAir(int x, int y, int z, Chunk chunk) : base(x, y, z, chunk)
    {

    }


    public override bool IsSolid(Direction dir)
    {
        return false;
    }

    public override void CalculateMeshData(MeshData mesh)
    {
        // HACK: We cannot call the default CalculateMeshData function on the edge voxels, so we are making them air.
        //  Once we change the get block function to get correct block in the entire world, we should need this function
        return;
    }
}
