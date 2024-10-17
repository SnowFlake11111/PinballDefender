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
    //[OnValueChanged("SetPlayerId")]
    public string playerName;

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
    int playerId = 0;
    [SerializeField] int playerDamage = 10;
    int maxAmmo;
    int currentAmmo;

    float resumeRotationTimer = 0.25f;

    Coroutine autoResumeRotation;

    Tweener rotateGun;

    Ball tempBallRef;

    //[SerializeField] List<Ball> ballList = new List<Ball>();
    //[SerializeField] List<GameUnit> alliesList = new List<GameUnit>();
    #endregion

    #region Buffs Timer
    float doubleDamageBuffTimer = 0;
    #endregion

    #region Start, Update
    private void Start()
    {
        SetPlayerId();
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
    //----------Public----------
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
        
        //Việc giữ các quả bóng trong một list là để đảm bảo việc giữ cho các va chạm giữa các quả bóng của người chơi sẽ bỏ qua va chạm đối với quân cùng phe
        //UPDATE: Đã đổi sang sử dụng Collision Layer
    }
    //To Do: Create a function that handle spawning unit




    //----------Private----------
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

    //----------Odin Functions----------
    IEnumerable PlayerIdList()
    {
        for (int i = 7; i <= 8; i++)
        {
            yield return LayerMask.LayerToName(i);
        }
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
