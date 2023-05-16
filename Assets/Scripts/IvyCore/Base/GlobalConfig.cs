using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace IvyCore
{
    public static class GlobleConfig
    {
        #region IvyCoreEditorUtilities相关
        public const bool DEFAULT_SHOW_GAME_PROTOTYPEACTION_INIT_LOG = false;
        public static string show_Game_ProtoTypeAction_Init_Log_Key = "SHOW_GAME_PROTOTYPEACTION_INIT_LOG";
        public static bool show_Game_ProtoTypeAction_Init_Log = DEFAULT_SHOW_GAME_PROTOTYPEACTION_INIT_LOG;


        /// <summary>
        /// 是否输出GameAction注册信息
        /// </summary>
        public static bool IsShowGameProtoTypeActionInitLog()
        {
#if UNITY_EDITOR
            return EditorPrefs.GetBool(show_Game_ProtoTypeAction_Init_Log_Key, DEFAULT_SHOW_GAME_PROTOTYPEACTION_INIT_LOG);
#else
            return false;
#endif
        }
        #endregion

#if UNITY_EDITOR
        static Dictionary<string, Texture2D> UnityBuildinIconDic = new Dictionary<string, Texture2D>();

        /// <summary>
        /// 获取Unity内建图标接口(仅限于UnityEditor下使用)
        /// </summary>
        /// <param name="name">内建图标名称</param>
        /// <returns></returns>
        public static Texture2D GetUnityBuildinIcon(string name)
        {
            if (UnityBuildinIconDic.ContainsKey(name))
            {
                return UnityBuildinIconDic[name];
            }
            var tex = UnityEditor.EditorGUIUtility.FindTexture(name);
            if (tex != null)
            {
                UnityBuildinIconDic[name] = tex;
                return tex;
            }
            return null;
        }
#endif
    }
}