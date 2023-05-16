using Ivy.Addressable;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AssetSystem : AddressableSystem
{
    public new static AssetSystem Instance { get; } = new AssetSystem();

    private Dictionary<string, Stack<GameObject>> gameObjectPoolDict = new Dictionary<string, Stack<GameObject>>();
    private Dictionary<string, Stack<MergeItem>> mergeItemPoolDict = new Dictionary<string, Stack<MergeItem>>();
    private Queue<BubbleItem> BubbleItemPoolQueue = new Queue<BubbleItem>();


    public GameObject Instantiate(string prefabName, Transform parent, Vector3 pos, Vector3 eular, Vector3 scale)
    {
        GameObject gO = Instantiate(prefabName, parent);
        if (gO != null)
        {
            gO.transform.localPosition = pos;
            gO.transform.localEulerAngles = eular;
            gO.transform.localScale = scale;
        }
        return gO;
    }

    /// <summary>
    /// 销毁物体
    /// </summary>
    public void DestoryGameObject(string prefabName, GameObject gO)
    {
        if (gO == null)
            return;

#if UNITY_EDITOR
        GameObject.DestroyImmediate(gO);
#else
        GameObject.Destroy(gO);
#endif
    }



    /// <summary>
    /// 生成场景Item
    /// </summary>
    public MergeItem SpawnMergeItem(string prefabName, Transform parent)
    {
        if (string.IsNullOrEmpty(prefabName))
        {
            Debug.LogError("[SpawnMergeItem] prefab name is null");
            return null;
        }


        if (mergeItemPoolDict.TryGetValue(prefabName, out Stack<MergeItem> mergeItemQueue))
        {
            if (mergeItemQueue != null && mergeItemQueue.Count > 0)
            {
                MergeItem itemTemp = mergeItemQueue.Pop();
                if (itemTemp != null)
                {
                    itemTemp.transform.SetParent(parent);
                    itemTemp.transform.localPosition = Vector3.zero;
                    itemTemp.transform.localEulerAngles = Vector3.zero;
                    itemTemp.transform.localScale = Vector3.one;
                    itemTemp.spriteRootTran.localScale = Vector3.one;
                    itemTemp.enabled = true;
                    return itemTemp;
                }
            }
        }

        GameObject prefab = Instantiate(prefabName, parent);
        if (prefab == null)
        {
            return null;
        }

        GameObject itemRoot = new GameObject(prefabName);
        //RectTransform rectTran = itemRoot.AddComponent<RectTransform>();
        itemRoot.gameObject.name = prefabName;
        itemRoot.tag = "Item";
        itemRoot.layer = LayerMask.NameToLayer("UI");
        itemRoot.transform.SetParent(parent);
        itemRoot.transform.localPosition = Vector3.zero;
        itemRoot.transform.localEulerAngles = Vector3.zero;
        itemRoot.transform.localScale = Vector3.one;
        //rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, UIPanel_Merge.gridWidth);
        //rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, UIPanel_Merge.gridWidth);

        GameObject spriteRoot = new GameObject("SpriteRoot", typeof(RectTransform));
        spriteRoot.layer = LayerMask.NameToLayer("UI");
        spriteRoot.transform.SetParent(itemRoot.transform);
        spriteRoot.transform.localPosition = Vector3.zero;
        spriteRoot.transform.localEulerAngles = Vector3.zero;
        spriteRoot.transform.localScale = Vector3.one;

        prefab.transform.SetParent(spriteRoot.transform);
        prefab.transform.localPosition = Vector3.zero;
        prefab.transform.localEulerAngles = Vector3.zero;
        prefab.transform.localScale = Vector3.one;

        MergeItem mergeItem = itemRoot.gameObject.AddComponent<MergeItem>();
        mergeItem.transition = UnityEngine.UI.Selectable.Transition.None;
        mergeItem.spriteRootTran = spriteRoot.transform;
        mergeItem.mainIconSprite = prefab;
        mergeItem.mainIconSprite.SetActive(true);

        return mergeItem;
    }

    /// <summary>
    /// 删除场景Item
    /// </summary>
    /// <param name="mergeItem"></param>
    private const int maxCycleCount = 5;
    public void UnspawnMergeItem(MergeItem mergeItem)
    {
        if (mergeItem == null)
        {
            Debug.LogError("[UnspawnMergeItem] mergeItem is null!");
            return;
        }
        mergeItem.BefroeUnspawn();
        mergeItem.gameObject.SetActive(false);

        mergeItem.enabled = true;
        mergeItem.transform.SetParent(UISystem.Instance.poolRootTran);


        if (mergeItem.ItemData == null
            || mergeItem.mainIconSprite == null
            || string.IsNullOrEmpty(mergeItem.ItemData.PrefabName))
        {
            Debug.LogError("[UnspawnMergeItem] mergeItem data lost!");
#if UNITY_EDITOR
            GameObject.DestroyImmediate(mergeItem.gameObject);
#else
            GameObject.Destroy(mergeItem.gameObject);
#endif
            return;
        }

        string prefabName = mergeItem.ItemData.PrefabName;
        if (mergeItemPoolDict.TryGetValue(prefabName, out Stack<MergeItem> uiQueue))
        {
            if (uiQueue.Count <= maxCycleCount)
            {
                var enqueue = true;
                var itr = uiQueue.GetEnumerator();
                while (itr.MoveNext())
                {
                    //if (mergeItem == itr.Current)
                    if (ReferenceEquals(mergeItem, itr.Current))
                    {
                        enqueue = false;
                        break;
                    }
                }
                if (enqueue)
                    uiQueue.Push(mergeItem);
            }
            else
            {
#if UNITY_EDITOR
                GameObject.DestroyImmediate(mergeItem.gameObject);
#else
                GameObject.Destroy(mergeItem.gameObject);
#endif
                return;
            }
        }
        else
        {
            uiQueue = new Stack<MergeItem>();
            uiQueue.Push(mergeItem);
            mergeItemPoolDict.Add(prefabName, uiQueue);
        }

    }

    // 生成场景泡泡Item
    public BubbleItem SpawnBubbleItem(Transform parent)
    {
        if (BubbleItemPoolQueue.Count > 0)
        {
            BubbleItem itemTemp = BubbleItemPoolQueue.Dequeue();
            if (itemTemp != null)
            {
                itemTemp.transform.SetParent(parent);
                itemTemp.transform.localPosition = Vector3.zero;
                itemTemp.transform.localEulerAngles = Vector3.zero;
                itemTemp.transform.localScale = Vector3.one;
                itemTemp.enabled = true;
                return itemTemp;
            }
        }

        GameObject prefab = Instantiate("BubbleItem", parent);
        if (prefab == null)
            return null;
        if (prefab.TryGetComponent<BubbleItem>(out var bubbleItem))
        {
            //bubbleItem.gameObject.name = $"Bubble_{itemData.Definition.PrefabName}";
            bubbleItem.gameObject.layer = LayerMask.NameToLayer("UI");
            bubbleItem.transform.SetParent(parent);
            bubbleItem.transform.localPosition = Vector3.zero;
            bubbleItem.transform.localEulerAngles = Vector3.zero;
            bubbleItem.transform.localScale = Vector3.one;
            return bubbleItem;
        }
        return null;
    }

    // 删除场景泡泡Item
    public void UnspawnBubbleItem(BubbleItem bubbleItem)
    {
        if (bubbleItem == null)
        {
            Debug.LogError("[UnspawnMergeItem] mergeItem is null!");
            return;
        }
        bubbleItem.StopAllBubbleStates();
        bubbleItem.gameObject.SetActive(false);
        bubbleItem.transform.SetParent(UISystem.Instance.poolRootTran);

        if (BubbleItemPoolQueue.Count <= maxCycleCount)
        {
            var enqueue = true;
            var itr = BubbleItemPoolQueue.GetEnumerator();
            while (itr.MoveNext())
            {
                if (ReferenceEquals(bubbleItem, itr.Current))
                {
                    enqueue = false;
                    break;
                }
            }
            if (enqueue)
                BubbleItemPoolQueue.Enqueue(bubbleItem);
        }
        else
        {
#if UNITY_EDITOR
            GameObject.DestroyImmediate(bubbleItem.gameObject);
#else
            GameObject.Destroy(bubbleItem.gameObject);
#endif
            return;
        }
    }

    // 预加载关卡元素
    public void PreloadMergeItems()
    {
        List<string> nameList = new List<string>();//存储要加入缓存池的预制体名字，再创建预制体（防止遍历创建缓存池的时候字典被改动）
        List<string> completedLevel = new List<string>();
        var levelTypeList = DungeonSystem.Instance.GetFinishedDungeon();
        if (levelTypeList != null)
        {
            foreach (var item in levelTypeList)
            {
                completedLevel.Add(MergeLevelManager.Instance.GetLevelNameByLevelType(item));
            }
        }
        var totalMapDataDic = MergeLevelManager.Instance.totalMapDataDict;
        if (totalMapDataDic != null)
        {
            foreach (var mapData in totalMapDataDic)
            {
                if (!completedLevel.Contains(mapData.Key))
                {
                    foreach (var item in mapData.Value.itemDataDict)
                    {
                        if (item.Value != null)
                        {
                            nameList.Add(item.Value.PrefabName);
                        }
                    }
                }
            }

            for (int i = 0; i < nameList.Count; i++)
            {
                if (!string.IsNullOrEmpty(nameList[i]))
                {
                    if (mergeItemPoolDict.TryGetValue(nameList[i], out Stack<MergeItem> itemQueue))
                    {
                        if (itemQueue.Count < maxCycleCount)
                        {
                            var poolItem = CreatePoolMergeItem(nameList[i]);
                            if (poolItem != null)
                                itemQueue.Push(poolItem);

                        }
                    }
                    else
                    {
                        var poolItem = CreatePoolMergeItem(nameList[i]);
                        if (poolItem != null)
                        {
                            var queue = new Stack<MergeItem>();
                            queue.Push(poolItem);
                            mergeItemPoolDict.Add(nameList[i], queue);
                        }
                    }
                }
            }
        }
    }

    private MergeItem CreatePoolMergeItem(string prefabName)
    {
        GameObject prefab = Instantiate(prefabName, UISystem.Instance.poolRootTran);
        if (prefab == null)
            return null;

        GameObject itemRoot = new GameObject(prefabName);
        itemRoot.gameObject.name = prefabName;
        itemRoot.tag = "Item";
        itemRoot.layer = LayerMask.NameToLayer("UI");
        itemRoot.transform.SetParent(UISystem.Instance.poolRootTran);
        itemRoot.transform.localPosition = Vector3.zero;
        itemRoot.transform.localEulerAngles = Vector3.zero;
        itemRoot.transform.localScale = Vector3.one;

        GameObject spriteRoot = new GameObject("SpriteRoot", typeof(RectTransform));
        spriteRoot.layer = LayerMask.NameToLayer("UI");
        spriteRoot.transform.SetParent(itemRoot.transform);
        spriteRoot.transform.localPosition = Vector3.zero;
        spriteRoot.transform.localEulerAngles = Vector3.zero;
        spriteRoot.transform.localScale = Vector3.one;

        prefab.transform.SetParent(spriteRoot.transform);
        prefab.transform.localPosition = Vector3.zero;
        prefab.transform.localEulerAngles = Vector3.zero;
        prefab.transform.localScale = Vector3.one;

        MergeItem mergeItem = itemRoot.gameObject.AddComponent<MergeItem>();
        mergeItem.transition = UnityEngine.UI.Selectable.Transition.None;
        mergeItem.spriteRootTran = spriteRoot.transform;
        mergeItem.mainIconSprite = prefab;
        mergeItem.mainIconSprite.SetActive(true);
        mergeItem.gameObject.SetActive(false);
        return mergeItem;
    }




    public void PrewarmUI(string key, int prewarmCount)
    {
        int needCount = prewarmCount;
        if (gameObjectPoolDict.TryGetValue(key, out var gOStack))
        {
            if (gOStack.Count >= prewarmCount)
                return;
            else
                needCount = prewarmCount - gOStack.Count;
        }

        for (int i = 0; i < needCount; i++)
        {
            var gO = Instantiate(key, UISystem.Instance.poolRootTran);
            UnspawnUI(key, gO, prewarmCount);
        }
    }

    public GameObject SpawnUI(string key, Transform parent)
    {
        if (gameObjectPoolDict.TryGetValue(key, out var gOStack) && gOStack.Count > 0)
        {
            var gO = gOStack.Pop();
            gO.transform.SetParent(parent);
            if (!gO.activeSelf)
                gO.SetActive(true);
            return gO;
        }
        else
        {
            var gO = Instantiate(key, parent);
            if (!gO.activeSelf)
                gO.SetActive(true);
            return gO;
        }
    }

    public void UnspawnUI(string key, GameObject gO, int maxCycleCount = 5)
    {
        if (gO == null)
            return;
        gO.SetActive(false);

        if (!string.IsNullOrEmpty(key) && maxCycleCount > 0)
        {
            if (gameObjectPoolDict.TryGetValue(key, out var gOStack))
            {
                if (gOStack.Count < maxCycleCount)
                {
                    gO.transform.SetParent(UISystem.Instance.poolRootTran);
                    gOStack.Push(gO);
                    return;
                }
            }
            else
            {
                gOStack = new Stack<GameObject>();
                gO.transform.SetParent(UISystem.Instance.poolRootTran);
                gOStack.Push(gO);
                return;
            }
        }

#if UNITY_EDITOR
        GameObject.DestroyImmediate(gO);
#else
        GameObject.Destroy(gO);
#endif
    }
}
