using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageUnit : GameUnitBase
{
    #region Public Variables
    [BoxGroup("Mage Stats", centerLabel: true)]
    [Header("Projectile Explosion damage")]
    public int explosionDamage = 0;

    [BoxGroup("Mage Stats")]
    [Space]
    [Header("Shoot effect")]
    public ParticleSystem shootEffect;

    [BoxGroup("Mage Stats")]
    [Space]
    [Header("IMPORTANT - Projectile reference")]
    public UnitProjectile energyball;

    [BoxGroup("Mage Stats")]
    [Space]
    [Header("IMPORTANT - Projectile spawn point")]
    public GameObject projectileSpawnPoint;
    #endregion

    #region Private Variables
    bool attackEnemy = false;

    UnitProjectile tempEnergyball;
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

    public int GetExplosionDamage()
    {
        //Note: this function is for the explosion to get the data, if Attack Up buff is active, this is where it take effect
        if (IsAttackUpActive())
        {
            return explosionDamage + Mathf.FloorToInt(explosionDamage * 0.2f);
        }
        else
        {
            return explosionDamage;
        }
        
    }
    //----------Private----------
    void AttackThinking()
    {
        if (enemyFound || gateFound)
        {
            StartShooting();
        }
        else
        {
            attackEnemy = false;
        }
    }

    void StartShooting()
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
    //----------Animation Functions----------
    public void CastEnergyBall()
    {
        if (enemyFound || gateFound)
        {
            shootEffect.Play();

            tempEnergyball = Instantiate(energyball, projectileSpawnPoint.transform.position, Quaternion.identity);
            tempEnergyball.transform.rotation = projectileSpawnPoint.transform.rotation;
            tempEnergyball.InitiateProjectile(GetComponent<GameUnitBase>(), gameObject.layer);
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
}
