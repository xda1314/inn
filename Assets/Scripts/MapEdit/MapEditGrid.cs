using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEditGridData
{
    public Vector2Int gridID;
    public bool active;
    public string prefabName;
    public bool locked;
    public bool inBox;
}

public class MapEditGrid : MonoBehaviour
{
    public Transform itemRoot;
    public Text txt_grid;
    public GameObject lockIcon;
    public GameObject InBox;

    public MapEditGridData gridData;

    private string lastPrefabName;
    private GameObject prefab;
    private Action setGridDataCB;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClickGrid);
    }

    public void RefreshPrefab(MapEditGridData gridData, Action setGridData)
    {
        this.setGridDataCB = setGridData;
        if (!gridData.active)
            GetComponent<Image>().color = new Color(0, 0, 0, 1);
        else if (gridData.inBox)
            GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 1);
        else if (gridData.locked)
            GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
        else
            GetComponent<Image>().color = Color.white;
        lockIcon.SetActive(gridData.locked);
        InBox.SetActive(gridData.inBox);
        txt_grid.text = $"({gridData.gridID.x},{gridData.gridID.y})";
        if (!string.IsNullOrEmpty(gridData.prefabName))
        {
            if (lastPrefabName != gridData.prefabName)
            {
                if (prefab != null)
                {
                    DestroyImmediate(prefab);
                    prefab = null;
                }
                prefab = AssetSystem.Instance.Instantiate(gridData.prefabName, itemRoot);
            }
            else
            {
                if (prefab == null)
                {
                    prefab = AssetSystem.Instance.Instantiate(gridData.prefabName, itemRoot);
                }
            }
        }
        else
        {
            if (prefab != null)
            {
                DestroyImmediate(prefab);
                prefab = null;
            }
        }
    }

    private void OnClickGrid()
    {
        setGridDataCB?.Invoke();
    }

}
