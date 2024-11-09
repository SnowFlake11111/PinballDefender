using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUnitBase : SerializedMonoBehaviour
{
    #region Public Variables
    [Header("UNIT HEALTH")]
    public int maxHitPoint = 0;
    public int hitPoint = 0;

    [Space]
    [Header("UNIT ANIMATOR")]
    public Animator animatorBase;

    [Space]
    [Header("UNIT COLLIDERBOX")]
    public BoxCollider unitColliderBox;

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

    [Space]
    [Header("Alt color for side identification")]
    public Material altColor;
    public SkinnedMeshRenderer mainBodyColor;

    [Space]
    [Header("IMPORTANT - Spawning info")]
    public int spawnCost = 0;
    public bool isBossOrMiniboss = false;
    #endregion

    #region Private Variables
    [Header("IMPORTANT - allow bounce or not")]
    [SerializeField] bool allowBallBounce = true;
    [SerializeField] int bounceCost = 1;

    [Space]
    [Header("Berserker unit only (or any unit in the future with fast movement animation)")]
    [LabelWidth(200)]
    [SerializeField] bool hasFastMovementAnimation = false;

    GameDirector gameDirector;
    bool spawnedByDirector = false;
    bool unitIsDead = false;
    #endregion

    #region Start, Update
    private void Start()
    {
        if (gameObject.layer != 7)
        {
            mainBodyColor.material = altColor;
        }

        if (GetComponent<UnitMovement>() != null)
        {
            GetComponent<UnitMovement>().Init();
        }
    }
    #endregion

    #region Functions
    //IMPORTANT: Vì script này được sử dụng làm base nên tất cả các function được đặt public để cho các script kế thừa có thể sử dụng chúng
    //----------Public----------
    //**** Unit Owner Section ****
    public void SpawnedByDirector(GameDirector director)
    {
        gameDirector = director;
        gameObject.layer = 6;
        spawnedByDirector = true;
    }

    public void SpawnedByPlayer()
    {
        //To Do: Let player register unit's layer so unity can handle the physics interaction
    }

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
        if (hitPoint <= 0)
        {
            AnnounceDeath();
            return;
        }

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
        if (damage >= hitPoint)
        {
            AnnounceDeath();

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
    }

    public void DeathAnimation()
    {
        if (GetComponent<UnitMovement>() != null)
        {
            GetComponent<UnitMovement>().DeathState();
        }

        animatorBase.Play("Death");
    }

    public void FinishDeathAnimation()
    {
        transform.DOMoveY(transform.position.y - 5f, 1f)
            .SetEase(Ease.Linear)
            .OnComplete(delegate
            {
                Destroy(gameObject);
            });
    }

    public void AnnounceDeath()
    {
        if (!unitIsDead)
        {
            unitIsDead = true;
        }
        else
        {
            return;
        }

        if (unitColliderBox != null)
        {
            unitColliderBox.enabled = false;
        }

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

        if (spawnedByDirector)
        {
            gameDirector.RemoveFallenUnit(this);
        }

        DeathAnimation();
    }


    //**** Ball Bounce Section ****
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

/*
 * Danh sách Layer:
 * 6: Director
 * 7: Player_1
 * 8: Player_2
 * 
 * NOTE: 3 layer này đã được thiết lập để không xử lý vật lý (như va chạm collider) đối với các object có cùng layer với chúng
 */