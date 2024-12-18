using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementController : MonoBehaviour
{
    #region Public Variables
    #endregion

    #region Private Variables
    List<int> tempRewardList = new List<int>();
    #endregion

    #region Start, Update
    #endregion

    #region Functions
    //----------Public----------
    public void AchievementCheckerForBattles()
    {
        if (!UseProfile.Achievement_FinishedAct1)
        {
            if (UseProfile.LevelProgress >= 10)
            {
                //Open pop-up reward
                tempRewardList.AddRange(new List<int>() { 1, 1001, 1002 });
                UseProfile.Achievement_FinishedAct1 = true;
            }
        }

        if (!UseProfile.Achievement_FinishedAct2)
        {
            if (UseProfile.LevelProgress >= 20)
            {
                //Open pop-up reward
                tempRewardList.Add(2);
                UseProfile.Achievement_FinishedAct2 = true;
            }
        }

        if (!UseProfile.Achievement_Completed3ScoreBattleMatches)
        {
            if (UseProfile.ScoreBattleMatchesCounter >= 3)
            {
                //Open pop-up reward
                tempRewardList.Add(3);
                UseProfile.Achievement_Completed3ScoreBattleMatches = true;
            }
        }

        if (!UseProfile.Achievement_Completed3DefenderBattleMatches)
        {
            if (UseProfile.DefenderBattleMatchesCounter >= 3)
            {
                //Open pop-up reward
                tempRewardList.Add(4);
                UseProfile.Achievement_Completed3DefenderBattleMatches = true;
            }
        }

        if (tempRewardList.Count > 0)
        {
            UnlockBox.Setup(tempRewardList).Show();
            tempRewardList.Clear();
        }
    }

    public void RegisterUnitUnlock(int id)
    {
        switch(id)
        {
            case 1:
                if (!UseProfile.WarriorUnlocked)
                {
                    tempRewardList.Add(101);
                    UseProfile.WarriorUnlocked = true;
                }
                break;
            case 2:
                if (!UseProfile.RangerUnlocked)
                {
                    tempRewardList.Add(102);
                    UseProfile.RangerUnlocked = true;
                }
                break;
            case 3:
                if (!UseProfile.MageUnlocked)
                {
                    tempRewardList.Add(103);
                    UseProfile.MageUnlocked = true;
                }
                break;
            case 4:
                if (!UseProfile.EnforcerUnlocked)
                {
                    tempRewardList.Add(104);
                    UseProfile.EnforcerUnlocked = true;
                }
                break;
            case 5:
                if (!UseProfile.DemonUnlocked)
                {
                    tempRewardList.Add(105);
                    UseProfile.DemonUnlocked = true;
                }
                break;
            case 6:
                if (!UseProfile.MonsterUnlocked)
                {
                    tempRewardList.Add(106);
                    UseProfile.MonsterUnlocked = true;
                }
                break;
            case 7:
                if (!UseProfile.HealerUnlocked)
                {
                    tempRewardList.Add(107);
                    UseProfile.HealerUnlocked = true;
                }
                break;
            case 8:
                if (!UseProfile.BerserkerUnlocked)
                {
                    tempRewardList.Add(108);
                    UseProfile.BerserkerUnlocked = true;
                }
                break;
            case 9:
                if (!UseProfile.BloodMageUnlocked)
                {
                    tempRewardList.Add(109);
                    UseProfile.BloodMageUnlocked = true;
                }
                break;
            case 10:
                if (!UseProfile.KingUnlocked)
                {
                    tempRewardList.Add(110);
                    UseProfile.KingUnlocked = true;
                }
                break;
        }
    }

    public bool UnlockSkinStatus(int skinId)
    {
        switch(skinId)
        {
            case 11:
                if (UseProfile.Achievement_FinishedAct1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 12:
                if (UseProfile.Achievement_FinishedAct2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 13:
                if (UseProfile.Achievement_Completed3ScoreBattleMatches)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 14:
                if (UseProfile.Achievement_Completed3DefenderBattleMatches)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 15:
                if (UseProfile.Achievement_FullyUpgradedDamage)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            default:
                Debug.LogError("Invalid Achievement Id for SKIN unlock, please check");
                return false;
        }
    }

    public bool UnlockTrailStatus(int trailId)
    {
        switch (trailId)
        {
            case 11:
                if (UseProfile.Achievement_FullyUpgradedBounce)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 12:
                if (UseProfile.Achievement_FullyUpgradedReloadSpeed)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 13:
                if (UseProfile.Achievement_FullyUpgradedMagazine)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 14:
                if (UseProfile.Achievement_FullyUpgradedCreditsGainRate)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 15:
                if (UseProfile.Achievement_FullyUpgradedMaxCredits)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 16:
                if (UseProfile.Achievement_FullyUpgradedGateHealth)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            default:
                Debug.LogError("Invalid Achievement Id for TRAIL unlock, please check");
                return false;
        }
    }

    public void UpdateScoreBattleCount()
    {
        if (!UseProfile.Achievement_Completed3ScoreBattleMatches)
        {
            UseProfile.ScoreBattleMatchesCounter++;
            if (UseProfile.ScoreBattleMatchesCounter >= 3)
            {
                UseProfile.Achievement_Completed3ScoreBattleMatches = true;
            }
        }
    }

    public void UpdateDefenderBattleCount()
    {
        if (!UseProfile.Achievement_Completed3DefenderBattleMatches)
        {
            UseProfile.DefenderBattleMatchesCounter++;
            if (UseProfile.DefenderBattleMatchesCounter >= 3)
            {
                UseProfile.Achievement_Completed3DefenderBattleMatches = true;
            }
        }
    }
    //----------Private----------
    #endregion

}
/*
 * List of rewards:
 * 1: skin 11
 * 2: skin 12
 * 3: skin 13
 * 4: skin 14
 * 5: skin 15
 * 6: trail 11
 * 7: trail 12
 * 8: trail 13
 * 9: trail 14
 * 10: trail 15
 * 11: trail 16
 * 101 Warrior unit
 * 102: Ranger unit
 * 103: Mage unit
 * 104: Enforcer unit
 * 105: Demon unit
 * 106: Monster unit
 * 107: Healer unit
 * 108: Berserker unit
 * 109: BloodMage unit
 * 110: King unit
 * 1001: ScoreBattle Mode
 * 1002: DefenderBattle Mode
 */