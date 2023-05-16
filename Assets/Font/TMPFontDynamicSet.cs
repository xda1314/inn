using Sirenix.OdinInspector;
using System;
using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(TMP_Text))]
public class TMPFontDynamicSet : MonoBehaviour
{
    [ReadOnly, SerializeField] public TMP_Text _TMP_text;

    public const string TMPFontAssetKey_normal = "TMPFont_Normal";
    public const string TMPFontAssetKey_normalBold = "TMPFont_NormalBold";
    public const string TMPFontAssetKey_ko = "TMPFont_ko";
    public const string TMPFontAssetKey_cn = "TMPFont_cn";

    //private TMP_FontAsset _originfontAsset;
    //private Material _originFontSharedMaterial;
    //private string _currentFontAsset = "normal";

    //private static TMP_FontAsset tmpFontAsset_ko;
    //private static TMP_FontAsset tmpFontAsset_cn;


    //private void Awake()
    //{
    //    if (_TMP_text == null)
    //    {
    //        _TMP_text = GetComponent<TMP_Text>();
    //    }
    //    if (_TMP_text != null)
    //    {
    //        _originfontAsset = _TMP_text.font;
    //        _originFontSharedMaterial = _TMP_text.fontSharedMaterial;
    //        _currentFontAsset = "normal";
    //    }
    //    else
    //    {
    //        GameDebug.LogError("没有找到TMP字体组件");
    //    }
    //}

    //private void Start()
    //{
    //    ChangeFontAsset();
    //    LanguageManager.OnLanguageChangeFinishEvent += ChangeFontAsset;
    //}

    //private void OnDestroy()
    //{
    //    LanguageManager.OnLanguageChangeFinishEvent -= ChangeFontAsset;
    //}

    //public static void TryLoadFontAsset_ko(Action finishCB)
    //{
    //    AssetSystem.Instance.LoadAssetAsync<TMP_FontAsset>(TMPFontAssetKey_ko, (TMP_FontAsset asset) =>
    //    {
    //        tmpFontAsset_ko = asset;
    //        finishCB();
    //    });
    //}

    //public static void TryLoadFontAsset_cn(Action finishCB)
    //{
    //    AssetSystem.Instance.LoadAssetAsync<TMP_FontAsset>(TMPFontAssetKey_cn, (TMP_FontAsset asset) =>
    //    {
    //        tmpFontAsset_cn = asset;
    //        finishCB();
    //    });
    //}

    //private void ChangeFontAsset()
    //{
    //    if (_TMP_text != null)
    //    {
    //        // 判断字体是否需要更换
    //        if (LanguageManager.IsDownloadFont_KO(LanguageManager.CurrentLangaugeCode, out string downloadKey1))
    //        {
    //            // 加载新字体
    //            if (_currentFontAsset != downloadKey1)
    //            {
    //                if (tmpFontAsset_ko == null)
    //                {
    //                    AssetSystem.Instance.LoadAssetAsync<TMP_FontAsset>(downloadKey1, (TMP_FontAsset asset) =>
    //                    {
    //                        tmpFontAsset_ko = asset;
    //                        if (tmpFontAsset_ko != null)
    //                        {
    //                            _TMP_text.font = tmpFontAsset_ko;
    //                            _currentFontAsset = downloadKey1;
    //                        }
    //                    });
    //                }
    //                else
    //                {
    //                    _TMP_text.font = tmpFontAsset_ko;
    //                    _currentFontAsset = downloadKey1;
    //                }
    //            }
    //        }
    //        else if (LanguageManager.IsDownloadFont_CN(LanguageManager.CurrentLangaugeCode, out string downloadKey2))
    //        {
    //            // 加载新字体
    //            if (_currentFontAsset != downloadKey2)
    //            {
    //                if (tmpFontAsset_cn == null)
    //                {
    //                    AssetSystem.Instance.LoadAssetAsync<TMP_FontAsset>(downloadKey2, (TMP_FontAsset asset) =>
    //                    {
    //                        tmpFontAsset_cn = asset;
    //                        if (tmpFontAsset_cn != null)
    //                        {
    //                            _TMP_text.font = tmpFontAsset_cn;
    //                            _currentFontAsset = downloadKey2;
    //                        }
    //                    });
    //                }
    //                else
    //                {
    //                    _TMP_text.font = tmpFontAsset_cn;
    //                    _currentFontAsset = downloadKey2;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            if (_currentFontAsset != "normal")
    //            {
    //                if (_originfontAsset != null && _originFontSharedMaterial != null)
    //                {
    //                    _TMP_text.font = _originfontAsset;
    //                    _TMP_text.fontSharedMaterial = _originFontSharedMaterial;
    //                    _currentFontAsset = "normal";
    //                }
    //                else
    //                {
    //                    GameDebug.LogError("没有找到默认字体设置");
    //                }
    //            }
    //        }

    //    }
    //}

    private void OnValidate()
    {
        if (_TMP_text == null)
        {
            _TMP_text = GetComponent<TMP_Text>();
        }
    }
}
