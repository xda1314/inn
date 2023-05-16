/********************************************************************
	created:	2019/03/11
	created:	11:3:2019   11:54
	file base:	ScriptableObjectUtility
	file ext:	cs
	author:		Wusunquan
	
	purpose:	
*********************************************************************/
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

namespace IvyCore
{
    public static class ScriptableObjectUtility
    {
        public static void CreateAsset<T>(string createFileName = null, string createPath = null, Action doAfterCreate = null) where T : ScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T>();

            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (createPath == null)
            {
                if (path == "")
                {
                    path = "Assets";
                }
                else if (Path.GetExtension(path) != "")
                {
                    path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
                }
            }
            else
                path = createPath;

            string fileName;
            if (createFileName == null)
            {
                fileName = typeof(T).ToString();
            }
            else
            {
                fileName = createFileName;
            }
            string assetPathAndName = "";
            var dataPath = Application.dataPath;
            var wantCreatePath = dataPath.Substring(0, dataPath.Length - 6) + path;
            if(!Directory.Exists(wantCreatePath))
            {
                Directory.CreateDirectory(wantCreatePath);
            }
            if (fileName.EndsWith(".asset"))
                assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + fileName);
            else
                assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + fileName + ".asset");
            
            AssetDatabase.CreateAsset(asset, assetPathAndName);

            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;

            if (doAfterCreate != null)
            {
                doAfterCreate();
            }
        }
    }

}

