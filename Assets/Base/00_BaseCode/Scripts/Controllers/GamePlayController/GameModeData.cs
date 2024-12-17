using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeData : SerializedMonoBehaviour
{
    #region Public Variables
    [Header("Ball skins & trails")]
    public List<Material> ballSkins = new List<Material>();
    public List<GameObject> ballTrails = new List<GameObject>();

    [Space]
    [Header("Unit datas")]
    [DictionaryDrawerSettings(KeyLabel = "Unit's ID", ValueLabel = "Unit Reference")]
    public Dictionary<int, GameUnitBase> units = new Dictionary<int, GameUnitBase>();

    [DictionaryDrawerSettings(KeyLabel = "Unit's ID", ValueLabel = "Unit's Portrait")]
    public Dictionary<int, Sprite> unitPortraits = new Dictionary<int, Sprite>();
    #endregion

    #region Private Variables
    bool campaignMode = false;
    bool scoreBattleMode = false;
    bool defenderBattleMode = false;

    bool scoreAct1Only = false;
    bool scoreAct2Only = false;

    int choosenCampaignStage = 0;

    int player_1DamageUpgradeCount = 0;
    int player_1BounceUpgradeCount = 0;
    int player_1MagazineUpgradeCount = 0;
    int player_1ReloadSpeedUpgradeCount = 0;
    int player_1CreditsGainRateUpgradeCount = 0;
    int player_1MaxCreditsUpgradeCount = 0;
    int player_1GateHealthUpgradeCount = 0;

    int player_2DamageUpgradeCount = 0;
    int player_2BounceUpgradeCount = 0;
    int player_2MagazineUpgradeCount = 0;
    int player_2ReloadSpeedUpgradeCount = 0;
    int player_2CreditsGainRateUpgradeCount = 0;
    int player_2MaxCreditsUpgradeCount = 0;
    int player_2GateHealthUpgradeCount = 0;
    #endregion

    #region Functions
    //----------Public----------
    public int GetChoosenMode()
    {
        if (campaignMode)
        {
            return 1;
        }
        else if (scoreBattleMode)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    public int GetCampaignStage()
    {
        return choosenCampaignStage;
    }

    public int GetScoreStageChoices()
    {
        if (scoreAct1Only && scoreAct2Only)
        {
            return 3;
        }
        else if (scoreAct2Only)
        {
            return 2;
        }
        else
        {
            return 1;
        }       
    }

    public void StartCampaign(int stageId)
    {
        choosenCampaignStage = stageId;

        campaignMode = true;
        scoreBattleMode = false;
        defenderBattleMode = false;
    }

    public void StartScore(int modeSelection)
    {
        switch (modeSelection)
        {
            case 1:
                scoreAct1Only = true;
                scoreAct2Only = false;
                break;
            case 2:
                scoreAct1Only = false;
                scoreAct2Only = true;
                break;
            case 3:
                scoreAct1Only = true;
                scoreAct2Only = true;
                break;
        }

        campaignMode = false;
        scoreBattleMode = true;
        defenderBattleMode = false;
    }

    public void StartDefender()
    {
        campaignMode = false;
        scoreBattleMode = false;
        defenderBattleMode = true;
    }

    public void RegisterPlayer_1ScoreChoices(int damage = 0, int bounce = 0, int magazine = 0, int reloadSpeed = 0)
    {
        player_1DamageUpgradeCount = damage;
        player_1BounceUpgradeCount = bounce;
        player_1MagazineUpgradeCount = magazine;
        player_1ReloadSpeedUpgradeCount = reloadSpeed;
    }

    public void RegisterPlayer_2ScoreChoices(int damage = 0, int bounce = 0, int magazine = 0, int reloadSpeed = 0)
    {
        player_2DamageUpgradeCount = damage;
        player_2BounceUpgradeCount = bounce;
        player_2MagazineUpgradeCount = magazine;
        player_2ReloadSpeedUpgradeCount = reloadSpeed;
    }

    public void RegisterPlayer_1DefenderChoices(int damage = 0, int bounce = 0, int magazine = 0, int reloadSpeed = 0, int creditsGainRate = 0, int maxCredits = 0, int gateHealth = 0)
    {
        player_1DamageUpgradeCount = damage;
        player_1BounceUpgradeCount = bounce;
        player_1MagazineUpgradeCount = magazine;
        player_1ReloadSpeedUpgradeCount = reloadSpeed;
        player_1CreditsGainRateUpgradeCount = creditsGainRate;
        player_1MaxCreditsUpgradeCount = maxCredits;
        player_1GateHealthUpgradeCount = gateHealth;
    }

    public void RegisterPlayer_2DefenderChoices(int damage = 0, int bounce = 0, int magazine = 0, int reloadSpeed = 0, int creditsGainRate = 0, int maxCredits = 0, int gateHealth = 0)
    {
        player_2DamageUpgradeCount = damage;
        player_2BounceUpgradeCount = bounce;
        player_2MagazineUpgradeCount = magazine;
        player_2ReloadSpeedUpgradeCount = reloadSpeed;
        player_2CreditsGainRateUpgradeCount = creditsGainRate;
        player_2MaxCreditsUpgradeCount = maxCredits;
        player_2GateHealthUpgradeCount = gateHealth;
    }

    public int GetPlayerDamageUpgrade(int id)
    {
        switch(id)
        {
            case 1:
                return player_1DamageUpgradeCount;
            case 2:
                return player_2DamageUpgradeCount;
            default:
                return 0;
        }
    }

    public int GetPlayerBounceUpgrade(int id)
    {
        switch (id)
        {
            case 1:
                return player_1BounceUpgradeCount;
            case 2:
                return player_2BounceUpgradeCount;
            default:
                return 0;
        }
    }

    public int GetPlayerMagazineUpgrade(int id)
    {
        switch (id)
        {
            case 1:
                return player_1MagazineUpgradeCount;
            case 2:
                return player_2MagazineUpgradeCount;
            default:
                return 0;
        }
    }

    public int GetPlayerReloadSpeedUpgrade(int id)
    {
        switch (id)
        {
            case 1:
                return player_1ReloadSpeedUpgradeCount;
            case 2:
                return player_2ReloadSpeedUpgradeCount;
            default:
                return 0;
        }
    }

    public int GetPlayerCreditsGainRateUpgrade(int id)
    {
        switch (id)
        {
            case 1:
                return player_1CreditsGainRateUpgradeCount;
            case 2:
                return player_2CreditsGainRateUpgradeCount;
            default:
                return 0;
        }
    }

    public int GetPlayerMaxCreditsUpgrade(int id)
    {
        switch (id)
        {
            case 1:
                return player_1MaxCreditsUpgradeCount;
            case 2:
                return player_2MaxCreditsUpgradeCount;
            default:
                return 0;
        }
    }

    public int GetPlayerGateHealthUpgrade(int id)
    {
        switch (id)
        {
            case 1:
                return player_1GateHealthUpgradeCount;
            case 2:
                return player_2GateHealthUpgradeCount;
            default:
                return 0;
        }
    }

    public Material GetBallSkin(int id)
    {
        if (id < ballSkins.Count && id >= 0)
        {
            return ballSkins[id];
        }
        else
        {
            Debug.LogError("Invalid skin id");
            return null;
        }
    }

    public GameObject GetBallTrail(int id)
    {
        if (id < ballTrails.Count && id >= 0)
        {
            return ballTrails[id];
        }
        else
        {
            Debug.LogError("Invalid trail id");
            return null;
        }
    }

    public GameUnitBase GetUnitScript(int unitId)
    {
        return units[unitId];
    }

    public Sprite GetUnitPortrait(int unitId)
    {
        return unitPortraits[unitId];
    }

    public int GetUnitCost(int unitId)
    {
        return units[unitId].spawnCost;
    }

    //----------Private----------
    #endregion
}
