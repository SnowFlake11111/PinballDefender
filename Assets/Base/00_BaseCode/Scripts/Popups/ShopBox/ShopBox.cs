using EventDispatcher;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class ShopBox : BaseBox
{
    #region Instance
    private static ShopBox instance;
    public static ShopBox Setup(bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<ShopBox>(PathPrefabs.SHOP));
            instance.Init();
        }

        instance.InitState();
        return instance;
    }
    #endregion

    #region Public Variables
    [Header("Player's Gem")]
    public Text playerGem;

    [Space]
    [Header("Other Buttons")]
    public Button closeBtn;

    [Space]
    [Header("Stat Changes preview")]
    public Text damageChangesPreview;
    public Text bounceChangesPreview;
    public Text reloadSpeedChangesPreview;
    public Text magazineChangesPreview;
    public Text creditsGainRateChangesPreview;
    public Text maxCreditsChangesPreview;
    public Text gateHpChangesPreview;

    [Space]
    [Header("Upgrades price")]
    public Text damagePrice;
    public Text bouncePrice;
    public Text reloadSpeedPrice;
    public Text magazinePrice;
    public Text creditsGainRatePrice;
    public Text maxCreditsPrice;
    public Text gateHpPrice;

    [Space]
    [Header("Stats upgrade Buttons")]
    public Button buyDamageBtn;
    public Button buyBounceUpgradeBtn;
    public Button buyReloadSpeedBtn;     
    public Button buyMagazineBtn;
    public Button buyCreditsGainRateUpgradeBtn;
    public Button buyMaxCreditsUpgradeBtn;
    public Button buyGateHpUpgradeBtn;

    [Space]
    [Header("Developer Section")]
    public Button add1000Gem;
    public Button lose1000Gem;
    public Button unlockAllBallTextures;
    public Button lockAllBallTextures;
    public Button unlockAllBallTrails;
    public Button lockAllBallTrails;
    public Button resetUpgradesCount;
    public Button unlockAllLevels;
    public Button resetLevelsProgress;
    #endregion

    #region Private Variables
    int damageUpgradePrice
    {
        get
        {
            return 250 + 250 * UseProfile.CampaignDamageUpgradeCount;
        }
    }
    int bounceUpgradePrice
    {
        get
        {
            return 150 + 150 * UseProfile.CampaignBounceUpgradeCount;
        }
    }
    int magazineUpgradePrice
    {
        get
        {
            return 100 + 100 * UseProfile.CampaignMagazineUpgradeCount;
        }
    }
    int reloadSpeedUpgradePrice
    {
        get
        {
            return 200 + 200 * UseProfile.CampaignReloadSpeedUpgradeCount;
        }
    }
    int creditsGainRateUpgradePrice
    {
        get
        {
            return 300 + 300 * UseProfile.CampaignCreditsGainRateUpgradeCount;
        }
    }
    int maxCreditsUpgradePrice
    {
        get
        {
            return 150 + 150 * UseProfile.CampaignMaxCreditsUpgradeCount;
        }
    }
    int gateHealthUpgradePrice
    {
        get
        {
            return 150 + 150 * UseProfile.CampaignGateHealthUpgradeCount;
        }
    }
    #endregion

    #region Start, Update
    private void Init()
    {
        closeBtn.onClick.AddListener(Close);

        buyDamageBtn.onClick.AddListener(delegate { BuyDamageUpgrade(); });
        buyBounceUpgradeBtn.onClick.AddListener(delegate { BuyBounceUpgrade(); });
        buyReloadSpeedBtn.onClick.AddListener(delegate { BuyReloadSpeedUpgrade(); });
        buyMagazineBtn.onClick.AddListener(delegate { BuyMagazineUpgrade(); });
        buyCreditsGainRateUpgradeBtn.onClick.AddListener(delegate { BuyCreditsGainRateUpgrade(); });
        buyMaxCreditsUpgradeBtn.onClick.AddListener(delegate { BuyMaxCreditsUpgrade(); });
        buyGateHpUpgradeBtn.onClick.AddListener(delegate { BuyGateHealthUpgrade(); });

        add1000Gem.onClick.AddListener(delegate { ModifyGem(1000); });
        lose1000Gem.onClick.AddListener(delegate { ModifyGem(-1000); });
        unlockAllBallTextures.onClick.AddListener(delegate { ChangeAllBallTexturesState(1); });
        lockAllBallTextures.onClick.AddListener(delegate { ChangeAllBallTexturesState(0); });
        unlockAllBallTrails.onClick.AddListener(delegate { ChangeAllBallTrailsState(1); });
        lockAllBallTrails.onClick.AddListener(delegate { ChangeAllBallTrailsState(0); });
        resetUpgradesCount.onClick.AddListener(delegate { ResetUpgrades(); });
        unlockAllLevels.onClick.AddListener(delegate { ChangeLevelProgress(1); });
        resetLevelsProgress.onClick.AddListener(delegate { ChangeLevelProgress(0); });

        playerGem.text = UseProfile.GameGem.ToString();

        PricesUpdate();
        PreviewsUpdate();

        this.RegisterListener(EventID.CHANGE_GEM, UpdateGem);
    }

    void InitState()
    {        
        BuyableUpgradesChecker();
    }
    #endregion

    #region Functions
    //---------Public----------
    //---------Private----------
    void PreviewsUpdate(int id = 0)
    {
        switch (id)
        {
            case 0:
                //Update all
                if (UseProfile.CampaignDamageUpgradeCount < 5)
                {
                    damageChangesPreview.text = (25 + 5 * UseProfile.CampaignDamageUpgradeCount).ToString() + " -> " + (25 + 5 * (UseProfile.CampaignDamageUpgradeCount + 1)).ToString();
                }
                else
                {
                    damageChangesPreview.text = (25 + 5 * UseProfile.CampaignDamageUpgradeCount).ToString();
                }

                if (UseProfile.CampaignBounceUpgradeCount < 5)
                {
                    bounceChangesPreview.text = (5 + 1 * UseProfile.CampaignBounceUpgradeCount).ToString() + " -> " + (5 + 1 * (UseProfile.CampaignBounceUpgradeCount + 1)).ToString();
                }
                else
                {
                    bounceChangesPreview.text = (5 + 1 * UseProfile.CampaignBounceUpgradeCount).ToString();
                }

                if (UseProfile.CampaignReloadSpeedUpgradeCount < 5)
                {
                    reloadSpeedChangesPreview.text = (Mathf.Round((5 - 0.5f * UseProfile.CampaignReloadSpeedUpgradeCount) * 10) / 10).ToString() + "s" + " -> " + (Mathf.Round((5 - 0.5f * (UseProfile.CampaignReloadSpeedUpgradeCount + 1)) * 10) / 10).ToString() + "s";
                }
                else
                {
                    reloadSpeedChangesPreview.text = (Mathf.Round((5 - 0.5f * UseProfile.CampaignReloadSpeedUpgradeCount) * 10) / 10).ToString() + "s";
                }

                if (UseProfile.CampaignMagazineUpgradeCount < 5)
                {
                    magazineChangesPreview.text = (5 + 1 * UseProfile.CampaignMagazineUpgradeCount).ToString() + " -> " + (5 + 1 * (UseProfile.CampaignMagazineUpgradeCount + 1)).ToString();
                }
                else
                {
                    magazineChangesPreview.text = (5 + 1 * UseProfile.CampaignMagazineUpgradeCount).ToString();
                }

                if (UseProfile.CampaignCreditsGainRateUpgradeCount < 5)
                {
                    creditsGainRateChangesPreview.text = (5 + 2 * UseProfile.CampaignCreditsGainRateUpgradeCount).ToString() + " -> " + (5 + 2 * (UseProfile.CampaignCreditsGainRateUpgradeCount + 1)).ToString();
                }
                else
                {
                    creditsGainRateChangesPreview.text = (5 + 2 * UseProfile.CampaignCreditsGainRateUpgradeCount).ToString();
                }

                if (UseProfile.CampaignMaxCreditsUpgradeCount < 5)
                {
                    maxCreditsChangesPreview.text = (100 + 80 * UseProfile.CampaignMaxCreditsUpgradeCount).ToString() + " -> " + (100 + 80 * (UseProfile.CampaignMaxCreditsUpgradeCount + 1)).ToString();
                }
                else
                {
                    maxCreditsChangesPreview.text = (100 + 80 * UseProfile.CampaignMaxCreditsUpgradeCount).ToString();
                }

                if (UseProfile.CampaignGateHealthUpgradeCount < 5)
                {
                    gateHpChangesPreview.text = (1250 + 250 * UseProfile.CampaignGateHealthUpgradeCount).ToString() + " -> " + (1250 + 250 * (UseProfile.CampaignGateHealthUpgradeCount + 1)).ToString();
                }
                else
                {
                    gateHpChangesPreview.text = (1250 + 250 * UseProfile.CampaignGateHealthUpgradeCount).ToString();
                }
                break;
            case 1:
                //Update DAMAGE
                if (UseProfile.CampaignDamageUpgradeCount < 5)
                {
                    damageChangesPreview.text = (25 + 5 * UseProfile.CampaignDamageUpgradeCount).ToString() + " -> " + (25 + 5 * (UseProfile.CampaignDamageUpgradeCount + 1)).ToString();
                }
                else
                {
                    damageChangesPreview.text = (25 + 5 * UseProfile.CampaignDamageUpgradeCount).ToString();
                }
                break;
            case 2:
                //Update BOUNCE
                if (UseProfile.CampaignBounceUpgradeCount < 5)
                {
                    bounceChangesPreview.text = (5 + 1 * UseProfile.CampaignBounceUpgradeCount).ToString() + " -> " + (5 + 1 * (UseProfile.CampaignBounceUpgradeCount + 1)).ToString();
                }
                else
                {
                    bounceChangesPreview.text = (5 + 1 * UseProfile.CampaignBounceUpgradeCount).ToString();
                }
                break;
            case 3:
                //Update RELOAD SPEED
                if (UseProfile.CampaignReloadSpeedUpgradeCount < 5)
                {
                    reloadSpeedChangesPreview.text = (Mathf.Round((5 - 0.5f * UseProfile.CampaignReloadSpeedUpgradeCount) * 10) / 10).ToString() + " -> " + (Mathf.Round((5 - 0.5f * (UseProfile.CampaignReloadSpeedUpgradeCount + 1)) * 10) / 10).ToString();
                }
                else
                {
                    reloadSpeedChangesPreview.text = (Mathf.Round((5 - 0.5f * UseProfile.CampaignReloadSpeedUpgradeCount) * 10) / 10).ToString();
                }
                break;
            case 4:
                //Update MAGAZINE
                if (UseProfile.CampaignMagazineUpgradeCount < 5)
                {
                    magazineChangesPreview.text = (5 + 1 * UseProfile.CampaignMagazineUpgradeCount).ToString() + " -> " + (5 + 1 * (UseProfile.CampaignMagazineUpgradeCount + 1)).ToString();
                }
                else
                {
                    magazineChangesPreview.text = (5 + 1 * UseProfile.CampaignMagazineUpgradeCount).ToString();
                }
                break;           
            case 5:
                //Update CREDITS GAIN RATE
                if (UseProfile.CampaignCreditsGainRateUpgradeCount < 5)
                {
                    creditsGainRateChangesPreview.text = (5 + 2 * UseProfile.CampaignCreditsGainRateUpgradeCount).ToString() + " -> " + (5 + 2 * (UseProfile.CampaignCreditsGainRateUpgradeCount + 1)).ToString();
                }
                else
                {
                    creditsGainRateChangesPreview.text = (5 + 2 * UseProfile.CampaignCreditsGainRateUpgradeCount).ToString();
                }
                break;
            case 6:
                //Update MAX CREDITS
                if (UseProfile.CampaignMaxCreditsUpgradeCount < 5)
                {
                    maxCreditsChangesPreview.text = (100 + 80 * UseProfile.CampaignMaxCreditsUpgradeCount).ToString() + " -> " + (100 + 80 * (UseProfile.CampaignMaxCreditsUpgradeCount + 1)).ToString();
                }
                else
                {
                    maxCreditsChangesPreview.text = (100 + 80 * UseProfile.CampaignMaxCreditsUpgradeCount).ToString();
                }
                break;
            case 7:
                //Update GATE HEALTH
                if (UseProfile.CampaignGateHealthUpgradeCount < 5)
                {
                    gateHpChangesPreview.text = (1250 + 250 * UseProfile.CampaignGateHealthUpgradeCount).ToString() + " -> " + (1250 + 250 * (UseProfile.CampaignGateHealthUpgradeCount + 1)).ToString();
                }
                else
                {
                    gateHpChangesPreview.text = (1250 + 250 * UseProfile.CampaignGateHealthUpgradeCount).ToString();
                }
                break;
        }
    }

    void PricesUpdate(int id = 0)
    {
        switch (id)
        {
            case 0:
                //Update all
                if (UseProfile.CampaignDamageUpgradeCount < 5)
                {
                    damagePrice.text = damageUpgradePrice.ToString();
                }
                else
                {
                    damagePrice.text = "MAXED";
                }

                if (UseProfile.CampaignBounceUpgradeCount < 5)
                {
                    bouncePrice.text = bounceUpgradePrice.ToString();
                }
                else
                {
                    bouncePrice.text = "MAXED";
                }

                if (UseProfile.CampaignReloadSpeedUpgradeCount < 5)
                {
                    reloadSpeedPrice.text = reloadSpeedUpgradePrice.ToString();
                }
                else
                {
                    reloadSpeedPrice.text = "MAXED";
                }

                if (UseProfile.CampaignMagazineUpgradeCount < 5)
                {
                    magazinePrice.text = magazineUpgradePrice.ToString();
                }
                else
                {
                    magazinePrice.text = "MAXED";
                }

                if (UseProfile.CampaignCreditsGainRateUpgradeCount < 5)
                {
                    creditsGainRatePrice.text = creditsGainRateUpgradePrice.ToString();
                }
                else
                {
                    creditsGainRatePrice.text = "MAXED";
                }

                if (UseProfile.CampaignMaxCreditsUpgradeCount < 5)
                {
                    maxCreditsPrice.text = maxCreditsUpgradePrice.ToString();
                }
                else
                {
                    maxCreditsPrice.text = "MAXED";
                }

                if (UseProfile.CampaignGateHealthUpgradeCount < 5)
                {
                    gateHpPrice.text = gateHealthUpgradePrice.ToString();
                }
                else
                {
                    gateHpPrice.text = "MAXED";
                }
                break;
            case 1:
                //Update DAMAGE
                if (UseProfile.CampaignDamageUpgradeCount < 5)
                {
                    damagePrice.text = damageUpgradePrice.ToString();
                }
                else
                {
                    damagePrice.text = "MAXED";
                }
                break;
            case 2:
                //Update BOUNCE
                if (UseProfile.CampaignBounceUpgradeCount < 5)
                {
                    bouncePrice.text = bounceUpgradePrice.ToString();
                }
                else
                {
                    bouncePrice.text = "MAXED";
                }
                break;
            case 3:
                //Update RELOAD SPEED
                if (UseProfile.CampaignReloadSpeedUpgradeCount < 5)
                {
                    reloadSpeedPrice.text = reloadSpeedUpgradePrice.ToString();
                }
                else
                {
                    reloadSpeedPrice.text = "MAXED";
                }
                break;
            case 4:
                //Update MAGAZINE
                if (UseProfile.CampaignMagazineUpgradeCount < 5)
                {
                    magazinePrice.text = magazineUpgradePrice.ToString();
                }
                else
                {
                    magazinePrice.text = "MAXED";
                }
                break;           
            case 5:
                //Update CREDITS GAIN RATE
                if (UseProfile.CampaignCreditsGainRateUpgradeCount < 5)
                {
                    creditsGainRatePrice.text = creditsGainRateUpgradePrice.ToString();
                }
                else
                {
                    creditsGainRatePrice.text = "MAXED";
                }
                break;
            case 6:
                //Update MAX CREDITS
                if (UseProfile.CampaignMaxCreditsUpgradeCount < 5)
                {
                    maxCreditsPrice.text = maxCreditsUpgradePrice.ToString();
                }
                else
                {
                    maxCreditsPrice.text = "MAXED";
                }
                break;
            case 7:
                //Update GATE HEALTH
                if (UseProfile.CampaignGateHealthUpgradeCount < 5)
                {
                    gateHpPrice.text = gateHealthUpgradePrice.ToString();
                }
                else
                {
                    gateHpPrice.text = "MAXED";
                }
                break;
        }
    }

    void BuyableUpgradesChecker()
    {
        if (UseProfile.CampaignDamageUpgradeCount < 5 && UseProfile.GameGem >= damageUpgradePrice)
        {
            buyDamageBtn.interactable = true;
        }
        else
        {
            buyDamageBtn.interactable = false;
        }

        if (UseProfile.CampaignBounceUpgradeCount < 5 && UseProfile.GameGem >= bounceUpgradePrice)
        {
            buyBounceUpgradeBtn.interactable = true;
        }
        else
        {
            buyBounceUpgradeBtn.interactable = false;
        }

        if (UseProfile.CampaignReloadSpeedUpgradeCount < 5 && UseProfile.GameGem >= reloadSpeedUpgradePrice)
        {
            buyReloadSpeedBtn.interactable = true;
        }
        else
        {
            buyReloadSpeedBtn.interactable = false;
        }

        if (UseProfile.CampaignMagazineUpgradeCount < 5 && UseProfile.GameGem >= magazineUpgradePrice)
        {
            buyMagazineBtn.interactable = true;
        }
        else
        {
            buyMagazineBtn.interactable = false;
        }

        if (UseProfile.CampaignCreditsGainRateUpgradeCount < 5 && UseProfile.GameGem >= creditsGainRateUpgradePrice)
        {
            buyCreditsGainRateUpgradeBtn.interactable = true;
        }
        else
        {
            buyCreditsGainRateUpgradeBtn.interactable = false;
        }

        if (UseProfile.CampaignMaxCreditsUpgradeCount < 5 && UseProfile.GameGem >= maxCreditsUpgradePrice)
        {
            buyMaxCreditsUpgradeBtn.interactable = true;
        }
        else
        {
            buyMaxCreditsUpgradeBtn.interactable = false;
        }

        if (UseProfile.CampaignGateHealthUpgradeCount < 5 && UseProfile.GameGem >= gateHealthUpgradePrice)
        {
            buyGateHpUpgradeBtn.interactable = true;
        }
        else
        {
            buyGateHpUpgradeBtn.interactable = false;
        }
    }

    void BuyDamageUpgrade()
    {
        UseProfile.GameGem -= damageUpgradePrice;
        UseProfile.CampaignDamageUpgradeCount++;
        PricesUpdate(1);
        PreviewsUpdate(1);
        BuyableUpgradesChecker();
        if (UseProfile.CampaignDamageUpgradeCount >= 5)
        {
            UnlockBox.Setup(new List<int> { 5 }).Show();
            UseProfile.Achievement_FullyUpgradedDamage = true;
        }
    }

    void BuyBounceUpgrade()
    {
        UseProfile.GameGem -= bounceUpgradePrice;
        UseProfile.CampaignBounceUpgradeCount++;
        PricesUpdate(2);
        PreviewsUpdate(2);
        BuyableUpgradesChecker();
        if (UseProfile.CampaignBounceUpgradeCount >= 5)
        {
            UnlockBox.Setup(new List<int> { 6 }).Show();
            UseProfile.Achievement_FullyUpgradedBounce = true;
        }
    }

    void BuyReloadSpeedUpgrade()
    {
        UseProfile.GameGem -= reloadSpeedUpgradePrice;
        UseProfile.CampaignReloadSpeedUpgradeCount++;
        PricesUpdate(3);
        PreviewsUpdate(3);
        BuyableUpgradesChecker();
        if (UseProfile.CampaignReloadSpeedUpgradeCount >= 5)
        {
            UnlockBox.Setup(new List<int> { 7 }).Show();
            UseProfile.Achievement_FullyUpgradedReloadSpeed = true;
        }
    }

    void BuyMagazineUpgrade()
    {
        UseProfile.GameGem -= magazineUpgradePrice;
        UseProfile.CampaignMagazineUpgradeCount++;
        PricesUpdate(4);
        PreviewsUpdate(4);
        BuyableUpgradesChecker();
        if (UseProfile.CampaignMagazineUpgradeCount >= 5)
        {
            UnlockBox.Setup(new List<int> { 8 }).Show();
            UseProfile.Achievement_FullyUpgradedMagazine = true;
        }
    }

    void BuyCreditsGainRateUpgrade()
    {
        UseProfile.GameGem -= creditsGainRateUpgradePrice;
        UseProfile.CampaignCreditsGainRateUpgradeCount++;
        PricesUpdate(5);
        PreviewsUpdate(5);
        BuyableUpgradesChecker();
        if (UseProfile.CampaignCreditsGainRateUpgradeCount >= 5)
        {
            UnlockBox.Setup(new List<int> { 9 }).Show();
            UseProfile.Achievement_FullyUpgradedCreditsGainRate = true;
        }
    }

    void BuyMaxCreditsUpgrade()
    {
        UseProfile.GameGem -= maxCreditsUpgradePrice;
        UseProfile.CampaignMaxCreditsUpgradeCount++;
        PricesUpdate(6);
        PreviewsUpdate(6);
        BuyableUpgradesChecker();
        if (UseProfile.CampaignMaxCreditsUpgradeCount >= 5)
        {
            UnlockBox.Setup(new List<int> { 10 }).Show();
            UseProfile.Achievement_FullyUpgradedMaxCredits = true;
        }
    }

    void BuyGateHealthUpgrade()
    {
        UseProfile.GameGem -= gateHealthUpgradePrice;
        UseProfile.CampaignGateHealthUpgradeCount++;
        PricesUpdate(7);
        PreviewsUpdate(7);
        BuyableUpgradesChecker();
        if (UseProfile.CampaignGateHealthUpgradeCount >= 5)
        {
            UnlockBox.Setup(new List<int> { 11 }).Show();
            UseProfile.Achievement_FullyUpgradedGateHealth = true;
        }
    }

    //----------Developer Functions----------
    void ResetUpgrades()
    {
        UseProfile.CampaignDamageUpgradeCount = 0;
        UseProfile.CampaignBounceUpgradeCount = 0;
        UseProfile.CampaignMagazineUpgradeCount = 0;
        UseProfile.CampaignReloadSpeedUpgradeCount = 0;
        UseProfile.CampaignCreditsGainRateUpgradeCount = 0;
        UseProfile.CampaignMaxCreditsUpgradeCount = 0;
        UseProfile.CampaignGateHealthUpgradeCount = 0;

        UseProfile.Achievement_FullyUpgradedDamage = false;
        UseProfile.Achievement_FullyUpgradedBounce = false;
        UseProfile.Achievement_FullyUpgradedReloadSpeed = false;
        UseProfile.Achievement_FullyUpgradedMagazine = false;
        UseProfile.Achievement_FullyUpgradedCreditsGainRate = false;
        UseProfile.Achievement_FullyUpgradedMaxCredits = false;
        UseProfile.Achievement_FullyUpgradedGateHealth = false;

        PricesUpdate();
        PreviewsUpdate();
        BuyableUpgradesChecker();
    }

    void ModifyGem(int amount)
    {
        if (amount == 0)
        {
            UseProfile.GameGem = 0;
            return;
        }

        if (UseProfile.GameGem + amount <= 0)
        {
            UseProfile.GameGem = 0;
        }
        else
        {
            UseProfile.GameGem += amount;
        }

        BuyableUpgradesChecker();
    }

    void ChangeAllBallTexturesState(int id)
    {
        switch (id)
        {
            case 0:
                //Lock all
                UseProfile.ScoreBattleMatchesCounter = 0;
                UseProfile.DefenderBattleMatchesCounter = 0;

                UseProfile.Achievement_FinishedAct1 = false;
                UseProfile.Achievement_FinishedAct2 = false;
                UseProfile.Achievement_Completed3ScoreBattleMatches = false;
                UseProfile.Achievement_Completed3DefenderBattleMatches = false;
                UseProfile.Achievement_FullyUpgradedDamage = false;
                break;
            case 1:
                //Unlock all
                UseProfile.ScoreBattleMatchesCounter = 3;
                UseProfile.DefenderBattleMatchesCounter = 3;

                UseProfile.Achievement_FinishedAct1 = true;
                UseProfile.Achievement_FinishedAct2 = true;               
                UseProfile.Achievement_Completed3ScoreBattleMatches = true;
                UseProfile.Achievement_Completed3DefenderBattleMatches = true;
                UseProfile.Achievement_FullyUpgradedDamage = true;
                break;
        }
    }

    void ChangeAllBallTrailsState(int id)
    {
        switch (id)
        {
            case 0:
                //Lock all
                UseProfile.Achievement_FullyUpgradedBounce = false;
                UseProfile.Achievement_FullyUpgradedReloadSpeed = false;
                UseProfile.Achievement_FullyUpgradedMagazine = false;
                UseProfile.Achievement_FullyUpgradedCreditsGainRate = false;
                UseProfile.Achievement_FullyUpgradedMaxCredits = false;
                UseProfile.Achievement_FullyUpgradedGateHealth = false;
                break;
            case 1:
                //Unlock all
                UseProfile.Achievement_FullyUpgradedBounce = true;
                UseProfile.Achievement_FullyUpgradedReloadSpeed = true;
                UseProfile.Achievement_FullyUpgradedMagazine = true;
                UseProfile.Achievement_FullyUpgradedCreditsGainRate = true;
                UseProfile.Achievement_FullyUpgradedMaxCredits = true;
                UseProfile.Achievement_FullyUpgradedGateHealth = true;
                break;
        }
    }

    void ChangeLevelProgress(int id)
    {
        switch (id)
        {
            case 0:
                //Set level progress to 0
                UseProfile.LevelProgress = 0;

                UseProfile.Achievement_FinishedAct1 = true;
                UseProfile.Achievement_FinishedAct2 = true;

                UseProfile.WarriorUnlocked = false;
                UseProfile.RangerUnlocked = false;
                UseProfile.MageUnlocked = false;
                UseProfile.EnforcerUnlocked = false;
                UseProfile.DemonUnlocked = false;
                UseProfile.MonsterUnlocked = false;
                UseProfile.HealerUnlocked = false;
                UseProfile.BerserkerUnlocked = false;
                UseProfile.BloodMageUnlocked = false;
                UseProfile.KingUnlocked = false;
                break;
            case 1:
                //Set level progress to 20
                UseProfile.LevelProgress = 20;

                UseProfile.Achievement_FinishedAct1 = false;
                UseProfile.Achievement_FinishedAct2 = false;

                UseProfile.WarriorUnlocked = true;
                UseProfile.RangerUnlocked = true;
                UseProfile.MageUnlocked = true;
                UseProfile.EnforcerUnlocked = true;
                UseProfile.DemonUnlocked = true;
                UseProfile.MonsterUnlocked = true;
                UseProfile.HealerUnlocked = true;
                UseProfile.BerserkerUnlocked = true;
                UseProfile.BloodMageUnlocked = true;
                UseProfile.KingUnlocked = true;
                break;
        }
    }

    #endregion

    #region Listener Function
    void UpdateGem(object pam)
    {
        playerGem.text = UseProfile.GameGem.ToString();
    }
    #endregion
}
