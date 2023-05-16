using Merge.Romance.Contorls;
using System.Collections.Generic;
using UnityEngine;
namespace IvyCore
{
    public class UI_TutorManager : SingletonMono<UI_TutorManager>
    {
        [HideInInspector]
        public UI_TutorData UI_TutorData_;
        [HideInInspector]
        public UI_TutorLayer UI_TutorLayer_;

        Dictionary<string, Transform> mAssignTransformDic_;
        Dictionary<string, System.Action> mAssignCallbackDic_;

        //用于程序段调用时添加教学执行完毕后回调
        private System.Action mTutorEndAction_ = null;

        UI_TutorAtomData CurrentRunningTutorData_ = null;
        int CurrentRunningDataIndex_ = -1;

        public System.Action ForceEndTutorAppendCallback = null;

        #region merge专用
        public static int GridWidth = 150;
        public Vector2Int curMergeClickPos = Vector2Int.zero;
        public Vector2Int curMergeDragStartPos = Vector2Int.zero;
        public Vector2Int curMergeDragEndPos = Vector2Int.zero;
        public bool IsTutorialClickStoreItem = false;

        public bool Tutorial_ReadyToLevelUp = false;
        public bool Tutorial_ReadyToMergeBackpackOut;
        #endregion

        public bool IsMenuInputActive = false;

        public bool AddAssignCallback(string name, System.Action a)
        {
            mAssignCallbackDic_[name] = a;
            return true;
        }
        public bool RemoveAssignCallback(string name)
        {
            if (mAssignCallbackDic_.ContainsKey(name))
            {
                mAssignCallbackDic_.Remove(name);
                return true;
            }
            return false;
        }
        public void RemoveAllAssignCallback()
        {
            mAssignCallbackDic_.Clear();
        }
        public System.Action GetAssignCallback(string name)
        {
            if (mAssignCallbackDic_.ContainsKey(name))
            {
                return mAssignCallbackDic_[name];
            }
            return null;
        }

        public bool AddAssignTransform(string name, Transform t)
        {
            mAssignTransformDic_[name] = t;
            return true;
        }
        public bool RemoveAssignTransform(string name)
        {
            if (mAssignTransformDic_.ContainsKey(name))
            {
                mAssignTransformDic_.Remove(name);
                return true;
            }
            return false;
        }
        public void RemoveAllAssignTransform()
        {
            mAssignTransformDic_.Clear();
        }
        public Transform GetAssignTransform(string name)
        {
            if (mAssignTransformDic_.ContainsKey(name))
            {
                return mAssignTransformDic_[name];
            }
            return null;
        }


        public bool IsTutoring()
        {
            return CurrentRunningTutorData_ != null;
        }

        public int GetCurrentRunningDataIndex()
        {
            return CurrentRunningDataIndex_;
        }

        public UI_TutorAtomData GetCurrentRunningTutorData()
        {
            return CurrentRunningTutorData_;
        }

        public void ForceEndTutor()
        {
            Debug.LogWarning("UI_TutorManager-ForceEndTutor");
            if (UI_TutorLayer_ != null)
            {
                var count = UI_TutorLayer_.transform.childCount;
                for (var i = count - 1; i >= 0; --i)
                {
                    var child = UI_TutorLayer_.transform.GetChild(i);
                    if (child != null)
                    {
                        if (UI_TutorLayer_.UI_RayIgnore_ != null)
                        {
                            if (child != UI_TutorLayer_.UI_RayIgnore_.transform)
                                GameObject.Destroy(child.gameObject);
                        }
                        else
                        {
                            GameObject.Destroy(child.gameObject);
                        }
                    }
                }
                UI_TutorLayer_.HideDialog();
                UI_TutorLayer_.gameObject.SetActive(false);
                EndCurrentTutor();
                if (ForceEndTutorAppendCallback != null)
                    ForceEndTutorAppendCallback();
            }
        }

        void OnSceneChange(UnityEngine.SceneManagement.Scene s1, UnityEngine.SceneManagement.Scene s2)
        {
            ForceEndTutor();
        }

        private void Awake()
        {
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += (UnityEngine.SceneManagement.Scene s1, UnityEngine.SceneManagement.Scene s2) =>
            {
                OnSceneChange(s1, s2);
            };
            mAssignTransformDic_ = new Dictionary<string, Transform>();
            mAssignCallbackDic_ = new Dictionary<string, System.Action>();

            var layerProtoType = AssetSystem.Instance.LoadAsset<GameObject>("__UI__/TudorLayer", null).Result.GetComponent<UI_TutorLayer>();
            if (layerProtoType != null)
            {
                UI_TutorLayer_ = GameObject.Instantiate<UI_TutorLayer>(layerProtoType);
                UI_TutorLayer_.gameObject.SetActive(false);
                UI_TutorLayer_.transform.SetParent(this.transform);
            }

            //             UI_TutorData_ = UI_TutorData.GetData();
            //             if (UI_TutorData_ == null)
            //             {
            //                 Debug.LogError("教学数据初始化失败,教学模块将无法正常使用");
            //             }
            if (UI_TutorLayer_ == null)
            {
                Debug.LogError("教学层初始化失败,教学模块将无法正常使用");
            }
        }

