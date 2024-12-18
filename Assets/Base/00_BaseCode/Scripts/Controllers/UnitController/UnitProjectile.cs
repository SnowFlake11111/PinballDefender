using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitProjectile : SerializedMonoBehaviour
{
    #region Public Variables
    [Header("IMPORTANT - Projectile life time")]
    public float projectileLifeTime = 0;

    [Space]
    [Header("IMPORTANT - Projectile fly speed")]
    public float projectileSpeed = 0;

    [Space]
    public int soundEffectId = 0;

    [Space]
    [Header("Has additional effect for caster ? (Currently Bloodmage only)")]
    public bool additionalEffects = false;

    [Space]
    [Header("Projectile owner")]
    public GameUnitBase projectileOwner;

    [Space]
    [Header("Switch to determine which unit the projectile gonna pull data from")]
    [Title("The unit will initiate the check itself")]
    public MageUnit mage;
    public DemonUnit demon;
    public HealerUnit healer;
    public BloodMageUnit bloodMage;
    public KingUnit king;

    [Space]
    [Header("If the projectile explode and deal damage, check this box")]
    public bool doesItExplode = false;

    [Space]
    [Header("IMPORTANT - Explosion reference must not be empty")]
    public ProjectileExplosion explosion;
    #endregion

    #region Private Variables
    int damage = 0;
    ProjectileExplosion tempExplosionReference;
    Coroutine selfDestructSequence;
    #endregion

    #region Start, Update
    private void Update()
    {
        if (projectileSpeed > 0)
        {
            transform.position += transform.forward * projectileSpeed * Time.deltaTime;
        }
    }
    #endregion

    #region Functions
    //----------Public----------
    public void InitiateProjectile(GameUnitBase projectileOwner, int layer)
    {
        this.projectileOwner = projectileOwner;
        gameObject.layer = layer;
        UnitChecker();
        selfDestructSequence = StartCoroutine(SelfDestruct());
    }
    //----------Private, Protected----------
    protected void Explode()
    {
        tempExplosionReference = Instantiate(explosion, transform.position, Quaternion.identity);
        tempExplosionReference.InitiateExplosion(projectileOwner, gameObject.layer, damage);

        StopCoroutine(selfDestructSequence);
        GameController.Instance.musicManager.PlaySoundEffect(soundEffectId);
        Destroy(gameObject);
    }

    protected void HarmlessExplode()
    {
        tempExplosionReference = Instantiate(explosion, transform.position, Quaternion.identity);
        tempExplosionReference.InitiateHarmlessExplosion();

        StopCoroutine(selfDestructSequence);
        GameController.Instance.musicManager.PlaySoundEffect(soundEffectId);
        Destroy(gameObject);
    }

    void DealDamageOnContactToUnit(GameUnitBase target)
    {
        target.TakeDamageFromUnit(projectileOwner, damage);

        Destroy(gameObject);
    }

    void DealDamageOnContactToGate(GateController gate)
    {
        gate.TakeDamage(projectileOwner, damage);

        Destroy(gameObject);
    }

    void GetDamageFromOwner()
    {
        if (mage != null)
        {
            damage = mage.GetExplosionDamage();
        }
        else if (demon != null)
        {
            damage = demon.GetFireballDamage();
        }
        else if (healer != null)
        {
            damage = healer.GetEnergyballDamage();
        }
        else if (bloodMage != null)
        {
            damage = bloodMage.GetExplosionDamage();
        }
        else if (king != null)
        {
            damage = king.GetSoulballDamage();
        }
    }

    void AdditionalEffectOnHit()
    {
        if (mage != null)
        {

        }
        else if (demon != null)
        {

        }
        else if (healer != null)
        {

        }
        else if (bloodMage != null)
        {
            bloodMage.ActivateBloodMagePower();
        }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(projectileLifeTime);
        HarmlessExplode();
        Destroy(gameObject);
    }

    //----------Odin's Functions----------
    void UnitChecker()
    {
        if (projectileOwner == null)
        {
            Debug.LogError("This projectile doesn't have an owner");
            return;
        }

        mage = null;
        demon = null;
        healer = null;
        bloodMage = null;

        if (projectileOwner.GetComponent<MageUnit>() != null)
        {
            mage = projectileOwner.GetComponent<MageUnit>();
        }
        else if (projectileOwner.GetComponent<DemonUnit>() != null)
        {
            demon = projectileOwner.GetComponent<DemonUnit>();
        }
        else if (projectileOwner.GetComponent<HealerUnit>() != null)
        {
            healer = projectileOwner.GetComponent<HealerUnit>();
        }
        else if (projectileOwner.GetComponent<BloodMageUnit>() != null)
        {
            bloodMage = projectileOwner.GetComponent<BloodMageUnit>();
        }
        else if (projectileOwner.GetComponent<KingUnit>() != null)
        {
            king = projectileOwner.GetComponent<KingUnit>();
        }
        else
        {
            Debug.LogError("Unknown unit");
        }
    }

    #endregion

    #region Collision Functions
    private void OnTriggerEnter(Collider other)
    {
        if (doesItExplode)
        {
            if (other.GetComponent<GameUnitBase>() != null || other.GetComponent<GateController>() != null)
            {
                if (additionalEffects)
                {
                    AdditionalEffectOnHit();
                }

                GetDamageFromOwner();
                Explode();
            }
        }
        else
        {
            GetDamageFromOwner();

            if (other.GetComponent<GameUnitBase>() != null)
            {
                if (additionalEffects)
                {
                    AdditionalEffectOnHit();
                }

                HarmlessExplode();
                DealDamageOnContactToUnit(other.GetComponent<GameUnitBase>());
            }
            else if (other.GetComponent<GateController>() != null)
            {
                if (additionalEffects)
                {
                    AdditionalEffectOnHit();
                }

                HarmlessExplode();
                DealDamageOnContactToGate(other.GetComponent<GateController>());
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (doesItExplode)
        {
            if (collision.collider.GetComponent<GameUnitBase>() != null || collision.collider.GetComponent<GateController>() != null)
            {
                if (additionalEffects)
                {
                    AdditionalEffectOnHit();
                }
                
                GetDamageFromOwner();
                Explode();
            }
        }
        else
        {
            if (collision.collider.GetComponent<GameUnitBase>() != null)
            {
                if (additionalEffects)
                {
                    AdditionalEffectOnHit();
                }

                GetDamageFromOwner();

                HarmlessExplode();
                DealDamageOnContactToUnit(collision.collider.GetComponent<GameUnitBase>());
            }
            else if (collision.collider.GetComponent<GateController>() != null)
            {
                if (additionalEffects)
                {
                    AdditionalEffectOnHit();
                }

                GetDamageFromOwner();

                HarmlessExplode();
                DealDamageOnContactToGate(collision.collider.GetComponent<GateController>());
            }
        }
    }
    #endregion
}
