using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "SO_AudioClip", menuName = "Create AudioClipSO SO")]
public class AudioClipSO : ScriptableObject
{
#if UNITY_EDITOR
    public static AudioSource tempAudioSource;

    [PropertyOrder(-100)]
    [PropertySpace(10)]
    [Button("停止播放所有音乐")]
    public void StopAudio()
    {
        if (tempAudioSource != null)
        {
            tempAudioSource.Stop();
            GameObject.DestroyImmediate(tempAudioSource.gameObject);
            tempAudioSource = null;
        }
    }
#endif

    [Title("背景音乐")]
    [LabelText("主界面背景音乐"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData homeBgm;

    [LabelText("关卡背景音乐"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData[] levelBgms;

    //[LabelText("转盘背景音乐"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    //public AudioClipData spinWheelBgm;

    [Title("消除音效")]
    [LabelText("Tap_Eliminate"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData Tap_Eliminate;
    [LabelText("Composites"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData[] Composites;

    [LabelText("Produce"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData Produce;
    [LabelText("Produce_EXP"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData Produce_EXP;//
    [LabelText("Produce_Bubble"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData Produce_Bubble;//.
    [LabelText("Break_Initial"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData Break_Initial;//.
    [LabelText("Break_Cobweb"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData Break_Cobweb;
    [LabelText("Break_Bubble"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData Break_Bubble;
    [LabelText("Break_Box"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData Break_Box;
    [LabelText("Use_Coins"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData Use_Coins;
    [LabelText("合成提示"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData merge_tips;
    [LabelText("放下"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData put_down;	

    [Title("通用音效")]
    [LabelText("Get_EXP"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData Get_EXP;//.
    [LabelText("Get_Coins"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData Get_Coins;//.
    [LabelText("Get_Gems"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData Get_Gems;//.
    [LabelText("Get_Energy"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData Get_Energy;//.
    [LabelText("Get_Reward"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData Get_Reward;//.
    [LabelText("Get_Good"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData Get_Good;//.

    [Title("UI音效")]
    [LabelText("Button"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData Button;
    [LabelText("Window_Open"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData Window_Open;
    [LabelText("Window_Close"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData Window_Close;
    [LabelText("Get_EXPReward"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData Get_EXPReward;//.
    [LabelText("Recharge"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData Recharge;//.
    [LabelText("Shop_Buy"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData Shop_Buy;//.重做

    [LabelText("Page切换"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData moveHomePage;
    [LabelText("获得新道具"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData getNewMergeItem;//.
    [LabelText("解锁出售撤回按钮"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData unlockSellReturnBtn;
    [LabelText("解锁背包位置"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData unlockStorePackOne;//.
    [LabelText("订单完成开场飞来"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData taskFinishFlyNear;//.
    [LabelText("订单完成开场飞走"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData taskFinishFlyFar;//.
    [LabelText("完成订单获得星星"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData finishTaskAndGetStar;//.
    /*[LabelText("订单完成弹窗"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData popup_Order;//*/
    [LabelText("图鉴箱子破碎"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData chainBookOpen;//.
    /*[LabelText("宝箱落地"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData boxFall;//
    [LabelText("宝箱打开"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData boxOpen;//*/
    [LabelText("宝箱奖励展示"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData boxGetReward;//
    [LabelText("奖励领取"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData btn_Receive;//
    [LabelText("转盘奖励"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData spinReward;//.
	[LabelText("礼盒开启"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData chest_open;//.
	[LabelText("礼盒奖励出现"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData chest_reward_show;//.
	[LabelText("过关"), LabelWidth(100), InlineProperty(LabelWidth = 50), Space(10)]
    public AudioClipData complete;//.
}


[System.Serializable]
public class AudioClipData
{
    [BoxGroup()]
    [LabelText("音量")]
    [Range(0f, 1f)]
    public float volumn = 1f;

    [BoxGroup()]
    [LabelText("音乐")]
    public AudioClip audioClip;

#if UNITY_EDITOR
    [HorizontalGroup("clip")]
    [Button("播放")]
    public async void PlayAudio()
    {
        StopAudio();

        if (audioClip != null)
        {
            GameObject tempGO = new GameObject("AudioSourceTemp");
            AudioClipSO.tempAudioSource = tempGO.AddComponent<AudioSource>();
            AudioClipSO.tempAudioSource.PlayOneShot(audioClip, volumn);
            await Task.Delay(Mathf.CeilToInt(audioClip.length * 1000));
            StopAudio();
        }
    }

    [HorizontalGroup("clip")]
    [Button("停止")]
    public void StopAudio()
    {
        if (AudioClipSO.tempAudioSource != null)
        {
            AudioClipSO.tempAudioSource.Stop();
            GameObject.DestroyImmediate(AudioClipSO.tempAudioSource.gameObject);
            AudioClipSO.tempAudioSource = null;
        }
    }
#endif
}
