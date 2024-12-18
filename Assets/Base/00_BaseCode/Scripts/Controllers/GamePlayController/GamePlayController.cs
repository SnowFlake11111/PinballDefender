using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum StateGame
{
    Loading = 0,
    Playing = 1,
    Win = 2,
    Lose = 3,
    Pause = 4
}

public class GamePlayController : Singleton<GamePlayController>
{
    public GameScene gameScene;
    public GameLevelController gameLevelController;

    bool modeCampaign = false;
    bool modeScoreBattle = false;
    bool modeDefenderBattle = false;

    [BoxGroup("Stage Editor")]
    public StageController campaignBase, scoreBase, defenderBase;

    [SerializeField] StageController tempRef;

    protected override void OnAwake()
    {
        //GameController.Instance.currentScene = SceneType.GamePlay;

        Init();

    }

    public void Init()
    {
        gameLevelController.Init();

        if (modeCampaign)
        {
            gameScene.Init(1);
        }
        else if (modeScoreBattle)
        {
            gameScene.Init(2);
        }
        else
        {
            gameScene.Init(3);
        }

        gameLevelController.PlayersCreditsCheck();
        GameController.Instance.FinishSceneTransition();
    }

    public void ChangeUILoadout(int id)
    {
        switch (id)
        {
            case 1:     //Campaign
                modeCampaign = true;
                modeScoreBattle = false;
                modeDefenderBattle = false;
                break;
            case 2:     //ScoreBattle
                modeScoreBattle = true;
                modeCampaign = false;
                modeDefenderBattle = false;
                break;
            case 3:     //DefenderBattle
                modeDefenderBattle = true;
                modeCampaign = false;
                modeScoreBattle = false;
                break;
        }
    }

    //private void OnEnable()
    //{
    //    ChangeBannerStateInGamePlay(false);
    //}

    //private void OnDisable()
    //{
    //    ChangeBannerStateInGamePlay(true);
    //}

    void ChangeBannerStateInGamePlay(bool turnBannerOn)
    {
        if(turnBannerOn)
        {
            GameController.Instance.admobAds.ShowBanner();
        }
        else
        {
            GameController.Instance.admobAds.DestroyBanner();
        }
       
    }

#if UNITY_EDITOR
    [Button]
    void SpawnCampaignBase()
    {
        string[] temp = AssetDatabase.FindAssets("t: Prefab", new[] { PathPrefabs.CAMPAIGN_LEVELS_FOLDER });
        int numberOfCampaignLevel = temp.Length;

        tempRef = Instantiate(campaignBase);
        tempRef.name = "Campaign_Level_" + (numberOfCampaignLevel + 1);
        tempRef.stageId = numberOfCampaignLevel + 1;
    }

    [Button]
    void SpawnScoreBattleBase()
    {
        string[] temp = AssetDatabase.FindAssets("t: Prefab", new[] { PathPrefabs.SCORE_LEVELS_FOLDER });
        int numberOfScoreLevel = temp.Length;

        tempRef = Instantiate(scoreBase);
        tempRef.name = "ScoreBattle_Level_" + (numberOfScoreLevel + 1);
    }

    [Button]
    void SpawnDefenderBattleBase()
    {
        string[] temp = AssetDatabase.FindAssets("t: Prefab", new[] { PathPrefabs.DEFENDER_LEVELS_FOLDER });
        int numberOfDefenderLevel = temp.Length;

        tempRef = Instantiate(defenderBase);
        tempRef.name = "DefenderBattle_Level_" + (numberOfDefenderLevel + 1);
    }

    [Button]
    void SaveSpawnedStage()
    {
        if (tempRef.modeCampaign)
        {
            PrefabUtility.SaveAsPrefabAsset(tempRef.gameObject, PathPrefabs.CAMPAIGN_LEVELS_FOLDER + tempRef.name + ".prefab");
        }
        else if (tempRef.modeScoreBattle)
        {
            PrefabUtility.SaveAsPrefabAsset(tempRef.gameObject, PathPrefabs.SCORE_LEVELS_FOLDER + tempRef.name + ".prefab");
        }
        else
        {
            PrefabUtility.SaveAsPrefabAsset(tempRef.gameObject, PathPrefabs.DEFENDER_LEVELS_FOLDER + tempRef.name + ".prefab");
        }

        DestroyImmediate(tempRef.gameObject);
    }
#endif
}
