using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.DynamicComplie;
using ShiyuviBlack.BLM_7;
namespace AutoInstance.杂项;

public class BLM_绝E_Opener: IOpener, ISlotSequence, IScript
{
    public Action CompeltedAction { get; set; }
    //技能id
    public const uint 火3 = 152;
    public const uint 火4 = 3577;
    public const uint 绝望 = 16505;
    public const uint 冰3 = 154;
    public const uint 冰4 = 3576;
    public const uint 雷 = 36986;
    public const uint 耀星 = 36989;
    public const uint 墨泉 = 158;
    public const uint 转圈 = 16506;
    public const uint 详述 = 25796;
    public const uint 黑魔纹 = 3573;
    public const uint 即刻 = 7561;
    public const uint 异言 = 16507;
    public const uint 三连 = 7421;
    public const uint 悖论 = 25797;
    public const uint 醒梦 = 7562;
    public const uint 心灵 = 149;
    
    public List<Action<Slot>> Sequence { get; } = new()
    {
        Step雷,
        Step火4三连,
        Step火4,Step火4,Step火4,
        Step绝望墨泉,
        Step火4,
        Step火4醒梦,
        Step耀星,
        Step悖论,
        Step绝望星灵,
    };
    private static void Step雷(Slot slot)
    {
        //slot.Add(new Spell(雷, SpellTargetType.Target));
        slot.Add(new Spell(即刻, SpellTargetType.Self));
        slot.Add(new Spell(详述, SpellTargetType.Self));
    }
    
    private static void Step火4三连(Slot slot)
    {
        slot.Add(new Spell(火4, SpellTargetType.Target));
        slot.Add(new Spell(三连, SpellTargetType.Self));
    }

    private static void Step火4(Slot slot)//火4
    {
        slot.Add(new Spell(火4, SpellTargetType.Target));
    }

    public static void Step绝望墨泉(Slot slot)
    {
        slot.Add(new Spell(绝望, SpellTargetType.Target));
        slot.Add(new Spell(墨泉, SpellTargetType.Self));
        slot.Add(new Spell(三连, SpellTargetType.Self));
    }

    public static void Step火4醒梦(Slot slot)
    {
        slot.Add(new Spell(火4, SpellTargetType.Target));
        slot.Add(new Spell(醒梦, SpellTargetType.Self));
    }

    public static void Step耀星(Slot slot)
    {
       slot.Add(new Spell(耀星, SpellTargetType.Target)); 
    }

    public static void Step悖论(Slot slot)
    {
       slot.Add(new Spell(悖论, SpellTargetType.Target)); 
    }

    public static void Step绝望星灵(Slot slot)
    {
      slot.Add(new Spell(绝望, SpellTargetType.Target));
      slot.Add(new Spell(心灵, SpellTargetType.Self));
    }

    public static void Step(Slot slot)
    {
        Qt.SetQt("停手", true);
    }
    
    
    
    
    
    
    public void InitCountDown(CountDownHandler countDownHandler)
    {
        Qt.SetQt("停手", true);
        // 倒数14秒的时候跳舞  (如果当前倒计时直接从10秒开始 这个也会触发 多个同时触发的 会按顺序处理)
        countDownHandler.AddAction(4000, BeforeBattle);
        // 倒数10秒的时候开始战斗
        countDownHandler.AddAction(3400, 火3,SpellTargetType.Target);
        countDownHandler.AddAction(250,雷,SpellTargetType.Target);
    }
    public static Spell BeforeBattle()
    {
        return new Spell(黑魔纹, Core.Me.Position);
    }
    
}