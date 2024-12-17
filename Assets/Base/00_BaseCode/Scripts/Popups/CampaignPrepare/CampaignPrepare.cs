using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampaignPrepare : BaseBox
{
    #region Instance
    private static CampaignPrepare instance;
    public static CampaignPrepare Setup(int campaignStage, bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<CampaignPrepare>(PathPrefabs.CAMPAIGN_PREPARE));
            instance.Init();
        }

        instance.InitState(campaignStage);
        return instance;
    }
    #endregion

    #region Public Variables
    [Header("Player stats")]
    public Text playerDamage;
    public Text playerBounce;
    public Text playerReloadSpeed;
    public Text playerMagazine;
    public Text playerCreditsGainRate;
    public Text playerMaxCredits;
    public Text playerGateHealth;

    [Space]
    [Header("Player units")]
    public Image unit_1;
    public Image unit_2;
    public Image unit_3;
    public Image unit_4;
    public Image unit_5;

    [Space]
    public Text unit_1Cost;
    public Text unit_2Cost;
    public Text unit_3Cost;
    public Text unit_4Cost;
    public Text unit_5Cost;

    [Space]
    [Header("Unit unlock icons")]
    public Dictionary<int, GameObject> unitIconsForUnlock = new Dictionary<int, GameObject>();

    [Space]
    [Header("Unlock icon")]
    public GameObject unitUnlockCheck;

    [Space]
    [Header("Buttons")]
    public Button closeBtn;
    public Button statsChangeBtn;
    public Button unitsChangeBtn;
    public Button proceedToCampaignMode;
    #endregion

    #region Private Variables
    int previousUnitUnlockId = -1;
    int chosenCampaignStage = 0;

    int playerPreviousDamageUpgradeCount = -1;
    int playerPreviousBounceUpgradeCount = -1;
    int playerPreviousReloadSpeedUpgradeCount = -1;
    int playerPreviousMagazineUpgradeCount = -1;
    int playerPreviousCreditsGainRateUpgradeCount = -1;
    int playerPreviousMaxCreditsUpgradeCount = -1;
    int playerPreviousGateHealthUpgradeCount = -1;

    int playerPreviousUnit_1 = 0;
    int playerPreviousUnit_2 = 0;
    int playerPreviousUnit_3 = 0;
    int playerPreviousUnit_4 = 0;
    int playerPreviousUnit_5 = 0;

    Color nonTransparentColor = new Color(1, 1, 1, 1);
    Color transparentColor = new Color(1, 1, 1, 0);
    #endregion

    #region Start, Update
    public void Init()
    {
        closeBtn.onClick.AddListener(delegate { CancelCampaignPrepare(); });
        statsChangeBtn.onClick.AddListener(delegate { OpenBarrack(); });
        unitsChangeBtn.onClick.AddListener(delegate { OpenShop(); });
        proceedToCampaignMode.onClick.AddListener(delegate { ProceedToCampaignMode(); });
    }

    public void InitState(int campaignStage)
    {
        chosenCampaignStage = campaignStage;
        CheckPlayerStats();
        CheckPlayerUnits();
        UnitUnlockChecker();
    }
    #endregion

    #region Functions
    //----------Public----------
    //----------Private----------
    void CheckPlayerStats()
    {
        if (playerPreviousDamageUpgradeCount != UseProfile.CampaignDamageUpgradeCount)
        {
            playerPreviousDamageUpgradeCount = UseProfile.CampaignDamageUpgradeCount;
            playerDamage.text = (25 + 5 * UseProfile.CampaignDamageUpgradeCount).ToString();
        }

        if (playerPreviousBounceUpgradeCount != UseProfile.CampaignBounceUpgradeCount)
        {
            playerPreviousBounceUpgradeCount = UseProfile.CampaignBounceUpgradeCount;
            playerBounce.text = (5 + 1 * UseProfile.CampaignBounceUpgradeCount).ToString();
        }

        if (playerPreviousReloadSpeedUpgradeCount != UseProfile.CampaignReloadSpeedUpgradeCount)
        {
            playerPreviousReloadSpeedUpgradeCount = UseProfile.CampaignReloadSpeedUpgradeCount;
            playerReloadSpeed.text = (Mathf.Round((5 - 0.5f * UseProfile.CampaignReloadSpeedUpgradeCount) * 10) / 10).ToString() + "s";
        }

        if (playerPreviousMagazineUpgradeCount != UseProfile.CampaignMagazineUpgradeCount)
        {
            playerPreviousMagazineUpgradeCount = UseProfile.CampaignMagazineUpgradeCount;
            playerMagazine.text = (5 + 1 * UseProfile.CampaignMagazineUpgradeCount).ToString();
        }

        if (playerPreviousCreditsGainRateUpgradeCount != UseProfile.CampaignCreditsGainRateUpgradeCount)
        {
            playerPreviousCreditsGainRateUpgradeCount = UseProfile.CampaignCreditsGainRateUpgradeCount;
            playerCreditsGainRate.text = (5 + 2 * UseProfile.CampaignCreditsGainRateUpgradeCount).ToString();
        }

        if (playerPreviousMaxCreditsUpgradeCount != UseProfile.CampaignMaxCreditsUpgradeCount)
        {
            playerPreviousMaxCreditsUpgradeCount = UseProfile.CampaignMaxCreditsUpgradeCount;
            playerMaxCredits.text = (100 + 80 * UseProfile.CampaignMaxCreditsUpgradeCount).ToString();
        }

        if (playerPreviousGateHealthUpgradeCount != UseProfile.CampaignGateHealthUpgradeCount)
        {
            playerPreviousGateHealthUpgradeCount = UseProfile.CampaignGateHealthUpgradeCount;
            playerGateHealth.text = (1250 + 250 * UseProfile.CampaignGateHealthUpgradeCount).ToString();
        }
    }

    void CheckPlayerUnits()
    {
        //Slot 1
        if (UseProfile.CampaignSlot1Unit == 0)
        {
            playerPreviousUnit_1 = 0;
            unit_1.color = transparentColor;
            unit_1Cost.text = "";
        }
        else if (playerPreviousUnit_1 != UseProfile.CampaignSlot1Unit)
        {
            playerPreviousUnit_1 = UseProfile.CampaignSlot1Unit;
            unit_1.color = nonTransparentColor;
            unit_1.sprite = GameController.Instance.gameModeData.GetUnitPortrait(UseProfile.CampaignSlot1Unit);
            unit_1Cost.text = GameController.Instance.gameModeData.GetUnitCost(UseProfile.CampaignSlot1Unit).ToString();
        }

        //Slot 2
        if (UseProfile.CampaignSlot2Unit == 0)
        {
            playerPreviousUnit_1 = 0;
            unit_2.color = transparentColor;
            unit_2Cost.text = "";
        }
        else if (playerPreviousUnit_2 != UseProfile.CampaignSlot2Unit)
        {
            playerPreviousUnit_2 = UseProfile.CampaignSlot2Unit;
            unit_2.color = nonTransparentColor;
            unit_2.sprite = GameController.Instance.gameModeData.GetUnitPortrait(UseProfile.CampaignSlot2Unit);
            unit_2Cost.text = GameController.Instance.gameModeData.GetUnitCost(UseProfile.CampaignSlot2Unit).ToString();
        }

        //Slot 3
        if (UseProfile.CampaignSlot3Unit == 0)
        {
            playerPreviousUnit_1 = 0;
            unit_3.color = transparentColor;
            unit_3Cost.text = "";
        }
        else if (playerPreviousUnit_3 != UseProfile.CampaignSlot3Unit)
        {
            playerPreviousUnit_3 = UseProfile.CampaignSlot3Unit;
            unit_3.color = nonTransparentColor;
            unit_3.sprite = GameController.Instance.gameModeData.GetUnitPortrait(UseProfile.CampaignSlot3Unit);
            unit_3Cost.text = GameController.Instance.gameModeData.GetUnitCost(UseProfile.CampaignSlot3Unit).ToString();
        }

        //Slot 4
        if (UseProfile.CampaignSlot4Unit == 0)
        {
            playerPreviousUnit_1 = 0;
            unit_4.color = transparentColor;
            unit_4Cost.text = "";
        }
        else if (playerPreviousUnit_4 != UseProfile.CampaignSlot4Unit)
        {
            playerPreviousUnit_4 = UseProfile.CampaignSlot4Unit;
            unit_4.color = nonTransparentColor;
            unit_4.sprite = GameController.Instance.gameModeData.GetUnitPortrait(UseProfile.CampaignSlot4Unit);
            unit_4Cost.text = GameController.Instance.gameModeData.GetUnitCost(UseProfile.CampaignSlot4Unit).ToString();
        }

        //Slot 5
        if (UseProfile.CampaignSlot5Unit == 0)
        {
            playerPreviousUnit_1 = 0;
            unit_5.color = transparentColor;
            unit_5Cost.text = "";
        }
        else if (playerPreviousUnit_5 != UseProfile.CampaignSlot5Unit)
        {
            playerPreviousUnit_5 = UseProfile.CampaignSlot5Unit;
            unit_5.color = nonTransparentColor;
            unit_5.sprite = GameController.Instance.gameModeData.GetUnitPortrait(UseProfile.CampaignSlot5Unit);
            unit_5Cost.text = GameController.Instance.gameModeData.GetUnitCost(UseProfile.CampaignSlot5Unit).ToString();
        }
    }

    void UnitUnlockChecker()
    {
        if (previousUnitUnlockId > 0)
        {
            unitIconsForUnlock[previousUnitUnlockId].SetActive(false);
        }

        switch (chosenCampaignStage)
        {
            case 0:
                unitIconsForUnlock[1].SetActive(true);
                previousUnitUnlockId = 0;
                break;
            case 2:
                unitIconsForUnlock[2].SetActive(true);
                previousUnitUnlockId = 1;
                break;
            case 4:
                unitIconsForUnlock[4].SetActive(true);
                previousUnitUnlockId = 3;
                break;
            case 6:
                unitIconsForUnlock[3].SetActive(true);
                previousUnitUnlockId = 2;
                break;
            case 9:
                unitIconsForUnlock[5].SetActive(true);
                previousUnitUnlockId = 4;
                break;
            case 10:
                unitIconsForUnlock[6].SetActive(true);
                previousUnitUnlockId = 5;
                break;
            case 12:
                unitIconsForUnlock[7].SetActive(true);
                previousUnitUnlockId = 6;
                break;
            case 14:
                unitIconsForUnlock[9].SetActive(true);
                previousUnitUnlockId = 8;
                break;
            case 16:
                unitIconsForUnlock[8].SetActive(true);
                previousUnitUnlockId = 7;
                break;
            case 19:
                unitIconsForUnlock[10].SetActive(true);
                previousUnitUnlockId = 9;
                break;
        }

        UnitUnlockShow();
    }

    void UnitUnlockShow()
    {
        if (UseProfile.LevelProgress > chosenCampaignStage)
        {
            unitUnlockCheck.SetActive(true);
        }
        else
        {
            unitUnlockCheck.SetActive(false);
        }
    }

    void OpenBarrack()
    {
        Barrack.Setup().Show();
    }

    void OpenShop()
    {
        ShopBox.Setup().Show();
    }

    void ProceedToCampaignMode()
    {
        GameController.Instance.gameModeData.StartCampaign(chosenCampaignStage);
        GameController.Instance.StartSceneTransition("GamePlay");
    }

    void CancelCampaignPrepare()
    {
        Close();
    }
    #endregion
}
