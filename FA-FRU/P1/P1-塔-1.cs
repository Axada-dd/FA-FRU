using System.Numerics;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;

namespace FA_FRU.P1;

public class P1_塔_1 : ITriggerScript
{
    private List<KeyValuePair<string, Vector3>> 踩塔 = [];
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if(condParams is not EnemyCastSpellCondParams spellCondParams) return false;
        if (spellCondParams.SpellId is not (40134 or 40129)) return false;
        if (!scriptEnv.KV.ContainsKey("P1塔雷"))
        {
           scriptEnv.KV.Add("P1塔雷", spellCondParams.SpellId == 40134); 
        }

        return true;
        
    }
}