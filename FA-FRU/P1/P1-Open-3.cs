using System.Numerics;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;
using DDDacr.工具;

namespace FA_FRU.P1;

public class P1_Open_3 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if(!scriptEnv.KV.ContainsKey("P1开场八方pos")&&!scriptEnv.KV.ContainsKey("P1开场八方nextpos")) return false;
        if (condParams is not ReceviceNoTargetAbilityEffectCondParams noTargetAbilityEffectCondParams) return false;
        if(noTargetAbilityEffectCondParams.ActionId is 40145 or 40146)
        {
            var nextpos = (Vector3)scriptEnv.KV["P1开场八方nextpos"];
            位移.Tp(nextpos);
            return true;
        }
        return false;
    }
}