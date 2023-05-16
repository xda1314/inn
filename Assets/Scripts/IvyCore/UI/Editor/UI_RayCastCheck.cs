using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_RayCastCheck{

    [MenuItem("IvyCore/UI/Tools/RayCast检测")]
	public static void RayCastCheck()
    {
        var scene = SceneManager.GetActiveScene();
        if(scene!=null)
        {
            var allObj = scene.GetRootGameObjects();
            for (var i = 0;i< allObj.Length; ++i)
            {
                if(allObj[i].name.Equals("__RaycastChecker__"))
                {
                    return;
                }
            }
            GameObject newObj = new GameObject();
            newObj.name = "__RaycastChecker__";
            newObj.AddComponent<UI_Tools_RaycastChecker>();
        }
    }

    [MenuItem("IvyCore/UI/Tools/取消当前场景RayCast检测")]
    public static void CancelCurrentSceneRayCastCheck()
    {
        var scene = SceneManager.GetActiveScene();
        if (scene != null)
        {
            var allObj = scene.GetRootGameObjects();
            for (var i = 0; i < allObj.Length; ++i)
            {
                if (allObj[i].name.Equals("__RaycastChecker__"))
                {
                    GameObject.DestroyImmediate(allObj[i]);
                }
            }
            
        }
    }

}
