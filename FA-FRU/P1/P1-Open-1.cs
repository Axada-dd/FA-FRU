using System.Numerics;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using DDDacr.工具;

namespace FA_FRU.P1;

public class P1_1 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not ReceviceAbilityEffectCondParams abilityEffectCondParams) return false;
        if(!scriptEnv.KV.ContainsKey("P1开场八方pos")&&!scriptEnv.KV.ContainsKey("P1开场八方nextpos")) return false;
        if (abilityEffectCondParams.ActionId != 40144 && abilityEffectCondParams.ActionId != 40148) return false;
        var pos = (Vector3)scriptEnv.KV["P1开场八方pos"];
        var nextpos = (Vector3)scriptEnv.KV["P1开场八方nextpos"];
        位移.Tp(nextpos);
        return true;
    }
}