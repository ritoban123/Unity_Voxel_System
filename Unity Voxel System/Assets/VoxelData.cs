using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Voxel Data", menuName ="Voxel/Voxel Data", order = 0)]
public class VoxelData : ScriptableObject
{
    public bool IsSolidUp;
    public bool IsSolidDown;
    public bool IsSolidNorth;
    public bool IsSolidSouth;
    public bool IsSolidEast;
    public bool IsSolidWest;

}
