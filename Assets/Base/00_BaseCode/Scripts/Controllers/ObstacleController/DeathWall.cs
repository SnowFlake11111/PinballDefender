using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour
{
    #region Public Variables
    #endregion

    #region Private Variable
    #endregion

    #region Functions
    //----------Public---------
    public void Init(int layer)
    {
        gameObject.layer = layer;
    }
    //----------Private---------
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
