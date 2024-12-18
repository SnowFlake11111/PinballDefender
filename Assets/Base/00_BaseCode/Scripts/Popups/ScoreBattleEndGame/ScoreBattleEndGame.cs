using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBattleEndGame : BaseBox
{
    #region Instance
    private static ScoreBattleEndGame instance;
    public static ScoreBattleEndGame Setup(bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<ScoreBattleEndGame>(PathPrefabs.SCORE_BATTLE_ENDGAME));
            instance.Init();
        }

        instance.InitState();
        return instance;
    }
    #endregion

    #region Public Variables
    [Header("Win and Lose text")]
    public GameObject player_1WinTextHolder;
    public GameObject player_2WinTextHolder;
    public GameObject matchDrawTextHolder;

    [Space]
    [Header("Player score")]
    public Text player_1Score;
    public Text player_2Score;

    [Space]
    [Header("Reward")]
    public GameObject rewardHolder;
    public Text rewardText;

    [Space]
    [Header("Buttons")]
    public Button backToMenuBtn;
    public Button playAgainBtn;
    #endregion

    #region Private Variables
    int player_1TempScore = 0;
    int player_2TempScore = 0;
    int rewardValue = 0;

    int matchResult
    {
        get
        {
            if (GamePlayController.Instance.gameLevelController.currentLevel.player_1.GetScore() > GamePlayController.Instance.gameLevelController.currentLevel.player_2.GetScore())
            {
                return 1;
            }
            else if (GamePlayController.Instance.gameLevelController.currentLevel.player_1.GetScore() < GamePlayController.Instance.gameLevelController.currentLevel.player_2.GetScore())
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
    }

    Tweener winTextAnimation;
    Tweener drawTextAnimation;
    Tweener player_1ScoreAnimation;
    Tweener player_2ScoreAnimation;
    Tweener rewardHolderAnimation;
    Tweener rewardValueAnimation;
    #endregion

    #region Start, Update
    public void Init()
    {
        backToMenuBtn.onClick.AddListener(delegate { BackToMenu(); });
        playAgainBtn.onClick.AddListener(delegate { PlayAgain(); });
    }

    public void InitState()
    {
        AnimateScore();
    }
    #endregion

    #region Functions
    //----------Public----------
    //----------Private----------
    void AnimateScore()
    {
        player_1ScoreAnimation = DOTween.To
            (
            delegate
            {
                return player_1TempScore;
            },
            delegate (int x)
            {
                player_1TempScore = x;
            },
            GamePlayController.Instance.gameLevelController.currentLevel.player_1.GetScore(),
            1000f
            )
            .SetSpeedBased(true)
            .OnUpdate(delegate
            {
                player_1Score.text = player_1TempScore.ToString();
            })
            .OnComplete(delegate
            {
                if (matchResult == 1)
                {
                    AnimateWinText(1);
                }
            });

        player_2ScoreAnimation = DOTween.To
            (
            delegate
            {
                return player_2TempScore;
            },
            delegate (int x)
            {
                player_2TempScore = x;
            },
            GamePlayController.Instance.gameLevelController.currentLevel.player_2.GetScore(),
            1000f
            )
            .SetSpeedBased(true)
            .OnUpdate(delegate
            {
                player_2Score.text = player_2TempScore.ToString();
            })
            .OnComplete(delegate
            {
                if (matchResult == 2)
                {
                    AnimateWinText(2);
                }
            });

        if (matchResult == 3)
        {
            StartCoroutine(Draw());
        }
    }

    void AnimateWinText(int playerId)
    {
        switch (playerId)
        {
            case 1:
                GameController.Instance.musicManager.PlaySoundEffect(11);

                player_1WinTextHolder.transform.localScale = Vector3.zero;
                player_1WinTextHolder.SetActive(true);
                winTextAnimation = player_1WinTextHolder.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f)
                    .SetEase(Ease.Linear)
                    .OnComplete(delegate
                    {
                        winTextAnimation = player_1WinTextHolder.transform.DOScale(Vector3.one, 0.5f)
                           .SetEase(Ease.Linear)
                           .OnComplete(delegate
                           {
                               backToMenuBtn.gameObject.SetActive(true);
                               playAgainBtn.gameObject.SetActive(true);
                               AnimateRewardValue();
                           });
                    });
                break;
            case 2:
                GameController.Instance.musicManager.PlaySoundEffect(11);

                player_2WinTextHolder.transform.localScale = Vector3.zero;
                player_2WinTextHolder.SetActive(true);
                winTextAnimation = player_2WinTextHolder.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f)
                    .SetEase(Ease.Linear)
                    .OnComplete(delegate
                    {
                        winTextAnimation = player_2WinTextHolder.transform.DOScale(Vector3.one, 0.5f)
                           .SetEase(Ease.Linear)
                           .OnComplete(delegate
                           {
                               backToMenuBtn.gameObject.SetActive(true);
                               playAgainBtn.gameObject.SetActive(true);
                               AnimateRewardValue();
                           });
                    });
                break;
            case 3:
                GameController.Instance.musicManager.PlaySoundEffect(7);

                matchDrawTextHolder.transform.localScale = Vector3.zero;
                matchDrawTextHolder.SetActive(true);
                drawTextAnimation = matchDrawTextHolder.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f)
                    .SetEase(Ease.Linear)
                    .OnComplete(delegate
                    {
                        drawTextAnimation = matchDrawTextHolder.transform.DOScale(Vector3.one, 0.5f)
                           .SetEase(Ease.Linear)
                           .OnComplete(delegate
                           {
                               backToMenuBtn.gameObject.SetActive(true);
                               playAgainBtn.gameObject.SetActive(true);
                               AnimateRewardValue();
                           });
                    });
                break;
        }
    }

    void AnimateRewardValue()
    {
        rewardHolder.transform.localScale = Vector3.zero;
        rewardHolder.SetActive(true);
        rewardHolderAnimation = rewardHolder.transform.DOScale(Vector3.one * 1.5f, 0.25f)
            .SetEase(Ease.Linear)
            .OnComplete(delegate
            {
                rewardHolderAnimation = rewardHolder.transform.DOScale(Vector3.one, 0.25f)
                    .SetEase(Ease.Linear)
                    .OnComplete(delegate
                    {
                        rewardValueAnimation = DOTween.To
                            (delegate
                            {
                                return rewardValue;
                            },
                            delegate (int x)
                            {
                                rewardValue = x;
                            },
                            GamePlayController.Instance.gameLevelController.currentLevel.prizeGem,
                            0.5f
                            )
                            .OnUpdate(delegate
                            {
                                rewardText.text = rewardValue.ToString();
                            })
                            .OnComplete(delegate
                            {
                                rewardValueAnimation = null;                              
                            });
                    });
            });
    }

    void BackToMenu()
    {
        GameController.Instance.StartSceneTransition("HomeScene");
    }

    void PlayAgain()
    {
        GameController.Instance.StartSceneTransition("GamePlay");
    }

    IEnumerator Draw()
    {
        WaitForSeconds oneSec = new WaitForSeconds(1);

        while (player_1ScoreAnimation.IsPlaying() || player_2ScoreAnimation.IsPlaying())
        {
            yield return oneSec;
        }

        AnimateWinText(3);
    }
    #endregion
}