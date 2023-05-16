using DG.Tweening;
using UnityEngine;

public class BtnOutlineFX : MonoBehaviour
{
    [SerializeField] private MeshRenderer outlineMat;
    [SerializeField] private ParticleSystem starParticle;
    public bool autoPlayFX = true;

    private Sequence sequence;
    private Tweener tweener;

    public void StartFX()
    {
        if (sequence != null)
            sequence.Kill(true);
        outlineMat.material.mainTextureOffset = new Vector2(1.1f, 0);

        sequence = DOTween.Sequence();
        sequence.AppendInterval(1.5f);
        sequence.InsertCallback(2, () =>
        {
            outlineMat.material.mainTextureOffset = new Vector2(0, 0);
            tweener = DOTween.To(() => outlineMat.material.mainTextureOffset,
                (value) => outlineMat.material.mainTextureOffset = value,
                new Vector2(-8.2f, 0), 5f).SetEase(Ease.Linear);
        });
        sequence.InsertCallback(6.2f, () => { starParticle.Play(); });
        sequence.AppendInterval(2.5f);
        sequence.SetLoops(-1);
        sequence.Play();
    }

    public void StopFX()
    {
        if (sequence != null)
        {
            sequence.Kill(true);
            sequence = null;
            if (tweener != null)
            {
                tweener.Kill();
                tweener = null;
                outlineMat.material.mainTextureOffset = new Vector2(1.1f, 0);
            }
            starParticle.Stop();
        }
    }

    public void OnEnable()
    {
        if (autoPlayFX)
            StartFX();
    }

    private void OnDisable()
    {
        if (autoPlayFX)
            StopFX();
    }

}
