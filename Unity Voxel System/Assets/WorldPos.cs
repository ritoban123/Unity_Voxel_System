using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WorldPos 
{
    public int X;
    public int Y;
    public int Z;

    public WorldPos(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public override bool Equals(object obj)
    {
        // If the objec is not a world pos (cannot be cast to one), then its not equal
        if((obj is WorldPos) == false)
        {
            return false;
        }
        // Actually cast obj to a world pos
        WorldPos pos = (WorldPos)obj;
        if (pos.X != X || pos.Y != Y || pos.Z != Z)
            return false;
        else
            return true;
    }
}
