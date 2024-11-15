using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnforcerUnit : GameUnitBase
{
    #region Public Variables
    [BoxGroup("Enforcer Stats", centerLabel: true)]
    [Header("Base Attack Damage")]
    public int baseAttackDamage = 0;

    [BoxGroup("Enforcer Stats")]
    [Space]
    [Header("Defense Up Skill cooldown")]
    public float defenseUpCooldown = 0;

    [BoxGroup("Enforcer Stats")]
    [Header("Defense Up Skill cooldown")]
    public float zoneLingerTime = 0;

    [BoxGroup("Enforcer Stats")]
    [Space]
    [Header("Shield Up Buff Controller")]
    public UnitBuffZone buffZone;

    [BoxGroup("Enforcer Stats")]
    [Space]
    [Header("Current Target")]
    public GameUnitBase currentTarget;
    #endregion

    #region Private Variables
    bool attackEnemy = false;

    int attackRandomizer = 0;

    Coroutine buffZoneHandler;
    #endregion

    #region Start, Update
    private void Start()
    {
        base.Start();
        deathEvent.AddListener(StopAutoSkill);
        StartAutoSkill();

        buffZone.InitBuffZone(gameObject.layer);
    }
    #endregion

    #region Functions
    //----------Public----------
    public void ActivateAttackPhase()
    {
        if (!attackEnemy)
        {
            attackEnemy = true;
            AttackThinking();
        }
    }

    //----------Private----------
    void AttackThinking()
    {
        if (enemyFound)
        {
            DealDamage();
        }
        else if (gateFound)
        {
            DealDamageToGate();
        }
        else
        {
            attackEnemy = false;
        }
    }

    void DealDamage()
    {
        GetClosestEnemy();
        InitiateAttackAnimation();
    }

    void DealDamageToGate()
    {
        InitiateAttackAnimation();
    }

    void ContinueAttackingOrNot()
    {
        CheckForTarget();
        if (enemyFound || gateFound)
        {
            AttackThinking();
        }
        else
        {
            attackEnemy = false;
            StopAttackAnimation();
        }
    }

    void GetClosestEnemy()
    {
        //The logic is simple, check all targets then compare distance with the pre-determined 'distance' variable, if the new distance is smaller than the 'distance' variable then take that target as the chosen target for attack and take the new distance number as the new 'distance' variable. Repeat this process till the list is empty

        if (targets.Count > 1)
        {
            GameUnitBase closestTarget = null;
            float distance = 999999;

            foreach (GameUnitBase target in targets)
            {
                if (distance > Vector3.Distance(transform.localPosition, target.transform.localPosition))
                {
                    closestTarget = target;
                    distance = Vector3.Distance(transform.localPosition, target.transform.localPosition);
                }
            }

            currentTarget = closestTarget;
        }
        else
        {
            currentTarget = targets[0];
        }
    }

    void StartAutoSkill()
    {
        buffZoneHandler = StartCoroutine(AutoDefenseUpZone());
    }

    void StopAutoSkill()
    {
        StopCoroutine(buffZoneHandler);
    }

    IEnumerator AutoDefenseUpZone()
    {
        yield return new WaitForSeconds(defenseUpCooldown);
        buffZone.ActivateBuffZone(zoneLingerTime);
        buffZoneHandler = StartCoroutine(AutoDefenseUpZone());
    }
    //----------Animation Functions----------
    public void SlashEnemy()
    {
        if (gateFound)
        {
            if (IsAttackUpActive())
            {
                enemyGate.TakeDamage(this, baseAttackDamage + Mathf.FloorToInt(baseAttackDamage * 0.2f));
            }
            else
            {
                enemyGate.TakeDamage(this, baseAttackDamage);
            }
        }
        else if (enemyFound)
        {
            if (IsAttackUpActive())
            {
                currentTarget.TakeDamageFromUnit(this, baseAttackDamage + Mathf.FloorToInt(baseAttackDamage * 0.2f));
            }
            else
            {
                currentTarget.TakeDamageFromUnit(this, baseAttackDamage);
            }

        }

        ContinueAttackingOrNot();
    }

    void StopAttackAnimation()
    {
        animatorBase.SetBool("HitLeft", false);
        animatorBase.SetBool("HitRight", false);
    }

    void InitiateAttackAnimation()
    {
        //This is the first unit to have more than 1 attack animation, and it will randomize these attack animations
        attackRandomizer = Random.Range(0, 2);

        switch (attackRandomizer)
        {
            case 0:
                animatorBase.SetBool("HitLeft", true);
                animatorBase.SetBool("HitRight", false);
                break;
            case 1:
                animatorBase.SetBool("HitLeft", false);
                animatorBase.SetBool("HitRight", true);
                break;
        }
    }
    //----------Odin Functions----------
    #endregion
}
