using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barrack : BaseBox
{
    #region Instance
    private static Barrack instance;
    public static Barrack Setup(bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<Barrack>(PathPrefabs.BARRACK));
            instance.Init();
        }

        instance.InitState();
        return instance;
    }
    #endregion

    #region Public Variables
    [BoxGroup("Unit Section", centerLabel: true)]
    [Header("Unit Buttons")]
    public Dictionary<int, Button> pickUnitBtns = new Dictionary<int, Button>();
    public Dictionary<int, Image> unitPortraits = new Dictionary<int, Image>();
    public Dictionary<int, GameObject> pickUnitBtnHighlight = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> unitIsPicked = new Dictionary<int, GameObject>();

    [BoxGroup("Unit Section")]
    [Space]
    [Header("Remove unit Buttons")]
    public List<Button> removeUnitBtns = new List<Button>();

    [BoxGroup("Unit Section")]
    [Space]
    [Header("Chosen units's Portraits")]
    public List<Image> chosenUnitPortraits = new List<Image>();

    [BoxGroup("Unit Section")]
    [Space]
    [Header("Chosen units's Cost")]
    public List<Text> chosenUnitCosts = new List<Text>();

    [BoxGroup("Unit Section")]
    [Space]
    [Header("Lock image for unit")]
    public Dictionary<int, GameObject> lockNotes = new Dictionary<int, GameObject>();

    [BoxGroup("Unit Section")]
    [Space]
    [Header("Unit's Info")]
    public Dictionary<int, GameObject> unitInfos = new Dictionary<int, GameObject>();

    [BoxGroup("Cosmetics Section", centerLabel: true)]
    [Header("Skin Buttons")]
    public List<Button> skinBtns = new List<Button>();
    [BoxGroup("Cosmetics Section")]
    public List<GameObject> skinBtnHighlights = new List<GameObject>();
    [BoxGroup("Cosmetics Section")]
    public List<GameObject> skinBtnLockNotes = new List<GameObject>();

    [BoxGroup("Cosmetics Section")]
    [Space]
    [Header("Trail Buttons")]
    public List<Button> trailBtns = new List<Button>();
    [BoxGroup("Cosmetics Section")]
    public List<GameObject> trailBtnHighlights = new List<GameObject>();
    [BoxGroup("Cosmetics Section")]
    public List<GameObject> trailBtnLockNotes = new List<GameObject>();

    [Space]
    [Header("Other buttons")]
    public Button confirmBtn;

    #endregion

    #region Private Variables
    List<int> chosenUnitIdList = new List<int>();

    int chosenSkin = -1;
    int chosenTrail = -1;

    int previousInfoIndex = 0;
    int newInfoIndex = 0;

    bool previewIsAlreadySetup = false;
    #endregion

    #region Start, Update
    #endregion

    #region Functions
    //---------Public----------
    public void Init()
    {
        confirmBtn.onClick.AddListener(delegate { ConfirmChoices(); });
        SetupUnitPickingButtons();
        SetupDeselectUnitButtons();
        SetupSkinSelectButtons();
        SetupTrailSelectButtons();
        CheckSavedUnits();

        SetupCosmeticPreview();
    }

    public void InitState()
    {       
        GameProgressCheckerForUnitUnlock();
        CosmeticUnlockChecker();
        SetupCosmeticPreview(true);
    }
    //---------Private----------
    void SetupUnitPickingButtons()
    {
        for (int i = 1; i <= 10; i++)
        {
            int temp = i;
            pickUnitBtns[temp].onClick.AddListener(delegate { PickedAnUnit(temp); });
        }
    }

    void SetupDeselectUnitButtons()
    {
        for (int i = 0; i < removeUnitBtns.Count; i++)
        {
            int temp = i;
            removeUnitBtns[temp].onClick.AddListener(delegate { DeselectAnUnit(temp); });
        }
    }

    void SetupSkinSelectButtons()
    {
        for (int i = 0; i < skinBtns.Count; i++)
        {
            int temp = i;
            skinBtns[temp].onClick.AddListener(delegate { SelectASkin(temp); });
        }
    }

    void SetupTrailSelectButtons()
    {
        for (int i = 0; i < trailBtns.Count; i++)
        {
            int temp = i;
            trailBtns[temp].onClick.AddListener(delegate { SelectATrail(temp); });
        }
    }

    void SetupCosmeticPreview(bool skipCosmeticSetup = false)
    {
        if (!previewIsAlreadySetup)
        {
            HomeController.Instance.ActivatePreviewRoom_1();
            previewIsAlreadySetup = true;
        }

        if (!skipCosmeticSetup)
        {
            SelectASkin(UseProfile.CampaignBallTextureChoice);
            SelectATrail(UseProfile.CampaignBallTrailChoice);
        }
    }

    void CheckSavedUnits()
    {
        chosenUnitIdList.Clear();

        if (UseProfile.CampaignSlot1Unit > 0)
        {
            PickedAnUnit(UseProfile.CampaignSlot1Unit, true);
        }

        if (UseProfile.CampaignSlot2Unit > 0)
        {
            PickedAnUnit(UseProfile.CampaignSlot2Unit, true);
        }

        if (UseProfile.CampaignSlot3Unit > 0)
        {
            PickedAnUnit(UseProfile.CampaignSlot3Unit, true);
        }

        if (UseProfile.CampaignSlot4Unit > 0)
        {
            PickedAnUnit(UseProfile.CampaignSlot4Unit, true);
        }

        if (UseProfile.CampaignSlot5Unit > 0)
        {
            PickedAnUnit(UseProfile.CampaignSlot5Unit, true);
        }
    }

    void GameProgressCheckerForUnitUnlock()
    {
        //1st
        if (UseProfile.LevelProgress < 1)
        {
            pickUnitBtns[1].interactable = false;
            lockNotes[1].SetActive(true);
        }
        else
        {
            pickUnitBtns[1].interactable = true;
            lockNotes[1].SetActive(false);
        }

        //2nd
        if (UseProfile.LevelProgress < 3)
        {
            pickUnitBtns[2].interactable = false;
            lockNotes[2].SetActive(true);
        }
        else
        {
            pickUnitBtns[2].interactable = true;
            lockNotes[2].SetActive(false);
        }

        //3rd
        if (UseProfile.LevelProgress < 5)
        {
            pickUnitBtns[4].interactable = false;
            lockNotes[4].SetActive(true);
        }
        else
        {
            pickUnitBtns[4].interactable = true;
            lockNotes[4].SetActive(false);
        }

        //4th
        if (UseProfile.LevelProgress < 7)
        {
            pickUnitBtns[3].interactable = false;
            lockNotes[3].SetActive(true);
        }
        else
        {
            pickUnitBtns[3].interactable = true;
            lockNotes[3].SetActive(false);
        }

        //5th
        if (UseProfile.LevelProgress < 10)
        {
            pickUnitBtns[5].interactable = false;
            lockNotes[5].SetActive(true);
        }
        else
        {
            pickUnitBtns[5].interactable = true;
            lockNotes[5].SetActive(false);
        }

        //6th
        if (UseProfile.LevelProgress < 11)
        {
            pickUnitBtns[6].interactable = false;
            lockNotes[6].SetActive(true);
        }
        else
        {
            pickUnitBtns[6].interactable = true;
            lockNotes[6].SetActive(false);
        }

        //7th
        if (UseProfile.LevelProgress < 13)
        {
            pickUnitBtns[7].interactable = false;
            lockNotes[7].SetActive(true);
        }
        else
        {
            pickUnitBtns[7].interactable = true;
            lockNotes[7].SetActive(false);
        }

        //8th
        if (UseProfile.LevelProgress < 15)
        {
            pickUnitBtns[9].interactable = false;
            lockNotes[9].SetActive(true);
        }
        else
        {
            pickUnitBtns[9].interactable = true;
            lockNotes[9].SetActive(false);
        }

        //9th
        if (UseProfile.LevelProgress < 17)
        {
            pickUnitBtns[8].interactable = false;
            lockNotes[8].SetActive(true);
        }
        else
        {
            pickUnitBtns[8].interactable = true;
            lockNotes[8].SetActive(false);
        }

        //10th
        if (UseProfile.LevelProgress < 20)
        {
            pickUnitBtns[10].interactable = false;
            lockNotes[10].SetActive(true);
        }
        else
        {
            pickUnitBtns[10].interactable = true;
            lockNotes[10].SetActive(false);
        }
    }

    void CosmeticUnlockChecker()
    {
        //1st
        if (GameController.Instance.gameAchievementController.UnlockSkinStatus(11))
        {
            skinBtnLockNotes[0].SetActive(false);
            skinBtns[10].interactable = true;
        }
        else
        {
            skinBtnLockNotes[0].SetActive(true);
            skinBtns[10].interactable = false;
        }

        //2nd
        if (GameController.Instance.gameAchievementController.UnlockSkinStatus(12))
        {
            skinBtnLockNotes[1].SetActive(false);
            skinBtns[11].interactable = true;
        }
        else
        {
            skinBtnLockNotes[1].SetActive(true);
            skinBtns[11].interactable = false;
        }

        //3rd
        if (GameController.Instance.gameAchievementController.UnlockSkinStatus(13))
        {
            skinBtnLockNotes[2].SetActive(false);
            skinBtns[12].interactable = true;
        }
        else
        {
            skinBtnLockNotes[2].SetActive(true);
            skinBtns[12].interactable = false;
        }

        //4th
        if (GameController.Instance.gameAchievementController.UnlockSkinStatus(14))
        {
            skinBtnLockNotes[3].SetActive(false);
            skinBtns[13].interactable = true;
        }
        else
        {
            skinBtnLockNotes[3].SetActive(true);
            skinBtns[13].interactable = false;
        }

        //5th
        if (GameController.Instance.gameAchievementController.UnlockSkinStatus(15))
        {
            skinBtnLockNotes[4].SetActive(false);
            skinBtns[14].interactable = true;
        }
        else
        {
            skinBtnLockNotes[4].SetActive(true);
            skinBtns[14].interactable = false;
        }

        //6th
        if (GameController.Instance.gameAchievementController.UnlockTrailStatus(11))
        {
            trailBtnLockNotes[0].SetActive(false);
            trailBtns[10].interactable = true;
        }
        else
        {
            trailBtnLockNotes[0].SetActive(true);
            trailBtns[10].interactable = false;
        }

        //7th
        if (GameController.Instance.gameAchievementController.UnlockTrailStatus(12))
        {
            trailBtnLockNotes[1].SetActive(false);
            trailBtns[11].interactable = true;
        }
        else
        {
            trailBtnLockNotes[1].SetActive(true);
            trailBtns[11].interactable = false;
        }

        //8th
        if (GameController.Instance.gameAchievementController.UnlockTrailStatus(13))
        {
            trailBtnLockNotes[2].SetActive(false);
            trailBtns[12].interactable = true;
        }
        else
        {
            trailBtnLockNotes[2].SetActive(true);
            trailBtns[12].interactable = false;
        }

        //9th
        if (GameController.Instance.gameAchievementController.UnlockTrailStatus(14))
        {
            trailBtnLockNotes[3].SetActive(false);
            trailBtns[13].interactable = true;
        }
        else
        {
            trailBtnLockNotes[3].SetActive(true);
            trailBtns[13].interactable = false;
        }

        //10th
        if (GameController.Instance.gameAchievementController.UnlockTrailStatus(15))
        {
            trailBtnLockNotes[4].SetActive(false);
            trailBtns[14].interactable = true;
        }
        else
        {
            trailBtnLockNotes[4].SetActive(true);
            trailBtns[14].interactable = false;
        }

        //11th
        if (GameController.Instance.gameAchievementController.UnlockTrailStatus(16))
        {
            trailBtnLockNotes[5].SetActive(false);
            trailBtns[15].interactable = true;
        }
        else
        {
            trailBtnLockNotes[5].SetActive(true);
            trailBtns[15].interactable = false;
        }
    }

    void ReorganizeChosenUnitList(int whereToBeginReorganizing)
    {
        unitIsPicked[chosenUnitIdList[whereToBeginReorganizing]].SetActive(false);

        for (int i = whereToBeginReorganizing; i < chosenUnitIdList.Count - 1; i++)
        {
            chosenUnitPortraits[i].sprite = chosenUnitPortraits[i + 1].sprite;
            chosenUnitCosts[i].text = chosenUnitCosts[i + 1].text;
        }

        chosenUnitPortraits[chosenUnitIdList.Count - 1].color = new Color(1, 1, 1, 0);
        chosenUnitPortraits[chosenUnitIdList.Count - 1].sprite = null;
        chosenUnitCosts[chosenUnitIdList.Count - 1].text = "";

        chosenUnitIdList.RemoveAt(whereToBeginReorganizing);
    }

    void UpdateUnitInfo(bool skipInfoUpdate = false)
    {
        if (skipInfoUpdate)
        {
            return;
        }

        if (previousInfoIndex != newInfoIndex)
        {
            if (previousInfoIndex > 0)
            {
                unitInfos[previousInfoIndex].SetActive(false);
                pickUnitBtnHighlight[previousInfoIndex].SetActive(false);
            }
            
            unitInfos[newInfoIndex].SetActive(true);            
            previousInfoIndex = newInfoIndex;
        }
    }

    void PickedAnUnit(int unitId, bool skipInfoUpdate = false)
    {
        if (chosenUnitIdList.Count >= 5)
        {
            return;
        }

        if (!chosenUnitIdList.Contains(unitId))
        {
            chosenUnitIdList.Add(unitId);
            unitIsPicked[unitId].SetActive(true);
            chosenUnitPortraits[chosenUnitIdList.Count - 1].color = new Color(1, 1, 1, 1);
            chosenUnitPortraits[chosenUnitIdList.Count - 1].sprite = unitPortraits[unitId].sprite;
            chosenUnitCosts[chosenUnitIdList.Count - 1].text = GameController.Instance.gameModeData.GetUnitCost(unitId).ToString();
        } 

        if (skipInfoUpdate)
        {
            UpdateUnitInfo(true);
        }
        else
        {
            pickUnitBtnHighlight[unitId].SetActive(true);
            newInfoIndex = unitId;
            UpdateUnitInfo();
        }
        
    }

    void DeselectAnUnit(int chosenUnitPosition)
    {
        if (chosenUnitPosition >= chosenUnitIdList.Count)
        {
            Debug.LogError("This slot doesn't have any unit");
            return;
        }

        ReorganizeChosenUnitList(chosenUnitPosition);
    }

    void SelectASkin(int buttonId)
    {
        if (buttonId == chosenSkin)
        {
            return;
        }

        if (chosenSkin > -1)
        {
            skinBtnHighlights[chosenSkin].SetActive(false);
            
        }

        skinBtnHighlights[buttonId].SetActive(true);
        HomeController.Instance.UpdateTextureToBall_1(buttonId);
        chosenSkin = buttonId;
    }

    void SelectATrail(int buttonId)
    {
        if (buttonId == chosenTrail)
        {
            return;
        }

        if (chosenTrail > -1)
        {
            trailBtnHighlights[chosenTrail].SetActive(false);
        }

        trailBtnHighlights[buttonId].SetActive(true);
        HomeController.Instance.UpdateTrailBall_1(buttonId);
        chosenTrail = buttonId;
    }

    void ConfirmChoices()
    {
        switch (chosenUnitIdList.Count)
        {
            case 0:
                UseProfile.CampaignSlot1Unit = 0;
                UseProfile.CampaignSlot2Unit = 0;
                UseProfile.CampaignSlot3Unit = 0;
                UseProfile.CampaignSlot4Unit = 0;
                UseProfile.CampaignSlot5Unit = 0;
                break;
            case 1:
                UseProfile.CampaignSlot1Unit = chosenUnitIdList[0];
                UseProfile.CampaignSlot2Unit = 0;
                UseProfile.CampaignSlot3Unit = 0;
                UseProfile.CampaignSlot4Unit = 0;
                UseProfile.CampaignSlot5Unit = 0;
                break;
            case 2:
                UseProfile.CampaignSlot1Unit = chosenUnitIdList[0];
                UseProfile.CampaignSlot2Unit = chosenUnitIdList[1];
                UseProfile.CampaignSlot3Unit = 0;
                UseProfile.CampaignSlot4Unit = 0;
                UseProfile.CampaignSlot5Unit = 0;
                break;
            case 3:
                UseProfile.CampaignSlot1Unit = chosenUnitIdList[0];
                UseProfile.CampaignSlot2Unit = chosenUnitIdList[1];
                UseProfile.CampaignSlot3Unit = chosenUnitIdList[2];
                UseProfile.CampaignSlot4Unit = 0;
                UseProfile.CampaignSlot5Unit = 0;
                break;
            case 4:
                UseProfile.CampaignSlot1Unit = chosenUnitIdList[0];
                UseProfile.CampaignSlot2Unit = chosenUnitIdList[1];
                UseProfile.CampaignSlot3Unit = chosenUnitIdList[2];
                UseProfile.CampaignSlot4Unit = chosenUnitIdList[3];
                UseProfile.CampaignSlot5Unit = 0;
                break;
            case 5:
                UseProfile.CampaignSlot1Unit = chosenUnitIdList[0];
                UseProfile.CampaignSlot2Unit = chosenUnitIdList[1];
                UseProfile.CampaignSlot3Unit = chosenUnitIdList[2];
                UseProfile.CampaignSlot4Unit = chosenUnitIdList[3];
                UseProfile.CampaignSlot5Unit = chosenUnitIdList[4];
                break;
            default:
                Debug.LogError("Abnormal data, please check");
                break;
        }

        UseProfile.CampaignBallTextureChoice = chosenSkin;
        UseProfile.CampaignBallTrailChoice = chosenTrail;

        HomeController.Instance.DeactivatePreviewRoom_1();
        previewIsAlreadySetup = false;

        Close();
    }
    #endregion
}
