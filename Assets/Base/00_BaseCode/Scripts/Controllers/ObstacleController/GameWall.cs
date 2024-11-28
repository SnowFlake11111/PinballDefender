using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWall : MonoBehaviour
{
    #region Public Variables
    [Header("IMPORTANT - set ball bounce cost")]
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
