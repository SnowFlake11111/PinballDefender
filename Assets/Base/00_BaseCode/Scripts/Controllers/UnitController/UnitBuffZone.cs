using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBuffZone : SerializedMonoBehaviour
{
    #region Public Variables
    [Header("Which buff will this zone grant")]
    public bool attackUp = false;
    public bool defenseUp = false;
    public bool rally = false;
    public bool heal = false;

    [ShowIf("AttackUpWillBeGranted")]
    [Space]
    public float attackUpDuration = 0;
    [ShowIf("DefenseUpWillBeGranted")]
    [Space]
    public float defenseUpDuration = 0;
    [ShowIf("RallyWillBeGranted")]
    [Space]
    public float rallyDuration = 0;
    [ShowIf("HealingWillBeGranted")]
    [Space]
    public float healMaxHealthPercent = 0;

    [Space]
    [Header("Allow this zone to buff its owner ?")]
    public bool allowSelfBuff = false;

    [Space]
    [Header("Unit that control this buff zone")]
    public GameUnitBase controlUnit;

    [Space]
    [Header("Effect and ColliderSphere")]
    public ParticleSystem buffEffect;
    public SphereCollider triggerColliderSphere;
    #endregion

    #region Private Variables
    Coroutine buffZoneTimerHandler;
    #endregion

    #region Start, Update
    #endregion

    #region Functions
    //----------Public----------
    public void InitBuffZone(int layer)
    {
        if (layer != 7)
        {
            gameObject.layer = 7;
        }
        else
        {
            //Giải thích: vì game đã được điều chỉnh phần collision cho các object có chung layer sẽ không tương tác với nhau, nên để mà các buff zone này có thể buff được đồng đội (và bản thân nếu được phép) thì layer của buff zone phải trùng với phe địch, như thế zone sẽ tương tác với đồng đội mà không tương tác với địch
            if (GamePlayController.Instance.gameLevelController.currentLevel.DoesStageHaveDirector())
            {
                gameObject.layer = 6;
            }
            else
            {
                gameObject.layer = 8;
            }
        }
    }

    public void ActivateBuffZone(float duration)
    {
        buffEffect.Play();
        triggerColliderSphere.enabled = true;

        if (allowSelfBuff)
        {
            GrantBuff(controlUnit);
        }

        if (buffZoneTimerHandler != null)
        {
            StopCoroutine(buffZoneTimerHandler);
        }

        buffZoneTimerHandler = StartCoroutine(BuffZoneTimer(duration));
    }
    //----------Private----------
    void GrantBuff(GameUnitBase ally)
    {
        if (attackUp)
        {
            ally.GrantAttackUp(attackUpDuration);
        }
        else if (defenseUp)
        {
            ally.GrantDefenseUp(defenseUpDuration);
        }
        else if (rally)
        {
            ally.GrantRally(rallyDuration);
        }
        else if (heal)
        {
            ally.GainHealthPercent(healMaxHealthPercent / 100);
        }
    }

    IEnumerator BuffZoneTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        triggerColliderSphere.enabled = false;
        buffZoneTimerHandler = null;
    }

    //----------Odin Functions----------
    bool AttackUpWillBeGranted()
    {
        if (attackUp)
        {
            return true;
        }
        else
        {
            attackUpDuration = 0;
            return false;
        }
    }

    bool DefenseUpWillBeGranted()
    {
        if (defenseUp)
        {
            return true;
        }
        else
        {
            defenseUpDuration = 0;
            return false;
        }
    }

    bool RallyWillBeGranted()
    {
        if (rally)
        {
            return true;
        }
        else
        {
            rallyDuration = 0;
            return false;
        }
    }

    bool HealingWillBeGranted()
    {
        if (heal)
        {
            return true;
        }
        else
        {
            healMaxHealthPercent = 0;
            return false;
        }
    }
    #endregion

    #region Collsion Functions
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GameUnitBase>() != null)
        {
            GrantBuff(other.GetComponent<GameUnitBase>());
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