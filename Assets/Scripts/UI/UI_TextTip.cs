using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public enum TextTipColorType
{
    Green,
    Red,
    Yellow
}


public class UI_TextTip : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI t_Tip;
    public static Color GreenLbl = new Color(0, 1, 0.4310f);
    public static Color RedLbl = new Color(1, 0, 0.0947f);
    public static Color YellowLbl = new Color(1, 0.9254902f, 0.6313726f);

    private const float mapLeftWorldPosX = -2.4f;
    private const float mapRightWorldPosX = 2.4f;
    public void ShowText(Vector3 worldPos, string txt, TextTipColorType colorType)
    {
        transform.DOKill();
        t_Tip.text = txt;
        float tmpLength = t_Tip.preferredWidth / 100 * 0.44f / 2;
        float offsetX = 0;
        if (worldPos.x > 0)
        {
            offsetX = mapRightWorldPosX - worldPos.x < tmpLength ? tmpLength - (mapRightWorldPosX - worldPos.x) : 0;
            offsetX = -offsetX;
        }
        else
        {
            offsetX = worldPos.x - mapLeftWorldPosX < tmpLength ? tmpLength - (worldPos.x - mapLeftWorldPosX) : 0;
        }
        transform.position = new Vector3(worldPos.x + offsetX, worldPos.y, 0);

        t_Tip.color = new Color(t_Tip.color.r, t_Tip.color.g, t_Tip.color.b, 0);
        switch (colorType)
        {
            case TextTipColorType.Red:
                t_Tip.color = RedLbl;
                break;
            case TextTipColorType.Yellow:
                t_Tip.color = YellowLbl;
                break;
            case TextTipColorType.Green:
            default:
                t_Tip.color = GreenLbl;
                break;
        }
        gameObject.SetActive(true);
        Vector3 endPos = transform.localPosition + Vector3.up * 100;
        Vector3 endPos2 = transform.localPosition + Vector3.up * 200;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMoveY(endPos.y, 0.5f));
        sequence.Insert(0, DOTween.To((alpha) =>
        {
            t_Tip.color = new Color(t_Tip.color.r, t_Tip.color.g, t_Tip.color.b, alpha);
        }, 0, 1, 0.5f));
        sequence.AppendInterval(1.5f);
        sequence.Append(transform.DOLocalMoveY(endPos2.y, 0.5f));
        sequence.Insert(2f, DOTween.To((alpha) =>
        {
            t_Tip.color = new Color(t_Tip.color.r, t_Tip.color.g, t_Tip.color.b, alpha);
        }, 1, 0, 0.3f));
        sequence.onKill += () =>
        {
            //回收
            t_Tip.color = new Color(t_Tip.color.r, t_Tip.color.g, t_Tip.color.b, 0);
            TextTipSystem.Instance.CycleTip(this);
        };
        sequence.Play();
    }
}
