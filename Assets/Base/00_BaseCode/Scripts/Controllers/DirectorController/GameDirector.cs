using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameDirector : SerializedMonoBehaviour
{
    #region Public Variables
    [Header("Units List")]
    [DictionaryDrawerSettings(KeyLabel = "Unit", ValueLabel = "Selectable ?")]
    public Dictionary<GameUnitBase, bool> unitsSpawnList = new Dictionary<GameUnitBase, bool>();

    [Space]
    [Header("Act specific unit list toggle")]
    [OnValueChanged("Act1UnitsToggle")]
    public bool act_1Units = false;
    [OnValueChanged("Act2UnitsToggle")]
    public bool act_2Units = false;

    [Space]
    [Header("Waves Data")]
    public List<List<GameUnitBase>> waveList = new List<List<GameUnitBase>>();
    public List<List<float>> spawnDelay = new List<List<float>>();
    public List<List<int>> waveSpawnRate = new List<List<int>>();
    public List<int> waveWaitRemainingEnemies = new List<int>();
    public List<float> waveWaitSeconds = new List<float>();
    public List<bool> showWarning = new List<bool>();

    [Space]
    [Header("Lanes")]
    public List<FieldLane> lanes = new List<FieldLane>();
    [OnValueChanged("SpawnAIsOn")]
    public bool spawnFromA = false;
    [OnValueChanged("SpawnBIsOn")]
    public bool spawnFromB = false;

    [Space]
    [Header("Current Units on Field")]
    public List<GameUnitBase> spawnedUnits = new List<GameUnitBase>();
    public GameObject unitsHolder;
    #endregion

    #region Private Variables
    [SerializeField] bool thisWaveWarningHasBeenShown = false;

    int currentWave = 0;
    int numberToSpawn = 0;
    int currentUnitId = 0;

    float progressPercentage
    {
        get
        {
            return 1 / ((float)waveList.Count - 1) * currentWave;
        }
    }

    Coroutine waveSpawner;
    Coroutine waitForNewWave;
    #endregion

    #region Start, Update
    #endregion

    #region Functions
    //**** Public ****
    public void InitGameDirector()
    {
        waveSpawner = StartCoroutine(SpawningUnit());
    }

    public void RemoveFallenUnit(GameUnitBase fallenUnit)
    {
        spawnedUnits.Remove(fallenUnit);
    }

    public void StartSpawningSequence()
    {
        if (waitForNewWave != null)
        {
            return;
        }
        else if (waveSpawner == null)
        {
            waveSpawner = StartCoroutine(SpawningUnit());
        }
    }

    public void StopSpawningSequence()
    {
        if (waitForNewWave != null)
        {
            StopCoroutine(waitForNewWave);
            waitForNewWave = null;
        }
        else if (waveSpawner != null)
        {
            StopCoroutine(waveSpawner);
            waveSpawner = null;
        }
    }

    //**** Private ****
    IEnumerator SpawningUnit()
    {
        numberToSpawn = Random.Range(waveSpawnRate[currentWave][0], waveSpawnRate[currentWave][1]);
        GameUnitBase spawnedUnit;
        FieldLane chosenLane;

        List<FieldLane> availabeLanes = new List<FieldLane>(lanes);

        if (showWarning[currentWave] && !thisWaveWarningHasBeenShown)
        {
            thisWaveWarningHasBeenShown = true;

            GamePlayController.Instance.gameScene.warningScreen.transform.localPosition = new Vector3(1200f, 0, 0);
            GamePlayController.Instance.gameScene.warningScreen.gameObject.SetActive(true);

            var temp = GamePlayController.Instance.gameScene.warningScreen.transform.DOLocalMoveX(0, 0.5f)
            .SetEase(Ease.Linear)
            .OnComplete(delegate
            {
                GamePlayController.Instance.gameScene.ActivateIconFlashingAnimation();
                GamePlayController.Instance.gameScene.warningScreen.transform.DOLocalMoveX(-1200f, 0.5f)
                .SetDelay(3f)
                .SetEase(Ease.Linear)
                .OnComplete(delegate
                {
                    GamePlayController.Instance.gameScene.warningScreen.gameObject.SetActive(false);
                })
                .WaitForCompletion();
            });

            yield return temp.WaitForKill();
        }

        for (int i = 1; i <= numberToSpawn; i++)
        {
            if (currentUnitId >= waveList[currentWave].Count)
            {
                break;
            }

            if (waveList[currentWave][currentUnitId].GetComponent<GameUnitBase>().isBossOrMiniboss)
            {
                List<FieldLane> middleLanesOnly = new List<FieldLane>(lanes.Skip(1).Take(lanes.Count - 2));

                chosenLane = middleLanesOnly[Random.Range(0, middleLanesOnly.Count)];
                availabeLanes.Remove(chosenLane);

                spawnedUnit = Instantiate(waveList[currentWave][currentUnitId], unitsHolder.transform);

                if (waveList[currentWave][currentUnitId].GetComponent<DemonUnit>() != null || waveList[currentWave][currentUnitId].GetComponent<KingUnit>() != null)
                {
                    spawnedUnit.transform.localScale = Vector3.one;
                }
                else
                {
                    spawnedUnit.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                }
            }
            else
            {
                chosenLane = availabeLanes[Random.Range(0, availabeLanes.Count)];
                availabeLanes.Remove(chosenLane);

                spawnedUnit = Instantiate(waveList[currentWave][currentUnitId], unitsHolder.transform);
                spawnedUnit.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            }            
            
            if (spawnFromA)
            {
                spawnedUnit.transform.eulerAngles = Vector3.zero;
                spawnedUnit.transform.position = chosenLane.spawnPointA.transform.position;
            }
            else if (spawnFromB)
            {
                spawnedUnit.transform.eulerAngles = new Vector3(0, 180, 0);
                spawnedUnit.transform.position = chosenLane.spawnPointB.transform.position;
            }

            if (spawnedUnit.GetComponent<UnitMovement>() != null)
            {
                if (spawnedUnit.GetComponent<UnitMovement>().zigzag)
                {
                    spawnedUnit.GetComponent<UnitMovement>().SetLane(chosenLane);
                }
            }

            spawnedUnit.SpawnedByDirector(this);

            spawnedUnits.Add(spawnedUnit);
            currentUnitId++;
        }

        yield return new WaitForSeconds(Random.Range(spawnDelay[currentWave][0], spawnDelay[currentWave][1] + Mathf.Epsilon));

        if (currentUnitId >= waveList[currentWave].Count)
        {
            waveSpawner = null;
            waitForNewWave = StartCoroutine(WaitForNewWave());
        }
        else
        {
            waveSpawner = StartCoroutine(SpawningUnit());
        }
    }

    IEnumerator WaitForNewWave()
    {
        if (currentWave + 1 < waveList.Count)
        {
            if (waveWaitSeconds[currentWave] > 0)
            {
                float remainingTimeBeforeNewWave = waveWaitSeconds[currentWave];

                while (spawnedUnits.Count > waveWaitRemainingEnemies[currentWave] && remainingTimeBeforeNewWave > 0)
                {
                    yield return new WaitForSeconds(1);
                    remainingTimeBeforeNewWave--;
                }
            }
            else
            {
                while (spawnedUnits.Count > waveWaitRemainingEnemies[currentWave])
                {
                    yield return new WaitForSeconds(0.1f);
                }
            }

            thisWaveWarningHasBeenShown = false;

            currentWave++;
            currentUnitId = 0;
            GamePlayController.Instance.gameScene.UpdateSpawnProgress(progressPercentage);
            waveSpawner = StartCoroutine(SpawningUnit());
        }
        else
        {
            while (spawnedUnits.Count > waveWaitRemainingEnemies[currentWave])
            {
                yield return new WaitForSeconds(0.1f);
            }

            Debug.LogError("You win");
            //To Do: Let Player win
        }

        waitForNewWave = null;
    }

    //**** Odin's Functions ****
    [BoxGroup("Create Wave", CenterLabel = true)]

    [VerticalGroup("Create Wave/Add Unit")]
    [SerializeField] List<GameUnitBase> waveUnits = new List<GameUnitBase>();

    [Space]
    [VerticalGroup("Create Wave/Add Unit")]
    [ValueDropdown("SelectableUnits")]
    [SerializeField] GameUnitBase chosenUnit;

    [VerticalGroup("Create Wave/Add Unit")]
    [Button(ButtonSizes.Medium)]
    void AddChosenUnit()
    {
        waveUnits.Add(chosenUnit);
    }

    [HorizontalGroup("Create Wave/Spawn Delay", width: 0.5f)]
    [SerializeField] float minSpawnDelay = 0;

    [HorizontalGroup("Create Wave/Spawn Delay", width: 0.5f)]
    [SerializeField] float maxSpawnDelay = 0;

    [HorizontalGroup("Create Wave/Spawn Rate", width: 0.5f)]
    [SerializeField] int minSpawnAtOnce = 0;

    [HorizontalGroup("Create Wave/Spawn Rate", width: 0.5f)]
    [SerializeField] int maxSpawnAtOnce = 0;

    [HorizontalGroup("Create Wave/Wave Transition", width: 0.5f)]
    [SerializeField] int waitRemainingEnemies = 0;

    [HorizontalGroup("Create Wave/Wave Transition", width: 0.5f)]
    [SerializeField] float waitSeconds = 0;

    [HorizontalGroup("Create Wave/Wave Warning")]
    [SerializeField] bool warnPlayer = false;

    [Space]
    [VerticalGroup("Create Wave/Remove Unit")]
    [ValueDropdown("SelectUnitPosition")]
    [SerializeField] int positionNumber = 0;

    [VerticalGroup("Create Wave/Remove Unit")]
    [Button]
    void RemoveUnitAtThisPosition()
    {
        if (waveUnits.Count >= positionNumber && waveUnits.Count > 0)
        {
            waveUnits.RemoveAt(positionNumber);
        }
        
    }

    [Space]
    [BoxGroup("Save Or Load Wave", CenterLabel = true)]

    [VerticalGroup("Save Or Load Wave/Save or Load")]
    [ValueDropdown("SelectPresentingWavePosition")]
    [SerializeField] int presentingWavePosition = 0;

    [VerticalGroup("Save Or Load Wave/Save or Load")]
    [Button]
    void SaveWaveAtThatPosition()
    {
        SaveWave(presentingWavePosition);
    }

    [VerticalGroup("Save Or Load Wave/Save or Load")]
    [Button]
    void LoadWaveAtThatPosition()
    {
        if (presentingWavePosition <= waveList.Count && waveList.Count > 0)
        {
            LoadWave(presentingWavePosition);
            DeleteWave(presentingWavePosition);
        }
    }

    [VerticalGroup("Save Or Load Wave/Save or Load")]
    [Button]
    void ResetAllChanges()
    {
        ResetWaveSettings();
    }

    //**** Wave Editing Functions
    void SaveWave(int pos)
    {
        bool allChecked = false;

        if (minSpawnDelay <= 0)
        {
            Debug.LogError("Min Spawn Delay must not be smaller or equal 0 - Zero delay between each spawn, not gonna happen");
            return;
        }
        else if (minSpawnDelay > maxSpawnDelay)
        {
            Debug.LogError("Max Spawn Delay must be bigger than Min Spawn Delay");
            return;
        }
        else if (minSpawnAtOnce <= 0)
        {
            Debug.LogError("Min Spawn Rate must be higher than 0 - You want Director to spawn 0 unit during its spawning phase for some reason ?");
            return;
        }
        else if (minSpawnAtOnce > maxSpawnAtOnce)
        {
            Debug.LogError("Max Spawn Rate must be higher than Min Spawn Rate");
            return;
        }
        else if (minSpawnAtOnce >= 5 || maxSpawnAtOnce >= 5)
        {
            Debug.LogError("Can only spawn up to 5 units at once");
            return;
        }
        else
        {
            allChecked = true;
        }

        if (allChecked)
        {
            waveList.Insert(pos, new List<GameUnitBase> (waveUnits));

            spawnDelay.Insert(pos, new List<float> { minSpawnDelay, maxSpawnDelay });

            waveSpawnRate.Insert(pos, new List<int> { minSpawnAtOnce, maxSpawnAtOnce });

            waveWaitRemainingEnemies.Insert(pos, waitRemainingEnemies);

            waveWaitSeconds.Insert(pos, waitSeconds);

            showWarning.Insert(pos, warnPlayer);

            ResetWaveSettings();
        }
    }

    void LoadWave(int pos)
    {
        if (pos >= waveList.Count)
        {
            Debug.LogError("This position is for creating new wave, not for loading wave");
            return;
        }

        waveUnits = waveList[pos];

        minSpawnDelay = spawnDelay[pos][0];
        maxSpawnDelay = spawnDelay[pos][1];

        minSpawnAtOnce = waveSpawnRate[pos][0];
        maxSpawnAtOnce = waveSpawnRate[pos][1];

        waitRemainingEnemies = waveWaitRemainingEnemies[pos];
        waitSeconds = waveWaitSeconds[pos];

        warnPlayer = showWarning[pos];
    }

    void DeleteWave(int pos)
    {
        if (pos >= waveList.Count)
        {
            Debug.LogError("Deleting a non-existence wave ???");
            return;
        }

        waveList.RemoveAt(pos);

        spawnDelay.RemoveAt(pos);

        waveSpawnRate.RemoveAt(pos);

        waveWaitRemainingEnemies.RemoveAt(pos);

        waveWaitSeconds.RemoveAt(pos);

        showWarning.RemoveAt(pos);
    }

    void ResetWaveSettings()
    {
        waveUnits.Clear();

        minSpawnDelay = 0;
        maxSpawnDelay = 0;

        minSpawnAtOnce = 0;
        maxSpawnAtOnce = 0;

        waitRemainingEnemies = 0;
        waitSeconds = 0;

        warnPlayer = false;
    }

    //**** Dropdown and Custom functions
    IEnumerable SelectableUnits()
    {
        List<GameUnitBase> units = new List<GameUnitBase>();
        foreach(GameUnitBase unit in unitsSpawnList.Keys)
        {
            if (unitsSpawnList[unit])
            {
                units.Add(unit);
            }
        }

        return units;
    }

    IEnumerable SelectUnitPosition()
    {
        List<int> unitPositions = new List<int>();
        for (int i = 0; i < waveUnits.Count; i++)
        {
            unitPositions.Add(i);
        }

        return unitPositions;
    }

    IEnumerable SelectPresentingWavePosition()
    {
        List<int> presentingWavesPositions = new List<int>();
        for (int i = 0;i < waveList.Count; i++)
        {
            presentingWavesPositions.Add(i);
        }

        presentingWavesPositions.Add(waveList.Count);
        return presentingWavesPositions;
    }

    void Act1UnitsToggle()
    {
        if (act_1Units)
        {
            act_2Units = false;

            foreach (GameUnitBase unit in unitsSpawnList.Keys.ToList())
            {
                if (unit.GetComponent<WarriorUnit>() != null ||
                    unit.GetComponent<RangerUnit>() != null ||
                    unit.GetComponent<MageUnit>() != null ||
                    unit.GetComponent<EnforcerUnit>() != null ||
                    unit.GetComponent<DemonUnit>() != null)
                {
                    unitsSpawnList[unit] = true;
                }
                else
                {
                    unitsSpawnList[unit] = false;
                }
            }
        }
        else
        {
            act_2Units = true;

            foreach (GameUnitBase unit in unitsSpawnList.Keys.ToList())
            {
                if (unit.GetComponent<MonsterUnit>() != null ||
                    unit.GetComponent<HealerUnit>() != null ||
                    unit.GetComponent<BerserkerUnit>() != null ||
                    unit.GetComponent<BloodMageUnit>() != null ||
                    unit.GetComponent<KingUnit>() != null)
                {
                    unitsSpawnList[unit] = true;
                }
                else
                {
                    unitsSpawnList[unit] = false;
                }
            }
        }
        
    }

    void Act2UnitsToggle()
    {
        if (act_2Units)
        {
            act_1Units = false;

            foreach (GameUnitBase unit in unitsSpawnList.Keys.ToList())
            {
                if (unit.GetComponent<MonsterUnit>() != null ||
                    unit.GetComponent<HealerUnit>() != null ||
                    unit.GetComponent<BerserkerUnit>() != null ||
                    unit.GetComponent<BloodMageUnit>() != null ||
                    unit.GetComponent<KingUnit>() != null)
                {
                    unitsSpawnList[unit] = true;
                }
                else
                {
                    unitsSpawnList[unit] = false;
                }
            }
        }
        else
        {
            act_1Units = true;

            foreach (GameUnitBase unit in unitsSpawnList.Keys.ToList())
            {
                if (unit.GetComponent<WarriorUnit>() != null ||
                    unit.GetComponent<RangerUnit>() != null ||
                    unit.GetComponent<MageUnit>() != null ||
                    unit.GetComponent<EnforcerUnit>() != null ||
                    unit.GetComponent<DemonUnit>() != null)
                {
                    unitsSpawnList[unit] = true;
                }
                else
                {
                    unitsSpawnList[unit] = false;
                }
            }
        }
        
    }

    void SpawnAIsOn()
    {
        if (spawnFromA)
        {
            spawnFromB = false;
        }
        else
        {
            spawnFromB = true;
        }
    }

    void SpawnBIsOn()
    {
        if (spawnFromB)
        {
            spawnFromA = false;
        }
        else
        {
            spawnFromA = true;
        }
    }
    #endregion
}
/*
 * Danh sách Layer:
 * 6: Director
 * 7: Player_1
 * 8: Player_2
 * 
 * NOTE: 3 layer này đã được thiết lập để không xử lý vật lý (như va chạm collider) đối với các object có cùng layer với chúng
 */