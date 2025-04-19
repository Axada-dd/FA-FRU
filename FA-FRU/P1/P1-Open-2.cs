using System.Numerics;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using DDDacr.工具;

namespace FA_FRU.P1;

public class P1_2 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not ReceviceAbilityEffectCondParams abilityEffectCondParams) return false;
        if (abilityEffectCondParams.ActionId != 40147 && abilityEffectCondParams.ActionId != 40149) return false;
        if(!scriptEnv.KV.ContainsKey("P1开场八方pos")&&!scriptEnv.KV.ContainsKey("P1开场八方nextpos")) return false;
        var pos = (Vector3)scriptEnv.KV["P1开场八方pos"];
        var nextpos = (Vector3)scriptEnv.KV["P1开场八方nextpos"];
        位移.Tp(pos);
        return true;
    }
}