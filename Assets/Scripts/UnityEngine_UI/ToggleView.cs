using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class ToggleView : MonoBehaviour
{
    [SerializeField] List<Toggle> toggleList = new List<Toggle>();
    [SerializeField] private Transform parent;
    [SerializeField] private ToggleGroup toggleGroup;

    private AutoScrollWindow autoScrollWindow;
    private Toggle toggle;
    private int pageCount;
    private int nowPage;

    public void RefreshToggles(AutoScrollWindow autoScrollWindow1, int count)
    {
        autoScrollWindow = autoScrollWindow1;
        pageCount = count;
        int index = 0;

        for (; index < pageCount; index++)
        {
            InitToggle(index);
        }

        for (; index < toggleList.Count; index++)
        {
            toggleList[index].gameObject.SetActive(false);
        }
        autoScrollWindow.OnIndexChange = null;
        autoScrollWindow.OnIndexChange = new Action<int>(ShowToggle);
        
    }

    public void InitToggle(int index)
    {
        toggle = toggleList[index].GetComponent<Toggle>();
        toggle.gameObject.SetActive(true);
        toggle.group = toggleGroup;
        toggle.gameObject.name = index.ToString();
        toggle.onValueChanged.AddListener(ifselect =>
        {
            if (ifselect) ChangePage(ifselect);
        });
    }

    public void ShowToggle(int nowPage)
    {
        for (int i = 0; i < pageCount; i++)
        {
            toggleList[i].isOn = (nowPage == i);
        }
    }

    private void ChangePage(bool arg0)
    {
        if (arg0)
        {
            toggle = toggleGroup.ActiveToggles().FirstOrDefault();
            int index = int.Parse(toggle.gameObject.name);
            autoScrollWindow.MoveToIndex(index);
            autoScrollWindow.ClearTime();
        }
    }

}