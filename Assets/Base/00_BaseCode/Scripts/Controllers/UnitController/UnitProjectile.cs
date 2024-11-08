using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitProjectile : SerializedMonoBehaviour
{
    #region Public Variables
    [Header("IMPORTANT - Projectile life time")]
    public int projectileLifeTime = 0;

    [Space]
    [Header("IMPORTANT - Projectile fly speed")]
    public int projectileSpeed = 0;

    [Space]
    [Header("Projectile owner")]
    public GameUnitBase projectileOwner;

    [Space]
    [Header("Switch to determine which unit the projectile gonna pull data from - Odin will handle this")]
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
    public void SetCollisionLayer(int id)
    {
        gameObject.layer = id;
    }
    //----------Private----------
    void Explode(Vector3 collisionPoint)
    {
        tempExplosionReference = Instantiate(explosion, collisionPoint, Quaternion.identity);
        tempExplosionReference.gameObject.layer = gameObject.layer;

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


    //----------Odin's Functions----------
    [Button]
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
    private void OnCollisionEnter(Collision collision)
    {
        if (doesItExplode)
        {
            if (collision.collider.GetComponent<GameUnitBase>() != null || collision.collider.GetComponent<GateController>() != null)
            {
                Explode(collision.GetContact(0).point);
            }
        }
        else
        {
            GetDamageFromOwner();

            if (collision.collider.GetComponent<GameUnitBase>() != null)
            {
                collision.collider.GetComponent<GameUnitBase>().TakeDamageFromUnit(projectileOwner, damage);
            }
            else if (collision.collider.GetComponent<GateController>() != null)
            {
                collision.collider.GetComponent<GateController>().TakeDamage(projectileOwner, damage);
            }
        }
    }
    #endregion
}
