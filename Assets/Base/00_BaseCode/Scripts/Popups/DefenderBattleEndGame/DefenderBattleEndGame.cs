using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenderBattleEndGame : BaseBox
{
    #region Instance
    private static DefenderBattleEndGame instance;
    public static DefenderBattleEndGame Setup(int loserId, bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<DefenderBattleEndGame>(PathPrefabs.DEFENDER_BATTLE_ENDGAME));
            instance.Init();
        }

        instance.InitState(loserId);
        return instance;
    }
    #endregion

    #region Public Variables
    [Header("Winner and Loser texts")]
    public GameObject player_1WinTextHolder;
    public GameObject player_1LoseTextHolder;
    public GameObject player_2WinTextHolder;
    public GameObject player_2LoseTextHolder;

    [Space]
    [Header("Match time")]
    public GameObject player_1MatchTimeHolder;
    public GameObject player_2MatchTimeHolder;
    public Text matchTime_player_1Side;
    public Text matchTime_player_2Side;

    [Space]
    [Header("Reward")]
    public GameObject player_1RewardHolder;
    public GameObject player_2RewardHolder;
    public Text player_1rewardText;
    public Text player_2rewardText;

    [Space]
    [Header("Buttons")]
    public Button player_1ReturnMenuBtn;
    public Button player_1RematchBtn;

    public Button player_2ReturnMenuBtn;
    public Button player_2RematchBtn;
    #endregion

    #region Private Variables
    int gemReward
    {
        get
        {
            return GamePlayController.Instance.gameLevelController.currentLevel.prizeGem + Mathf.RoundToInt(GamePlayController.Instance.gameLevelController.currentLevel.prizeGem * 0.25f * (GamePlayController.Instance.gameScene.GetDefenderClockTime() % 30));
        }
    }
    int player_1rewardValue = 0;
    int player_2rewardValue = 0;

    bool player_1HasTakenAction = false;
    bool player_2HasTakenAction = false;

    Tweener rewardHolderAnimation;
    Tweener rewardValueAnimation;
    #endregion

    #region Start, Update
    public void Init()
    {
        player_1ReturnMenuBtn.onClick.AddListener(delegate { PlayerChoice(1, 0); });
        player_1RematchBtn.onClick.AddListener(delegate { PlayerChoice(1, 1); });

        player_2ReturnMenuBtn.onClick.AddListener (delegate { PlayerChoice(2, 0); });
        player_2RematchBtn.onClick.AddListener(delegate { PlayerChoice(2, 1); });
    }

    public void InitState(int loserId)
    {
        MatchResult(loserId);
        ShowMatchTime();
        AnimateRewardValue();
    }
    #endregion

    #region Functions
    //----------Public----------
    //----------Private----------
    void MatchResult(int loserId)
    {
        GameController.Instance.musicManager.PlaySoundEffect(6);

        switch (loserId)
        {
            case 1:
                player_1LoseTextHolder.transform.localScale = Vector3.zero;
                player_2WinTextHolder.transform.localScale = Vector3.zero;

                player_1LoseTextHolder.SetActive(true);
                player_2WinTextHolder.SetActive(true);

                player_1LoseTextHolder.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.Linear);
                player_2WinTextHolder.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.Linear);
                break;
            case 2:
                player_1WinTextHolder.transform.localScale = Vector3.zero;
                player_2LoseTextHolder.transform.localScale = Vector3.zero;

                player_1WinTextHolder.SetActive(true);
                player_2LoseTextHolder.SetActive(true);

                player_1WinTextHolder.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.Linear);
                player_2LoseTextHolder.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.Linear);
                break;
        }
    }

    void ShowMatchTime()
    {
        int hour = GamePlayController.Instance.gameScene.GetDefenderClockTime() / 3600;
        int minute = GamePlayController.Instance.gameScene.GetDefenderClockTime() % 3600 / 60;
        int second = GamePlayController.Instance.gameScene.GetDefenderClockTime() % 60;

        player_1MatchTimeHolder.SetActive(true);
        player_2MatchTimeHolder.SetActive(true);

        matchTime_player_1Side.text = string.Format("{0: 00}:{1: 00}:{2: 00}", hour, minute, second);
        matchTime_player_2Side.text = string.Format("{0: 00}:{1: 00}:{2: 00}", hour, minute, second);
    }

    void AnimateRewardValue()
    {
        player_1RewardHolder.transform.localScale = Vector3.zero;
        player_1RewardHolder.SetActive(true);
        player_2RewardHolder.transform.localScale = Vector3.zero;
        player_2RewardHolder.SetActive(true);


        rewardHolderAnimation = player_1RewardHolder.transform.DOScale(Vector3.one * 1.5f, 0.25f)
            .SetDelay(0.5f)
            .SetEase(Ease.Linear)
            .OnComplete(delegate
            {
                rewardHolderAnimation = player_1RewardHolder.transform.DOScale(Vector3.one, 0.25f)
                    .SetEase(Ease.Linear)
                    .OnComplete(delegate
                    {
                        rewardValueAnimation = DOTween.To
                            (delegate
                            {
                                return player_1rewardValue;
                            },
                            delegate (int x)
                            {
                                player_1rewardValue = x;
                            },
                            gemReward,
                            0.5f
                            )
                            .OnUpdate(delegate
                            {
                                player_1rewardText.text = player_1rewardValue.ToString();
                            })
                            .OnComplete(delegate
                            {
                                rewardValueAnimation = null;
                            });
                    });
            });

        rewardHolderAnimation = player_2RewardHolder.transform.DOScale(Vector3.one * 1.5f, 0.5f)
            .SetDelay(0.5f)
            .SetEase(Ease.Linear)
            .OnComplete(delegate
            {
                rewardHolderAnimation = player_2RewardHolder.transform.DOScale(Vector3.one, 0.5f)
                    .SetEase(Ease.Linear)
                    .OnComplete(delegate
                    {
                        rewardValueAnimation = DOTween.To
                            (delegate
                            {
                                return player_2rewardValue;
                            },
                            delegate (int x)
                            {
                                player_2rewardValue = x;
                            },
                            gemReward,
                            0.5f
                            )
                            .OnUpdate(delegate
                            {
                                player_2rewardText.text = player_2rewardValue.ToString();
                            })
                            .OnComplete(delegate
                            {
                                rewardValueAnimation = null;

                                player_1ReturnMenuBtn.gameObject.SetActive(true);
                                player_1RematchBtn.gameObject.SetActive(true);
                                player_2ReturnMenuBtn.gameObject.SetActive(true);
                                player_2RematchBtn.gameObject.SetActive(true);
                            });
                    });
            });
    }

    void PlayerChoice(int playerId, int choiceId)
    {
        switch (playerId)
        {
            case 1:
                switch (choiceId)
                {
                    case 0:
                        BackToMenu();
                        break;
                    case 1:
                        player_1HasTakenAction = true;
                        player_1ReturnMenuBtn.interactable = false;
                        player_1RematchBtn.interactable = false;
                        break;
                }
                break;
            case 2:
                switch (choiceId)
                {
                    case 0:
                        BackToMenu();
                        break;
                    case 1:
                        player_2HasTakenAction = true;
                        player_2ReturnMenuBtn.interactable = false;
                        player_2RematchBtn.interactable = false;
                        break;
                }
                break;
        }

        if (player_1HasTakenAction && player_2HasTakenAction)
        {
            Rematch();
        }
    }

    void BackToMenu()
    {
        GameController.Instance.StartSceneTransition("HomeScene");
    }

    void Rematch()
    {
        GameController.Instance.StartSceneTransition("GamePlay");
    }
    #endregion
}