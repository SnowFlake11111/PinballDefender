using DG.Tweening;
using DG.Tweening.Plugins.Options;
using EventDispatcher;
using MoreMountains.NiceVibrations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class HomeScene : BaseScene
{
    #region Old Codes
    //public Button btnSetting;

    //public Text tvCoin;

    //public Button btnCoin;

    //public HorizontalScrollSnap horizontalScrollSnap;
    //public List<MenuTabButton> lsMenuTabButtons;
    //public List<SceneBase> lsSceneBases;
    //public void ShowGift()
    //{


    //}
    //public int NumberPage(ButtonType buttonType)
    //{
    //    switch (buttonType)
    //    {
    //        case ButtonType.ShopButton:
    //            return 0;
    //            break;

    //        case ButtonType.HomeButton:
    //            return 1;
    //            break;

    //        case ButtonType.RankButton:
    //            return 2;
    //            break;

    //    }
    //    return 0;
    //}


    //public void Init()
    //{


    //    foreach (var item in lsMenuTabButtons)
    //    {
    //        item.Init(this);
    //    }
    //    HandleClickButton(ButtonType.HomeButton);

    //    foreach (var item in lsSceneBases)
    //    {
    //        item.Init();
    //    }
    //    tvCoin.text = "" + UseProfile.Coin;
    //    btnSetting.onClick.AddListener(delegate { GameController.Instance.musicManager.PlayClickSound(); OnSettingClick(); });
    //    EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.CHANGE_COIN, OnCoinChange);
    //    btnCoin.onClick.AddListener(delegate { HandleClickButton(ButtonType.ShopButton); });

    //}

    //public void HandleClickButton(ButtonType buttonType)
    //{

    //    GameController.Instance.musicManager.PlayClickSound();
    //    foreach (var item in lsMenuTabButtons)
    //    {
    //        item.GetBackToNormal();
    //    }

    //    foreach (var item in lsMenuTabButtons)
    //    {
    //        if (item.buttonType == buttonType)
    //        {
    //            item.GetSelected();
    //            ChangePage(NumberPage(item.buttonType));
    //            break;
    //        }
    //    }

    //    MMVibrationManager.Haptic(HapticTypes.MediumImpact);

    //}
    //private void ChangeTab(ButtonType buttonType)
    //{
    //    foreach (var item in lsMenuTabButtons)
    //    {
    //        item.GetBackToNormal();
    //    }
    //    foreach (var item in lsMenuTabButtons)
    //    {
    //        if (item.buttonType == buttonType)
    //        {
    //            item.GetSelected();
    //            break;
    //        }
    //    }
    //}

    //public void ChangePage(int param)
    //{

    //    horizontalScrollSnap.ChangePage(param);

    //}


    //public void OnScreenChange(int currentPage)
    //{
    //    switch (currentPage)
    //    {
    //        case 0:
    //            ChangeTab(ButtonType.ShopButton);

    //            break;
    //        case 1:
    //            ChangeTab(ButtonType.HomeButton);

    //            break;
    //        case 2:
    //            ChangeTab(ButtonType.RankButton);

    //            break;
    //    }
    //}



    //private void OnCoinChange(object param)
    //{
    //    tvCoin.text = "" + UseProfile.Coin;
    //}
    #endregion

    #region Public Variables

    [Header("-----TOP SCREEN-----")]
    public Text heartCounter;
    public Text heartRecoveryCountDown;
    public Text money;
    public Text star;
    public Text unlimitedHeartAdsCooldown;

    [Space]
    [Header("-----MID AND BOTTOM SCREEN-----")]
    public Button startGame;
    public Text currentLevel;

    [Space]
    [Header("-----BUTTONS-----")]
    public Button openSetting;
    public Button openShop;
    public Button openBuyHeart;
    public GameObject buyHeartIcon;
    public Button tempUnlimitedHeartAds;
    public Button openPackShop;

    #endregion

    #region Private Variables

    int hour, minute, second;
    int adsMinute, adsSecond;

    #endregion

    public void Init()
    {
        startGame.onClick.AddListener(delegate { StartGame(); });
        openShop.onClick.AddListener(delegate { OpenShop(); });
        openSetting.onClick.AddListener(delegate { OpenSetting(); });
        openBuyHeart.onClick.AddListener(delegate { OpenBuyHeart(); });
        tempUnlimitedHeartAds.onClick.AddListener(delegate { UnlimitedHeartAds(); });

        openPackShop.onClick.AddListener(delegate { OpenPackShop(); });

        ShowMoney();
        ShowStar();
        currentLevel.text = "Level " + (UseProfile.CurrentLevel + 1).ToString();

        InitState();

        GameController.Instance.musicManager.MusicTransition();

        this.RegisterListener(EventID.CHANGE_COIN, UpdateMoney);
        this.RegisterListener(EventID.CHANGE_STAR, UpdateMoney);
        this.RegisterListener(EventID.HEART_UPDATE, UpdateHeart);
    }

    void InitState()
    {
        //HeartChecker();
    }

    private void Update()
    {
        //UpdateTimer();
        //UpdateAdsCooldown();
    }

    void StartGame()
    {

    }

    void OpenShop()
    {
        //Open Shop pop-up
        GameController.Instance.musicManager.PlayClickSound();

    }

    private void OpenSetting()
    {
        GameController.Instance.musicManager.PlayClickSound();

        SettingBox.Setup().Show();
        //MMVibrationManager.Haptic(HapticTypes.MediumImpact);
    }

    void OpenBuyHeart()
    {
        GameController.Instance.musicManager.PlayClickSound();

    }

    void OpenPackShop()
    {
        GameController.Instance.musicManager.PlayClickSound();

    }

    public void UpdateTimer()
    {
        //Note to self: Use string.Format to reformat the timer after the countdown function is complete
        //Example: timer.text = string.Format("{0: 00}: {1: 00}", minute, second);

        #region Old timer
        //if (UseProfile.RemainingTimeForUnlimitedHeart > 0)
        //{
        //    heartCounter.text = "∞";

        //    if(UseProfile.RemainingTimeForUnlimitedHeart > 3600)
        //    {
        //        hour = Mathf.RoundToInt(UseProfile.RemainingTimeForUnlimitedHeart) / 3600;
        //        minute = (Mathf.RoundToInt(UseProfile.RemainingTimeForUnlimitedHeart) % 3600) / 60;
        //        second = Mathf.RoundToInt(UseProfile.RemainingTimeForUnlimitedHeart) % 60;

        //        heartRecoveryCountDown.text = string.Format(" {0: 00}: {1: 00} : {2: 00}", hour, minute, second);
        //    }
        //    else
        //    {
        //        minute = Mathf.RoundToInt(UseProfile.RemainingTimeForUnlimitedHeart) / 60;
        //        second = Mathf.RoundToInt(UseProfile.RemainingTimeForUnlimitedHeart) % 60;

        //        heartRecoveryCountDown.text = string.Format(" {0: 00}: {1: 00}", minute, second);
        //    }
        //}
        //else
        //{
        //    if (heartCountdown)
        //    {
        //        minute = Mathf.RoundToInt(currentCountdownTime) / 60;
        //        second = Mathf.RoundToInt(currentCountdownTime) % 60;

        //        heartRecoveryCountDown.text = string.Format(" {0: 00}: {1: 00}", minute, second);

        //        if (Mathf.RoundToInt(currentCountdownTime) <= 0)
        //        {
        //            UseProfile.Heart += 1;
        //            if (UseProfile.Heart < 5)
        //            {
        //                currentCountdownTime = secondsPerHeartRecovery;
        //            }
        //        }
        //    }
        //}
        #endregion

        if (UseProfile.RemainingTimeForUnlimitedHeart > 0)
        {
            if (UseProfile.RemainingTimeForUnlimitedHeart > 3600)
            {
                hour = Mathf.RoundToInt(UseProfile.RemainingTimeForUnlimitedHeart) / 3600;
                minute = (Mathf.RoundToInt(UseProfile.RemainingTimeForUnlimitedHeart) % 3600) / 60;
                second = Mathf.RoundToInt(UseProfile.RemainingTimeForUnlimitedHeart) % 60;

                heartRecoveryCountDown.text = string.Format(" {0: 00}: {1: 00} : {2: 00}", hour, minute, second);
            }
            else
            {
                minute = Mathf.RoundToInt(UseProfile.RemainingTimeForUnlimitedHeart) / 60;
                second = Mathf.RoundToInt(UseProfile.RemainingTimeForUnlimitedHeart) % 60;

                heartRecoveryCountDown.text = string.Format(" {0: 00}: {1: 00}", minute, second);
            }
        }
        else
        {
            if (UseProfile.Heart < UseProfile.MaxHeart)
            {
                minute = Mathf.RoundToInt(UseProfile.RemainingTimeHeartCooldown) / 60;
                second = Mathf.RoundToInt(UseProfile.RemainingTimeHeartCooldown) % 60;

                heartRecoveryCountDown.text = string.Format(" {0: 00}: {1: 00}", minute, second);
            }
            else
            {
                heartRecoveryCountDown.text = "FULL";
            }
        }
    }

    void UpdateAdsCooldown()
    {
        if (UseProfile.RemainingTimeForAdsCoolDownUnlimitedHeart > 0)
        {
            tempUnlimitedHeartAds.interactable = false;
            if (!unlimitedHeartAdsCooldown.gameObject.activeSelf)
            {
                unlimitedHeartAdsCooldown.gameObject.SetActive(true);
            }

            adsMinute = Mathf.RoundToInt(UseProfile.RemainingTimeForAdsCoolDownUnlimitedHeart) / 60;
            adsSecond = Mathf.RoundToInt(UseProfile.RemainingTimeForAdsCoolDownUnlimitedHeart) % 60;

            unlimitedHeartAdsCooldown.text = string.Format(" {0: 00}: {1: 00}", adsMinute, adsSecond);
        }
        else
        {
            tempUnlimitedHeartAds.interactable = true;
            if (unlimitedHeartAdsCooldown.gameObject.activeSelf)
            {
                unlimitedHeartAdsCooldown.gameObject.SetActive(false);
            }
        }
    }

    void UnlimitedHeartAds()
    {
        //Init ads
        GameController.Instance.musicManager.PlayClickSound();

        if (GameController.Instance.activateAds)
        {
            GameController.Instance.admobAds.ShowVideoReward(
            delegate { GrantUnlimitedHeart(); },
            delegate { FailedToGetReward(); },
            delegate { },
            ActionWatchVideo.SecondChanceAds,
            UseProfile.CurrentLevel.ToString()
            );
        }
        else
        {
            GrantUnlimitedHeart();
        }
        

        void GrantUnlimitedHeart()
        {
            UseProfile.RemainingTimeForUnlimitedHeart += 900;
            UseProfile.RemainingTimeForAdsCoolDownUnlimitedHeart += 3600;

            UseProfile.RemainingTimeHeartCooldown = 0;

            GameController.Instance.AnalyticsController.PostWatchUnlimitedHeartAds();
            HeartChecker();
        }

        void FailedToGetReward()
        {
            GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp
                        (
                        tempUnlimitedHeartAds.transform.localPosition,
                        "No video at the moment!",
                        Color.white,
                        isSpawnItemPlayer: true,
                        false,
                        null
                        );
        }
    }

    void HeartChecker()
    {
        if (UseProfile.RemainingTimeForUnlimitedHeart <= 0)
        {
            heartCounter.text = UseProfile.Heart + "/" + UseProfile.MaxHeart;
        }
        else
        {
            heartCounter.text = "∞";
        }

        if (UseProfile.Heart <= 0)
        {
            buyHeartIcon.SetActive(true);
            openBuyHeart.interactable = true;
        }
        else
        {
            buyHeartIcon.SetActive(false);
            openBuyHeart.interactable = false;
        }
    }

    void ShowMoney()
    {
        if (UseProfile.Coin >= 1000000000)
        {
            money.text = (UseProfile.Coin / 1000000000).ToString() + "b";
        }
        if (UseProfile.Coin >= 1000000)
        {
            money.text = (UseProfile.Coin / 1000000).ToString() + "m";
        }
        else if (UseProfile.Coin >= 1000)
        {
            money.text = (UseProfile.Coin / 1000).ToString() + "k";
        }
        else
        {
            money.text = UseProfile.Coin.ToString();
        }
    }

    void ShowStar()
    {
        if (UseProfile.Star >= 1000000000)
        {
            star.text = (UseProfile.Star / 1000000000).ToString() + "b";
        }
        if (UseProfile.Star >= 1000000)
        {
            star.text = (UseProfile.Star / 1000000).ToString() + "m";
        }
        else if (UseProfile.Star >= 1000)
        {
            star.text = (UseProfile.Star / 1000).ToString() + "k";
        }
        else
        {
            star.text = UseProfile.Star.ToString();
        }
    }

    public override void OnEscapeWhenStackBoxEmpty()
    {
        //Hiển thị popup bạn có muốn thoát game ko?
    }

    #region Listener Functions
    void UpdateMoney(object dam)
    {
        ShowMoney();
        ShowStar();
    }

    void UpdateHeart(object dam)
    {
        if (UseProfile.RemainingTimeForUnlimitedHeart <= 0)
        {
            HeartChecker();
        }
    }
    #endregion
}
