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

    public VoxelData Data { get; protected set; }

    public int X;
    public int Y;
    public int Z;
    public Chunk Chunk;

    public Voxel(int x, int y, int z, Chunk chunk, VoxelData data)
    {
        X = x;
        Y = y;
        Z = z;
        Chunk = chunk;
        Data = data;
    }


    public virtual void CalculateMeshData(MeshData mesh)
    {
        if (IsSolid(Direction.North) && Chunk.GetVoxel(X, Y, Z + 1).IsSolid(Direction.South) == false)
            CalculateFaceData(mesh, Direction.North);
        if (IsSolid(Direction.South) && Chunk.GetVoxel(X, Y, Z - 1).IsSolid(Direction.North) == false)
            CalculateFaceData(mesh, Direction.South);
        if (IsSolid(Direction.East) && Chunk.GetVoxel(X + 1, Y, Z).IsSolid(Direction.West) == false)
            CalculateFaceData(mesh, Direction.East);
        if (IsSolid(Direction.West) && Chunk.GetVoxel(X - 1, Y, Z).IsSolid(Direction.East) == false)
            CalculateFaceData(mesh, Direction.West);
        if (IsSolid(Direction.Up) && Chunk.GetVoxel(X, Y + 1, Z).IsSolid(Direction.Down) == false)
            CalculateFaceData(mesh, Direction.Up);
        if (IsSolid(Direction.Down) && Chunk.GetVoxel(X, Y - 1, Z).IsSolid(Direction.Up) == false)
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
            default:
                return;
        }
        mesh.AddUV(new Vector2(0, 0));
        mesh.AddUV(new Vector2(0, 1));
        mesh.AddUV(new Vector2(1, 1));
        mesh.AddUV(new Vector2(1, 0));

    }

    // FIXME: Use a separate voxel data class for input!
    /// <summary>
    /// Is this block (type) solid from a given direction
    /// </summary>
    /// <param name="dir">The Directio</param>
    /// <returns>True is is solid</returns>
    public virtual bool IsSolid(Direction dir)
    {
        switch (dir)
        {
            case Direction.North:
                return Data.IsSolidNorth;
            case Direction.South:
                return Data.IsSolidSouth;
            case Direction.Up:
                return Data.IsSolidUp;
            case Direction.Down:
                return Data.IsSolidDown;
            case Direction.East:
                return Data.IsSolidEast;
            case Direction.West:
                return Data.IsSolidWest;
        }
        return false;
    }
}
