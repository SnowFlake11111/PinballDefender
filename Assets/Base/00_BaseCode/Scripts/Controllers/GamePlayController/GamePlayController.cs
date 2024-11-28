using Crystal;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
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
    public PlayerContain playerContain;
    public GameScene gameScene;
    public GameLevelController gameLevelController;

    [OnValueChanged("CampaignActivated")]
    public bool modeCampaign = false;
    [OnValueChanged("ScoreBattleActivated")]
    public bool modeScoreBattle = false;
    [OnValueChanged("DefenderBattleActivated")]
    public bool modeDefenderBattle = false;

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

    //----------Odin Functions----------
    void CampaignActivated()
    {
        modeScoreBattle = false;
        modeDefenderBattle = false;
    }

    void ScoreBattleActivated()
    {
        modeCampaign = false;
        modeDefenderBattle = false;
    }

    void DefenderBattleActivated()
    {
        modeCampaign = false;
        modeScoreBattle = false;
    }
}
