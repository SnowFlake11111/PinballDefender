using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUnit : MonoBehaviour
{
    #region Public Variables
    [Header("UNIT HEALTH")]
    public int maxHitPoint = 0;
    public int hitPoint = 0;

    [Space]
    [Header("UNIT OWNER ID")]
    public int ownerId = 0;

    [Space]
    [Header("IMPORTANT - allow bounce or not")]
    [SerializeField] bool allowBallBounce = true;
    [SerializeField] int bounceCost = 1;


    #endregion

    #region Private Variables
    #endregion



    #region Start, Update
    public void Init()
    {
        
    }
    #endregion

    #region Functions
    public void GainHealth(int health)
    {
        if (hitPoint + health >= maxHitPoint)
        {
            hitPoint = maxHitPoint;
        }
        else
        {
            hitPoint += health;
        }
    }

    public void TakeDamage(int damage)
    {
        if (damage > hitPoint)
        {
            Destroy(gameObject);
            //To do: Add function to register the kill for the owner of the ball that killed this unit
        }
        else
        {
            hitPoint -= damage;
        }
    }

    public int GetOwnerId()
    {
        return ownerId;
    }

    public bool GetBouncePermission()
    {
        return allowBallBounce;
    }

    public int GetBounceCost()
    {
        return bounceCost;
    }
    #endregion
}
