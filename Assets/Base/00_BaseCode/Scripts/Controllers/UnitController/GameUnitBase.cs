using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameUnitBase : SerializedMonoBehaviour
{
    #region Public Variables
    [Header("UNIT ID")]
    public int unitId = 0;

    [Space]
    [Header("UNIT HEALTH")]
    public int maxHitPoint = 0;
    public int hitPoint = 0;
    public Image healthBar;
    public Image healthBarDelay;

    [Space]
    [Header("UNIT ANIMATOR")]
    public Animator animatorBase;

    [Space]
    [Header("UNIT COLLIDERBOX")]
    public BoxCollider unitColliderBox;

    [Space]
    [Header("UNIT ATTACKZONE")]
    public AttackZone attackZone;

    [Space]
    [Header("UNIT MOVEMENT SCRIPT (can be empty)")]
    public UnitMovement unitMovement;

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
    public int moneyGainOnKill = 0;
    public int scoreGainOnKill = 0;
    public bool isBossOrMiniboss = false;

    [ShowIf("HpEventSwitches")]
    public bool stunAt66PercentHp = false;
    [ShowIf("HpEventSwitches")]
    public bool stunAt50PercentHp = false;
    [ShowIf("HpEventSwitches")]
    public bool stunAt33PercentHp = false;

    [Space]
    [Header("BERSERKER ONLY")]
    public bool canEnrage = false;
    [ShowIf("EnrageHpTrigger")]
    public float enrageAtOrBelow = 0;

    [Space]
    [Header("IMPORTANT - Buff Icons")]
    public GameObject buffsHolder;
    public Image attackUpIcon;
    public Image defenseUpIcon;
    public Image rallyIcon;
    public Image rageIcon;

    [NonSerialized] public PlayerController playerOwner;
    #endregion

    #region Private Variables
    [Header("IMPORTANT - set ball bounce cost")]
    [SerializeField] int bounceCost = 1;

    GameDirector gameDirector;
    bool spawnedByDirector = false;

    bool unitIsDead = false;

    Coroutine attackUpTimer;
    Coroutine defenseUpTimer;
    Coroutine rallyTimer;

    Image attackUpActive;
    Image defenseUpActive;
    Image rallyActive;
    Image rageActive;

    float currentHealthPercent
    {
        get
        {
            if (hitPoint <= 0)
            {
                return 0;
            }
            return hitPoint / (float)maxHitPoint;
        }
    }

    Tweener healthBarMainChange;
    Tweener healthBarDelayChange;

    bool stunnedAt66 = false;
    bool stunnedAt50 = false;
    bool stunnedAt33 = false;

    //----------Buffs timer-----------
    float defenseUp = 0;
    float attackUp = 0;
    float rally = 0;

    //----------Berserker buff----------
    bool rage = false;
    #endregion

    #region Event Handler
    [NonSerialized]
    public UnityEvent stunEventWithoutValue = new UnityEvent();
    [NonSerialized]
    public UnityEvent<int> stunEventWithValue = new UnityEvent<int>();
    [NonSerialized]
    public UnityEvent deathEvent = new UnityEvent();
    #endregion

    #region Start, Update
    #endregion

    #region Functions
    //IMPORTANT: Vì script này được sử dụng làm base nên tất cả các function được đặt public để cho các script kế thừa có thể sử dụng chúng
    //----------Public----------
    //**** Unit Initialize ****
    public void InitUnit()
    {
        if (gameObject.layer != 7)
        {
            mainBodyColor.material = altColor;
            healthBar.color = new Color(255 / 255, 30 / 255, 30 / 255);
        }

        if (attackZone != null)
        {
            attackZone.Init();
        }
    }

    //**** Unit Owner Section ****
    public void SpawnedByDirector(GameDirector director)
    {
        gameDirector = director;
        gameObject.layer = 6;
        spawnedByDirector = true;
    }

    public void SpawnedByPlayer(PlayerController player)
    {
        //To Do: Let player register unit's layer so unity can handle the physics interaction [done]
        gameObject.layer = player.gameObject.layer;
        playerOwner = player;
    }

    //**** Health Control Section ****
    public void GainHealthPercent(float healPercent)
    {
        if (hitPoint + Mathf.FloorToInt(maxHitPoint * healPercent) >= maxHitPoint)
        {
            hitPoint = maxHitPoint;
        }
        else
        {
            hitPoint += Mathf.FloorToInt(maxHitPoint * healPercent);
        }

        UpdateHealthBar(true);
    }

    public void UpdateHealthBar()
    {
        if (healthBarMainChange != null)
        {
            healthBarMainChange.ChangeEndValue(currentHealthPercent, true);
        }
        else
        {
            if (healthBarDelayChange != null)
            {
                healthBarDelayChange.Kill();
            }

            healthBarMainChange = healthBar.DOFillAmount(currentHealthPercent, 0.5f)
                .OnComplete(delegate
                {
                    healthBarMainChange = null;
                    healthBarDelayChange = healthBarDelay.DOFillAmount(currentHealthPercent, 0.5f)
                        .SetDelay(1)
                        .SetAutoKill(true)
                        .OnComplete(delegate
                        {
                            healthBarDelayChange = null;
                        });
                });
        }

        HealthEvents();
    }

    public void UpdateHealthBar(bool gainHealth)
    {
        if (healthBarMainChange != null)
        {
            healthBarMainChange.ChangeEndValue(currentHealthPercent, true);
        }
        else
        {
            if (healthBarDelayChange != null)
            {
                healthBarDelayChange.Kill();
            }

            healthBarMainChange = healthBar.DOFillAmount(currentHealthPercent, 0.5f)
                .OnStart(delegate
                {
                    healthBarMainChange = null;
                    healthBarDelayChange = healthBarDelay.DOFillAmount(currentHealthPercent, 0.5f)
                        .OnComplete(delegate
                        {
                            healthBarDelayChange = null;
                        });
                });
        }

        HealthEvents();
    }

    public void HealthEvents()
    {
        if (canEnrage)
        {
            if (currentHealthPercent <= enrageAtOrBelow / 100 && !rage)
            {
                ActivateRage();
            }
            else if (currentHealthPercent > enrageAtOrBelow / 100 && rage)
            {
                DeactivateRage();
            }
        }

        if (stunAt66PercentHp)
        {
            if (currentHealthPercent <= 0.66f && !stunnedAt66)
            {
                stunnedAt66 = true;
                if (stunEventWithValue != null)
                {
                    stunEventWithValue.Invoke(66);
                }
                
                if (stunEventWithoutValue != null)
                {
                    stunEventWithoutValue.Invoke();
                }
            }
        }

        if (stunAt50PercentHp)
        {
            if (currentHealthPercent <= 0.5f && !stunnedAt50)
            {
                stunnedAt50 = true;
                if (stunEventWithValue != null)
                {
                    stunEventWithValue.Invoke(50);
                }

                if (stunEventWithoutValue != null)
                {
                    stunEventWithoutValue.Invoke();
                }
            }
        }

        if (stunAt33PercentHp)
        {
            if (currentHealthPercent <= 0.33f && !stunnedAt33)
            {
                stunnedAt33 = true;
                if (stunEventWithValue != null)
                {
                    stunEventWithValue.Invoke(33);
                }

                if (stunEventWithoutValue != null)
                {
                    stunEventWithoutValue.Invoke();
                }
            }
        }
    }

    //**** Detecting Enemies Section ****
    public void CheckForTarget()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null)
            {
                targets.RemoveAt(i);
                i--;
            }
        }

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
            if (unitMovement != null)
            {
                unitMovement.StopMoving();
            }
        }
        else
        {
            if (unitMovement != null)
            {
                unitMovement.StartMoving();
            }
        }
    }

    //**** Damage Taken Section ****
    public void TakeDamage(PlayerController player, int damage, int ballBounceCount = 0)
    {
        if (defenseUp > 0)
        {
            if ((damage / 2) >= hitPoint)
            {
                hitPoint = 0;
                SendRewardToPlayer(player, ballBounceCount);
                AnnounceDeath();

                //To do: Add function to register the kill for the owner of the ball that killed this unit
            }
            else
            {
                hitPoint -= damage / 2;
            }
        }
        else
        {
            if (damage >= hitPoint)
            {
                hitPoint = 0;
                SendRewardToPlayer(player, ballBounceCount);
                AnnounceDeath();

                //To do: Add function to register the kill for the owner of the ball that killed this unit
            }
            else
            {
                hitPoint -= damage;              
            }
        }

        UpdateHealthBar();
    }

    public void TakeDamageFromUnit(GameUnitBase attacker, int damage)
    {
        RegisterAttacker(attacker);

        if (defenseUp > 0)
        {
            if ((damage / 2) > hitPoint)
            {
                hitPoint = 0;
                if (!attacker.spawnedByDirector)
                {
                    SendRewardToPlayer(attacker.playerOwner);
                }
                AnnounceDeath();

                //To do: Add function to register the kill for the owner of the ball that killed this unit
            }
            else
            {
                hitPoint -= damage / 2;
            }
        }
        else
        {
            if (damage > hitPoint)
            {
                hitPoint = 0;
                if (!attacker.spawnedByDirector)
                {
                    SendRewardToPlayer(attacker.playerOwner);
                }
                AnnounceDeath();

                //To do: Add function to register the kill for the owner of the ball that killed this unit
            }
            else
            {
                hitPoint -= damage;               
            }
        }

        UpdateHealthBar();
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
    public bool IsThisUnitDead()
    {
        if(unitIsDead)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SendRewardToPlayer(PlayerController executioner, int ballBounceCount = 0)
    {
        if (GamePlayController.Instance.gameLevelController.currentLevel.modeCampaign || GamePlayController.Instance.gameLevelController.currentLevel.modeDefenderBattle)
        {
            if (moneyGainOnKill <= 0)
            {
                return;
            }

            executioner.CreditsGainedFromKill(moneyGainOnKill);
        }
        else
        {
            //To do: Handle sending points to player on kill [done]
            if (scoreGainOnKill <= 0)
            {
                return;
            }
            executioner.ScoreGainedFromKill(scoreGainOnKill + Mathf.RoundToInt(scoreGainOnKill * 0.25f * ballBounceCount));
        }
    }

    public void InstantDeath()
    {
        //This function is strictly used for end of the field walls only
        AnnounceDeath();
    }

    public void DeathAnimation()
    {
        if (unitMovement != null)
        {
            unitMovement.DeathState();
        }

        if (animatorBase.speed != 1)
        {
            animatorBase.speed = 1;
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
            if (deathEvent != null)
            {
                deathEvent.Invoke();
            }
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
    public int GetBounceCost()
    {
        return bounceCost;
    }

    //**** Buffs Section ****
    public bool IsAttackUpActive()
    {
        if (attackUp > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsDefenseUpActive()
    {
        if (defenseUp > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsRallyActive()
    {
        if (rally > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsRageActive()
    {
        return rage;
    }

    public void GrantAttackUp(float duration)
    {
        attackUp = duration;
        if (attackUpTimer == null)
        {
            attackUpActive = Instantiate(attackUpIcon, buffsHolder.transform);
            attackUpTimer = StartCoroutine(AttackUpDuration());
        }
    }

    public void GrantDefenseUp(float duration)
    {
        defenseUp = duration;
        if (defenseUpTimer == null)
        {
            defenseUpActive = Instantiate(defenseUpIcon, buffsHolder.transform);
            defenseUpTimer = StartCoroutine(DefenseUpDuration());
        }
    }

    public void GrantRally(float duration)
    {
        rally = duration;
        if (rallyTimer == null)
        {
            if (unitMovement != null)
            {
                unitMovement.ActivateRallyBuff();
            }

            rallyActive = Instantiate(rallyIcon, buffsHolder.transform);
            rallyTimer = StartCoroutine(RallyDuration());
        }
    }

    public void ActivateRage()
    {
        rage = true;
        rageActive = Instantiate(rageIcon, buffsHolder.transform);
        if (unitMovement != null)
        {
            unitMovement.ActivateEnrageSpeedBuff();
        }
        animatorBase.speed = 1.5f;
    }

    public void DeactivateRage()
    {
        rage = false;
        Destroy(rageActive.gameObject);
        if (unitMovement != null)
        {
            unitMovement.DisableEnrageSpeedBuff();
        }
        animatorBase.speed = 1;
    }

    IEnumerator AttackUpDuration()
    {
        while (attackUp > 0)
        {
            yield return new WaitForSeconds(1);
            attackUp--;
        }

        attackUpTimer = null;
        Destroy(attackUpActive.gameObject);
    }

    IEnumerator DefenseUpDuration()
    {
        while (defenseUp > 0)
        {
            yield return new WaitForSeconds(1);
            defenseUp--;
        }

        defenseUpTimer = null;
        Destroy(defenseUpActive.gameObject);
    }

    IEnumerator RallyDuration()
    {
        while (rally > 0)
        {
            yield return new WaitForSeconds(1);
            rally--;
        }

        if (unitMovement != null)
        {
            unitMovement.DisableRallyBuff();
        }
        rallyTimer = null;
        Destroy(rallyActive.gameObject);
    }

    //----------Private----------
    //----------Odin Functions----------
    bool HpEventSwitches()
    {
        if (isBossOrMiniboss)
        {
            return true;
        }
        else
        {
            stunAt66PercentHp = false;
            stunAt50PercentHp = false;
            stunAt33PercentHp = false;

            return false;
        }
    }

    bool EnrageHpTrigger()
    {
        if (canEnrage)
        {
            return true;
        }
        else
        {
            enrageAtOrBelow = 0;
            return false;
        }
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