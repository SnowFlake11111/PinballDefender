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
    [Header("Current Target")]
    public GameUnitBase currentTarget;
    #endregion

    #region Private Variables
    bool attackEnemy = false;
    #endregion

    #region Start, Update
    #endregion

    #region Functions
    //----------Public----------
    public void ActivateAttackPhase()
    {
        if (!attackEnemy)
        {
            AttackThinking();
            attackEnemy = true;
        }
    }
    //----------Private----------
    void AttackThinking()
    {
        //Since this unit only has basic attack, just attack -> wait -> repeat
        //To Do: Attack function [done]
        //To Do: Change these Attack function into called during attack animation of the unit [done]
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
    //----------Animation Functions----------
    public void AttackEnemy()
    {
        //To Do: Handle Attack Up buff

        if (gateFound)
        {
            enemyGate.TakeDamage(this, baseAttackDamage);
        }
        else if (enemyFound)
        {
            currentTarget.TakeDamageFromUnit(this, baseAttackDamage);
        }

        ContinueAttackingOrNot();
    }

    void StopAttackAnimation()
    {
        if (animatorBase.GetBool("Attack"))
        {
            animatorBase.SetBool("Attack", false);
        }
        
    }

    void InitiateAttackAnimation()
    {
        if (!animatorBase.GetBool("Attack"))
        {
            animatorBase.SetBool("Attack", true);
        }
    }
    //----------Odin Functions----------
    #endregion

    #region Collision Functions
    #endregion
}
