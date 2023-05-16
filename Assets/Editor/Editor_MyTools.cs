using UnityEditor;
using UnityEngine;

public class Editor_MyTools
{
    [MenuItem("工具箱/设置Scene视角", priority = 300)]
    private static void SetSceneView()
    {
        SceneView.lastActiveSceneView.orthographic = true;
        SceneView.lastActiveSceneView.rotation = Quaternion.Euler(36.666f, 45, 0);
    }

    [MenuItem("GameObject/复制路径", priority = 48)]
    private static void CopyGameObjectPath()
    {
        Object obj = Selection.activeObject;
        if (obj == null)
        {
            Debug.LogError("You must select Obj first!");
            return;
        }
        string result = AssetDatabase.GetAssetPath(obj);
        if (string.IsNullOrEmpty(result))//如果不是资源则在场景中查找
        {
            Transform selectChild = Selection.activeTransform;
            if (selectChild != null)
            {
                result = selectChild.name;
                while (selectChild.parent != null)
                {
                    selectChild = selectChild.parent;
                    result = string.Format("{0}/{1}", selectChild.name, result);
                }
            }
        }
        Editor_MyTools.Copy(result);
        Debug.Log(string.Format("The gameobject:{0}'s path has been copied!", obj.name));
    }

    public static void Copy(string format, params object[] args)
    {
        string result = string.Format(format, args);
        TextEditor editor = new TextEditor();
        editor.text = result;
        editor.OnFocus();
        editor.Copy();
    }
}
