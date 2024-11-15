using Crystal;
using DG.Tweening;
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
    

    protected override void OnAwake()
    {
        //GameController.Instance.currentScene = SceneType.GamePlay;

        Init();

    }

    public void Init()
    {      
        gameScene.Init();
        
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
