using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    #region Public Variables
    [Header("IMPORTANT - Movement style")]
    [OnValueChanged("MinimumMovementChoice_Straight")]
    public bool straight = true;
    [OnValueChanged("MinimumMovementChoice_ZigZag")]
    public bool zigzag = false;

    [Header("IMPORTANT - Movement BASE speed")]
    public float movementSpeedBase = 0;

    [HideIf("GetCondition")]
    [LabelWidth(300)]
    public float laneChangeFrequencyInSeconds = 0;
    #endregion

    #region Private Variables
    float realSpeed = 0;

    bool allowedMoving = false;
    #endregion

    #region Start, Update
    private void Start()
    {
        //To Do: set this to Init() later on
        realSpeed = movementSpeedBase;
        allowedMoving = true;
    }
    private void Update()
    {
        if (allowedMoving)
        {
            if (straight)
            {
                MoveStraight();
            }
            else
            {
                MoveZigZag();
            }
        }
    }
    #endregion

    #region Functions
    //----------Public----------
    public void StartMoving()
    {
        if (!allowedMoving)
        {
            allowedMoving = true;
        }
    }

    public void StopMoving()
    {
        if (allowedMoving)
        {
            allowedMoving = false;
        }
    }

    //----------Private----------
    void MoveStraight()
    {
        transform.position += transform.forward * realSpeed * Time.deltaTime;
    }

    void MoveZigZag()
    {
        transform.position += transform.forward * realSpeed * Time.deltaTime;
        //To Do: Finish zigzag movement after lanes on field are finished
    }

    IEnumerator ChangeLane()
    {
        yield return new WaitForSeconds(laneChangeFrequencyInSeconds);

    }

    //----------Odin Functions----------
    void MinimumMovementChoice_Straight()
    {
        if (straight)
        {
            zigzag = false;
        }
        else
        {
            zigzag = true;
        }
    }

    void MinimumMovementChoice_ZigZag()
    {
        if (zigzag)
        {
            straight = false;
        }
        else
        {
            straight = true;
        }
    }

    bool GetCondition()
    {
        return zigzag ? false : true;
    }
    #endregion
}