        public void ClearData()
        {
            if (UI_TutorData_ != null)
            {
                UI_TutorData_.ClearData();
                Resources.UnloadAsset(UI_TutorData_);
            }
            UI_TutorData_ = null;
        }

        public void RunTutorWithName(string name, System.Action endCallBack = null)
        {
            if (isTutorClose)
                return;
            ShowAnalytics("Tutorial_" + name);       
#if UNITY_EDITOR
            if (Debug.isDebugBuild && DebugSetting.CanUseDebugConfig(out var debugSO) && debugSO.DebugSkipTutorial)
            {
                Debug.LogWarning("当前教学被设置屏蔽!");
                return;
            }
#endif
            if (CurrentRunningTutorData_ != null)
            {
                Debug.LogWarning("当前教学未结束,新教学未能正常进行!");
                return;
            }

            if (UI_TutorData_ == null)
            {
                UI_TutorData_ = UI_TutorData.GetData();
                if (UI_TutorData_ == null)
                {
                    Debug.LogError("教学数据初始化失败,教学模块将无法正常使用");
                }
            }
            if (UI_TutorData_.IsDataNameExist(name))
            {
                var curData = UI_TutorData_.GetTutorAtomData(name);
                if (curData.GetDataCount() > 0)
                {
                    IsMenuInputActive = InputControl.Instance.IsActive();
                    if (IsMenuInputActive)
                        InputControl.Instance.SetActive(false);
                    CurrentRunningTutorData_ = curData;
                    CurrentRunningDataIndex_ = -1;
                    mTutorEndAction_ = endCallBack;
                    GoNextTutor();
                }
                else
                {
                    Debug.LogWarning("当前教学内容为空,新教学未能正常进行!");
                    return;
                }
            }
        }
        private void ShowAnalytics(string name) 
        {
            int analy = -1;
            switch (name) 
            {
                case Consts.SaveKey_Tutorial_InnGuide:
                    analy = 1;
                    break;
                case Consts.SaveKey_Tutorial_InnTask:
                    analy = 2;
                    break;
                case Consts.SaveKey_Tutorial_InnGetReward:
                    analy = 3;
                    break;
                case Consts.SaveKey_Tutorial_InnBackPack:
                    analy = 4;
                    break;
                case Consts.SaveKey_Tutorial_InnEndLevel:
                    analy = 5;
                    break;
                case Consts.SaveKey_Tutorial_InnLevelReward1:
                    analy = 6;
                    break;
                case Consts.SaveKey_Tutorial_InnLevelReward2:
                    analy = 7;
                    break;
                case Consts.SaveKey_Tutorial_InnLevelChest1:
                    analy = 8;
                    break;
                case Consts.SaveKey_Tutorial_InnLevelChest2:
                    analy = 9;
                    break;
                case Consts.SaveKey_Tutorial_InnStarChest1:
                    analy = 10;
                    break;
                case Consts.SaveKey_Tutorial_InnStarChest2:
                    analy = 11;
                    break;
                case Consts.SaveKey_Tutorial_GetEnergy:
                    analy = 12;
                    break;
                case Consts.SaveKey_Tutorial_InnAdventure1:
                    analy = 13;
                    break;
                default:
                    break;
            }
            if (analy != -1) 
            {
                AnalyticsUtil.TrackRetentionStep(analy, name);
            }           
        }

        public void EndCurrentTutor()
        {
            if (IsMenuInputActive)
                InputControl.Instance.SetActive(true);
            curMergeClickPos = Vector2Int.zero;
            curMergeDragStartPos = Vector2Int.zero;
            CurrentRunningTutorData_ = null;
            CurrentRunningDataIndex_ = -1;
            if (mTutorEndAction_ != null)
                mTutorEndAction_.Invoke();
        }

        public void GoNextTutor()
        {
            if (CurrentRunningTutorData_ != null)
            {
                if (CurrentRunningDataIndex_ >= 0)
                    CurrentRunningTutorData_.GetDataByIndex(CurrentRunningDataIndex_).OnEndTutor(UI_TutorLayer_);
                ++CurrentRunningDataIndex_;
                var nextData = CurrentRunningTutorData_.GetDataByIndex(CurrentRunningDataIndex_);
                if (nextData != null)
                    nextData.OnBeginTutor(UI_TutorLayer_);
                else
                {
                    EndCurrentTutor();
                }
            }
        }

        #region 教学的停止与开启
        private bool isTutorClose = false;
        public void CloseTutor()
        {
            isTutorClose = true;
            if (IsTutoring())
                ForceEndTutor();
        }
        public void OpenTutor()
        {
            isTutorClose = false;
        }
        #endregion
    }
}
