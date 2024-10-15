using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWall : MonoBehaviour
{
    #region Public Variables
    [Header("IMPORTANT - allow bounce or not")]
    [SerializeField] bool allowBallBounce = true;
    [SerializeField] int bounceCost = 1;

    #endregion

    #region Private Variables
    #endregion

    #region Start, Update
    #endregion

    #region Functions
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
