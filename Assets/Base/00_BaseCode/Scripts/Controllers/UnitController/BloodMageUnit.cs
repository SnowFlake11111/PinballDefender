using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodMageUnit : GameUnitBase
{
    #region Public Variables
    [BoxGroup("BloodMage Stats", centerLabel: true)]

    [BoxGroup("BloodMage Stats/DarkFireball", centerLabel: true)]
    [Header("Explosion Damage")]
    public int explosionDamage = 0;

    [BoxGroup("BloodMage Stats/DarkFireball")]
    [Space]
    [Header("Shoot effect")]
    public ParticleSystem shootEffect;

    [BoxGroup("BloodMage Stats/DarkFireball")]
    [Space]
    [Header("Projectile spawn point")]
    public GameObject projectileSpawnPoint;

    [BoxGroup("BloodMage Stats/DarkFireball")]
    [Space]
    [Header("Dark Fireball reference")]
    public UnitProjectile darkFireball;

    [BoxGroup("BloodMage Stats/SharepowerZone", centerLabel: true)]
    [Header("Buffzone reference")]
    public UnitBuffZone sharepowerZone;

    [BoxGroup("BloodMage Stats/SharepowerZone")]
    [Space]
    [Header("Buffzone linger time")]
    public float zoneLingerTime = 0;

    [BoxGroup("BloodMage Stats/SharepowerZone")]
    [Space]
    [Header("Self heal amount")]
    public float healAmount = 0;
    #endregion

    #region Private Variables
    bool attackEnemy = false;

    UnitProjectile tempDarkFireball;
    #endregion

    #region Start, Update
    private void Start()
    {
        InitUnit();
        if (unitMovement != null)
        {
            unitMovement.Init();
        }

        sharepowerZone.InitBuffZone(gameObject.layer);
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

    public int GetExplosionDamage()
    {
        if (IsAttackUpActive())
        {
            return explosionDamage + Mathf.FloorToInt(explosionDamage * 0.2f);
        }
        else
        {
            return explosionDamage;
        }
    }

    public void ActivateBloodMagePower()
    {
        if (!IsThisUnitDead())
        {
            GainHealthPercent(healAmount);
            sharepowerZone.ActivateBuffZone(zoneLingerTime);

            GameController.Instance.musicManager.PlaySoundEffect(183);
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
    public void CastDarkFireball()
    {
        if (enemyFound || gateFound)
        {
            shootEffect.Play();

            tempDarkFireball = Instantiate(darkFireball, projectileSpawnPoint.transform.position, Quaternion.identity);
            tempDarkFireball.transform.rotation = projectileSpawnPoint.transform.rotation;
            tempDarkFireball.InitiateProjectile(GetComponent<GameUnitBase>(), gameObject.layer);

            GameController.Instance.musicManager.PlaySoundEffect(181);
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
