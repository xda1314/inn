using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeGridData : System.ICloneable
{
    public MergeGrid GridGO { get; private set; }

    public Vector2Int GridPos { get; private set; }


    public MergeGridData(Vector2Int gridPos)
    {
        this.GridPos = gridPos;
    }

    public void ChangeGridGO(MergeGrid gridGO)
    {
        GridGO = gridGO;
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}
