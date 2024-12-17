using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementBox : BaseBox
{
    #region Instance
    private static AchievementBox instance;
    public static AchievementBox Setup(bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<AchievementBox>(PathPrefabs.ACHIEVEMENT_BOX));
            instance.Init();
        }

        instance.InitState();
        return instance;
    }
    #endregion

    #region Public Variables
    [Header("Check icon")]
    public List<GameObject> checkIcons = new List<GameObject>();

    [Space]
    [Header("Achievement progress text")]
    public Text achievementFinish3ScoreBattlesText;
    public Text achievementFinish3DefenderBattlesText;
    public Image achievementFinish3ScoreBattlesProgress;
    public Image achievementFinish3DefenderBattlesProgress;

    [Space]
    [Header("Buttons")]
    public Button closeBtn;
    #endregion

    #region Private Variables
    #endregion

    #region Start, Update
    public void Init()
    {
        closeBtn.onClick.AddListener(Close);
    }

    public void InitState()
    {
        AchievementCheck();
        AchievementProgressUpdater();
    }
    #endregion

    #region Functions
    //----------Public----------
    //----------Private----------
    void AchievementCheck()
    {

        if (UseProfile.Achievement_FinishedAct1)
        {
            checkIcons[0].SetActive(true);
        }
        else
        {
            checkIcons[0].SetActive(false);
        }

        if (UseProfile.Achievement_FinishedAct2)
        {
            checkIcons[1].SetActive(true);
        }
        else
        {
            checkIcons[1].SetActive(false);
        }

        if (UseProfile.Achievement_Completed3ScoreBattleMatches)
        {
            checkIcons[2].SetActive(true);
        }
        else
        {
            checkIcons[2].SetActive(false);
        }

        if (UseProfile.Achievement_Completed3DefenderBattleMatches)
        {
            checkIcons[3].SetActive(true);
        }
        else
        {
            checkIcons[3].SetActive(false);
        }

        if (UseProfile.Achievement_FullyUpgradedDamage)
        {
            checkIcons[4].SetActive(true);
        }
        else
        {
            checkIcons[4].SetActive(false);
        }


        if (UseProfile.Achievement_FullyUpgradedBounce)
        {
            checkIcons[5].SetActive(true);
        }
        else
        {
            checkIcons[5].SetActive(false);
        }

        if (UseProfile.Achievement_FullyUpgradedReloadSpeed)
        {
            checkIcons[6].SetActive(true);
        }
        else
        {
            checkIcons[6].SetActive(false);
        }

        if (UseProfile.Achievement_FullyUpgradedMagazine)
        {
            checkIcons[7].SetActive(true);
        }
        else
        {
            checkIcons[7].SetActive(false);
        }

        if (UseProfile.Achievement_FullyUpgradedCreditsGainRate)
        {
            checkIcons[8].SetActive(true);
        }
        else
        {
            checkIcons[8].SetActive(false);
        }

        if (UseProfile.Achievement_FullyUpgradedMaxCredits)
        {
            checkIcons[9].SetActive(true);
        }
        else
        {
            checkIcons[9].SetActive(false);
        }

        if (UseProfile.Achievement_FullyUpgradedGateHealth)
        {
            checkIcons[10].SetActive(true);
        }
        else
        {
            checkIcons[10].SetActive(false);
        }
    }

    void AchievementProgressUpdater()
    {
        achievementFinish3ScoreBattlesText.text = UseProfile.ScoreBattleMatchesCounter.ToString() + "/3";
        achievementFinish3DefenderBattlesText.text = UseProfile.DefenderBattleMatchesCounter.ToString() + "/3";

        achievementFinish3ScoreBattlesProgress.fillAmount = UseProfile.ScoreBattleMatchesCounter / 3f;
        achievementFinish3DefenderBattlesProgress.fillAmount = UseProfile.DefenderBattleMatchesCounter / 3f;
    }
    #endregion
}
