using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerUnit : GameUnitBase
{
    #region Public Variables
    [BoxGroup("Ranger Stats", centerLabel: true)]
    [Header("Base Attack Damage")]
    public int baseAttackDamage = 0;

    [BoxGroup("Ranger Stats")]
    [Space]
    [Header("Shoot Effect")]
    public ParticleSystem shootEffect;
    #endregion

    #region Private Variables
    bool attackEnemy = false;

    GameUnitBase currentTarget;
    #endregion

    #region Start, Update
    private void Start()
    {
        InitUnit();
        if (unitMovement != null)
        {
            unitMovement.Init();
        }
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
        //This unit attack logic is basically similar to the Warrior unit, the main difference is that this one attack from a distance rather than up close and personal, but the long range attack is a hitscan so theres no big difference from the Warrior
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
        //Similar to Warrior

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
    public void ShootEnemy()
    {
        if (gateFound && enemyGate != null)
        {
            if (IsAttackUpActive())
            {
                enemyGate.TakeDamage(this, baseAttackDamage + Mathf.FloorToInt(baseAttackDamage * 0.2f));
            }
            else
            {
                enemyGate.TakeDamage(this, baseAttackDamage);
            }

            shootEffect.Play();
            GameController.Instance.musicManager.PlaySoundEffect(111);
        }
        else if (enemyFound && currentTarget != null)
        {
            if (IsAttackUpActive())
            {
                currentTarget.TakeDamageFromUnit(this, baseAttackDamage + Mathf.FloorToInt(baseAttackDamage * 0.2f));
            }
            else
            {
                currentTarget.TakeDamageFromUnit(this, baseAttackDamage);
            }

            shootEffect.Play();
            GameController.Instance.musicManager.PlaySoundEffect(111);
        }

        ContinueAttackingOrNot();
        //To do: Play an effect for when the unit shoot [done]
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
