using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : SerializedMonoBehaviour
{
    #region Public Variables
    [Header("Units List")]
    [DictionaryDrawerSettings(KeyLabel = "Unit", ValueLabel = "Selectable ?")]
    public Dictionary<GameUnitBase, bool> unitsSpawnList = new Dictionary<GameUnitBase, bool>();

    [Space]
    [Header("Waves Data")]
    public List<List<GameUnitBase>> waveList = new List<List<GameUnitBase>>();
    public List<List<float>> spawnDelay = new List<List<float>>();
    public List<List<int>> waveSpawnRate = new List<List<int>>();

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
    int currentWave = 0;
    int numberToSpawn = 0;
    int currentUnitId = 0;

    Coroutine waveSpawner;
    Coroutine waitForNewWave;
    #endregion

    #region Start, Update
    public void Start()
    {
        waveSpawner = StartCoroutine(SpawningUnit());
    }
    #endregion

    #region Functions
    //**** Public ****
    public void RemoveFallenUnit(GameUnitBase fallenUnit)
    {
        spawnedUnits.Remove(fallenUnit);
    }

    //**** Private ****
    IEnumerator SpawningUnit()
    {
        numberToSpawn = Random.Range(waveSpawnRate[currentWave][0], waveSpawnRate[currentWave][1]);
        GameUnitBase spawnedUnit;
        FieldLane chosenLane;

        List<FieldLane> availabeLanes = new List<FieldLane>(lanes);

        for (int i = 1; i <= numberToSpawn; i++)
        {
            if (currentUnitId >= waveList[currentWave].Count)
            {
                break;
            }
            chosenLane = availabeLanes[Random.Range(0, availabeLanes.Count)];
            availabeLanes.Remove(chosenLane);

            spawnedUnit = Instantiate(waveList[currentWave][currentUnitId], unitsHolder.transform);
            spawnedUnit.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            
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
            waitForNewWave = StartCoroutine(WaitForNewWave());
        }
        else
        {
            waveSpawner = StartCoroutine(SpawningUnit());
        }
    }

    IEnumerator WaitForNewWave()
    {
        while(spawnedUnits.Count > 0)
        {
            yield return new WaitForSeconds(0.5f);
        }

        if (currentWave + 1 >= waveList.Count)
        {
            //To Do: Let Player win
        }
        else
        {
            currentWave++;
            currentUnitId = 0;
            waveSpawner = StartCoroutine(SpawningUnit());
        }
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
    void SaveWaveAtThatPosition()
    {
        SaveWave(presentingWavePosition);
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

            ResetWaveSettings();
        }
    }

    void LoadWave(int pos)
    {
        waveUnits = waveList[pos];

        minSpawnDelay = spawnDelay[pos][0];
        maxSpawnDelay = spawnDelay[pos][1];

        minSpawnAtOnce = waveSpawnRate[pos][0];
        maxSpawnAtOnce = waveSpawnRate[pos][1];
    }

    void DeleteWave(int pos)
    {
        waveList.RemoveAt(pos);

        spawnDelay.RemoveAt(pos);

        waveSpawnRate.RemoveAt(pos);
    }

    void ResetWaveSettings()
    {
        waveUnits.Clear();

        minSpawnDelay = 0;
        maxSpawnDelay = 0;

        minSpawnAtOnce = 0;
        maxSpawnAtOnce = 0;
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