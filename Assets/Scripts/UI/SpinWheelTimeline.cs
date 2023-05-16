using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Playables;

public class SpinWheelTimeline : MonoBehaviour
{
    [SerializeField] private GameObject arrowGO;
    [SerializeField] private GameObject wheelGO;

    private float needEularAngles = 0;
    private float currentEularAngles = 0;
    float currentSecond = 0;

    public Sequence sequence;
    [HideInInspector] public bool isFinish = true;

    public void SpinWheel_ByDOTween(int rewardIndex, Action finishcb, Action insertcb)
    {
        if (sequence != null)
        {
            sequence.Kill(true);
        }

        float dis = wheelGO.transform.localEulerAngles.z % 45;
        float offset = dis > 0 ? (dis > 22.5f ? dis - 45 : dis) : (dis < -22.5f ? dis + 45 : dis);
        wheelGO.transform.localEulerAngles = new Vector3(0, 0, Mathf.RoundToInt(wheelGO.transform.localEulerAngles.z - offset));
        arrowGO.transform.localEulerAngles = Vector3.zero;


        isFinish = false;
        currentEularAngles = wheelGO.transform.localEulerAngles.z;
        float temp = currentEularAngles % 360;
        if (temp > 0)
        {
            temp -= 360;
        }
        needEularAngles = 360 * 4 - 45 * (-temp / 45) + 45 * rewardIndex;

        //Debug.LogError("currentEularAngles:" + currentEularAngles);

        ////AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.spinStart);

        sequence = DOTween.Sequence();
        currentSecond = 0;
        //加速阶段
        sequence.Append(wheelGO.transform.DOLocalRotate(new Vector3(0, 0, currentEularAngles - 90), 1f, RotateMode.FastBeyond360).SetEase(Ease.InBack));
        currentEularAngles += -90;
        currentEularAngles %= 360;
        if (currentEularAngles < 0)
        {
            currentEularAngles += 360;
        }

        currentSecond += 1f;
        sequence.Insert(0.70f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 53), 0.04f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        sequence.Insert(0.74f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.04f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        sequence.Insert(0.81f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 53), 0.03f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        sequence.Insert(0.84f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.03f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        sequence.Insert(0.90f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 53), 0.02f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        sequence.Insert(0.92f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.02f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        sequence.Insert(0.96f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 53), 0.02f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        sequence.Insert(0.98f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.02f, RotateMode.FastBeyond360).SetEase(Ease.Linear));


        //匀速阶段
        float perSecondForSpin45 = 0.1f;
        float totalSecondInMid = (needEularAngles - 90 - 720) / 45 * perSecondForSpin45;
        sequence.Append(wheelGO.transform.DOLocalRotate(new Vector3(0, 0, currentEularAngles - needEularAngles + 90 + 720), totalSecondInMid, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        currentEularAngles -= needEularAngles - 90 - 720;
        currentEularAngles %= 360;
        if (currentEularAngles < 0)
        {
            currentEularAngles += 360;
        }

        //Debug.LogError("perSecondForSpin45:" + perSecondForSpin45);
        //Debug.LogError("totalSecondInMid:" + totalSecondInMid);
        for (float i = 0; i < totalSecondInMid; i += perSecondForSpin45)
        {
            sequence.Insert(currentSecond + i + 0.04f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 53), 0.02f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
            sequence.Insert(currentSecond + i + 0.06f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.02f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        }


        currentSecond += totalSecondInMid;
        //减速阶段
        sequence.Append(wheelGO.transform.DOLocalRotate(new Vector3(0, 0, currentEularAngles - 720), 6, RotateMode.FastBeyond360).SetEase(Ease.OutQuart));
        currentEularAngles -= 720;
        //1
        sequence.Insert(currentSecond + 0.04f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 53), 0.02f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        sequence.Insert(currentSecond + 0.06f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.02f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        //2
        sequence.Insert(currentSecond + 0.14f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 53), 0.02f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        sequence.Insert(currentSecond + 0.16f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.02f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        //3
        sequence.Insert(currentSecond + 0.24f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 53), 0.02f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        sequence.Insert(currentSecond + 0.26f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.02f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        //4
        sequence.Insert(currentSecond + 0.35f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 53), 0.02f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        sequence.Insert(currentSecond + 0.37f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.02f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        //5
        sequence.Insert(currentSecond + 0.46f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 53), 0.03f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        sequence.Insert(currentSecond + 0.485f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.03f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        //6
        sequence.Insert(currentSecond + 0.585f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 53), 0.04f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        sequence.Insert(currentSecond + 0.62f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.04f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        //7
        sequence.Insert(currentSecond + 0.72f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 53), 0.04f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        sequence.Insert(currentSecond + 0.76f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.04f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        //8
        sequence.Insert(currentSecond + 0.86f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 53), 0.04f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        sequence.Insert(currentSecond + 0.9f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.04f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        //9
        sequence.Insert(currentSecond + 1.02f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 53), 0.04f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        sequence.Insert(currentSecond + 1.06f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.04f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        //10
        sequence.Insert(currentSecond + 1.19f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 53), 0.05f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        sequence.Insert(currentSecond + 1.24f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.05f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        //11
        sequence.Insert(currentSecond + 1.38f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 53), 0.06f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        sequence.Insert(currentSecond + 1.44f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.06f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        //12
        sequence.Insert(currentSecond + 1.6f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 53), 0.07f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        sequence.Insert(currentSecond + 1.67f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.07f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        //13
        sequence.Insert(currentSecond + 1.86f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 53), 0.08f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        sequence.Insert(currentSecond + 1.94f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.08f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        //14
        sequence.Insert(currentSecond + 2.18f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 53), 0.12f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        sequence.Insert(currentSecond + 2.30f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.12f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        //15
        sequence.Insert(currentSecond + 2.62f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 53), 0.20f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        sequence.Insert(currentSecond + 2.82f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.20f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        //16
        sequence.Insert(currentSecond + 3.34f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 53), 0.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        sequence.Insert(currentSecond + 3.84f, arrowGO.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.2f, RotateMode.FastBeyond360).SetEase(Ease.Linear));

        sequence.InsertCallback(currentSecond + 3.8f, () =>
        {
            insertcb?.Invoke();
        });
        //float timer = 0;
        //sequence.onPlay += () =>
        //{
        //    timer = 0;
        //};
        //sequence.onUpdate += () =>
        //{
        //    timer += Time.deltaTime;
        //    Debug.LogError(timer);
        //};

        sequence.onComplete += () =>
        {
            wheelGO.transform.localEulerAngles = new Vector3(0, 0, currentEularAngles);
            arrowGO.transform.localEulerAngles = Vector3.zero;
            isFinish = true;
            finishcb?.Invoke();
        };
        sequence.Play();
    }

    public void KillAnimation()
    {
        if (sequence != null)
        {
            sequence.Rewind(true);
            sequence.Kill();
            isFinish = true;
        }
    }


}
