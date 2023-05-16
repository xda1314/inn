using DG.Tweening;
using IvyCore;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel_Jigsaw : UIPanelBase
{
    [Header("Jigsaw")]
    [SerializeField] Transform jigsawSpineRoot;
    [SerializeField] Transform jiasawBGRoot;
    [SerializeField] Transform jigsawRoot;
    [SerializeField] Button btn_Continue;
    [SerializeField] TextMeshProUGUI t_Continue;
    [SerializeField] TextMeshProUGUI t_Episode;
    [Header("New Chapter")]
    [SerializeField] Transform newChapterRoot;
    [SerializeField] Transform trans_Spine;
    [SerializeField] TextMeshProUGUI t_Title;
    [SerializeField] TextMeshProUGUI t_UnlockLevel;

    [SerializeField] Button btn_text;
    [SerializeField] SkeletonGraphic spine_Paper;

    bool canPointBtn = false;
    string curFinalTaskId;
    List<Image> jigsawImgList = new List<Image>();//存储所有拼图
    List<Image> jigsawBgList = new List<Image>();//存储所有拼图背景
    bool isCompleteChapter;

    Dictionary<string, GameObject> saveBGSpineDic = new Dictionary<string, GameObject>();
    GameObject saveChapterSpine;
    private void PlayPaperSpine(int index) 
    {
        spine_Paper.AnimationState.SetAnimation(0, "sizhi" + index.ToString() + "_b", false);      
        StartCoroutine(sss(index));
    }
    IEnumerator sss(int index) 
    {
        yield return new WaitForSeconds(3f);
        string name = "sizhi" + index.ToString();
        spine_Paper.AnimationState.SetAnimation(0, name, false);
    }
    public override void OnInitUI()
    {
        base.OnInitUI();
        int index = 1;
        btn_text.onClick.AddListener(() =>
        {
            //index++;
            PlayPaperSpine(index);
        });



        btn_Continue.onClick.AddListener(() =>
        {
            IvyCore.UI_Manager.Instance.InvokeRefreshEvent("", "page_Play_RefreshChapterChest");
            if (canPointBtn) 
            {
                UISystem.Instance.HideAllUI();
                UISystem.Instance.uiMainMenu.ShowEvaluateView();
            }             
        });
        TryRefreshBGSpine(true);      
        Transform[] jigsawTrans = jigsawRoot.GetComponentsInChildren<Transform>();
        for (int i = 0; i < jigsawTrans.Length; i++)
        {
            if (jigsawTrans[i].gameObject.name != "JigsawRoot")
            {
                jigsawImgList.Add(jigsawTrans[i].GetComponent<Image>());
            }
        }
        Transform[] jigsawBGTrans = jiasawBGRoot.GetComponentsInChildren<Transform>();
        for (int i = 0; i < jigsawBGTrans.Length; i++)
        {
            if (jigsawBGTrans[i].gameObject.name != "JigsawBGRoot")
            {
                jigsawBgList.Add(jigsawBGTrans[i].GetComponent<Image>());
            }
        }
        newChapterRoot.transform.localScale = Vector3.zero;
        newChapterRoot.transform.parent.gameObject.SetActive(false);
    }
    public override IEnumerator OnShowUI()
    {
        canPointBtn = false;
        RefreshJigsaw();
        TryRefreshBGSpine(false);
        yield return base.OnShowUI();
    }
    public override IEnumerator OnHideUI()
    {
        yield return base.OnHideUI();
        t_Episode.transform.localPosition = new Vector3(0, 1400, 0);
        btn_Continue.transform.localPosition = new Vector3(0, -1400, 0);
        newChapterRoot.transform.localScale = Vector3.zero;
        newChapterRoot.transform.parent.gameObject.SetActive(false);
    }



    private void RefreshJigsaw()
    {
        //此时已切关,所以关卡finalTaskId是上一关的
        curFinalTaskId = TaskGoalsManager.Instance.IsCompleteAllChapter ? TaskGoalsManager.Instance.curLevelIndex.ToString() + "_final" : (TaskGoalsManager.Instance.curLevelIndex - 1).ToString() + "_final";   
        List<string> taskIdList = new List<string>();//该章节所有的final taskId
        foreach (var item in TaskGoalsDefinition.ChapterDataDic)
        {
            if (item.Value.taskIdList.Contains(curFinalTaskId))
            {
                taskIdList = item.Value.taskIdList;
                break;
            }
        }
        //刷新ui
        isCompleteChapter = curFinalTaskId == taskIdList[taskIdList.Count - 1];//是否完成一个章节
        t_Continue.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/Home_Button1");
        t_Episode.text = isCompleteChapter ? I2.Loc.ScriptLocalization.Get("Obj/Chain/OrdersDescribe3") : I2.Loc.ScriptLocalization.Get("Obj/Chain/OrdersDescribe2");
        //拼图先全部显示
        for (int i = 0; i < jigsawImgList.Count; i++)
        {
            jigsawImgList[i].color = new Color(1, 1, 1, 1);
        }
        //再隐藏
        for (int i = 0; i < taskIdList.Count; i++)
        {
            if (taskIdList[i] == curFinalTaskId) //刚解锁的拼图
            {
                if (TaskGoalsDefinition.TaskDefinitionsDict.TryGetValue(taskIdList[i], out TaskData taskData))
                {
                    int index = 0;
                    List<Image> bgList = new List<Image>();//刚解锁的拼图背景
                    for (int j = 0; j < taskData.taskDefinition.UnLockAreaIndexList.Count; j++)
                    {
                        StartCoroutine(HideJigsawTween(jigsawImgList[taskData.taskDefinition.UnLockAreaIndexList[j] - 1], index));
                        bgList.Add(jigsawBgList[taskData.taskDefinition.UnLockAreaIndexList[j] - 1]);
                        index++;
                    }
                    StartCoroutine(TweenAfterHideJigsaw(taskData.taskDefinition.UnLockAreaIndexList.Count, bgList));
                }
                break;
            }
            else //隐藏已解锁的拼图
            {
                if (TaskGoalsDefinition.TaskDefinitionsDict.TryGetValue(taskIdList[i], out TaskData taskData))
                {
                    for (int j = 0; j < taskData.taskDefinition.UnLockAreaIndexList.Count; j++)
                    {
                        jigsawImgList[taskData.taskDefinition.UnLockAreaIndexList[j] - 1].color = new Color(1, 1, 1, 0);
                    }
                }
            }
        }
    }
    private IEnumerator HideJigsawTween(Image img, int index)
    {
        yield return new WaitForSeconds(0.5f);//延时0.5s后开始隐藏拼图
        yield return new WaitForSeconds(0.05f * index);//每隔0.05s隐藏一个拼图
        DOTween.To(() => img.color.a, value => img.color = new Color(1, 1, 1, value), 0, 0.3f);
    }
    private IEnumerator TweenAfterHideJigsaw(int jigsawNum, List<Image> bgList) 
    {
        float twinkleDelay = jigsawNum * 0.05f + 1f;
        yield return new WaitForSeconds(twinkleDelay);

        for (int i = 0; i < bgList.Count; i++)
        {
            DOTween.To(() => bgList[i].color.a, value =>
            {
                bgList[i].color = new Color(1, 1, 1, value);
            }, 0, 0.2f);

            Sequence sequence = DOTween.Sequence();
            sequence.Append(bgList[i].DOColor(new Color(1, 1, 1, 0), 0.4f));
            sequence.Insert(0.4f, bgList[i].DOColor(new Color(1, 1, 1, 1), 0.4f));
            sequence.Insert(0.8f, bgList[i].DOColor(new Color(1, 1, 1, 0), 0.4f));
            sequence.Insert(1.2f, bgList[i].DOColor(new Color(1, 1, 1, 1), 0.4f));
            sequence.Play();
        }
        Invoke("JigsawDescAndBtnTween", 1.6f);
        if (isCompleteChapter)
        {
            Invoke("NewChapterViewTween", 2.5f);
        }
    }
    private void JigsawDescAndBtnTween()
    {
        if (isCompleteChapter)
        {
            btn_Continue.transform.DOLocalMoveY(-800, 0.7f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                canPointBtn = true;
            });
        }
        else 
        {
            t_Episode.transform.DOLocalMoveY(800, 0.7f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                btn_Continue.transform.DOLocalMoveY(-800, 0.7f).SetEase(Ease.OutQuad).OnComplete(() =>
                {
                    canPointBtn = true;
                });
            });
        }     
    }
    private void NewChapterViewTween()
    {
        if (!newChapterRoot.parent.gameObject.activeSelf) 
        {
            newChapterRoot.parent.gameObject.SetActive(true);
        }
        if (saveChapterSpine) 
        {
            Destroy(saveChapterSpine);
        }
        
        t_Title.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/OrdersDescribe4");
        if (TaskGoalsDefinition.TaskDefinitionsDict.TryGetValue(curFinalTaskId, out TaskData taskData))
        {
            if (TaskGoalsDefinition.ChapterDataDic.TryGetValue(taskData.taskDefinition.previewChapterPic, out ChapterData chapterData))
            {
                t_UnlockLevel.gameObject.SetActive(true);
                t_UnlockLevel.text = string.Format(I2.Loc.ScriptLocalization.Get("Obj/Chain/OrdersDescribe5"), chapterData.minLevel, chapterData.maxLevel);
            }
            else 
            {
                t_UnlockLevel.gameObject.SetActive(false);
            }
            AssetSystem.Instance.LoadAsset<SkeletonDataAsset>(taskData.taskDefinition.previewChapterPic, (skeletonDataAsset) =>
            {
                AssetSystem.Instance.LoadAsset<Material>("SkeletonGraphicDefault", (material) =>
                {
                    SkeletonGraphic skeleton = SkeletonGraphic.NewSkeletonGraphicGameObject(skeletonDataAsset, trans_Spine, material);
                    if (skeleton != null)
                    {
                        saveChapterSpine = skeleton.gameObject;
                        skeleton.AnimationState.SetAnimation(0, "animation", false);
                        skeleton.timeScale = 0;
                    }
                });
            });

        }

        ChooseSkinSystem.Instance.SetSkinByName(taskData.taskDefinition.previewChapterPic);
        newChapterRoot.transform.parent.gameObject.SetActive(true);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(newChapterRoot.DOScale(Vector3.one * 1.2f, 0.5f).SetEase(Ease.OutQuad));
        sequence.Insert(0.5f, newChapterRoot.DOScale(Vector3.one, 0.2f).SetEase(Ease.InQuad));
        sequence.Play();
    }


    private void TryRefreshBGSpine(bool isInit) 
    {
        if (isInit)
        {
            if (TaskGoalsDefinition.taskLevelDataDic.TryGetValue(TaskGoalsManager.Instance.curLevelIndex, out TaskLevelData taskLevelData))
            {
                AssetSystem.Instance.LoadAsset<SkeletonDataAsset>(taskLevelData.previewPic, (skeletonDataAsset) =>
                {
                    AssetSystem.Instance.LoadAsset<Material>("SkeletonGraphicDefault", (material) =>
                    {
                        SkeletonGraphic skeleton = SkeletonGraphic.NewSkeletonGraphicGameObject(skeletonDataAsset, jigsawSpineRoot, material);
                        if (skeleton != null)
                        {
                            saveBGSpineDic.Add(taskLevelData.previewPic, skeleton.gameObject);
                            skeleton.AnimationState.SetAnimation(0, "animation", false);
                            skeleton.timeScale = 0;
                        }
                    });
                });
            }
        }
        else 
        {
            if (TaskGoalsDefinition.TaskDefinitionsDict.TryGetValue(curFinalTaskId, out TaskData taskData))
            {
                if (saveBGSpineDic.ContainsKey(taskData.taskDefinition.previewChapterPic))
                {
                    return;
                }
                saveBGSpineDic.Clear();
                AssetSystem.Instance.LoadAsset<SkeletonDataAsset>(taskData.taskDefinition.previewChapterPic, (skeletonDataAsset) =>
                {
                    AssetSystem.Instance.LoadAsset<Material>("SkeletonGraphicDefault", (material) =>
                    {
                        SkeletonGraphic skeleton = SkeletonGraphic.NewSkeletonGraphicGameObject(skeletonDataAsset, jigsawSpineRoot, material);
                        if (skeleton != null)
                        {
                            saveBGSpineDic.Add(taskData.taskDefinition.previewChapterPic, skeleton.gameObject);
                            skeleton.AnimationState.SetAnimation(0, "animation", false);
                            skeleton.timeScale = 0;
                        }
                    });
                });
            }
        }
    }
}
