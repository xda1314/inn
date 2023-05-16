using Ivy;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIPanel_TestMap : UIMergeBase
{
    public Image img_mapBg;

    [Header("map")]
    public GameObject mapGroup;
    public Button btn_ChooseMap;
    public Text txt_ChooseMap;
    public Button btn_RestartCurrentMap;
    public Button btn_EditCurrentMap;
    public Button btn_CreateNewMap;
    [Header("set grid")]
    public GameObject mapSetGridGroup;
    public InputField inputField_GridX;
    public InputField inputField_GridY;
    public Button btn_createMap;
    [Header("show edit map")]
    public RectTransform itemShowCenter;
    public GameObject itemShowGroup;
    public Button btn_CloseShowEditMap;
    public Button btn_SaveShowEditMap;
    public GameObject mapEditGridTemplete;
    public GridLayoutGroup mapEditGrid;
    [Header("set item")]
    public GameObject SetMapItemGroup;
    public InputField item_prefabName;
    public Toggle item_activeGrid;
    public Text txt_activeGrid;
    public Toggle item_lock;
    public Toggle item_InBox;
    public Button btn_ResetGridData;
    public Button btn_ConfirmData;

#if UNITY_EDITOR

    public override void OnInitUI()
    {
        base.OnInitUI();
        btn_ChooseMap.onClick.AddListener(() =>
        {
            string path = UnityEditor.EditorUtility.OpenFilePanel("选择地图", MapEditSystem.Instance.configPath, "json");
            MapEditSystem.Instance.EnterEditMap(path, false);
            RefreshMap();
        });

        btn_RestartCurrentMap.onClick.AddListener(() =>
        {
            if (string.IsNullOrEmpty(MapEditSystem.Instance.fileFullPath))
                return;
            MapEditSystem.Instance.EnterEditMap(MapEditSystem.Instance.fileFullPath, true);
            RefreshMap();
        });
        btn_EditCurrentMap.onClick.AddListener(() =>
        {
            if (string.IsNullOrEmpty(MapEditSystem.Instance.fileFullPath))
                return;
            MapEditSystem.Instance.EditGridDataDict.Clear();
            foreach (var item in MergeLevelManager.Instance.CurrentMapData.gridDataDict)
            {
                MapEditGridData editGridData = new MapEditGridData();
                editGridData.gridID = item.Key;
                editGridData.active = true;
                if (MergeLevelManager.Instance.CurrentMapData.itemDataDict.TryGetValue(item.Key, out var itemData))
                {
                    editGridData.prefabName = itemData.PrefabName;
                    editGridData.locked = itemData.IsLocked;
                    editGridData.inBox = itemData.IsInBox;
                }
                MapEditSystem.Instance.EditGridDataDict.Add(editGridData.gridID, editGridData);
            }
            int x = 0; int y = 0;
            foreach (var item in MergeLevelManager.Instance.CurrentMapData.gridDataDict)
            {
                if (item.Key.x > x)
                    x = item.Key.x;
                if (item.Key.y > y)
                    y = item.Key.y;
            }
            MapEditSystem.Instance.EditGridRowColumn = new Vector2Int(x, y);
            RefreshShowEditMapUI(true);
        });
        btn_CreateNewMap.onClick.AddListener(() =>
        {
            mapSetGridGroup.gameObject.SetActive(true);
        });
        btn_createMap.onClick.AddListener(() =>
        {
            if (string.IsNullOrEmpty(inputField_GridX.text)
                || string.IsNullOrEmpty(inputField_GridY.text))
                return;
            mapSetGridGroup.gameObject.SetActive(false);
            MapEditSystem.Instance.EditGridRowColumn = new Vector2Int(int.Parse(inputField_GridX.text), int.Parse(inputField_GridY.text));
            MapEditSystem.Instance.EditGridDataDict.Clear();
            RefreshShowEditMapUI(true, true);
        });
        btn_CloseShowEditMap.onClick.AddListener(() =>
        {
            RefreshShowEditMapUI(false);
        });
        btn_SaveShowEditMap.onClick.AddListener(() =>
        {
            string path = SaveEditMap();
            if (string.IsNullOrEmpty(path))
                return;
            RefreshShowEditMapUI(false);

            MapEditSystem.Instance.EnterEditMap(path, true);
            RefreshMap();
        });

    }


    public void RefreshMap()
    {
        try
        {
            Core_RefreshMap();
        }
        catch
        {
            try
            {
                MapEditSystem.Instance.EnterEditMap(MapEditSystem.Instance.fileFullPath, true);
                Core_RefreshMap();
            }
            catch (System.Exception e2)
            {
                LogUtils.LogError("mergeController.InitMapInfo error:" + e2);
            }
        }
    }

    private void Core_RefreshMap()
    {
        if (MergeLevelManager.Instance.CurrentMapData != null)
            mergeController.CleanMap();
        if (string.IsNullOrEmpty(MapEditSystem.Instance.fileFullPath))
        {
            txt_ChooseMap.text = "未选择地图";
            txt_ChooseMap.color = Color.red;
            return;
        }
        else
        {
            txt_ChooseMap.color = Color.black;
            txt_ChooseMap.text = "当前地图：\n" + Path.GetFileName(MapEditSystem.Instance.fileFullPath);
        }

        if (!MergeLevelManager.Instance.ChangeMergeLevelByDungeonType(MergeLevelType.DebugMap_FreeGrid))
            return;

        int x = 0; int y = 0;
        foreach (var item in MergeLevelManager.Instance.CurrentMapData.gridDataDict)
        {
            if (item.Key.x > x)
                x = item.Key.x;
            if (item.Key.y > y)
                y = item.Key.y;
        }
        MapEditSystem.Instance.CurrentMapGridCount = new Vector2Int(x, y);

        if (DebugSetting.CanUseDebugMap(out var debugMap)
            && debugMap.Debug_EnterDebugMap
            && debugMap.Debug_MapBgSprite != null)
        {
            img_mapBg.sprite = debugMap.Debug_MapBgSprite;
        }
        if (img_mapBg.TryGetComponent<RectTransform>(out var mapRect))
            mapRect.sizeDelta = new Vector2(150 * MapEditSystem.Instance.CurrentMapGridCount.x, 150 * MapEditSystem.Instance.CurrentMapGridCount.y);

        mergeController.InitMapInfo(this, null, null, null);
    }


    /// <summary>
    /// 展示所有格子物品界面
    /// </summary>
    /// <param name="show"></param>
    public void RefreshShowEditMapUI(bool show, bool createNew = false)
    {
        itemShowGroup.gameObject.SetActive(show);
        if (!show)
            return;

        while (mapEditGrid.transform.childCount > 0)
        {
            DestroyImmediate(mapEditGrid.transform.GetChild(0).gameObject);
        }
        MapEditSystem.Instance.EditGridDict.Clear();

        mapEditGrid.constraintCount = MapEditSystem.Instance.EditGridRowColumn.x;
        for (int y = 0; y < MapEditSystem.Instance.EditGridRowColumn.y; y++)
        {
            for (int x = 0; x < MapEditSystem.Instance.EditGridRowColumn.x; x++)
            {
                if (MapEditSystem.Instance.EditGridDataDict.TryGetValue(new Vector2Int(x + 1, y + 1), out var gridData))
                {
                }
                else
                {
                    gridData = new MapEditGridData();
                    gridData.gridID = new Vector2Int(x + 1, y + 1);
                    gridData.active = createNew;
                    MapEditSystem.Instance.EditGridDataDict.Add(gridData.gridID, gridData);
                }
                GameObject gO = Instantiate(mapEditGridTemplete, mapEditGrid.transform);
                gO.SetActive(true);
                var grid = gO.GetComponent<MapEditGrid>();
                grid.RefreshPrefab(gridData, () =>
                {
                    SetMapItemGroup.SetActive(true);
                    RefreshSetItemDataUI(gridData);
                });
                MapEditSystem.Instance.EditGridDict.Add(gridData.gridID, grid);
            }
        }

        itemShowCenter.sizeDelta = new Vector2((150 + 5) * MapEditSystem.Instance.EditGridRowColumn.x, (150 + 5) * MapEditSystem.Instance.EditGridRowColumn.y);
    }


    /// <summary>
    /// 编辑格子属性界面
    /// </summary>
    /// <param name="mapEditGridData"></param>
    public void RefreshSetItemDataUI(MapEditGridData mapEditGridData)
    {
        item_prefabName.text = mapEditGridData.prefabName ?? string.Empty;
        item_lock.isOn = mapEditGridData.locked;
        item_InBox.isOn = mapEditGridData.inBox;
        item_activeGrid.isOn = mapEditGridData.active;
        txt_activeGrid.text = $"启用格子 ({mapEditGridData.gridID.x},{mapEditGridData.gridID.y})";

        btn_ConfirmData.onClick.RemoveAllListeners();
        btn_ConfirmData.onClick.AddListener(() =>
        {
            mapEditGridData.active = item_activeGrid.isOn;
            if (item_activeGrid)
            {
                if (!string.IsNullOrEmpty(item_prefabName.text) && !AssetSystem.Instance.ContainsKey(item_prefabName.text))
                {
                    item_prefabName.text = "预制体不存在！";
                    return;
                }
                mapEditGridData.prefabName = item_prefabName.text ?? string.Empty;
                mapEditGridData.locked = string.IsNullOrEmpty(mapEditGridData.prefabName) ? false : item_lock.isOn;
                mapEditGridData.inBox = string.IsNullOrEmpty(mapEditGridData.prefabName) ? false : item_InBox.isOn;
            }
            else
            {
                MergeLevelManager.Instance.CurrentMapData.gridDataDict.Remove(mapEditGridData.gridID);
                MergeLevelManager.Instance.CurrentMapData.itemDataDict.Remove(mapEditGridData.gridID);
            }

            SetMapItemGroup.SetActive(false);
            RefreshShowEditMapUI(true);
        });
        btn_ResetGridData.onClick.RemoveAllListeners();
        btn_ResetGridData.onClick.AddListener(() =>
        {
            item_prefabName.text = "";
            item_lock.isOn = false;
            item_InBox.isOn = false;
        });

    }

    public string SaveEditMap()
    {
        Dictionary<string, Dictionary<string, string>> jsonDict = new Dictionary<string, Dictionary<string, string>>();
        foreach (var item in MapEditSystem.Instance.EditGridDataDict)
        {
            if (!item.Value.active)
                continue;
            string id = (item.Value.gridID.x + (item.Value.gridID.y - 1) * MapEditSystem.Instance.EditGridRowColumn.x).ToString();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("Id", id);
            dict.Add("PositionX", item.Value.gridID.x.ToString());
            dict.Add("PositionY", item.Value.gridID.y.ToString());
            dict.Add("Prefab", item.Value.prefabName ?? "");
            dict.Add("Lock", !string.IsNullOrEmpty(item.Value.prefabName) && item.Value.locked ? "x" : "");
            dict.Add("InBox", !string.IsNullOrEmpty(item.Value.prefabName) && item.Value.inBox ? "x" : "");
            jsonDict.Add(id, dict);
        }

        string path = UnityEditor.EditorUtility.SaveFilePanel("保存配表", Path.Combine(Application.dataPath, "TestConfigs", "DebugMap"), "map_debug_new", "json");
        if (string.IsNullOrEmpty(path))
            return string.Empty;

        Dictionary<string, Dictionary<string, Dictionary<string, string>>> jsonDict22 = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
        string fileName = Path.GetFileNameWithoutExtension(path);
        jsonDict22.Add(fileName, jsonDict);
        File.WriteAllText(path, JsonConvert.SerializeObject(jsonDict22));
        UnityEditor.AssetDatabase.Refresh();
        return path;
    }

    public override IEnumerator OnShowUI()
    {
        mapSetGridGroup.gameObject.SetActive(false);
        itemShowGroup.gameObject.SetActive(false);
        SetMapItemGroup.gameObject.SetActive(false);
        MapEditSystem.Instance.EnterEditMap("", false);
        RefreshMap();
        yield return base.OnShowUI();
    }

    public override IEnumerator OnHideUI()
    {
        yield return base.OnHideUI();
        if (MergeLevelManager.Instance.CurrentMapData != null)
            mergeController.CleanMap();
    }

#endif


    public override void RefreshSelectItemInfo()
    {
    }
    public override void FixSellItem()
    {
    }
    public override void RefreshRewardBox()
    {
    }
    public override void RefreshStoreEffect(bool show)
    {
    }
    public override void RefreshBottomStore(int index = -1)
    {
    }
    public override void ShowNewDiscoveryView(string prefabName)
    {
    }
    public override void DestroyTaskItemById(string taskId)
    {
    }
    public override void SetNeedleNum()
    {
    }
    public override void TryTweenTaskBack()
    {
    }
}
