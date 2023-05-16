using DG.Tweening;
using IvyCore;
using Merge.Romance.Contorls;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void PageDelegate(int pageIndex, GameObject page);
public delegate ScrollRect PageChangeDelegate(int pageIndex, bool isLocked);

// [ExecuteInEditMode]
public class HVPageView : MonoBehaviour
{
    public event PageDelegate OnPageEnterEvent;
    public event PageDelegate OnPageExitEvent;
    public event PageChangeDelegate OnPageChangeEvent;

    public static HVPageView current;

    [SerializeField]
    private Canvas _canvas;

    private float duration = 0.3f;
    public Canvas Canvas
    {
        get
        {
            if (_canvas == null)
            {
                _canvas = transform.parent.GetComponent<Canvas>();
            }

            return _canvas;
        }
    }

    [SerializeField]
    private ScrollRect _hContainer;

    public ScrollRect HContainer
    {
        get
        {
            if (_hContainer == null)
            {
                _hContainer = GetComponent<ScrollRect>();
            }

            return _hContainer;
        }
    }

    [SerializeField]
    private GameObject _tabbar;

    public GameObject Tabbar
    {
        get
        {
            if (_tabbar == null)
            {
                _tabbar = transform.Find("Tabbar").gameObject;
            }

            return _tabbar;
        }
    }

    [SerializeField]
    private GameObject _selectTab;

    public GameObject SelectTab
    {
        get
        {
            if (_selectTab == null)
            {
                _selectTab = Tabbar.transform.Find("SelectTab").gameObject;
            }

            return _selectTab;
        }
    }

    [SerializeField, SetProperty("PageCount")]
    private int _pageCount;

    public int PageCount
    {
        get => _pageCount;
        set
        {
            _pageCount = value;
            if (_pageCount > 0)
            {
                float pageW = HContainer.viewport.rect.width;
                float tabW = pageW / (_pageCount + 1);
                float tabH = ((RectTransform)_tabbar.transform).rect.height;
                for (int i = 0; i < _pageCount; i++)
                {
                    GameObject page = Pages[i];
                    if (page != null)
                    {
                        page.transform.SetParent(HContainer.content);
                        page.transform.localScale = Vector3.one;
                        //page.transform.localPosition = new Vector3(pageW * (i * 1.5f + 0.5f), 0, 0);
                        (page.transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
                            pageW);
                        //Pages[i].SetActive(!LockFlags[i] && _startPage == i);
                        //Pages[i].SetActive(_startPage == i);
                    }

                    if (i < _pageCount - 1)
                    {
                        GameObject bar = Bars[i];
                        if (bar != null)
                        {
                            RectTransform trans = (RectTransform)bar.transform;
                            trans.localScale = Vector3.one;
                            trans.anchoredPosition = new Vector2(tabW * (i + 1), trans.anchoredPosition.y);
                        }
                    }

                    GameObject icon = Icons[i];
                    if (icon != null)
                    {
                        RectTransform trans = (RectTransform)icon.transform;
                        trans.localScale = Vector3.one;
                        float w = trans.rect.width;
                        float h = trans.rect.height;
                        trans.pivot = Vector2.zero;
                        trans.anchorMin = Vector2.zero;
                        trans.anchorMax = Vector2.zero;
                        trans.anchoredPosition = new Vector2((tabW - w) * 0.5f + tabW * i, (tabH - h) * 0.5f);
                    }
                }

                RectTransform selectTabTrans = (RectTransform)SelectTab.transform;
                selectTabTrans.localScale = Vector3.one;
                //selectTabTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tabW * 2);


                //float x = pageW * (_pageCount * 1.5f + 0.5f);
                //HContainer.content.sizeDelta = new Vector2(x, HContainer.content.sizeDelta.y);
            }
        }
    }

    public List<GameObject> Pages;
    public List<GameObject> Bars;
    public List<GameObject> Icons;
    public List<GameObject> IconsEnable;
    public List<GameObject> IconsDisable;
    public List<bool> LockFlags;
    public List<RectTransform> DontDragPageArea;
    public List<RectTransform> DragScollVPageArea;


    private int _SelectLabel
    {
        set
        {
            //Localizer.Language = CurrentLanguage.GetDescription();
            for (int i = 0; i < _pageCount; i++)
            {
                Icons[i].transform.Find("Label").gameObject.SetActive(i == value);
            }
        }
    }

