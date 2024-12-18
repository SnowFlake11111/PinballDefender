using DG.Tweening;
using DG.Tweening.Plugins.Options;
using EventDispatcher;
using MoreMountains.NiceVibrations;
using Sirenix.OdinInspector;
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
    public Text money;

    [Space]
    [Header("-----MID AND BOTTOM SCREEN-----")]
    public Button startScoreBattle;
    public Button startDefenderBattle;

    [Space]
    [Header("-----BUTTONS-----")]
    public Button openBarrack;
    public Button openSetting;
    public Button openShop;
    public Button openAchievement;

    [Space]
    [Header("Game Stages")]
    public GameObject stagesHolder;
    public List<Button> stageBtns = new List<Button>();
    #endregion

    #region Private Variables
    #endregion

    #region Start, Update
    //private void Update()
    //{
    //    UpdateTimer();
    //    UpdateAdsCooldown();
    //}
    #endregion

    #region Functions
    //---------Public----------
    public void Init()
    {
        openShop.onClick.AddListener(delegate { OpenShop(); });
        openSetting.onClick.AddListener(delegate { OpenSetting(); });
        openBarrack.onClick.AddListener(delegate { OpenBarrack(); });
        openAchievement.onClick.AddListener(delegate { OpenAchievementBox(); });
        startScoreBattle.onClick.AddListener(delegate { OpenScoreBattlePrepare(); });
        startDefenderBattle.onClick.AddListener (delegate { OpenDefenderBattlePrepare(); });

        ShowMoney();
        SetupStageButtons();
        UpdateStageProgress();

        //GameController.Instance.musicManager.MusicTransition();

        GameController.Instance.gameAchievementController.AchievementCheckerForBattles();

        this.RegisterListener(EventID.CHANGE_GEM, UpdateMoney);
        this.RegisterListener(EventID.LEVEL_PROGRESS_CHANGE, UpdateStageProgress);
    }

    //---------Private----------
    void SetupStageButtons()
    {
        for(int i = 0; i < stageBtns.Count; i++)
        {
            int temp = i;
            stageBtns[i].onClick.AddListener(delegate { OpenCampaignPrepare(temp); });
        }
    }

    void OpenShop()
    {
        //Open Shop pop-up
        GameController.Instance.musicManager.PlayClickSound();
        ShopBox.Setup().Show();
    }

    void OpenSetting()
    {
        GameController.Instance.musicManager.PlayClickSound();

        SettingBox.Setup().Show();
        //MMVibrationManager.Haptic(HapticTypes.MediumImpact);
    }

    void OpenBarrack()
    {
        GameController.Instance.musicManager.PlayClickSound();
        Barrack.Setup().Show();
    }

    void OpenCampaignPrepare(int stageId)
    {
        GameController.Instance.musicManager.PlayClickSound();
        CampaignPrepare.Setup(stageId).Show();
    }

    void OpenScoreBattlePrepare()
    {
        GameController.Instance.musicManager.PlayClickSound();
        ScoreBattlePrepare.Setup().Show();
    }

    void OpenDefenderBattlePrepare()
    {
        GameController.Instance.musicManager.PlayClickSound();
        DefenderBattlePrepare.Setup().Show();
    }

    void OpenAchievementBox()
    {
        GameController.Instance.musicManager.PlayClickSound();
        AchievementBox.Setup().Show();
    }

    void ShowMoney()
    {
        if (UseProfile.GameGem >= 1000000000)
        {
            money.text = (UseProfile.GameGem / 1000000000).ToString() + "b";
        }
        if (UseProfile.GameGem >= 1000000)
        {
            money.text = (UseProfile.GameGem / 1000000).ToString() + "m";
        }
        else if (UseProfile.GameGem >= 1000)
        {
            money.text = (UseProfile.GameGem / 1000).ToString() + "k";
        }
        else
        {
            money.text = UseProfile.GameGem.ToString();
        }
    }

    public override void OnEscapeWhenStackBoxEmpty()
    {
        //Hiển thị popup bạn có muốn thoát game ko?
    }

    //---------Start Game----------
    void StartCampaignPrepare()
    {
        //Open Campaign Prepare
    }

    void StartScoreBattlePrepare()
    {
        //Open Score Battle Prepare
    }

    void StartDefenderBattlePrepare()
    {
        //Open Defender Battle Prepare
    }

    //---------Odin Functions----------
    [Button]
    void UpdateStages()
    {
        stageBtns.Clear();
        foreach(Transform children in stagesHolder.transform)
        {
            if (children.childCount > 0)
            {
                foreach(Transform stageBtn in children.transform)
                {
                    if (stageBtn.GetComponent<Button>() != null && stageBtn.name.Contains("Stage"))
                    {
                        stageBtns.Add(stageBtn.GetComponent<Button>());
                    }
                }
            }
        }
    }
    #endregion

    #region Listener Functions
    void UpdateMoney(object dam)
    {
        ShowMoney();
    }

    void UpdateStageProgress(object dam = null)
    {
        for (int i = 0; i < stageBtns.Count; i++)
        {
            if (i <= UseProfile.LevelProgress)
            {
                stageBtns[i].interactable = true;
            }
            else
            {
                stageBtns[i].interactable = false;
            }
        }

        if (UseProfile.LevelProgress < 10)
        {
            startScoreBattle.gameObject.SetActive(false);
            startDefenderBattle.gameObject.SetActive(false);
        }
        else
        {
            startScoreBattle.gameObject.SetActive(true);
            startDefenderBattle.gameObject.SetActive(true);
        }
    }
    #endregion
}
