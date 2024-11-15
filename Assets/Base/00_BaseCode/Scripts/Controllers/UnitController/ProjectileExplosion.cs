using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExplosion : MonoBehaviour
{
    #region Public Variables
    [Header("IMPORTANT - ParticleSystem reference")]
    public ParticleSystem explosionEffect;

    [Space]
    [Header("IMPORTANT - Projectile owner")]
    public GameUnitBase projectileOwner;
    #endregion

    #region Private Variables
    #endregion

    #region Start, Update
    int explosionDamage = 0;

    float lingerTimer = 0;
    #endregion

    #region Functions
    //----------Public----------
    public void InitiateExplosion(GameUnitBase projectileOwner, int layer, int damage)
    {
        this.projectileOwner = projectileOwner;
        gameObject.layer = layer;
        explosionDamage = damage;
        lingerTimer = explosionEffect.main.duration + explosionEffect.main.startLifetime.constantMax;

        StartCoroutine(SelfDestruct());
    }

    public void InitiateHarmlessExplosion()
    {
        if (GetComponent<SphereCollider>() != null)
        {
            GetComponent<SphereCollider>().enabled = false;
        }

        lingerTimer = explosionEffect.main.duration + explosionEffect.main.startLifetime.constantMax;

        StartCoroutine(SelfDestruct());
    }
    //----------Private----------
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(lingerTimer);
        Destroy(gameObject);
    }
    //----------Odin Function----------
    #endregion

    #region Collision Functions
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GameUnitBase>() != null)
        {
            other.GetComponent<GameUnitBase>().TakeDamageFromUnit(projectileOwner, explosionDamage);
        }
        else if (other.GetComponent<GateController>() != null)
        {
            other.GetComponent<GateController>().TakeDamage(projectileOwner, explosionDamage);
        }
    }
    #endregion
}
