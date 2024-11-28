using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldProjectile : MonoBehaviour
{
    #region Public Variables
    [Header("Projectile Damage")]
    public int damage = 0;

    [Space]
    [Header("Explosion check (if yes, the explosion will deal projectile damage)")]
    public bool doesItExplode = false;

    [Space]
    [Header("Projectile Speed")]
    public float projectileSpeed = 0;

    [Space]
    [Header("Explosion effect")]
    public FieldExplosion explosionEffect;
    #endregion

    #region Private Variables
    PlayerController owner;
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
    public void InitiateFieldProjectile(PlayerController playerInitiatedTheShot, int layer)
    {
        owner = playerInitiatedTheShot;
        gameObject.layer = layer;
    }
    //----------Private----------
    void Explode()
    {

    }

    void HarmlessExplode()
    {

    }

    void DealDamageOnContactToUnit(GameUnitBase target)
    {
        target.TakeDamage(owner, damage);
    }
    #endregion

    #region Collision Functions
    private void OnTriggerEnter(Collider other)
    {
        if (doesItExplode)
        {
            if (other.GetComponent<GameUnitBase>() != null || other.GetComponent<GameWall>() != null)
            {
                Explode();
            }
        }
        else
        {
            if (other.GetComponent<GameUnitBase>() != null)
            {
                HarmlessExplode();
                DealDamageOnContactToUnit(other.GetComponent<GameUnitBase>());
            }
            else if (other.GetComponent<GameWall>() != null)
            {
                HarmlessExplode();
            }
        }
    }

    #endregion
}
