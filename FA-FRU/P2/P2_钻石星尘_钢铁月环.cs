using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;

namespace FA_FRU.P2;

public class P2_钻石星尘_钢铁月环 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not EnemyCastSpellCondParams spellCondParams) return false;
        if (spellCondParams.SpellId is not (40202 or 40203)) return false;
        if(!scriptEnv.KV.ContainsKey("P2_钻石星尘_钢铁"))scriptEnv.KV.Add("P2_钻石星尘_钢铁", spellCondParams.SpellId == 40202);
        return true;
    }
}