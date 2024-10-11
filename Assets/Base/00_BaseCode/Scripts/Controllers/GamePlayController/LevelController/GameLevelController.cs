using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelController : MonoBehaviour
{
    #region Public Variables
    //public List<LevelController> levels = new List<LevelController>();
    //public LevelController currentLevel;
    #endregion

    #region Private Variables
    Quaternion levelLocalRotation;
    #endregion

    private void Start()
    {
        UseProfile.Reshuffle = true;
        GenerateLevel();
    }

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
    }
}
