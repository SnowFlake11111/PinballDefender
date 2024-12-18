using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using MoreMountains.NiceVibrations;
using EventDispatcher;
using Sirenix.OdinInspector;
using DG.Tweening;
using System.Linq;

public class GameScene : BaseScene
{
    #region Public Variables
    [BoxGroup("SHARED DATAS", centerLabel: true)]
    [Header("Screens")]
    public Image warningScreen;

    [BoxGroup("SHARED DATAS")]
    [Header("UNIT REFERENCES")]
    public Dictionary<int, GameUnitBase> units = new Dictionary<int, GameUnitBase>();

    [BoxGroup("SHARED DATAS")]
    [Space]
    [Header("Unit image references")]
    public Dictionary<int, Sprite> unitImages = new Dictionary<int, Sprite>();

    //***   CAMPAIGN    ***
    [BoxGroup("CAMPAIGN", centerLabel: true)]
    [LabelWidth(250)]
    public GameObject campaignButtonsHolder;

    [BoxGroup("CAMPAIGN/Player Visual", centerLabel: true)]
    [LabelWidth(250)]
    public Image campaignPlayerGateHealth;

    [BoxGroup("CAMPAIGN/Player Visual")]
    [LabelWidth(250)]
    public Image campaignPlayerGateDelayHealth;

    [BoxGroup("CAMPAIGN/Player Visual")]
    [LabelWidth(250)]
    public Slider campaignPlayerProgress;

    [Space]
    [BoxGroup("CAMPAIGN/Player Visual")]
    [LabelWidth(250)]
    public Text campaignPlayerCurrentAmmo;

    [BoxGroup("CAMPAIGN/Player Visual")]
    [LabelWidth(250)]
    public Text campaignPlayerMaxAmmo;

    [Space]
    [BoxGroup("CAMPAIGN/Player Visual")]
    [LabelWidth(250)]
    public Image campaignPlayerReloadEffect;

    [BoxGroup("CAMPAIGN/Player Visual")]
    [LabelWidth(250)]
    public Image campaignPlayerCreditsGainEffect;

    [Space]
    [BoxGroup("CAMPAIGN/Player Visual")]
    [LabelWidth(250)]
    public Text campaignPlayerSpawnCredits;

    [BoxGroup("CAMPAIGN/Player Visual")]
    [LabelWidth(250)]
    public Text campaignPlayerMaxSpawnCredits;

    [BoxGroup("CAMPAIGN/Player Control", centerLabel: true)]
    [LabelWidth(250)]
    public Button campaignPlayerShootBtn;

    [BoxGroup("CAMPAIGN/Player Control")]
    [LabelWidth(250)]
    public Button campaignSetting;

    [BoxGroup("CAMPAIGN/Player Spawn", centerLabel: true)]
    public List<Button> campaignUnitSpawnBtn = new List<Button>();

    [BoxGroup("CAMPAIGN/Player Spawn")]
    public List<Image> campaignUnitSpawnImages = new List<Image>();

    [BoxGroup("CAMPAIGN/Player Spawn")]
    public List<Text> campaignUnitSpawnCost = new List<Text>();

    [BoxGroup("CAMPAIGN/Player Spawn")]
    public List<Button> campaignUnitSpawnLaneBtn = new List<Button>();

    //***   SCORE BATTLE    ***
    [BoxGroup("SCORE BATTLE", centerLabel: true)]
    [LabelWidth(250)]
    public GameObject scoreButtonsHolder;

    [BoxGroup("SCORE BATTLE/Player Visual", centerLabel: true)]
    [LabelWidth(250)]
    public Slider scoreComparisonBar;

    [BoxGroup("SCORE BATTLE/Player Visual")]
    [LabelWidth(250)]
    public Slider scoreProgress;

    [Space]
    [BoxGroup("SCORE BATTLE/Player Visual")]
    [LabelWidth(250)]
    public Text scorePlayer_1Score;

    [BoxGroup("SCORE BATTLE/Player Visual")]
    [LabelWidth(250)]
    public Text scorePlayer_2Score;

    [Space]
    [BoxGroup("SCORE BATTLE/Player Visual")]
    [LabelWidth(250)]
    public Text scorePlayer_1CurrentAmmo;

    [BoxGroup("SCORE BATTLE/Player Visual")]
    [LabelWidth(250)]
    public Text scorePlayer_1MaxAmmo;


    [Space]
    [BoxGroup("SCORE BATTLE/Player Visual")]
    [LabelWidth(250)]
    public Text scorePlayer_2CurrentAmmo;

    [BoxGroup("SCORE BATTLE/Player Visual")]
    [LabelWidth(250)]
    public Text scorePlayer_2MaxAmmo;

    [Space]
    [BoxGroup("SCORE BATTLE/Player Visual")]
    [LabelWidth(250)]
    public Image scorePlayer_1ReloadEffect;

    [BoxGroup("SCORE BATTLE/Player Visual")]
    [LabelWidth(250)]
    public Image scorePlayer_2ReloadEffect;

    [BoxGroup("SCORE BATTLE/Player Control", centerLabel: true)]
    [LabelWidth(250)]
    public Button scorePlayer_1ShootBtn;

    [BoxGroup("SCORE BATTLE/Player Control")]
    [LabelWidth(250)]
    public Button scorePlayer_2ShootBtn;

    [BoxGroup("SCORE BATTLE/Player Control")]
    [LabelWidth(250)]
    [Space]
    public Button scoreSetting;

    //***   DEFENDER BATTLE ***
    [BoxGroup("DEFENDER BATTLE", centerLabel: true)]
    [LabelWidth(250)]
    public GameObject defenderButtonsHolder;

    [BoxGroup("DEFENDER BATTLE/Player Visual", centerLabel: true)]
    [LabelWidth(250)]
    public Image defenderPlayer_1GateHealth;

    [BoxGroup("DEFENDER BATTLE/Player Visual")]
    [LabelWidth(250)]
    public Image defenderPlayer_1GateDelayHealth;

    [BoxGroup("DEFENDER BATTLE/Player Visual")]
    [LabelWidth(250)]
    public Image defenderPlayer_2GateHealth;

    [BoxGroup("DEFENDER BATTLE/Player Visual")]
    [LabelWidth(250)]
    public Image defenderPlayer_2GateDelayHealth;

    [Space]
    [BoxGroup("DEFENDER BATTLE/Player Visual")]
    [LabelWidth(250)]
    public Text defenderPlayer_1CurrentAmmo;

    [BoxGroup("DEFENDER BATTLE/Player Visual")]
    [LabelWidth(250)]
    public Text defenderPlayer_2CurrentAmmo;

    [Space]
    [BoxGroup("DEFENDER BATTLE/Player Visual")]
    [LabelWidth(250)]
    public Text defenderPlayer_1MaxAmmo;

    [BoxGroup("DEFENDER BATTLE/Player Visual")]
    [LabelWidth(250)]
    public Text defenderPlayer_2MaxAmmo;

