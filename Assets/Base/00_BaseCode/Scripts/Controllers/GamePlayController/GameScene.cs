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

    GameObject unitHolder;

    int chosenBoosterId;
    int player_1prevCredits = 0;
    int player_2prevCredits = 0;
    int player_1prevScore = 0;
    int player_2prevScore = 0;

    bool campaignMode = false;
    bool scoreBattleMode = false;
    bool defenderBattleMode = false;

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
    Tween player_1Reload;
    Tween player_2Reload;
    Tween player_1GainCredits;
    Tween player_2GainCredits;
    Tween player_1GainScore;
    Tween player_2GainScore;
    Tween player_1LowAmmoWarning;
    Tween player_2LowAmmoWarning;
    Tween player_1LowCreditsWarning;
    Tween player_2LowCreditsWarning;
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
                //player_1GainCreditsAnimation = campaignPlayerSpawnCredits.DOText(newCurrentCredits.ToString(), 0.75f, scrambleMode: ScrambleMode.Numerals)
                //    .OnComplete(delegate
                //    {
                //        campaignPlayerSpawnCredits.text = newCurrentCredits.ToString();
                //        player_1GainCreditsAnimation = null;
                //    });

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
                //player_1GainCreditsAnimation = defenderPlayer_1SpawnCredits.DOText(newCurrentCredits.ToString(), 0.75f, scrambleMode: ScrambleMode.Numerals)
                //    .OnComplete(delegate
                //    {
                //        defenderPlayer_1SpawnCredits.text = newCurrentCredits.ToString();
                //        player_1GainCreditsAnimation = null;
                //    });
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
            //player_2GainCreditsAnimation = defenderPlayer_2SpawnCredits.DOText(newCurrentCredits.ToString(), 0.75f, scrambleMode: ScrambleMode.Numerals)
            //        .OnComplete(delegate
            //        {
            //            defenderPlayer_2SpawnCredits.text = newCurrentCredits.ToString();
            //            player_2GainCreditsAnimation = null;
            //        });

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
        for (int i = 0; i < player_1UnitsCostList.Count; i++)
        {
            //Note: Incomplete, awaiting test from campaign
        }
    }

    //*** BUTTONS ACTION ***
    public void Player_1StartReload()
    {
        if (player_1Reload == null)
        {
            if (campaignMode)
            {
                player_1Reload = campaignPlayerReloadEffect.DOFillAmount(1, 5f)
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
                player_1Reload = scorePlayer_1ReloadEffect.DOFillAmount(1, 5f)
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
                player_1Reload = defenderPlayer_1ReloadEffect.DOFillAmount(1, 5f)
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
                player_2Reload = scorePlayer_2ReloadEffect.DOFillAmount(1, 5f)
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
                player_2Reload = defenderPlayer_2ReloadEffect.DOFillAmount(1, 5f)
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
                        GamePlayController.Instance.gameLevelController.currentLevel.player_1.CreditsGainedOverTime(2);
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
                        GamePlayController.Instance.gameLevelController.currentLevel.player_2.CreditsGainedOverTime(2);
                        player_2GainCredits = null;
                        if (!GamePlayController.Instance.gameLevelController.currentLevel.player_2.IsCreditsMaxed())
                        {
                            Player_2GainingCredits();
                        }
                    });
        }
    }

    public void Player_1PickedUnitToSpawn(GameUnitBase pickedUnit)
    {
        if (player_1ChosenUnitToSpawn != null)
        {
            if (player_1ChosenUnitToSpawn == pickedUnit)
            {
                foreach (Button lanePickingBtn in campaignUnitSpawnLaneBtn)
                {
                    if (lanePickingBtn.gameObject.activeSelf)
                    {
                        lanePickingBtn.gameObject.SetActive(false);
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
                foreach (Button lanePickingBtn in defenderPlayer_1UnitSpawnLaneBtn.Skip(1).Take(campaignUnitSpawnLaneBtn.Count - 2))
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
        //Note: Incomplete, awaiting test from campaign
        player_2ChosenUnitToSpawn = pickedUnit;
    }

    public void Player_1SpawnUnitProcedure(int laneId)
    {
        if (player_1ChosenUnitToSpawn != null)
        {
            GamePlayController.Instance.gameLevelController.currentLevel.player_1.SpawnAnUnit(player_1ChosenUnitToSpawn, lanes[laneId].spawnPointA, lanes[laneId], unitHolder);

            foreach(Button lanePickingBtn in campaignUnitSpawnLaneBtn)
            {
                if (lanePickingBtn.gameObject.activeSelf)
                {
                    lanePickingBtn.gameObject.SetActive(false);
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
        //Note: Incomplete, awaiting test from campaign
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

                for (int i = 0; i < defenderPlayer_1UnitSpawnBtn.Count; i++)
                {
                    int tempIndex = i;
                    defenderPlayer_1UnitSpawnBtnImage[i].sprite = unitImages[i + 1];
                    defenderPlayer_1UnitSpawnBtnCost[i].text = player_1chosenUnits[i].spawnCost.ToString();
                    player_1UnitsCostList.Add(player_1chosenUnits[i].spawnCost);
                    defenderPlayer_1UnitSpawnBtn[i].onClick.AddListener(delegate { Player_1PickedUnitToSpawn(player_1chosenUnits[tempIndex]); });
                }
            }
            else
            {
                for (int i = 0; i < defenderPlayer_1UnitSpawnBtn.Count; i++)
                {
                    int tempIndex = i;
                    defenderPlayer_1UnitSpawnBtnImage[i].sprite = unitImages[i + 1];
                    defenderPlayer_1UnitSpawnBtnCost[i].text = player_1chosenUnits[i].spawnCost.ToString();
                    player_1UnitsCostList.Add(player_1chosenUnits[i].spawnCost);
                    defenderPlayer_1UnitSpawnBtn[i].onClick.AddListener(delegate { Player_1PickedUnitToSpawn(player_1chosenUnits[tempIndex]); });
                }
            }
        }
    }

    void Player_2SetupSpawnList()
    {
        //Note: Incomplete, awaiting test from campaign
        List<GameUnitBase> choosenUnits = new List<GameUnitBase>();
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
        //Note: Incomplete, awaiting test from campaign
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

        defenderPlayer_1ShootBtn.onClick.AddListener(delegate { Player_1Shoot(); });
        defenderPlayer_2ShootBtn.onClick.AddListener(delegate { Player_2Shoot(); });

        player_1prevCredits = GamePlayController.Instance.gameLevelController.currentLevel.player_1.GetCurrentCredits();
        player_2prevCredits = GamePlayController.Instance.gameLevelController.currentLevel.player_2.GetCurrentCredits();
    }

    //----------Buttons----------
    void OpenSetting()
    {
        GameController.Instance.musicManager.PlayClickSound();

        SettingBox.Setup().Show();
    }
    
    #endregion

    #region Listener Functions

    #endregion
}
