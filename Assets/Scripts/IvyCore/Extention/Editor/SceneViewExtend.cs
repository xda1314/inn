using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace IvyCore
{
    public class SceneViewExtend {
        [InitializeOnLoadMethod]
        static void Init()
        {
            SceneView.onSceneGUIDelegate += OnSceneGUI;
        }
        static Vector2 RightMouseDownPosition_ = Vector2.zero;
        const float PositionThreshold_ = 5.0f;
        static void OnSceneGUI(SceneView sceneView)
        {
            Event e = Event.current;
            bool is_handled = false;
            if(Event.current != null && Event.current.button == 1 && Event.current.type == EventType.MouseDown)
            {
                RightMouseDownPosition_=new Vector2(Event.current.mousePosition.x, Event.current.mousePosition.y);
            }
            if (Event.current != null && Event.current.button == 1 && Event.current.type == EventType.MouseUp )
            {
                if (Math.Abs(RightMouseDownPosition_.x- Event.current.mousePosition.x) < PositionThreshold_ && 
                    Math.Abs(RightMouseDownPosition_.y - Event.current.mousePosition.y) < PositionThreshold_)
                {
                    if (Selection.gameObjects.Length>0&&Selection.gameObjects[0].transform is RectTransform)
                    {
                        if(Selection.gameObjects.Length==1)
                        {
                            AddPriorityItems();
                        }
                        else if(Selection.gameObjects.Length > 1)
                        {
                            AddLayoutItems();
                            AddSizeItems();
                        
                        }
                        if(Selection.gameObjects.Length>=1)
                        {
                            AddShowOrHideItems();
                        }
                        AddCommonItems();
                        SceneViewContextMenuExtend.Show();
                        is_handled = true;
                    }
                }
            }
            if (is_handled)
                Event.current.Use();
        }

        static void AddCommonItems()
        {
            //SceneViewContextMenuExtend.AddItem("添加系统控件/Button", false, () => { });
            //SceneViewContextMenuExtend.AddItem("添加公司控件/ProgressPar", false, () => { });
            SceneViewContextMenuExtend.AddItem("添加组件/教学相关/教学展示组件", false, () => {
                var selectObj = Selection.activeGameObject;
                if(selectObj!=null)
                {
                    if(selectObj.GetComponent<UI_TutorShowObject>()==null)
                    {
                        selectObj.AddComponent<UI_TutorShowObject>();
                    }
                    else
                    {
                        Debug.LogWarning("教学展示组件已添加,无需重复添加");
                    }
                }
            });
        }

        static void AddLayoutItems()
        {
            SceneViewContextMenuExtend.AddItem("布局相关/左对齐", false, SceneViewExtendTools.Layout_AlignToLeft);
            SceneViewContextMenuExtend.AddItem("布局相关/右对齐", false, SceneViewExtendTools.Layout_AlignToRight);
            SceneViewContextMenuExtend.AddItem("布局相关/上对齐", false, SceneViewExtendTools.Layout_AlignToTop);
            SceneViewContextMenuExtend.AddItem("布局相关/下对齐", false, SceneViewExtendTools.Layout_AlignToBottom);
            SceneViewContextMenuExtend.AddItem("布局相关/水平均匀分布", false, SceneViewExtendTools.Layout_UniformDistributionInHorziontal);
            SceneViewContextMenuExtend.AddItem("布局相关/垂直均匀分布", false, SceneViewExtendTools.Layout_UniformDistributionInVertical);
        }

        static void AddSizeItems()
        {
            SceneViewContextMenuExtend.AddItem("尺寸相关/尺寸一致[Max]", false, SceneViewExtendTools.Size_ResizeToMax);
            SceneViewContextMenuExtend.AddItem("尺寸相关/尺寸一致[Min]", false, SceneViewExtendTools.Size_ResizeToMin);
            foreach (var go in Selection.gameObjects)
            {
                var curEditGo = go;
                SceneViewContextMenuExtend.AddItem("尺寸相关/指定/" + curEditGo.name, false, () =>
                {
                    var height = ((RectTransform)curEditGo.transform).sizeDelta.y;
                    var width = ((RectTransform)curEditGo.transform).sizeDelta.x;
                    foreach (GameObject gameObject in Selection.gameObjects)
                    {
                        if(gameObject!= curEditGo)
                        {
                            Undo.RecordObject((RectTransform)gameObject.transform, "尺寸相关/指定");
                            ((RectTransform)gameObject.transform).sizeDelta = new Vector2(width, height);
                        }
                    }
                });
            }
        }

        static void AddPriorityItems()
        {
            SceneViewContextMenuExtend.AddItem("层次相关/最里层", false, SceneViewExtendTools.Priority_MoveToTop);
            SceneViewContextMenuExtend.AddItem("层次相关/最外层", false, SceneViewExtendTools.Priority_MoveToBottom);
            SceneViewContextMenuExtend.AddItem("层次相关/向上一层", false, SceneViewExtendTools.Priority_MoveUp);
            SceneViewContextMenuExtend.AddItem("层次相关/向下一层", false, SceneViewExtendTools.Priority_MoveDown);
        }

        static void AddShowOrHideItems()
        {
            bool hasHideObj = false;
            foreach (var item in Selection.gameObjects)
            {
                if (!item.activeSelf)
                {
                    hasHideObj = true;
                    break;
                }
            }
            if (hasHideObj)
                SceneViewContextMenuExtend.AddItem("显示", false, SceneViewExtendTools.Active_ShowAll);
            else
                SceneViewContextMenuExtend.AddItem("隐藏", false, SceneViewExtendTools.Active_HideAll);
        }
    }

}