    [Space]
    [BoxGroup("DEFENDER BATTLE/Player Visual")]
    [LabelWidth(250)]
    public Text defenderPlayer_1SpawnCredits;

    [BoxGroup("DEFENDER BATTLE/Player Visual")]
    [LabelWidth(250)]
    public Text defenderPlayer_1MaxSpawnCredits;

    [Space]
    [BoxGroup("DEFENDER BATTLE/Player Visual")]
    [LabelWidth(250)]
    public Text defenderPlayer_2SpawnCredits;

    [BoxGroup("DEFENDER BATTLE/Player Visual")]
    [LabelWidth(250)]
    public Text defenderPlayer_2MaxSpawnCredits;

    [Space]
    [BoxGroup("DEFENDER BATTLE/Player Visual")]
    [LabelWidth(250)]
    public Image defenderPlayer_1ReloadEffect;

    [BoxGroup("DEFENDER BATTLE/Player Visual")]
    [LabelWidth(250)]
    public Image defenderPlayer_2ReloadEffect;

    [Space]
    [BoxGroup("DEFENDER BATTLE/Player Visual")]
    [LabelWidth(250)]
    public Image defenderPlayer_1CreditsGainEffect;

    [BoxGroup("DEFENDER BATTLE/Player Visual")]
    [LabelWidth(250)]
    public Image defenderPlayer_2CreditsGainEffect;

    [BoxGroup("DEFENDER BATTLE/Player Control", centerLabel: true)]
    [LabelWidth(250)]
    public Button defenderPlayer_1ShootBtn;

    [BoxGroup("DEFENDER BATTLE/Player Control")]
    [LabelWidth(250)]
    public Button defenderPlayer_2ShootBtn;

    [BoxGroup("DEFENDER BATTLE/Player Control")]
    [LabelWidth(250)]
    public Button defenderSetting;

    [BoxGroup("DEFENDER BATTLE/Player Spawn", centerLabel: true)]
    public List<Button> defenderPlayer_1UnitSpawnBtn = new List<Button>();

    [BoxGroup("DEFENDER BATTLE/Player Spawn")]
    public List<Image> defenderPlayer_1UnitSpawnBtnImage = new List<Image>();

    [BoxGroup("DEFENDER BATTLE/Player Spawn")]
    public List<Text> defenderPlayer_1UnitSpawnBtnCost = new List<Text>();

    [BoxGroup("DEFENDER BATTLE/Player Spawn")]
    public List<Button> defenderPlayer_1UnitSpawnLaneBtn = new List<Button>();

    [Space]
    [BoxGroup("DEFENDER BATTLE/Player Spawn", centerLabel: true)]
    public List<Button> defenderPlayer_2UnitSpawnBtn = new List<Button>();

    [BoxGroup("DEFENDER BATTLE/Player Spawn")]
    public List<Image> defenderPlayer_2UnitSpawnBtnImage = new List<Image>();

    [BoxGroup("DEFENDER BATTLE/Player Spawn")]
    public List<Text> defenderPlayer_2UnitSpawnBtnCost = new List<Text>();

    [BoxGroup("DEFENDER BATTLE/Player Spawn")]
    public List<Button> defenderPlayer_2UnitSpawnLaneBtn = new List<Button>();

    [Space]
    [Header("-----MISC-----")]
    public Button settingBtn;
    public Animator warningScreenIconAnimator;

    #endregion

    #region Private Variables
    [Header("REMOVE WHEN THE GAME SYSTEM IS COMPLETED")]
    [SerializeField] List<GameUnitBase> player_1chosenUnits = new List<GameUnitBase>();
    [SerializeField] List<GameUnitBase> player_2chosenUnits = new List<GameUnitBase>();

    GameObject unitHolder;

    int defenderClockTime = 0;
    int defenderTimeBonus = 0;
    int player_1prevCredits = 0;
    int player_2prevCredits = 0;
    int player_1prevScore = 0;
    int player_2prevScore = 0;

    float player_1CurrentScore
    {
        get
        {
            return GamePlayController.Instance.gameLevelController.currentLevel.player_1.GetScore();
        }
    }
    float player_2CurrentScore
    {
        get
        {
            return GamePlayController.Instance.gameLevelController.currentLevel.player_2.GetScore();
        }
    }
    float scoreComparisonPercent
    {
        get
        {
            return player_1CurrentScore / (player_1CurrentScore + player_2CurrentScore);
        }
    }

    bool campaignMode = false;
    bool scoreBattleMode = false;
    bool defenderBattleMode = false;
    bool clockPause = false;

    List<int> player_1UnitsCostList = new List<int>();
    List<int> player_2UnitsCostList = new List<int>();

    List<FieldLane> lanes = new List<FieldLane>();

    GameUnitBase player_1ChosenUnitToSpawn;
    GameUnitBase player_2ChosenUnitToSpawn;

    Tweener levelProgress;
    Tweener player_1GateHpChange;
    Tweener player_1GateDelayHpChange;
    Tweener player_2GateHpChange;
    Tweener player_2GateDelayHpChange;
    Tweener player_1GainCreditsAnimation;
    Tweener player_2GainCreditsAnimation;
    Tweener player_1GainScoreAnimation;
    Tweener player_2GainScoreAnimation;
    Tweener scoreComparisonAnimation;
    Tween player_1Reload;
    Tween player_2Reload;
    Tween player_1GainCredits;
    Tween player_2GainCredits;
    Tween player_1LowAmmoWarning;
    Tween player_2LowAmmoWarning;

    Coroutine defenderModeClock;
    #endregion

    #region Functions
    //----------Public----------
    public void Init(int modeId)
    {
        //ShowBanner();
        //settingBtn.onClick.AddListener(delegate { OpenSetting(); });

        //GameController.Instance.musicManager.MusicTransition();
        
        switch (modeId)
        {
            case 1:
                InitiateCampaignMode();
                break;
            case 2:
                InitiateScoreBattleMode();
                break;
            case 3:
                InitiateDefenderBattleMode();
                break;
        }

        lanes = GamePlayController.Instance.gameLevelController.currentLevel.GetMapLanes();
        unitHolder = GamePlayController.Instance.gameLevelController.currentLevel.unitHolder;
    }

    public void ActivateIconFlashingAnimation()
    {
        warningScreenIconAnimator.SetBool("Flashing", true);
    }

    public void DeactivateIconFlashingAnimation()
    {
        warningScreenIconAnimator.SetBool("Flashing", false);
    }

    public override void OnEscapeWhenStackBoxEmpty()
    {
        throw new NotImplementedException();
    }

    public void StopPlayer_1Entirely()
    {
        if (campaignMode || defenderBattleMode)
        {
            if (player_1Reload != null)
            {
                player_1Reload.Kill();
            }

            if (player_1GainCreditsAnimation != null)
            {
                player_1GainCreditsAnimation.Kill();
            }
        }
        else
        {
            if (player_1Reload != null)
            {
                player_1Reload.Kill();
            }
        }

        GamePlayController.Instance.gameLevelController.currentLevel.player_1.PermanentlyStopRotation();
    }

