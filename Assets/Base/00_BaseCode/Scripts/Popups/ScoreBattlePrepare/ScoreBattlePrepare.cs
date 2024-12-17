using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreBattlePrepare : BaseBox
{
    #region Instance
    private static ScoreBattlePrepare instance;
    public static ScoreBattlePrepare Setup(bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<ScoreBattlePrepare>(PathPrefabs.SCORE_BATTLE_PREPARE));
            instance.Init();
        }

        instance.InitState();
        return instance;
    }
    #endregion

    #region Public Variables
    [Header("Other Buttons")]
    public Button cancelBtn;

    [Space]
    [Header("Stat Changes preview")]
    public Text player_1DamageChangesPreview;
    public Text player_1BounceChangesPreview;
    public Text player_1ReloadSpeedChangesPreview;
    public Text player_1MagazineChangesPreview;

    [Space]
    public Text player_2DamageChangesPreview;
    public Text player_2BounceChangesPreview;
    public Text player_2ReloadSpeedChangesPreview;
    public Text player_2MagazineChangesPreview;

    [Space]
    [Header("Upgrades count")]
    public Text player_1CurrentDamageCount;
    public Text player_1CurrentBounceCount;
    public Text player_1CurrentReloadSpeedCount;
    public Text player_1CurrentMagazineCount;

    [Space]
    public Text player_2CurrentDamageCount;
    public Text player_2CurrentBounceCount;
    public Text player_2CurrentReloadSpeedCount;
    public Text player_2CurrentMagazineCount;

    [Space]
    [Header("Stats upgrade Buttons")]
    public Button player_1IncreaseDamageBtn;
    public Button player_1IncreaseBounceBtn;
    public Button player_1IncreaseReloadSpeedBtn;
    public Button player_1IncreaseMagazineBtn;

    [Space]
    public Button player_2IncreaseDamageBtn;
    public Button player_2IncreaseBounceBtn;
    public Button player_2IncreaseReloadSpeedBtn;
    public Button player_2IncreaseMagazineBtn;

    [Space]
    [Header("Stats downgrade Buttons")]
    public Button player_1DecreaseDamageBtn;
    public Button player_1DecreaseBounceBtn;
    public Button player_1DecreaseReloadSpeedBtn;
    public Button player_1DecreaseMagazineBtn;

    [Space]
    public Button player_2DecreaseDamageBtn;
    public Button player_2DecreaseBounceBtn;
    public Button player_2DecreaseReloadSpeedBtn;
    public Button player_2DecreaseMagazineBtn;

    [Space]
    [Header("Ball skins and trails")]
    public List<Button> player_1SkinChoices = new List<Button>();
    public List<Button> player_1TrailChoices = new List<Button>();
    public List<GameObject> player_1SkinChoicesHighlight = new List<GameObject>();
    public List<GameObject> player_1TrailChoicesHighlight = new List<GameObject>();
    public List<GameObject> player_1SkinLocknotes = new List<GameObject>();
    public List<GameObject> player_1TrailLocknotes = new List<GameObject>();

    [Space]
    public List<Button> player_2SkinChoices = new List<Button>();
    public List<Button> player_2TrailChoices = new List<Button>();
    public List<GameObject> player_2SkinChoicesHighlight = new List<GameObject>();
    public List<GameObject> player_2TrailChoicesHighlight = new List<GameObject>();
    public List<GameObject> player_2SkinLocknotes = new List<GameObject>();
    public List<GameObject> player_2TrailLocknotes = new List<GameObject>();

    [Space]
    [Header("Game mode choice")]
    public Button chooseAct_1Only;
    public Button chooseAct_2Only;

    [Space]
    public GameObject act_1ChosenHightlight;
    public GameObject act_2ChosenHightlight;

    [Space]
    [Header("Player ready up buttons")]
    public Button player_1ReadyBtn;
    public Button player_2ReadyBtn;

    [Space]
    public GameObject player_1ReadyIcon;
    public GameObject player_2ReadyIcon;
    #endregion

    #region Private Variables
    int player_1DamageUpgradeCount = 0;
    int player_1BounceUpgradeCount = 0;
    int player_1ReloadSpeedUpgradeCount = 0;
    int player_1MagazineUpgradeCount = 0;
    int player_1SkinChoice = -1;
    int player_1TrailChoice = -1;

    int player_2DamageUpgradeCount = 0;
    int player_2BounceUpgradeCount = 0;
    int player_2ReloadSpeedUpgradeCount = 0;
    int player_2MagazineUpgradeCount = 0;
    int player_2SkinChoice = -1;
    int player_2TrailChoice = -1;

    int modeChoice = 0;

    bool act_1ModeChosen = false;
    bool act_2ModeChosen = false;
    bool player_1IsReady = false;
    bool player_2IsReady = false;

    bool previewIsAlreadySetup = false;
    #endregion

    #region Start, Update
    public void Init()
    {
        SetupStatChangeButtons();

        HomeController.Instance.ActivatePreviewRoom_1();
        HomeController.Instance.ActivatePreviewRoom_2();

        SetupSkinAndTrailPreview();
        SetupSkinAndTrailButtons();
        SetupGameModeButtons();
        SetupReadyUpButtons();

        UpdateButtons(1);
        UpdateCurrentUpgradeCount(1);
        UpdateUpgradesPreview(1);

        UpdateButtons(2);
        UpdateCurrentUpgradeCount(2);
        UpdateUpgradesPreview(2);

        ChoosingMode(1);

        cancelBtn.onClick.AddListener(delegate { CancelScoreModePrepare(); });
    }

    public void InitState()
    {
        SetupSkinAndTrailPreview(true);
        CosmeticUnlockChecker();
    }
    #endregion

    #region Functions
    //---------Public----------
    //---------Private----------
    void SetupStatChangeButtons()
    {
        //Player 1
        player_1IncreaseDamageBtn.onClick.AddListener(delegate { ChangeDamageUpgradeCount(1, 1); });
        player_1IncreaseBounceBtn.onClick.AddListener(delegate { ChangeBounceUpgradeCount(1, 1); });
        player_1IncreaseReloadSpeedBtn.onClick.AddListener(delegate { ChangeReloadSpeedUpgradeCount(1, 1); });
        player_1IncreaseMagazineBtn.onClick.AddListener(delegate { ChangeMagazineUpgradeCount(1, 1); });

        player_1DecreaseDamageBtn.onClick.AddListener(delegate { ChangeDamageUpgradeCount(1, 0); });
        player_1DecreaseBounceBtn.onClick.AddListener(delegate { ChangeBounceUpgradeCount(1,0); });
        player_1DecreaseReloadSpeedBtn.onClick.AddListener(delegate { ChangeReloadSpeedUpgradeCount(1,0); });
        player_1DecreaseMagazineBtn.onClick.AddListener(delegate { ChangeMagazineUpgradeCount(1, 0); });

        //Player 2
        player_2IncreaseDamageBtn.onClick.AddListener(delegate { ChangeDamageUpgradeCount(2, 1); });
        player_2IncreaseBounceBtn.onClick.AddListener(delegate { ChangeBounceUpgradeCount(2, 1); });
        player_2IncreaseReloadSpeedBtn.onClick.AddListener(delegate { ChangeReloadSpeedUpgradeCount(2, 1); });
        player_2IncreaseMagazineBtn.onClick.AddListener(delegate { ChangeMagazineUpgradeCount(2, 1); });

        player_2DecreaseDamageBtn.onClick.AddListener(delegate { ChangeDamageUpgradeCount(2, 0); });
        player_2DecreaseBounceBtn.onClick.AddListener(delegate { ChangeBounceUpgradeCount(2, 0); });
        player_2DecreaseReloadSpeedBtn.onClick.AddListener(delegate { ChangeReloadSpeedUpgradeCount(2, 0); });
        player_2DecreaseMagazineBtn.onClick.AddListener(delegate { ChangeMagazineUpgradeCount(2, 0); });
    }

    void SetupSkinAndTrailButtons()
    {
        for (int i = 0; i < player_1SkinChoices.Count; i++)
        {
            int temp = i;
            player_1SkinChoices[temp].onClick.AddListener(delegate { ChoosingSkin(1, temp); });
        }

        for (int i = 0; i < player_1TrailChoices.Count; i++)
        {
            int temp = i;
            player_1TrailChoices[temp].onClick.AddListener(delegate { ChoosingTrail(1, temp); });
        }

        for (int i = 0; i < player_2SkinChoices.Count; i++)
        {
            int temp = i;
            player_2SkinChoices[temp].onClick.AddListener(delegate { ChoosingSkin(2, temp); });
        }

        for (int i = 0; i < player_2TrailChoices.Count; i++)
        {
            int temp = i;
            player_2TrailChoices[temp].onClick.AddListener(delegate { ChoosingTrail(2, temp); });
        }
    }

    void SetupSkinAndTrailPreview(bool skipCosmeticSetup = false)
    {
        if (!previewIsAlreadySetup)
        {
            HomeController.Instance.ActivatePreviewRoom_1();
            HomeController.Instance.ActivatePreviewRoom_2();
            previewIsAlreadySetup = true;
        }
        
        if (!skipCosmeticSetup)
        {
            ChoosingSkin(1, UseProfile.MultiplayerPlayer_1BallTextureChoice);
            ChoosingTrail(1, UseProfile.MultiplayerPlayer_1BallTrailChoice);

            ChoosingSkin(2, UseProfile.MultiplayerPlayer_2BallTextureChoice);
            ChoosingTrail(2, UseProfile.MultiplayerPlayer_2BallTrailChoice);
        }
    }

    void SetupGameModeButtons()
    {
        chooseAct_1Only.onClick.AddListener(delegate { ChoosingMode(1); });
        chooseAct_2Only.onClick.AddListener(delegate { ChoosingMode(2); });
    }

    void SetupReadyUpButtons()
    {
        player_1ReadyBtn.onClick.AddListener(delegate { ReadyUp(1); });
        player_2ReadyBtn.onClick.AddListener(delegate { ReadyUp(2); });
    }

    void CosmeticUnlockChecker()
    {
        //1st
        if (GameController.Instance.gameAchievementController.UnlockSkinStatus(11))
        {
            player_1SkinLocknotes[0].SetActive(false);
            player_2SkinLocknotes[0].SetActive(false);
            player_1SkinChoices[10].interactable = true;
            player_2SkinChoices[10].interactable = true;
        }
        else
        {
            player_1SkinLocknotes[0].SetActive(true);
            player_2SkinLocknotes[0].SetActive(true);
            player_1SkinChoices[10].interactable = false;
            player_2SkinChoices[10].interactable = false;
        }

        //2nd
        if (GameController.Instance.gameAchievementController.UnlockSkinStatus(12))
        {
            player_1SkinLocknotes[1].SetActive(false);
            player_2SkinLocknotes[1].SetActive(false);
            player_1SkinChoices[11].interactable = true;
            player_2SkinChoices[11].interactable = true;
        }
        else
        {
            player_1SkinLocknotes[1].SetActive(true);
            player_2SkinLocknotes[1].SetActive(true);
            player_1SkinChoices[11].interactable = false;
            player_2SkinChoices[11].interactable = false;
        }

        //3rd
        if (GameController.Instance.gameAchievementController.UnlockSkinStatus(13))
        {
            player_1SkinLocknotes[2].SetActive(false);
            player_2SkinLocknotes[2].SetActive(false);
            player_1SkinChoices[12].interactable = true;
            player_2SkinChoices[12].interactable = true;
        }
        else
        {
            player_1SkinLocknotes[2].SetActive(true);
            player_2SkinLocknotes[2].SetActive(true);
            player_1SkinChoices[12].interactable = false;
            player_2SkinChoices[12].interactable = false;
        }

        //4th
        if (GameController.Instance.gameAchievementController.UnlockSkinStatus(14))
        {
            player_1SkinLocknotes[3].SetActive(false);
            player_2SkinLocknotes[3].SetActive(false);
            player_1SkinChoices[13].interactable = true;
            player_2SkinChoices[13].interactable = true;
        }
        else
        {
            player_1SkinLocknotes[3].SetActive(true);
            player_2SkinLocknotes[3].SetActive(true);
            player_1SkinChoices[13].interactable = false;
            player_2SkinChoices[13].interactable = false;
        }

        //5th
        if (GameController.Instance.gameAchievementController.UnlockSkinStatus(15))
        {
            player_1SkinLocknotes[4].SetActive(false);
            player_2SkinLocknotes[4].SetActive(false);
            player_1SkinChoices[14].interactable = true;
            player_2SkinChoices[14].interactable = true;
        }
        else
        {
            player_1SkinLocknotes[4].SetActive(true);
            player_2SkinLocknotes[4].SetActive(true);
            player_1SkinChoices[14].interactable = false;
            player_2SkinChoices[14].interactable = false;
        }

        //6th
        if (GameController.Instance.gameAchievementController.UnlockTrailStatus(11))
        {
            player_1TrailLocknotes[0].SetActive(false);
            player_2TrailLocknotes[0].SetActive(false);
            player_1TrailChoices[10].interactable = true;
            player_2TrailChoices[10].interactable = true;
        }
        else
        {
            player_1TrailLocknotes[0].SetActive(true);
            player_2TrailLocknotes[0].SetActive(true);
            player_1TrailChoices[10].interactable = false;
            player_2TrailChoices[10].interactable = false;
        }

        //7th
        if (GameController.Instance.gameAchievementController.UnlockTrailStatus(12))
        {
            player_1TrailLocknotes[1].SetActive(false);
            player_2TrailLocknotes[1].SetActive(false);
            player_1TrailChoices[11].interactable = true;
            player_2TrailChoices[11].interactable = true;
        }
        else
        {
            player_1TrailLocknotes[1].SetActive(true);
            player_2TrailLocknotes[1].SetActive(true);
            player_1TrailChoices[11].interactable = false;
            player_2TrailChoices[11].interactable = false;
        }

        //8th
        if (GameController.Instance.gameAchievementController.UnlockTrailStatus(13))
        {
            player_1TrailLocknotes[2].SetActive(false);
            player_2TrailLocknotes[2].SetActive(false);
            player_1TrailChoices[12].interactable = true;
            player_2TrailChoices[12].interactable = true;
        }
        else
        {
            player_1TrailLocknotes[2].SetActive(true);
            player_2TrailLocknotes[2].SetActive(true);
            player_1TrailChoices[12].interactable = false;
            player_2TrailChoices[12].interactable = false;
        }

        //9th
        if (GameController.Instance.gameAchievementController.UnlockTrailStatus(14))
        {
            player_1TrailLocknotes[3].SetActive(false);
            player_2TrailLocknotes[3].SetActive(false);
            player_1TrailChoices[13].interactable = true;
            player_2TrailChoices[13].interactable = true;
        }
        else
        {
            player_1TrailLocknotes[3].SetActive(true);
            player_2TrailLocknotes[3].SetActive(true);
            player_1TrailChoices[13].interactable = false;
            player_2TrailChoices[13].interactable = false;
        }

        //10th
        if (GameController.Instance.gameAchievementController.UnlockTrailStatus(15))
        {
            player_1TrailLocknotes[4].SetActive(false);
            player_2TrailLocknotes[4].SetActive(false);
            player_1TrailChoices[14].interactable = true;
            player_2TrailChoices[14].interactable = true;
        }
        else
        {
            player_1TrailLocknotes[4].SetActive(true);
            player_2TrailLocknotes[4].SetActive(true);
            player_1TrailChoices[14].interactable = false;
            player_2TrailChoices[14].interactable = false;
        }

        //11th
        if (GameController.Instance.gameAchievementController.UnlockTrailStatus(16))
        {
            player_1TrailLocknotes[5].SetActive(false);
            player_2TrailLocknotes[5].SetActive(false);
            player_1TrailChoices[15].interactable = true;
            player_2TrailChoices[15].interactable = true;
        }
        else
        {
            player_1TrailLocknotes[5].SetActive(true);
            player_2TrailLocknotes[5].SetActive(true);
            player_1TrailChoices[15].interactable = false;
            player_2TrailChoices[15].interactable = false;
        }
    }

    void UpdateButtons(int playerId, int id = 0)
    {
        switch (playerId)
        {
            case 1:
                switch (id)
                {
                    case 0:
                        //Update all
                        if (player_1DamageUpgradeCount < 5)
                        {
                            player_1IncreaseDamageBtn.interactable = true;
                            if (player_1DamageUpgradeCount > 0)
                            {
                                player_1DecreaseDamageBtn.interactable = true;
                            }
                            else
                            {
                                player_1DecreaseDamageBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_1IncreaseDamageBtn.interactable = false;
                            player_1DecreaseDamageBtn.interactable = true;
                        }

                        if (player_1BounceUpgradeCount < 5)
                        {
                            player_1IncreaseBounceBtn.interactable = true;
                            if (player_1BounceUpgradeCount > 0)
                            {
                                player_1DecreaseBounceBtn.interactable = true;
                            }
                            else
                            {
                                player_1DecreaseBounceBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_1IncreaseBounceBtn.interactable = false;
                            player_1DecreaseBounceBtn.interactable = true;
                        }

                        if (player_1ReloadSpeedUpgradeCount < 5)
                        {
                            player_1IncreaseReloadSpeedBtn.interactable = true;
                            if (player_1ReloadSpeedUpgradeCount > 0)
                            {
                                player_1DecreaseReloadSpeedBtn.interactable = true;
                            }
                            else
                            {
                                player_1DecreaseReloadSpeedBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_1IncreaseReloadSpeedBtn.interactable = false;
                            player_1DecreaseReloadSpeedBtn.interactable = true;
                        }

                        if (player_1MagazineUpgradeCount < 5)
                        {
                            player_1IncreaseMagazineBtn.interactable = true;
                            if (player_1MagazineUpgradeCount > 0)
                            {
                                player_1DecreaseMagazineBtn.interactable = true;
                            }
                            else
                            {
                                player_1DecreaseMagazineBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_1IncreaseMagazineBtn.interactable = false;
                            player_1DecreaseMagazineBtn.interactable = true;
                        }
                        break;
                    case 1:
                        //Update DAMAGE
                        if (player_1DamageUpgradeCount < 5)
                        {
                            player_1IncreaseDamageBtn.interactable = true;
                            if (player_1DamageUpgradeCount > 0)
                            {
                                player_1DecreaseDamageBtn.interactable = true;
                            }
                            else
                            {
                                player_1DecreaseDamageBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_1IncreaseDamageBtn.interactable = false;
                            player_1DecreaseDamageBtn.interactable = true;
                        }
                        break;
                    case 2:
                        //Update BOUNCE
                        if (player_1BounceUpgradeCount < 5)
                        {
                            player_1IncreaseBounceBtn.interactable = true;
                            if (player_1BounceUpgradeCount > 0)
                            {
                                player_1DecreaseBounceBtn.interactable = true;
                            }
                            else
                            {
                                player_1DecreaseBounceBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_1IncreaseBounceBtn.interactable = false;
                            player_1DecreaseBounceBtn.interactable = true;
                        }
                        break;
                    case 3:
                        //Update RELOAD SPEED
                        if (player_1ReloadSpeedUpgradeCount < 5)
                        {
                            player_1IncreaseReloadSpeedBtn.interactable = true;
                            if (player_1ReloadSpeedUpgradeCount > 0)
                            {
                                player_1DecreaseReloadSpeedBtn.interactable = true;
                            }
                            else
                            {
                                player_1DecreaseReloadSpeedBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_1IncreaseReloadSpeedBtn.interactable = false;
                            player_1DecreaseReloadSpeedBtn.interactable = true;
                        }
                        break;
                    case 4:
                        //Update MAGAZINE
                        if (player_1MagazineUpgradeCount < 5)
                        {
                            player_1IncreaseMagazineBtn.interactable = true;
                            if (player_1MagazineUpgradeCount > 0)
                            {
                                player_1DecreaseMagazineBtn.interactable = true;
                            }
                            else
                            {
                                player_1DecreaseMagazineBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_1IncreaseMagazineBtn.interactable = false;
                            player_1DecreaseMagazineBtn.interactable = true;
                        }
                        break;
                }
                break;
            case 2:
                switch (id)
                {
                    case 0:
                        //Update all
                        if (player_2DamageUpgradeCount < 5)
                        {
                            player_2IncreaseDamageBtn.interactable = true;
                            if (player_2DamageUpgradeCount > 0)
                            {
                                player_2DecreaseDamageBtn.interactable = true;
                            }
                            else
                            {
                                player_2DecreaseDamageBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_2IncreaseDamageBtn.interactable = false;
                            player_2DecreaseDamageBtn.interactable = true;
                        }

                        if (player_2BounceUpgradeCount < 5)
                        {
                            player_2IncreaseBounceBtn.interactable = true;
                            if (player_2BounceUpgradeCount > 0)
                            {
                                player_2DecreaseBounceBtn.interactable = true;
                            }
                            else
                            {
                                player_2DecreaseBounceBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_2IncreaseBounceBtn.interactable = false;
                            player_2DecreaseBounceBtn.interactable = true;
                        }

                        if (player_2ReloadSpeedUpgradeCount < 5)
                        {
                            player_2IncreaseReloadSpeedBtn.interactable = true;
                            if (player_2ReloadSpeedUpgradeCount > 0)
                            {
                                player_2DecreaseReloadSpeedBtn.interactable = true;
                            }
                            else
                            {
                                player_2DecreaseReloadSpeedBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_2IncreaseReloadSpeedBtn.interactable = false;
                            player_2DecreaseReloadSpeedBtn.interactable = true;
                        }

                        if (player_2MagazineUpgradeCount < 5)
                        {
                            player_2IncreaseMagazineBtn.interactable = true;
                            if (player_2MagazineUpgradeCount > 0)
                            {
                                player_2DecreaseMagazineBtn.interactable = true;
                            }
                            else
                            {
                                player_2DecreaseMagazineBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_2IncreaseMagazineBtn.interactable = false;
                            player_2DecreaseMagazineBtn.interactable = true;
                        }
                        break;
                    case 1:
                        //Update DAMAGE
                        if (player_2DamageUpgradeCount < 5)
                        {
                            player_2IncreaseDamageBtn.interactable = true;
                            if (player_2DamageUpgradeCount > 0)
                            {
                                player_2DecreaseDamageBtn.interactable = true;
                            }
                            else
                            {
                                player_2DecreaseDamageBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_2IncreaseDamageBtn.interactable = false;
                            player_2DecreaseDamageBtn.interactable = true;
                        }
                        break;
                    case 2:
                        //Update BOUNCE
                        if (player_2BounceUpgradeCount < 5)
                        {
                            player_2IncreaseBounceBtn.interactable = true;
                            if (player_2BounceUpgradeCount > 0)
                            {
                                player_2DecreaseBounceBtn.interactable = true;
                            }
                            else
                            {
                                player_2DecreaseBounceBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_2IncreaseBounceBtn.interactable = false;
                            player_2DecreaseBounceBtn.interactable = true;
                        }
                        break;
                    case 3:
                        //Update RELOAD SPEED
                        if (player_2ReloadSpeedUpgradeCount < 5)
                        {
                            player_2IncreaseReloadSpeedBtn.interactable = true;
                            if (player_2ReloadSpeedUpgradeCount > 0)
                            {
                                player_2DecreaseReloadSpeedBtn.interactable = true;
                            }
                            else
                            {
                                player_2DecreaseReloadSpeedBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_2IncreaseReloadSpeedBtn.interactable = false;
                            player_2DecreaseReloadSpeedBtn.interactable = true;
                        }
                        break;
                    case 4:
                        //Update MAGAZINE
                        if (player_2MagazineUpgradeCount < 5)
                        {
                            player_2IncreaseMagazineBtn.interactable = true;
                            if (player_2MagazineUpgradeCount > 0)
                            {
                                player_2DecreaseMagazineBtn.interactable = true;
                            }
                            else
                            {
                                player_2DecreaseMagazineBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_2IncreaseMagazineBtn.interactable = false;
                            player_2DecreaseMagazineBtn.interactable = true;
                        }
                        break;
                }
                break;
        }
        
    }

    void UpdateCurrentUpgradeCount(int playerId, int id = 0)
    {
        switch (playerId)
        {
            case 1:
                switch (id)
                {
                    case 0:
                        player_1CurrentDamageCount.text = player_1DamageUpgradeCount.ToString();
                        player_1CurrentBounceCount.text = player_1BounceUpgradeCount.ToString();
                        player_1CurrentReloadSpeedCount.text = player_1ReloadSpeedUpgradeCount.ToString();
                        player_1CurrentMagazineCount.text = player_1MagazineUpgradeCount.ToString();
                        break;
                    case 1:
                        player_1CurrentDamageCount.text = player_1DamageUpgradeCount.ToString();
                        break;
                    case 2:
                        player_1CurrentBounceCount.text = player_1BounceUpgradeCount.ToString();
                        break;
                    case 3:
                        player_1CurrentReloadSpeedCount.text = player_1ReloadSpeedUpgradeCount.ToString();
                        break;
                    case 4:
                        player_1CurrentMagazineCount.text = player_1MagazineUpgradeCount.ToString();
                        break;
                }
                break;
            case 2:
                switch (id)
                {
                    case 0:
                        player_2CurrentDamageCount.text = player_2DamageUpgradeCount.ToString();
                        player_2CurrentBounceCount.text = player_2BounceUpgradeCount.ToString();
                        player_2CurrentReloadSpeedCount.text = player_2ReloadSpeedUpgradeCount.ToString();
                        player_2CurrentMagazineCount.text = player_2MagazineUpgradeCount.ToString();
                        break;
                    case 1:
                        player_2CurrentDamageCount.text = player_2DamageUpgradeCount.ToString();
                        break;
                    case 2:
                        player_2CurrentBounceCount.text = player_2BounceUpgradeCount.ToString();
                        break;
                    case 3:
                        player_2CurrentReloadSpeedCount.text = player_2ReloadSpeedUpgradeCount.ToString();
                        break;
                    case 4:
                        player_2CurrentMagazineCount.text = player_2MagazineUpgradeCount.ToString();
                        break;
                }
                break;
        }       
    }

    void UpdateUpgradesPreview(int playerId, int id = 0)
    {
        switch (playerId)
        {
            case 1:
                switch (id)
                {
                    case 0:
                        //Update all
                        if (player_1DamageUpgradeCount < 5)
                        {
                            player_1DamageChangesPreview.text = (25 + 5 * player_1DamageUpgradeCount).ToString() + " -> " + (25 + 5 * (player_1DamageUpgradeCount + 1)).ToString();
                        }
                        else
                        {
                            player_1DamageChangesPreview.text = (25 + 5 * player_1DamageUpgradeCount).ToString();
                        }

                        if (player_1BounceUpgradeCount < 5)
                        {
                            player_1BounceChangesPreview.text = (5 + 1 * player_1BounceUpgradeCount).ToString() + " -> " + (5 + 1 * (player_1BounceUpgradeCount + 1)).ToString();
                        }
                        else
                        {
                            player_1BounceChangesPreview.text = (5 + 1 * player_1BounceUpgradeCount).ToString();
                        }

                        if (player_1ReloadSpeedUpgradeCount < 5)
                        {
                            player_1ReloadSpeedChangesPreview.text = (Mathf.Round((5 - 0.5f * player_1ReloadSpeedUpgradeCount) * 10) / 10).ToString() + "s" + " -> " + (Mathf.Round((5 - 0.5f * (player_1ReloadSpeedUpgradeCount + 1)) * 10) / 10).ToString() + "s";
                        }
                        else
                        {
                            player_1ReloadSpeedChangesPreview.text = (Mathf.Round((5 - 0.5f * player_1ReloadSpeedUpgradeCount) * 10) / 10).ToString() + "s";
                        }

                        if (player_1MagazineUpgradeCount < 5)
                        {
                            player_1MagazineChangesPreview.text = (5 + 1 * player_1MagazineUpgradeCount).ToString() + " -> " + (5 + 1 * (player_1MagazineUpgradeCount + 1)).ToString();
                        }
                        else
                        {
                            player_1MagazineChangesPreview.text = (5 + 1 * player_1MagazineUpgradeCount).ToString();
                        }
                        break;
                    case 1:
                        //Update DAMAGE
                        if (player_1DamageUpgradeCount < 5)
                        {
                            player_1DamageChangesPreview.text = (25 + 5 * player_1DamageUpgradeCount).ToString() + " -> " + (25 + 5 * (player_1DamageUpgradeCount + 1)).ToString();
                        }
                        else
                        {
                            player_1DamageChangesPreview.text = (25 + 5 * player_1DamageUpgradeCount).ToString();
                        }
                        break;
                    case 2:
                        //Update BOUNCE
                        if (player_1BounceUpgradeCount < 5)
                        {
                            player_1BounceChangesPreview.text = (5 + 1 * player_1BounceUpgradeCount).ToString() + " -> " + (5 + 1 * (player_1BounceUpgradeCount + 1)).ToString();
                        }
                        else
                        {
                            player_1BounceChangesPreview.text = (5 + 1 * player_1BounceUpgradeCount).ToString();
                        }
                        break;
                    case 3:
                        //Update RELOAD SPEED
                        if (player_1ReloadSpeedUpgradeCount < 5)
                        {
                            player_1ReloadSpeedChangesPreview.text = (Mathf.Round((5 - 0.5f * player_1ReloadSpeedUpgradeCount) * 10) / 10).ToString() + "s" + " -> " + (Mathf.Round((5 - 0.5f * (player_1ReloadSpeedUpgradeCount + 1)) * 10) / 10).ToString() + "s";
                        }
                        else
                        {
                            player_1ReloadSpeedChangesPreview.text = (Mathf.Round((5 - 0.5f * player_1ReloadSpeedUpgradeCount) * 10) / 10).ToString() + "s";
                        }
                        break;
                    case 4:
                        //Update MAGAZINE
                        if (player_1MagazineUpgradeCount < 5)
                        {
                            player_1MagazineChangesPreview.text = (5 + 1 * player_1MagazineUpgradeCount).ToString() + " -> " + (5 + 1 * (player_1MagazineUpgradeCount + 1)).ToString();
                        }
                        else
                        {
                            player_1MagazineChangesPreview.text = (5 + 1 * player_1MagazineUpgradeCount).ToString();
                        }
                        break;
                }
                break;
            case 2:
                switch (id)
                {
                    case 0:
                        //Update all
                        if (player_2DamageUpgradeCount < 5)
                        {
                            player_2DamageChangesPreview.text = (25 + 5 * player_2DamageUpgradeCount).ToString() + " -> " + (25 + 5 * (player_2DamageUpgradeCount + 1)).ToString();
                        }
                        else
                        {
                            player_2DamageChangesPreview.text = (25 + 5 * player_2DamageUpgradeCount).ToString();
                        }

                        if (player_2BounceUpgradeCount < 5)
                        {
                            player_2BounceChangesPreview.text = (5 + 1 * player_2BounceUpgradeCount).ToString() + " -> " + (5 + 1 * (player_2BounceUpgradeCount + 1)).ToString();
                        }
                        else
                        {
                            player_2BounceChangesPreview.text = (5 + 1 * player_2BounceUpgradeCount).ToString();
                        }

                        if (player_2ReloadSpeedUpgradeCount < 5)
                        {
                            player_2ReloadSpeedChangesPreview.text = (Mathf.Round((5 - 0.5f * player_2ReloadSpeedUpgradeCount) * 10) / 10).ToString() + "s" + " -> " + (Mathf.Round((5 - 0.5f * (player_2ReloadSpeedUpgradeCount + 1)) * 10) / 10).ToString() + "s";
                        }
                        else
                        {
                            player_2ReloadSpeedChangesPreview.text = (Mathf.Round((5 - 0.5f * player_2ReloadSpeedUpgradeCount) * 10) / 10).ToString() + "s";
                        }

                        if (player_2MagazineUpgradeCount < 5)
                        {
                            player_2MagazineChangesPreview.text = (5 + 1 * player_2MagazineUpgradeCount).ToString() + " -> " + (5 + 1 * (player_2MagazineUpgradeCount + 1)).ToString();
                        }
                        else
                        {
                            player_2MagazineChangesPreview.text = (5 + 1 * player_2MagazineUpgradeCount).ToString();
                        }
                        break;
                    case 1:
                        //Update DAMAGE
                        if (player_2DamageUpgradeCount < 5)
                        {
                            player_2DamageChangesPreview.text = (25 + 5 * player_2DamageUpgradeCount).ToString() + " -> " + (25 + 5 * (player_2DamageUpgradeCount + 1)).ToString();
                        }
                        else
                        {
                            player_2DamageChangesPreview.text = (25 + 5 * player_2DamageUpgradeCount).ToString();
                        }
                        break;
                    case 2:
                        //Update BOUNCE
                        if (player_2BounceUpgradeCount < 5)
                        {
                            player_2BounceChangesPreview.text = (5 + 1 * player_2BounceUpgradeCount).ToString() + " -> " + (5 + 1 * (player_2BounceUpgradeCount + 1)).ToString();
                        }
                        else
                        {
                            player_2BounceChangesPreview.text = (5 + 1 * player_2BounceUpgradeCount).ToString();
                        }
                        break;
                    case 3:
                        //Update RELOAD SPEED
                        if (player_2ReloadSpeedUpgradeCount < 5)
                        {
                            player_2ReloadSpeedChangesPreview.text = (Mathf.Round((5 - 0.5f * player_2ReloadSpeedUpgradeCount) * 10) / 10).ToString() + "s" + " -> " + ((Mathf.Round(5 - 0.5f * (player_2ReloadSpeedUpgradeCount + 1)) * 10) / 10).ToString() + "s";
                        }
                        else
                        {
                            player_2ReloadSpeedChangesPreview.text = (Mathf.Round((5 - 0.5f * player_2ReloadSpeedUpgradeCount) * 10) / 10).ToString() + "s";
                        }
                        break;
                    case 4:
                        //Update MAGAZINE
                        if (player_2MagazineUpgradeCount < 5)
                        {
                            player_2MagazineChangesPreview.text = (5 + 1 * player_2MagazineUpgradeCount).ToString() + " -> " + (5 + 1 * (player_2MagazineUpgradeCount + 1)).ToString();
                        }
                        else
                        {
                            player_2MagazineChangesPreview.text = (5 + 1 * player_2MagazineUpgradeCount).ToString();
                        }
                        break;
                }
                break;
        }
        
    }

    void ChoosingSkin(int playerId, int choiceId)
    {
        switch(playerId)
        {
            case 1:
                if (player_1SkinChoice == choiceId)
                {
                    return;  
                }

                if (player_1SkinChoice > -1)
                {
                    player_1SkinChoicesHighlight[player_1SkinChoice].SetActive(false);
                }

                player_1SkinChoicesHighlight[choiceId].SetActive(true);
                HomeController.Instance.UpdateTextureToBall_1(choiceId);
                player_1SkinChoice = choiceId;
                break;
            case 2:
                if (player_2SkinChoice == choiceId)
                {
                    return;
                }

                if (player_2SkinChoice > -1)
                {
                    player_2SkinChoicesHighlight[player_2SkinChoice].SetActive(false);
                }

                player_2SkinChoicesHighlight[choiceId].SetActive(true);
                HomeController.Instance.UpdateTextureToBall_2(choiceId);
                player_2SkinChoice = choiceId;
                break;
        }
    }

    void ChoosingTrail(int playerId, int choiceId)
    {
        switch (playerId)
        {
            case 1:
                if (player_1TrailChoice == choiceId)
                {
                    return;
                }

                if (player_1TrailChoice > -1)
                {
                    player_1TrailChoicesHighlight[player_1TrailChoice].SetActive(false);
                }

                player_1TrailChoicesHighlight[choiceId].SetActive(true);
                HomeController.Instance.UpdateTrailBall_1(choiceId);
                player_1TrailChoice = choiceId;
                break;
            case 2:
                if (player_2TrailChoice == choiceId)
                {
                    return;
                }

                if (player_2TrailChoice > -1)
                {
                    player_2TrailChoicesHighlight[player_2TrailChoice].SetActive(false);
                }

                player_2TrailChoicesHighlight[choiceId].SetActive(true);
                HomeController.Instance.UpdateTrailBall_2(choiceId);
                player_2TrailChoice = choiceId;
                break;
        }
    }

    void UpdateModeChoice(int id, bool modeStatus)
    {
        switch (id)
        {
            case 1:
                if (modeStatus)
                {
                    act_1ChosenHightlight.SetActive(true);
                }
                else
                {
                    act_1ChosenHightlight.SetActive(false);
                }
                break;
            case 2:
                if (modeStatus)
                {
                    act_2ChosenHightlight.SetActive(true);
                }
                else
                {
                    act_2ChosenHightlight.SetActive(false);
                }
                break;
        }

        if (act_1ModeChosen & act_2ModeChosen)
        {
            modeChoice = 3;
        }
        else if (act_1ModeChosen)
        {
            modeChoice = 1;
        }
        else
        {
            modeChoice = 2;
        }
    }

    void ChangeDamageUpgradeCount(int playerId, int id)
    {
        switch (playerId)
        {
            case 1:
                switch (id)
                {
                    case 0:
                        player_1DamageUpgradeCount--;
                        break;
                    case 1:
                        player_1DamageUpgradeCount++;
                        break;
                }
                break;
            case 2:
                switch (id)
                {
                    case 0:
                        player_2DamageUpgradeCount--;
                        break;
                    case 1:
                        player_2DamageUpgradeCount++;
                        break;
                }
                break;
        }
        

        UpdateButtons(playerId, 1);
        UpdateCurrentUpgradeCount(playerId, 1);
        UpdateUpgradesPreview(playerId, 1);
    }

    void ChangeBounceUpgradeCount(int playerId, int id)
    {
        switch (playerId)
        {
            case 1:
                switch (id)
                {
                    case 0:
                        player_1BounceUpgradeCount--;
                        break;
                    case 1:
                        player_1BounceUpgradeCount++;
                        break;
                }
                break;
            case 2:
                switch (id)
                {
                    case 0:
                        player_2BounceUpgradeCount--;
                        break;
                    case 1:
                        player_2BounceUpgradeCount++;
                        break;
                }
                break;
        }
        

        UpdateButtons(playerId, 2);
        UpdateCurrentUpgradeCount(playerId, 2);
        UpdateUpgradesPreview(playerId, 2);
    }

    void ChangeReloadSpeedUpgradeCount(int playerId, int id)
    {
        switch (playerId)
        {
            case 1:
                switch (id)
                {
                    case 0:
                        player_1ReloadSpeedUpgradeCount--;
                        break;
                    case 1:
                        player_1ReloadSpeedUpgradeCount++;
                        break;
                }
                break;
            case 2:
                switch (id)
                {
                    case 0:
                        player_2ReloadSpeedUpgradeCount--;
                        break;
                    case 1:
                        player_2ReloadSpeedUpgradeCount++;
                        break;
                }
                break;
        }       

        UpdateButtons(playerId, 3);
        UpdateCurrentUpgradeCount(playerId, 3);
        UpdateUpgradesPreview(playerId, 3);
    }

    void ChangeMagazineUpgradeCount(int playerId, int id)
    {
        switch (playerId)
        {
            case 1:
                switch (id)
                {
                    case 0:
                        player_1MagazineUpgradeCount--;
                        break;
                    case 1:
                        player_1MagazineUpgradeCount++;
                        break;
                }
                break;
            case 2:
                switch (id)
                {
                    case 0:
                        player_2MagazineUpgradeCount--;
                        break;
                    case 1:
                        player_2MagazineUpgradeCount++;
                        break;
                }
                break;
        }        

        UpdateButtons(playerId, 4);
        UpdateCurrentUpgradeCount(playerId, 4);
        UpdateUpgradesPreview(playerId, 4);
    }

    void ChoosingMode(int modeId)
    {
        switch(modeId)
        {
            case 1:
                if (act_1ModeChosen)
                {
                    act_1ModeChosen = false;
                    UpdateModeChoice(1, false);
                }
                else
                {
                    act_1ModeChosen = true;
                    UpdateModeChoice(1, true);
                }
                
                if (!act_1ModeChosen && !act_2ModeChosen)
                {
                    ChoosingMode(2);
                }
                break;
            case 2:
                if (act_2ModeChosen)
                {
                    act_2ModeChosen = false;
                    UpdateModeChoice(2, false);
                }
                else
                {
                    act_2ModeChosen = true;
                    UpdateModeChoice(2, true);
                }

                if (!act_1ModeChosen && !act_2ModeChosen)
                {
                    ChoosingMode(1);
                }
                break;
        }
    }

    void ReadyUp(int playerId)
    {
        switch (playerId)
        {
            case 1:
                if (player_1IsReady)
                {
                    player_1ReadyIcon.SetActive(false);
                    player_1IsReady = false;
                }
                else
                {
                    player_1ReadyIcon.SetActive(true);
                    player_1IsReady = true;
                }
                break;
            case 2:
                if (player_2IsReady)
                {
                    player_2ReadyIcon.SetActive(false);
                    player_2IsReady = false;
                }
                else
                {
                    player_2ReadyIcon.SetActive(true);
                    player_2IsReady = true;
                }
                break;
        }

        if (player_1IsReady && player_2IsReady)
        {
            ProceedToScoreBattleMode();
        }
    }

    void ProceedToScoreBattleMode()
    {
        GameController.Instance.gameModeData.RegisterPlayer_1ScoreChoices(player_1DamageUpgradeCount, player_1BounceUpgradeCount, player_1MagazineUpgradeCount, player_1ReloadSpeedUpgradeCount);
        GameController.Instance.gameModeData.RegisterPlayer_2ScoreChoices(player_2DamageUpgradeCount, player_2BounceUpgradeCount, player_2MagazineUpgradeCount, player_2ReloadSpeedUpgradeCount);

        GameController.Instance.gameModeData.StartScore(modeChoice);

        UseProfile.MultiplayerPlayer_1BallTextureChoice = player_1SkinChoice;
        UseProfile.MultiplayerPlayer_1BallTrailChoice = player_1TrailChoice;
        UseProfile.MultiplayerPlayer_2BallTextureChoice = player_2SkinChoice;
        UseProfile.MultiplayerPlayer_2BallTrailChoice = player_2TrailChoice;

        GameController.Instance.StartSceneTransition("GamePlay");
    }

    void CancelScoreModePrepare()
    {
        if (player_1IsReady)
        {
            ReadyUp(1);
        }

        if (player_2IsReady)
        {
            ReadyUp(2);
        }

        HomeController.Instance.DeactivatePreviewRoom_1();
        HomeController.Instance.DeactivatePreviewRoom_2();
        previewIsAlreadySetup = false;

        Close();
    }
    #endregion
}