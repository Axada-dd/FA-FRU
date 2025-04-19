using System.Numerics;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;

namespace FA_FRU.P1;

public class P1_Open_Remote_1 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not ReceviceAbilityEffectCondParams abilityEffectCondParams) return false;
        if (abilityEffectCondParams.ActionId != 40147 && abilityEffectCondParams.ActionId != 40149) return false;
        if(!scriptEnv.KV.ContainsKey("P1开场八方pos")) return false;
        var P1开场八方pos = (Dictionary<string, Vector3>)scriptEnv.KV["P1开场八方pos"];
        foreach (var pos in P1开场八方pos)
        {
            RemoteControlHelper.SetPos(pos.Key, pos.Value);
        }
        return true;
    }
}