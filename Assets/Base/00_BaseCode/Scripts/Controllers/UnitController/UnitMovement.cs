using DG.Tweening;
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

    [Space]
    [Header("IMPORTANT - Movement BASE speed")]
    public float movementSpeedBase = 0;

    [Space]
    [Header("IMPORTANT - The unit")]
    public GameUnitBase controllingUnit;
    public Animator animatorBase;

    [Space]
    [HideIf("GetCondition")]
    [LabelWidth(300)]
    public float laneChangeFrequencyInSeconds = 0;
    #endregion

    #region Private Variables
    float realSpeed = 0;

    [SerializeField]bool allowedMoving = true;

    FieldLane currentLane;
    FieldLane newLane;

    List<FieldLane> possibleLanes = new List<FieldLane>();

    Coroutine changeLaneThinking;
    #endregion

    #region Start, Update

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
    public void Init()
    {
        realSpeed = movementSpeedBase;
        if (allowedMoving)
        {
            animatorBase.SetBool("Move", true);
        }
        else
        {
            animatorBase.SetBool("Move", false);
        }

        if (zigzag)
        {
            changeLaneThinking = StartCoroutine(ChangeLane());
        }
    }

    public void StartMoving()
    {
        if (!allowedMoving)
        {
            allowedMoving = true;
            animatorBase.SetBool("Move", true);
        }
    }

    public void StopMoving()
    {
        if (allowedMoving)
        {
            allowedMoving = false;
            animatorBase.SetBool("Move", false);
        }
    }

    public void DeathState()
    {
        allowedMoving = false;
    }

    public void SetLane(FieldLane lane)
    {
        currentLane = lane;
        possibleLanes = lane.GetNeighbourLanes();
    }
    //----------Private----------
    void MoveStraight()
    {
        transform.position += transform.forward * realSpeed * Time.deltaTime;
    }

    void MoveZigZag()
    {
        transform.position += transform.forward * realSpeed * Time.deltaTime;
        //To Do: Finish zigzag movement after lanes on field are finished (done)
    }

    IEnumerator ChangeLane()
    {
        yield return new WaitForSeconds(laneChangeFrequencyInSeconds);
        newLane = possibleLanes[Random.Range(0, possibleLanes.Count)];

        transform.DOMoveX(newLane.transform.position.x, 0.5f)
            .OnComplete(delegate
            {
                currentLane = newLane;
                possibleLanes = newLane.GetNeighbourLanes();
                changeLaneThinking = StartCoroutine(ChangeLane());
            });
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
