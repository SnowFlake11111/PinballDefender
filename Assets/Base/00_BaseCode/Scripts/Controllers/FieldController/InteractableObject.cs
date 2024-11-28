using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    #region Public Variables
    [Header("Shoot effect")]
    public Animator objectAnimator;
    public ParticleSystem shootEffect;

    [Space]
    [Header("Projectile spawn point")]
    public GameObject projectileSpawnPoint;

    [Space]
    [Header("Projectile reference")]
    public FieldProjectile projectile;
    public int projectileDamage = 0;

    [Space]
    [Header("This object's CollisionBox")]
    public BoxCollider objectColliderBox;
    #endregion

    #region Private Variables
    #endregion

    #region Start, Update
    #endregion

    #region Functions
    #endregion

    #region Collision Functions
    #endregion
}
