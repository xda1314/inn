using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDropDown : MonoBehaviour
{
    private void Start()
    {
        var dropDown = GetComponent<Dropdown>();
        if (dropDown != null)
        {
            dropDown.onValueChanged.AddListener((int x) => { if (x != 0) dropDown.value = 0; });
        }
    }

}
