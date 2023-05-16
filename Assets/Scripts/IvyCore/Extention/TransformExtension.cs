using System;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension {
    public static Transform GetChildByName(this Transform trans, string name,bool recursion=false)
    {
        if(trans!=null)
        {
            var count = trans.childCount;
            for(var index = 0;index<count;++index)
            {
                var child = trans.GetChild(index);
                if(child.name.Equals(name))
                {
                    return child;
                }
                if(recursion)
                {
                    var recursionResult = child.GetChildByName(name,recursion);
                    if (recursionResult != null)
                        return recursionResult;
                }
            }
        }
        return null;
    }

    public static Transform[] GetChildrenByName(this Transform trans, string name, bool recursion = false)
    {
        List<Transform> topList = new List<Transform>();
        if (trans != null)
        {
            var count = trans.childCount;
            for (var index = 0; index < count; ++index)
            {
                var child = trans.GetChild(index);
                if (child.name.Equals(name))
                {
                    topList.Add(child);
                }
                if (recursion)
                {
                    child.GetChildrenByName(name,topList, recursion);
                }
            }
        }
        return topList.ToArray();
    }

    public static void GetChildrenByName(this Transform trans, string name,List<Transform> list, bool recursion = false)
    {
        if (trans != null)
        {
            var count = trans.childCount;
            for (var index = 0; index < count; ++index)
            {
                var child = trans.GetChild(index);
                if (child.name.Equals(name))
                {
                    list.Add(child);
                }
                if (recursion)
                {
                    child.GetChildrenByName(name, list,recursion);
                }
            }
        }
    }

    public static void DestoryAllChildren(this Transform trans,float delayDestoryTime=0,Func<Transform,bool> checkAction = null)
    {
        for(var i=trans.childCount-1;i>=0;--i)
        {
            var childTrans = trans.GetChild(i);
            if (checkAction == null || checkAction(childTrans))
            {
                GameObject.Destroy(childTrans.gameObject, delayDestoryTime);
            }
        }
    }
}
