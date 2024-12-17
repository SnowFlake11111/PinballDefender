using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockBox : BaseBox
{
    #region Instance
    private static UnlockBox instance;
    public static UnlockBox Setup(List<int> rewardIds, bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<UnlockBox>(PathPrefabs.UNLOCK_BOX));
            instance.Init();
        }

        instance.InitState(rewardIds);
        return instance;
    }
    #endregion

    #region Public Variables
    [Header("Reward prefab references")]
    public Dictionary<int, GameObject> rewardReferences = new Dictionary<int, GameObject>();

    [Space]
    [Header("Rewards holder")]
    public GameObject rewardHolder;

    [Space]
    [Header("Buttons")]
    public Button closeBtn;
    #endregion

    #region Private Variables
    List<GameObject> spawnedRewards = new List<GameObject>();

    GameObject tempObjectRef;
    #endregion

    #region Start, Update
    public void Init()
    {
        closeBtn.onClick.AddListener(delegate { CloseRewardScreen(); });
    }

    public void InitState(List<int> rewardIds)
    {
        RegisterRewardsToShow(rewardIds);
    }
    #endregion

    #region Functions
    //----------Public----------
    //----------Private----------
    void RegisterRewardsToShow(List<int> rewardIds)
    {
        foreach(int rewardId in rewardIds)
        {
            ProcessRewardData(rewardId);
        }
    }

    void ProcessRewardData(int id)
    {
        tempObjectRef = Instantiate(rewardReferences[id], rewardHolder.transform);
        tempObjectRef.transform.localScale = Vector3.one;
        spawnedRewards.Add(tempObjectRef);
    }

    void CloseRewardScreen()
    {
        foreach (GameObject rewards in spawnedRewards)
        {
            Destroy(rewards);
        }

        Close();
    }
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
 * 1001: GameMode ScoreBattle
 * 1002: GameMode DefenderBattle
 */