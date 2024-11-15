using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : SerializedMonoBehaviour
{
    #region Public Variables
    [Header("Stage ID")]
    public int stageId = 0;

    [Space]
    [Header("Map Lanes")]
    public List<FieldLane> laneList = new List<FieldLane>();

    [Space]
    [Header("Game Director (can be left empty)")]
    public GameDirector gameDirector;
    #endregion

    #region Private Variables
    #endregion

    #region Functions
    //----------Public----------
    public List<FieldLane> GetMapLanes()
    {
        return laneList;
    }

    public bool DoesStageHaveDirector()
    {
        if (gameDirector != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //----------Private----------
    #endregion
}
