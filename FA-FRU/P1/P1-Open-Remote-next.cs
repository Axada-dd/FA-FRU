using System.Numerics;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;

namespace FA_FRU.P1;

public class P1_Open_Remote_next : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not ReceviceAbilityEffectCondParams abilityEffectCondParams) return false;
        if (abilityEffectCondParams.ActionId != 40144 && abilityEffectCondParams.ActionId != 40148) return false;
        if(!scriptEnv.KV.ContainsKey("P1开场八方nextpos")) return false;
        var P1开场八方nextpos = (Dictionary<string, Vector3>)scriptEnv.KV["P1开场八方nextpos"];
        foreach (var nextpos in P1开场八方nextpos)
        {
            RemoteControlHelper.SetPos(nextpos.Key, nextpos.Value);
        }
        return true;
    }
}