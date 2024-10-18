using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    #region Public Variables
    [Header("EXTREMELY IMPORTANT - Assign the unit of this attackzone")]
    public GameUnitBase unitBase;
    #endregion

    #region Private Variables
    #endregion

    #region Start, Update
    #endregion

    #region Functions
    #endregion

    #region Collision Functions
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GameUnitBase>() != null)
        {
            unitBase.RegisterTarget(other.GetComponent<GameUnitBase>());
            //Debug.LogError(unitBase.name + " Found a target");
        }
        else if (other.GetComponent<GateController>() != null)
        {
            unitBase.RegisterEnemyGate(other.GetComponent<GateController>());
        }

        //To Do: Register Gate for unit [done]
    }

    private void OnTriggerExit(Collider other)
    {
        //To Do: This one is likely to be called on Gate alone when player pushes enemies away
        if (other.GetComponent<GameUnitBase>() != null)
        {
            unitBase.RemoveTarget(other.GetComponent<GameUnitBase>());
            //Debug.LogError(unitBase.name + " Found a target");
        }
        else if (other.GetComponent<GateController>() != null)
        {
            unitBase.RemoveEnemyGate();
        }
    }
    #endregion
}