    private void _SelectTab(int pageIndex, bool doAnimation)
    {
        float pageW = ((RectTransform)transform).rect.width;
        float tabW = pageW / (_pageCount + 1);
        float tabH = ((RectTransform)Tabbar.transform).rect.height;
        for (int i = 0; i < _pageCount; i++)
        {
            if (i < _pageCount - 1)
            {
                GameObject bar = Bars[i];
                if (bar != null)
                {
                    RectTransform trans = (RectTransform)bar.transform;
                    Vector2 pos = new Vector2(tabW * (i >= pageIndex ? i + 2 : i + 1), 0);
                    if (Vector2.Distance(pos, trans.anchoredPosition) > 0.001f)
                    {
                        if (doAnimation)
                        {
                            DOTween.To(() => trans.anchoredPosition, x => trans.anchoredPosition = x, pos, duration);
                        }
                        else
                        {
                            trans.anchoredPosition = pos;
                        }
                    }
                }
            }

            GameObject icon = Icons[i];
            if (icon != null)
            {
                RectTransform trans = (RectTransform)icon.transform;
                float w = trans.rect.width;
                float h = trans.rect.height;

                if (i == pageIndex)
                {
                    float scale = 1.28f;
                    Vector3 targetScale = Vector3.one * scale;
                    if (Vector3.Distance(trans.localScale, targetScale) > 0.001f)
                    {
                        if (doAnimation)
                        {
                            DOTween.To(() => trans.localScale, x => trans.localScale = x, targetScale, duration);
                        }
                        else
                        {
                            trans.localScale = Vector3.one * scale;
                        }
                    }

                    Vector2 targetPos = new Vector2((tabW * 2f - w * scale) * 0.5f + tabW * i,
                        (tabH - h) * 1.3f + 50);
                    if (Vector2.Distance(trans.anchoredPosition, targetPos) > 0.001f)
                    {
                        if (doAnimation)
                        {
                            DOTween.To(() => trans.anchoredPosition, x => trans.anchoredPosition = x, targetPos, duration);
                        }
                        else
                        {
                            trans.anchoredPosition = targetPos;
                        }
                    }
                }
                else
                {
                    if (Vector3.Distance(trans.localScale, Vector3.one) > 0.001f)
                    {
                        if (doAnimation)
                        {
                            DOTween.To(() => trans.localScale, x => trans.localScale = x, Vector3.one, duration);
                        }
                        else
                        {
                            trans.localScale = Vector3.one;
                        }
                    }

                    Vector2 targetPos = new Vector2((tabW - w) * 0.5f + tabW * (i > pageIndex ? i + 1 : i),
                        (tabH - h) * 0.5f);
                    if (Vector2.Distance(trans.anchoredPosition, targetPos) > 0.001f)
                    {
                        if (doAnimation)
                        {
                            DOTween.To(() => trans.anchoredPosition, x => trans.anchoredPosition = x, targetPos, duration);
                        }
                        else
                        {
                            trans.anchoredPosition = targetPos;
                        }
                    }
                }
            }
        }

        RectTransform selectTabTrans = (RectTransform)SelectTab.transform;
        Vector2 targetPos2 = new Vector2((tabW * 2f - 200 * 1.28f) * 0.5f + tabW * pageIndex - 13, 0);
        if (Vector2.Distance(selectTabTrans.anchoredPosition, targetPos2) > 0.001f)
        {
            if (doAnimation)
            {
                DOTween.To(() => selectTabTrans.anchoredPosition, x => selectTabTrans.anchoredPosition = x,
                    targetPos2,
                    duration).OnComplete(() => { _SelectLabel = pageIndex; });
            }
            else
            {
                selectTabTrans.anchoredPosition = targetPos2;
                _SelectLabel = pageIndex;
            }
        }
    }

    /// <summary>
    /// 鼠标上一次的位置
    /// </summary>
    private Vector2 mOldPosition;

    [SerializeField, SetProperty("StartPage")]
    private int _startPage = -1;

    public int StartPage
    {
        get => _startPage;
        set
        {
            int count = Pages.Count;
            if (count <= 1)
            {
                Debug.LogError("参数错误:页面个数不对");
                return;
            }

            value = value < 0 ? 0 : value >= count ? count - 1 : value;
            _currentPage = _startPage = value;
            float target = value * 0.2f;
            Vector3 vec = HContainer.content.transform.localPosition;
            HContainer.content.transform.localPosition =
                new Vector3(-HContainer.content.rect.width * target, vec.y, vec.z);

            _SelectTab(value, false);
        }
    }

    private int _currentPage = -1;

