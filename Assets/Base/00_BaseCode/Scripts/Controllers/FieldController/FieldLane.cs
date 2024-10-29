using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldLane : MonoBehaviour
{
    #region Public Variables
    [Header("Neighbour Lanes")]
    public FieldLane leftLane;
    public FieldLane rightLane;

    [Space]
    [Header("Unit Spawn Points")]
    public GameObject spawnPointA;
    public GameObject spawnPointB;
    #endregion

    #region Private Variables
    #endregion

    #region Start, Update
    #endregion

    #region Functions
    //**** Public ****
    [Button]
    public void FindNeighbourLanes()
    {
        leftLane = null;
        rightLane = null;

        RaycastHit rightSideResult;
        RaycastHit leftSideResult;

        if (Physics.Raycast(transform.position, Vector3.right, out rightSideResult, Mathf.Infinity, 1 << LayerMask.NameToLayer("FieldLane")))
        {
            if (rightSideResult.collider.GetComponent<FieldLane>() != null)
            {
                rightLane = rightSideResult.collider.GetComponent<FieldLane>();
            }
        }

        if (Physics.Raycast(transform.position, Vector3.left, out leftSideResult, Mathf.Infinity, 1 << LayerMask.NameToLayer("FieldLane")))
        {
            if (leftSideResult.collider.GetComponent<FieldLane>() != null)
            {
                leftLane = leftSideResult.collider.GetComponent<FieldLane>();
            }
        }
    }

    [Button]
    public void GetSpawnPoints()
    {
        foreach (Transform child in transform)
        {
            if (child.transform.localPosition.z > 0)
            {
                spawnPointB = child.gameObject;
            }
            else
            {
                spawnPointA = child.gameObject;
            }
        }
    }

    //For unit script to get the neightbour lanes data from the land that unit standing on
    public List<FieldLane> GetNeighbourLanes()
    {
        List<FieldLane> results = new List<FieldLane>();

        if (leftLane != null)
        {
            results.Add(leftLane);
        }

        if (rightLane != null)
        {
            results.Add(rightLane);
        }

        return results;
    }

    //**** Private ****
    #endregion
}
