using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWall : MonoBehaviour
{
    #region Public Variables
    [Header("IMPORTANT - set ball bounce cost")]
    [SerializeField] int bounceCost = 1;
    #endregion

    #region Private Variables
    #endregion

    #region Start, Update
    #endregion

    #region Functions
    //----------Public----------

    //**** Ball Bounce Section ****
    public int GetBounceCost()
    {
        return bounceCost;
    }

    //----------Private----------
    #endregion

    #region Collision Functions
    #endregion
}