    public int CurrentPage
    {
        get => _currentPage;
        set
        {
            int count = Pages.Count;
            if (count <= 1)
            {
                Debug.LogError("参数错误:页面个数不对");
                return;
            }

            value = value < 0 ? 0 : value >= count ? count - 1 : value;

            if (_currentPage != value)
            {
                //int delta = value - _currentPage;
                //if (delta > 0)
                //{
                //    while (delta > 0)
                //    {
                //        Pages[_currentPage + delta].SetActive(true);
                //        delta--;
                //    }
                //}
                //else
                //{
                //    while (delta < 0)
                //    {
                //        Pages[_currentPage + delta].SetActive(true);
                //        delta++;
                //    }
                //}

                _isScollV = false;
                _dragSpeed = 0;
                _canMove = false;
                OnPageEnterEvent(value, Pages[value]);
                if (_currentPage >= 0)
                {
                    OnPageExitEvent(_currentPage, Pages[_currentPage]);
                }

                _currentPage = value;

                mVScrollRect = OnPageChangeEvent(_currentPage, LockFlags[_currentPage]);
                if (mVScrollRect != null)
                {
                    _isDownOnScrollV = true;
                    if (_currentPage != 1)
                    {
                        Vector3 vec = mVScrollRect.content.transform.localPosition;
                        mVScrollRect.content.transform.localPosition = new Vector3(vec.x, 0, vec.z);
                    }
                    if (_currentPage == 0)
                    {
                        ShopSystem.Instance.refreshShopAction?.Invoke();
                    }

                }

            }

            Pages[_currentPage].SetActive(true);

            float target = value * 0.2f;
            HContainer.content.transform.DOLocalMoveX(-HContainer.content.rect.width * target, 0.5f);
            //HContainer.content.transform.DOLocalMoveX(-HContainer.content.rect.width * target, 0.5f).OnComplete(
            //    () =>
            //    {
            //        for (int i = 0; i < _pageCount; i++)
            //        {
            //            Pages[i].SetActive(i == _currentPage);
            //        }
            //    });

            _SelectTab(value, true);
        }
    }

    /// <summary>
    /// 是否需要进行滑动方向判定
    /// </summary>
    private bool mNeedCaculate = false;

    /// <summary>
    /// 是否进行竖向滚动
    /// </summary>
    private bool _isScollV = false;

    /// <summary>
    /// 当前竖向滚动条(每个page最多只能有一个)
    /// </summary>
    private ScrollRect mVScrollRect;

    /// <summary>
    /// 触摸起始点是否在竖向滚动区域
    /// </summary>
    private bool _isDownOnScrollV = true;

    /// <summary>
    /// 触摸起始点是否在不可滚动区域
    /// </summary>
    private bool _tapDontDragPageArea = false;

    /// <summary>
    /// 触摸起始点是否在垂直滚动区域
    /// </summary>
    private bool _tapVDragPageArea = false;

    /// <summary>
    /// 触摸起始点是否在导航栏(也是不可滚动区域)
    /// </summary>
    private bool _tapTabbar = false;

    /// <summary>
    /// 有竖向滑动的情况下，拖拽结束时是否进行竖向滚动区域缓动效果
    /// </summary>
    private bool _canMove = false;
    /// <summary>
    /// 拖拽结束时竖向滚动区域缓动速度
    /// </summary>
    private float _dragSpeed = 0;

    private void Awake()
    {
        if (PageCount <= 1)
        {
            Debug.LogError("参数错误:页面个数不对");
        }
        current = this;
        InputControl.Instance.EVENT_MOUSE_DOWN += OnPointerDown;
        InputControl.Instance.EVENT_MOUSE_UP += OnPointerUp;
        InputControl.Instance.EVENT_MOUSE_DRAG += OnDrag;
        foreach (var item in Pages)
        {
            if (item == null || UISystem.Instance.uiMainMenu == null)
                continue;
            RectTransform rect = item.transform.GetComponent<RectTransform>();
            RectTransform rectMenu = UISystem.Instance.uiMainMenu.GetComponent<RectTransform>();
            if (rect != null && rectMenu != null)
            {
                rect.sizeDelta = new Vector2(rectMenu.rect.width, rectMenu.rect.height);
            }
        }
    }

    private void Start()
    {
        PageCount = PageCount;
        StartPage = StartPage;
        CheckIconLockState();
        //StartCoroutine(WaitToHidePage());
    }

    //private IEnumerator WaitToHidePage()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    for (var i = 0; i < Pages.Count; ++i)
    //    {
    //        Pages[i].SetActive(i == _currentPage);
    //    }
    //}

    void OnDestory()
    {
        InputControl.Instance.EVENT_MOUSE_DOWN -= OnPointerDown;
        InputControl.Instance.EVENT_MOUSE_UP -= OnPointerUp;
        InputControl.Instance.EVENT_MOUSE_DRAG -= OnDrag;
    }



