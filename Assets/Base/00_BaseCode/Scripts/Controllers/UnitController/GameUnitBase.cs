using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameUnitBase : MonoBehaviour
{
    #region Public Variables
    [Header("UNIT HEALTH")]
    public int maxHitPoint = 0;
    public int hitPoint = 0;

    [Space]
    [Header("UNIT OWNER ID")]
    public int ownerId = 0;

    [Space]
    [Header("Enemy Detection Status")]
    public bool enemyFound = false;

    [Space]
    [Header("Gate Detection Status")]
    public bool gateFound = false;

    [Space]
    [Header("Attackers List")]
    public List<GameUnitBase> attackers = new List<GameUnitBase>();

    [Space]
    [Header("Targets List")]
    public List<GameUnitBase> targets = new List<GameUnitBase>();

    [Space]
    [Header("Enemy Gate")]
    public GateController enemyGate;
    #endregion

    #region Private Variables
    [Header("IMPORTANT - allow bounce or not")]
    [SerializeField] bool allowBallBounce = true;
    [SerializeField] int bounceCost = 1;
    #endregion

    #region Start, Update
    #endregion

    #region Functions
    //IMPORTANT: Vì script này được sử dụng làm base nên tất cả các function được đặt public để cho các script kế thừa có thể sử dụng chúng
    //----------Public----------
    //**** Health Control Section ****
    public void SetHealth(int health)
    {
        maxHitPoint = health;
        hitPoint = health;
    }
    public void GainHealth(int health)
    {
        if (hitPoint + health >= maxHitPoint)
        {
            hitPoint = maxHitPoint;
        }
        else
        {
            hitPoint += health;
        }
    }

    //**** Detecting Enemies Section ****
    public void CheckForTarget()
    {
        if (targets.Count > 0)
        {
            enemyFound = true;
            ChangeMovementStatus();
        }
        else
        {
            enemyFound = false;
            GateDetected();
        }
    }

    public void RegisterTarget(GameUnitBase target)
    {
        if (!targets.Contains(target))
        {
            targets.Add(target);
            CheckForTarget();
        }
    }

    public void RemoveTarget(GameUnitBase fallenEnemy)
    {
        targets.Remove(fallenEnemy);
        CheckForTarget();
    }

    //**** Detecting Enemy's Gate Section ****
    public void RegisterEnemyGate(GateController gate)
    {
        enemyGate = gate;
        GateDetected();
    }

    public void RemoveEnemyGate()
    {
        enemyGate = null;
        GateDetected();
    }

    public void GateDetected()
    {
        if (enemyGate != null)
        {
            gateFound = true;
        }
        else
        {
            gateFound = false;
        }

        ChangeMovementStatus();
    }

    //**** Movement Control ****
    public void ChangeMovementStatus()
    {
        if (enemyFound || gateFound)
        {
            if (GetComponent<UnitMovement>() != null)
            {
                GetComponent<UnitMovement>().StopMoving();
            }
        }
        else
        {
            if (GetComponent<UnitMovement>() != null)
            {
                GetComponent<UnitMovement>().StartMoving();
            }
        }
    }

    //**** Damage Taken Section ****
    public void TakeDamage(int damage)
    {
        if (damage > hitPoint)
        {
            AnnounceDeath();
            Destroy(gameObject);
            //To do: Add function to register the kill for the owner of the ball that killed this unit
        }
        else
        {
            hitPoint -= damage;
        }
    }

    public void TakeDamageFromUnit(GameUnitBase attacker, int damage)
    {
        RegisterAttacker(attacker);

        if (damage > hitPoint)
        {
            AnnounceDeath();
            Destroy(gameObject);
            //To do: Add function to register the kill for the owner of the ball that killed this unit
        }
        else
        { 
            hitPoint -= damage;
        }
    }

    public void RegisterAttacker(GameUnitBase attacker)
    {
        //Hàm này là để ghi các unit tấn công unit này vào một danh sách để khi unit này chết, nó sẽ chạy lệnh AnnounceDeath để thông báo tới tất cả các unit đang tấn công nó biết rằng nó đã chết
        if (!attackers.Contains(attacker))
        {
            attackers.Add(attacker);
        }
    }

    //**** Unit's Death Section ****
    public void InstantDeath()
    {
        //This function is strictly used for end of the field walls only
        AnnounceDeath();
        Destroy(gameObject);
    }

    public void AnnounceDeath()
    {
        foreach(GameUnitBase attacker in attackers)
        {
            if (attacker != null)
            {
                attacker.RemoveTarget(this);
            }
        }

        if (enemyGate != null)
        {
            enemyGate.RemoveAttacker(this);
        }
    }


    //**** Ball Bounce Section ****
    public int GetOwnerId()
    {
        return ownerId;
    }

    public bool GetBouncePermission()
    {
        return allowBallBounce;
    }

    public int GetBounceCost()
    {
        return bounceCost;
    }

    //----------Private----------
    #endregion
}
