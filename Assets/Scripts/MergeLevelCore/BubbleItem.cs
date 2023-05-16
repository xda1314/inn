using Coffee.UIExtensions;
using DG.Tweening;
using IvyCore;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BubbleItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private const int Max_x = 460;//540-80
    //private const int Max_y = 490;
    private const int Min_x = -460;
    //private const int Min_y = -490;
    [SerializeField] private Transform _rootTrans;
    [SerializeField] private GameObject _itemObj;
    [SerializeField] private UIParticle _bubble;
    private UIParticle _bubbleBroke;
    private Rigidbody2D _rigidbody;
    private Vector3 _lastPos;
    private bool hasStart = false;

    private MergeController mergeController;
    public MergeItemData ItemData { get; private set; }

    // Start is called before the first frame update
    private void Start()
    {
        hasStart = true;
        this.TryGetComponent<Rigidbody2D>(out _rigidbody);
        InitUI();
    }

    public void InitWithData(MergeItemData data, MergeController control)
    {
        ItemData = data;
        mergeController = control;
        data.SetData_BubbleGO(this);
        if (hasStart)
            InitUI();
    }

    private void InitUI()
    {
        _lastPos = transform.localPosition;
        ShowBubbleParticle();
        AssetSystem.Instance.DestoryGameObject("", _itemObj);
        _itemObj = AssetSystem.Instance.Instantiate(ItemData.Definition.PrefabName, this.transform);
        _itemObj.transform.localPosition = Vector3.zero;
        _itemObj.transform.localScale = Vector3.one;
        _rigidbody.velocity = new Vector2(Random.Range(50, 130) / 10.0f, Random.Range(50, 130) / 10.0f);
    }

    // Update is called once per frame
    private bool _readyToFloat = false;
    private float _readyFloatInterval = 0;
    private float _checkInterval = 0;
    void Update()
    {
        //元素图片与父节点保持反选择
        var rotate = this.transform.localRotation.eulerAngles;
        _itemObj.transform.localRotation = Quaternion.Euler(Vector3.forward * rotate.z * -1);

        //检查泡泡是否移动
        _checkInterval += Time.deltaTime;
        if (_checkInterval > 1.0f)
        {
            _checkInterval = 0;
            if (!_onDragging)
            {
                _readyToFloat = (transform.localPosition - _lastPos).sqrMagnitude < 0.3f;
                _lastPos = transform.localPosition;
            }
        }
        //泡泡未移动时，准备做悬浮动画
        if (_readyToFloat)
        {
            _readyFloatInterval += Time.deltaTime;
            if (_readyFloatInterval > 0.5f)
            {
                _readyFloatInterval = 0;
                BubbleFloat();
            }
        }

        //检测泡泡生命周期
        if (ExtensionTool.IsDateSmallerThanNow(ItemData.bubbleDieTime))
        {
            if (ReferenceEquals(mergeController.currentSelectItemData, ItemData))
            {
                mergeController.HideWithTween_BoxSelected();
                mergeController.SetCurrentSelectData(null);
            }
            MergeLevelManager.Instance.CurrentMapData.RemoveDataFromBubbleList(ItemData);
            //倒计时结束未花钱解锁，产出金币并破灭消失
            if (!string.IsNullOrEmpty(ItemData.Definition.BubbleDieOutputPrefab))
                CreateItemFromBubble(ItemData.Definition.BubbleDieOutputPrefab);
            //破灭动画
            PlayBubbleBrokeEffect();
        }
    }

    private void CreateItemFromBubble(string prefabName, bool selected = false)
    {
        Vector2Int v2Int = new Vector2Int(1, 1);//初始位置
        //查找最近空格
        if (mergeController.ConvertLocalPositionToGrid(transform.position, out Vector2Int gridPos)
            && mergeController.TryGetNearEmptyGrid(gridPos, out var emptyPos, false))
        {
            mergeController.CreateItem_FlyToGrid(prefabName, transform.position, emptyPos, 0, false, selected);
        }
        else if(mergeController.TryGetNearEmptyGrid(v2Int, out var emptyPos2, false))//从左上角开始飞
        {
            mergeController.CreateItem_FlyToGrid(prefabName, transform.position, emptyPos2, 0, false, selected);
        }
        else//格子满了放入临时背包
        {
            var mergeRewardItem = new MergeRewardItem
            {
                name = prefabName,
                num = 1
            };
            RewardBoxManager.Instance.AddRewardItem(MergeLevelManager.Instance.CurrentLevelType, mergeRewardItem, false);
            ShowAddToRewardBoxTween(prefabName);
        }
    }

    public void UnlockBubble()
    {
        mergeController.SetCurrentSelectData(null);
        MergeLevelManager.Instance.CurrentMapData.RemoveDataFromBubbleList(ItemData);
        CreateItemFromBubble(ItemData.PrefabName, true);
        PlayBubbleBrokeEffect();
    }

    private void ShowAddToRewardBoxTween(string prefabName)
    {
        var item = AssetSystem.Instance.Instantiate(prefabName, this.transform.parent);
        item.transform.localScale = Vector3.one * 0.5f;
        item.transform.position = this.transform.position;

        var targetPos = mergeController.RewardBoxTrans.position;
        float dis = Vector3.Distance(targetPos, item.transform.position);
        float durationTime = 0.4f + dis * 0.1f;
        Vector2 p0 = item.transform.position;
        Vector2 p2 = targetPos;
        Vector2 p1 = new Vector2(Vector2.Lerp(p0, p2, 0.5f).x, item.transform.position.y + dis);
        var posz = item.transform.position.z;

        var seq = DOTween.Sequence();
        seq.Append(DOTween.To(setter: value =>
        {
            Vector2 vector = DoAnimTools.Bezier(p0, p1, p2, value);
            item.transform.position = new Vector3(vector.x, vector.y, posz);
        }, startValue: 0, endValue: 1, duration: durationTime).SetEase(Ease.OutQuad));
        seq.Insert(0, item.transform.DOScale(Vector3.one, durationTime - 0.4f).SetEase(Ease.OutQuad));

        seq.onComplete = () =>
        {
            AssetSystem.Instance.DestoryGameObject(prefabName, item);
            mergeController.RefreshRewardBox();
        };
        seq.Play();
    }

    private void PlayBubbleBrokeEffect()
    {
        StopAllBubbleStates();
        if (_bubbleBroke == null)
        {
            var obj = AssetSystem.Instance.Instantiate(Consts.BubbleBroke_Effect, this.transform);
            obj.transform.localPosition = Vector3.zero;
            _bubbleBroke = obj.GetComponent<UIParticle>();
        }
        else
            _bubbleBroke.gameObject.SetActive(true);
        if (_bubbleBroke != null)
        {
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Break_Bubble);
            _bubbleBroke.Play();
        }
        StartCoroutine(WaitToUnspawnBubble());
    }

    private IEnumerator WaitToUnspawnBubble()
    {
        _itemObj.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InSine);
        yield return new WaitForSeconds(0.8f);
        _bubbleBroke.gameObject.SetActive(false);
        AssetSystem.Instance.UnspawnBubbleItem(this);
    }

    #region 事件系统
    private bool _onDragging = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        _rigidbody.velocity = Vector2.zero;
        if (UI_TutorManager.Instance.IsTutoring() && ItemData.GridPos != UI_TutorManager.Instance.curMergeClickPos)
            return;
        if (eventData.dragging)
            return;

        mergeController.ResetTipCountDown();

        mergeController.HideWithTween_BoxSelected();

        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Tap_Eliminate);
        mergeController.SetCurrentSelectData(ItemData);
    }

    private Vector3 _lastDragPos = Vector3.zero;
    private Vector3 _offsetPos = Vector3.zero;
    public void OnBeginDrag(PointerEventData eventData)
    {
        _onDragging = true;
        StopTweenBubbleFloat();
        _rigidbody.velocity = Vector2.zero;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out Vector3 worldPos))
        {
            _offsetPos = transform.position - worldPos;
            _lastDragPos = transform.localPosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out Vector3 worldPos))
        {
            transform.position = worldPos + _offsetPos;
            CheckLocalPosition();
            _lastDragPos = transform.localPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out Vector3 worldPos))
        {
            transform.position = worldPos + _offsetPos;
            CheckLocalPosition();
            var endPos = transform.localPosition;
            _rigidbody.velocity = new Vector2(endPos.x - _lastDragPos.x, endPos.y - _lastDragPos.y) / 8f;
        }
        _onDragging = false;
    }
    #endregion

    private void CheckLocalPosition()
    {
        int posx = 0, posy = 0;
        var Min_y = UIPanel_Merge.BubbleZoneMinY;
        var Max_y = UIPanel_Merge.BubbleZoneMaxY;
        var localPos = transform.localPosition;
        if (localPos.x < Min_x)
            posx = Min_x;
        else if (localPos.x > Max_x)
            posx = Max_x;
        if (localPos.y < Min_y)
            posy = Min_y;
        else if (localPos.y > Max_y)
            posy = Max_y;
        if (posx != 0 && posy != 0)
            transform.localPosition = new Vector3(posx, posy, 0);
        else if (posx != 0)
            transform.localPosition = new Vector3(posx, localPos.y, 0);
        else if (posy != 0)
            transform.localPosition = new Vector3(localPos.x, posy, 0);
    }

    #region 泡泡悬浮动作
    private Sequence _floatTween;
    private void BubbleFloat()
    {
        _readyToFloat = false;
        if (_floatTween != null)
            return;
        _floatTween = DOTween.Sequence();
        _floatTween.Append(transform.DOLocalMoveY(20, 1.0f).SetRelative(true).SetEase(Ease.InOutSine));
        _floatTween.Append(transform.DOLocalMoveY(-20, 1.0f).SetRelative(true).SetEase(Ease.InOutSine));
        _floatTween.SetLoops(-1);
        _floatTween.Play();
    }

    private void StopTweenBubbleFloat()
    {
        _readyToFloat = false;
        if (_floatTween != null)
        {
            _floatTween.Kill(true);
            _floatTween = null;
        }
    }
    #endregion

    #region 泡泡粒子
    private void ShowBubbleParticle()
    {
        if (_bubble != null)
        {
            _bubble.gameObject.SetActive(true);
            _bubble.Play();
        }
    }

    private void StopBubbleParticle()
    {
        if (_bubble != null)
        {
            _bubble.gameObject.SetActive(false);
            _bubble.Pause();
        }
    }
    #endregion

    public void StopAllBubbleStates()
    {
        this.enabled = false;
        StopTweenBubbleFloat();
        StopBubbleParticle();
    }
}
