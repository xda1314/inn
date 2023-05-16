using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

namespace IvyCore
{
    public class UI_Utility
    {

        /// <summary>
        /// 加载外部图片生成纹理(仅限编辑器模式下使用)
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Texture2D LoadTextureFromExternalResource(string filePath)
        {
#if UNITY_EDITOR
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                fs.Seek(0, SeekOrigin.Begin);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, (int)fs.Length);
                fs.Close();
                fs.Dispose();
                fs = null;
                Texture2D texture = new Texture2D(512, 512);
                texture.LoadImage(bytes);
                return texture;
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
#endif
            return null;
        }

        public static Vector2 CenterPivot = new Vector2(0.5f, 0.5f);

        /// <summary>
        /// 加载外部图片生成Sprite(不添加至工程,仅限编辑器模式下使用)
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Sprite LoadSpriteFromExternalResource(string filePath)
        {
#if UNITY_EDITOR
            if (!File.Exists(filePath))
            {
                Debug.LogErrorFormat("LoadSpriteFromExternalResource() cannot find file:{0}", filePath);
                return null;
            }
            var tex = LoadTextureFromExternalResource(filePath);
            Sprite s = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), CenterPivot);
            return s;
#else
            return null;
#endif
        }
    }
}