    public void StopPlayer_2Entirely()
    {
        if (defenderBattleMode)
        {
            if (player_2Reload != null)
            {
                player_2Reload.Kill();
            }

            if (player_2GainCreditsAnimation != null)
            {
                player_2GainCreditsAnimation.Kill();
            }
        }
        else
        {
            if (player_2Reload != null)
            {
                player_2Reload.Kill();
            }
        }

        GamePlayController.Instance.gameLevelController.currentLevel.player_2.PermanentlyStopRotation();
    }

    //*** VISUAL UPDATE***
    public void UpdateSpawnProgress(float progress)
    {
        if (levelProgress == null)
        {
            if (campaignMode)
            {
                levelProgress = campaignPlayerProgress.DOValue(progress, 0.75f)
                    .SetEase(Ease.Linear)
                    .OnComplete(delegate
                    {
                        levelProgress = null;
                    });
            }
            else
            {
                levelProgress = scoreProgress.DOValue(progress, 0.75f)
                    .SetEase(Ease.Linear)
                    .OnComplete(delegate
                    {
                        levelProgress = null;
                    });
            }
        }
        else
        {
            levelProgress.ChangeEndValue(progress, true);
        }
    }

    public void UpdateGateHp(float hpPercent, int playerId)
    {
        switch (playerId)
        {
            case 1:
                if (player_1GateDelayHpChange != null)
                {
                    player_1GateDelayHpChange.Kill();
                }

                if (player_1GateHpChange == null)
                {
                    if (campaignMode)
                    {
                        player_1GateHpChange = campaignPlayerGateHealth.DOFillAmount(hpPercent, 0.5f)
                            .SetEase(Ease.Linear)
                            .OnComplete(delegate
                            {
                                player_1GateHpChange = null;

                                player_1GateDelayHpChange = campaignPlayerGateDelayHealth.DOFillAmount(hpPercent, 0.5f)
                                    .SetDelay(1)
                                    .SetEase(Ease.Linear)
                                    .OnComplete(delegate
                                    {
                                        player_1GateDelayHpChange = null;
                                    });
                            });
                    }
                    else
                    {
                        player_1GateHpChange = defenderPlayer_1GateHealth.DOFillAmount(hpPercent, 0.5f)
                            .SetEase(Ease.Linear)
                            .OnComplete(delegate
                            {
                                player_1GateHpChange = null;

                                player_1GateDelayHpChange = defenderPlayer_1GateDelayHealth.DOFillAmount(hpPercent, 0.5f)
                                    .SetDelay(1)
                                    .SetEase(Ease.Linear)
                                    .OnComplete(delegate
                                    {
                                        player_1GateDelayHpChange = null;
                                    });
                            });
                    }
                }
                else
                {
                    player_1GateHpChange.ChangeEndValue(hpPercent, true);
                }
                break;
            case 2:
                if (player_2GateDelayHpChange != null)
                {
                    player_2GateDelayHpChange.Kill();
                }

                if (player_2GateHpChange == null)
                {
                    player_2GateHpChange = defenderPlayer_2GateHealth.DOFillAmount(hpPercent, 0.5f)
                        .SetEase(Ease.Linear)
                        .OnComplete(delegate
                        {
                            player_2GateHpChange = null;

                            player_2GateDelayHpChange = defenderPlayer_2GateDelayHealth.DOFillAmount(hpPercent, 0.5f)
                                .SetDelay(1)
                                .SetEase(Ease.Linear)
                                .OnComplete(delegate
                                {
                                    player_2GateDelayHpChange = null;
                                });
                        });
                }
                else
                {
                    player_2GateHpChange.ChangeEndValue(hpPercent, true);
                }
                break;
        }
    }

    public void Player_1UpdateAmmo()
    {
        if (campaignMode)
        {
            campaignPlayerCurrentAmmo.text = GamePlayController.Instance.gameLevelController.currentLevel.player_1.GetCurrentAmmo().ToString();
        }
        else if (scoreBattleMode)
        {
            scorePlayer_1CurrentAmmo.text = GamePlayController.Instance.gameLevelController.currentLevel.player_1.GetCurrentAmmo().ToString();
        }
        else
        {
            defenderPlayer_1CurrentAmmo.text = GamePlayController.Instance.gameLevelController.currentLevel.player_1.GetCurrentAmmo().ToString();
        }
    }

    public void Player_2UpdateAmmo()
    {
        if (scoreBattleMode)
        {
            scorePlayer_2CurrentAmmo.text = GamePlayController.Instance.gameLevelController.currentLevel.player_2.GetCurrentAmmo().ToString();
        }
        else
        {
            defenderPlayer_2CurrentAmmo.text = GamePlayController.Instance.gameLevelController.currentLevel.player_2.GetCurrentAmmo().ToString();
        }
    }

    public void Player_1UpdateCurrentCredits(int newCurrentCredits)
    {
        Player_1UpdateSpawnBtnPriceCheck();

        if (player_1GainCreditsAnimation != null)
        {
            player_1GainCreditsAnimation.ChangeEndValue(newCurrentCredits, true);
        }
        else
        {
            if (campaignMode)
            {
                player_1GainCreditsAnimation = DOTween.To
                    (
                        delegate
                        {
                            return player_1prevCredits;
                        },
                        delegate (int x)
                        {
                            player_1prevCredits = x;
                        },
                        newCurrentCredits,
                        0.5f
                    )
                    .OnUpdate(delegate
                    {
                        campaignPlayerSpawnCredits.text = player_1prevCredits.ToString();
                    })
                    .OnComplete(delegate
                    {
                        player_1prevCredits = newCurrentCredits;
                        player_1GainCreditsAnimation = null;
                    });
            }
            else
            {
                player_1GainCreditsAnimation = DOTween.To
                    (
                        delegate
                        {
                            return player_1prevCredits;
                        },
                        delegate (int x)
                        {
                            player_1prevCredits = x;
                        },
                        newCurrentCredits,
                        0.5f
                    )
                    .OnUpdate(delegate
                    {
                        defenderPlayer_1SpawnCredits.text = player_1prevCredits.ToString();
                    })
                    .OnComplete(delegate
                    {
                        player_1prevCredits = newCurrentCredits;
                        player_1GainCreditsAnimation = null;
                    });
            }
        }
    }

    public void Player_2UpdateCurrentCredits(int newCurrentCredits)
    {
        Player_2UpdateSpawnBtnPriceCheck();

        if (player_2GainCreditsAnimation != null)
        {
            player_2GainCreditsAnimation.ChangeEndValue(newCurrentCredits, true);
        }
        else
        {
            player_2GainCreditsAnimation = DOTween.To
                    (
                        delegate
                        {
                            return player_2prevCredits;
                        },
                        delegate (int x)
                        {
                            player_2prevCredits = x;
                        },
                        newCurrentCredits,
                        0.5f
                    )
                    .OnUpdate(delegate
                    {
                        defenderPlayer_2SpawnCredits.text = player_2prevCredits.ToString();
                    })
                    .OnComplete(delegate
                    {
                        player_2prevCredits = newCurrentCredits;
                        player_2GainCreditsAnimation = null;
                    });
        }
    }

