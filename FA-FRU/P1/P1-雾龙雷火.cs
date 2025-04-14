using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;

namespace FA_FRU.P1;

public class P1_雾龙雷火 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is EnemyCastSpellCondParams spellCondParams && (spellCondParams.SpellId == 40155 || spellCondParams.SpellId == 40154))
        {
            if (!scriptEnv.KV.ContainsKey("P1雾龙雷"))
            {
                if (spellCondParams.SpellId == 40155)
                {
                    scriptEnv.KV.Add("P1雾龙雷", true);
                    return true;
                }
                scriptEnv.KV.Add("P1雾龙雷", false);
                return true;
            }
        }
        return false;
    }
}