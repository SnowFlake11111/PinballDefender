using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorUnit : GameUnitBase
{
    #region Public Variables
    [Space]
    [Header("Base Attack Damage")]
    public int baseAttackDamage = 0;
    
    [Space]
    [Header("Attack Speed")]
    public float attackDelayTimer = 0;

    [Space]
    [Header("Current Target")]
    public GameUnitBase currentTarget;
    #endregion

    #region Private Variables
    int realAttackDamage = 0;

    bool attackEnemy = false;
    bool attackGate = false;

    [SerializeField] GateController gate;

    Coroutine attackEnemyAction;
    #endregion

    #region Start, Update
    private void Start()
    {
        //SetHealth(100);
        realAttackDamage = baseAttackDamage;
    }

    private void Update()
    {
        if (enemyFound || gateFound)
        {
            //To Do: Attack Script
            ActivateAttackPhase();
        }
    }
    #endregion

    #region Functions
    //----------Public----------
    public void ContinueAttackingOrNot()
    {
        CheckForTarget();
        if (enemyFound || gateFound)
        {
            AttackThinking();
        }
        else
        {
            attackEnemy = false;
            animatorBase.SetBool("Attack", false);
        }
    }

    //----------Private----------
    void ActivateAttackPhase()
    {
        if (!attackEnemy)
        {
            AttackThinking();
            attackEnemy = true;
        }
    }

    void AttackThinking()
    {
        //Since this unit only has basic attack, just attack -> wait -> repeat
        //To Do: Attack function [done]
        //To Do: Change these Attack function into called during attack animation of the unit
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
    //----------Animation Functions----------
    public void AttackEnemy()
    {
        if (gateFound)
        {
            enemyGate.TakeDamage(this, realAttackDamage);
        }
        else
        {
            currentTarget.TakeDamageFromUnit(this, realAttackDamage);
        }

        ContinueAttackingOrNot();
        //ContinueAttackingOrNot();
    }

    void InitiateAttackAnimation()
    {
        animatorBase.SetBool("Attack", true);
    }
    //----------Odin Functions----------
    #endregion

    #region Collision Functions
    #endregion
}
