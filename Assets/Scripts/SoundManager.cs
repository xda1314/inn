// ILSpyBased#2
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace ivy.game
{
    public class SoundManager : MonoBehaviour
    {
        public enum FireworkSoundType
        {
            Explore1,
            Explore2,
            Explore3
        }

        public enum CandelabraSoundType
        {
            LitFirstOrSecondCandle,
            LitThirdCandle,
            Fire
        }

        public enum ClockSoundType
        {
            Countdown,
            ExtraMoves,
            Match,
            TimeUp
        }

        public enum UmbrellaSoundType
        {
            Celected,
            Match,
            Explosion
        }

        public enum TileSoundType
        {
            Collect
        }

        public enum TableSoundType
        {
            Match,
            Collected
        }

        public enum LampSoundType
        {
            TurnOn,
            ClearAndCollect
        }

        public enum CuckooSoundType
        {
            Close,
            Open,
            Clear
        }

        public enum DoorAndKeySoundType
        {
            ClearKey,
            ClearLock,
            OpenDoor
        }

        public enum StatueSoundType
        {
            Clear,
            Decrease
        }

        public enum TelephoneSoundType
        {
            Clear,
            Ring
        }

        public enum LawnmowerSoundType
        {
            Chargging,
            Trigger
        }

        public enum ToyTrainSoundType
        {
            Forward,
            Collect
        }

        [SerializeField]
        private AudioSource bgMusic;

        [SerializeField]
        private AudioSource sound;

        private string bgMusicName;

        private AudioClipsHolder common;

        private AudioClipsHolder sceneBased;

        private Dictionary<int, float> _startTimes;

        private const float MIN_INTERVAL = 0.1f;

        private bool _enableSound = true;

        private bool fadeInning;

        private bool fadeOuting;

        private float fadeInOutTimeMax = 0f;

        private float fadeInOutTime;

        private static SoundManager _instance;

        private IEnumerator orderBgm;

        private string _currLevelBgMusic = string.Empty;

        private string[] levelBgMusics = new string[2] {
            "Matchington Mansion Gameplay Loopable Ver 2 Mastered",
            "Matchington_BGM_3_Ver_4.wav"
        };

        private readonly Dictionary<string, string> COMBOS = new Dictionary<string, string> {
            {
                "big_1",
                "VO-Luxurious"
            },
            {
                "big_2",
                "VO-Gorgeous"
            },
            {
                "middle_1",
                "VO-Outstanding"
            },
            {
                "middle_2",
                "VO-Brilliant"
            },
            {
                "middle_3",
                "VO-Magnificent"
            },
            {
                "middle_4",
                "VO-Sensational"
            },
            {
                "small_1",
                "VO-Fantastic"
            },
            {
                "small_2",
                "VO-Excellent"
            },
            {
                "small_3",
                "VO-Fancy"
            },
            {
                "small_4",
                "VO-Great Job"
            },
            {
                "small_5",
                "VO-Smashing"
            }
        };

        private string[] fireworkSoundNames = new string[3] {
            "Firework_Explode_1_Ver_1",
            "Firework_Explode_2_Ver_1",
            "Firework_Explode_3_Ver_1"
        };

        private string[] candelabraSoundNames = new string[3] {
            "Candelabra_Lit_1_Ver_1",
            "Candelabra_Lit_2_Ver_1",
            "Candelabra_Ver_1"
        };

        private string[] clockSoundNames = new string[4] {
            "Alarm_Clock_Countdown_Ver_1",
            "Alarm_Clock_Extra_Moves_Ver_1",
            "Alarm_Clock_Match_Ver_1",
            "Alarm_Clock_TimeUp_Ver_1"
        };

        private string[] umbrellaSoundNames = new string[3] {
            "Umbrella_Select_Ver_1",
            "Umbrella_Match_Ver_1",
            "Umbrella_Explosion_Ver_1"
        };

        private string[] fileSoundNames = new string[1] {
            "Tile_Collect_Ver_2"
        };

        private string[] tableSoundNames = new string[2] {
            "Tables_Match_Ver_1",
            "Tables_Collected_Ver_1"
        };

        private string[] lampSoundNames = new string[2] {
            "Lamp_Turn_On_Ver_1",
            "Lamp_Clear_Ver_1"
        };

        private string[] cuckooSoundNames = new string[3] {
            "Cuckoo_Close_SFX_Ver_1",
            "Cuckoo_Open_SFX_Ver_1",
            "Cuckoo_Clear_SFX_Ver_1"
        };

        private string[] DoorAndKeySoundNames = new string[3] {
            "Key_Clear_Ver_1",
            "Key_Hit_Door_Ver_1",
            "Door_Open_Ver_2"
        };

        private string[] StatueSoundNames = new string[2] {
            "Statue_Clear_Ver_3",
            "Statue_Decrease_Ver_1"
        };

        private string[] TelephoneSoundNames = new string[2] {
            "Telephone_Clear_Ver_2",
            "Telephone_Tap_Ver_1"
        };

        private string[] LawnmowerSoundNames = new string[2] {
            "Vacuum_Charging",
            "Vacuum_Trigger"
        };

        private string[] ToyTrainSoundNames = new string[2] {
            "Train_Forward",
            "Toy_Train_Arrive"
        };

        public static SoundManager Instance
        {
            get
            {
                return SoundManager._instance;
            }
        }

        public string toyTrainForwardName
        {
            get
            {
                return this.ToyTrainSoundNames[0];
            }
        }

        protected SoundManager()
        {
        }

        public static void InitInstance()
        {
            if (SoundManager._instance == null)
            {
                GameObject gameObject = AssetSystem.Instance.Instantiate("SoundManager", null);
                SoundManager._instance = gameObject.GetComponent<SoundManager>();
            }
        }

        private void Awake()
        {
            this.init();
        }

        private void init()
        {
            this._startTimes = new Dictionary<int, float>();
            //if (this.common == null)
            //{
            //    this.common = this.LoadAudioHolder("Common_Sounds");
            //}
            UnityEngine.Object.DontDestroyOnLoad(this);
        }

        private void Start()
        {
            //ModelBinder.BindProp(0.ToString(), new ModelBinder.PropChangedListener(this.OnMusicChanged));
            //ModelBinder.BindProp(ModelPropKey.UserMusic.ToString(), new ModelBinder.PropChangedListener(this.OnMusicChanged));
        }

        private void Update()
        {
            if (this.fadeOuting)
            {
                this.fadeInning = false;
                this.fadeInOutTime += Time.deltaTime;
                if (this.fadeInOutTime >= this.fadeInOutTimeMax)
                {
                    this.fadeOuting = false;
                    this.bgMusic.volume = 0f;
                    this.bgMusic.Stop();
                }
                else
                {
                    this.bgMusic.volume = 1f - this.fadeInOutTime / this.fadeInOutTimeMax;
                }
            }
            if (this.fadeInning)
            {
                this.fadeInOutTime += Time.deltaTime;
                if (this.fadeInOutTime >= this.fadeInOutTimeMax)
                {
                    this.fadeInning = false;
                    this.bgMusic.volume = 1f;
                }
                else
                {
                    this.bgMusic.volume = this.fadeInOutTime / this.fadeInOutTimeMax;
                }
            }
        }

        private void OnDestroy()
        {
            //ModelBinder.UnbindProp(0.ToString(), new ModelBinder.PropChangedListener(this.OnMusicChanged));
            //ModelBinder.UnbindProp(ModelPropKey.UserMusic.ToString(), new ModelBinder.PropChangedListener(this.OnMusicChanged));
        }

        private void OnMusicChanged(object changeValue)
        {
            if ((bool)changeValue)
            {
                this.ResumeBGMusic();
            }
            else
            {
                this.StopBGMusic();
            }
        }

        public void PlayBGMusic(string name, bool loop = true, bool overrideMode = true)
        {
            if (overrideMode)
            {
                this.StopOrderedBgm();
            }
            bool flag = true;
            if (this.bgMusicName != name)
            {
                this.bgMusic.clip = this.LoadMusicClip(name);
            }
            else if (this.bgMusic.isPlaying)
            {
                flag = false;
            }
            if (this.fadeOuting)
            {
                this.fadeOuting = false;
                this.fadeInOutTime = 0f;
                flag = true;
            }
            this.bgMusicName = name;
            this.bgMusic.loop = loop;
            if (flag)
            {
                this.bgMusic.Play();
                this.fadeInning = true;
                this.fadeInOutTime = 0f;
                this.bgMusic.volume = 0f;
            }
        }

        private AudioClip LoadMusicClip(string namePath)
        {
            //if (namePath.Contains("Addon"))
            //{
            //    return Singleton<AssetBundleManager>.Instance.LoadAsset<AudioClip>(namePath, null);
            //}
            if (namePath.EndsWith(".wav"))
            {
                int nLen = namePath.Length;
                int nStart = namePath.LastIndexOf("/") + 1;
                namePath = namePath.Substring(nStart, nLen - nStart - 4);
            }
            return (AudioClip)Resources.Load("Musics/" + namePath, typeof(AudioClip));
        }

        public void StopBGMusic()
        {
            if (!this.fadeOuting && this.bgMusic.isPlaying)
            {
                this.fadeOuting = true;
                this.fadeInOutTime = 0f;
                this.bgMusic.volume = 1f;
            }
        }

        public void ResumeBGMusic()
        {
            if (this.bgMusicName != null)
            {
                this.PlayBGMusic(this.bgMusicName, this.bgMusic.loop, true);
            }
        }

        public void SetBGVol(float vol)
        {
            if (this.bgMusic != null)
            {
                this.bgMusic.volume = vol;
            }
        }

        public float GetBGVol()
        {
            return (!(this.bgMusic == null)) ? this.bgMusic.volume : 0f;
        }

        private void PlayOrderedBgm(params string[] music)
        {
            this.StopOrderedBgm();
            this.orderBgm = this.DoPlayOrderedBgm(music);
            base.StartCoroutine(this.orderBgm);
        }

        private void StopOrderedBgm()
        {
            if (this.orderBgm != null)
            {
                base.StopCoroutine(this.orderBgm);
                this.orderBgm = null;
            }
        }

        private IEnumerator DoPlayOrderedBgm(params string[] music)
        {
            //int i = 0;
            //while (true)
            //{
            //    if (i < music.Length)
            //    {
            //        if (i != music.Length - 1)
            //        {
            //            break;
            //        }
            //        this.PlayBGMusic(music[i], true, false);
            //        i++;
            //        continue;
            //    }
            //    yield break;
            //}
            //this.PlayBGMusic(music[i], false, false);
            //yield return (object)new WaitForSeconds(this.bgMusic.clip.length);
            ///*Error: Unable to find new state assignment for yield return*/;
            ///
            _003CDoPlayOrderedBgm_003Ec__Iterator0 _003CDoPlayOrderedBgm_003Ec__Iterator = new _003CDoPlayOrderedBgm_003Ec__Iterator0();
            _003CDoPlayOrderedBgm_003Ec__Iterator.music = music;
            _003CDoPlayOrderedBgm_003Ec__Iterator._0024this = this;
            return _003CDoPlayOrderedBgm_003Ec__Iterator;
        }

        private sealed class _003CDoPlayOrderedBgm_003Ec__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int _003Ci_003E__0;

            internal string[] music;

            internal SoundManager _0024this;

            internal object _0024current;

            internal bool _0024disposing;

            internal int _0024PC;

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this._0024current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this._0024current;
                }
            }

            [DebuggerHidden]
            public _003CDoPlayOrderedBgm_003Ec__Iterator0()
            {
            }

            public bool MoveNext()
            {
                uint num = (uint)this._0024PC;
                this._0024PC = -1;
                switch (num)
                {
                    case 0u:
                        this._003Ci_003E__0 = 0;
                        goto IL_00bd;
                    case 1u:
                        this._003Ci_003E__0++;
                        goto IL_00bd;
                    default:
                        {
                            return false;
                        }
                    IL_00bd:
                        if (this._003Ci_003E__0 < this.music.Length)
                        {
                            if (this._003Ci_003E__0 == this.music.Length - 1)
                            {
                                this._0024this.PlayBGMusic(this.music[this._003Ci_003E__0], true, false);
                                goto case 1u;
                            }
                            this._0024this.PlayBGMusic(this.music[this._003Ci_003E__0], false, false);
                            this._0024current = new WaitForSeconds(this._0024this.bgMusic.clip.length);
                            if (!this._0024disposing)
                            {
                                this._0024PC = 1;
                            }
                            break;
                        }
                        this._0024PC = -1;
                        goto default;
                }
                return true;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this._0024disposing = true;
                this._0024PC = -1;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }
        }

        public void PlayLevelContinue()
        {
            this.PlayOrderedBgm("Extra Moves Ver 1", this._currLevelBgMusic);
        }

        public void PlayOpeningMelody()
        {
            this.PlayOrderedBgm("Opening Melody Ver 1", "Nature_Ambience");
        }

        public void PlayPurchaseCoins()
        {
            this.PlayOrderedBgm("Purchase_Coins", "BGM_map");
        }

        public void PlayPurchaseCoinsInLevel()
        {
            this.PlayOrderedBgm("Purchase_Coins", this._currLevelBgMusic);
        }

        public void PlayJammingTimeBGM()
        {
            //this.PlayOrderedBgm("Level_Start_Melody", "VictoryLoopVer1");
            this.PlayBGMusic("VictoryLoopVer1", false, true);
        }

        public void PlayLevelWinBGM()
        {
            this.PlayBGMusic("VictoryLoopEndVer1", false, true);
        }

        public void PlayLevelFailBGM()
        {
            this.PlayBGMusic("Defeat", false, true);
        }

        public void PlaySocialBGM()
        {
            this.PlayBGMusic("Matchington_Mansion_Social_Loop", true, true);
        }

        public void PlayAdd5MovesBGM()
        {
            this.PlayBGMusic("Out_Of_Moves", false, true);
        }

        public AudioClip GetCatchedAudioClip(string name)
        {
            AudioClip audioClip = this.common.TryGetAudioClip(name);
            if (audioClip == null)
            {
                audioClip = this.sceneBased.TryGetAudioClip(name);
            }
            return audioClip;
        }

        public void PlaySound(string name, float delay = 0f, float volumeScale = 1f)
        {
            //AudioClip audioClip = this.common.TryGetAudioClip(name);
            //if (audioClip == null && this.sceneBased != null)
            //{
            //    audioClip = this.sceneBased.TryGetAudioClip(name);
            //}
            //if (audioClip != null)
            //{
            //    this.PlaySound(audioClip, delay, volumeScale);
            //}
        }

        public void PlaySoundBundle(string path, float delay = 0f, float volumeScale = 1f)
        {
            //AudioClip audioClip = SingletonMono<AssetBundleManager>.Instance.LoadAsset<AudioClip>(path, null);
            //if (audioClip != null)
            //{
            //    this.PlaySound(audioClip, delay, volumeScale);
            //}
        }

        public void PlaySound(AudioClip clip, float delay = 0f, float volumeScale = 1f)
        {
            //if (this._enableSound)
            //{
            //    UserModel user = SingletonMono<GameManager>.Instance.User;
            //    if (user != null && user.SoundEnabled && !(clip == null))
            //    {
            //        if (delay > 0f)
            //        {
            //            base.gameObject.RunActions(new KDelayTime(delay), new BDCallFuncO(delegate (object obj)
            //            {
            //                this.PlaySound((AudioClip)obj, 0f, volumeScale);
            //            }, clip));
            //        }
            //        else if (this.SetStartTime(clip.GetInstanceID()))
            //        {
            //            this.sound.PlayOneShot(clip, volumeScale);
            //        }
            //    }
            //}
        }

        public void SwapAction()
        {
        }

        public void SwapFail()
        {
            this.PlaySound("Swap Pieces Fail Ver 1", 0f, 1f);
        }

        public void Shuffle()
        {
            this.PlaySound("Gameplay_Shuffle", 0.55f, 1f);
        }

        public void MoveHit()
        {
            this.PlaySound("Match_Settle", 0f, 1f);
        }

        public void ElimElement(int count, float delay = 0f)
        {
            if (count == 1)
            {
                this.PlaySound("Match Base Ver 1", 0f, 1f);
            }
            else
            {
                string name = "Match " + (count - 1).ToString() + " Ver 2";
                this.PlaySound(name, 0f, 1f);
            }
        }

        public void Combo(string animSkinName)
        {
            if (this.COMBOS.ContainsKey(animSkinName))
            {
                this.PlaySound(this.COMBOS[animSkinName], 0f, 1f);
            }
        }

        //道具出现音效--大炸弹
        public void CreateStripedBomb()
        {
            this.PlaySound("Powerup_Create", 0f, 1f);
        }

        //道具出现音效--小炸弹
        public void CreateSquareBomb()
        {
            this.PlaySound("Powerup_Create", 0f, 1f);
        }

        //道具出现音效--火箭筒
        public void CreateCrossBomb()
        {
            this.PlaySound("Powerup_Create", 0f, 1f);
        }

        //道具出现音效--彩球
        public void CreateColorBomb()
        {
            this.PlaySound("Powerup_Create_Rainbow", 0f, 1f);
        }

        public void CreateQuadBomb()
        {
        }

        //道具作用音效--大炸弹
        public void LargeBombExplode()
        {
            this.PlaySound("Powerup_LargeBomb", 0f, 1f);
        }

        public void ExtraLargeBombExplode()
        {
            this.PlaySound("Powerup_ExtraLargeBomb", 0f, 1f);
        }

        //道具作用音效--火箭筒
        public void StripedBombExplode()
        {
            this.PlaySound("Powerup_LineBlaster", 0f, 1f);
        }

        //道具作用音效--小炸弹
        public void SquareBombExplode()
        {
            this.PlaySound("Powerup_SmallBomb", 0f, 1f);
        }

        public void ColorBombExplode()
        {
            this.PlaySound("Rainbow Plus Match Anything", 0f, 1f);
        }

        public void RainbowBombBoomStep(string step)
        {
            if (step == "Combine")
            {
                this.PlaySound("Powerup_Rainbow_ColorTransformation", 0f, 1f);
            }
            else if (step == "StartTrail")
            {
                this.PlaySound("Powerup_Rainbow_Lines", 0f, 1f);
            }
            else if (step == "StartEliminate")
            {
                this.PlaySound("Powerup_Rainbow_Explosion", 0f, 1f);
            }
        }

        public void ColorBombExplodeOther()
        {
        }

        public void QuadBombExplode()
        {
        }

        public void CrossBombExplode()
        {
            this.PlaySound("Powerup_Combo_CrossBlast", 0f, 1f);
        }

        public void SquareSquareBombExplode()
        {
        }

        public void SquareStripedBombExplode()
        {
            this.PlaySound("Powerup_Combo_WideCrossBlast", 0f, 1f);
        }

        public void ColorColorBombExplode()
        {
            this.PlaySound("Powerup_DoubleRainbow", 0f, 1f);
        }

        public void LinePlusSmallCombo()
        {
            this.PlaySound("Match 4 Line Plus Match 4 Bomb", 0f, 1f);
        }

        public void LargePlusLargeCombo()
        {
            this.PlaySound("Powerup_ExtraLargeBomb", 0f, 1f);
        }

        public void LargePlusSmallCombo()
        {
            this.PlaySound("L and T Plus Match 4 Bomb Ver 1", 0f, 1f);
        }

        public void QuadSquareBombExplode()
        {
        }

        public void ElimJelly()
        {
        }

        public void ThroughOutTeleporter()
        {
        }

        public void ConveyorMove()
        {
        }

        public void DyeRugTakeEffect()
        {
        }

        public void CreateBubble()
        {
        }

        public void CollectBubble()
        {
        }

        public void PopcornBucketGrowUp()
        {
        }

        public void PopcornBucketEruption()
        {
        }

        public void CreatePopcorn()
        {
        }

        public void ElimPopcorn()
        {
        }

        public void SprawlCotton()
        {
            this.PlaySound("Obstacle_Soap_Spread", 0f, 1f);
        }

        public void ElimCotton()
        {
            this.PlaySound("Obstacle_Soap_Clear", 0f, 1f);
        }

        public void ElimBook()
        {
            this.PlaySound("Obstacle_Book_Decrease", 0f, 1f);
        }

        public void ElimMiceBox(int hp)
        {
            this.PlaySound("Obstacle_Frame_Decrease", 0f, 1f);
        }

        public void MouseMove()
        {
        }

        public void ElimTeaCup()
        {
            this.PlaySound("Obstacle_Teacup_Clear", 0f, 1f);
        }

        public void BreakPretzel()
        {
            this.PlaySound("Obstacle_Rope_Decrease", 0f, 1f);
        }

        public void MirrorTransform()
        {
            this.PlaySound("Obstacle_Mirror_Transform", 0f, 1f);
        }

        public void CrushIceLayer(bool withEnvelope)
        {
            this.PlaySound("Obstacle_Glass_Decrease", 0f, 1f);
        }

        public void MonsterFly()
        {
        }

        public void MonsterEat()
        {
        }

        public void MonsterDizzy()
        {
        }

        public void MonsterRevive()
        {
        }

        public void ElimMonster()
        {
        }

        public void AngryBirdShowSelf()
        {
        }

        public void AngryBirdFly()
        {
        }

        public void TargetCompleted()
        {
            this.PlaySound("Target_Complete", 0f, 1f);
        }

        public void CollectElement()
        {
            this.PlaySound("Target_Collect", 0f, 1f);
        }

        public void CollectMouse()
        {
            this.PlaySound("Mouse Collect Ver 1", 0f, 1f);
        }

        public void CollectBear()
        {
            this.PlaySound("Bear Collect Ver 1", 0f, 1f);
        }

        public void ElimAntiqueBox(int hp)
        {
            this.PlaySound("Obstacle_Box_Decrease", 0f, 1f);
        }

        public void ElimAddStep()
        {
            this.PlaySound("Obstacle_ExtraMove_Clear", 0f, 1f);
        }

        public void CollectToy()
        {
        }

        public void MusicExit()
        {
            this.PlaySound("Chandelier_Drop", 0f, 1f);
        }

        public void SprawJam()
        {
        }

        public void JamJarGrowUp()
        {
        }

        public void JamJarEruption()
        {
        }

        public void CreateHoneyJar()
        {
        }

        public void BreakHoneyJar()
        {
        }

        public void SplashHoney()
        {
        }

        public void CrushHoney()
        {
        }

        public void BubbleNozzleGrowUp()
        {
        }

        public void UsePropSpoon()
        {
            this.PlaySound("Gameplay_SpoonHit", 0f, 1f);
        }

        public void UsePropColorBomb()
        {
        }

        public void GetRequireStar()
        {
        }

        public void ButtonOn()
        {
            this.PlaySound("UI_Button_Regular", 0f, 1f);
        }

        public void ButtonOff()
        {
            this.PlaySound("UI_Button_Regular", 0f, 1f);
        }

        public void ToggleButtonOn()
        {
            this.ButtonOn();
        }

        public void PropButtonSelect()
        {
            this.ButtonOn();
        }

        public void SetButtonOn()
        {
            this.ButtonOn();
        }

        public void SetButtonOff()
        {
            this.ButtonOff();
        }

        public void MapLevelButtonOn()
        {
            this.ButtonOn();
        }

        public void WindowOpen()
        {
            this.PlaySound("UI_Popup_Open", 0f, 1f);
        }

        public void WindowClose()
        {
            this.PlaySound("UI_Popup_Close", 0f, 1f);
        }

        public void PopupWindowDown()
        {
        }

        public void PopupWindowUp()
        {
        }

        public void UnlockLevel()
        {
        }

        public void StarStamp1()
        {
        }

        public void StarStamp2()
        {
        }

        public void StarStamp3()
        {
        }

        public void BalloonFlyAway()
        {
            this.PlaySound("Balloon_Fly_Away_Ver_1_Nrm", 0f, 1f);
        }

        public void ElimButton()
        {
            this.PlaySound("Button_And_String_Decrease_Ver_1_Nrm", 0f, 1f);
        }

        public void HarpElim()
        {
            this.PlaySound("Harp_Match_Adjacent_Ver_1_Nrm", 0f, 1f);
        }

        public void BucketSprayOut()
        {
            this.PlaySound("Purple_Box_Spray_Out_Ver_1_Nrm", 0f, 1f);
        }

        public void StoneElim(int hp)
        {
            if (hp == 0)
            {
                this.PlaySound("Stone_Clear_Ver_1_Nrm", 0f, 1f);
            }
            else
            {
                this.PlaySound("Stone_Crack_Ver_1_Nrm", 0f, 1f);
            }
        }

        public void PlayFireworkSound(FireworkSoundType type)
        {
            this.PlaySound(this.fireworkSoundNames[(int)type], 0f, 1f);
        }

        public void PlayCandelabraSound(CandelabraSoundType type)
        {
            this.PlaySound(this.candelabraSoundNames[(int)type], 0f, 1f);
        }

        public void PlayClockSound(ClockSoundType type)
        {
            this.PlaySound(this.clockSoundNames[(int)type], 0f, 1f);
        }

        public void PlayUmbrellaSound(UmbrellaSoundType type, float delayTime = 0f)
        {
            this.PlaySound(this.umbrellaSoundNames[(int)type], delayTime, 1f);
        }

        public void PlayTileSound(TileSoundType type, float delay)
        {
            this.PlaySound(this.fileSoundNames[(int)type], delay, 1.5f);
        }

        public void PlayTableSound(TableSoundType type, float delay = 0f)
        {
            this.PlaySound(this.tableSoundNames[(int)type], delay, 1f);
        }

        public void PlayLampSound(LampSoundType type, float delay = 0f)
        {
            this.PlaySound(this.lampSoundNames[(int)type], delay, 1f);
        }

        public void PlaySoundCuckoo(CuckooSoundType type, float delay = 0f)
        {
            this.PlaySound(this.cuckooSoundNames[(int)type], delay, 1f);
        }

        public void PlaySoundDoorAndKey(DoorAndKeySoundType type, float delay = 0f)
        {
            this.PlaySound(this.DoorAndKeySoundNames[(int)type], delay, 1f);
        }

        public void PlaySoundStatue(StatueSoundType type)
        {
            this.PlaySound(this.StatueSoundNames[(int)type], 0f, 1f);
        }

        public void PlaySoundTelephone(TelephoneSoundType type, float delay = 0f)
        {
            this.PlaySound(this.TelephoneSoundNames[(int)type], delay, 1f);
        }

        public void PlaySoundLanwmower(LawnmowerSoundType type, float delay = 0f)
        {
            this.PlaySound(this.LawnmowerSoundNames[(int)type], delay, 1f);
        }

        public void PlaySoundCurtains()
        {
            this.PlaySound("Curtain_SFX_Ver", 0.1f, 1f);
        }

        public void PlaySoundRedCarpet()
        {
            this.PlaySound("Carpet_Unroll_Ver", 0f, 1f);
        }

        public void PlaySoundToyTrain(ToyTrainSoundType type, float delay)
        {
            this.PlaySound(this.ToyTrainSoundNames[(int)type], delay, 1f);
        }

        public void AddFiveStep()
        {
            this.PlaySound("Gameplay_AddMoves", 0f, 1f);
        }

        public void GetDiamonds()
        {
        }

        public void YouDidIt()
        {
            this.PlaySound("VO-You did it", 0f, 1f);
        }

        public void GirlMove()
        {
        }

        public void GirlEatLollipop()
        {
        }

        public void GirlFly()
        {
        }

        public void GirlInTeleporter()
        {
        }

        public void GirlOutTeleporter()
        {
        }

        public void GainCoins()
        {
            this.PlaySound("Economy_Coins_Gain", 0f, 1f);
        }

        public void SpendCoins()
        {
            this.PlaySound("Economy_Coins_Spend", 0f, 1f);
        }

        public void GainLife()
        {
            this.PlaySound("Economy_Hearts_Gain", 0f, 1f);
        }

        public void GainStar()
        {
            this.PlaySound("Economy_Stars_Gain", 0f, 1f);
        }

        public void SpendStar()
        {
            this.PlaySound("Economy_Stars_Spend", 0f, 1f);
        }

        public void Hammering()
        {
            this.PlaySound("Hammering Ver 1", 0f, 1f);
        }

        public void PlayAnimStateSoundEffect(string sound)
        {
            this.PlaySound(sound, 0f, 1f);
        }

        public void WallPaperTransition()
        {
            this.PlaySound("VFX_WallpaperTransition", 0f, 1f);
        }

        private bool SetStartTime(int audio)
        {
            if (!this._startTimes.ContainsKey(audio))
            {
                this._startTimes[audio] = 0f;
            }
            if (Time.time - this._startTimes[audio] > 0.1f)
            {
                this._startTimes[audio] = Time.time;
                return true;
            }
            return false;
        }

        public void OpenDailyChest()
        {
            this.OpenGiftBox();
        }

        public void OpenGiftBox()
        {
            this.PlaySound("VFX_OpenGiftBox", 0f, 1f);
        }

        public void PlayIceCreanDrop()
        {
            this.PlaySoundBundle("Other/Addon/DailyEventFoodTruck/Ice_Cream_Drop_SFX.wav", 1.45f, 1f);
        }

        public void PlayIceCreanRewardFlyIn()
        {
            this.PlaySoundBundle("Other/Addon/DailyEventFoodTruck/Reward_Fly_In.wav", 0f, 1f);
        }
    }



    /*
    // Is alternative restored code with simple decompilation options:
    using BDUnity;
    using BDUnity.Actions;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class SoundManager : MonoBehaviour
    {
        public enum FireworkSoundType
        {
            Explore1,
            Explore2,
            Explore3
        }

        public enum CandelabraSoundType
        {
            LitFirstOrSecondCandle,
            LitThirdCandle,
            Fire
        }

        public enum ClockSoundType
        {
            Countdown,
            ExtraMoves,
            Match,
            TimeUp
        }

        public enum UmbrellaSoundType
        {
            Celected,
            Match,
            Explosion
        }

        public enum TileSoundType
        {
            Collect
        }

        public enum TableSoundType
        {
            Match,
            Collected
        }

        public enum LampSoundType
        {
            TurnOn,
            ClearAndCollect
        }

        public enum CuckooSoundType
        {
            Close,
            Open,
            Clear
        }

        public enum DoorAndKeySoundType
        {
            ClearKey,
            ClearLock,
            OpenDoor
        }

        public enum StatueSoundType
        {
            Clear,
            Decrease
        }

        public enum TelephoneSoundType
        {
            Clear,
            Ring
        }

        public enum LawnmowerSoundType
        {
            Chargging,
            Trigger
        }

        public enum ToyTrainSoundType
        {
            Forward,
            Collect
        }

        [CompilerGenerated]
        private sealed class _003CDoPlayOrderedBgm_003Ec__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int _003Ci_003E__0;

            internal string[] music;

            internal SoundManager _0024this;

            internal object _0024current;

            internal bool _0024disposing;

            internal int _0024PC;

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this._0024current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this._0024current;
                }
            }

            [DebuggerHidden]
            public _003CDoPlayOrderedBgm_003Ec__Iterator0()
            {
            }

            public bool MoveNext()
            {
                uint num = (uint)this._0024PC;
                this._0024PC = -1;
                switch (num)
                {
                    case 0u:
                        this._003Ci_003E__0 = 0;
                        goto IL_00bd;
                    case 1u:
                        this._003Ci_003E__0++;
                        goto IL_00bd;
                    default:
                        {
                            return false;
                        }
                        IL_00bd:
                        if (this._003Ci_003E__0 < this.music.Length)
                        {
                            if (this._003Ci_003E__0 == this.music.Length - 1)
                            {
                                this._0024this.PlayBGMusic(this.music[this._003Ci_003E__0], true, false);
                                goto case 1u;
                            }
                            this._0024this.PlayBGMusic(this.music[this._003Ci_003E__0], false, false);
                            this._0024current = new WaitForSeconds(this._0024this.bgMusic.clip.length);
                            if (!this._0024disposing)
                            {
                                this._0024PC = 1;
                            }
                            break;
                        }
                        this._0024PC = -1;
                        goto default;
                }
                return true;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this._0024disposing = true;
                this._0024PC = -1;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }
        }

        [SerializeField]
        private AudioSource bgMusic;

        [SerializeField]
        private AudioSource sound;

        private string bgMusicName;

        private AudioClipsHolder common;

        private AudioClipsHolder sceneBased;

        private Dictionary<int, float> _startTimes;

        private const float MIN_INTERVAL = 0.1f;

        private bool _enableSound = true;

        private bool fadeInning;

        private bool fadeOuting;

        private float fadeInOutTimeMax = 1f;

        private float fadeInOutTime;

        private static SoundManager _instance;

        private IEnumerator orderBgm;

        private string _currLevelBgMusic = string.Empty;

        private string[] levelBgMusics = new string[2] {
            "Matchington Mansion Gameplay Loopable Ver 2 Mastered",
            "Other/Addon/Musics/Matchington_BGM_3_Ver_4.wav"
        };

        private readonly Dictionary<string, string> COMBOS = new Dictionary<string, string> {
            {
                "big_1",
                "VO-Luxurious"
            },
            {
                "big_2",
                "VO-Gorgeous"
            },
            {
                "middle_1",
                "VO-Outstanding"
            },
            {
                "middle_2",
                "VO-Brilliant"
            },
            {
                "middle_3",
                "VO-Magnificent"
            },
            {
                "middle_4",
                "VO-Sensational"
            },
            {
                "small_1",
                "VO-Fantastic"
            },
            {
                "small_2",
                "VO-Excellent"
            },
            {
                "small_3",
                "VO-Fancy"
            },
            {
                "small_4",
                "VO-Great Job"
            },
            {
                "small_5",
                "VO-Smashing"
            }
        };

        private string[] fireworkSoundNames = new string[3] {
            "Firework_Explode_1_Ver_1",
            "Firework_Explode_2_Ver_1",
            "Firework_Explode_3_Ver_1"
        };

        private string[] candelabraSoundNames = new string[3] {
            "Candelabra_Lit_1_Ver_1",
            "Candelabra_Lit_2_Ver_1",
            "Candelabra_Ver_1"
        };

        private string[] clockSoundNames = new string[4] {
            "Alarm_Clock_Countdown_Ver_1",
            "Alarm_Clock_Extra_Moves_Ver_1",
            "Alarm_Clock_Match_Ver_1",
            "Alarm_Clock_TimeUp_Ver_1"
        };

        private string[] umbrellaSoundNames = new string[3] {
            "Umbrella_Select_Ver_1",
            "Umbrella_Match_Ver_1",
            "Umbrella_Explosion_Ver_1"
        };

        private string[] fileSoundNames = new string[1] {
            "Tile_Collect_Ver_2"
        };

        private string[] tableSoundNames = new string[2] {
            "Tables_Match_Ver_1",
            "Tables_Collected_Ver_1"
        };

        private string[] lampSoundNames = new string[2] {
            "Lamp_Turn_On_Ver_1",
            "Lamp_Clear_Ver_1"
        };

        private string[] cuckooSoundNames = new string[3] {
            "Cuckoo_Close_SFX_Ver_1",
            "Cuckoo_Open_SFX_Ver_1",
            "Cuckoo_Clear_SFX_Ver_1"
        };

        private string[] DoorAndKeySoundNames = new string[3] {
            "Key_Clear_Ver_1",
            "Key_Hit_Door_Ver_1",
            "Door_Open_Ver_2"
        };

        private string[] StatueSoundNames = new string[2] {
            "Statue_Clear_Ver_3",
            "Statue_Decrease_Ver_1"
        };

        private string[] TelephoneSoundNames = new string[2] {
            "Telephone_Clear_Ver_2",
            "Telephone_Tap_Ver_1"
        };

        private string[] LawnmowerSoundNames = new string[2] {
            "Vacuum_Charging",
            "Vacuum_Trigger"
        };

        private string[] ToyTrainSoundNames = new string[2] {
            "Train_Forward",
            "Toy_Train_Arrive"
        };

        public static SoundManager Instance
        {
            get
            {
                return SoundManager._instance;
            }
        }

        public string CurLevelBgMusic
        {
            get
            {
                int num = 0;
                if ((UnityEngine.Object)Singleton<GameManager>.Instance != (UnityEngine.Object)null && Singleton<GameManager>.Instance.currLevel > 0)
                {
                    num = (Singleton<GameManager>.Instance.currLevel + 1) % this.levelBgMusics.Length;
                }
                if (num > 0 && ((UnityEngine.Object)Singleton<AssetBundleManager>.Instance == (UnityEngine.Object)null || Singleton<AssetBundleManager>.Instance.HasMissingAssets(false, this.levelBgMusics[num])))
                {
                    num = 0;
                }
                return this.levelBgMusics[num];
            }
        }

        public string toyTrainForwardName
        {
            get
            {
                return this.ToyTrainSoundNames[0];
            }
        }

        protected SoundManager()
        {
        }

        public static void InitInstance()
        {
            if ((UnityEngine.Object)SoundManager._instance == (UnityEngine.Object)null)
            {
                GameObject gameObject = AssetUtility.InstantiatePrefab("SoundManager", true, true, true);
                SoundManager._instance = gameObject.GetComponent<SoundManager>();
            }
        }

        private void Awake()
        {
            this.init();
        }

        private void init()
        {
            this._startTimes = new Dictionary<int, float>();
            if ((UnityEngine.Object)this.common == (UnityEngine.Object)null)
            {
                this.common = this.LoadAudioHolder("Common_Sounds");
            }
            UnityEngine.Object.DontDestroyOnLoad(this);
        }

        private AudioClipsHolder LoadAudioHolder(string name)
        {
            string assetName = "SoundsHop/" + name;
            GameObject gameObject = AssetUtility.InstantiatePrefab(assetName, true, true, true);
            gameObject.transform.SetParent(base.transform);
            return gameObject.GetComponent<AudioClipsHolder>();
        }

        public void OnSceneSwitched(SceneType type)
        {
            string text = null;
            switch (type)
            {
                case SceneType.AreaScene:
                    text = "Map_Sounds";
                    break;
                case SceneType.PlayScene:
                    text = "Level_Sounds";
                    break;
            }
            if (text != null)
            {
                if ((UnityEngine.Object)this.sceneBased != (UnityEngine.Object)null)
                {
                    if (this.sceneBased.name == text)
                    {
                        return;
                    }
                    UnityEngine.Object.Destroy(this.sceneBased.gameObject);
                }
                this.sceneBased = this.LoadAudioHolder(text);
            }
        }

        private void Start()
        {
            ModelBinder.BindProp(0.ToString(), new ModelBinder.PropChangedListener(this.OnMusicChanged));
        }

        private void Update()
        {
            if (this.fadeOuting)
            {
                this.fadeInning = false;
                this.fadeInOutTime += Time.deltaTime;
                if (this.fadeInOutTime >= this.fadeInOutTimeMax)
                {
                    this.fadeOuting = false;
                    this.bgMusic.volume = 0f;
                    this.bgMusic.Stop();
                }
                else
                {
                    this.bgMusic.volume = 1f - this.fadeInOutTime / this.fadeInOutTimeMax;
                }
            }
            if (this.fadeInning)
            {
                this.fadeInOutTime += Time.deltaTime;
                if (this.fadeInOutTime >= this.fadeInOutTimeMax)
                {
                    this.fadeInning = false;
                    this.bgMusic.volume = 1f;
                }
                else
                {
                    this.bgMusic.volume = this.fadeInOutTime / this.fadeInOutTimeMax;
                }
            }
        }

        private void OnDestroy()
        {
            ModelBinder.UnbindProp(0.ToString(), new ModelBinder.PropChangedListener(this.OnMusicChanged));
        }

        private void OnMusicChanged(object changeValue)
        {
            if ((bool)changeValue)
            {
                this.ResumeBGMusic();
            }
            else
            {
                this.StopBGMusic();
            }
        }

        public void PlayBGMusic(string name, bool loop = true, bool overrideMode = true)
        {
            if (overrideMode)
            {
                this.StopOrderedBgm();
            }
            bool flag = true;
            if (this.bgMusicName != name)
            {
                this.bgMusic.clip = this.LoadMusicClip(name);
            }
            else if (this.bgMusic.isPlaying)
            {
                flag = false;
            }
            if (this.fadeOuting)
            {
                this.fadeOuting = false;
                this.fadeInOutTime = 0f;
                flag = true;
            }
            this.bgMusicName = name;
            this.bgMusic.loop = loop;
            if (flag && Singleton<GameManager>.Instance.User.MusicEnabled)
            {
                this.bgMusic.Play();
                this.fadeInning = true;
                this.fadeInOutTime = 0f;
                this.bgMusic.volume = 0f;
            }
        }

        private AudioClip LoadMusicClip(string namePath)
        {
            if (namePath.Contains("Addon"))
            {
                return Singleton<AssetBundleManager>.Instance.LoadAsset<AudioClip>(namePath, null);
            }
            return (AudioClip)Resources.Load("Musics/" + namePath, typeof(AudioClip));
        }

        public void StopBGMusic()
        {
            if (!this.fadeOuting && this.bgMusic.isPlaying)
            {
                this.fadeOuting = true;
                this.fadeInOutTime = 0f;
                this.bgMusic.volume = 1f;
            }
        }

        public void ResumeBGMusic()
        {
            if (this.bgMusicName != null)
            {
                this.PlayBGMusic(this.bgMusicName, this.bgMusic.loop, true);
            }
        }

        public void SetBGVol(float vol)
        {
            if ((UnityEngine.Object)this.bgMusic != (UnityEngine.Object)null)
            {
                this.bgMusic.volume = vol;
            }
        }

        public float GetBGVol()
        {
            return (!((UnityEngine.Object)this.bgMusic == (UnityEngine.Object)null)) ? this.bgMusic.volume : 0f;
        }

        private void PlayOrderedBgm(params string[] music)
        {
            this.StopOrderedBgm();
            this.orderBgm = this.DoPlayOrderedBgm(music);
            base.StartCoroutine(this.orderBgm);
        }

        private void StopOrderedBgm()
        {
            if (this.orderBgm != null)
            {
                base.StopCoroutine(this.orderBgm);
                this.orderBgm = null;
            }
        }

        [DebuggerHidden]
        private IEnumerator DoPlayOrderedBgm(params string[] music)
        {
            _003CDoPlayOrderedBgm_003Ec__Iterator0 _003CDoPlayOrderedBgm_003Ec__Iterator = new _003CDoPlayOrderedBgm_003Ec__Iterator0();
            _003CDoPlayOrderedBgm_003Ec__Iterator.music = music;
            _003CDoPlayOrderedBgm_003Ec__Iterator._0024this = this;
            return _003CDoPlayOrderedBgm_003Ec__Iterator;
        }

        public void PlayMapBGM(MapType mapType)
        {
            switch (mapType)
            {
                case MapType.House:
                    this.PlayBGMusic("BGM_map", true, true);
                    break;
                case MapType.Island:
                    this.PlayBGMusic("Other/Addon/environment/Default/IslandDefault/Music/Tropical_BGM_Ver_6_Loopable.wav", true, true);
                    break;
                case MapType.BeachParty:
                    this.PlaySocialBGM();
                    break;
                default:
                    this.PlayBGMusic("BGM_map", true, true);
                    break;
            }
        }

        public void PlayLevelStartMelody()
        {
            this._currLevelBgMusic = this.CurLevelBgMusic;
            this.PlayOrderedBgm("Victory Loop Begin Ver 1", this._currLevelBgMusic);
        }

        public void PlayLevelContinue()
        {
            this.PlayOrderedBgm("Extra Moves Ver 1", this._currLevelBgMusic);
        }

        public void PlayOpeningMelody()
        {
            this.PlayOrderedBgm("Opening Melody Ver 1", "Nature_Ambience");
        }

        public void PlayPurchaseCoins()
        {
            this.PlayOrderedBgm("Purchase_Coins", "BGM_map");
        }

        public void PlayPurchaseCoinsInLevel()
        {
            this.PlayOrderedBgm("Purchase_Coins", this._currLevelBgMusic);
        }

        public void PlayJammingTimeBGM()
        {
            this.PlayOrderedBgm("Level_Start_Melody", "Victory Loop Ver 1");
        }

        public void PlayLevelWinBGM()
        {
            this.PlayBGMusic("Victory Loop End Ver 1", false, true);
        }

        public void PlayLevelFailBGM()
        {
            this.PlayBGMusic("Defeat", false, true);
        }

        public void PlaySocialBGM()
        {
            this.PlayBGMusic("Matchington_Mansion_Social_Loop", true, true);
        }

        public void PlayAdd5MovesBGM()
        {
            this.PlayBGMusic("Out_Of_Moves", false, true);
        }

        public AudioClip GetCatchedAudioClip(string name)
        {
            AudioClip audioClip = this.common.TryGetAudioClip(name);
            if ((UnityEngine.Object)audioClip == (UnityEngine.Object)null)
            {
                audioClip = this.sceneBased.TryGetAudioClip(name);
            }
            return audioClip;
        }

        public void PlaySound(string name, float delay = 0f, float volumeScale = 1f)
        {
            AudioClip audioClip = this.common.TryGetAudioClip(name);
            if ((UnityEngine.Object)audioClip == (UnityEngine.Object)null)
            {
                audioClip = this.sceneBased.TryGetAudioClip(name);
            }
            if ((UnityEngine.Object)audioClip != (UnityEngine.Object)null)
            {
                this.PlaySound(audioClip, delay, volumeScale);
            }
        }

        public void PlaySoundBundle(string path, float delay = 0f, float volumeScale = 1f)
        {
            AudioClip audioClip = Singleton<AssetBundleManager>.Instance.LoadAsset<AudioClip>(path, null);
            if ((UnityEngine.Object)audioClip != (UnityEngine.Object)null)
            {
                this.PlaySound(audioClip, delay, volumeScale);
            }
        }

        public void PlaySound(AudioClip clip, float delay = 0f, float volumeScale = 1f)
        {
            if (this._enableSound)
            {
                UserModel user = Singleton<GameManager>.Instance.User;
                if (user != null && user.SoundEnabled && !((UnityEngine.Object)clip == (UnityEngine.Object)null))
                {
                    if (delay > 0f)
                    {
                        base.gameObject.RunActions(new KDelayTime(delay), new BDCallFuncO(delegate(object obj)
                        {
                            this.PlaySound((AudioClip)obj, 0f, volumeScale);
                        }, clip));
                    }
                    else if (this.SetStartTime(clip.GetInstanceID()))
                    {
                        this.sound.PlayOneShot(clip, volumeScale);
                    }
                }
            }
        }

        public void SwapAction()
        {
        }

        public void SwapFail()
        {
            this.PlaySound("Swap Pieces Fail Ver 1", 0f, 1f);
        }

        public void Shuffle()
        {
            this.PlaySound("Gameplay_Shuffle", 0.55f, 1f);
        }

        public void MoveHit()
        {
            this.PlaySound("Match_Settle", 0f, 1f);
        }

        public void ElimElement(int count, float delay = 0f)
        {
            if (count == 1)
            {
                this.PlaySound("Match Base Ver 1", 0f, 1f);
            }
            else
            {
                string name = "Match " + (count - 1).ToString() + " Ver 2";
                this.PlaySound(name, 0f, 1f);
            }
        }

        public void Combo(string animSkinName)
        {
            if (this.COMBOS.ContainsKey(animSkinName))
            {
                this.PlaySound(this.COMBOS[animSkinName], 0f, 1f);
            }
        }

        public void CreateStripedBomb()
        {
            this.PlaySound("Powerup_Create", 0f, 1f);
        }

        public void CreateSquareBomb()
        {
            this.PlaySound("Powerup_Create", 0f, 1f);
        }

        public void CreateCrossBomb()
        {
            this.PlaySound("Powerup_Create", 0f, 1f);
        }

        public void CreateColorBomb()
        {
            this.PlaySound("Powerup_Create_Rainbow", 0f, 1f);
        }

        public void CreateQuadBomb()
        {
        }

        public void LargeBombExplode()
        {
            this.PlaySound("Powerup_LargeBomb", 0f, 1f);
        }

        public void ExtraLargeBombExplode()
        {
            this.PlaySound("Powerup_ExtraLargeBomb", 0f, 1f);
        }

        public void StripedBombExplode()
        {
            this.PlaySound("Powerup_LineBlaster", 0f, 1f);
        }

        public void SquareBombExplode()
        {
            this.PlaySound("Powerup_SmallBomb", 0f, 1f);
        }

        public void ColorBombExplode()
        {
            this.PlaySound("Rainbow Plus Match Anything", 0f, 1f);
        }

        public void RainbowBombBoomStep(string step)
        {
            if (step == "Combine")
            {
                this.PlaySound("Powerup_Rainbow_ColorTransformation", 0f, 1f);
            }
            else if (step == "StartTrail")
            {
                this.PlaySound("Powerup_Rainbow_Lines", 0f, 1f);
            }
            else if (step == "StartEliminate")
            {
                this.PlaySound("Powerup_Rainbow_Explosion", 0f, 1f);
            }
        }

        public void ColorBombExplodeOther()
        {
        }

        public void QuadBombExplode()
        {
        }

        public void CrossBombExplode()
        {
            this.PlaySound("Powerup_Combo_CrossBlast", 0f, 1f);
        }

        public void SquareSquareBombExplode()
        {
        }

        public void SquareStripedBombExplode()
        {
            this.PlaySound("Powerup_Combo_WideCrossBlast", 0f, 1f);
        }

        public void ColorColorBombExplode()
        {
            this.PlaySound("Powerup_DoubleRainbow", 0f, 1f);
        }

        public void LinePlusSmallCombo()
        {
            this.PlaySound("Match 4 Line Plus Match 4 Bomb", 0f, 1f);
        }

        public void LargePlusLargeCombo()
        {
            this.PlaySound("Powerup_ExtraLargeBomb", 0f, 1f);
        }

        public void LargePlusSmallCombo()
        {
            this.PlaySound("L and T Plus Match 4 Bomb Ver 1", 0f, 1f);
        }

        public void QuadSquareBombExplode()
        {
        }

        public void ElimJelly()
        {
        }

        public void ThroughOutTeleporter()
        {
        }

        public void ConveyorMove()
        {
        }

        public void DyeRugTakeEffect()
        {
        }

        public void CreateBubble()
        {
        }

        public void CollectBubble()
        {
        }

        public void PopcornBucketGrowUp()
        {
        }

        public void PopcornBucketEruption()
        {
        }

        public void CreatePopcorn()
        {
        }

        public void ElimPopcorn()
        {
        }

        public void SprawlCotton()
        {
            this.PlaySound("Obstacle_Soap_Spread", 0f, 1f);
        }

        public void ElimCotton()
        {
            this.PlaySound("Obstacle_Soap_Clear", 0f, 1f);
        }

        public void ElimBook()
        {
            this.PlaySound("Obstacle_Book_Decrease", 0f, 1f);
        }

        public void ElimMiceBox(int hp)
        {
            this.PlaySound("Obstacle_Frame_Decrease", 0f, 1f);
        }

        public void MouseMove()
        {
        }

        public void ElimTeaCup()
        {
            this.PlaySound("Obstacle_Teacup_Clear", 0f, 1f);
        }

        public void BreakPretzel()
        {
            this.PlaySound("Obstacle_Rope_Decrease", 0f, 1f);
        }

        public void MirrorTransform()
        {
            this.PlaySound("Obstacle_Mirror_Transform", 0f, 1f);
        }

        public void CrushIceLayer(bool withEnvelope)
        {
            this.PlaySound("Obstacle_Glass_Decrease", 0f, 1f);
        }

        public void MonsterFly()
        {
        }

        public void MonsterEat()
        {
        }

        public void MonsterDizzy()
        {
        }

        public void MonsterRevive()
        {
        }

        public void ElimMonster()
        {
        }

        public void AngryBirdShowSelf()
        {
        }

        public void AngryBirdFly()
        {
        }

        public void TargetCompleted()
        {
            this.PlaySound("Target_Complete", 0f, 1f);
        }

        public void CollectElement()
        {
            this.PlaySound("Target_Collect", 0f, 1f);
        }

        public void CollectMouse()
        {
            this.PlaySound("Mouse Collect Ver 1", 0f, 1f);
        }

        public void CollectBear()
        {
            this.PlaySound("Bear Collect Ver 1", 0f, 1f);
        }

        public void ElimAntiqueBox(int hp)
        {
            this.PlaySound("Obstacle_Box_Decrease", 0f, 1f);
        }

        public void ElimAddStep()
        {
            this.PlaySound("Obstacle_ExtraMove_Clear", 0f, 1f);
        }

        public void CollectToy()
        {
        }

        public void MusicExit()
        {
            this.PlaySound("Chandelier_Drop", 0f, 1f);
        }

        public void SprawJam()
        {
        }

        public void JamJarGrowUp()
        {
        }

        public void JamJarEruption()
        {
        }

        public void CreateHoneyJar()
        {
        }

        public void BreakHoneyJar()
        {
        }

        public void SplashHoney()
        {
        }

        public void CrushHoney()
        {
        }

        public void BubbleNozzleGrowUp()
        {
        }

        public void UsePropSpoon()
        {
            this.PlaySound("Gameplay_SpoonHit", 0f, 1f);
        }

        public void UsePropColorBomb()
        {
        }

        public void GetRequireStar()
        {
        }

        public void ButtonOn()
        {
            this.PlaySound("UI_Button_Regular", 0f, 1f);
        }

        public void ButtonOff()
        {
            this.PlaySound("UI_Button_Regular", 0f, 1f);
        }

        public void ToggleButtonOn()
        {
            this.ButtonOn();
        }

        public void PropButtonSelect()
        {
            this.ButtonOn();
        }

        public void SetButtonOn()
        {
            this.ButtonOn();
        }

        public void SetButtonOff()
        {
            this.ButtonOff();
        }

        public void MapLevelButtonOn()
        {
            this.ButtonOn();
        }

        public void WindowOpen(int type = 1)
        {
            this.PlaySound("UI_Popup_Open_" + type, 0f, 1f);
        }

        public void WindowClose(int type = 1)
        {
            this.PlaySound("UI_Popup_Close_" + type, 0f, 1f);
        }

        public void PopupWindowDown()
        {
        }

        public void PopupWindowUp()
        {
        }

        public void UnlockLevel()
        {
        }

        public void StarStamp1()
        {
        }

        public void StarStamp2()
        {
        }

        public void StarStamp3()
        {
        }

        public void BalloonFlyAway()
        {
            this.PlaySound("Balloon_Fly_Away_Ver_1_Nrm", 0f, 1f);
        }

        public void ElimButton()
        {
            this.PlaySound("Button_And_String_Decrease_Ver_1_Nrm", 0f, 1f);
        }

        public void HarpElim()
        {
            this.PlaySound("Harp_Match_Adjacent_Ver_1_Nrm", 0f, 1f);
        }

        public void BucketSprayOut()
        {
            this.PlaySound("Purple_Box_Spray_Out_Ver_1_Nrm", 0f, 1f);
        }

        public void PlayTapOnElementSound(ElementType type)
        {
            switch (type)
            {
                case ElementType.EndlessWell:
                    this.PlaySound("Harp_Tap_On_Ver_1_Nrm", 0f, 1f);
                    break;
                case ElementType.PopcornBucket:
                    this.PlaySound("Purple_Box_Tap_On_Ver_1_Nrm", 0f, 1f);
                    break;
            }
        }

        public void StoneElim(int hp)
        {
            if (hp == 0)
            {
                this.PlaySound("Stone_Clear_Ver_1_Nrm", 0f, 1f);
            }
            else
            {
                this.PlaySound("Stone_Crack_Ver_1_Nrm", 0f, 1f);
            }
        }

        public void PlayFireworkSound(FireworkSoundType type)
        {
            this.PlaySound(this.fireworkSoundNames[(int)type], 0f, 1f);
        }

        public void PlayCandelabraSound(CandelabraSoundType type)
        {
            this.PlaySound(this.candelabraSoundNames[(int)type], 0f, 1f);
        }

        public void PlayClockSound(ClockSoundType type)
        {
            this.PlaySound(this.clockSoundNames[(int)type], 0f, 1f);
        }

        public void PlayUmbrellaSound(UmbrellaSoundType type, float delayTime = 0f)
        {
            this.PlaySound(this.umbrellaSoundNames[(int)type], delayTime, 1f);
        }

        public void PlayTileSound(TileSoundType type, float delay)
        {
            this.PlaySound(this.fileSoundNames[(int)type], delay, 1.5f);
        }

        public void PlayTableSound(TableSoundType type, float delay = 0f)
        {
            this.PlaySound(this.tableSoundNames[(int)type], delay, 1f);
        }

        public void PlayLampSound(LampSoundType type, float delay = 0f)
        {
            this.PlaySound(this.lampSoundNames[(int)type], delay, 1f);
        }

        public void PlaySoundCuckoo(CuckooSoundType type, float delay = 0f)
        {
            this.PlaySound(this.cuckooSoundNames[(int)type], delay, 1f);
        }

        public void PlaySoundDoorAndKey(DoorAndKeySoundType type, float delay = 0f)
        {
            this.PlaySound(this.DoorAndKeySoundNames[(int)type], delay, 1f);
        }

        public void PlaySoundStatue(StatueSoundType type)
        {
            this.PlaySound(this.StatueSoundNames[(int)type], 0f, 1f);
        }

        public void PlaySoundTelephone(TelephoneSoundType type, float delay = 0f)
        {
            this.PlaySound(this.TelephoneSoundNames[(int)type], delay, 1f);
        }

        public void PlaySoundLanwmower(LawnmowerSoundType type, float delay = 0f)
        {
            this.PlaySound(this.LawnmowerSoundNames[(int)type], delay, 1f);
        }

        public void PlaySoundCurtains()
        {
            this.PlaySound("Curtain_SFX_Ver", 0.1f, 1f);
        }

        public void PlaySoundRedCarpet()
        {
            this.PlaySound("Carpet_Unroll_Ver", 0f, 1f);
        }

        public void PlaySoundToyTrain(ToyTrainSoundType type, float delay)
        {
            this.PlaySound(this.ToyTrainSoundNames[(int)type], delay, 1f);
        }

        public void AddFiveStep()
        {
            this.PlaySound("Gameplay_AddMoves", 0f, 1f);
        }

        public void GetDiamonds()
        {
        }

        public void YouDidIt()
        {
            this.PlaySound("VO-You did it", 0f, 1f);
        }

        public void GirlMove()
        {
        }

        public void GirlEatLollipop()
        {
        }

        public void GirlFly()
        {
        }

        public void GirlInTeleporter()
        {
        }

        public void GirlOutTeleporter()
        {
        }

        public void GainCoins()
        {
            this.PlaySound("Economy_Coins_Gain", 0f, 1f);
        }

        public void SpendCoins()
        {
            this.PlaySound("Economy_Coins_Spend", 0f, 1f);
        }

        public void GainLife()
        {
            this.PlaySound("Gain Life Ver 1", 0f, 1f);
        }

        public void GainStar()
        {
            this.PlaySound("Economy_Stars_Gain", 0f, 1f);
        }

        public void SpendStar()
        {
            this.PlaySound("Economy_Stars_Spend", 0f, 1f);
        }

        public void Hammering()
        {
            this.PlaySound("Hammering Ver 1", 0f, 1f);
        }

        public void PlayAnimStateSoundEffect(string sound)
        {
            this.PlaySound(sound, 0f, 1f);
        }

        public void WallPaperTransition()
        {
            this.PlaySound("VFX_WallpaperTransition", 0f, 1f);
        }

        private bool SetStartTime(int audio)
        {
            if (!this._startTimes.ContainsKey(audio))
            {
                this._startTimes[audio] = 0f;
            }
            if (Time.time - this._startTimes[audio] > 0.1f)
            {
                this._startTimes[audio] = Time.time;
                return true;
            }
            return false;
        }

        public void OpenDailyChest()
        {
            this.OpenGiftBox();
        }

        public void OpenGiftBox()
        {
            this.PlaySound("VFX_OpenGiftBox", 0f, 1f);
        }

        public void PlayIceCreanDrop()
        {
            this.PlaySoundBundle("Other/Addon/DailyEventFoodTruck/Ice_Cream_Drop_SFX.wav", 1.45f, 1f);
        }

        public void PlayIceCreanRewardFlyIn()
        {
            this.PlaySoundBundle("Other/Addon/DailyEventFoodTruck/Reward_Fly_In.wav", 0f, 1f);
        }
    }

    */

}
