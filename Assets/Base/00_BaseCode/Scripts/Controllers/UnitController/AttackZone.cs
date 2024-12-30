using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : SerializedMonoBehaviour
{
    #region Public Variables
    [Header("EXTREMELY IMPORTANT - Assign the unit of this attackzone")]
    public GameUnitBase unitBase;

    [Space]
    [Header("Specific unit type reference - Odin will detect this")]
    public WarriorUnit warrior;
    public RangerUnit ranger;
    public MageUnit mage;
    public EnforcerUnit enforcer;
    public DemonUnit demon;
    public MonsterUnit monster;
    public HealerUnit healer;
    public BerserkerUnit berserker;
    public BloodMageUnit bloodMage;
    public KingUnit king;
    public KingMinionUnit kingMinion;

    [Space]
    [Header("Specific unit function call id - Odin will detect this")]
    public int unitCallId = 0;
    #endregion

    #region Private Variables
    #endregion

    #region Start, Update
    #endregion

    #region Functions
    //----------Public----------
    public void Init()
    {
        gameObject.layer = unitBase.gameObject.layer;
    }
    //----------Private----------
    void SendAttackMessage()
    {
        switch (unitCallId)
        {
            case 1:
                //Warrior
                warrior.ActivateAttackPhase();
                break;
            case 2:
                //Ranger
                ranger.ActivateAttackPhase();
                break;
            case 3:
                //Mage
                mage.ActivateAttackPhase();
                break;
            case 4:
                //Enforcer
                enforcer.ActivateAttackPhase();
                break;
            case 5:
                //Demon
                demon.ActivateAttackPhase();               
                break;
            case 6:
                //Monster
                monster.ActivateAttackPhase();
                break;
            case 7:
                //Healer
                healer.ActivateAttackPhase();
                break;
            case 8:
                //Berserker
                berserker.ActivateAttackPhase();
                break;
            case 9:
                //Blood Mage
                bloodMage.ActivateAttackPhase();
                break;
            case 10:
                //King
                king.ActivateAttackPhase();
                break;
            case 11:
                //King Minion
                kingMinion.ActivateAttackPhase();
                break;
            default:
                Debug.LogError("Unknown AttackZone owner, attack message failed to deliver");
                break;
        }

        
    }
    //----------Odin Functions----------
    [Button]
    void DetectUnitType()
    {
        warrior = null;
        ranger = null;
        mage = null;
        enforcer = null;
        demon = null;
        monster = null;
        healer = null;
        berserker = null;
        bloodMage = null;
        king = null;

        UnitTypeChecker();
    }

    void UnitTypeChecker()
    {
        //Lý do ở đây là để gán tham chiếu thông qua Odin - việc này sẽ chỉ chạy trong lúc thiết kế chứ không phải trong lúc game chạy, nên là không phải lo tới hiệu suất
        //Sử dụng tham chiếu sẽ đỡ tốn hiệu năng hơn việc chạy GetComponent liên tục trong lúc game chạy

        if (unitBase.GetComponent<WarriorUnit>() != null)
        {
            unitCallId = 1;
            warrior = unitBase.GetComponent<WarriorUnit>();
        }
        else if (unitBase.GetComponent<RangerUnit>() != null)
        {
            unitCallId = 2;
            ranger = unitBase.GetComponent<RangerUnit>();
        }
        else if (unitBase.GetComponent<MageUnit>() != null)
        {
            unitCallId = 3;
            mage = unitBase.GetComponent<MageUnit>();
        }
        else if (unitBase.GetComponent<EnforcerUnit>() != null)
        {
            unitCallId = 4;
            enforcer = unitBase.GetComponent<EnforcerUnit>();
        }
        else if (unitBase.GetComponent<DemonUnit>() != null)
        {
            unitCallId = 5;
            demon = unitBase.GetComponent<DemonUnit>();
        }
        else if (unitBase.GetComponent<MonsterUnit>() != null)
        {
            unitCallId = 6;
            monster = unitBase.GetComponent<MonsterUnit>();
        }
        else if (unitBase.GetComponent<HealerUnit>() != null)
        {
            unitCallId = 7;
            healer = unitBase.GetComponent<HealerUnit>();
        }
        else if (unitBase.GetComponent<BerserkerUnit>() != null)
        {
            unitCallId = 8;
            berserker = unitBase.GetComponent<BerserkerUnit>();
        }
        else if (unitBase.GetComponent<BloodMageUnit>() != null)
        {
            unitCallId = 9;
            bloodMage = unitBase.GetComponent<BloodMageUnit>();
        }
        else if (unitBase.GetComponent<KingUnit>() != null)
        {
            unitCallId = 10;    
            king = unitBase.GetComponent<KingUnit>();
        }
        else if (unitBase.GetComponent<KingMinionUnit>() != null)
        {
            unitCallId = 11;
            kingMinion = unitBase.GetComponent<KingMinionUnit>();
        }
        else
        {
            Debug.LogError("This unit doesn't belong to any known type");
        }
    }
    #endregion

    #region Collision Functions
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GameUnitBase>() != null)
        {
            other.GetComponent<GameUnitBase>().RegisterAttacker(unitBase);
            unitBase.RegisterTarget(other.GetComponent<GameUnitBase>());
            SendAttackMessage();
        }
        else if (other.GetComponent<GateController>() != null)
        {
            unitBase.RegisterEnemyGate(other.GetComponent<GateController>());
            SendAttackMessage();
        }

        //To Do: Register Gate for unit [done]
    }

    private void OnTriggerExit(Collider other)
    {
        //To Do: This one is likely to be called on Gate alone when player pushes enemies away
        if (other.GetComponent<GameUnitBase>() != null)
        {
            if (other.GetComponent<BoxCollider>().enabled)
            {
                unitBase.RemoveTarget(other.GetComponent<GameUnitBase>());
                //Debug.LogError(unitBase.name + " Found a target");
            }
        }
        else if (other.GetComponent<GateController>() != null)
        {
            if (other.GetComponent<BoxCollider>().enabled)
            {
                unitBase.RemoveEnemyGate();
            }
        }
    }
    #endregion
}
