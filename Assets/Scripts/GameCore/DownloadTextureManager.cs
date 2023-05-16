using Ivy;
using Ivy.Common;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadTextureManager
{
    // https://cdn.lisgame.com/promote/com.merge.elves.png

    public static bool HasTextureCache(string uri)
    {
        string localPath = Path.Combine(Application.persistentDataPath, uri.Replace("https://", "").Replace("http://", ""));
        return File.Exists(localPath);
    }

    public static void DownloadTextureAsync(string uri, Action successCB, Action failCB)
    {
        SystemMonoEvent.Instance.StartCoroutine(Coroutine_LoadTexture(uri, _ =>
        {
            successCB?.Invoke();
        }, failCB, true));
    }

    private static IEnumerator Coroutine_LoadTexture(string uri, Action<Texture2D> successCB, Action failCB, bool saveLocal = false)
    {
        if (string.IsNullOrEmpty(uri))
            yield break;

        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(new System.Uri(uri)))
        {
            yield return uwr.SendWebRequest();
            if (uwr.isNetworkError || uwr.isHttpError)
            {
                LogUtils.LogError("[DownloadTextureManager][Coroutine_LoadTexture] error:" + uwr.error);
                LogUtils.LogError("[DownloadTextureManager][Coroutine_LoadTexture] uri:" + uri);
                failCB?.Invoke();
            }
            else
            {
                var texture = DownloadHandlerTexture.GetContent(uwr);
                if (texture != null && (texture.width != 8 || texture.height != 8))
                {
                    if (saveLocal)
                    {
                        var data = uwr.downloadHandler.data;
                        string localPath = Path.Combine(Application.persistentDataPath, uri.Replace("https://", "").Replace("http://", ""));
                        SaveTextureToLocal(localPath, data);
                    }
                    successCB?.Invoke(texture);
                }
                else
                {
                    LogUtils.LogError("[DownloadTextureManager][Coroutine_LoadTexture] data");
                    failCB?.Invoke();
                }
            }
        }
    }


    public static void LoadTextureAsync(string uri, Action<Texture2D> finishCB, Action failCB)
    {
        if (string.IsNullOrEmpty(uri))
            return;

        string localPath = Path.Combine(Application.persistentDataPath, uri.Replace("https://", "").Replace("http://", ""));
        if (File.Exists(localPath))
        {
            SystemMonoEvent.Instance.StartCoroutine(Coroutine_LoadTexture(localPath, finishCB, failCB));
        }
        else
        {
            SystemMonoEvent.Instance.StartCoroutine(Coroutine_LoadTexture(uri, finishCB, failCB, true));
        }
    }

    private static void SaveTextureToLocal(string localPath, byte[] bytes)
    {
        try
        {
            if (bytes == null)
                return;

            string dirPath = Path.GetDirectoryName(localPath);
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            if (File.Exists(localPath))
                File.Delete(localPath);

            using (var fs = new FileStream(localPath, FileMode.Create, FileAccess.Write))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
        }
        catch (Exception e)
        {
            LogUtils.LogError("[SaveTextureToLocal error]" + e);
        }

    }
}