    private Vector2 transforPointer(RectTransform trans, Vector2 pointer)
    {
        Vector2 _pos = Vector2.one;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(trans,
            pointer, Canvas.worldCamera, out _pos);
        return _pos;
    }

    private bool checkRectContainsPointer(RectTransform trans, Vector2 pointer)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(trans, pointer, Canvas.worldCamera);
    }

    private void DragPage()
    {
        Vector2 mousePosition = Input.mousePosition;
        RectTransform trans = HContainer.content.transform.parent as RectTransform;
        Vector2 pDragVector = transforPointer(trans, mousePosition) - transforPointer(trans, mOldPosition);
        if (mNeedCaculate)
        {
            mNeedCaculate = false;

            if (Mathf.Abs(pDragVector.x) > Mathf.Abs(pDragVector.y))
                _isScollV = false;
            else
                _isScollV = true;

            if (!mVScrollRect || !_isDownOnScrollV)
                _isScollV = false;
        }

        if (_isScollV || _tapVDragPageArea)
        {
            _dragSpeed = pDragVector.y;
            // Vector3 vec = mVScrollRect.content.position;
            Vector3 locVec = mVScrollRect.content.localPosition;
            float down = mVScrollRect.content.rect.height - mVScrollRect.viewport.rect.height;
            float delta = _dragSpeed;
            mVScrollRect.content.localPosition = new Vector3(locVec.x, locVec.y + delta, locVec.z);
            if (locVec.y <= 0 && _dragSpeed < 0)
            {
                delta = Mathf.Lerp(0, delta, (550f + locVec.y) / 550f);
            }
            else if (locVec.y >= down && _dragSpeed > 0)
            {
                delta = Mathf.Lerp(0, delta, (550f - locVec.y + down) / 550f);
            }
        }
        else
        {
            _dragSpeed = pDragVector.x;
            Vector3 vec = HContainer.content.localPosition;
            float delta = _dragSpeed;
            int count = Pages.Count;
            if (_currentPage == 0 && _dragSpeed > 0)
            {
                var posX = HContainer.content.rect.width * 0.1f;
                delta = Mathf.Lerp(0, delta, (posX - vec.x) / delta / 10f);
            }
            else if (_currentPage == count - 1 && _dragSpeed < 0)
            {
                float pageWidth = HContainer.content.rect.width * 0.2f;
                var posX = pageWidth * 0.5f - HContainer.content.rect.width;
                delta = Mathf.Lerp(0, delta, (posX - vec.x) / delta / 10f);
            }

            HContainer.content.localPosition = new Vector3(vec.x + delta, vec.y, vec.z);

            float pageW = ((RectTransform)transform).rect.width;
            float tabW = pageW * 2 / (_pageCount + 1);
            RectTransform selectTabTrans = (RectTransform)SelectTab.transform;
            Vector2 targetPos2 = selectTabTrans.anchoredPosition;
            targetPos2.x += -delta * tabW / pageW;
            selectTabTrans.anchoredPosition = targetPos2;
        }

        mOldPosition = mousePosition;
    }

    private void OnDrag(Vector2 mousePosition)
    {
        if (_tapDontDragPageArea) return;
        DragPage();
    }


    private void OnPointerDown(Vector2 mousePosition)
    {
        mOldPosition = mousePosition;

        _tapTabbar = _tapDontDragPageArea = false;
        if (DontDragPageArea != null)
        {
            foreach (var rect in DontDragPageArea)
            {
                if (checkRectContainsPointer(rect, mousePosition))
                {
                    _tapDontDragPageArea = true;
                    if (rect == Tabbar.transform)
                    {
                        _tapTabbar = true;
                    }

                    break;
                }
            }
        }

        if (DragScollVPageArea != null)
        {
            foreach (var rect in DragScollVPageArea)
            {
                if (checkRectContainsPointer(rect, mousePosition))
                {
                    _tapVDragPageArea = true;
                    break;
                }
                else
                {
                    _tapVDragPageArea = false;
                }
            }
        }

        if (mVScrollRect)
        {
            if (checkRectContainsPointer(mVScrollRect.GetComponent<RectTransform>(), mousePosition))
            {
                mNeedCaculate = true;
                _isDownOnScrollV = true;
                _canMove = false;
            }
            else
                _isDownOnScrollV = false;
        }
    }

    private void OnPointerUp(Vector2 mousePosition)
    {
        if (_tapTabbar && Mathf.Pow(mOldPosition.x - mousePosition.x, 2) + Mathf.Pow(mOldPosition.y - mousePosition.y, 2) < 100f)
        {
            int count = Icons.Count;
            for (int i = 0; i < count; i++)
            {
                if (LockFlags[i]) continue;
                if (checkRectContainsPointer(Icons[i].transform as RectTransform, mousePosition))
                {
                    if (CurrentPage != i)
                    {
                        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.moveHomePage);
                        VibrateSystem.Haptic(MoreMountains.NiceVibrations.HapticTypes.Selection);
                    }
                    CurrentPage = i;
                    return;
                }
            }
        }
        else if (!_tapDontDragPageArea)
        {
            DragPage();
            _canMove = false;

            if (_isScollV || _tapVDragPageArea)
            {
                if (mVScrollRect != null)
                {
                    Vector3 locVec = mVScrollRect.content.localPosition;
                    float down = mVScrollRect.content.rect.height - mVScrollRect.viewport.rect.height;
                    if (down <= 0 || locVec.y <= 0)
                    {
                        mVScrollRect.content.transform.DOLocalMoveY(0, duration);
                    }
                    else if (locVec.y >= down)
                    {
                        mVScrollRect.content.transform.DOLocalMoveY(down, duration);
                    }
                    else
                    {
                        _canMove = true;
                    }
                }
            }
            else
            {
                float delta = 5f;
                if (System.Math.Abs(_dragSpeed) > delta)
                {
                    if (_dragSpeed < 0)
                        PageSlideRight();
                    else
                        PageSlideLeft();
                }
                else
                {
                    int count = Pages.Count;
                    float step = 1f / (count * 3 + 1) * HContainer.content.rect.width;
                    float pageStartX = -step * (_currentPage * 3 + 1);
                    delta = HContainer.content.localPosition.x - pageStartX;
                    if (System.Math.Abs(delta) > step * 1.2f)
                    {
                        if (delta < 0)
                            PageSlideRight();
                        else
                            PageSlideLeft();
                    }
                    else
                        CurrentPage = _currentPage;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (_canMove && _isScollV && mVScrollRect != null)
        {
            if (_dragSpeed > 0)
            {
                _dragSpeed -= duration;
            }
            else
            {
                _dragSpeed += duration;
            }

            if (Mathf.Abs(_dragSpeed) < duration)
            {
                _dragSpeed = 0;
                _canMove = false;
            }

            Vector3 locVec = mVScrollRect.content.localPosition;
            float down = mVScrollRect.content.rect.height - mVScrollRect.viewport.rect.height;
            float delta = _dragSpeed;
            mVScrollRect.content.localPosition = new Vector3(locVec.x, locVec.y + delta, locVec.z);
            if (locVec.y <= 0 && _dragSpeed < 0)
            {
                _canMove = false;
                mVScrollRect.content.transform.DOLocalMoveY(0, duration);
            }
            else if (locVec.y >= down && _dragSpeed > 0)
            {
                _canMove = false;
                mVScrollRect.content.transform.DOLocalMoveY(down, duration);
            }

            mVScrollRect.onValueChanged?.Invoke(mVScrollRect.normalizedPosition);
        }
    }

    private void PageSlideLeft()
    {
        if (CurrentPage > 0)
        {
            var goalPage = CurrentPage;
            while (goalPage > 0)
            {
                goalPage--;
                if (!LockFlags[goalPage])
                {
                    AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.moveHomePage);
                    CurrentPage = goalPage;
                    break;
                }
            }
        }
        else
            CurrentPage = _currentPage;
    }

    private void PageSlideRight()
    {
        if (CurrentPage < PageCount - 1)
        {
            var goalPage = CurrentPage;
            while (goalPage < PageCount - 1)
            {
                goalPage++;
                if (!LockFlags[goalPage])
                {
                    AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.moveHomePage);
                    CurrentPage = goalPage;
                    break;
                }
            }
        }
        else
            CurrentPage = _currentPage;
    }

    private void CheckIconLockState()
    {
        for (int i = 0; i < _pageCount; i++)
        {
            var iconEnable = IconsEnable[i];
            if (iconEnable != null)
                iconEnable.SetActive(!LockFlags[i]);
            var iconDisable = IconsDisable[i];
            if (iconDisable != null)
                iconDisable.SetActive(LockFlags[i]);
        }
    }

    public void OpenMenu(int index)
    {
        if (index < LockFlags.Count && LockFlags[index])
        {
            LockFlags[index] = false;
            CheckIconLockState();
        }
    }

    public static void SkipToPage(int index)
    {
        if (current != null)
            current.CurrentPage = index;
    }
}
