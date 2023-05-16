using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MapEditSystem
{
    public static MapEditSystem Instance { get; } = new MapEditSystem();

    public Vector2Int CurrentMapGridCount = new Vector2Int(7, 7);

    public Vector2Int EditGridRowColumn;
    public Dictionary<Vector2Int, MapEditGridData> EditGridDataDict = new Dictionary<Vector2Int, MapEditGridData>();
    public Dictionary<Vector2Int, MapEditGrid> EditGridDict = new Dictionary<Vector2Int, MapEditGrid>();

    public string configPath => Path.Combine("Assets", "TestConfigs", "DebugMap");
    public string fileFullPath;
    public string MapKey => Path.GetFileNameWithoutExtension(fileFullPath);

#if UNITY_EDITOR
    public void EnterEditMap(string fileFullPath, bool restart)
    {
        this.fileFullPath = fileFullPath;
        MergeLevelManager.Instance.Debug_AddMapData(MapDefinition.Debug_AddMapDefinition(fileFullPath), restart);
    }

#endif
}
