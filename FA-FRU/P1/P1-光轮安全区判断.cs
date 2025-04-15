using AEAssist.CombatRoutine.Module.Target;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;

namespace FA_FRU.P1;

public class P1_光轮安全区判断 : ITriggerScript
{
    
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not EnemyCastSpellCondParams spellCondParams) return false;
        if (spellCondParams.SpellId != 40152) return false;
        var atEast = TargetMgr.Instance.Units.Values
            .Where(u => u.IsCasting && u.CastActionId == 40152 && (MathF.Abs(u.Position.Z - 100) < 1)).ToList()[0]
            .Position.X-100>1;
        if (!scriptEnv.KV.ContainsKey("P1光轮左安全"))
        {
            scriptEnv.KV.Add("P1光轮左安全", atEast);
        }
        return true;
    }
}