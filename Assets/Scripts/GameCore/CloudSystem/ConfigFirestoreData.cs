using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ivy.game;
using Ivy;

public class ConfigFirestoreData 
{
//    public const string CollectionName = "configs";

//    public const string DocumentID_Event_Date = "event_date";

//    // public static ConfigFirestoreData Instance => CloudSystem.GetServerMessage<ConfigFirestoreData>(CollectionName);

//    public override string GetCollectionName()
//    {
//        return CollectionName;
//    }

//    public void ReadFirestore_EventDate()
//    {
//        isReadDataFinish = false;
//#if UNITY_EDITOR
//        var asset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>($"Assets/Config/EditorConfig/{DocumentID_Event_Date}.json");
//        if (asset != null)
//        {
//            string jsonStr = asset.text;
//            RiseSdkListener.Instance.onFirestoreReadData(CollectionName + "|" + DocumentID_Event_Date + "|" + jsonStr);
//        }
//        return;
//#endif
//        ReadFirestore(DocumentID_Event_Date, (string jsonStr) =>
//        {
//            try
//            {
//                GameDebug.Log("[ParseEventDate]" + jsonStr);

//                int startIndex = jsonStr.IndexOf('|');
//                if (startIndex < 0)
//                {
//                    return;
//                }
//                string doucumentID = jsonStr.Substring(0, startIndex);
//                if (string.IsNullOrEmpty(doucumentID))
//                {
//                    return;
//                }
//                string jsonData = string.Empty;
//                if (startIndex + 1 >= jsonStr.Length)
//                {
//                    return;
//                }
//                jsonData = jsonStr.Substring(startIndex + 1);

//                switch (doucumentID)
//                {
//                    case DocumentID_Event_Date:
//                        {
//                            FirstoreConfigData_EventDate.ParseConfigData_EventDate(jsonData);
//                        }
//                        break;
//                    default:
//                        break;
//                }

//                GameManager.Instance.TryStartEvent();
//            }
//            catch (Exception e)
//            {
//                GameDebug.LogError(e);
//            }
//        });
//    }

}

