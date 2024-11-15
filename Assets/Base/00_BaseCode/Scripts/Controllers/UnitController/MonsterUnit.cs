using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MonsterUnit : GameUnitBase
{
    #region Public Variables
    [BoxGroup("Monster Stats", centerLabel: true)]
    [Header("Base Attack Damage")]
    public int baseAttackDamage = 0;
    #endregion

    #region Private Variables
    bool attackEnemy = false;

    GameUnitBase firstTarget;
    GameUnitBase secondTarget;
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
        //For Monster unit, it will attack 2 targets at the same time, an additional effect to this is that the damage will be doubled if there's only 1 target within the attack zone (gate is counted as a target)
        if (gateFound)
        {
            if (targets.Count > 1)
            {
                FindOneTarget(true);
            }
            else if (targets.Count == 1)
            {
                FindOneTarget(false);
            }
        }
        else if (enemyFound)
        {
            if (targets.Count > 1)
            {
                FindTwoTargets();
            }
            else if (targets.Count == 1)
            {
                FindOneTarget(false);
            }
        }
    }

    void FindOneTarget(bool currentlyMoreThanOneTarget)
    {
        if (currentlyMoreThanOneTarget)
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

            firstTarget = closestTarget;
        }
        else
        {
            firstTarget = targets[0];
        }
    }

    void FindTwoTargets()
    {
        float firstDistance = 9999;
        float secondDistance = 9999;

        GameUnitBase firstClosestTarget = null;
        GameUnitBase secondClosestTarget = null;

        foreach(GameUnitBase target in targets)
        {
            if (firstDistance > Vector3.Distance(transform.localPosition, target.transform.localPosition))
            {
                firstClosestTarget = target;
                firstDistance = Vector3.Distance(transform.localPosition, target.transform.localPosition);
            }
        }

        foreach (GameUnitBase target in targets)
        {
            if (secondDistance > Vector3.Distance(transform.localPosition, target.transform.localPosition))
            {
                if (target !=  firstClosestTarget)
                {
                    secondClosestTarget = target;
                    secondDistance = Vector3.Distance(transform.localPosition, target.transform.localPosition);
                }
            }
        }

        firstTarget = firstClosestTarget;
        secondTarget = secondClosestTarget;
    }

    //----------Animation Functions----------
    public void DoubleAttack()
    {
        if (gateFound)
        {
            if (IsAttackUpActive())
            {
                if (firstTarget != null)
                {
                    firstTarget.TakeDamageFromUnit(this, baseAttackDamage + Mathf.FloorToInt(baseAttackDamage * 0.2f));
                    enemyGate.TakeDamage(this, baseAttackDamage + Mathf.FloorToInt(baseAttackDamage * 0.2f));
                }
                else
                {
                    enemyGate.TakeDamage(this, baseAttackDamage * 2 + Mathf.FloorToInt(baseAttackDamage * 2 * 0.2f));
                }
            }
            else
            {
                if (firstTarget != null)
                {
                    firstTarget.TakeDamageFromUnit(this, baseAttackDamage);
                    enemyGate.TakeDamage(this, baseAttackDamage);
                }
                else
                {
                    enemyGate.TakeDamage(this, baseAttackDamage * 2);
                }
            }
        }
        else if (enemyFound)
        {
            if (IsAttackUpActive())
            {
                if (firstTarget != null && secondTarget != null)
                {
                    firstTarget.TakeDamageFromUnit(this, baseAttackDamage + Mathf.FloorToInt(baseAttackDamage * 0.2f));
                    secondTarget.TakeDamageFromUnit(this, baseAttackDamage + Mathf.FloorToInt(baseAttackDamage * 0.2f));
                }
                else if (firstTarget != null)
                {
                    firstTarget.TakeDamageFromUnit(this, baseAttackDamage * 2 + Mathf.FloorToInt(baseAttackDamage * 2 * 0.2f));
                }
                else if (secondTarget != null)
                {
                    secondTarget.TakeDamageFromUnit(this, baseAttackDamage * 2 + Mathf.FloorToInt(baseAttackDamage * 2 * 0.2f));
                }
            }
            else
            {
                if (firstTarget != null && secondTarget != null)
                {
                    firstTarget.TakeDamageFromUnit(this, baseAttackDamage);
                    secondTarget.TakeDamageFromUnit(this, baseAttackDamage);
                }
                else if (firstTarget != null)
                {
                    firstTarget.TakeDamageFromUnit(this, baseAttackDamage * 2);
                }
                else if (secondTarget != null)
                {
                    secondTarget.TakeDamageFromUnit(this, baseAttackDamage * 2);
                }
            }

        }

        firstTarget = null;
        secondTarget = null;

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
    #endregion
}
