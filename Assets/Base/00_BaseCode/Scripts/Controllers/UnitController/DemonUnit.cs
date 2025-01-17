using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class DemonUnit : GameUnitBase
{
    #region Public Variables
    [BoxGroup("Demon Stats", centerLabel: true)]
    [Header("Demonic Slash Damage")]
    public int baseDamage = 0;
    [BoxGroup("Demon Stats")]
    [Header("Demonic Fireball Damage")]
    public int fireballDamage = 0;

    [BoxGroup("Demon Stats")]
    [Space]
    [Header("Demonic Fireball Cooldown")]
    public float fireballCooldown = 0;

    [BoxGroup("Demon Stats")]
    [Space]
    [Header("IMPORTANT - Fireball reference (this is HITTABLE)")]
    [BoxGroup("Demon Stats")]
    public UnitHittableProjectile demonicFireball;
    [BoxGroup("Demon Stats")]
    public SpawningFlashEffect demonicFireballFlash;
    #endregion

    #region Private Variables
    float distanceFromCaster = 0;

    bool attackEnemy = false;
    bool shootFireball = false;

    int attackRandomizer = 0;
    int firstFireballLane = 0;
    int secondFireballLane = 0;

    SpawningFlashEffect firstFireballSummonFlash;
    SpawningFlashEffect secondFireballSummonFlash;

    UnitHittableProjectile tempFirstFireball;
    UnitHittableProjectile tempSecondFireball;

    List<FieldLane> laneList = new List<FieldLane>();

    Coroutine autoFireball;
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

        stunEventWithoutValue.AddListener(Stunned);
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

    public int GetFireballDamage()
    {
        if (IsAttackUpActive())
        {
            return fireballDamage + Mathf.FloorToInt(fireballDamage * 0.2f);
        }
        else
        {
            return fireballDamage;
        }
    }

    public void Stunned()
    {
        StopAttackAnimation();
        animatorBase.Play("Stunned");

        transform.DOLocalMoveZ(transform.localPosition.z - transform.forward.z * 2.25f, 0.5f)
        .OnComplete(delegate
        {
            StartAutoCastFireball();
            //CheckForTarget();
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

    void StartAutoCastFireball()
    {
        autoFireball = StartCoroutine(AutoFireball());
    }

    IEnumerator AutoFireball()
    {
        yield return new WaitForSeconds(fireballCooldown);
        InitiateCastingAnimation();
    }

    //----------Animation Functions----------
    public void DemonicSlash()
    {
        //This attack will hit everything within the AttackZone, thus this will scan for every targets it detect and deal damage to 'em, including gate if it's within the range as well
        //As the usual way of getting enemies will cause Invalid Operation error (collection was modified error), its necessary to create a seperate List for this function

        List<GameUnitBase> enemiesInRange = new List<GameUnitBase>(targets);

        if (IsAttackUpActive())
        {
            foreach (GameUnitBase enemy in enemiesInRange)
            {
                if (enemy != null)
                {
                    enemy.TakeDamageFromUnit(this, baseDamage + Mathf.FloorToInt(baseDamage * 0.2f));
                }
            }

            if (gateFound && enemyGate != null)
            {
                enemyGate.TakeDamage(this, baseDamage + Mathf.FloorToInt(baseDamage * 0.2f));
            }

            GameController.Instance.musicManager.PlaySoundEffect(141);
        }
        else
        {
            foreach (GameUnitBase enemy in enemiesInRange)
            {
                if (enemy != null)
                {
                    enemy.TakeDamageFromUnit(this, baseDamage);
                }
            }

            if (gateFound && enemyGate != null)
            {
                enemyGate.TakeDamage(this, baseDamage);
            }

            GameController.Instance.musicManager.PlaySoundEffect(141);
        }

        if (!shootFireball)
        {
            ContinueAttackingOrNot();
        }      
    }

    public void DemonicFireball()
    {
        if (transform.forward.z < 0)
        {
            distanceFromCaster = transform.position.z + 1.25f;
        }
        else
        {
            distanceFromCaster = transform.position.z - 1.25f;
        }

        firstFireballLane = Random.Range(0, laneList.Count);
        secondFireballLane = Random.Range(0, laneList.Count);

        while (secondFireballLane == firstFireballLane)
        {
            secondFireballLane = Random.Range(0, laneList.Count);
        }

        firstFireballSummonFlash = Instantiate(demonicFireballFlash, new Vector3(laneList[firstFireballLane].transform.position.x, transform.position.y, distanceFromCaster), Quaternion.identity);
        firstFireballSummonFlash.transform.localRotation = transform.localRotation;
        firstFireballSummonFlash.InitSummonFlashEffect();
        tempFirstFireball = Instantiate(demonicFireball, new Vector3(laneList[firstFireballLane].transform.position.x, transform.position.y, distanceFromCaster), Quaternion.identity);
        tempFirstFireball.transform.localRotation = transform.localRotation;
        tempFirstFireball.InitiateProjectile(GetComponent<GameUnitBase>(), gameObject.layer);
        
        secondFireballSummonFlash = Instantiate(demonicFireballFlash, new Vector3(laneList[secondFireballLane].transform.position.x, transform.position.y, distanceFromCaster), Quaternion.identity);
        secondFireballSummonFlash.transform.localRotation = transform.localRotation;
        secondFireballSummonFlash.InitSummonFlashEffect();
        tempSecondFireball = Instantiate(demonicFireball, new Vector3(laneList[secondFireballLane].transform.position.x, transform.position.y, distanceFromCaster), Quaternion.identity);
        tempSecondFireball.transform.localRotation = transform.localRotation;
        tempSecondFireball.InitiateProjectile(GetComponent<GameUnitBase>(), gameObject.layer);

        GameController.Instance.musicManager.PlaySoundEffect(142);
        StopCastingAnimation();
    }

    void StopAttackAnimation()
    {
        animatorBase.SetBool("HitLeft", false);
        animatorBase.SetBool("HitRight", false);
    }

    void InitiateAttackAnimation()
    {
        //This is the first unit to have more than 1 attack animation, and it will randomize these attack animations
        attackRandomizer = Random.Range(0, 2);

        switch (attackRandomizer)
        {
            case 0:
                animatorBase.SetBool("HitLeft", true);
                animatorBase.SetBool("HitRight", false);
                break;
            case 1:
                animatorBase.SetBool("HitLeft", false);
                animatorBase.SetBool("HitRight", true);
                break;
        }
    }

    public void StopCastingAnimation()
    {
        shootFireball = false;
        animatorBase.SetBool("Fireball", false);
        autoFireball = StartCoroutine(AutoFireball());
        ContinueAttackingOrNot();
    }

    void InitiateCastingAnimation()
    {
        shootFireball = true;
        StopAttackAnimation();
        unitMovement.StopMoving();
        animatorBase.SetBool("Fireball", true);
    }
    //----------Odin Functions----------
    #endregion
}
