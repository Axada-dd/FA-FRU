using System.Numerics;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;

namespace FA_FRU.P1;

public class P1_光轮TP : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not ReceviceNoTargetAbilityEffectCondParams noTargetAbilityEffectCondParams) return false;
        if (noTargetAbilityEffectCondParams.ActionId != 40164) return false;//扩散雷
        if (!scriptEnv.KV.ContainsKey("P1光轮坐标1")) return false;
        
        var P1光轮坐标1 = (Dictionary<string, Vector3>)scriptEnv.KV["P1光轮坐标1"];
        foreach (var 坐标 in P1光轮坐标1)
        {
            RemoteControlHelper.SetPos(坐标.Key, 坐标.Value);
        }
        return true;
    }
}