/* Voxel.cs  
 * (c) 2017 Ritoban Roy-Chowdhury. All rights reserved 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voxel
{
    /// <summary>
    /// East/West is X, Up/Down is Y, North/South is Z
    /// </summary>
    public enum Direction { Up, Down, North, South, East, West }

    // TODO: What about different block types?
    public int X;
    public int Y;
    public int Z;
    public Chunk Chunk;

    public Voxel(int x, int y, int z, Chunk chunk)
    {
        X = x;
        Y = y;
        Z = z;
        Chunk = chunk;
    }


    public virtual void CalculateMeshData(MeshData mesh)
    {
        // BUG: This will give us a null reference exception if this voxel is on the edge of the chunk. This will be fixed when we have multiple chunks and we can compare to the voxels in the other chunks
        if (Chunk.GetVoxel(X, Y, Z + 1).IsSolid(Direction.South) == false)
            CalculateFaceData(mesh, Direction.North);
        if (Chunk.GetVoxel(X, Y, Z - 1).IsSolid(Direction.North) == false) 
            CalculateFaceData(mesh, Direction.South);
        if (Chunk.GetVoxel(X + 1, Y, Z).IsSolid(Direction.West) == false)
            CalculateFaceData(mesh, Direction.East);
        if (Chunk.GetVoxel(X - 1, Y, Z).IsSolid(Direction.East) == false) 
            CalculateFaceData(mesh, Direction.West);
        if (Chunk.GetVoxel(X, Y + 1, Z).IsSolid(Direction.Down) == false) 
            CalculateFaceData(mesh, Direction.Up);
        if (Chunk.GetVoxel(X, Y - 1, Z).IsSolid(Direction.Up) == false)
            CalculateFaceData(mesh, Direction.Down);


    }

    protected virtual void CalculateFaceData(MeshData mesh, Direction dir)
    {
        switch (dir)
        {
            case Direction.North:
                // FIXME: What about voxels that are larger than 1 Unity unit?
                // Top-left
                mesh.AddVertex(X - 0.5f, Y - 0.5f, Z + 0.5f);
                // Top-right
                mesh.AddVertex(X + 0.5f, Y - 0.5f, Z + 0.5f);
                // Bottom Right
                mesh.AddVertex(X + 0.5f, Y + 0.5f, Z + 0.5f);
                // Bottom Left
                mesh.AddVertex(X - 0.5f, Y + 0.5f, Z + 0.5f);
                mesh.AddQuadTris();
                break;
            case Direction.South:
                mesh.AddVertex(X + 0.5f, Y - 0.5f, Z - 0.5f);
                mesh.AddVertex(X - 0.5f, Y - 0.5f, Z - 0.5f);
                mesh.AddVertex(X - 0.5f, Y + 0.5f, Z - 0.5f);
                mesh.AddVertex(X + 0.5f, Y + 0.5f, Z - 0.5f);
                mesh.AddQuadTris();
                break;
            case Direction.Up:
                mesh.AddVertex(X - 0.5f, Y + 0.5f, Z - 0.5f);
                mesh.AddVertex(X - 0.5f, Y + 0.5f, Z + 0.5f);
                mesh.AddVertex(X + 0.5f, Y + 0.5f, Z + 0.5f);
                mesh.AddVertex(X + 0.5f, Y + 0.5f, Z - 0.5f);
                mesh.AddQuadTris();
                break;
            case Direction.Down:
                mesh.AddVertex(X + 0.5f, Y - 0.5f, Z - 0.5f);
                mesh.AddVertex(X + 0.5f, Y - 0.5f, Z + 0.5f);
                mesh.AddVertex(X - 0.5f, Y - 0.5f, Z + 0.5f);
                mesh.AddVertex(X - 0.5f, Y - 0.5f, Z - 0.5f);
                mesh.AddQuadTris();
                break;
            case Direction.East:
                mesh.AddVertex(X + 0.5f, Y + 0.5f, Z - 0.5f);
                mesh.AddVertex(X + 0.5f, Y + 0.5f, Z + 0.5f);
                mesh.AddVertex(X + 0.5f, Y - 0.5f, Z + 0.5f);
                mesh.AddVertex(X + 0.5f, Y - 0.5f, Z - 0.5f);
                mesh.AddQuadTris();
                break;
            case Direction.West:
                mesh.AddVertex(X - 0.5f, Y - 0.5f, Z - 0.5f);
                mesh.AddVertex(X - 0.5f, Y - 0.5f, Z + 0.5f);
                mesh.AddVertex(X - 0.5f, Y + 0.5f, Z + 0.5f);
                mesh.AddVertex(X - 0.5f, Y + 0.5f, Z - 0.5f);
                mesh.AddQuadTris();
                break;


        }
    }
    /// <summary>
    /// Is this block (type) solid from a given direction
    /// </summary>
    /// <param name="dir">The Directio</param>
    /// <returns>True is is solid</returns>
    public virtual bool IsSolid(Direction dir)
    {
        return true;
    }
}
