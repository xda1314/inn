using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorWindow_ShowAnimCurveDetail : EditorWindow {
    [MenuItem("IvyCore/Test/ShowAnimCurveDetail")]
    static void ShowWindow()
    {
        var window = EditorWindow.GetWindow<EditorWindow_ShowAnimCurveDetail>();
        window.Init();
        window.Show();
    }

    AnimationCurve ac_;
    public void Init()
    {
        ac_ = new AnimationCurve();
    }

    private void OnGUI()
    {
        ac_ = EditorGUILayout.CurveField(ac_);
        if(ac_!=null)
        {
            for(var i =0;i<ac_.length;++i)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(string.Format("Time:{0}",ac_.keys[i].time));
                GUILayout.Label(string.Format("InTangent:{0}", ac_.keys[i].inTangent));
                GUILayout.Label(string.Format("OutTangent:{0}", ac_.keys[i].outTangent));
                GUILayout.Label(string.Format("TangentMode:{0}", ac_.keys[i].tangentMode));
                GUILayout.Label(string.Format("Value:{0}", ac_.keys[i].value));
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
