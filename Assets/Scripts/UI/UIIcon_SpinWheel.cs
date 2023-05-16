using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIcon_SpinWheel : MonoBehaviour
{
    [SerializeField] private GameObject[] lightOnArray;
    [SerializeField] private GameObject[] lightOffArray;
    [SerializeField] private Transform spinTran;


    private float timer = 0;
    private bool isSingle = false;
    private void Update()
    {
        if (spinTran != null)
            spinTran.Rotate(Vector3.forward * -30 * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer >= 0.5f)
        {
            timer -= 0.5f;
            isSingle = !isSingle;
            for (int i = 0; i < lightOnArray.Length; i++)
            {
                if (i % 2 == 0)
                {
                    lightOnArray[i].SetActive(isSingle);
                    lightOffArray[i].SetActive(!isSingle);
                }
                else
                {
                    lightOnArray[i].SetActive(!isSingle);
                    lightOffArray[i].SetActive(isSingle);
                }
            }
        }
    }
}
