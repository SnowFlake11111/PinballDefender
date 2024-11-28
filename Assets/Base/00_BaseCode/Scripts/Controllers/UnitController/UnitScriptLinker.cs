using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScriptLinker : SerializedMonoBehaviour
{
    #region Public Variables
    [Header("LINK TO THIS UNIT")]
    public GameUnitBase linkedUnit;

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
    #endregion

    #region Private Variables
    #endregion

    #region Start, Update
    #endregion

    #region Functions
    //----------Public----------
    //Theo ban đầu thì dự kiến là để kiểm tra if else trong 1 hàm để tiết kiệm thời gian viết code, nhưng do có tận 10 unit để kiểm tra nên việc này đã thay đổi thành viết hàm cho riêng các unit, kèm với việc có unit sẽ có tận 3 hành động nên chạy switch case như bên AttackZone là không thể

    //----------Attack----------
    public void WarriorAttack()
    {
        warrior.AttackEnemy();
    }

    public void RangerAttack()
    {
        ranger.ShootEnemy();
    }

    public void MageCast()
    {
        mage.CastFireball();
    }

    public void EnforcerSlash()
    {
        enforcer.SlashEnemy();
    }

    public void DemonSlash()
    {
        demon.DemonicSlash();
    }

    public void DemonCast()
    {
        demon.DemonicFireball();
    }

    public void MonsterDoubleAttack()
    {
        monster.DoubleAttack();
    }

    public void HealerCast()
    {
        healer.CastEnergyBall();
    }

    public void BerserkerAttack()
    {
        berserker.AttackEnemy();
    }

    public void BloodMageCast()
    {
        bloodMage.CastDarkFireball();
    }

    public void KingCast()
    {
        king.CastSoulball();
    }

    public void KingSummon()
    {
        king.SummonMinion();
    }

    public void KingMinionAttack()
    {
        kingMinion.AttackEnemy();
    }

    public void KingMinionFinshSpawning()
    {
        kingMinion.FinishiSpawnAnimation();
    }
    //----------Death----------
    public void Death()
    {
        linkedUnit.FinishDeathAnimation();
    }

    //----------Private----------
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

        if (linkedUnit.GetComponent<WarriorUnit>() != null)
        { 
            warrior = linkedUnit.GetComponent<WarriorUnit>();
        }
        else if (linkedUnit.GetComponent<RangerUnit>() != null)
        {
            ranger = linkedUnit.GetComponent<RangerUnit>();
        }
        else if (linkedUnit.GetComponent<MageUnit>() != null)
        {
            mage = linkedUnit.GetComponent<MageUnit>();
        }
        else if (linkedUnit.GetComponent<EnforcerUnit>() != null)
        {
            enforcer = linkedUnit.GetComponent<EnforcerUnit>();
        }
        else if (linkedUnit.GetComponent<DemonUnit>() != null)
        {
            demon = linkedUnit.GetComponent<DemonUnit>();
        }
        else if (linkedUnit.GetComponent <MonsterUnit>() != null)
        {
            monster = linkedUnit.GetComponent<MonsterUnit>();
        }
        else if (linkedUnit.GetComponent<HealerUnit>() != null)
        {
            healer = linkedUnit.GetComponent<HealerUnit>();
        }
        else if (linkedUnit.GetComponent<BerserkerUnit>() != null)
        {
            berserker = linkedUnit.GetComponent<BerserkerUnit>();
        }
        else if (linkedUnit.GetComponent<BloodMageUnit>() != null)
        {
            bloodMage = linkedUnit.GetComponent<BloodMageUnit>();
        }
        else if (linkedUnit.GetComponent<KingUnit>() != null)
        {
            king = linkedUnit.GetComponent<KingUnit>();
        }
        else if (linkedUnit.GetComponent<KingMinionUnit>() != null)
        {
            kingMinion = linkedUnit.GetComponent<KingMinionUnit>();
        }
        else
        {
            Debug.LogError("This unit doesn't belong to any known type");
        }
    }
    #endregion
}
