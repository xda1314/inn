using System.Collections.Generic;

namespace IvyCore
{
    public class UI_TutorAtomBaseDataManager : Singleton<UI_TutorAtomBaseDataManager>
    {
        public void RegistTutorAtomBaseData(string DataName, CreateTutorAtomBaseDataDelegate delegateFunc)
        {
            if (!RegistCreateTutorAtomBaseDataDic_.ContainsKey(DataName))
            {
                RegistCreateTutorAtomBaseDataDic_.Add(DataName, delegateFunc);
                //if (GlobleConfig.IsShowGameProtoTypeActionInitLog())
                //{
                //    UnityEngine.Profiling.Profiler.BeginSample("Wusunquan");
                //    LogManager.Log("GameAction 注册, ActionName = {0}", actionName);
                //    UnityEngine.Profiling.Profiler.EndSample();
                //    //Debug.Log("GameAction 注册, ActionName =" + actionName);
                //}
            }
            else
            {
                //if (GlobleConfig.IsShowGameProtoTypeActionInitLog())
                //    Debug.LogWarning("GameAction 重复注册 已替换, ActionName =" + actionName);
                RegistCreateTutorAtomBaseDataDic_[DataName] = delegateFunc;
            }
        }

        public Dictionary<string, CreateTutorAtomBaseDataDelegate> GetRegistCreateTutorAtomBaseDataDic()
        {
            return RegistCreateTutorAtomBaseDataDic_;
        }

        public delegate UI_TutorAtomBaseData CreateTutorAtomBaseDataDelegate();
        private Dictionary<string, CreateTutorAtomBaseDataDelegate> RegistCreateTutorAtomBaseDataDic_ = new Dictionary<string, CreateTutorAtomBaseDataDelegate>();
    }
}
