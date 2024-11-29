using DG.Tweening;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SerializedMonoBehaviour
{
    #region Public Variables
    [Header("IMPORTANT - Used to identify player's side")]
    [ValueDropdown("PlayerNameList")]
    [OnValueChanged("SetPlayerId")]
    public string playerName;
    [ValueDropdown("PlayerIdList")]
    [OnValueChanged("SetPlayerName")]
    public int playerId = 0;

    [Space]
    [Header("Allow Spawning units ?")]
    public bool allowUnitSpawning = false;

    [Space]
    [Header("Gun model")]
    public GameObject gunModel;

    [Space]
    [Header("MISC REFERENCES")]
    public GameObject playerHolder;
    public GameObject ballSpawnPoint;

    public Ball ball;
    #endregion

    #region Private Variables
    int playerDamage = 10;
    int playerScore = 0;
    int maxAmmo = 5;
    int currentAmmo = 5;
    int currentCredits = 0;
    int maxCredits = 300;
    int creditsGainedOverTime = 25;

    float resumeRotationTimer = 0.5f;

    Coroutine autoResumeRotation;

    Tweener rotateGun;
    Tweener gunShootEffect;
    Tweener finishGunShootEffect;

    Ball tempBallRef;

    Vector3 ogModelPosition;
    Vector3 ogRotation;

    GameUnitBase tempSpawnedUnitReference;
    #endregion

    #region Buffs Timer
    float doubleDamageBuffTimer = 0;
    #endregion

    #region Start, Update
    private void OnDestroy()
    {
        gunShootEffect.Kill();
    }
    #endregion

    #region Functions
    //----------Public----------
    public void Init()
    {
        RotateHandler();

        ogRotation = gameObject.transform.localEulerAngles;
        ogModelPosition = gunModel.transform.localPosition;
        gameObject.layer = playerId;
    }

    public void Shoot()
    {
        tempBallRef = Instantiate(ball, ballSpawnPoint.transform.position, Quaternion.identity, playerHolder.transform);
        tempBallRef.transform.localScale = Vector3.one / 2;
        tempBallRef.transform.localEulerAngles = ballSpawnPoint.transform.eulerAngles;

        tempBallRef.ShootBall(ballSpawnPoint.transform.forward);
        tempBallRef.SetOwner(playerId, this);

        //Debug.LogError(playerId);

        if (doubleDamageBuffTimer > 0)
        {
            tempBallRef.SetDamage(playerDamage * 2);
        }
        else
        {
            tempBallRef.SetDamage(playerDamage);
        }

        tempBallRef.SetBounceLimit(5);

        ShootEffect();

        //Việc giữ các quả bóng trong một list là để đảm bảo việc giữ cho các va chạm giữa các quả bóng của người chơi sẽ bỏ qua va chạm đối với quân cùng phe
        //UPDATE: Đã đổi sang sử dụng Collision Layer
    }

    public void TemporaryStopRotation()
    {
        if (autoResumeRotation != null)
        {
            StopCoroutine(autoResumeRotation);
        }

        autoResumeRotation = StartCoroutine(ResumeRotationTimer());
    }

    //To Do: Create a function that handle spawning unit

    public void SpawnAnUnit(GameUnitBase unitToSpawn, GameObject spawnPoint, FieldLane spawnLane, GameObject unitHolder)
    {
        tempSpawnedUnitReference = Instantiate(unitToSpawn, unitHolder.transform);
        tempSpawnedUnitReference.transform.localRotation = spawnPoint.transform.localRotation;
        tempSpawnedUnitReference.transform.position = spawnPoint.transform.position;
        tempSpawnedUnitReference.SpawnedByPlayer(this);

        if (tempSpawnedUnitReference.isBossOrMiniboss)
        {
            if (tempSpawnedUnitReference.GetComponent<DemonUnit>() != null || tempSpawnedUnitReference.GetComponent<KingUnit>() != null)
            {
                tempSpawnedUnitReference.transform.localScale = Vector3.one;
            }
            else
            {
                tempSpawnedUnitReference.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            }
        }
        else
        {
            tempSpawnedUnitReference.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        }

        if (tempSpawnedUnitReference.GetComponent<UnitMovement>() != null)
        {
            if (tempSpawnedUnitReference.GetComponent<UnitMovement>().zigzag)
            {
                tempSpawnedUnitReference.GetComponent<UnitMovement>().SetLane(spawnLane);
            }
        }

        currentCredits -= tempSpawnedUnitReference.spawnCost;

        RequestCreditsGain();
        RequestUpdateCreditsNumber();
    }

    public void GainScore(int score)
    {
        playerScore += score;
        //To do: add dotween to animate score value going up/down (prob never go down)
    }

    public int GetScore()
    {
        return playerScore;
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public int GetMaxAmmo()
    {
        return maxAmmo;
    }

    public int GetCurrentCredits()
    {
        return currentCredits;
    }

    public int GetMaxCredits()
    {
        return maxCredits;
    }

    public void CheckCredits()
    {
        RequestCreditsGain();
    }

    public void AmmoReloaded()
    {
        currentAmmo = maxAmmo;
        RequestUpdateAmmoNumber();
    }

    public void CreditsGainedFromKill(int amount)
    {
        if (currentCredits + amount > maxCredits)
        {
            currentCredits = maxCredits;
        }
        else
        {
            currentCredits += amount;
        }

        RequestUpdateCreditsNumber();
    }

    public void CreditsGainedOverTime(int modeId)
    {
        switch (modeId)
        {
            case 1:
                if (currentCredits + creditsGainedOverTime > maxCredits)
                {
                    currentCredits = maxCredits;
                }
                else
                {
                    currentCredits += creditsGainedOverTime;
                }
                break;
            case 2:
                if (currentCredits + creditsGainedOverTime > maxCredits)
                {
                    currentCredits = maxCredits;
                }
                else
                {
                    currentCredits += creditsGainedOverTime;
                }
                break;
        }

        RequestUpdateCreditsNumber();
    }

    public void ScoreGainedFromKill(int amount)
    {
        playerScore += amount;
        RequestScoreUpdate(playerScore);
    }

    public bool IsAmmoFull()
    {
        if (currentAmmo >= maxAmmo)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsCreditsMaxed()
    {
        if (currentCredits >= maxCredits)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //----------Private----------
    void RotateHandler()
    {
        rotateGun = transform.DOLocalRotate(new Vector3(0, ogRotation.y + 80f, 0), 100f).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(delegate
        {
            rotateGun = transform.DOLocalRotate(new Vector3(0, ogRotation.y - 80f, 0), 100f).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(delegate
            {
                RotateHandler();
            });
        });
    }

    void ShootEffect()
    {
        if (gunShootEffect != null)
        {
            if (finishGunShootEffect != null)
            {
                if (finishGunShootEffect.IsPlaying())
                {
                    finishGunShootEffect.Kill();
                }
            }
            gunShootEffect.Restart();
        }
        else
        {
            gunShootEffect = gunModel.transform.DOLocalMoveZ(gunModel.transform.localPosition.z - gunModel.transform.forward.z * 0.2f, 0.05f)
                .SetEase(Ease.Linear)
                .SetAutoKill(false)
                .OnComplete(delegate
                {
                    finishGunShootEffect = gunModel.transform.DOLocalMoveZ(ogModelPosition.z, 0.05f)
                    .SetEase(Ease.Linear);
                });
        }

        currentAmmo--;

        RequestReload();
        RequestUpdateAmmoNumber();
    }

    void RequestReload()
    {
        if (currentAmmo < maxAmmo)
        {
            if (playerId == 7)
            {
                GamePlayController.Instance.gameScene.Player_1StartReload();
            }
            else
            {
                GamePlayController.Instance.gameScene.Player_2StartReload();
            }
        }
    }

    void RequestCreditsGain()
    {
        if (currentCredits < maxCredits)
        {
            if (playerId == 7)
            {
                GamePlayController.Instance.gameScene.Player_1GainingCredits();
            }
            else
            {
                GamePlayController.Instance.gameScene.Player_2GainingCredits();
            }
        }
    }

    void RequestUpdateAmmoNumber()
    {
        if (playerId == 7)
        {
            GamePlayController.Instance.gameScene.Player_1UpdateAmmo();
        }
        else
        {
            GamePlayController.Instance.gameScene.Player_2UpdateAmmo();
        }
    }

    void RequestUpdateCreditsNumber()
    {
        if (playerId == 7)
        {
            GamePlayController.Instance.gameScene.Player_1UpdateCurrentCredits(currentCredits);
        }
        else
        {
            GamePlayController.Instance.gameScene.Player_2UpdateCurrentCredits(currentCredits);
        }
    }

    void RequestScoreUpdate(int amount)
    {
        if (playerId == 7)
        {
            GamePlayController.Instance.gameScene.Player_1UpdatePoint(amount);
        }
        else
        {
            GamePlayController.Instance.gameScene.Player_2UpdatePoint(amount);
        }
    }

    IEnumerator ResumeRotationTimer()
    {
        rotateGun.Pause();
        yield return new WaitForSeconds(resumeRotationTimer);
        rotateGun.Play();
    }

    //----------Odin Functions----------
    IEnumerable PlayerNameList()
    {
        for (int i = 7; i <= 8; i++)
        {
            yield return LayerMask.LayerToName(i);
        }
    }
    IEnumerable PlayerIdList()
    {
        for (int i = 7; i <= 8; i++)
        {
            yield return i;
        }
    }

    void SetPlayerName()
    {
        playerName = LayerMask.LayerToName(playerId);
    }

    void SetPlayerId()
    {
        playerId = LayerMask.NameToLayer(playerName);       
    }
    #endregion

    #region Buff Handler
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
