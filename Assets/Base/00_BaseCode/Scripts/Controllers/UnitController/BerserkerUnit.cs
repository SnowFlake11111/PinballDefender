using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkerUnit : GameUnitBase
{
    #region Public Variables
    [BoxGroup("Berserker Stats", centerLabel: true)]
    [Header("Base Attack Damage")]
    public int baseAttackDamage = 0;
    #endregion

    #region Private Variables
    int attackRandomizer = 0;

    bool attackEnemy = false;

    GameUnitBase currentTarget;
    #endregion

    #region Start, Update
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
        //To Do: Handle Attack Up buff [done]

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
        animatorBase.SetBool("Bite", false);
    }

    void InitiateAttackAnimation()
    {
        attackRandomizer = Random.Range(0, 3);

        switch (attackRandomizer)
        {
            case 0:
                animatorBase.SetBool("HitLeft", true);
                animatorBase.SetBool("HitRight", false);
                animatorBase.SetBool("Bite", false);
                break;
            case 1:
                animatorBase.SetBool("HitLeft", false);
                animatorBase.SetBool("HitRight", true);
                animatorBase.SetBool("Bite", false);
                break;
            case 2:
                animatorBase.SetBool("HitLeft", false);
                animatorBase.SetBool("HitRight", false);
                animatorBase.SetBool("Bite", true);
                break;
        }
    }
    //----------Odin Functions----------
    #endregion

    #region Collision Functions
    #endregion
}
