using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;
using DDDacr.工具;
using ECommons;

namespace FA_FRU.P1;

public class P1TEST : ITriggerScript
{
    List<int> P1转轮召抓人 = [0, 0, 0, 0, 0, 0, 0, 0];
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not EnemyCastSpellCondParams spellCondParams) return false;
        if (spellCondParams.SpellId != 40152) return false;
        var atEast = TargetMgr.Instance.Units.Values
            .Where(u => u.IsCasting && u.CastActionId == 40152 && (MathF.Abs(u.Position.Z - 100) < 1)).ToList()[0]
            .Position.X-100>1;
        return true;
    }

}