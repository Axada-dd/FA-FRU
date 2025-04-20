using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;

namespace FA_FRU.初始化;

public class 死人清轴 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not ActorDeathParams actorDeathParams) return false;
        if (actorDeathParams.DataId != 0) return false;
        return true;
    }
}