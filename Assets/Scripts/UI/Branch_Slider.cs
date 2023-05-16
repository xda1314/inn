using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Branch_Slider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI t_Slider;

    private MergeLevelType curLevelType;
    // Start is called before the first frame update
    private float pos_y;
    
    private void Start()
    {
        pos_y = slider.transform.localPosition.y;
        gameObject.SetActive(false);
    }

    public void RefreshUI(bool is_finish) 
    {
        int branchPoint;
        Vector2Int vec2;
        branchPoint = BranchSystem.Instance.branchPoint;
        vec2 = BranchSystem.Instance.GetPointProgress(branchPoint);
        t_Slider.text = vec2.x.ToString() + "/" + vec2.y.ToString();
        var val = (float)vec2.x / vec2.y;
        if (is_finish && val == 0) 
        {
            val = 1 ;
            t_Slider.text = vec2.y.ToString() + "/" + vec2.y.ToString();
        }  
        DOTween.To(() => slider.value, x => slider.value = x, val, 0.2f);
    }

    public void DoAniIN() 
    {
        transform.DOLocalMoveY(pos_y-400, 0.3f);
        gameObject.SetActive(true);
    }

    public void DoAniOut() 
    {
        transform.DOLocalMoveY(pos_y, 0.3f).SetDelay(1.1f).OnComplete(()=> 
        {
            gameObject.SetActive(false);
        });
    }

}
