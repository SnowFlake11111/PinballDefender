using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelController : MonoBehaviour
{
    #region Public Variables
    public List<StageController> levels = new List<StageController>();
    public StageController currentLevel;
    #endregion

    #region Private Variables
    #endregion

    #region Start, Update
    #endregion

    #region Functions
    //----------Public----------
    public void Init()
    {
        GenerateLevel();
    }

    public void PlayersCreditsCheck()
    {
        if (currentLevel.player_1 != null)
        {
            currentLevel.player_1.CheckCredits();
        }

        if (currentLevel.player_2 != null)
        {
            currentLevel.player_2.CheckCredits();
        }
    }
    //----------Private----------

    void GenerateLevel()
    {
        //if(UseProfile.CurrentLevel >= levels.Count)
        //{
        //    levelLocalRotation = levels[UseProfile.CurrentLevel % levels.Count].transform.localRotation;

        //    currentLevel = Instantiate(levels[UseProfile.CurrentLevel % levels.Count], Vector3.zero, levelLocalRotation);
        //}
        //else
        //{
        //    levelLocalRotation = levels[UseProfile.CurrentLevel].transform.localRotation;

        //    currentLevel = Instantiate(levels[UseProfile.CurrentLevel], Vector3.zero, levelLocalRotation);
        //}

        //if (UseProfile.Reshuffle)
        //{
        //    currentLevel.cakeController.ApplyColorRandomlyToCakes();
        //    UseProfile.Reshuffle = false;
        //}

        currentLevel.Init();
    }
    #endregion
}