    public void Player_1UpdatePoint(int currentPoint)
    {
        if (player_1GainScoreAnimation != null)
        {
            player_1GainScoreAnimation.ChangeEndValue(currentPoint, true);
        }
        else
        {
            player_1GainScoreAnimation = DOTween.To
                    (
                        delegate
                        {
                            return player_1prevScore;
                        },
                        delegate (int x)
                        {
                            player_1prevScore = x;
                        },
                        currentPoint,
                        0.5f
                    )
                    .OnUpdate(delegate
                    {
                        scorePlayer_1Score.text = player_1prevScore.ToString();
                    })
                    .OnComplete(delegate
                    {
                        player_1prevScore = currentPoint;
                        player_1GainScoreAnimation = null;
                    });
        }

        ScoreComparisonBarAnimation();
    }

    public void Player_2UpdatePoint(int currentPoint)
    {
        if (player_2GainScoreAnimation != null)
        {
            player_2GainScoreAnimation.ChangeEndValue(currentPoint, true);
        }
        else
        {
            player_2GainScoreAnimation = DOTween.To
                    (
                        delegate
                        {
                            return player_2prevScore;
                        },
                        delegate (int x)
                        {
                            player_2prevScore = x;
                        },
                        currentPoint,
                        0.5f
                    )
                    .OnUpdate(delegate
                    {
                        scorePlayer_2Score.text = player_2prevScore.ToString();
                    })
                    .OnComplete(delegate
                    {
                        player_2prevScore = currentPoint;
                        player_2GainScoreAnimation = null;
                    });
        }

        ScoreComparisonBarAnimation();
    }

