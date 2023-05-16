using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IvyCore
{

    public class UI_Manager : SingletonMono<UI_Manager>
    {
        //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        //static void OnInitializeLoad()
        //{
        //    SceneManager.activeSceneChanged += (Scene s1, Scene s2) =>
        //    {
        //        OnSceneChange(s1, s2);
        //    };
        //    UI_Manager.Instance.InitWhenEngineInitialize();
        //}

        //static void OnSceneChange(Scene s1, Scene s2)
        //{
        //    UI_Manager.Instance.InitWhenSceneChange(s1, s2);
        //}

        public void InitWhenEngineInitialize()
        {
            //进行层级数据加载
            var UI_LRD = UI_LayerRegisterData.getData();
            if (UI_LRD != null)
            {
                for (var i = 0; i < UI_LRD.dataList_.Count; ++i)
                {
                    var layerName = UI_LRD.dataList_[i].registName_;
                    uiLayerNameIndexDic_[layerName] = i;
                    uiLayerNameList_.Add(layerName);
                }
                Debug.Log("UI层级数据加载完毕");
            }
            else
            {
                Debug.LogError("UI层级数据加载失败");
            }
            UI_LRD = null;
            //进行窗体数据加载
            //var UI_WRD = UI_WindowRegisterData.getData();
            //if (UI_WRD != null)
            //{
            //    for (var i = 0; i < UI_WRD.dataList_.Count; ++i)
            //    {
            //        var data = UI_WRD.dataList_[i];
            //        uiWindowNamePrefabObjDic_[data.registName_] = data.windowPrefab_;
            //    }
            //    Debug.Log("UI窗体数据加载完毕");
            //}
            //else
            //{
            //    Debug.LogError("UI窗体数据加载失败");
            //}
            //UI_WRD = null;
            //进行场景关联数据加载
            var UI_SRD = UI_SceneRegisterData.getData();
            if (UI_SRD != null)
            {
                for (var i = 0; i < UI_SRD.dataList_.Count; ++i)
                {
                    var data = UI_SRD.dataList_[i];
                    if (!uiSceneLayerWindowDic_.ContainsKey(data.scenePath_))
                    {
                        uiSceneLayerWindowDic_[data.scenePath_] = new Dictionary<string, List<string>>();
                    }
                    foreach (var kv in data.sceneLayerDic_)
                    {
                        if (!uiSceneLayerWindowDic_[data.scenePath_].ContainsKey(kv.Key))
                        {
                            uiSceneLayerWindowDic_[data.scenePath_][kv.Key] = new List<string>();
                        }
                        for (var j = 0; j < kv.Value.Count; ++j)
                        {
                            uiSceneLayerWindowDic_[data.scenePath_][kv.Key].Add(kv.Value[j]);
                        }
                    }
                }
                Debug.Log("UI场景关联数据加载完毕");
            }
            else
            {
                Debug.LogError("UI场景关联数据加载失败");
            }
            UI_SRD = null;
        }

        //public void InitWhenSceneChange(Scene s1, Scene s2)
        //{
        //    uiFindWindowsDic_.Clear();
        //    #region 创建层级节点
        //    {
        //        UnityEngine.Debug.Log("创建层级节点");
        //        uiRootNode_ = new GameObject()
        //        {
        //            name = uiRootNodeName_
        //        };
        //        uiRootNode_.transform.position = Vector3.zero;
        //        var enumerator = uiLayerNameIndexDic_.GetEnumerator();
        //        while (enumerator.MoveNext())
        //        {
        //            var nodeName = enumerator.Current.Key;
        //            var newGameObjNode = new GameObject() { name = nodeName };
        //            newGameObjNode.transform.position = Vector3.zero;
        //            newGameObjNode.transform.SetParent(uiRootNode_.transform);
        //            uiLayerNameNodeDic_[nodeName] = newGameObjNode;
        //        }
        //    }
        //    #endregion

        //    #region 创建初始界面
        //    if (uiSceneLayerWindowDic_.ContainsKey(s2.path))
        //    {
        //        var layerDataDic = uiSceneLayerWindowDic_[s2.path];
        //        var enumeratorLayer = layerDataDic.GetEnumerator();
        //        while (enumeratorLayer.MoveNext())
        //        {
        //            var layerName = enumeratorLayer.Current.Key;
        //            var windowDataList = enumeratorLayer.Current.Value;
        //            for (int i = 0; i < windowDataList.Count; ++i)
        //            {
        //                var windowName = windowDataList[i];
        //                CreateAndAddWindowByName<GameObject>(windowName, layerName, UIAddType.eNone);
        //            }
        //        }
        //    }
        //    #endregion

        //    RefreshRegisterCamera(s2);
        //}
        ///// <summary>
        ///// 刷新当前场景中的camera
        ///// </summary>
        ///// <param name="_curScene"></param>
        //public void RefreshRegisterCamera(Scene _curScene)
        //{
        //    Scene curScene = _curScene;
        //    if (curScene == null)
        //    {
        //        curScene = SceneManager.GetActiveScene();
        //    }
        //    cameraRegisterDic_.Clear();
        //    var rootObjsList = curScene.GetRootGameObjects();
        //    Camera[] tempUseSaveList = Camera.allCameras;

        //    for (var index = 0; index < tempUseSaveList.Length; ++index)
        //    {
        //        if (cameraRegisterDic_.ContainsKey(tempUseSaveList[index].gameObject.name))
        //        {
        //            Debug.LogErrorFormat("当前场景中存在同名Camera {0},UI管理器中Camera注册异常", tempUseSaveList[index].gameObject.name);
        //        }
        //        else
        //            cameraRegisterDic_.Add(tempUseSaveList[index].gameObject.name, tempUseSaveList[index]);
        //    }
        //}

        //public bool IsCameraRegister(Camera camera)
        //{
        //    return cameraRegisterDic_.ContainsValue(camera);
        //}

        //public bool IsCameraRegister(string cameraName)
        //{
        //    return cameraRegisterDic_.ContainsKey(cameraName);
        //}

        //public Camera GetRegisterCamera(string name)
        //{
        //    if (name != null && cameraRegisterDic_.ContainsKey(name))
        //    {
        //        return cameraRegisterDic_[name];
        //    }
        //    return null;
        //}

        //public Camera GetDefaultRegisterCamera()
        //{
        //    if (cameraRegisterDic_.Count > 0)
        //    {
        //        var itr = cameraRegisterDic_.GetEnumerator();
        //        while (itr.MoveNext())
        //        {
        //            return itr.Current.Value;
        //        }
        //    }
        //    return null;
        //}

        //public bool AddWindowToLayer(GameObject window, string layerName, UIAddType addType)
        //{
        //    if (uiLayerNameNodeDic_.ContainsKey(layerName))
        //    {
        //        var layerNode = uiLayerNameNodeDic_[layerName];
        //        window.transform.SetParent(layerNode.transform);
        //        if (!uiFindWindowsDic_.ContainsKey(layerName))
        //        {
        //            uiFindWindowsDic_[layerName] = new List<GameObject>();
        //        }
        //        uiFindWindowsDic_[layerName].Add(window);
        //        return true;
        //    }
        //    return false;
        //}

        public T GetWindow<T>(string windowName, string layerName) where T : class
        {
            if (uiFindWindowsDic_.ContainsKey(layerName))
            {
                var itr = uiFindWindowsDic_[layerName].GetEnumerator();
                while (itr.MoveNext())
                {
                    if (itr.Current.name.Equals(windowName))
                    {
                        if (typeof(T) == typeof(GameObject))
                        {
                            return itr.Current as T;
                        }
                        return itr.Current.GetComponent<T>();
                    }
                }
            }
            return null;
        }

        static int orderInLayer = 1;
        [System.Obsolete]
        public void CreateAndAddWindowByName<T>(string windowName, string layerName, UIAddType addType, string cameraName = null, Action<UIPanelBase> action = null)
        {
            //UISystem.Instance.ShowUI(new UIPanelDataBase(windowName, UIShowLayer.None, panel =>
            //{
            //    action?.Invoke(panel);
            //    panel.NeedCycle = false;
            //    var canvas = panel.GetComponent<Canvas>();
            //    if (canvas != null && canvas.worldCamera == null)
            //    {
            //        canvas.sortingLayerID = SortingLayer.GetLayerValueFromName("UI");
            //        orderInLayer++;
            //        canvas.sortingOrder = orderInLayer;
            //        canvas.worldCamera = UISystem.Instance.UICamera;
            //        canvas.renderMode = RenderMode.ScreenSpaceCamera;
            //    }
            //}, null));


            //if (uiLayerNameNodeDic_.ContainsKey(layerName))
            //{
            //    var layerNode = uiLayerNameNodeDic_[layerName];
            //    var newWindow = AssetSystem.Instance.SpawnGameObject(windowName, layerNode.transform);
            //    if (newWindow == null)
            //    {
            //        UnityEngine.Debug.LogErrorFormat("Window Create Faild : Name = {0} LayerName = {1}", windowName, layerName);
            //    }
            //    newWindow.name = windowName;
            //    if (!uiFindWindowsDic_.ContainsKey(layerName))
            //    {
            //        uiFindWindowsDic_[layerName] = new List<GameObject>();
            //    }
            //    switch (addType)
            //    {
            //        case UIAddType.eMonopolize:
            //            break;
            //        case UIAddType.eSuperposition:
            //            break;
            //        default:
            //            break;
            //    }
            //    uiFindWindowsDic_[layerName].Add(newWindow);
            //    var newWindowT = newWindow.GetComponent<T>();
            //    var uiWindow = newWindow.GetComponent<UI_Window>();
            //    //if (uiWindow != null && uiWindow.canvasRenderModeUseCamera_)
            //    //{
            //    //    var canvas = uiWindow.GetComponent<Canvas>();
            //    //    if (canvas != null)
            //    //    {
            //    //        //var camera = GetRegisterCamera(cameraName);
            //    //        //if (camera == null)
            //    //        //{
            //    //        //    camera = GetDefaultRegisterCamera();
            //    //        //}
            //    //        //canvas.worldCamera = camera;
            //    //        canvas.worldCamera = UISystem.Instance.UICamera;
            //    //        canvas.renderMode = RenderMode.ScreenSpaceCamera;
            //    //    }
            //    //}
            //    return newWindowT;
            //}
            //return default(T);
        }

        public bool RemoveWindowByName(string layerName, string windowName, bool releaseObj = true)
        {
            var itr = uiFindWindowsDic_.GetEnumerator();
            while (itr.MoveNext())
            {
                if (itr.Current.Key.Equals(layerName))
                {
                    var list = itr.Current.Value;
                    var enrList = list.GetEnumerator();
                    var index = 0;
                    while (enrList.MoveNext())
                    {
                        if (enrList.Current.name.Equals(windowName))
                        {
                            list.RemoveAt(index);
                            if (releaseObj)
                                GameObject.Destroy(enrList.Current);
                            return true;
                        }
                        ++index;
                    }
                }
            }
            return false;
        }

        public bool NotifyWindowRunEnterOrOutAction(string windowName, bool enterActionRun)
        {
            var itr = uiFindWindowsDic_.GetEnumerator();
            while (itr.MoveNext())
            {
                var list = itr.Current.Value;
                var enrList = list.GetEnumerator();
                var index = 0;
                while (enrList.MoveNext())
                {
                    if (enrList.Current.name.Equals(windowName))
                    {
                        var uiWindowComponent = enrList.Current.GetComponent<UI_Window>();
                        if (uiWindowComponent != null)
                        {
                            if (enterActionRun)
                                uiWindowComponent.RunEnterAction();
                            else
                                uiWindowComponent.RunOutAction();
                            return true;
                        }
                    }
                    ++index;
                }
            }
            return false;
        }

        public bool NotifyWindowRunEnterOrOutAction(string layerName, string windowName, bool enterActionRun)
        {
            var itr = uiFindWindowsDic_.GetEnumerator();
            while (itr.MoveNext())
            {
                if (itr.Current.Key.Equals(layerName))
                {
                    var list = itr.Current.Value;
                    var enrList = list.GetEnumerator();
                    var index = 0;
                    while (enrList.MoveNext())
                    {
                        if (enrList.Current.name.Equals(windowName))
                        {
                            var uiWindowComponent = enrList.Current.GetComponent<UI_Window>();
                            if (uiWindowComponent != null)
                            {
                                if (enterActionRun)
                                    uiWindowComponent.RunEnterAction();
                                else
                                    uiWindowComponent.RunOutAction();
                                return true;
                            }
                        }
                        ++index;
                    }
                }
            }
            return false;
        }

        public bool RemoveWindowByName(string windowName, bool releaseObj = true, float delayTime = 0.0f)
        {
            var itr = uiFindWindowsDic_.GetEnumerator();
            while (itr.MoveNext())
            {
                var list = itr.Current.Value;
                var enrList = list.GetEnumerator();
                var index = 0;
                while (enrList.MoveNext())
                {
                    if (enrList.Current.name.Equals(windowName))
                    {
                        list.RemoveAt(index);
                        if (releaseObj)
                            GameObject.Destroy(enrList.Current, delayTime);
                        return true;
                    }
                    ++index;
                }
            }
            return false;
        }

        public bool RemoveWindowByObj(GameObject obj, bool releaseObj = true)
        {
            var itr = uiFindWindowsDic_.GetEnumerator();
            while (itr.MoveNext())
            {
                var list = itr.Current.Value;
                var enrList = list.GetEnumerator();
                var index = 0;
                while (enrList.MoveNext())
                {
                    if (enrList.Current == obj)
                    {
                        list.RemoveAt(index);
                        if (releaseObj)
                            GameObject.Destroy(enrList.Current);
                        return true;
                    }
                    ++index;
                }
            }
            return false;
        }

        void clearDataWhenSceneChanged()
        {
            uiFindWindowsDic_.Clear();
        }

        public List<string> GetRegisterLayerNames()
        {
            return uiLayerNameList_;
        }

        public void RegisterRefreshEvent(UI_Base obj, UnityAction<string> func, string eventGroupName = "default")
        {
            var gObj = obj.gameObject;
            RegisterRefreshEvent(gObj, func, eventGroupName);
        }

        public void RegisterRefreshEvent(GameObject obj, UnityAction<string> func, string eventGroupName = "default")
        {
            var gObj = obj;
            if (!uiElementRefreshEventRegisterDic.ContainsKey(eventGroupName))
            {
                uiElementRefreshEventRegisterDic[eventGroupName] = new Dictionary<GameObject, UIElementRefreshEvent>();
            }
            if (!uiElementRefreshEventRegisterDic[eventGroupName].ContainsKey(gObj))
            {
                uiElementRefreshEventRegisterDic[eventGroupName][gObj] = new UIElementRefreshEvent();
            }
            uiElementRefreshEventRegisterDic[eventGroupName][gObj].AddListener(func);
        }

        public void UnRegisterRefreshEvent(string eventGroupName)
        {
            if (uiElementRefreshEventRegisterDic.ContainsKey(eventGroupName))
            {
                uiElementRefreshEventRegisterDic.Remove(eventGroupName);
            }
        }

        public void UnRegisterRefreshEvent(UI_Base eventOwnerObj)
        {
            var gObj = eventOwnerObj.gameObject;
            UnRegisterRefreshEvent(gObj);
        }

        public void UnRegisterRefreshEvent(GameObject eventOwnerObj)
        {
            var etr = uiElementRefreshEventRegisterDic.GetEnumerator();
            while (etr.MoveNext())
            {
                if (etr.Current.Value.ContainsKey(eventOwnerObj))
                {
                    etr.Current.Value.Remove(eventOwnerObj);
                }
            }
        }

        public void InvokeRefreshEvent(string invokeParameter = "", string targetEventGroupName = "default")
        {
            if (uiElementRefreshEventRegisterDic.ContainsKey(targetEventGroupName))
            {
                var etr = uiElementRefreshEventRegisterDic[targetEventGroupName].GetEnumerator();
                while (etr.MoveNext())
                {
                    if (etr.Current.Key.activeSelf)
                        etr.Current.Value.Invoke(invokeParameter);
                }
            }
        }

        /// <summary>
        /// 引擎初始时根据源数据进行初始化 用于存储层级名称和实际Index对应关系
        /// </summary>
        Dictionary<string, int> uiLayerNameIndexDic_ = new Dictionary<string, int>();
        /// <summary>
        /// 引擎初始时根据源数据进行初始化 用于存储层级名称
        /// </summary>
        List<string> uiLayerNameList_ = new List<string>();
        /// <summary>
        /// 场景变更时刷新 用于存储层级名称和实际节点的对应关系
        /// </summary>
        Dictionary<string, GameObject> uiLayerNameNodeDic_ = new Dictionary<string, GameObject>();
        ///// <summary>
        ///// 引擎初始时根据源数据进行初始化 用于存储界面名称和界面prefab对应关系
        ///// </summary>
        //Dictionary<string, GameObject> uiWindowNamePrefabObjDic_ = new Dictionary<string, GameObject>();
        /// <summary>
        /// 引擎初始时根据源数据进行初始化 用于场景初始化时界面的创建
        /// </summary>
        Dictionary<string, Dictionary<string, List<string>>> uiSceneLayerWindowDic_ = new Dictionary<string, Dictionary<string, List<string>>>();
        /// <summary>
        /// 场景切换时创建的UI顶级节点
        /// </summary> 
        GameObject uiRootNode_ = null;
        /// <summary>
        /// 运行时保存当前各层级存在界面容器
        /// </summary>
        Dictionary<string, List<GameObject>> uiFindWindowsDic_ = new Dictionary<string, List<GameObject>>();
        /// <summary>
        /// 运行时保存当前各层级存在界面容器
        /// </summary>
        Dictionary<string, Camera> cameraRegisterDic_ = new Dictionary<string, Camera>();

        /// <summary>
        /// UI顶级节点名称
        /// </summary>
        const string uiRootNodeName_ = "IVY_UI_ROOT_NODE";

        bool firstLoad_ = true;
        /// <summary>
        /// 界面添加类型 独占和叠加两种方式
        /// </summary>
        public enum UIAddType
        {
            eNone,
            /// <summary>
            /// 独占 同层界面将执行退场动作
            /// </summary>
            eMonopolize,
            /// <summary>
            /// 叠加 对同层界面无影响
            /// </summary>
            eSuperposition,
        }
        public class UIElementRefreshEvent : UnityEvent<string> { }
        Dictionary<string, Dictionary<GameObject, UIElementRefreshEvent>> uiElementRefreshEventRegisterDic = new Dictionary<string, Dictionary<GameObject, UIElementRefreshEvent>>();

    }
}
