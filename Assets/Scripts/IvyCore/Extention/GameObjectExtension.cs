using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtension {
    /// <summary>
    /// 递归获取所有子节点指定的组件 包括未激活节点
    /// </summary>
    public static void GetComponentsInChildrenByRecursion<T>(this GameObject obj, ref List<T> saveList,bool includeSelf = true,Func<T,bool> addToListCheckFunc=null,Func<T,bool> goOnRecursionCheckFunc=null) where T:class
    {
        if(includeSelf)
        {
            var component = obj.GetComponent(typeof(T));
            T findComponent = null;
            if (component != null)
            {
                findComponent = component as T;
                if (addToListCheckFunc != null)
                {
                    if (addToListCheckFunc(findComponent))
                    {
                        saveList.Add(findComponent);
                    }
                }
                else
                    saveList.Add(findComponent);
            }
            if (goOnRecursionCheckFunc != null)
            {
                if (!goOnRecursionCheckFunc(findComponent))
                {
                    return;
                }
            }
            var childCount = obj.transform.childCount;
            for (var i = 0; i < childCount; ++i)
            {
                obj.transform.GetChild(i).gameObject.GetComponentsInChildrenByRecursion<T>(ref saveList, includeSelf, addToListCheckFunc, goOnRecursionCheckFunc);
            }
        }
        else
        {
            var childCount = obj.transform.childCount;
            for (var i = 0; i < childCount; ++i)
            {
                T findComponent = obj.transform.GetChild(i).GetComponent<T>();

                if (findComponent != null)
                {
                    if (addToListCheckFunc != null)
                    {
                        if (addToListCheckFunc(findComponent))
                        {
                            saveList.Add(findComponent);
                        }
                    }
                    else
                        saveList.Add(findComponent);
                }
                if (goOnRecursionCheckFunc != null)
                {
                    if (!goOnRecursionCheckFunc(findComponent))
                    {
                        continue;
                    }
                }
                obj.transform.GetChild(i).gameObject.GetComponentsInChildrenByRecursion<T>(ref saveList, includeSelf, addToListCheckFunc, goOnRecursionCheckFunc);
            }
        }
    }
	
}