    public void Player_1UpdateSpawnBtnPriceCheck()
    {
        if (campaignMode)
        {
            for (int i = 0; i < player_1UnitsCostList.Count; i++)
            {
                if (player_1UnitsCostList[i] <= GamePlayController.Instance.gameLevelController.currentLevel.player_1.GetCurrentCredits())
                {
                    if (campaignUnitSpawnBtn[i].gameObject.activeSelf)
                    {
                        campaignUnitSpawnBtn[i].interactable = true;
                    }
                }
                else
                {
                    if (campaignUnitSpawnBtn[i].gameObject.activeSelf)
                    {
                        campaignUnitSpawnBtn[i].interactable = false;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < player_1UnitsCostList.Count; i++)
            {
                if (player_1UnitsCostList[i] <= GamePlayController.Instance.gameLevelController.currentLevel.player_1.GetCurrentCredits())
                {
                    if (defenderPlayer_1UnitSpawnBtn[i].gameObject.activeSelf)
                    {
                        defenderPlayer_1UnitSpawnBtn[i].interactable = true;
                    }
                }
                else
                {
                    if (defenderPlayer_1UnitSpawnBtn[i].gameObject.activeSelf)
                    {
                        defenderPlayer_1UnitSpawnBtn[i].interactable = false;
                    }
                }
            }
        }
    }

    public void Player_2UpdateSpawnBtnPriceCheck()
    {
        for (int i = 0; i < player_2UnitsCostList.Count; i++)
        {
            if (player_2UnitsCostList[i] <= GamePlayController.Instance.gameLevelController.currentLevel.player_2.GetCurrentCredits())
            {
                if (defenderPlayer_2UnitSpawnBtn[i].gameObject.activeSelf)
                {
                    defenderPlayer_2UnitSpawnBtn[i].interactable = true;
                }
            }
            else
            {
                if (defenderPlayer_2UnitSpawnBtn[i].gameObject.activeSelf)
                {
                    defenderPlayer_2UnitSpawnBtn[i].interactable = false;
                }
            }
        }
    }

    public void Player_1StartReload()
    {
        if (player_1Reload == null)
        {
            if (campaignMode)
            {
                player_1Reload = campaignPlayerReloadEffect.DOFillAmount(1, 5f - 0.5f * UseProfile.CampaignReloadSpeedUpgradeCount)
                    .SetEase(Ease.Linear)
                    .OnComplete(delegate
                    {
                        campaignPlayerReloadEffect.fillAmount = 0;
                        GamePlayController.Instance.gameLevelController.currentLevel.player_1.AmmoReloaded();
                        player_1Reload = null;
                        if (!GamePlayController.Instance.gameLevelController.currentLevel.player_1.IsAmmoFull())
                        {
                            Player_1StartReload();
                        }
                    });
            }
            else if (scoreBattleMode)
            {
                player_1Reload = scorePlayer_1ReloadEffect.DOFillAmount(1, 5f - 0.5f * GameController.Instance.gameModeData.GetPlayerReloadSpeedUpgrade(1))
                    .SetEase(Ease.Linear)
                    .OnComplete(delegate
                    {
                        scorePlayer_1ReloadEffect.fillAmount = 0;
                        GamePlayController.Instance.gameLevelController.currentLevel.player_1.AmmoReloaded();
                        player_1Reload = null;
                        if (!GamePlayController.Instance.gameLevelController.currentLevel.player_1.IsAmmoFull())
                        {
                            Player_1StartReload();
                        }
                    });
            }
            else
            {
                player_1Reload = defenderPlayer_1ReloadEffect.DOFillAmount(1, 5f - 0.5f * GameController.Instance.gameModeData.GetPlayerReloadSpeedUpgrade(1))
                    .SetEase(Ease.Linear)
                    .OnComplete(delegate
                    {
                        defenderPlayer_1ReloadEffect.fillAmount = 0;
                        GamePlayController.Instance.gameLevelController.currentLevel.player_1.AmmoReloaded();
                        player_1Reload = null;
                        if (!GamePlayController.Instance.gameLevelController.currentLevel.player_1.IsAmmoFull())
                        {
                            Player_1StartReload();
                        }
                    });
            }
        }
    }

    public void Player_2StartReload()
    {
        if (player_2Reload == null)
        {
            if (scoreBattleMode)
            {
                player_2Reload = scorePlayer_2ReloadEffect.DOFillAmount(1, 5f - 0.5f * GameController.Instance.gameModeData.GetPlayerReloadSpeedUpgrade(2))
                    .SetEase(Ease.Linear)
                    .OnComplete(delegate
                    {
                        scorePlayer_2ReloadEffect.fillAmount = 0;
                        GamePlayController.Instance.gameLevelController.currentLevel.player_2.AmmoReloaded();
                        player_2Reload = null;
                        if (!GamePlayController.Instance.gameLevelController.currentLevel.player_2.IsAmmoFull())
                        {
                            Player_2StartReload();
                        }
                    });
            }
            else
            {
                player_2Reload = defenderPlayer_2ReloadEffect.DOFillAmount(1, 5f - 0.5f * GameController.Instance.gameModeData.GetPlayerReloadSpeedUpgrade(2))
                    .SetEase(Ease.Linear)
                    .OnComplete(delegate
                    {
                        defenderPlayer_2ReloadEffect.fillAmount = 0;
                        GamePlayController.Instance.gameLevelController.currentLevel.player_2.AmmoReloaded();
                        player_2Reload = null;
                        if (!GamePlayController.Instance.gameLevelController.currentLevel.player_2.IsAmmoFull())
                        {
                            Player_2StartReload();
                        }
                    });
            }
        }
    }

    public void Player_1GainingCredits()
    {
        if (player_1GainCredits == null)
        {
            if (campaignMode)
            {
                player_1GainCredits = campaignPlayerCreditsGainEffect.DOFillAmount(1, 4f)
                    .SetEase(Ease.Linear)
                    .OnComplete(delegate
                    {
                        campaignPlayerCreditsGainEffect.fillAmount = 0;
                        GamePlayController.Instance.gameLevelController.currentLevel.player_1.CreditsGainedOverTime(1);
                        player_1GainCredits = null;
                        if (!GamePlayController.Instance.gameLevelController.currentLevel.player_1.IsCreditsMaxed())
                        {
                            Player_1GainingCredits();
                        }
                    });
            }
            else
            {
                player_1GainCredits = defenderPlayer_1CreditsGainEffect.DOFillAmount(1, 4f)
                    .SetEase(Ease.Linear)
                    .OnComplete(delegate
                    {
                        defenderPlayer_1CreditsGainEffect.fillAmount = 0;
                        GamePlayController.Instance.gameLevelController.currentLevel.player_1.CreditsGainedOverTime(2, defenderTimeBonus);
                        player_1GainCredits = null;
                        if (!GamePlayController.Instance.gameLevelController.currentLevel.player_1.IsCreditsMaxed())
                        {
                            Player_1GainingCredits();
                        }
                    });
            }
        }
    }

    public void Player_2GainingCredits()
    {
        if (player_2GainCredits == null)
        {
            player_2GainCredits = defenderPlayer_2CreditsGainEffect.DOFillAmount(1, 4f)
                    .SetEase(Ease.Linear)
                    .OnComplete(delegate
                    {
                        defenderPlayer_2CreditsGainEffect.fillAmount = 0;
                        GamePlayController.Instance.gameLevelController.currentLevel.player_2.CreditsGainedOverTime(2, defenderTimeBonus);
                        player_2GainCredits = null;
                        if (!GamePlayController.Instance.gameLevelController.currentLevel.player_2.IsCreditsMaxed())
                        {
                            Player_2GainingCredits();
                        }
                    });
        }
    }

    public void StartDefenderModeClock()
    {
        defenderClockTime = 0;
        defenderTimeBonus = 0;
        defenderModeClock = StartCoroutine(StartDefenderClockForCreditsBonus());
    }

    public void StopDefenderModeClock()
    {
        StopCoroutine(defenderModeClock);
    }

    public int GetDefenderClockTime()
    {
        return defenderClockTime;
    }

    //*** BUTTONS ACTION ***
    public void Player_1PickedUnitToSpawn(GameUnitBase pickedUnit)
    {
        GameController.Instance.musicManager.PlayClickSound();
        if (player_1ChosenUnitToSpawn != null)
        {
            if (player_1ChosenUnitToSpawn == pickedUnit)
            {
                if (campaignMode)
                {
                    foreach (Button lanePickingBtn in campaignUnitSpawnLaneBtn)
                    {
                        if (lanePickingBtn.gameObject.activeSelf)
                        {
                            lanePickingBtn.gameObject.SetActive(false);
                        }
                    }
                }
                else
                {
                    foreach (Button lanePickingBtn in defenderPlayer_1UnitSpawnLaneBtn)
                    {
                        if (lanePickingBtn.gameObject.activeSelf)
                        {
                            lanePickingBtn.gameObject.SetActive(false);
                        }
                    }
                }

                player_1ChosenUnitToSpawn = null;
                return;
            }
        }

        player_1ChosenUnitToSpawn = pickedUnit;

        if (pickedUnit.isBossOrMiniboss)
        {
            if (campaignMode)
            {
                foreach (Button lanePickingBtn in campaignUnitSpawnLaneBtn.Skip(1).Take(campaignUnitSpawnLaneBtn.Count - 2))
                {
                    if (!lanePickingBtn.gameObject.activeSelf)
                    {
                        lanePickingBtn.gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                foreach (Button lanePickingBtn in defenderPlayer_1UnitSpawnLaneBtn.Skip(1).Take(defenderPlayer_1UnitSpawnLaneBtn.Count - 2))
                {
                    if (!lanePickingBtn.gameObject.activeSelf)
                    {
                        lanePickingBtn.gameObject.SetActive(true);
                    }
                }
            }
        }
        else
        {
            if (campaignMode)
            {
                foreach (Button lanePickingBtn in campaignUnitSpawnLaneBtn)
                {
                    if (!lanePickingBtn.gameObject.activeSelf)
                    {
                        lanePickingBtn.gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                foreach (Button lanePickingBtn in defenderPlayer_1UnitSpawnLaneBtn)
                {
                    if (!lanePickingBtn.gameObject.activeSelf)
                    {
                        lanePickingBtn.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    public void Player_2PickedUnitToSpawn(GameUnitBase pickedUnit)
    {
        GameController.Instance.musicManager.PlayClickSound();
        if (player_2ChosenUnitToSpawn != null)
        {
            if (player_2ChosenUnitToSpawn == pickedUnit)
            {
                foreach (Button lanePickingBtn in defenderPlayer_2UnitSpawnLaneBtn)
                {
                    if (lanePickingBtn.gameObject.activeSelf)
                    {
                        lanePickingBtn.gameObject.SetActive(false);
                    }
                }

                player_2ChosenUnitToSpawn = null;
                return;
            }
        }

        player_2ChosenUnitToSpawn = pickedUnit;

        if (pickedUnit.isBossOrMiniboss)
        {
            foreach (Button lanePickingBtn in defenderPlayer_2UnitSpawnLaneBtn.Skip(1).Take(defenderPlayer_2UnitSpawnLaneBtn.Count - 2))
            {
                if (!lanePickingBtn.gameObject.activeSelf)
                {
                    lanePickingBtn.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            foreach (Button lanePickingBtn in defenderPlayer_2UnitSpawnLaneBtn)
            {
                if (!lanePickingBtn.gameObject.activeSelf)
                {
                    lanePickingBtn.gameObject.SetActive(true);
                }
            }
        }
    }

    public void Player_1SpawnUnitProcedure(int laneId)
    {
        if (player_1ChosenUnitToSpawn != null)
        {
            GamePlayController.Instance.gameLevelController.currentLevel.player_1.SpawnAnUnit(player_1ChosenUnitToSpawn, lanes[laneId].spawnPointA, lanes[laneId], unitHolder);

            if (campaignMode)
            {
                foreach (Button lanePickingBtn in campaignUnitSpawnLaneBtn)
                {
                    if (lanePickingBtn.gameObject.activeSelf)
                    {
                        lanePickingBtn.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                foreach (Button lanePickingBtn in defenderPlayer_1UnitSpawnLaneBtn)
                {
                    if (lanePickingBtn.gameObject.activeSelf)
                    {
                        lanePickingBtn.gameObject.SetActive(false);
                    }
                }
            }

            player_1ChosenUnitToSpawn = null;
        }
        else
        {
            Debug.LogError("Player 1 has't chosen any unit to spawn");
        }
    }

    public void Player_2SpawnUnitProcedure(int laneId)
    {
        if (player_2ChosenUnitToSpawn != null)
        {
            GamePlayController.Instance.gameLevelController.currentLevel.player_2.SpawnAnUnit(player_2ChosenUnitToSpawn, lanes[laneId].spawnPointB, lanes[laneId], unitHolder);

            foreach (Button lanePickingBtn in defenderPlayer_2UnitSpawnLaneBtn)
            {
                if (lanePickingBtn.gameObject.activeSelf)
                {
                    lanePickingBtn.gameObject.SetActive(false);
                }
            }

            player_2ChosenUnitToSpawn = null;
        }
        else
        {
            Debug.LogError("Player 2 has't chosen any unit to spawn");
        }
    }

    //----------Private----------

    void Player_1Shoot()
    {
        GamePlayController.Instance.gameLevelController.currentLevel.player_1.TemporaryStopRotation();

        if (GamePlayController.Instance.gameLevelController.currentLevel.player_1.GetCurrentAmmo() <= 0)
        {
            if (player_1LowAmmoWarning == null)
            {
                if (campaignMode)
                {
                    player_1LowAmmoWarning = campaignPlayerCurrentAmmo.DOColor(Color.red, 0.5f)
                        .SetLoops(4, LoopType.Yoyo)
                        .SetEase(Ease.Linear)
                        .OnComplete(delegate
                        {
                            player_1LowAmmoWarning = null;
                        });
                }
                else if (scoreBattleMode)
                {
                    player_1LowAmmoWarning = scorePlayer_1CurrentAmmo.DOColor(Color.red, 0.5f)
                        .SetLoops(4, LoopType.Yoyo)
                        .SetEase(Ease.Linear)
                        .OnComplete(delegate
                        {
                            player_1LowAmmoWarning = null;
                        });
                }
                else
                {
                    player_1LowAmmoWarning = defenderPlayer_1CurrentAmmo.DOColor(Color.red, 0.5f)
                        .SetLoops(4, LoopType.Yoyo)
                        .SetEase(Ease.Linear)
                        .OnComplete(delegate
                        {
                            player_1LowAmmoWarning = null;
                        });
                }
            }
            else
            {
                player_1LowAmmoWarning.Restart();
            }

            return;
        }
        else
        {
            GamePlayController.Instance.gameLevelController.currentLevel.player_1.Shoot();
        }

    }

    void Player_2Shoot()
    {
        GamePlayController.Instance.gameLevelController.currentLevel.player_2.TemporaryStopRotation();

        if (GamePlayController.Instance.gameLevelController.currentLevel.player_2.GetCurrentAmmo() <= 0)
        {
            if (player_2LowAmmoWarning == null)
            {
                if (scoreBattleMode)
                {
                    player_2LowAmmoWarning = scorePlayer_2CurrentAmmo.DOColor(Color.red, 0.5f)
                        .SetLoops(4, LoopType.Yoyo)
                        .SetEase(Ease.Linear)
                        .OnComplete(delegate
                        {
                            player_2LowAmmoWarning = null;
                        });
                }
                else
                {
                    player_2LowAmmoWarning = defenderPlayer_2CurrentAmmo.DOColor(Color.red, 0.5f)
                        .SetLoops(4, LoopType.Yoyo)
                        .SetEase(Ease.Linear)
                        .OnComplete(delegate
                        {
                            player_2LowAmmoWarning = null;
                        });
                }
            }
            else
            {
                player_2LowAmmoWarning.Restart();
            }

            return;
        }
        else
        {
            GamePlayController.Instance.gameLevelController.currentLevel.player_2.Shoot();
        }
    }

    void Player_1SetupSpawnList()
    {
        GetUnitList(1);
        player_1UnitsCostList.Clear();

        if (campaignMode)
        {
            int emptySlotCount = campaignUnitSpawnBtn.Count - player_1chosenUnits.Count;

            if (emptySlotCount > 0)
            {
                for (int i = player_1chosenUnits.Count; i < campaignUnitSpawnBtn.Count; i++)
                {
                    campaignUnitSpawnBtn[i].gameObject.SetActive(false);
                }

                for (int i = 0; i < player_1chosenUnits.Count; i++)
                {
                    int tempIndex = i;
                    campaignUnitSpawnImages[i].sprite = unitImages[player_1chosenUnits[i].unitId];
                    campaignUnitSpawnCost[i].text = player_1chosenUnits[i].spawnCost.ToString();
                    player_1UnitsCostList.Add(player_1chosenUnits[i].spawnCost);
                    campaignUnitSpawnBtn[i].onClick.AddListener(delegate { Player_1PickedUnitToSpawn(player_1chosenUnits[tempIndex]); });
                }
            }
            else
            {
                for (int i = 0; i < player_1chosenUnits.Count; i++)
                {
                    int tempIndex = i;
                    campaignUnitSpawnImages[i].sprite = unitImages[player_1chosenUnits[i].unitId];
                    campaignUnitSpawnCost[i].text = player_1chosenUnits[i].spawnCost.ToString();
                    player_1UnitsCostList.Add(player_1chosenUnits[i].spawnCost);
                    campaignUnitSpawnBtn[i].onClick.AddListener(delegate { Player_1PickedUnitToSpawn(player_1chosenUnits[tempIndex]); });
                }
            }
        }
        else
        {
            int emptySlotCount = defenderPlayer_1UnitSpawnBtn.Count - player_1chosenUnits.Count;

            if (emptySlotCount > 0)
            {
                for (int i = player_1chosenUnits.Count; i < defenderPlayer_1UnitSpawnBtn.Count; i++)
                {
                    defenderPlayer_1UnitSpawnBtn[i].gameObject.SetActive(false);
                }

                for (int i = 0; i < player_1chosenUnits.Count; i++)
                {
                    int tempIndex = i;
                    defenderPlayer_1UnitSpawnBtnImage[i].sprite = unitImages[player_1chosenUnits[i].unitId];
                    defenderPlayer_1UnitSpawnBtnCost[i].text = player_1chosenUnits[i].spawnCost.ToString();
                    player_1UnitsCostList.Add(player_1chosenUnits[i].spawnCost);
                    defenderPlayer_1UnitSpawnBtn[i].onClick.AddListener(delegate { Player_1PickedUnitToSpawn(player_1chosenUnits[tempIndex]); });
                }
            }
            else
            {
                for (int i = 0; i < player_1chosenUnits.Count; i++)
                {
                    int tempIndex = i;
                    defenderPlayer_1UnitSpawnBtnImage[i].sprite = unitImages[player_1chosenUnits[i].unitId];
                    defenderPlayer_1UnitSpawnBtnCost[i].text = player_1chosenUnits[i].spawnCost.ToString();
                    player_1UnitsCostList.Add(player_1chosenUnits[i].spawnCost);
                    defenderPlayer_1UnitSpawnBtn[i].onClick.AddListener(delegate { Player_1PickedUnitToSpawn(player_1chosenUnits[tempIndex]); });
                }
            }
        }
    }

    void Player_2SetupSpawnList()
    {
        GetUnitList(2);
        player_2UnitsCostList.Clear();

        int emptySlotCount = defenderPlayer_2UnitSpawnBtn.Count - player_2chosenUnits.Count;

        if (emptySlotCount > 0)
        {
            for (int i = player_2chosenUnits.Count; i < defenderPlayer_2UnitSpawnBtn.Count; i++)
            {
                defenderPlayer_2UnitSpawnBtn[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < player_2chosenUnits.Count; i++)
            {
                int tempIndex = i;
                defenderPlayer_2UnitSpawnBtnImage[i].sprite = unitImages[player_2chosenUnits[i].unitId];
                defenderPlayer_2UnitSpawnBtnCost[i].text = player_2chosenUnits[i].spawnCost.ToString();
                player_2UnitsCostList.Add(player_2chosenUnits[i].spawnCost);
                defenderPlayer_2UnitSpawnBtn[i].onClick.AddListener(delegate { Player_2PickedUnitToSpawn(player_2chosenUnits[tempIndex]); });
            }
        }
        else
        {
            for (int i = 0; i < player_2chosenUnits.Count; i++)
            {
                int tempIndex = i;
                defenderPlayer_2UnitSpawnBtnImage[i].sprite = unitImages[player_2chosenUnits[i].unitId];
                defenderPlayer_2UnitSpawnBtnCost[i].text = player_2chosenUnits[i].spawnCost.ToString();
                player_2UnitsCostList.Add(player_2chosenUnits[i].spawnCost);
                defenderPlayer_2UnitSpawnBtn[i].onClick.AddListener(delegate { Player_2PickedUnitToSpawn(player_2chosenUnits[tempIndex]); });
            }
        }       
    }

    void Player_1SetupSpawnLaneButtons()
    {
        if (campaignMode)
        {
            for(int i = 0; i < campaignUnitSpawnLaneBtn.Count; i++)
            {
                int tempIndex = i;
                campaignUnitSpawnLaneBtn[i].onClick.AddListener(delegate { Player_1SpawnUnitProcedure(tempIndex); });

                if (campaignUnitSpawnLaneBtn[i].gameObject.activeSelf)
                {
                    campaignUnitSpawnLaneBtn[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < defenderPlayer_1UnitSpawnLaneBtn.Count; i++)
            {
                int tempIndex = i;
                defenderPlayer_1UnitSpawnLaneBtn[i].onClick.AddListener(delegate { Player_1SpawnUnitProcedure(tempIndex); });

                if (defenderPlayer_1UnitSpawnLaneBtn[i].gameObject.activeSelf)
                {
                    defenderPlayer_1UnitSpawnLaneBtn[i].gameObject.SetActive(false);
                }
            }
        }
    }

    void Player_2SetupSpawnLaneButtons()
    {
        for (int i = 0; i < defenderPlayer_2UnitSpawnLaneBtn.Count; i++)
        {
            int tempIndex = i;
            defenderPlayer_2UnitSpawnLaneBtn[i].onClick.AddListener(delegate { Player_2SpawnUnitProcedure(defenderPlayer_2UnitSpawnLaneBtn.Count - 1 - tempIndex); });

            if (defenderPlayer_2UnitSpawnLaneBtn[i].gameObject.activeSelf)
            {
                defenderPlayer_2UnitSpawnLaneBtn[i].gameObject.SetActive(false);
            }
        }
    }

    void ScoreComparisonBarAnimation()
    {
        if (scoreComparisonAnimation != null)
        {
            scoreComparisonAnimation.ChangeEndValue(scoreComparisonPercent, true);
        }
        else
        {
            scoreComparisonAnimation = scoreComparisonBar.DOValue(scoreComparisonPercent, 0.5f)
                .SetEase(Ease.Linear)
                .OnComplete(delegate
                {
                    scoreComparisonAnimation = null;
                });
        }
        
    }

    void GetUnitList(int id)
    {
        switch (id)
        {
            case 1:
                player_1chosenUnits.Clear();
                GameUnitBase unitPlayer_1Found;

                if (campaignMode)
                {
                    if (units.TryGetValue(UseProfile.CampaignSlot1Unit, out unitPlayer_1Found))
                    {
                        player_1chosenUnits.Add(unitPlayer_1Found);
                    }

                    if (units.TryGetValue(UseProfile.CampaignSlot2Unit, out unitPlayer_1Found))
                    {
                        player_1chosenUnits.Add(unitPlayer_1Found);
                    }

                    if (units.TryGetValue(UseProfile.CampaignSlot3Unit, out unitPlayer_1Found))
                    {
                        player_1chosenUnits.Add(unitPlayer_1Found);
                    }

                    if (units.TryGetValue(UseProfile.CampaignSlot4Unit, out unitPlayer_1Found))
                    {
                        player_1chosenUnits.Add(unitPlayer_1Found);
                    }

                    if (units.TryGetValue(UseProfile.CampaignSlot5Unit, out unitPlayer_1Found))
                    {
                        player_1chosenUnits.Add(unitPlayer_1Found);
                    }
                }
                else
                {
                    if (units.TryGetValue(UseProfile.MultiplayerPlayer_1Slot1Unit, out unitPlayer_1Found))
                    {
                        player_1chosenUnits.Add(unitPlayer_1Found);
                    }

                    if (units.TryGetValue(UseProfile.MultiplayerPlayer_1Slot2Unit, out unitPlayer_1Found))
                    {
                        player_1chosenUnits.Add(unitPlayer_1Found);
                    }

                    if (units.TryGetValue(UseProfile.MultiplayerPlayer_1Slot3Unit, out unitPlayer_1Found))
                    {
                        player_1chosenUnits.Add(unitPlayer_1Found);
                    }

                    if (units.TryGetValue(UseProfile.MultiplayerPlayer_1Slot4Unit, out unitPlayer_1Found))
                    {
                        player_1chosenUnits.Add(unitPlayer_1Found);
                    }

                    if (units.TryGetValue(UseProfile.MultiplayerPlayer_1Slot5Unit, out unitPlayer_1Found))
                    {
                        player_1chosenUnits.Add(unitPlayer_1Found);
                    }
                }
                break;
            case 2:
                player_2chosenUnits.Clear();
                GameUnitBase unitPlayer_2Found;

                if (units.TryGetValue(UseProfile.MultiplayerPlayer_2Slot1Unit, out unitPlayer_2Found))
                {
                    player_2chosenUnits.Add(unitPlayer_2Found);
                }

                if (units.TryGetValue(UseProfile.MultiplayerPlayer_2Slot2Unit, out unitPlayer_2Found))
                {
                    player_2chosenUnits.Add(unitPlayer_2Found);
                }

                if (units.TryGetValue(UseProfile.MultiplayerPlayer_2Slot3Unit, out unitPlayer_2Found))
                {
                    player_2chosenUnits.Add(unitPlayer_2Found);
                }

                if (units.TryGetValue(UseProfile.MultiplayerPlayer_2Slot4Unit, out unitPlayer_2Found))
                {
                    player_2chosenUnits.Add(unitPlayer_2Found);
                }

                if (units.TryGetValue(UseProfile.MultiplayerPlayer_2Slot5Unit, out unitPlayer_2Found))
                {
                    player_2chosenUnits.Add(unitPlayer_2Found);
                }
                break;
        }
    }

    IEnumerator StartDefenderClockForCreditsBonus()
    {
        WaitForSeconds oneSecond = new WaitForSeconds(1);

        while (!clockPause)
        {
            if (defenderTimeBonus < 5)
            {
                defenderTimeBonus = defenderClockTime % 12;
            }

            defenderClockTime++;

            yield return oneSecond;
        }
    }

    //----------Mode Section----------
    //***   CAMPAIGN    ***
    void InitiateCampaignMode()
    {
        campaignMode = true;

        scoreButtonsHolder.SetActive(false);
        defenderButtonsHolder.SetActive(false);
        campaignButtonsHolder.SetActive(true);

        campaignPlayerCurrentAmmo.text = GamePlayController.Instance.gameLevelController.currentLevel.player_1.GetCurrentAmmo().ToString();
        campaignPlayerMaxAmmo.text = GamePlayController.Instance.gameLevelController.currentLevel.player_1.GetMaxAmmo().ToString();

        campaignPlayerSpawnCredits.text = GamePlayController.Instance.gameLevelController.currentLevel.player_1.GetCurrentCredits().ToString();
        campaignPlayerMaxSpawnCredits.text = GamePlayController.Instance.gameLevelController.currentLevel.player_1.GetMaxCredits().ToString();

        player_1prevCredits = GamePlayController.Instance.gameLevelController.currentLevel.player_1.GetCurrentCredits();

        campaignSetting.onClick.AddListener(delegate { OpenSetting(); });

        Player_1SetupSpawnLaneButtons();
        Player_1SetupSpawnList();
        Player_1UpdateSpawnBtnPriceCheck();

        campaignPlayerShootBtn.onClick.AddListener(delegate { Player_1Shoot(); });
    }

    //***   SCORE BATTLE    ***
    void InitiateScoreBattleMode()
    {
        scoreBattleMode = true;

        defenderButtonsHolder.SetActive(false);
        campaignButtonsHolder.SetActive(false);
        scoreButtonsHolder.SetActive(true);

        scorePlayer_1CurrentAmmo.text = GamePlayController.Instance.gameLevelController.currentLevel.player_1.GetCurrentAmmo().ToString();
        scorePlayer_2CurrentAmmo.text = GamePlayController.Instance.gameLevelController.currentLevel.player_2.GetCurrentAmmo().ToString();

        scorePlayer_1MaxAmmo.text = GamePlayController.Instance.gameLevelController.currentLevel.player_1.GetMaxAmmo().ToString();
        scorePlayer_2MaxAmmo.text = GamePlayController.Instance.gameLevelController.currentLevel.player_2.GetMaxAmmo().ToString();

        scorePlayer_1Score.text = GamePlayController.Instance.gameLevelController.currentLevel.player_1.GetScore().ToString();
        scorePlayer_2Score.text = GamePlayController.Instance.gameLevelController.currentLevel.player_2.GetScore().ToString();

        player_1prevScore = GamePlayController.Instance.gameLevelController.currentLevel.player_1.GetScore();
        player_2prevScore = GamePlayController.Instance.gameLevelController.currentLevel.player_2.GetScore();

        scoreSetting.onClick.AddListener(delegate { OpenSetting(); });

        scorePlayer_1ShootBtn.onClick.AddListener(delegate { Player_1Shoot(); });
        scorePlayer_2ShootBtn.onClick.AddListener (delegate { Player_2Shoot(); });
    }

    //***   DEFENDER BATTLE ***
    void InitiateDefenderBattleMode()
    {
        defenderBattleMode = true;

        scoreButtonsHolder.SetActive(false);
        campaignButtonsHolder.SetActive(false);
        defenderButtonsHolder.SetActive(true);

        defenderPlayer_1CurrentAmmo.text = GamePlayController.Instance.gameLevelController.currentLevel.player_1.GetCurrentAmmo().ToString();
        defenderPlayer_2CurrentAmmo.text = GamePlayController.Instance.gameLevelController.currentLevel.player_2.GetCurrentAmmo().ToString();

        defenderPlayer_1MaxAmmo.text = GamePlayController.Instance.gameLevelController.currentLevel.player_1.GetMaxAmmo().ToString();
        defenderPlayer_2MaxAmmo.text = GamePlayController.Instance.gameLevelController.currentLevel.player_2.GetMaxAmmo().ToString();

        defenderPlayer_1SpawnCredits.text = GamePlayController.Instance.gameLevelController.currentLevel.player_1.GetCurrentCredits().ToString();
        defenderPlayer_2SpawnCredits.text = GamePlayController.Instance.gameLevelController.currentLevel.player_2.GetCurrentCredits().ToString();

        defenderPlayer_1MaxSpawnCredits.text = GamePlayController.Instance.gameLevelController.currentLevel.player_1.GetMaxCredits().ToString();
        defenderPlayer_2MaxSpawnCredits.text = GamePlayController.Instance.gameLevelController.currentLevel.player_2.GetMaxCredits().ToString();

        player_1prevCredits = GamePlayController.Instance.gameLevelController.currentLevel.player_1.GetCurrentCredits();
        player_2prevCredits = GamePlayController.Instance.gameLevelController.currentLevel.player_2.GetCurrentCredits();

        defenderSetting.onClick.AddListener(delegate { OpenSetting(); });

        Player_1SetupSpawnLaneButtons();
        Player_1SetupSpawnList();
        Player_1UpdateSpawnBtnPriceCheck();

        Player_2SetupSpawnLaneButtons();
        Player_2SetupSpawnList();
        Player_2UpdateSpawnBtnPriceCheck();

        StartDefenderModeClock();

        defenderPlayer_1ShootBtn.onClick.AddListener(delegate { Player_1Shoot(); });
        defenderPlayer_2ShootBtn.onClick.AddListener(delegate { Player_2Shoot(); });
    }

    //----------Buttons----------
    void OpenSetting()
    {      
        GameController.Instance.musicManager.PlayClickSound();
        Time.timeScale = 0;
        SettingBox.Setup().Show();
    }
    
    #endregion

    #region Listener Functions

    #endregion
}
