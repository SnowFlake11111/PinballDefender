using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GameLevelController : MonoBehaviour
{
    #region Public Variables
    [Header("Campaign Levels")]
    public List<StageController> campaignLevels = new List<StageController>();

    [Space]
    [Header("Score Levels")]
    public List<StageController> scoreLevels = new List<StageController>();

    [Space]
    [Header("Defender Levels")]
    public List<StageController> defenderLevels = new List<StageController>();

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
        switch (GameController.Instance.gameModeData.GetChoosenMode())
        {
            case 1:
                currentLevel = Instantiate(campaignLevels[GameController.Instance.gameModeData.GetCampaignStage()]);
                break;
            case 2:
                int stageNumber = 0;
                switch (GameController.Instance.gameModeData.GetScoreStageChoices())
                {
                    case 1:
                        stageNumber = Random.Range(0, 5);
                        break;
                    case 2:
                        stageNumber = Random.Range(5, 10);
                        break;
                    case 3:
                        stageNumber = Random.Range(0, scoreLevels.Count);
                        break;
                }

                currentLevel = Instantiate(scoreLevels[stageNumber]);
                break;
            case 3:
                currentLevel = Instantiate(defenderLevels[Random.Range(0, defenderLevels.Count)]);
                break;
        }

        currentLevel.Init();
    }

    [Button]
    void LoadLevelsFromPrefab()
    {
        campaignLevels.Clear();
        scoreLevels.Clear();
        defenderLevels.Clear();

        string[] campaignLevelsPrefab = AssetDatabase.FindAssets("t: Prefab", new[] { PathPrefabs.CAMPAIGN_LEVELS_FOLDER });
        string[] scoreLevelsPrefab = AssetDatabase.FindAssets("t: Prefab", new[] { PathPrefabs.SCORE_LEVELS_FOLDER });
        string[] defenderLevelsPrefab = AssetDatabase.FindAssets("t: Prefab", new[] { PathPrefabs.DEFENDER_LEVELS_FOLDER });

        string temp;

        if (campaignLevelsPrefab.Length > 0)
        {
            foreach (string campaign in campaignLevelsPrefab)
            {
                temp = AssetDatabase.GUIDToAssetPath(campaign);
                campaignLevels.Add(AssetDatabase.LoadAssetAtPath<StageController>(temp));
            }
        }

        if (scoreLevelsPrefab.Length > 0)
        {
            foreach (string scoreBattle in scoreLevelsPrefab)
            {
                temp = AssetDatabase.GUIDToAssetPath(scoreBattle);
                scoreLevels.Add(AssetDatabase.LoadAssetAtPath<StageController>(temp));
            }
        }

        if (defenderLevelsPrefab.Length > 0)
        {
            foreach (string defenderBattle in defenderLevelsPrefab)
            {
                temp = AssetDatabase.GUIDToAssetPath(defenderBattle);
                defenderLevels.Add(AssetDatabase.LoadAssetAtPath<StageController>(temp));
            }
        }
    }
    #endregion
}
