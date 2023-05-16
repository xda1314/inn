using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace IvyCore
{
    //[RequireComponent(typeof(RectTransform)), RequireComponent(typeof(Canvas)), RequireComponent(typeof(CanvasScaler)), RequireComponent(typeof(GraphicRaycaster)), DisallowMultipleComponent]
    public class UI_Window : MonoBehaviour
    {

        //#if UNITY_EDITOR
        //        [MenuItem("IvyCore/UI/Create/UIWindow")]
        //        static void createUIWindow()
        //        {
        //            GameObject obj = new GameObject();
        //            obj.name = "UI_Window";
        //            obj.AddComponent<UI_Window>();
        //            var canvas = obj.GetComponent<Canvas>();
        //            if (canvas != null)
        //            {
        //                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        //            }
        //            EditorGUIUtility.PingObject(obj.gameObject);
        //        }
        //#endif
        //        [LabelText("自动注册层级"), Tooltip("============")]
        //        public bool autoRegister_ = true;
        //        [ValueDropdown("GetAllRegisterLayerName"), ShowIf("autoRegister_"), LabelText("层级名称")]
        //        public string registerLayerName_;

        //        [LabelText("UI渲染使用Camera模式")]
        //        public bool canvasRenderModeUseCamera_ = false;

        //        private List<string> GetAllRegisterLayerName()
        //        {
        //#if UNITY_EDITOR
        //            List<string> layerNameList = new List<string>();
        //            var data = AssetDatabase.LoadAssetAtPath<UI_LayerRegisterData>("Assets/" + UI_LayerRegisterData.DataFilePath + "/" + UI_LayerRegisterData.DataFileName);
        //            if (data != null)
        //            {

        //                foreach (var layerData in data.dataList_)
        //                {
        //                    layerNameList.Add(layerData.registName_);
        //                }

        //            }
        //            return layerNameList;
        //#else
        //            return UI_Manager.Instance.GetRegisterLayerNames();
        //#endif
        //        }

        //public void Awake()
        //{

        //}

        //public void OnEnable()
        //{

        //}

        public void RunEnterAction()
        {
            var childList = transform.GetComponentsInChildren<UI_Base>();
            for (var i = 0; i < childList.Length; ++i)
            {
                childList[i].RunEnterAction();
            }
        }

        public void RunOutAction()
        {
            UISystem.Instance.HideUI(GetComponent<UIPanelBase>());
            //var childList = transform.GetComponentsInChildren<UI_Base>();
            //for (var i = 0; i < childList.Length; ++i)
            //{
            //    childList[i].RunOutAction();
            //}
        }

        //public void Start()
        //{
        //    RunEnterAction();

        //    if (autoRegister_)
        //    {
        //        if (string.IsNullOrEmpty(registerLayerName_))
        //        {
        //            Debug.LogErrorFormat("窗体 {0} 未选择将注册的UI层级", this.gameObject.name);
        //        }
        //        else
        //        {
        //            UI_Manager.Instance.AddWindowToLayer(this.gameObject, registerLayerName_, UI_Manager.UIAddType.eSuperposition);
        //        }
        //    }
        //}
    }
}


