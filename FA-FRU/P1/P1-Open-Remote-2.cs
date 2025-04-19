using System.Numerics;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;

namespace FA_FRU.P1;

public class P1_Open_Remote_2 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not ReceviceNoTargetAbilityEffectCondParams noTargetAbilityEffectCondParams) return false;
        if (noTargetAbilityEffectCondParams.ActionId is not (40145 or 40146)) return false;
        if(!scriptEnv.KV.ContainsKey("P1开场八方nextpos")) return false;
        var P1开场八方nextpos = (Dictionary<string, Vector3>)scriptEnv.KV["P1开场八方nextpos"];
        foreach (var pos in P1开场八方nextpos)
        {
            RemoteControlHelper.SetPos(pos.Key, pos.Value);
        }
        return true;
    }
}