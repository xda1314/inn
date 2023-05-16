using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace IvyCore
{
    public class IvyCoreEditorUtilities : AssetPostprocessor
    {
        

        static IvyCoreEditorUtilities()
        {
            Initialize();
        }

        static void Initialize()
        {
            GlobleConfig.show_Game_ProtoTypeAction_Init_Log = EditorPrefs.GetBool(GlobleConfig.show_Game_ProtoTypeAction_Init_Log_Key, GlobleConfig.DEFAULT_SHOW_GAME_PROTOTYPEACTION_INIT_LOG);
        }
        static bool ceshi1 = true;
        static bool ceshi2 = true;
        static bool sss = false;
        [PreferenceItem("IvyCore")]
        static void PreferencesGUI()
        {
            GlobleConfig.show_Game_ProtoTypeAction_Init_Log = EditorGUILayout.Toggle("全局日志", GlobleConfig.show_Game_ProtoTypeAction_Init_Log);
            EditorGUILayout.Space();
            ceshi1 = Sirenix.Utilities.Editor.SirenixEditorGUI.Foldout(ceshi1, "GameAction相关");
            if(ceshi1)
            {
                ++EditorGUI.indentLevel;
                EditorGUI.BeginChangeCheck();
                GlobleConfig.show_Game_ProtoTypeAction_Init_Log = EditorGUILayout.Toggle("显示GameAction注册日志", GlobleConfig.show_Game_ProtoTypeAction_Init_Log);
                if (EditorGUI.EndChangeCheck())
                    EditorPrefs.SetBool(GlobleConfig.show_Game_ProtoTypeAction_Init_Log_Key, GlobleConfig.show_Game_ProtoTypeAction_Init_Log);
                --EditorGUI.indentLevel;
            }
            ceshi2 = Sirenix.Utilities.Editor.SirenixEditorGUI.Foldout(ceshi2, "UI相关");
            if (ceshi2)
            {
                ++EditorGUI.indentLevel;
                EditorGUILayout.Toggle("fds", true);
                --EditorGUI.indentLevel;
            }
        }

        public static GameActions CreateAndGetGameActionsDataInEditorMode()
        {
            var filePathSB = new StringBuilder();
            filePathSB.Append(Application.dataPath).Append("/").Append(GameActions.DataFilePath).Append("/").Append(GameActions.DataFileName);
            if (!File.Exists(filePathSB.ToString()))
            {
                ScriptableObjectUtility.CreateAsset<GameActions>(GameActions.DataFileName, "Assets/" + GameActions.DataFilePath);
            }
            var data = AssetDatabase.LoadAssetAtPath<GameActions>("Assets/" + GameActions.DataFilePath + "/" + GameActions.DataFileName);
            return data;
        }
    }
}