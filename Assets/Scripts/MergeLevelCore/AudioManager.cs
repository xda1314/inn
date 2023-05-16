using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

public class AudioManager : SingletonMono<AudioManager>
{
    private AudioSource bgmAudioSource;
    private const float globalAudioVolumn = 1;
    private AudioClipData currentBGMAudioData;
    private AudioSource accompanimentSource;
    private AudioClipData accompanimentData;

    private bool isBgmStopping = false;

    /// <summary>
    /// 音效库
    /// </summary>
    public static AudioClipSO audioClipSO { get; private set; }

    private void Awake()
    {
        //场景背景音乐
        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        bgmAudioSource.playOnAwake = false;
        bgmAudioSource.loop = true;
        bgmAudioSource.volume = 0;

        //副歌部分
        accompanimentSource = gameObject.AddComponent<AudioSource>();
        accompanimentSource.playOnAwake = false;
        accompanimentSource.loop = true;
        accompanimentSource.volume = 0;

        AssetSystem.Instance.LoadAsset<AudioClipSO>("SO_AudioClip", (so) =>
        {
            audioClipSO = so;
        });
        if (audioClipSO == null)
        {
            Debug.LogError("AudioClipSO 无法找到！！！");
        }
        //无论什么情况下 都播放背景音乐

    }

    private bool pauseState = false;
    public void PauseBGM()
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.Pause();
            pauseState = true;
        }
    }

    public void UnPauseBGM()
    {
        pauseState = false;
        if (bgmAudioSource != null)
        {
            bgmAudioSource.UnPause();        
        }
    }


    public void PlayBGM()
    {
        PlayBGM(audioClipSO.homeBgm);
    }
    public async void PlayInMergeBGM(bool inLevel)
    {
        if (isBgmStopping)
        {
            await Task.Delay(500);
        }
        else if (bgmAudioSource.isPlaying)
        {
            StopBGM();
            await Task.Delay(1000);
        }

        if (inLevel)
        {
            int count = audioClipSO.levelBgms.Length;
            int rand = UnityEngine.Random.Range(0, count);
            PlayBGM(audioClipSO.levelBgms[rand]);
        }
        else
        {
            PlayBGM(audioClipSO.homeBgm);
        }
    }



    public async void ChangeBGM(AudioClipData clipSO)
    {
        if (isBgmStopping)
        {
            await Task.Delay(500);
        }
        else if (bgmAudioSource.isPlaying)
        {
            StopBGM();
            await Task.Delay(1000);
        }

        PlayBGM(clipSO);
    }

    // 切换背景声音开关
    public void ChangeActivePlayBGM()
    {
        bgmAudioSource.DOKill();
        if (currentBGMAudioData != null)
        {
            if (!GameManager.Instance.playerData.IsMusicOn)
            {
                bgmAudioSource.volume = 0;
            }
            else
            {
                float targetVolumn = globalAudioVolumn * currentBGMAudioData.volumn;
                bgmAudioSource.DOFade(targetVolumn, 1f).onComplete += () =>
                {
                    bgmAudioSource.volume = targetVolumn;
                };
            }
        }
        accompanimentSource.DOKill();
        if (accompanimentData != null)
        {
            if (!GameManager.Instance.playerData.IsMusicOn)
            {
                accompanimentSource.volume = 0;
            }
            else
            {
                float targetVolumn = globalAudioVolumn * currentBGMAudioData.volumn;
                accompanimentSource.DOFade(targetVolumn, 1f).onComplete += () =>
                {
                    accompanimentSource.volume = targetVolumn;
                };
            }
        }
    }

    // 播放背景音乐
    private void PlayBGM(AudioClipData audioData, AudioClipData audioData1 = null)
    {
        if (audioData == null)
        {
            Debug.LogError("bgm is null!");
            return;
        }

        currentBGMAudioData = audioData;

        float targetVolumn = globalAudioVolumn * audioData.volumn;
        if (!GameManager.Instance.playerData.IsMusicOn)
        {
            targetVolumn = 0;
        }

        bgmAudioSource.volume = 0;
        bgmAudioSource.clip = audioData.audioClip;
        bgmAudioSource.Play();
        bgmAudioSource.DOKill();
        bgmAudioSource.DOFade(targetVolumn, 1f).onComplete += () =>
        {
            bgmAudioSource.volume = targetVolumn;
        };

        //副歌部分   暂时不清楚 如何同时播放两个  待优化
        if (audioData1 == null)
        {
            return;
        }

        accompanimentData = audioData1;
        accompanimentSource.volume = 0;
        accompanimentSource.clip = accompanimentData.audioClip;
        accompanimentSource.Play();
        accompanimentSource.DOKill();
        accompanimentSource.DOFade(targetVolumn, 1f).onComplete += () =>
        {
            accompanimentSource.volume = targetVolumn;
        };

    }

    // 取消播放背景音乐
    public void StopBGM()
    {
        isBgmStopping = true;
        bgmAudioSource.DOKill();
        bgmAudioSource.DOFade(0, 1f).onComplete += () =>
        {
            currentBGMAudioData = null;
            isBgmStopping = false;
            bgmAudioSource.Stop();
        };

        accompanimentSource.DOKill();
        accompanimentSource.DOFade(0, 1f).onComplete += () =>
        {
            accompanimentData = null;
            accompanimentSource.Stop();
        };
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="audioClip"></param>
    public void PlayEffect(AudioClipData audioData)
    {
        if (audioData == null || audioData.audioClip == null)
        {
            //ivy.game.GameDebug.LogError("audioData is null");
            return;
        }

        if (GameManager.Instance != null && GameManager.Instance.playerData != null && GameManager.Instance.playerData.IsEffectOn)
        {
            AudioSource.PlayClipAtPoint(audioData.audioClip, Vector3.zero, globalAudioVolumn * audioData.volumn);
        }
    }

    /// <summary>
    /// 播放合成音效
    /// </summary>
    /// <param name="itemLevel"></param>
    public void PlayMergeSound(int itemLevel)
    {
        var index = itemLevel - 1;
        if (index >= 0 && index < audioClipSO.Composites.Length)
            PlayEffect(audioClipSO.Composites[index]);
    }
}
