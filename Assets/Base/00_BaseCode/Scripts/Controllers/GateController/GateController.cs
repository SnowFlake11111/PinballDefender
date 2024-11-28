using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    #region Public Variables
    [Header("Gate HP")]
    public int baseHitPoint = 500;

    [Space]
    [Header("Attackers List")]
    public List<GameUnitBase> attackers = new List<GameUnitBase>();


    #endregion

    #region Private Variables
    [SerializeField] int currentHitPoint = 0;

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
    #endregion

    #region Start, Update
    private void Start()
    {
        SetHealth();
    }
    #endregion

    #region Functions
    //----------Public----------
    //**** Health Control ****
    public void SetHealth()
    {
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
    }

    //----------Odin Functions----------
    #endregion

    #region Collision Functions
    #endregion
}
