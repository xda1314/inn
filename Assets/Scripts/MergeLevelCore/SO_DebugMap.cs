using Sirenix.OdinInspector;
using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SO_DebugMap : ScriptableObject
{

    [LabelText("开启编辑地图"), PropertyOrder(0), OnValueChanged("OnEnterDebugMapValueChange")]
    public bool Debug_EnterDebugMap;


    [Space(5)]
    [Header("设置格子颜色")]
    [EnableIf("Debug_EnterDebugMap"), LabelText("底板图片"), PropertyOrder(10)]
    public Sprite Debug_MapBgSprite;
    [EnableIf("Debug_EnterDebugMap"), LabelText("格子图片"), PropertyOrder(11)]
    public Sprite Debug_MapGridSprite;


#if UNITY_EDITOR
    //[EnableIf("Debug_EnterDebugMap"), Button("进入上面地图", ButtonSizes.Large), PropertyOrder(22)]
    //public void EnterEditMap()
    //{
    //    if (!Application.isPlaying)
    //    {
    //        UnityEditor.EditorUtility.DisplayDialog("失败", "请先运行游戏，再点击进入地图", "ok");
    //        return;
    //    }

    //    if (DebugSetting.CanUseDebugMap(out var debugMap) && debugMap.Debug_EnterDebugMap)
    //    {
    //        UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_TestMap, UIShowLayer.TopPopup));
    //    }
    //}
    //[EnableIf("Debug_EnterDebugMap"), Button("清除上面地图存档", ButtonSizes.Large), PropertyOrder(23)]
    //public void ResetEditMapData()
    //{
    //    PlayerPrefs.DeleteKey(Consts.SaveKey_LevelData_Prefix + MapKey);
    //}
#endif

    private void OnEnterDebugMapValueChange()
    {
        if (!Application.isPlaying)
            return;

        UISystem.Instance?.uiMainMenu?.pagePlay?.RefreshBackGroundText();
    }


}
