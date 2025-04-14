using System.Numerics;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using DDDacr.工具;

namespace FA_FRU.P1;

public class P1_上天八方_1: ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not ReceviceAbilityEffectCondParams abilityEffectCondParams) return false;
        if(!scriptEnv.KV.ContainsKey("P1上天八方pos")&&!scriptEnv.KV.ContainsKey("P1上天八方nextpos")) return false;
        if (abilityEffectCondParams.ActionId != 40329 && abilityEffectCondParams.ActionId != 40330) return false;
        var nextpos = (Vector3)scriptEnv.KV["P1上天八方nextpos"];
        位移.Tp(nextpos);
        return true;
    }
}