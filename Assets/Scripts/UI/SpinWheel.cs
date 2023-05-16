using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWheel : MonoBehaviour
{

    [SerializeField] GameObject refreshRouletteTime;
    [SerializeField] GameObject costFreeText;
    [SerializeField] GameObject costDiamontText;

    bool startCountDown;
    bool costFree=true;
    float hour, min, second;
    // Start is called before the first frame update
    void Start()
    {
        hour = 24 - System.DateTime.Now.Hour;
        min = 60 - System.DateTime.Now.Minute;
        second = 60 - System.DateTime.Now.Second;
        if (costFree)
        {
            refreshRouletteTime.transform.parent.gameObject.SetActive(!costFree);
            costFreeText.SetActive(costFree);
            costDiamontText.SetActive(!costFree);
        }
        else
        {
            refreshRouletteTime.transform.parent.gameObject.SetActive(!costFree);
            costFreeText.SetActive(costFree);
            costDiamontText.SetActive(!costFree);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startCountDown)
            CountDown();
    }

    public void PlayRouletteGame()
    {
        startCountDown = true;
        costFree = false;
        refreshRouletteTime.transform.parent.gameObject.SetActive(true);
        costFreeText.SetActive(costFree);
        costDiamontText.SetActive(!costFree);
        //UIManager.Instance.OpenUIPanel(Consts.UI_ShowRoulette, UILayer.Normal, ShowUIType.RefreshSame);
    }

    private void CountDown()
    {
        hour = 24 - System.DateTime.Now.Hour;
        min = 60 - System.DateTime.Now.Minute;
        second = 60 - System.DateTime.Now.Second;
        if (second == 0)
        {
            min--;
        }
        if (min == 0)
        {
            hour--;
        }
        if (hour == 0)
        {
            costFree = true;
            costFreeText.SetActive(costFree);
            costDiamontText.SetActive(!costFree);
        }
        //refreshRouletteTime.GetComponent<UILabel>().text = hour + "h:" + min + "m";
    }
}
