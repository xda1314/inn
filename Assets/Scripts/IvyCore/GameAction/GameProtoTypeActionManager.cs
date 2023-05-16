using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IvyCore
{
    public class GameProtoTypeActionManager: Singleton<GameProtoTypeActionManager>
    {
        public void RegistGameAction(string actionName,createActionDelegate delegateFunc)
        {
            if (!registGameActionCreateDic_.ContainsKey(actionName))
            {
                registGameActionCreateDic_.Add(actionName, delegateFunc);
                if(GlobleConfig.IsShowGameProtoTypeActionInitLog())
                {
                    UnityEngine.Profiling.Profiler.BeginSample("Wusunquan");
                    LogManager.Log("GameAction 注册, ActionName = {0}", actionName);
                    UnityEngine.Profiling.Profiler.EndSample();
                    //Debug.Log("GameAction 注册, ActionName =" + actionName);
                }
            }
            else
            {
                if (GlobleConfig.IsShowGameProtoTypeActionInitLog())
                    Debug.LogWarning("GameAction 重复注册 已替换, ActionName ="+actionName);
                registGameActionCreateDic_[actionName] = delegateFunc;
            }
        }

        public Dictionary<string, createActionDelegate> GetRegistGameActionCreateDic()
        {
            return registGameActionCreateDic_;
        }

        public delegate ActionBase createActionDelegate();
        private Dictionary<string, createActionDelegate> registGameActionCreateDic_ = new Dictionary<string, createActionDelegate>();
    }
}