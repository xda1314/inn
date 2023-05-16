using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShowTip_Item : MonoBehaviour
{
    [SerializeField] private GameObject m_ItemRoot;
    [SerializeField] private GameObject m_ArrowGO;
    [SerializeField] private GameObject m_UnDiscoveredTag;
    [SerializeField] private TextMeshProUGUI m_Index;
    [SerializeField] private GameObject m_SelectBox;
    [SerializeField] private GameObject Bg_Chain;
    [SerializeField] private GameObject Bg_Contain;
    [SerializeField] private GameObject Obj_Lock;

    GameObject SaveContainPrefab = null;
    string SaveContainPrefabName = null;
    /// <summary>
    /// 刷新可从容器获得的物品
    /// </summary>
    public void RefreshContaner(string prefabName, bool isLock)
    {
        if (SaveContainPrefab != null && SaveContainPrefabName != null)
        {
            AssetSystem.Instance.DestoryGameObject(SaveContainPrefabName, SaveContainPrefab);
        }
        m_ArrowGO.SetActive(false);
        m_Index.gameObject.SetActive(false);
        Bg_Chain.SetActive(false);
        Bg_Contain.SetActive(true);
        GameObject go = AssetSystem.Instance.Instantiate(prefabName, m_ItemRoot.transform);
        Obj_Lock.SetActive(isLock);

        SaveContainPrefab = go;
        SaveContainPrefabName = prefabName;
    }




    GameObject SaveChainPrefab = null;
    string SaveChainPrefabName = null;
    /// <summary>
    /// 刷新匹配链
    /// </summary>
    /// <param name="definition"></param>
    /// <param name="index"></param>
    /// <param name="showSelect">是否显示选中框</param>
    public void RefreshChain(MergeItemDefinition definition, int index, bool showSelect, bool isBox = false)
    {
        if (SaveChainPrefab != null && SaveChainPrefabName != null)
        {
            AssetSystem.Instance.DestoryGameObject(SaveChainPrefabName, SaveChainPrefab);
        }
        if (isBox) 
        {
            GameObject go = AssetSystem.Instance.Instantiate(definition.PrefabName, m_ItemRoot.transform);
            m_Index.gameObject.SetActive(false);
            return;
        }
        if (definition.PrefabName == "Needle") //针特殊处理
        {
            GameObject go = AssetSystem.Instance.Instantiate(definition.PrefabName, m_ItemRoot.transform);
            m_Index.gameObject.SetActive(false);
            return;
        }

        if (!m_Index.gameObject.activeSelf)
            m_Index.gameObject.SetActive(true);
        m_Index.text = (index + 1).ToString();
        m_Index.color = showSelect ? new Color32(90, 152, 77, 255) : new Color32(169, 72, 38, 255);
        if (definition.m_discoveryState == MergeItemDiscoveryState.Unlock || definition.m_discoveryState == MergeItemDiscoveryState.Discovered)
        {
            GameObject go = AssetSystem.Instance.Instantiate(definition.PrefabName, m_ItemRoot.transform);
            SaveChainPrefab = go;
            SaveChainPrefabName = definition.PrefabName;
            m_UnDiscoveredTag.SetActive(false);
        }
        else
        {
            m_UnDiscoveredTag.SetActive(true);
        }
        m_SelectBox.SetActive(showSelect);

    }






    public void SetupItemPosition(int id, int itemCount)
    {
        if (id == itemCount - 1 || id == 3 || id == 7 || id == 11 || id == 15 || id == 19 || id == 23)
            m_ArrowGO.SetActive(false);
        else
            m_ArrowGO.SetActive(true);
    }

}
