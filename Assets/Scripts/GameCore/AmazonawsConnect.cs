using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class AmazonawsConnect
{
    public static Dictionary<string, string> LambdaHttpUrls = new Dictionary<string, string>()
    {
        { "date","https://date.lisgame.com/"}
    };

    private static string encryptionToken = string.Empty;
    private static bool m_initFirestoreSucccess;

    private static IEnumerator RequestFromURL(string url, string json, Action<string> callback, string func)
    {
        //var uwr = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);
        byte[] bytes = Encoding.UTF8.GetBytes(json);
        string str = Convert.ToBase64String(bytes);
        Action<string> CallFunc = callback;
        using (var uwr = UnityWebRequest.Post(url, str))
        {

            //uwr.timeout = 3;
            //byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(m_sendJson);
            //uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            uwr.downloadHandler = new DownloadHandlerBuffer();
            uwr.SetRequestHeader("Content-Type", "application/json");
            if (!string.IsNullOrEmpty(encryptionToken))
            {
                uwr.SetRequestHeader("token", encryptionToken);
            }
            //Send the request then wait here until it returns
            yield return uwr.SendWebRequest();
            if (uwr.isNetworkError)
            {
                Debug.Log("Error While Sending: " + uwr.error + "-----func:" + func);
            }
            else
            {
                Debug.Log("Received: " + uwr.downloadHandler.text + "-----func:" + func);
                string text = uwr.downloadHandler.text;//response.DataAsText; // 服务器回复
                Dictionary<string, object> info = JsonConvert.DeserializeObject<Dictionary<string, object>>(text);
                UnityEngine.Debug.Log("--------------response data is " + text + "-----func:" + func);
                if (info != null && info.ContainsKey("result"))
                {
                    CallFunc?.Invoke(text);
                }
            }
        }
    }

    public static void CallLambdaFunction(string func, Dictionary<string, string> field, Action<string> callback)
    {
        if (LambdaHttpUrls.TryGetValue(func, out string url))
        {
            if (!m_initFirestoreSucccess && field != null && !field.ContainsKey("id") && (func == "mf_set_firestore" || func == "mf_read_firestore" || func == "mf_update_firestore"))
            {
                GameDebug.LogError("before init CallLambdaFunction func:" + func);
                return;
            }
            GameDebug.Log("--------------CallLambdaFunction func is " + func);
            string json = field == null ? string.Empty : JsonConvert.SerializeObject(field);
            GameManager.Instance.StartCoroutine(RequestFromURL(url, json, callback, func));
        }
    }


}

