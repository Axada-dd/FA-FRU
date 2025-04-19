using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;

namespace FA_FRU.P1;

public class P1_塔_2 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if(condParams is not ReceviceNoTargetAbilityEffectCondParams noTargetAbilityEffectCondParams) return false;
        if (noTargetAbilityEffectCondParams.ActionId is not (40129 or 40134)) return false;
        if (!scriptEnv.KV.ContainsKey("P1踩塔")) return false;
        Share.TrustDebugPoint.Clear();
        var 踩塔 = (List<KeyValuePair<string, Vector3>>) scriptEnv.KV["P1踩塔"];
        foreach (var action in 踩塔)
        {
            RemoteControlHelper.SetPos(action.Key,action.Value);
        }
        return true;
    }
}