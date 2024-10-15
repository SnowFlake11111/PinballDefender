using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Public Variables
    [Header("IMPORTANT - Used to identify player's side")]
    [ValueDropdown("PlayerIdList")]
    public int playerId;

    [Space]
    [Header("FOR MULTIPLAYER MODE - DEFENDER")]
    public bool allowUnitSpawning = false;

    [Space]
    [Header("MISC REFERENCES")]
    public GameObject playerHolder;
    public GameObject ballSpawnPoint;

    public Ball ball;
    #endregion

    #region Private Variables
    [SerializeField] int playerDamage = 10;
    int maxAmmo;
    int currentAmmo;

    float resumeRotationTimer = 0.25f;

    Coroutine autoResumeRotation;

    Tweener rotateGun;

    Ball tempBallRef;
    #endregion

    #region Buffs Timer
    float doubleDamageBuffTimer = 0;
    #endregion

    #region Dropdown Values Lists
    IEnumerable PlayerIdList()
    {
        for (int i = 1; i <= 2; i++)
        {
            yield return i;
        }
    }
    #endregion

    #region Start, Update
    private void Start()
    {
        RotateHandler();
    }

    private void Update()
    {
        InputHandler();
    }

    public void Init()
    {
        if (allowUnitSpawning)
        {
            //To do
            //Unit Spawn function
        }
    }
    #endregion

    #region Functions
    //Public
    public void Shoot()
    {
        if (autoResumeRotation != null)
        {
            StopCoroutine(autoResumeRotation);
        }
            
        autoResumeRotation = StartCoroutine(ResumeRotationTimer());

        tempBallRef = Instantiate(ball, ballSpawnPoint.transform.position, Quaternion.identity, playerHolder.transform);
        tempBallRef.transform.localScale = Vector3.one / 2;
        tempBallRef.transform.localEulerAngles = ballSpawnPoint.transform.eulerAngles;

        tempBallRef.ShootBall(ballSpawnPoint.transform.forward);
        tempBallRef.SetOwner(playerId);

        if (doubleDamageBuffTimer > 0)
        {
            tempBallRef.SetDamage(playerDamage * 2);
        }
        else
        {
            tempBallRef.SetDamage(playerDamage);
        }
        
    }

    //Private
    void RotateHandler()
    {
        rotateGun = transform.DOLocalRotate(new Vector3(0, 80f, 0), 100f).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(delegate
        {
            rotateGun = transform.DOLocalRotate(new Vector3(0, -80f, 0), 100f).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(delegate
            {
                RotateHandler();
            });
        });
    }

    void InputHandler()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    IEnumerator ResumeRotationTimer()
    {
        rotateGun.Pause();
        yield return new WaitForSeconds(resumeRotationTimer);
        rotateGun.Play();
    }
    #endregion

    #region Buff Handler
    #endregion
}
