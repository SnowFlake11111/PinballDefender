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
    [Header("Projectile owner")]
    public GameUnitBase projectileOwner;

    [Space]
    [Header("Switch to determine which unit the projectile gonna pull data from")]
    [Title("The unit will initiate the check itself")]
    public MageUnit mage;
    public DemonUnit demon;
    public HealerUnit healer;
    public BloodMageUnit bloodMage;

    [Space]
    [Header("If the projectile explode, check this box")]
    public bool doesItExplode = false;

    [Space]
    [HideIf("DoesItExplode")]
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
    //----------Private----------
    void Explode(Vector3 collisionPoint)
    {
        tempExplosionReference = Instantiate(explosion, collisionPoint, Quaternion.identity);
        tempExplosionReference.InitiateExplosion(projectileOwner, gameObject.layer, damage);

        StopCoroutine(selfDestructSequence);
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

        }
        else if (healer != null)
        {

        }
        else if (bloodMage != null)
        {

        }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(projectileLifeTime);
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
        else
        {
            Debug.LogError("Unknown unit");
        }
    }

    bool DoesItExplode()
    {
        if (doesItExplode)
        {
            return false;
        }
        else
        {
            explosion = null;
            return true;
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
                GetDamageFromOwner();
                Explode(transform.position);
            }
        }
        else
        {
            GetDamageFromOwner();

            if (other.GetComponent<GameUnitBase>() != null)
            {
                DealDamageOnContactToUnit(other.GetComponent<GameUnitBase>());
            }
            else if (other.GetComponent<GateController>() != null)
            {
                DealDamageOnContactToGate(other.GetComponent<GateController>());
            }
        }
    }
    #endregion
}
