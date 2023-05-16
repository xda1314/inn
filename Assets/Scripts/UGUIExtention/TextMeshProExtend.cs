using IvyCore;
using System.Collections;
using TMPro;
using UnityEngine;
[DisallowMultipleComponent]
public class TextMeshProExtend : MonoBehaviour
{
    private TextMeshProUGUI _TMP;
    //private void Awake()
    //{
    //    _TMP = GetComponent<TextMeshProUGUI>();
    //    if (_TMP != null)
    //    {
    //        UI_Manager.Instance.RegisterRefreshEvent(this.gameObject, (str) =>
    //        {
    //            if (_TMP.gameObject.activeInHierarchy)
    //                StartCoroutine(ChageFont());
    //        }, "LanguageChanged");
    //    }
    //}

    //private void OnEnable()
    //{
    //    if (_TMP != null)
    //        StartCoroutine(ChageFont());
    //}

    //public IEnumerator ChageFont()
    //{
    //    if (_TMP == null)
    //        yield break;
    //    var fontName = "ANTQUAB";
    //    var lastFontName = "Alibaba-PuHuiTi-Regular";
    //    //if (LanguageManager.IsAsiaLanguage())
    //    //{
    //    //    fontName = "Alibaba-PuHuiTi-Regular";
    //    //    lastFontName = "ANTQUAB";
    //    //}

    //    if (_TMP.font.name.Equals(fontName + " SDF"))
    //        yield break;
    //    var fontMtName = _TMP.fontMaterial.name;
    //    fontMtName = fontMtName.Replace(lastFontName, fontName);
    //    if (fontMtName.Contains("(Instance)"))
    //        fontMtName = fontMtName.Substring(0, fontMtName.Length - 11);
    //    yield return null;
    //    AssetSystem.Instance.LoadAsset<TMP_FontAsset>(fontName + " SDF", (font) =>
    //    {
    //        _TMP.font = font;
    //    });

    //    AssetSystem.Instance.LoadAsset<Material>(fontMtName, (mt) =>
    //    {
    //        _TMP.fontMaterial = mt;
    //    });
    //}

    //private void OnDestroy()
    //{
    //    UI_Manager.Instance.UnRegisterRefreshEvent(this.gameObject);
    //}
}
