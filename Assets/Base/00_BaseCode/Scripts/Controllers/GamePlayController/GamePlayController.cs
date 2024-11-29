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

    bool modeCampaign = false;
    bool modeScoreBattle = false;
    bool modeDefenderBattle = false;

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
}
