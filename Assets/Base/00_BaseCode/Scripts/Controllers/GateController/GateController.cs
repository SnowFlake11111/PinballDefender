using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    #region Public Variables
    #endregion

    #region Private Variables
    int baseHitPoint = 1250;
    int currentHitPoint = 1250;

    float hpPercentage
    {
        get
        {
            if (currentHitPoint <= 0)
            {
                return 0;
            }
            return currentHitPoint / (float)baseHitPoint;
        }
    }
    
    List<GameUnitBase> attackers = new List<GameUnitBase>();
    #endregion

    #region Start, Update
    #endregion

    #region Functions
    //----------Public----------
    //**** Health Control ****
    public void Init()
    {
        SetupGateHealth();
        currentHitPoint = baseHitPoint;
        //To Do: Set realHitPoint to be the actual hp of the gate with some extra hp if upgrade count goes up
    }

    //**** Take Damage ****
    public void TakeDamage(GameUnitBase attacker, int damage)
    {
        RegisterAttacker(attacker);

        if (damage > currentHitPoint)
        {
            AnnounceDeath();
            Destroy(gameObject);
        }
        else
        {
            currentHitPoint -= damage;
        }

        RequestUpdateHpValue();
    }

    public void RemoveAttacker(GameUnitBase attacker)
    {
        attackers.Remove(attacker);
    }
    //----------Private----------
    void SetupGateHealth()
    {
        if (GamePlayController.Instance.gameLevelController.currentLevel.modeCampaign)
        {
            baseHitPoint = 1250 + 250 * UseProfile.CampaignGateHealthUpgradeCount;
        }
        else
        {
            if (gameObject.layer == 7)
            {
                baseHitPoint = 1250 + 250 * GameController.Instance.gameModeData.GetPlayerGateHealthUpgrade(1);
            }
            else
            {
                baseHitPoint = 1250 + 250 * GameController.Instance.gameModeData.GetPlayerGateHealthUpgrade(2);
            }
        }
    }

    //**** Take Damage ****
    void RegisterAttacker(GameUnitBase attacker)
    {
        if (!attackers.Contains(attacker))
        {
            attackers.Add(attacker);
        }
    }

    void RequestUpdateHpValue()
    {
        if (gameObject.layer == 7)
        {
            GamePlayController.Instance.gameScene.UpdateGateHp(hpPercentage, 1);
        }
        else
        {
            GamePlayController.Instance.gameScene.UpdateGateHp(hpPercentage, 2);
        }
    }

    //**** Death Section ****
    void AnnounceDeath()
    {
        foreach (GameUnitBase attacker in attackers)
        {
            if (attacker != null)
            {
                attacker.RemoveEnemyGate();
            }
        }

        //To do: Handle player losing situation [done]
        if (gameObject.layer != 7)
        {
            GamePlayController.Instance.gameLevelController.currentLevel.GameOutcome(2);
        }
        else
        {
            GamePlayController.Instance.gameLevelController.currentLevel.GameOutcome(1);
        }
    }

    //----------Odin Functions----------
    #endregion

    #region Collision Functions
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