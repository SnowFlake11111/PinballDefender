using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
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
    [Header("Map Lanes")]
    public List<FieldLane> laneList = new List<FieldLane>();

    [Space]
    [Header("Player 1")]
    public PlayerController player_1;

    [Space]
    [Header("Player 2 (can be left empty)")]
    public PlayerController player_2;

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
    #endregion

    #region Private Variables
    #endregion

    #region Functions
    //----------Public----------
    public void Init()
    {
        if (modeCampaign)
        {
            player_1.Init();
            gameDirector.InitGameDirector();
            GamePlayController.Instance.ChangeUILoadout(1);
        }
        else if (modeScoreBattle)
        {
            player_1.Init();
            player_2.Init();
            gameDirector.InitGameDirector();
            GamePlayController.Instance.ChangeUILoadout(2);
        }
        else
        {
            player_1.Init();
            player_2.Init();
            GamePlayController.Instance.ChangeUILoadout(3);
        }

        DeathWallSetup();
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
