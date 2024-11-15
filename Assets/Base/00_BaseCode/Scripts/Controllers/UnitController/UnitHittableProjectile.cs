using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHittableProjectile : UnitProjectile
{
    #region Public Variables
    [BoxGroup("Hittable Projectile Stats", centerLabel: true)]
    [Header("Projectile Hp")]
    public int projectileMaxHp = 0;
    [BoxGroup("Hittable Projectile Stats")]
    public int projectileCurrentHp = 0;
    #endregion

    #region Private Variables
    [Header("IMPORTANT - allow bounce or not")]
    [BoxGroup("Hittable Projectile Stats")]
    [SerializeField] bool allowBallBounce = true;
    [BoxGroup("Hittable Projectile Stats")]
    [SerializeField] int bounceCost = 1;
    #endregion

    #region Start, Update
    #endregion

    #region Functions
    //----------Public----------
    public void TakeDamage(int damage)
    {
        if (damage >= projectileCurrentHp)
        {
            HarmlessExplode();
        }
        else
        {
            projectileCurrentHp -= damage;
        }
    }

    public bool GetBouncePermission()
    {
        return allowBallBounce;
    }

    public int GetBounceCost()
    {
        return bounceCost;
    }
    //----------Private----------
    //----------Odin's Functions----------
    #endregion

    #region Collision Functions
    #endregion
}
