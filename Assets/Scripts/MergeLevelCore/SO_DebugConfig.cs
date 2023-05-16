using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using Ivy;
using Ivy.Common;
using Ivy.Firebase;
using IvyCore;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Sirenix.OdinInspector;

//[CreateAssetMenu(fileName = "SO_DebugConfig", menuName = "创建SO_DebugConfig配置")]
public class SO_DebugConfig : ScriptableObject
{
    [Title("测试付费"), Space(10), PropertyOrder(0)]
    [LabelText("激活测试付费 (在Development Build下，自动通过付费)")]
    public bool DebugPayPass;

    [Title("商城"), Space(10), PropertyOrder(0)]
    [LabelText("激活商城私人礼包刷新")]
    public bool DdebugRefreshShop;

#if UNITY_EDITOR
    [Button("清除金币砖石体力", ButtonSizes.Large), PropertyOrder(1)]
    public void DebugDeleteTest()
    {
        Currencies.AddTest();
    }

    [Title("测试回档操作"), PropertySpace(10)]
    [Button("编辑器运行时，打印输出存档Config/EditorConfig/player_base.json", ButtonSizes.Large), PropertyOrder(1)]
    public void DebugWritePlayerData()
    {
        if (!Application.isPlaying)
        {
            return;
        }
        string jsonstr = PlayerFirestoreData.Instance.LocalToFirestoreJson();
        System.IO.File.WriteAllText(Application.dataPath + "/Config/EditorConfig/player_base.json", jsonstr);
        UnityEditor.AssetDatabase.Refresh();
        Debug.LogWarning("打印输出存档Config/EditorConfig/player_base.json成功");
    }
    [Button("打开存档player_base.json", ButtonSizes.Large), PropertyOrder(1)]
    public void OpenPlayerFirestoreDataFolder()
    {
        UnityEditor.EditorUtility.OpenWithDefaultApp(Application.dataPath + "/Config/EditorConfig/player_base.json");
    }

    [LabelText("下次编辑器运行时，利用本地player_base.json模拟回档操作"), PropertyOrder(2)]
    public bool DebugReloadFirestoreData;
#endif


    [Title("跳过新手引导"), Space(10), PropertyOrder(3)]
    [LabelText("编辑器模式下，跳过新手引导")]
    public bool DebugSkipTutorial;

    [Title("测试物品"), Space(10), PropertyOrder(4)]
    [LabelText("激活")]
    public bool DebugAddToRewardBoxActive;
    [LabelText("向礼盒中添加测试物品"), PropertyOrder(5)]
    public List<string> DebugAddToRewardBox;

    [Title("测试产出气泡"), Space(10)]
    [LabelText("激活（仅限能产出气泡的物品）"), PropertyOrder(6)]
    public bool DebugSpawnBubble;
    [LabelText("产出概率"), PropertyOrder(7)]
    [Range(0, 100)]
    public int DebugSpawnBubbleRate;


    [Title("测试RemoteConfig"), Space(10)]
    [LabelText("激活(利用本地配表模拟)"), PropertyOrder(8)]
    public bool DebugRemoteConfig;
    [LabelText("测试当前版本"), PropertyOrder(9)]
    public string DebugCurrentVersion = "0.1.0";


    [Title("测试断网"), Space(10)]
    [LabelText("禁止获取服务器时间"), PropertyOrder(10)]
    public bool DebugNotGetServerTime = false;
    [LabelText("模拟获取服务器时间延迟（毫秒）"), PropertyOrder(11)]
    public int DebugGetServerTimeDelay = 500;

    [Title("测试时间"), Space(10)]
    [LabelText("激活设定服务器时间 (仅限编辑器下)"), PropertyOrder(12)]
    public bool DebugSetGetServer = false;
    [LabelText("设定当前服务器时间 (示例：2022/1/11 9:00:46 +00:00)"), PropertyOrder(13)]
    public string DebugSetServerDate;
    [LabelText("激活设定本地时间 (仅限编辑器下)"), PropertyOrder(14)]
    public bool DebugSetLocal = false;
    [LabelText("设定当前本地时间 (示例：2022/1/11 9:00:46 +00:00)"), PropertyOrder(15)]
    public string DebugSetLocalDate;

    [Title("测试新产出池"), Space(10)]
    [LabelText("启用新的产出池"), PropertyOrder(16)]
    public bool NewSpawnPool = false;

    [Title("测试任务"), Space(10)]
    [LabelText("直接完成任务"), PropertyOrder(17)]
    public bool CompleteTaskWithoutItem = false;
    [LabelText("向任务列表中增加某任务"), PropertyOrder(18)]
    public string TaskName;
    [LabelText("编辑器模式测试回档"), PropertyOrder(19)]
    public bool textRetreated;
    [LabelText("跳到某一关,为0时关闭跳关功能"), PropertyOrder(20)]
    public int SkipToLevel = 0;
    [LabelText("跳关时是否需要清理地图数据"), PropertyOrder(21)]
    public bool needClearMapByShipLevel = false;

    [LabelText("跳转支线"), PropertyOrder(22)]
    public MergeLevelType mergeLevel = MergeLevelType.none;


#if UNITY_EDITOR
    private static string Test_GetUSer_HTTP1 = "https://us-central1-merge-inn-7e97b.cloudfunctions.net/mail-get_json_by_uid?uid=";
    private static string Test_GetUSer_HTTP2 = "&access_token=993dcb3ba34d69ed2b06d78ab5";
    [Title("回档玩家数据"), Space(10)]
    [LabelText("玩家uid"), PropertyOrder(30)]
    public string userFirebaseID;
    [LabelText("回档"), Button(ButtonSizes.Large), PropertyOrder(30)]
    public void GetFirebaseByFirebaseID()
    {
        if (!Application.isPlaying)
            return;
        if (string.IsNullOrEmpty(userFirebaseID))
            return;
        SystemMonoEvent.Instance.StartCoroutine(Coroutine_GetFirebase());
    }

    public IEnumerator Coroutine_GetFirebase()
    {
        EditorUtility.DisplayProgressBar("回档", "下载中", 0.2f);
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(Test_GetUSer_HTTP1 + userFirebaseID + Test_GetUSer_HTTP2);
        unityWebRequest.timeout = 10;
        yield return unityWebRequest.SendWebRequest();
        EditorUtility.ClearProgressBar();
        if (unityWebRequest.error != null)
        {
            EditorUtility.DisplayDialog("回档", "网络异常", "ok");
        }
        else
        {
            try
            {
                string json = unityWebRequest.downloadHandler.text;
                JObject jObj = JObject.Parse(json);
                if (jObj.Count == 1)
                {
                    foreach (var item in jObj)
                    {
                        string jData = item.Value.ToString();
                        PlayerFirestoreData info = item.Value["player_data"].ToObject<PlayerFirestoreData>();
                        if (info != null)
                        {
                            UI_TutorManager.Instance.CloseTutor();
                            CloudSystem.Instance.TryChangeLocalDataFromFirestore(info);
                            EditorUtility.DisplayDialog("回档", "成功", "ok");
                        }
                        else
                        {
                            EditorUtility.DisplayDialog("回档", "数据异常！！！", "ok");
                        }
                        break;
                    }
                }
                else
                {
                    EditorUtility.DisplayDialog("回档", "数据异常！！！", "ok");
                }
            }
            catch (Exception e)
            {
                EditorUtility.DisplayDialog("回档", "出现错误！！！\n" + e.Message, "ok");
            }
        }

    }
#endif
}
