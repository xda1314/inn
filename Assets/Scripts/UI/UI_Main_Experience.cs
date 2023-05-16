using TMPro;
using UnityEngine;
using UnityEngine.UI;
using IvyCore;

public class UI_Main_Experience : MonoBehaviour
{
    [SerializeField] private Slider slider_exp;
    [SerializeField] private TextMeshProUGUI lbl_exp;
    [SerializeField] private TextMeshProUGUI text_empirica;
    [SerializeField] private ParticleSystem effect_ExpFull;
    public void SetSlider()
    {
        if (GameManager.Instance.playerData == null)
            return;
        lbl_exp.text = GameManager.Instance.playerData.CurrentExpLevel.ToString();
        bool isExpFull = GameManager.Instance.playerData.UnCollectedExp >= GameManager.Instance.playerData.NextExpLevelNeedExp;
        effect_ExpFull.gameObject.SetActive(isExpFull);
        if (slider_exp != null)
        {
            text_empirica.text = isExpFull ? GameManager.Instance.playerData.NextExpLevelNeedExp.ToString() + "/" + GameManager.Instance.playerData.NextExpLevelNeedExp.ToString() :
                GameManager.Instance.playerData.UnCollectedExp.ToString() + "/" + GameManager.Instance.playerData.NextExpLevelNeedExp.ToString();
            slider_exp.value = isExpFull ? 1 : (float)GameManager.Instance.playerData.UnCollectedExp / GameManager.Instance.playerData.NextExpLevelNeedExp;
        }
    }
}
