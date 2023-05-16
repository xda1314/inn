using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

namespace IvyCore
{
    public class CreateAsset : Editor
    {
        //[MenuItem("IvyCnf/Asset")]
        static void Create()
        {
            IvyCnfData cnf = ScriptableObject.CreateInstance<IvyCnfData>();
            if (!cnf)
            {
                Debug.LogWarning("cnf not found");
                return;
            }

            string path = Application.dataPath + "/" + IvyCnfManager.DataFilePath;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path = "Assets/" + IvyCnfManager.DataFilePath + "/" + IvyCnfManager.DataFileName;
            AssetDatabase.CreateAsset(cnf, path);
        }
    }
}