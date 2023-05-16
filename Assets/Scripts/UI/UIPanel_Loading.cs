using ivy.game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Ivy;
using System;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UIPanel_Loading : UIPanelBase
{
    [Header("组件")]
    [SerializeField] private Slider slider_loading;
    [SerializeField] private TextMeshProUGUI text_loading;
    [SerializeField] private TextMeshProUGUI text_chat;
    [SerializeField] private Image Image_GameName;
    [SerializeField] private ImageSpritesContainer SpritesContainer;

    [SerializeField] private Slider slider_Downloading;
    [SerializeField] private TextMeshProUGUI t_DownloadValue;
    [Header("变量")]
    private float timer = 0;

    private void Start()
    {
        text_loading.text = I2.Loc.ScriptLocalization.Get("Obj/Name/LoadingText1");
        string language = LanguageManager.CurrentLangaugeCode;
        Sprite sprite = SpritesContainer.GetSprite(language);
        if (sprite == null)
        {
            sprite = SpritesContainer.GetSprite("en");
        }
        if (sprite != null)
            Image_GameName.sprite = sprite;
        Image_GameName.SetNativeSize();
        ChangeChat();
        CheckNotch();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5)
        {
            ChangeChat();
            timer = 0;
        }
    }

    private void ChangeChat()
    {
        int randNum = UnityEngine.Random.Range(1, 6);
        text_chat.text = I2.Loc.ScriptLocalization.Get($"Obj/Desc/Loading/Text{randNum}");
    }

    public void SetLoadingProcess(float value, float duration = 2f)
    {
        slider_loading.DOKill();
        if (duration <= 0)
        {
            slider_loading.value = value;
        }
        else
        {
            slider_loading.DOValue(value, duration);
        }
    }

    //暂时加个UI 刘海屏适配  TODO 修改

    private void CheckNotch()
    {
        if (RiseSdk.Instance.HasNotch())
        {
            float addDis = Screen.height * 0.04f;
#if UNITY_ANDROID
            int notchHeight = RiseSdk.Instance.GetNotchHeight();
            addDis = addDis < notchHeight ? notchHeight : addDis;
#endif
#if UNITY_IOS
                    addDis = 90f;
#endif
            GameManager.Instance.MNotchAdaptY_ = addDis;
        }

    }
    public void ShowDownloadSlider(bool show)
    {
        slider_Downloading.gameObject.SetActive(show);
        slider_loading.gameObject.SetActive(!show);
    }
    public void SetDownloadSliderValue(AsyncOperationHandle downloadhandle)
    {
        if (downloadhandle.IsValid())
        {
            DownloadStatus status = downloadhandle.GetDownloadStatus();
            slider_Downloading.value = status.Percent < 0.05 ? 0.05f : status.Percent;
            t_DownloadValue.text = String.Format("{0:N2}", status.DownloadedBytes / (1024 * 1024f)) + " / " + String.Format("{0:N2}", status.TotalBytes / (1024 * 1024f)) + "MB";
        }
    }
}
