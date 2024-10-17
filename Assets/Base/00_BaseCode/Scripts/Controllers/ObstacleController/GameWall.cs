using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWall : MonoBehaviour
{
    #region Public Variables
    [Header("IMPORTANT - allow bounce or not")]
    [SerializeField] bool allowBallBounce = true;
    [SerializeField] int bounceCost = 1;

    [Space]
    [Header("Will this wall delete unit upon detecting one")]
    [SerializeField] bool endOfFieldWall = false;
    #endregion

    #region Private Variables
    #endregion

    #region Start, Update
    #endregion

    #region Functions
    //----------Public----------

    //**** Ball Bounce Section ****
    public bool GetBouncePermission()
    {
        return allowBallBounce;
    }

    public int GetBounceCost()
    {
        return bounceCost;
    }

    //----------Private----------
    #endregion

    #region Collision Functions
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GameUnitBase>() != null)
        {
            other.GetComponent<GameUnitBase>().InstantDeath();
        }
    }
    #endregion
}
