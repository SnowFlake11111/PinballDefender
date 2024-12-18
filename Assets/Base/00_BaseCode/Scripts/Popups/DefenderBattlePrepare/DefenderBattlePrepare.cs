using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefenderBattlePrepare : BaseBox
{
    #region Instance
    private static DefenderBattlePrepare instance;
    public static DefenderBattlePrepare Setup(bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<DefenderBattlePrepare>(PathPrefabs.DEFENDER_BATTLE_PREPARE));
            instance.Init();
        }

        instance.InitState();
        return instance;
    }
    #endregion

    #region Public Variables
    [BoxGroup("Stat Changes preview", centerLabel: true)]
    [BoxGroup("Stat Changes preview/Player 1", centerLabel: true)]
    public Text player_1DamageChangesPreview;
    [BoxGroup("Stat Changes preview/Player 1")]
    public Text player_1BounceChangesPreview;
    [BoxGroup("Stat Changes preview/Player 1")]
    public Text player_1ReloadSpeedChangesPreview;
    [BoxGroup("Stat Changes preview/Player 1")]
    public Text player_1MagazineChangesPreview;
    [BoxGroup("Stat Changes preview/Player 1")]
    public Text player_1CreditsGainRateChangesPreview;
    [BoxGroup("Stat Changes preview/Player 1")]
    public Text player_1MaxCreditsChangesPreview;
    [BoxGroup("Stat Changes preview/Player 1")]
    public Text player_1GateHealthChangesPreview;

    [BoxGroup("Stat Changes preview/Player 2", centerLabel: true)]
    public Text player_2DamageChangesPreview;
    [BoxGroup("Stat Changes preview/Player 2")]
    public Text player_2BounceChangesPreview;
    [BoxGroup("Stat Changes preview/Player 2")]
    public Text player_2ReloadSpeedChangesPreview;
    [BoxGroup("Stat Changes preview/Player 2")]
    public Text player_2MagazineChangesPreview;
    [BoxGroup("Stat Changes preview/Player 2")]
    public Text player_2CreditsGainRateChangesPreview;
    [BoxGroup("Stat Changes preview/Player 2")]
    public Text player_2MaxCreditsChangesPreview;
    [BoxGroup("Stat Changes preview/Player 2")]
    public Text player_2GateHealthChangesPreview;

    [BoxGroup("Upgrades count", centerLabel: true)]
    [BoxGroup("Upgrades count/Player 1", centerLabel: true)]
    public Text player_1CurrentDamageCount;
    [BoxGroup("Upgrades count/Player 1")]
    public Text player_1CurrentBounceCount;
    [BoxGroup("Upgrades count/Player 1")]
    public Text player_1CurrentReloadSpeedCount;
    [BoxGroup("Upgrades count/Player 1")]
    public Text player_1CurrentMagazineCount;
    [BoxGroup("Upgrades count/Player 1")]
    public Text player_1CurrentCreditsGainRateCount;
    [BoxGroup("Upgrades count/Player 1")]
    public Text player_1CurrentMaxCreditsCount;
    [BoxGroup("Upgrades count/Player 1")]
    public Text player_1CurrentGateHealthCount;


    [BoxGroup("Upgrades count/Player 2", centerLabel: true)]
    public Text player_2CurrentDamageCount;
    [BoxGroup("Upgrades count/Player 2")]
    public Text player_2CurrentBounceCount;
    [BoxGroup("Upgrades count/Player 2")]
    public Text player_2CurrentReloadSpeedCount;
    [BoxGroup("Upgrades count/Player 2")]
    public Text player_2CurrentMagazineCount;
    [BoxGroup("Upgrades count/Player 2")]
    public Text player_2CurrentCreditsGainRateCount;
    [BoxGroup("Upgrades count/Player 2")]
    public Text player_2CurrentMaxCreditsCount;
    [BoxGroup("Upgrades count/Player 2")]
    public Text player_2CurrentGateHealthCount;

    [BoxGroup("Upgrades count/Player 1 (On player 2 side)", centerLabel: true)]
    public Text player_1CurrentDamageCount_player2Side;
    [BoxGroup("Upgrades count/Player 1 (On player 2 side)")]
    public Text player_1CurrentBounceCount_player2Side;
    [BoxGroup("Upgrades count/Player 1 (On player 2 side)")]
    public Text player_1CurrentReloadSpeedCount_player2Side;
    [BoxGroup("Upgrades count/Player 1 (On player 2 side)")]
    public Text player_1CurrentMagazineCount_player2Side;
    [BoxGroup("Upgrades count/Player 1 (On player 2 side)")]
    public Text player_1CurrentCreditsGainRateCount_player2Side;
    [BoxGroup("Upgrades count/Player 1 (On player 2 side)")]
    public Text player_1CurrentMaxCreditsCount_player2Side;
    [BoxGroup("Upgrades count/Player 1 (On player 2 side)")]
    public Text player_1CurrentGateHealthCount_player2Side;

    [BoxGroup("Upgrades count/Player 2 (On player 1 side)", centerLabel: true)]
    public Text player_2CurrentDamageCount_player1Side;
    [BoxGroup("Upgrades count/Player 2 (On player 1 side)")]
    public Text player_2CurrentBounceCount_player1Side;
    [BoxGroup("Upgrades count/Player 2 (On player 1 side)")]
    public Text player_2CurrentReloadSpeedCount_player1Side;
    [BoxGroup("Upgrades count/Player 2 (On player 1 side)")]
    public Text player_2CurrentMagazineCount_player1Side;
    [BoxGroup("Upgrades count/Player 2 (On player 1 side)")]
    public Text player_2CurrentCreditsGainRateCount_player1Side;
    [BoxGroup("Upgrades count/Player 2 (On player 1 side)")]
    public Text player_2CurrentMaxCreditsCount_player1Side;
    [BoxGroup("Upgrades count/Player 2 (On player 1 side)")]
    public Text player_2CurrentGateHealthCount_player1Side;

    [BoxGroup("Stats upgrade Buttons", centerLabel: true)]
    [BoxGroup("Stats upgrade Buttons/Player 1", centerLabel: true)]
    public Button player_1IncreaseDamageBtn;
    [BoxGroup("Stats upgrade Buttons/Player 1")]
    public Button player_1IncreaseBounceBtn;
    [BoxGroup("Stats upgrade Buttons/Player 1")]
    public Button player_1IncreaseReloadSpeedBtn;
    [BoxGroup("Stats upgrade Buttons/Player 1")]
    public Button player_1IncreaseMagazineBtn;
    [BoxGroup("Stats upgrade Buttons/Player 1")]
    public Button player_1IncreaseCreditsGainRateBtn;
    [BoxGroup("Stats upgrade Buttons/Player 1")]
    public Button player_1IncreaseMaxCreditsBtn;
    [BoxGroup("Stats upgrade Buttons/Player 1")]
    public Button player_1IncreaseGateHealthBtn;

    [BoxGroup("Stats upgrade Buttons/Player 2", centerLabel: true)]
    public Button player_2IncreaseDamageBtn;
    [BoxGroup("Stats upgrade Buttons/Player 2")]
    public Button player_2IncreaseBounceBtn;
    [BoxGroup("Stats upgrade Buttons/Player 2")]
    public Button player_2IncreaseReloadSpeedBtn;
    [BoxGroup("Stats upgrade Buttons/Player 2")]
    public Button player_2IncreaseMagazineBtn;
    [BoxGroup("Stats upgrade Buttons/Player 2")]
    public Button player_2IncreaseCreditsGainRateBtn;
    [BoxGroup("Stats upgrade Buttons/Player 2")]
    public Button player_2IncreaseMaxCreditsBtn;
    [BoxGroup("Stats upgrade Buttons/Player 2")]
    public Button player_2IncreaseGateHealthBtn;

    [BoxGroup("Stats downgrade Buttons", centerLabel: true)]
    [BoxGroup("Stats downgrade Buttons/Player 1", centerLabel: true)]
    public Button player_1DecreaseDamageBtn;
    [BoxGroup("Stats downgrade Buttons/Player 1")]
    public Button player_1DecreaseBounceBtn;
    [BoxGroup("Stats downgrade Buttons/Player 1")]
    public Button player_1DecreaseReloadSpeedBtn;
    [BoxGroup("Stats downgrade Buttons/Player 1")]
    public Button player_1DecreaseMagazineBtn;
    [BoxGroup("Stats downgrade Buttons/Player 1")]
    public Button player_1DecreaseCreditsGainRateBtn;
    [BoxGroup("Stats downgrade Buttons/Player 1")]
    public Button player_1DecreaseMaxCreditsBtn;
    [BoxGroup("Stats downgrade Buttons/Player 1")]
    public Button player_1DecreaseGateHealthBtn;

    [BoxGroup("Stats downgrade Buttons/Player 2", centerLabel: true)]
    public Button player_2DecreaseDamageBtn;
    [BoxGroup("Stats downgrade Buttons/Player 2")]
    public Button player_2DecreaseBounceBtn;
    [BoxGroup("Stats downgrade Buttons/Player 2")]
    public Button player_2DecreaseReloadSpeedBtn;
    [BoxGroup("Stats downgrade Buttons/Player 2")]
    public Button player_2DecreaseMagazineBtn;
    [BoxGroup("Stats downgrade Buttons/Player 2")]
    public Button player_2DecreaseCreditsGainRateBtn;
    [BoxGroup("Stats downgrade Buttons/Player 2")]
    public Button player_2DecreaseMaxCreditsBtn;
    [BoxGroup("Stats downgrade Buttons/Player 2")]
    public Button player_2DecreaseGateHealthBtn;

    [BoxGroup("Unit Section", centerLabel: true)]
    [BoxGroup("Unit Section/Player 1", centerLabel: true)]
    public Dictionary<int, Button> player_1PickUnitBtns = new Dictionary<int, Button>();
    [BoxGroup("Unit Section/Player 1")]
    public Dictionary<int, Image> player_1UnitPortraits = new Dictionary<int, Image>();
    [BoxGroup("Unit Section/Player 1")]
    public Dictionary<int, GameObject> player_1PickUnitBtnHighlight = new Dictionary<int, GameObject>();
    [BoxGroup("Unit Section/Player 1")]
    public Dictionary<int, GameObject> player_1UnitIsPicked = new Dictionary<int, GameObject>();
    [BoxGroup("Unit Section/Player 1")]
    public List<Button> player_1RemoveUnitBtns = new List<Button>();
    [BoxGroup("Unit Section/Player 1")]
    public List<Image> player_1ChosenUnitPortraits = new List<Image>();
    [BoxGroup("Unit Section/Player 1")]
    public List<Text> player_1ChosenUnitCosts = new List<Text>();
    [BoxGroup("Unit Section/Player 1")]
    public Dictionary<int, GameObject> player_1UnitLockNotes = new Dictionary<int, GameObject>();
    [BoxGroup("Unit Section/Player 1")]
    public Dictionary<int, GameObject> player_1UnitInfos = new Dictionary<int, GameObject>();

    [BoxGroup("Unit Section/Player 2", centerLabel: true)]
    public Dictionary<int, Button> player_2PickUnitBtns = new Dictionary<int, Button>();
    [BoxGroup("Unit Section/Player 2")]
    public Dictionary<int, Image> player_2UnitPortraits = new Dictionary<int, Image>();
    [BoxGroup("Unit Section/Player 2")]
    public Dictionary<int, GameObject> player_2PickUnitBtnHighlight = new Dictionary<int, GameObject>();
    [BoxGroup("Unit Section/Player 2")]
    public Dictionary<int, GameObject> player_2UnitIsPicked = new Dictionary<int, GameObject>();
    [BoxGroup("Unit Section/Player 2")]
    public List<Button> player_2RemoveUnitBtns = new List<Button>();
    [BoxGroup("Unit Section/Player 2")]
    public List<Image> player_2ChosenUnitPortraits = new List<Image>();
    [BoxGroup("Unit Section/Player 2")]
    public List<Text> player_2ChosenUnitCosts = new List<Text>();
    [BoxGroup("Unit Section/Player 2")]
    public Dictionary<int, GameObject> player_2UnitLockNotes = new Dictionary<int, GameObject>();
    [BoxGroup("Unit Section/Player 2")]
    public Dictionary<int, GameObject> player_2UnitInfos = new Dictionary<int, GameObject>();

    [BoxGroup("Ball skins and trails", centerLabel: true)]
    [BoxGroup("Ball skins and trails/Player 1", centerLabel: true)]
    public List<Button> player_1SkinChoices = new List<Button>();
    [BoxGroup("Ball skins and trails/Player 1")]
    public List<Button> player_1TrailChoices = new List<Button>();
    [BoxGroup("Ball skins and trails/Player 1")]
    public List<GameObject> player_1SkinChoicesHighlight = new List<GameObject>();
    [BoxGroup("Ball skins and trails/Player 1")]
    public List<GameObject> player_1TrailChoicesHighlight = new List<GameObject>();
    [BoxGroup("Ball skins and trails/Player 1")]
    public List<GameObject> player_1SkinLocknotes = new List<GameObject>();
    [BoxGroup("Ball skins and trails/Player 1")]
    public List<GameObject> player_1TrailLocknotes = new List<GameObject>();

    [BoxGroup("Ball skins and trails/Player 2", centerLabel: true)]
    public List<Button> player_2SkinChoices = new List<Button>();
    [BoxGroup("Ball skins and trails/Player 2")]
    public List<Button> player_2TrailChoices = new List<Button>();
    [BoxGroup("Ball skins and trails/Player 2")]
    public List<GameObject> player_2SkinChoicesHighlight = new List<GameObject>();
    [BoxGroup("Ball skins and trails/Player 2")]
    public List<GameObject> player_2TrailChoicesHighlight = new List<GameObject>();
    [BoxGroup("Ball skins and trails/Player 2")]
    public List<GameObject> player_2SkinLocknotes = new List<GameObject>();
    [BoxGroup("Ball skins and trails/Player 2")]
    public List<GameObject> player_2TrailLocknotes = new List<GameObject>();

    [Space]
    [Header("Player ready up buttons")]
    public Button player_1ReadyBtn;
    public Button player_2ReadyBtn;

    [Space]
    public GameObject player_1ReadyIcon;
    public GameObject player_2ReadyIcon;

    [Header("Other Buttons")]
    public Button player_1CancelBtn;
    public Button player_2CancelBtn;
    #endregion

    #region Private Variables
    List<int> player_1ChosenUnitIdList = new List<int>();
    List<int> player_2ChosenUnitIdList = new List<int>();

    int player_1PreviousInfoIndex = 0;
    int player_1NewInfoIndex = 0;
    int player_2PreviousInfoIndex = 0;
    int player_2NewInfoIndex = 0;

    int player_1DamageUpgradeCount = 0;
    int player_1BounceUpgradeCount = 0;
    int player_1ReloadSpeedUpgradeCount = 0;
    int player_1MagazineUpgradeCount = 0;
    int player_1CreditsGainRateUpgradeCount = 0;
    int player_1MaxCreditsUpgradeCount = 0;
    int player_1GateHealthUpgradeCount = 0;
    int player_1SkinChoice = -1;
    int player_1TrailChoice = -1;

    int player_2DamageUpgradeCount = 0;
    int player_2BounceUpgradeCount = 0;
    int player_2ReloadSpeedUpgradeCount = 0;
    int player_2MagazineUpgradeCount = 0;
    int player_2CreditsGainRateUpgradeCount = 0;
    int player_2MaxCreditsUpgradeCount = 0;
    int player_2GateHealthUpgradeCount = 0;
    int player_2SkinChoice = -1;
    int player_2TrailChoice = -1;

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
        SetupReadyUpButtons();

        UpdateButtons(1);
        UpdateCurrentUpgradeCount(1);
        UpdateUpgradesPreview(1);

        UpdateButtons(2);
        UpdateCurrentUpgradeCount(2);
        UpdateUpgradesPreview(2);

        SetupUnitPickingButtons();
        SetupDeselectUnitButtons();

        CheckSavedUnits();

        player_1CancelBtn.onClick.AddListener(delegate { CancelDefenderModePrepare(); });
        player_2CancelBtn.onClick.AddListener(delegate { CancelDefenderModePrepare(); });
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
        player_1IncreaseCreditsGainRateBtn.onClick.AddListener(delegate { ChangeCreditsGainRateUpgradeCount(1, 1); }); ;
        player_1IncreaseMaxCreditsBtn.onClick.AddListener(delegate { ChangeMaxCreditsUpgradeCount(1, 1); }); ;
        player_1IncreaseGateHealthBtn.onClick.AddListener(delegate { ChangeGateHealthUpgradeCount(1, 1); }); ;

        player_1DecreaseDamageBtn.onClick.AddListener(delegate { ChangeDamageUpgradeCount(1, 0); });
        player_1DecreaseBounceBtn.onClick.AddListener(delegate { ChangeBounceUpgradeCount(1, 0); });
        player_1DecreaseReloadSpeedBtn.onClick.AddListener(delegate { ChangeReloadSpeedUpgradeCount(1, 0); });
        player_1DecreaseMagazineBtn.onClick.AddListener(delegate { ChangeMagazineUpgradeCount(1, 0); });
        player_1DecreaseCreditsGainRateBtn.onClick.AddListener(delegate { ChangeCreditsGainRateUpgradeCount(1, 0); }); ;
        player_1DecreaseMaxCreditsBtn.onClick.AddListener(delegate { ChangeMaxCreditsUpgradeCount(1, 0); }); ;
        player_1DecreaseGateHealthBtn.onClick.AddListener(delegate { ChangeGateHealthUpgradeCount(1, 0); }); ;

        //Player 2
        player_2IncreaseDamageBtn.onClick.AddListener(delegate { ChangeDamageUpgradeCount(2, 1); });
        player_2IncreaseBounceBtn.onClick.AddListener(delegate { ChangeBounceUpgradeCount(2, 1); });
        player_2IncreaseReloadSpeedBtn.onClick.AddListener(delegate { ChangeReloadSpeedUpgradeCount(2, 1); });
        player_2IncreaseMagazineBtn.onClick.AddListener(delegate { ChangeMagazineUpgradeCount(2, 1); });
        player_2IncreaseCreditsGainRateBtn.onClick.AddListener(delegate { ChangeCreditsGainRateUpgradeCount(2, 1); }); ;
        player_2IncreaseMaxCreditsBtn.onClick.AddListener(delegate { ChangeMaxCreditsUpgradeCount(2, 1); }); ;
        player_2IncreaseGateHealthBtn.onClick.AddListener(delegate { ChangeGateHealthUpgradeCount(2, 1); }); ;

        player_2DecreaseDamageBtn.onClick.AddListener(delegate { ChangeDamageUpgradeCount(2, 0); });
        player_2DecreaseBounceBtn.onClick.AddListener(delegate { ChangeBounceUpgradeCount(2, 0); });
        player_2DecreaseReloadSpeedBtn.onClick.AddListener(delegate { ChangeReloadSpeedUpgradeCount(2, 0); });
        player_2DecreaseMagazineBtn.onClick.AddListener(delegate { ChangeMagazineUpgradeCount(2, 0); });
        player_2DecreaseCreditsGainRateBtn.onClick.AddListener(delegate { ChangeCreditsGainRateUpgradeCount(2, 0); }); ;
        player_2DecreaseMaxCreditsBtn.onClick.AddListener(delegate { ChangeMaxCreditsUpgradeCount(2, 0); }); ;
        player_2DecreaseGateHealthBtn.onClick.AddListener(delegate { ChangeGateHealthUpgradeCount(2, 0); }); ;
    }

    void SetupUnitPickingButtons()
    {
        for (int i = 1; i <= 10; i++)
        {
            int temp = i;
            player_1PickUnitBtns[temp].onClick.AddListener(delegate { PickedAnUnit(1, temp); });
        }

        for (int i = 1; i <= 10; i++)
        {
            int temp = i;
            player_2PickUnitBtns[temp].onClick.AddListener(delegate { PickedAnUnit(2, temp); });
        }
    }

    void SetupDeselectUnitButtons()
    {
        for (int i = 0; i < player_1RemoveUnitBtns.Count; i++)
        {
            int temp = i;
            player_1RemoveUnitBtns[temp].onClick.AddListener(delegate { DeselectAnUnit(1,temp); });
        }

        for (int i = 0; i < player_2RemoveUnitBtns.Count; i++)
        {
            int temp = i;
            player_2RemoveUnitBtns[temp].onClick.AddListener(delegate { DeselectAnUnit(2, temp); });
        }
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

    void SetupReadyUpButtons()
    {
        player_1ReadyBtn.onClick.AddListener(delegate { ReadyUp(1); });
        player_2ReadyBtn.onClick.AddListener(delegate { ReadyUp(2); });
    }

    void CheckSavedUnits()
    {
        player_1ChosenUnitIdList.Clear();
        player_1ChosenUnitIdList.Clear();

        //Player 1 multiplayer units 
        if (UseProfile.MultiplayerPlayer_1Slot1Unit > 0)
        {
            PickedAnUnit(1, UseProfile.MultiplayerPlayer_1Slot1Unit, true);
        }

        if (UseProfile.MultiplayerPlayer_1Slot2Unit > 0)
        {
            PickedAnUnit(1, UseProfile.MultiplayerPlayer_1Slot2Unit, true);
        }

        if (UseProfile.MultiplayerPlayer_1Slot3Unit > 0)
        {
            PickedAnUnit(1, UseProfile.MultiplayerPlayer_1Slot3Unit, true);
        }

        if (UseProfile.MultiplayerPlayer_1Slot4Unit > 0)
        {
            PickedAnUnit(1, UseProfile.MultiplayerPlayer_1Slot4Unit, true);
        }

        if (UseProfile.MultiplayerPlayer_1Slot5Unit > 0)
        {
            PickedAnUnit(1, UseProfile.MultiplayerPlayer_1Slot5Unit, true);
        }

        //Player 2 multiplayer units
        if (UseProfile.MultiplayerPlayer_2Slot1Unit > 0)
        {
            PickedAnUnit(1, UseProfile.MultiplayerPlayer_2Slot1Unit, true);
        }

        if (UseProfile.MultiplayerPlayer_2Slot2Unit > 0)
        {
            PickedAnUnit(1, UseProfile.MultiplayerPlayer_2Slot2Unit, true);
        }

        if (UseProfile.MultiplayerPlayer_2Slot3Unit > 0)
        {
            PickedAnUnit(1, UseProfile.MultiplayerPlayer_2Slot3Unit, true);
        }

        if (UseProfile.MultiplayerPlayer_2Slot4Unit > 0)
        {
            PickedAnUnit(1, UseProfile.MultiplayerPlayer_2Slot4Unit, true);
        }

        if (UseProfile.MultiplayerPlayer_2Slot5Unit > 0)
        {
            PickedAnUnit(1, UseProfile.MultiplayerPlayer_2Slot5Unit, true);
        }
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

                        if (player_1CreditsGainRateUpgradeCount < 5)
                        {
                            player_1IncreaseCreditsGainRateBtn.interactable = true;
                            if (player_1CreditsGainRateUpgradeCount > 0)
                            {
                                player_1DecreaseCreditsGainRateBtn.interactable = true;
                            }
                            else
                            {
                                player_1DecreaseCreditsGainRateBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_1IncreaseCreditsGainRateBtn.interactable = false;
                            player_1DecreaseCreditsGainRateBtn.interactable = true;
                        }

                        if (player_1MaxCreditsUpgradeCount < 5)
                        {
                            player_1IncreaseMaxCreditsBtn.interactable = true;
                            if (player_1MaxCreditsUpgradeCount > 0)
                            {
                                player_1DecreaseMaxCreditsBtn.interactable = true;
                            }
                            else
                            {
                                player_1DecreaseMaxCreditsBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_1IncreaseMaxCreditsBtn.interactable = false;
                            player_1DecreaseMaxCreditsBtn.interactable = true;
                        }

                        if (player_1GateHealthUpgradeCount < 5)
                        {
                            player_1IncreaseGateHealthBtn.interactable = true;
                            if (player_1GateHealthUpgradeCount > 0)
                            {
                                player_1DecreaseGateHealthBtn.interactable = true;
                            }
                            else
                            {
                                player_1DecreaseGateHealthBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_1IncreaseGateHealthBtn.interactable = false;
                            player_1DecreaseGateHealthBtn.interactable = true;
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
                    case 5:
                        //Update CREDITS GAIN RATE
                        if (player_1CreditsGainRateUpgradeCount < 5)
                        {
                            player_1IncreaseCreditsGainRateBtn.interactable = true;
                            if (player_1CreditsGainRateUpgradeCount > 0)
                            {
                                player_1DecreaseCreditsGainRateBtn.interactable = true;
                            }
                            else
                            {
                                player_1DecreaseCreditsGainRateBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_1IncreaseCreditsGainRateBtn.interactable = false;
                            player_1DecreaseCreditsGainRateBtn.interactable = true;
                        }
                        break;
                    case 6:
                        //Update MAX CREDITS
                        if (player_1MaxCreditsUpgradeCount < 5)
                        {
                            player_1IncreaseMaxCreditsBtn.interactable = true;
                            if (player_1MaxCreditsUpgradeCount > 0)
                            {
                                player_1DecreaseMaxCreditsBtn.interactable = true;
                            }
                            else
                            {
                                player_1DecreaseMaxCreditsBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_1IncreaseMaxCreditsBtn.interactable = false;
                            player_1DecreaseMaxCreditsBtn.interactable = true;
                        }
                        break;
                    case 7:
                        //Update GATE HEALTH
                        if (player_1GateHealthUpgradeCount < 5)
                        {
                            player_1IncreaseGateHealthBtn.interactable = true;
                            if (player_1GateHealthUpgradeCount > 0)
                            {
                                player_1DecreaseGateHealthBtn.interactable = true;
                            }
                            else
                            {
                                player_1DecreaseGateHealthBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_1IncreaseGateHealthBtn.interactable = false;
                            player_1DecreaseGateHealthBtn.interactable = true;
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

                        if (player_2CreditsGainRateUpgradeCount < 5)
                        {
                            player_2IncreaseCreditsGainRateBtn.interactable = true;
                            if (player_2CreditsGainRateUpgradeCount > 0)
                            {
                                player_2DecreaseCreditsGainRateBtn.interactable = true;
                            }
                            else
                            {
                                player_2DecreaseCreditsGainRateBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_2IncreaseCreditsGainRateBtn.interactable = false;
                            player_2DecreaseCreditsGainRateBtn.interactable = true;
                        }

                        if (player_2MaxCreditsUpgradeCount < 5)
                        {
                            player_2IncreaseMaxCreditsBtn.interactable = true;
                            if (player_2MaxCreditsUpgradeCount > 0)
                            {
                                player_2DecreaseMaxCreditsBtn.interactable = true;
                            }
                            else
                            {
                                player_2DecreaseMaxCreditsBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_2IncreaseMaxCreditsBtn.interactable = false;
                            player_2DecreaseMaxCreditsBtn.interactable = true;
                        }

                        if (player_2GateHealthUpgradeCount < 5)
                        {
                            player_2IncreaseGateHealthBtn.interactable = true;
                            if (player_2GateHealthUpgradeCount > 0)
                            {
                                player_2DecreaseGateHealthBtn.interactable = true;
                            }
                            else
                            {
                                player_2DecreaseGateHealthBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_2IncreaseGateHealthBtn.interactable = false;
                            player_2DecreaseGateHealthBtn.interactable = true;
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
                    case 5:
                        //Update CREDITS GAIN RATE
                        if (player_2CreditsGainRateUpgradeCount < 5)
                        {
                            player_2IncreaseCreditsGainRateBtn.interactable = true;
                            if (player_2CreditsGainRateUpgradeCount > 0)
                            {
                                player_2DecreaseCreditsGainRateBtn.interactable = true;
                            }
                            else
                            {
                                player_2DecreaseCreditsGainRateBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_2IncreaseCreditsGainRateBtn.interactable = false;
                            player_2DecreaseCreditsGainRateBtn.interactable = true;
                        }
                        break;
                    case 6:
                        //Update MAX CREDITS
                        if (player_2MaxCreditsUpgradeCount < 5)
                        {
                            player_2IncreaseMaxCreditsBtn.interactable = true;
                            if (player_2MaxCreditsUpgradeCount > 0)
                            {
                                player_2DecreaseMaxCreditsBtn.interactable = true;
                            }
                            else
                            {
                                player_2DecreaseMaxCreditsBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_2IncreaseMaxCreditsBtn.interactable = false;
                            player_2DecreaseMaxCreditsBtn.interactable = true;
                        }
                        break;
                    case 7:
                        //Update GATE HEALTH
                        if (player_2GateHealthUpgradeCount < 5)
                        {
                            player_2IncreaseGateHealthBtn.interactable = true;
                            if (player_2GateHealthUpgradeCount > 0)
                            {
                                player_2DecreaseGateHealthBtn.interactable = true;
                            }
                            else
                            {
                                player_2DecreaseGateHealthBtn.interactable = false;
                            }
                        }
                        else
                        {
                            player_2IncreaseGateHealthBtn.interactable = false;
                            player_2DecreaseGateHealthBtn.interactable = true;
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
                        player_1CurrentCreditsGainRateCount.text = player_1CreditsGainRateUpgradeCount.ToString();
                        player_1CurrentMaxCreditsCount.text = player_1MaxCreditsUpgradeCount.ToString();
                        player_1CurrentGateHealthCount.text = player_1GateHealthUpgradeCount.ToString();

                        player_1CurrentDamageCount_player2Side.text = player_1DamageUpgradeCount.ToString();
                        player_1CurrentBounceCount_player2Side.text = player_1BounceUpgradeCount.ToString();
                        player_1CurrentReloadSpeedCount_player2Side.text = player_1ReloadSpeedUpgradeCount.ToString();
                        player_1CurrentMagazineCount_player2Side.text = player_1MagazineUpgradeCount.ToString();
                        player_1CurrentCreditsGainRateCount_player2Side.text = player_1CreditsGainRateUpgradeCount.ToString();
                        player_1CurrentMaxCreditsCount_player2Side.text = player_1MaxCreditsUpgradeCount.ToString();
                        player_1CurrentGateHealthCount_player2Side.text = player_1GateHealthUpgradeCount.ToString();
                        break;
                    case 1:
                        player_1CurrentDamageCount.text = player_1DamageUpgradeCount.ToString();
                        player_1CurrentDamageCount_player2Side.text = player_1DamageUpgradeCount.ToString();
                        break;
                    case 2:
                        player_1CurrentBounceCount.text = player_1BounceUpgradeCount.ToString();
                        player_1CurrentBounceCount_player2Side.text = player_1BounceUpgradeCount.ToString();
                        break;
                    case 3:
                        player_1CurrentReloadSpeedCount.text = player_1ReloadSpeedUpgradeCount.ToString();
                        player_1CurrentReloadSpeedCount_player2Side.text = player_1ReloadSpeedUpgradeCount.ToString();
                        break;
                    case 4:
                        player_1CurrentMagazineCount.text = player_1MagazineUpgradeCount.ToString();
                        player_1CurrentMagazineCount_player2Side.text = player_1MagazineUpgradeCount.ToString();
                        break;
                    case 5:
                        player_1CurrentCreditsGainRateCount.text = player_1CreditsGainRateUpgradeCount.ToString();
                        player_1CurrentCreditsGainRateCount_player2Side.text = player_1CreditsGainRateUpgradeCount.ToString();
                        break;
                    case 6:
                        player_1CurrentMaxCreditsCount.text = player_1MaxCreditsUpgradeCount.ToString();
                        player_1CurrentMaxCreditsCount_player2Side.text = player_1MaxCreditsUpgradeCount.ToString();
                        break;
                    case 7:
                        player_1CurrentGateHealthCount.text = player_1GateHealthUpgradeCount.ToString();
                        player_1CurrentGateHealthCount_player2Side.text = player_1GateHealthUpgradeCount.ToString();
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
                        player_2CurrentCreditsGainRateCount.text = player_2CreditsGainRateUpgradeCount.ToString();
                        player_2CurrentMaxCreditsCount.text = player_2MaxCreditsUpgradeCount.ToString();
                        player_2CurrentGateHealthCount.text = player_1GateHealthUpgradeCount.ToString();

                        player_2CurrentDamageCount_player1Side.text = player_2DamageUpgradeCount.ToString();
                        player_2CurrentBounceCount_player1Side.text = player_2BounceUpgradeCount.ToString();
                        player_2CurrentReloadSpeedCount_player1Side.text = player_2ReloadSpeedUpgradeCount.ToString();
                        player_2CurrentMagazineCount_player1Side.text = player_2MagazineUpgradeCount.ToString();
                        player_2CurrentCreditsGainRateCount_player1Side.text = player_2CreditsGainRateUpgradeCount.ToString();
                        player_2CurrentMaxCreditsCount_player1Side.text = player_2MaxCreditsUpgradeCount.ToString();
                        player_2CurrentGateHealthCount_player1Side.text = player_1GateHealthUpgradeCount.ToString();
                        break;
                    case 1:
                        player_2CurrentDamageCount.text = player_2DamageUpgradeCount.ToString();
                        player_2CurrentDamageCount_player1Side.text = player_2DamageUpgradeCount.ToString();
                        break;
                    case 2:
                        player_2CurrentBounceCount.text = player_2BounceUpgradeCount.ToString();
                        player_2CurrentBounceCount_player1Side.text = player_2BounceUpgradeCount.ToString();
                        break;
                    case 3:
                        player_2CurrentReloadSpeedCount.text = player_2ReloadSpeedUpgradeCount.ToString();
                        player_2CurrentReloadSpeedCount_player1Side.text = player_2ReloadSpeedUpgradeCount.ToString();
                        break;
                    case 4:
                        player_2CurrentMagazineCount.text = player_2MagazineUpgradeCount.ToString();
                        player_2CurrentMagazineCount_player1Side.text = player_2MagazineUpgradeCount.ToString();
                        break;
                    case 5:
                        player_2CurrentCreditsGainRateCount.text = player_2CreditsGainRateUpgradeCount.ToString();
                        player_2CurrentCreditsGainRateCount_player1Side.text = player_2CreditsGainRateUpgradeCount.ToString();
                        break;
                    case 6:
                        player_2CurrentMaxCreditsCount.text = player_2MaxCreditsUpgradeCount.ToString();
                        player_2CurrentMaxCreditsCount_player1Side.text = player_2MaxCreditsUpgradeCount.ToString();
                        break;
                    case 7:
                        player_2CurrentGateHealthCount.text = player_1GateHealthUpgradeCount.ToString();
                        player_2CurrentGateHealthCount_player1Side.text = player_2GateHealthUpgradeCount.ToString();
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

                        if (player_1CreditsGainRateUpgradeCount < 5)
                        {
                            player_1CreditsGainRateChangesPreview.text = (5 + 2 * player_1CreditsGainRateUpgradeCount).ToString() + " -> " + (5 + 2 * (player_1CreditsGainRateUpgradeCount + 1)).ToString();
                        }
                        else
                        {
                            player_1CreditsGainRateChangesPreview.text = (5 + 2 * player_1CreditsGainRateUpgradeCount).ToString();
                        }

                        if (player_1MaxCreditsUpgradeCount < 5)
                        {
                            player_1MaxCreditsChangesPreview.text = (100 + 80 * player_1MaxCreditsUpgradeCount).ToString() + " -> " + (100 + 80 * (player_1MaxCreditsUpgradeCount + 1)).ToString();
                        }
                        else
                        {
                            player_1MaxCreditsChangesPreview.text = (100 + 80 * player_1MaxCreditsUpgradeCount).ToString();
                        }

                        if (player_1GateHealthUpgradeCount < 5)
                        {
                            player_1GateHealthChangesPreview.text = (1250 + 250 * player_1GateHealthUpgradeCount).ToString() + " -> " + (1250 + 250 * (player_1GateHealthUpgradeCount + 1)).ToString();
                        }
                        else
                        {
                            player_1GateHealthChangesPreview.text = (1250 + 250 * player_1GateHealthUpgradeCount).ToString();
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
                    case 5:
                        //Update CREDITS GAIN RATE
                        if (player_1CreditsGainRateUpgradeCount < 5)
                        {
                            player_1CreditsGainRateChangesPreview.text = (5 + 2 * player_1CreditsGainRateUpgradeCount).ToString() + " -> " + (5 + 2 * (player_1CreditsGainRateUpgradeCount + 1)).ToString();
                        }
                        else
                        {
                            player_1CreditsGainRateChangesPreview.text = (5 + 2 * player_1CreditsGainRateUpgradeCount).ToString();
                        }
                        break;
                    case 6:
                        //Update MAX CREDITS
                        if (player_1MaxCreditsUpgradeCount < 5)
                        {
                            player_1MaxCreditsChangesPreview.text = (100 + 80 * player_1MaxCreditsUpgradeCount).ToString() + " -> " + (100 + 80 * (player_1MaxCreditsUpgradeCount + 1)).ToString();
                        }
                        else
                        {
                            player_1MaxCreditsChangesPreview.text = (100 + 80 * player_1MaxCreditsUpgradeCount).ToString();
                        }
                        break;
                    case 7:
                        //Update GATE HEALTH
                        if (player_1GateHealthUpgradeCount < 5)
                        {
                            player_1GateHealthChangesPreview.text = (1250 + 250 * player_1GateHealthUpgradeCount).ToString() + " -> " + (1250 + 250 * (player_1GateHealthUpgradeCount + 1)).ToString();
                        }
                        else
                        {
                            player_1GateHealthChangesPreview.text = (1250 + 250 * player_1GateHealthUpgradeCount).ToString();
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

                        if (player_2CreditsGainRateUpgradeCount < 5)
                        {
                            player_2CreditsGainRateChangesPreview.text = (5 + 2 * player_2CreditsGainRateUpgradeCount).ToString() + " -> " + (5 + 2 * (player_2CreditsGainRateUpgradeCount + 1)).ToString();
                        }
                        else
                        {
                            player_2CreditsGainRateChangesPreview.text = (5 + 2 * player_2CreditsGainRateUpgradeCount).ToString();
                        }

                        if (player_2MaxCreditsUpgradeCount < 5)
                        {
                            player_2MaxCreditsChangesPreview.text = (100 + 80 * player_2MaxCreditsUpgradeCount).ToString() + " -> " + (100 + 80 * (player_2MaxCreditsUpgradeCount + 1)).ToString();
                        }
                        else
                        {
                            player_2MaxCreditsChangesPreview.text = (100 + 80 * player_2MaxCreditsUpgradeCount).ToString();
                        }

                        if (player_2GateHealthUpgradeCount < 5)
                        {
                            player_2GateHealthChangesPreview.text = (1250 + 250 * player_2GateHealthUpgradeCount).ToString() + " -> " + (1250 + 250 * (player_2GateHealthUpgradeCount + 1)).ToString();
                        }
                        else
                        {
                            player_2GateHealthChangesPreview.text = (1250 + 250 * player_2GateHealthUpgradeCount).ToString();
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
                    case 5:
                        //Update CREDITS GAIN RATE
                        if (player_2CreditsGainRateUpgradeCount < 5)
                        {
                            player_2CreditsGainRateChangesPreview.text = (5 + 2 * player_2CreditsGainRateUpgradeCount).ToString() + " -> " + (5 + 2 * (player_2CreditsGainRateUpgradeCount + 1)).ToString();
                        }
                        else
                        {
                            player_2CreditsGainRateChangesPreview.text = (5 + 2 * player_2CreditsGainRateUpgradeCount).ToString();
                        }
                        break;
                    case 6:
                        //Update MAX CREDITS
                        if (player_2MaxCreditsUpgradeCount < 5)
                        {
                            player_2MaxCreditsChangesPreview.text = (100 + 80 * player_2MaxCreditsUpgradeCount).ToString() + " -> " + (100 + 80 * (player_2MaxCreditsUpgradeCount + 1)).ToString();
                        }
                        else
                        {
                            player_2MaxCreditsChangesPreview.text = (100 + 80 * player_2MaxCreditsUpgradeCount).ToString();
                        }
                        break;
                    case 7:
                        //Update GATE HEALTH
                        if (player_2GateHealthUpgradeCount < 5)
                        {
                            player_2GateHealthChangesPreview.text = (1250 + 250 * player_2GateHealthUpgradeCount).ToString() + " -> " + (1250 + 250 * (player_2GateHealthUpgradeCount + 1)).ToString();
                        }
                        else
                        {
                            player_2GateHealthChangesPreview.text = (1250 + 250 * player_2GateHealthUpgradeCount).ToString();
                        }
                        break;
                }
                break;
        }

    }

    void UpdateUnitInfo(int playerId, bool skipInfoUpdate = false)
    {
        switch (playerId)
        {
            case 1:
                if (skipInfoUpdate)
                {
                    return;
                }

                if (player_1PreviousInfoIndex != player_1NewInfoIndex)
                {
                    if (player_1PreviousInfoIndex > 0)
                    {
                        player_1UnitInfos[player_1PreviousInfoIndex].SetActive(false);
                        player_1PickUnitBtnHighlight[player_1PreviousInfoIndex].SetActive(false);
                    }

                    player_1UnitInfos[player_1NewInfoIndex].SetActive(true);
                    player_1PreviousInfoIndex = player_1NewInfoIndex;
                }
                break;
            case 2:
                if (skipInfoUpdate)
                {
                    return;
                }

                if (player_2PreviousInfoIndex != player_2NewInfoIndex)
                {
                    if (player_2PreviousInfoIndex > 0)
                    {
                        player_2UnitInfos[player_2PreviousInfoIndex].SetActive(false);
                        player_2PickUnitBtnHighlight[player_2PreviousInfoIndex].SetActive(false);
                    }

                    player_2UnitInfos[player_2NewInfoIndex].SetActive(true);
                    player_2PreviousInfoIndex = player_2NewInfoIndex;
                }
                break;
        }
        
    }

    void ReorganizeChosenUnitList(int playerId, int whereToBeginReorganizing)
    {
        switch (playerId)
        {
            case 1:
                player_1UnitIsPicked[player_1ChosenUnitIdList[whereToBeginReorganizing]].SetActive(false);

                for (int i = whereToBeginReorganizing; i < player_1ChosenUnitIdList.Count - 1; i++)
                {
                    player_1ChosenUnitPortraits[i].sprite = player_1ChosenUnitPortraits[i + 1].sprite;
                    player_1ChosenUnitCosts[i].text = player_1ChosenUnitCosts[i + 1].text;
                }

                player_1ChosenUnitPortraits[player_1ChosenUnitIdList.Count - 1].color = new Color(1, 1, 1, 0);
                player_1ChosenUnitPortraits[player_1ChosenUnitIdList.Count - 1].sprite = null;
                player_1ChosenUnitCosts[player_1ChosenUnitIdList.Count - 1].text = "";

                player_1ChosenUnitIdList.RemoveAt(whereToBeginReorganizing);
                break;
            case 2:
                player_2UnitIsPicked[player_2ChosenUnitIdList[whereToBeginReorganizing]].SetActive(false);

                for (int i = whereToBeginReorganizing; i < player_2ChosenUnitIdList.Count - 1; i++)
                {
                    player_2ChosenUnitPortraits[i].sprite = player_2ChosenUnitPortraits[i + 1].sprite;
                    player_2ChosenUnitCosts[i].text = player_2ChosenUnitCosts[i + 1].text;
                }

                player_2ChosenUnitPortraits[player_2ChosenUnitIdList.Count - 1].color = new Color(1, 1, 1, 0);
                player_2ChosenUnitPortraits[player_2ChosenUnitIdList.Count - 1].sprite = null;
                player_2ChosenUnitCosts[player_2ChosenUnitIdList.Count - 1].text = "";

                player_2ChosenUnitIdList.RemoveAt(whereToBeginReorganizing);
                break;
        }
       
    }

    void PickedAnUnit(int playerId, int unitId, bool skipInfoUpdate = false)
    {
        GameController.Instance.musicManager.PlayClickSound();
        switch (playerId)
        {
            case 1:
                if (player_1ChosenUnitIdList.Count >= 5)
                {
                    return;
                }

                if (!player_1ChosenUnitIdList.Contains(unitId))
                {
                    player_1ChosenUnitIdList.Add(unitId);
                    player_1UnitIsPicked[unitId].SetActive(true);
                    player_1ChosenUnitPortraits[player_1ChosenUnitIdList.Count - 1].color = new Color(1, 1, 1, 1);
                    player_1ChosenUnitPortraits[player_1ChosenUnitIdList.Count - 1].sprite = player_1UnitPortraits[unitId].sprite;
                    player_1ChosenUnitCosts[player_1ChosenUnitIdList.Count - 1].text = GameController.Instance.gameModeData.GetUnitCost(unitId).ToString();
                }

                if (skipInfoUpdate)
                {
                    UpdateUnitInfo(1, true);
                }
                else
                {
                    player_1PickUnitBtnHighlight[unitId].SetActive(true);
                    player_1NewInfoIndex = unitId;
                    UpdateUnitInfo(1);
                }
                break;
            case 2:
                if (player_2ChosenUnitIdList.Count >= 5)
                {
                    return;
                }

                if (!player_2ChosenUnitIdList.Contains(unitId))
                {
                    player_2ChosenUnitIdList.Add(unitId);
                    player_2UnitIsPicked[unitId].SetActive(true);
                    player_2ChosenUnitPortraits[player_2ChosenUnitIdList.Count - 1].color = new Color(1, 1, 1, 1);
                    player_2ChosenUnitPortraits[player_2ChosenUnitIdList.Count - 1].sprite = player_2UnitPortraits[unitId].sprite;
                    player_2ChosenUnitCosts[player_2ChosenUnitIdList.Count - 1].text = GameController.Instance.gameModeData.GetUnitCost(unitId).ToString();
                }

                if (skipInfoUpdate)
                {
                    UpdateUnitInfo(2, true);
                }
                else
                {
                    player_2PickUnitBtnHighlight[unitId].SetActive(true);
                    player_2NewInfoIndex = unitId;
                    UpdateUnitInfo(2);
                }
                break;
        }
    }

    void DeselectAnUnit(int playerId, int chosenUnitPosition)
    {
        GameController.Instance.musicManager.PlayClickSound();
        switch (playerId)
        {
            case 1:
                if (chosenUnitPosition >= player_1ChosenUnitIdList.Count)
                {
                    Debug.LogError("This slot doesn't have any unit");
                    return;
                }

                ReorganizeChosenUnitList(1, chosenUnitPosition);
                break;
            case 2:
                if (chosenUnitPosition >= player_2ChosenUnitIdList.Count)
                {
                    Debug.LogError("This slot doesn't have any unit");
                    return;
                }

                ReorganizeChosenUnitList(2, chosenUnitPosition);
                break;
        }
    }

    void ChoosingSkin(int playerId, int choiceId)
    {
        GameController.Instance.musicManager.PlayClickSound();
        switch (playerId)
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
        GameController.Instance.musicManager.PlayClickSound();
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

    void ChangeDamageUpgradeCount(int playerId, int id)
    {
        GameController.Instance.musicManager.PlayClickSound();
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
        GameController.Instance.musicManager.PlayClickSound();
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
        GameController.Instance.musicManager.PlayClickSound();
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
        GameController.Instance.musicManager.PlayClickSound();
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

    void ChangeCreditsGainRateUpgradeCount(int playerId, int id)
    {
        GameController.Instance.musicManager.PlayClickSound();
        switch (playerId)
        {
            case 1:
                switch (id)
                {
                    case 0:
                        player_1CreditsGainRateUpgradeCount--;
                        break;
                    case 1:
                        player_1CreditsGainRateUpgradeCount++;
                        break;
                }
                break;
            case 2:
                switch (id)
                {
                    case 0:
                        player_2CreditsGainRateUpgradeCount--;
                        break;
                    case 1:
                        player_2CreditsGainRateUpgradeCount++;
                        break;
                }
                break;
        }


        UpdateButtons(playerId, 5);
        UpdateCurrentUpgradeCount(playerId, 5);
        UpdateUpgradesPreview(playerId, 5);
    }

    void ChangeMaxCreditsUpgradeCount(int playerId, int id)
    {
        GameController.Instance.musicManager.PlayClickSound();
        switch (playerId)
        {
            case 1:
                switch (id)
                {
                    case 0:
                        player_1MaxCreditsUpgradeCount--;
                        break;
                    case 1:
                        player_1MaxCreditsUpgradeCount++;
                        break;
                }
                break;
            case 2:
                switch (id)
                {
                    case 0:
                        player_2MaxCreditsUpgradeCount--;
                        break;
                    case 1:
                        player_2MaxCreditsUpgradeCount++;
                        break;
                }
                break;
        }


        UpdateButtons(playerId, 6);
        UpdateCurrentUpgradeCount(playerId, 6);
        UpdateUpgradesPreview(playerId, 6);
    }

    void ChangeGateHealthUpgradeCount(int playerId, int id)
    {
        GameController.Instance.musicManager.PlayClickSound();
        switch (playerId)
        {
            case 1:
                switch (id)
                {
                    case 0:
                        player_1GateHealthUpgradeCount--;
                        break;
                    case 1:
                        player_1GateHealthUpgradeCount++;
                        break;
                }
                break;
            case 2:
                switch (id)
                {
                    case 0:
                        player_2GateHealthUpgradeCount--;
                        break;
                    case 1:
                        player_2GateHealthUpgradeCount++;
                        break;
                }
                break;
        }


        UpdateButtons(playerId, 7);
        UpdateCurrentUpgradeCount(playerId, 7);
        UpdateUpgradesPreview(playerId, 7);
    }

    void ReadyUp(int playerId)
    {
        GameController.Instance.musicManager.PlayClickSound();
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
            ProceedToDefenderBattleMode();
        }
    }

    void ProceedToDefenderBattleMode()
    {
        GameController.Instance.gameModeData.RegisterPlayer_1DefenderChoices(player_1DamageUpgradeCount, player_1BounceUpgradeCount, player_1MagazineUpgradeCount, player_1ReloadSpeedUpgradeCount, player_1CreditsGainRateUpgradeCount, player_1MaxCreditsUpgradeCount, player_1GateHealthUpgradeCount);
        GameController.Instance.gameModeData.RegisterPlayer_2DefenderChoices(player_2DamageUpgradeCount, player_2BounceUpgradeCount, player_2MagazineUpgradeCount, player_2ReloadSpeedUpgradeCount, player_2CreditsGainRateUpgradeCount, player_2MaxCreditsUpgradeCount, player_2GateHealthUpgradeCount);

        GameController.Instance.gameModeData.StartDefender();

        SaveUnit();
        SaveCosmetic();

        GameController.Instance.StartSceneTransition("GamePlay");
    }

    void CancelDefenderModePrepare()
    {
        GameController.Instance.musicManager.PlayClickSound();
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

    //----------Save Data----------
    void SaveUnit()
    {
        //Player 1 multiplayer units 
        switch (player_1ChosenUnitIdList.Count)
        {
            case 0:
                UseProfile.MultiplayerPlayer_1Slot1Unit = 0;
                UseProfile.MultiplayerPlayer_1Slot2Unit = 0;
                UseProfile.MultiplayerPlayer_1Slot3Unit = 0;
                UseProfile.MultiplayerPlayer_1Slot4Unit = 0;
                UseProfile.MultiplayerPlayer_1Slot5Unit = 0;
                break;
            case 1:
                UseProfile.MultiplayerPlayer_1Slot1Unit = player_1ChosenUnitIdList[0];
                UseProfile.MultiplayerPlayer_1Slot2Unit = 0;
                UseProfile.MultiplayerPlayer_1Slot3Unit = 0;
                UseProfile.MultiplayerPlayer_1Slot4Unit = 0;
                UseProfile.MultiplayerPlayer_1Slot5Unit = 0;
                break;
            case 2:
                UseProfile.MultiplayerPlayer_1Slot1Unit = player_1ChosenUnitIdList[0];
                UseProfile.MultiplayerPlayer_1Slot2Unit = player_1ChosenUnitIdList[1];
                UseProfile.MultiplayerPlayer_1Slot3Unit = 0;
                UseProfile.MultiplayerPlayer_1Slot4Unit = 0;
                UseProfile.MultiplayerPlayer_1Slot5Unit = 0;
                break;
            case 3:
                UseProfile.MultiplayerPlayer_1Slot1Unit = player_1ChosenUnitIdList[0];
                UseProfile.MultiplayerPlayer_1Slot2Unit = player_1ChosenUnitIdList[1];
                UseProfile.MultiplayerPlayer_1Slot3Unit = player_1ChosenUnitIdList[2];
                UseProfile.MultiplayerPlayer_1Slot4Unit = 0;
                UseProfile.MultiplayerPlayer_1Slot5Unit = 0;
                break;
            case 4:
                UseProfile.MultiplayerPlayer_1Slot1Unit = player_1ChosenUnitIdList[0];
                UseProfile.MultiplayerPlayer_1Slot2Unit = player_1ChosenUnitIdList[1];
                UseProfile.MultiplayerPlayer_1Slot3Unit = player_1ChosenUnitIdList[2];
                UseProfile.MultiplayerPlayer_1Slot4Unit = player_1ChosenUnitIdList[3];
                UseProfile.MultiplayerPlayer_1Slot5Unit = 0;
                break;
            case 5:
                UseProfile.MultiplayerPlayer_1Slot1Unit = player_1ChosenUnitIdList[0];
                UseProfile.MultiplayerPlayer_1Slot2Unit = player_1ChosenUnitIdList[1];
                UseProfile.MultiplayerPlayer_1Slot3Unit = player_1ChosenUnitIdList[2];
                UseProfile.MultiplayerPlayer_1Slot4Unit = player_1ChosenUnitIdList[3];
                UseProfile.MultiplayerPlayer_1Slot5Unit = player_1ChosenUnitIdList[4];
                break;
        }

        //Player 2 multiplayer units
        switch (player_2ChosenUnitIdList.Count)
        {
            case 0:
                UseProfile.MultiplayerPlayer_2Slot1Unit = 0;
                UseProfile.MultiplayerPlayer_2Slot2Unit = 0;
                UseProfile.MultiplayerPlayer_2Slot3Unit = 0;
                UseProfile.MultiplayerPlayer_2Slot4Unit = 0;
                UseProfile.MultiplayerPlayer_2Slot5Unit = 0;
                break;
            case 1:
                UseProfile.MultiplayerPlayer_2Slot1Unit = player_2ChosenUnitIdList[0];
                UseProfile.MultiplayerPlayer_2Slot2Unit = 0;
                UseProfile.MultiplayerPlayer_2Slot3Unit = 0;
                UseProfile.MultiplayerPlayer_2Slot4Unit = 0;
                UseProfile.MultiplayerPlayer_2Slot5Unit = 0;
                break;
            case 2:
                UseProfile.MultiplayerPlayer_2Slot1Unit = player_2ChosenUnitIdList[0];
                UseProfile.MultiplayerPlayer_2Slot2Unit = player_2ChosenUnitIdList[1];
                UseProfile.MultiplayerPlayer_2Slot3Unit = 0;
                UseProfile.MultiplayerPlayer_2Slot4Unit = 0;
                UseProfile.MultiplayerPlayer_2Slot5Unit = 0;
                break;
            case 3:
                UseProfile.MultiplayerPlayer_2Slot1Unit = player_2ChosenUnitIdList[0];
                UseProfile.MultiplayerPlayer_2Slot2Unit = player_2ChosenUnitIdList[1];
                UseProfile.MultiplayerPlayer_2Slot3Unit = player_2ChosenUnitIdList[2];
                UseProfile.MultiplayerPlayer_2Slot4Unit = 0;
                UseProfile.MultiplayerPlayer_2Slot5Unit = 0;
                break;
            case 4:
                UseProfile.MultiplayerPlayer_2Slot1Unit = player_2ChosenUnitIdList[0];
                UseProfile.MultiplayerPlayer_2Slot2Unit = player_2ChosenUnitIdList[1];
                UseProfile.MultiplayerPlayer_2Slot3Unit = player_2ChosenUnitIdList[2];
                UseProfile.MultiplayerPlayer_2Slot4Unit = player_2ChosenUnitIdList[3];
                UseProfile.MultiplayerPlayer_2Slot5Unit = 0;
                break;
            case 5:
                UseProfile.MultiplayerPlayer_2Slot1Unit = player_2ChosenUnitIdList[0];
                UseProfile.MultiplayerPlayer_2Slot2Unit = player_2ChosenUnitIdList[1];
                UseProfile.MultiplayerPlayer_2Slot3Unit = player_2ChosenUnitIdList[2];
                UseProfile.MultiplayerPlayer_2Slot4Unit = player_2ChosenUnitIdList[3];
                UseProfile.MultiplayerPlayer_2Slot5Unit = player_2ChosenUnitIdList[4];
                break;
        }

    }

    void SaveCosmetic()
    {
        UseProfile.MultiplayerPlayer_1BallTextureChoice = player_1SkinChoice;
        UseProfile.MultiplayerPlayer_1BallTrailChoice = player_1TrailChoice;
        UseProfile.MultiplayerPlayer_2BallTextureChoice = player_2SkinChoice;
        UseProfile.MultiplayerPlayer_2BallTrailChoice = player_2TrailChoice;
    }
    #endregion
}