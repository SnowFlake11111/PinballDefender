using MoreMountains.NiceVibrations;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using EventDispatcher;
using DG.Tweening;
#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif


public class GameController : MonoBehaviour
{
    #region Public Variables
    public static GameController Instance;

    public GameModeData gameModeData;
    public AchievementController gameAchievementController;
    public MoneyEffectController moneyEffectController;
    public UseProfile useProfile;
    public DataContain dataContain;
    public MusicManagerGameBase musicManager;
    public AdmobAds admobAds;

    public AnalyticsController AnalyticsController;
    public IapController iapController;

    public GameObject sceneTransitionScreen;

    [HideInInspector] public SceneType currentScene;

    [Space]
    [Header("-----IAP AND ADS CONTROLLER-----")]
    public bool activateAds;
    public bool activateIap;

    [Space]
    [Header("-----BOOSTERS CONTROLLER-----")]
    public bool unlockAllBoosters;
    #endregion

    #region Private Variables
    int defaultScreenHeight = 1920;
    int defaultScreenWidth = 1080;

    [NonSerialized] public float cooldownPerHeart = 1800;

    [SerializeField]float idleTimer = 0;

    Vector3 sceneTransitionScreenOgPos;
    Tweener sceneTransitionAnimation;
    #endregion


    #region Start, Update
    protected void Awake()
    {
        Instance = this;
        Init();

        DontDestroyOnLoad(this);

        //GameController.Instance.useProfile.IsRemoveAds = true;


#if UNITY_IOS

    if(ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == 
    ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
    {

        ATTrackingStatusBinding.RequestAuthorizationTracking();

    }

#endif

    }

    private void Start()
    {
        //   musicManager.PlayBGMusic();
    }

    //private void Update()
    //{
    //    RescaleCamSize();

    //    UnlimitedHeartCountDown();
    //    AdsCooldownUnlimitedHeartCountDown();
    //    HeartCooldownCountDown();

    //    if (!useProfile.IsRemoveAds && activateAds)
    //    {
    //        if (SceneManager.GetActiveScene().name == "HomeScene")
    //        {
    //            ActivateInter();
    //        }
    //    }
    //}
    #endregion

    #region Functions
    public void Init()
    {
        //Debug.LogError("GameController loaded");

        //Application.targetFrameRate = 60;
        //useProfile.CurrentLevelPlay = UseProfile.CurrentLevel;
        admobAds.Init();
        musicManager.Init();
        iapController.Init();

        MMVibrationManager.SetHapticsActive(useProfile.OnVibration);

        sceneTransitionScreenOgPos = sceneTransitionScreen.transform.position;

        //CheckRemainingUnlimitedHeartTime();
        //CheckRemainingCooldownAdsUnlimitedHeart();
        //CheckRemainingHeartCooldown();

        //this.RegisterListener(EventID.REMOVE_ADS, ChangeAdsState);
        //Debug.LogError("Ads listener registered");

        // GameController.Instance.admobAds.ShowBanner();
    }

    public void StartSceneTransition(string nextSceneName)
    {
        sceneTransitionAnimation = sceneTransitionScreen.transform.DOMoveY(0, 0.75f)
            .OnComplete(delegate
            {
                SceneManager.LoadScene(nextSceneName);
            });
    }

    public void FinishSceneTransition()
    {
        if (sceneTransitionAnimation != null)
        {
            sceneTransitionScreen.transform.DOMoveY(sceneTransitionScreenOgPos.y, 0.75f);
            sceneTransitionAnimation = null;
        }
    }

    void RescaleCamSize()
    {
        if(SceneManager.GetActiveScene().name == "GamePlay")
        {
            Camera.main.orthographicSize = 6f;
            //if (Screen.width > defaultScreenWidth || Screen.height > defaultScreenHeight)

            //if ((Screen.width / Screen.height) >= (18f / 9f))
            //{
            //    Camera.main.orthographicSize = 6f;
            //}
            //else
            //{
            //    Camera.main.orthographicSize = 5f;
            //}
        }
        else
        {
            Camera.main.orthographicSize = 5f;
        }
    }

    void UnlimitedHeartCountDown()
    {
        if(UseProfile.RemainingTimeForUnlimitedHeart > 0)
        {
            UseProfile.RemainingTimeForUnlimitedHeart -= Time.unscaledDeltaTime;

            if (UseProfile.RemainingTimeForUnlimitedHeart <= 0)
            {
                UseProfile.RemainingTimeForUnlimitedHeart = 0;
                UseProfile.Heart = UseProfile.MaxHeart;
            }
        }
    }

    void AdsCooldownUnlimitedHeartCountDown()
    {
        if (UseProfile.RemainingTimeForAdsCoolDownUnlimitedHeart > 0 )
        {
            UseProfile.RemainingTimeForAdsCoolDownUnlimitedHeart -= Time.unscaledDeltaTime;

            if (UseProfile.RemainingTimeForAdsCoolDownUnlimitedHeart <= 0)
            {
                UseProfile.RemainingTimeForAdsCoolDownUnlimitedHeart = 0;
            }
        }
    }

    void HeartCooldownCountDown()
    {
        if (UseProfile.RemainingTimeHeartCooldown > 0)
        {
            UseProfile.RemainingTimeHeartCooldown -= Time.unscaledDeltaTime;

            if (UseProfile.RemainingTimeHeartCooldown <= 0)
            {
                UseProfile.Heart++;
                if (UseProfile.Heart < UseProfile.MaxHeart)
                {
                    UseProfile.RemainingTimeHeartCooldown = cooldownPerHeart;
                }
                else
                {
                    UseProfile.RemainingTimeHeartCooldown = 0;
                }
            }
        }
    }

    void CheckRemainingHeartCooldown()
    {
        if (UseProfile.RemainingTimeHeartCooldown > 0 )
        {
            int gainedHeart = Mathf.RoundToInt((float)(DateTime.UtcNow - UseProfile.TimeSinceLastExit).TotalSeconds / cooldownPerHeart);

            if (UseProfile.Heart + gainedHeart >= UseProfile.MaxHeart)
            {
                UseProfile.Heart = UseProfile.MaxHeart;
                UseProfile.RemainingTimeHeartCooldown = 0;
            }
            else
            {
                UseProfile.Heart += gainedHeart;
                UseProfile.RemainingTimeHeartCooldown = Mathf.RoundToInt(UseProfile.RemainingTimeHeartCooldown - (float)(DateTime.UtcNow - UseProfile.TimeSinceLastExit).TotalSeconds % cooldownPerHeart);
            }
        }
    }

    void CheckRemainingUnlimitedHeartTime()
    {
        if(UseProfile.RemainingTimeForUnlimitedHeart > 0)
        {
            int remainingTimer = Mathf.RoundToInt(UseProfile.RemainingTimeForUnlimitedHeart - (float)(DateTime.UtcNow - UseProfile.TimeSinceLastExit).TotalSeconds);

            if (remainingTimer <= 0)
            {
                UseProfile.RemainingTimeForUnlimitedHeart = 0;
                UseProfile.RemainingTimeHeartCooldown = 0;
                UseProfile.Heart = UseProfile.MaxHeart;
            }
            else
            {
                UseProfile.RemainingTimeForUnlimitedHeart -= (float)(DateTime.UtcNow - UseProfile.TimeSinceLastExit).TotalSeconds;
            }
        }
    }

    void CheckRemainingCooldownAdsUnlimitedHeart()
    {
        if (UseProfile.RemainingTimeForAdsCoolDownUnlimitedHeart > 0)
        {
            int remainingTimer = Mathf.RoundToInt(UseProfile.RemainingTimeForAdsCoolDownUnlimitedHeart - (float)(DateTime.UtcNow - UseProfile.TimeSinceLastExit).TotalSeconds);

            if (remainingTimer <= 0)
            {
                UseProfile.RemainingTimeForAdsCoolDownUnlimitedHeart = 0;
            }
            else
            {
                UseProfile.RemainingTimeForAdsCoolDownUnlimitedHeart -= (float)(DateTime.UtcNow - UseProfile.TimeSinceLastExit).TotalSeconds;
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        Initiate.Fade(sceneName.ToString(), Color.black, 2f);
    }

    private void OnApplicationQuit()
    {
        UseProfile.TimeSinceLastExit = DateTime.UtcNow;
    }
    #endregion

    #region Inter Ads
    void ActivateInter()
    {
        if (idleTimer < 15)
        {
            idleTimer += Time.deltaTime;
            if (Input.anyKeyDown)
            {
                idleTimer = 0;
            }
        }
        else
        {
            if (Input.anyKeyDown)
            {
                admobAds.ShowInterstitial(actionWatchLog: "idle_interestitial_ads");
                idleTimer = 0;
            }
        }
    }
    #endregion
}
public enum SceneType
{
    StartLoading = 0,
    MainHome = 1,
    GamePlay = 2
}