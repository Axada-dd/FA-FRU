using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;

namespace FA_FRU.P1;

public class P1_光轮 : ITriggerScript
{
    private bool 左红 = false;
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is EnemyCastSpellCondParams spellCondParams &&
            (spellCondParams.SpellId == 40150 || spellCondParams.SpellId == 40151))
        {
            if (spellCondParams.SpellId == 40150)
            {
                左红 = true;
            }
        }
        
        return false;
    }
}