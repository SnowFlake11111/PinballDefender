using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerUnit : GameUnitBase
{
    #region Public Variables
    [BoxGroup("Healer Stats", centerLabel: true)]

    [BoxGroup("Healer Stats/Energyball", centerLabel: true)]
    [Header("Projectile damage")]
    public int energyballDamage = 0;

    [Space]
    [BoxGroup("Healer Stats/Energyball")]
    [Header("Shoot effect")]
    public ParticleSystem shootEffect;

    [Space]
    [BoxGroup("Healer Stats/Energyball")]
    [Header("IMPORTANT - Projectile reference")]
    public UnitProjectile energyball;

    [BoxGroup("Healer Stats/Energyball")]
    [Header("IMPORTANT - Projectile spawn point")]
    public GameObject projectileSpawnPoint;

    [Space]
    [BoxGroup("Healer Stats/Healzone", centerLabel: true)]
    [Header("Healzone Cooldown")]
    public float healzoneCooldown = 0;

    [Space]
    [BoxGroup("Healer Stats/Healzone")]
    [Header("Healzone linger time")]
    public float healzoneLingerTime = 0;

    [Space]
    [BoxGroup("Healer Stats/Healzone")]
    [Header("Healzone Controller")]
    public UnitBuffZone healzone;
    #endregion

    #region Private Variables
    bool attackEnemy = false;

    UnitProjectile tempEnergyball;

    Coroutine healZoneHandler;
    #endregion

    #region Start, Update
    private void Start()
    {
        InitUnit();
        if (unitMovement != null)
        {
            unitMovement.Init();
        }

        deathEvent.AddListener(StopAutoSkill);
        StartAutoSkill();

        healzone.InitBuffZone(gameObject.layer);
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

    public int GetEnergyballDamage()
    {
        //Note: this function is for the explosion to get the data, if Attack Up buff is active, this is where it take effect
        if (IsAttackUpActive())
        {
            return energyballDamage + Mathf.FloorToInt(energyballDamage * 0.2f);
        }
        else
        {
            return energyballDamage;
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

    void StartAutoSkill()
    {
        healZoneHandler = StartCoroutine(AutoHealzone());
    }

    void StopAutoSkill()
    {
        StopCoroutine(healZoneHandler);
    }

    IEnumerator AutoHealzone()
    {
        yield return new WaitForSeconds(healzoneCooldown);
        healzone.ActivateBuffZone(healzoneLingerTime);
        healZoneHandler = StartCoroutine(AutoHealzone());
        GameController.Instance.musicManager.PlaySoundEffect(163);
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

            GameController.Instance.musicManager.PlaySoundEffect(161);
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
