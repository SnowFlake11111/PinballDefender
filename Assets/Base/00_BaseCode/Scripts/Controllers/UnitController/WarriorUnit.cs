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

    Coroutine attackEnemyAction;
    #endregion

    #region Start, Update
    private void Start()
    {
        //To Do: Turn this into Init() later on
        SetHealth(100);
        realAttackDamage = baseAttackDamage;
    }

    private void Update()
    {
        if (enemyFound)
        {
            //To Do: Attack Script
            AttackEnemy();
        }
    }
    #endregion

    #region Functions
    //----------Public----------
    //----------Private----------
    void AttackEnemy()
    {
        if (!attackEnemy)
        {
            attackEnemyAction = StartCoroutine(AttackThinking());
            attackEnemy = true;
        }
    }

    IEnumerator AttackThinking()
    {
        //Since this unit only has basic attack, just attack -> wait -> repeat
        //To Do: Attack function [done]
        //To Do: Change these Attack function into called during attack animation of the unit
        yield return new WaitForSeconds(attackDelayTimer);
        if (enemyFound)
        {
            GetClosestEnemy();
            currentTarget.TakeDamageFromUnit(this, realAttackDamage);
            CheckForTarget();
            if (enemyFound)
            {
                attackEnemyAction = StartCoroutine(AttackThinking());
            }
            else
            {
                attackEnemy = false;
            }
        }
        else
        {
            attackEnemy = false;
        }
    }

    void GetClosestEnemy()
    {
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
    //----------Odin Functions----------
    #endregion

    #region Collision Functions
    #endregion
}