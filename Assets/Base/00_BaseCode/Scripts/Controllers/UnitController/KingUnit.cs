using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingUnit : GameUnitBase
{
    #region Public Variables
    [BoxGroup("King Stats", centerLabel: true)]

    [BoxGroup("King Stats/Soulballl", centerLabel: true)]
    [Header("Soulball Damage")]
    public int soulballDamage = 0;

    [BoxGroup("King Stats/Soulballl")]
    [Space]
    [Header("Projectile Spawn Point")]
    public GameObject projectileSpawnPoint;

    [BoxGroup("King Stats/Soulballl")]
    [Space]
    [Header("Shoot Effect")]
    public ParticleSystem shootEffect;

    [BoxGroup("King Stats/Soulballl")]
    [Space]
    [Header("Soulball Reference (Hittable Projectile)")]
    public UnitHittableProjectile soulball;

    [BoxGroup("King Stats/Rallyzone", centerLabel: true)]
    [Header("Rallyzone Cooldown")]
    public float rallyzoneCooldown = 0;

    [BoxGroup("King Stats/Rallyzone")]
    [Header("Rallyzone linger timer")]
    public float zoneLingerTime = 0;

    [BoxGroup("King Stats/Rallyzone")]
    [Header("Rallyzone reference")]
    public UnitBuffZone rallyzone;

    [BoxGroup("King Stats/Minion", centerLabel: true)]
    [Header("Setup Stun (Phase change) - MUST TURN ON ISBOSSORMINIBOSS FIRST")]
    [OnValueChanged("StunValueHasBeenChosenForPhase2")]
    [ValueDropdown("ChooseStunValueForHpPercentPhase2")]
    [LabelWidth(200)]
    public int stunAtHpPercentPhase_2 = 0;

    [BoxGroup("King Stats/Minion")]
    [ShowIf("IsFirstStunEventFilled")]
    [OnValueChanged("StunValueHasBeenChosenForPhase3")]
    [ValueDropdown("ChooseStunValueForHpPercentPhase3")]
    [LabelWidth(200)]
    public int stunAtHpPercentPhase_3 = 0;

    [BoxGroup("King Stats/Minion")]
    [Space]
    [Header("Minion summon limit (0 will be NO LIMIT)")]
    public int minionLimit = 0;

    [BoxGroup("King Stats/Minion")]
    [Space]
    [Header("Skill cooldown")]
    public float summonMinionCooldown = 0;

    [BoxGroup("King Stats/Minion")]
    [Space]
    [Header("Minion reference")]
    public KingMinionUnit kingMinion;
    #endregion

    #region Private Variables
    float distanceFromCaster = 0;

    bool attackEnemy = false;
    bool summoning = false;

    int minionLane = 0;
    int maxMinionPerSummon = 0;
    int currentMinionCount = 0;
    int summonedMinionCount = 0;

    UnitHittableProjectile tempSoulball;

    List<FieldLane> laneList = new List<FieldLane>();
    List<int> pickedLanes = new List<int>();

    KingMinionUnit tempMinionReference;

    Coroutine buffZoneHandler;
    Coroutine tempSoulballReference;
    Coroutine summoningSequence;
    #endregion

    #region Start, Update
    private void Start()
    {
        InitUnit();
        if (unitMovement != null)
        {
            unitMovement.Init();
        }

        laneList.Clear();
        laneList = new List<FieldLane>(GamePlayController.Instance.gameLevelController.currentLevel.GetMapLanes());

        stunEventWithValue.AddListener(Stunned);
        deathEvent.AddListener(StopAutoRally);

        StartAutoRally();
    }
    #endregion

    #region Functions
    //----------Public----------
    public void ActivateAttackPhase()
    {
        if (!attackEnemy)
        {
            attackEnemy = true;
            AttackThinking();
        }
    }

    public int GetSoulballDamage()
    {
        if (IsAttackUpActive())
        {
            return soulballDamage + Mathf.FloorToInt(soulballDamage * 0.2f);
        }
        else
        {
            return soulballDamage;
        }
    }

    public void MinionDied()
    {
        currentMinionCount--;
        if (currentMinionCount < minionLimit)
        {
            StartSummoningSkill();
        }
    }

    public void Stunned(int currentHpPercent)
    {
        StopAttackAnimation();
        animatorBase.Play("Stunned");

        transform.DOLocalMoveZ(transform.localPosition.z - transform.forward.z * 2.25f, 0.5f)
        .OnComplete(delegate
        {
            PhaseChange(currentHpPercent);
        });

    }
    //----------Private----------
    void AttackThinking()
    {
        if (enemyFound || gateFound)
        {
            DealDamage();
        }
        else
        {
            attackEnemy = false;
        }
    }

    void DealDamage()
    {
        InitiateAttackAnimation();
    }

    void ContinueAttackingOrNot()
    {
        CheckForTarget();
        if (enemyFound || gateFound)
        {
            AttackThinking();
        }
        else
        {
            attackEnemy = false;
            StopAttackAnimation();
        }
    }

    void StartAutoRally()
    {
        buffZoneHandler = StartCoroutine(AutoRally());
    }

    void StopAutoRally()
    {
        StopCoroutine(buffZoneHandler);
    }

    void StartSummoningSkill()
    {
        if (summoningSequence == null)
        {
            summoningSequence = StartCoroutine(SummonMinionSkill());
        }
    }

    void StopSummoningSkill()
    {
        if (summoningSequence != null)
        {
            StopCoroutine(summoningSequence);
        }
        summoningSequence = null;
    }

    IEnumerator AutoRally()
    {
        yield return new WaitForSeconds(rallyzoneCooldown);
        rallyzone.ActivateBuffZone(zoneLingerTime);
        buffZoneHandler = StartCoroutine(AutoRally());
        GameController.Instance.musicManager.PlaySoundEffect(193);
    }

    IEnumerator SummonMinionSkill()
    {
        yield return new WaitForSeconds(summonMinionCooldown);
        InitiateSummoningAnimation();
        GameController.Instance.musicManager.PlaySoundEffect(194);
        summoningSequence = null;
    }

    void PhaseChange(int hpPercent)
    {
        if (hpPercent == stunAtHpPercentPhase_2)
        {
            maxMinionPerSummon = 1;
            StartSummoningSkill();
        }
        else if (hpPercent == stunAtHpPercentPhase_3)
        {
            maxMinionPerSummon = 2;
            StartSummoningSkill();
        }
    }
    
    //----------Animation Functions----------
    public void CastSoulball()
    {
        if (enemyFound || gateFound)
        {
            shootEffect.Play();

            tempSoulball = Instantiate(soulball, projectileSpawnPoint.transform.position, Quaternion.identity);
            tempSoulball.transform.rotation = projectileSpawnPoint.transform.rotation;
            tempSoulball.InitiateProjectile(GetComponent<GameUnitBase>(), gameObject.layer);

            GameController.Instance.musicManager.PlaySoundEffect(191);
        }

        if (!summoning)
        {
            ContinueAttackingOrNot();
        }
    }

    public void SummonMinion()
    {
        pickedLanes.Clear();
        summonedMinionCount = 0;

        if (transform.forward.z < 0)
        {
            distanceFromCaster = transform.position.z + 1.25f;
        }
        else
        {
            distanceFromCaster = transform.position.z - 1.25f;
        }

        if (minionLimit > 0)
        {
            while (summonedMinionCount < maxMinionPerSummon && currentMinionCount < minionLimit)
            {
                minionLane = Random.Range(0, laneList.Count);

                while (pickedLanes.Contains(minionLane))
                {
                    minionLane = Random.Range(0, laneList.Count);
                }

                tempMinionReference = Instantiate(kingMinion, new Vector3(laneList[minionLane].transform.position.x, transform.position.y, distanceFromCaster), Quaternion.identity);
                tempMinionReference.transform.localRotation = transform.localRotation;
                tempMinionReference.RegisterSummoner(this, gameObject.layer, laneList[minionLane]);

                pickedLanes.Add(minionLane);
                currentMinionCount++;
                summonedMinionCount++;
            }
        }
        else
        {
            while (summonedMinionCount < maxMinionPerSummon)
            {
                minionLane = Random.Range(0, laneList.Count);

                while (pickedLanes.Contains(minionLane))
                {
                    minionLane = Random.Range(0, laneList.Count);
                }

                tempMinionReference = Instantiate(kingMinion, new Vector3(laneList[minionLane].transform.position.x, transform.position.y, distanceFromCaster), Quaternion.identity);
                tempMinionReference.transform.localRotation = transform.localRotation;
                tempMinionReference.RegisterSummoner(this, gameObject.layer, laneList[minionLane]);

                pickedLanes.Add(minionLane);
                currentMinionCount++;
                summonedMinionCount++;
            }
        }
        

        StopSummoningAnimation();

        if (minionLimit > 0)
        {
            if (currentMinionCount < minionLimit)
            {
                StartSummoningSkill();
            }
            else
            {
                StopSummoningSkill();
            }
        }
        else
        {
            StartSummoningSkill();
        }
    }

    void StopAttackAnimation()
    {
        if (animatorBase.GetBool("Attack"))
        {
            animatorBase.SetBool("Attack", false);
        }
    }

    void InitiateAttackAnimation()
    {
        if (!animatorBase.GetBool("Attack"))
        {
            animatorBase.SetBool("Attack", true);
        }
    }

    public void StopSummoningAnimation()
    {
        summoning = false;
        animatorBase.SetBool("Summon", false);
        ContinueAttackingOrNot();
    }

    void InitiateSummoningAnimation()
    {
        summoning = true;
        StopAttackAnimation();
        unitMovement.StopMoving();
        animatorBase.SetBool("Summon", true);
    }
    //----------Odin Functions----------
    IEnumerable ChooseStunValueForHpPercentPhase2()
    {
        if (isBossOrMiniboss)
        {
            return new List<int> { 0, 33, 50, 66 };
        }
        else
        {
            return null;
        }
    }

    void StunValueHasBeenChosenForPhase2()
    {
        if (stunAtHpPercentPhase_2 == 0)
        {
            stunAtHpPercentPhase_2 = 0;

            stunAt66PercentHp = false;
            stunAt50PercentHp = false;
            stunAt33PercentHp = false;

            return;
        }

        switch (stunAtHpPercentPhase_2)
        {
            case 66:
                stunAt66PercentHp = true;
                break;
            case 50:
                stunAt50PercentHp = true;
                break;
            case 33:
                stunAt33PercentHp = true;
                break;
        }
    }

    bool IsFirstStunEventFilled()
    {
        if (stunAtHpPercentPhase_2 != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerable ChooseStunValueForHpPercentPhase3()
    {
        if (isBossOrMiniboss && stunAtHpPercentPhase_2 != 0)
        {
            List<int> result = new List<int>() { 0, 33, 50, 66 };

            return result.GetRange(0, result.IndexOf(stunAtHpPercentPhase_2));
        }
        else
        {
            return null;
        }
    }

    void StunValueHasBeenChosenForPhase3()
    {
        if (stunAtHpPercentPhase_3 == 0)
        {
            return;
        }

        switch (stunAtHpPercentPhase_3)
        {
            case 66:
                stunAt66PercentHp = true;
                break;
            case 50:
                stunAt50PercentHp = true;
                break;
            case 33:
                stunAt33PercentHp = true;
                break;
        }
    }
    #endregion
}
