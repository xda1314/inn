using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UIPanelData_Download : UIPanelDataBase
{
    public long startDownloadSize;
    public string downloadKey;
    public Action downloadSuccessCB;
    public Action downloadFailCB;

    public UIPanelData_Download(
        long startDownloadSize,
        string downloadKey,
        Action downloadSuccessCB,
        Action downloadFailCB) : base(Consts.UIPanel_Download)
    {
        this.startDownloadSize = startDownloadSize;
        this.downloadKey = downloadKey;
        this.downloadSuccessCB = downloadSuccessCB;
        this.downloadFailCB = downloadFailCB;
    }
}

/// <summary>
/// 通用弹窗
/// </summary>
public class UIPanel_Download : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI tmp_Title;
    [SerializeField] private TextMeshProUGUI tmp_Desc;

    [SerializeField] private Button btn_Close;
    [SerializeField] private Button btn_Download;
    [SerializeField] private TextMeshProUGUI tmp_Download;
    [SerializeField] private Button btn_InProgress;
    [SerializeField] private TextMeshProUGUI tmp_InProgress;

    [SerializeField] private Slider slider_Download;
    [SerializeField] private TextMeshProUGUI tmp_DownloadProgress;

    private UIPanelData_Download panelData;


    public override void OnInitUI()
    {
        base.OnInitUI();

        isDownloading = false;
        btn_Close.onClick.AddListener(() =>
        {
            UISystem.Instance.HideUI(this);
        });
        btn_Download.onClick.AddListener(OnBtnOKClick);
    }

    public override IEnumerator OnShowUI()
    {
        tmp_Title.text = I2.Loc.ScriptLocalization.Get("Obj/Download/Text1");
        tmp_Desc.text = I2.Loc.ScriptLocalization.Get("Obj/Download/Text2");
        tmp_Download.text = I2.Loc.ScriptLocalization.Get("Obj/Download/Text3");
        tmp_DownloadProgress.text = I2.Loc.ScriptLocalization.Get("Obj/Download/Text4");

        yield return base.OnShowUI();

        if (UIPanelData == null)
            yield break;
        panelData = UIPanelData as UIPanelData_Download;
        if (panelData == null)
            yield break;

        if (AssetSystem.Instance.IsAssetDownloading(panelData.downloadKey, out downloadhandle))
        {
            isDownloading = true;
            btn_Download.gameObject.SetActive(false);
            btn_InProgress.gameObject.SetActive(true);
            UpdateSliderProgress(downloadhandle);
        }
        else
        {
            isDownloading = false;
            btn_Download.gameObject.SetActive(true);
            btn_InProgress.gameObject.SetActive(false);

            slider_Download.value = 0.1f;
            if (panelData.startDownloadSize >= 0)
                tmp_DownloadProgress.text = "0 / " + String.Format("{0:N2}", panelData.startDownloadSize / (1024 * 1024f)) + "MB";
            else
                tmp_DownloadProgress.text = "0 / ?MB";
        }

        yield break;
    }

    private void UpdateSliderProgress(AsyncOperationHandle downloadhandle)
    {
        if (downloadhandle.IsValid())
        {
            DownloadStatus status = downloadhandle.GetDownloadStatus();
            slider_Download.value = status.Percent < 0.1 ? 0.1f : status.Percent;
            tmp_DownloadProgress.text = String.Format("{0:N2}", status.DownloadedBytes / (1024 * 1024f)) + " / " + String.Format("{0:N2}", status.TotalBytes / (1024 * 1024f)) + "MB";
        }
    }

    private void OnBtnOKClick()
    {
        if (panelData == null)
            return;

        string downloadKey = panelData.downloadKey;
        downloadhandle = AssetSystem.Instance.DownloadDependenciesAsync(downloadKey, () =>
        {
            // 下载成功
            panelData.downloadSuccessCB?.Invoke();
            if (UISystem.Instance.TryGetUI<UIPanel_Download>(Consts.UIPanel_Download, out var uIPanel_Download))
            {
                if (uIPanel_Download.panelData != null && uIPanel_Download.panelData.downloadKey == downloadKey)
                {
                    UISystem.Instance.HideUI(uIPanel_Download);
                }
            }
        }, () =>
        {
            // 下载失败
            panelData.downloadFailCB?.Invoke();
        });
        isDownloading = true;
    }

    private AsyncOperationHandle downloadhandle;
    private bool isDownloading = false;
    private float timer = 0.1f;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.2f)
        {
            timer = 0;
            if (isDownloading)
            {
                UpdateSliderProgress(downloadhandle);
            }
        }
    }


}
