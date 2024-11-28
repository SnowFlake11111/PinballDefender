using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class KingMinionUnit : GameUnitBase
{
    #region Public Variables
    [BoxGroup("King Minion Stats", centerLabel: true)]
    [Header("Base Attack Damage")]
    public int baseAttackDamage = 0;
    #endregion

    #region Private Variables
    bool attackEnemy = false;

    KingUnit summoner;
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

    public void RegisterSummoner(KingUnit summoner, int layer, FieldLane currentLane)
    {
        this.summoner = summoner;
        gameObject.layer = layer;

        InitUnit();

        unitMovement.SetLane(currentLane);

        deathEvent.AddListener(ReportDeathToSummoner);
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

    void ReportDeathToSummoner()
    {
        if (summoner != null)
        {
            summoner.MinionDied();
        }
    }
    //----------Animation Functions----------
    public void AttackEnemy()
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

    public void FinishiSpawnAnimation()
    {
        if (unitMovement != null)
        {
            unitMovement.Init();
        }
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
