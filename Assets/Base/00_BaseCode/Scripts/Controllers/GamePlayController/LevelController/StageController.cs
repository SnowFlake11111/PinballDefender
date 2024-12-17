using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StageController : SerializedMonoBehaviour
{
    #region Public Variables
    [Header("Stage ID")]
    public int stageId = 0;

    [Space]
    [Header("IMPORTANT - Set gamemode for this stage")]
    [OnValueChanged("CampaignActivated")]
    public bool modeCampaign = false;
    [OnValueChanged("ScoreBattleActivated")]
    public bool modeScoreBattle = false;
    [OnValueChanged("DefenderBattleActivated")]
    public bool modeDefenderBattle = false;

    [Space]
    [Header("Prize gem for player on winning")]
    public int prizeGem = 0;

    [Space]
    [Header("Map Lanes")]
    public List<FieldLane> laneList = new List<FieldLane>();

    [Space]
    [Header("Player 1")]
    public PlayerController player_1;

    [Space]
    [Header("Player 2 (can be left empty)")]
    public PlayerController player_2;

    [Space]
    [Header("Player 1 Gate (can be left empty)")]
    public GateController player_1Gate;

    [Space]
    [Header("Player 2 Gate (can be left empty)")]
    public GateController player_2Gate;

    [Space]
    [Header("End of the field wall")]
    public DeathWall deathWallASide;
    public DeathWall deathWallBSide;

    [Space]
    [Header("Unit Holder")]
    public GameObject unitHolder;

    [Space]
    [Header("Game Director (can be left empty)")]
    public GameDirector gameDirector;

    [Space]
    [Header("Ball hit effect")]
    public GameObject ballHitEffect;

    [BoxGroup("Wall & Floor Material")]
    public List<Material> walls = new List<Material>();

    [BoxGroup("Wall & Floor Material")]
    public List<Material> floors = new List<Material>();

    [BoxGroup("Wall & Floor Material")]
    [Space]
    public List<MeshRenderer> wallsToApplyMaterial = new List<MeshRenderer>();

    [BoxGroup("Wall & Floor Material")]
    public List<MeshRenderer> floorsToApplyMaterial = new List<MeshRenderer>();

    [BoxGroup("Wall & Floor Material")]
    [Space]
    [OnValueChanged("ChangeWall")]
    [ValueDropdown("GetWallChoices")]
    public int wallChoice = 0;

    [BoxGroup("Wall & Floor Material")]
    [OnValueChanged("ChangeFloor")]
    [ValueDropdown("GetFloorChoices")]
    public int floorChoice = 0;
    #endregion

    #region Private Variables
    bool gameIsAlreadyEnded = false;

    GameObject hitEffectTempReference;
    #endregion

    #region Functions
    //----------Public----------
    public void Init()
    {
        if (modeCampaign)
        {
            player_1.Init(1);
            player_1Gate.Init();
            gameDirector.InitGameDirector();
            GamePlayController.Instance.ChangeUILoadout(1);
        }
        else if (modeScoreBattle)
        {
            player_1.Init(2);
            player_2.Init(2);
            gameDirector.InitGameDirector();
            GamePlayController.Instance.ChangeUILoadout(2);
        }
        else
        {
            player_1.Init(3);
            player_1Gate.Init();
            player_2.Init(3);
            player_2Gate.Init();
            GamePlayController.Instance.ChangeUILoadout(3);
        }

        DeathWallSetup();
        BallHitEffectSetup();
    }

    public List<FieldLane> GetMapLanes()
    {
        return laneList;
    }

    public bool DoesStageHaveDirector()
    {
        if (gameDirector != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GameOutcome(int loserId = 0)
    {
        if (gameIsAlreadyEnded)
        {
            return;
        }

        if (modeCampaign)
        {
            if (loserId != 0)
            {
                //Player lose
                CampaignEndGame.Setup(stageId, false).Show();
            }
            else
            {
                //Player win
                if (UseProfile.LevelProgress < stageId)
                {
                    UseProfile.LevelProgress = stageId;
                }

                CampaignEndGame.Setup(stageId, true).Show();
                UseProfile.GameGem += prizeGem;
                UnitUnlockChecker(stageId);
            }

            gameDirector.StopSpawningSequence();
            GamePlayController.Instance.gameScene.StopPlayer_1Entirely();
        }
        else if (modeScoreBattle)
        {
            //Open ScoreBattle End Screen
            ScoreBattleEndGame.Setup().Show();

            GamePlayController.Instance.gameScene.StopPlayer_1Entirely();
            GamePlayController.Instance.gameScene.StopPlayer_2Entirely();

            GameController.Instance.gameAchievementController.UpdateScoreBattleCount();

            UseProfile.GameGem += prizeGem;
        }
        else
        {
            GamePlayController.Instance.gameScene.StopDefenderModeClock();

            DefenderBattleEndGame.Setup(loserId).Show();

            GamePlayController.Instance.gameScene.StopPlayer_1Entirely();
            GamePlayController.Instance.gameScene.StopPlayer_2Entirely();

            GameController.Instance.gameAchievementController.UpdateDefenderBattleCount();

            UseProfile.GameGem += prizeGem + Mathf.RoundToInt(prizeGem * 0.25f * (GamePlayController.Instance.gameScene.GetDefenderClockTime() % 30));
        }

        gameIsAlreadyEnded = true;
    }

    public void ActivateHitEffect(Vector3 spawnPos)
    {
        hitEffectTempReference = SimplePool2.Spawn(ballHitEffect, spawnPos, Quaternion.identity);
        hitEffectTempReference.GetComponent<HitEffect>().Init();
    }
    //----------Private----------
    void DeathWallSetup()
    {
        deathWallASide.Init(7);

        if (!modeDefenderBattle)
        {
            deathWallBSide.Init(6);
        }
        else
        {
            deathWallBSide.Init(8);
        }
    }

    void BallHitEffectSetup()
    {
        if (SimplePool2.GetStackCount(ballHitEffect) < 10)
        {
            SimplePool2.Preload(ballHitEffect, 15);
        }
    }

    void UnitUnlockChecker(int stageId)
    {
        switch(stageId)
        {
            case 1:
                GameController.Instance.gameAchievementController.RegisterUnitUnlock(1);
                break;
            case 3:
                GameController.Instance.gameAchievementController.RegisterUnitUnlock(2);
                break;
            case 5:
                GameController.Instance.gameAchievementController.RegisterUnitUnlock(4);
                break;
            case 7:
                GameController.Instance.gameAchievementController.RegisterUnitUnlock(3);
                break;
            case 10:
                GameController.Instance.gameAchievementController.RegisterUnitUnlock(5);
                break;
            case 11:
                GameController.Instance.gameAchievementController.RegisterUnitUnlock(6);
                break;
            case 13:
                GameController.Instance.gameAchievementController.RegisterUnitUnlock(7);
                break;
            case 15:
                GameController.Instance.gameAchievementController.RegisterUnitUnlock(9);
                break;
            case 17:
                GameController.Instance.gameAchievementController.RegisterUnitUnlock(8);
                break;
            case 20:
                GameController.Instance.gameAchievementController.RegisterUnitUnlock(10);
                break;
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

    void ChangeWall()
    {
        if (wallsToApplyMaterial.Count <= 0)
        {
            Debug.LogError("There are no walls");
        }
        else
        {
            foreach(MeshRenderer wall in wallsToApplyMaterial)
            {
                wall.material = walls[wallChoice];
            }
        }
    }

    void ChangeFloor()
    {
        if (floorsToApplyMaterial.Count <= 0)
        {
            Debug.LogError("There are no floors");
        }
        else
        {
            foreach(MeshRenderer floor in floorsToApplyMaterial)
            {
                floor.material = floors[floorChoice];
            }
        }
    }

    IEnumerable GetFloorChoices()
    {
        List<int> choices = new List<int>();
        for (int i = 0; i < floors.Count; i++)
        {
            choices.Add(i);
        }

        return choices;
    }

    IEnumerable GetWallChoices()
    {
        List<int> choices = new List<int>();
        for (int i = 0; i < walls.Count; i++)
        {
            choices.Add(i);
        }

        return choices;
    }
    #endregion
}

/*
 * Danh sách Layer:
 * 6: Director
 * 7: Player_1
 * 8: Player_2
 * 
 * NOTE: 3 layer này đã được thiết lập để không xử lý vật lý (như va chạm collider) đối với các object có cùng layer với chúng
 */
