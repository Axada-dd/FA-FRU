using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;

namespace FA_FRU.P2;

public class P2_镜子分摊分散 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not EnemyCastSpellCondParams spellCondParams) return false;
        if (spellCondParams.SpellId is not (40220 or 40221)) return false;
        Share.TrustDebugPoint.Clear();
        if (spellCondParams.SpellId == 40220)
        {
            
        }
        return false;
    }
}