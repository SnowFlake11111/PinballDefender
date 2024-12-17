using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampaignEndGame : BaseBox
{
    #region Instance
    private static CampaignEndGame instance;
    public static CampaignEndGame Setup(int currentStageId, bool winOrLose, bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<CampaignEndGame>(PathPrefabs.CAMPAIGN_ENDGAME));
            instance.Init();
        }

        instance.InitState(currentStageId, winOrLose);
        return instance;
    }
    #endregion

    #region Public Variables
    [Header("Win and Lose text")]
    public GameObject winTextHolder;
    public GameObject loseTextHolder;

    [Header("Reward")]
    public GameObject rewardHolder;
    public Text rewardText;

    [Header("Tips for losing")]
    public List<GameObject> tips = new List<GameObject>();

    [Header("Buttons")]
    public Button backToMenuBtn;
    public Button nextStageBtn;
    #endregion

    #region Private Variables
    int previousTipId = -1;
    int currentStageId = 0;
    int rewardValue = 0;

    Tweener rewardHolderAnimation;
    Tweener rewardValueAnimation;
    #endregion

    #region Start, Update
    public void Init()
    {
        backToMenuBtn.onClick.AddListener(delegate { BackToMenu(); });
        nextStageBtn.onClick.AddListener(delegate { NextStage(); });
    }

    public void InitState(int currentStageId, bool winOrLose)
    {
        this.currentStageId = currentStageId;
        WinOrLose(winOrLose);
    }
    #endregion

    #region Functions
    //----------Public----------
    //----------Private----------
    void WinOrLose(bool playerWon)
    {
        if (playerWon)
        {
            winTextHolder.SetActive(true);
            loseTextHolder.SetActive(false);
            AnimateRewardValue();
        }
        else
        {
            winTextHolder.SetActive(false);
            loseTextHolder.SetActive(true);
            ShowTip();
        }
    }

    void AnimateRewardValue()
    {
        rewardHolder.transform.localScale = Vector3.zero;
        rewardHolder.SetActive(true);
        rewardHolderAnimation = rewardHolder.transform.DOScale(Vector3.one * 1.5f, 0.5f)
            .SetEase(Ease.Linear)
            .OnComplete(delegate
            {
                rewardHolderAnimation = rewardHolder.transform.DOScale(Vector3.one, 0.5f)
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

    void ShowTip()
    {
        if (previousTipId > 0)
        {
            tips[previousTipId].SetActive(false);
        }

        int temp = UnityEngine.Random.Range(0, tips.Count);
        tips[temp].SetActive(true);
        previousTipId = temp;
    }

    void BackToMenu()
    {
        GameController.Instance.StartSceneTransition("HomeScene");
    }

    void NextStage()
    {
        //Small explaination: Reason why the stage id is sent directly to the function rather than increase it by 1 even though the stage system is run on a List, is because the id is already like index + 1 by itself, thus if player want to proceed to the next stage just need to assign the current stageId into the field and the player will go to the next stage. This is of course will be wrong if the stageId on the stages are following the rule of using the same number of the stages position in the List
        GameController.Instance.gameModeData.StartCampaign(currentStageId);
        GameController.Instance.StartSceneTransition("GamePlay");
    }
    #endregion
}
