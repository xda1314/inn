using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTipSystem
{
    public static TextTipSystem Instance { get; } = new TextTipSystem();

    public Stack<UI_TextTip> textTipStack = new Stack<UI_TextTip>();

    public void ShowTip(Vector3 worldPos, string txt, TextTipColorType colorType)
    {
        //if (AssetSystem.Instance == null)
        //{
        //    return;
        //}

        //if (TutorialSystem.Instance == null || TutorialSystem.Instance.IsInTutorial)
        //{
        //    return;
        //}

        if (textTipStack.Count > 0)
        {
            UI_TextTip ui = textTipStack.Pop();
            if (ui != null)
            {
                ui.ShowText(worldPos, txt, colorType);
            }
        }
        else
        {
            AssetSystem.Instance.InstantiateAsync(Consts.UI_TextTip, UISystem.Instance.dialogRootTran, gO =>
            {
                gO.transform.position = worldPos;
                UI_TextTip ui = gO.GetComponent<UI_TextTip>();
                if (ui != null)
                {
                    ui.ShowText(worldPos, txt, colorType);
                }
                else
                {
                    GameObject.Destroy(gO);
                }
            });
        }


    }

    public void CycleTip(UI_TextTip ui)
    {
        ui.gameObject.SetActive(false);
        if (textTipStack.Count < 20)
        {
            textTipStack.Push(ui);
        }
        else
        {
            GameObject.Destroy(ui.gameObject);
        }
    }

}
