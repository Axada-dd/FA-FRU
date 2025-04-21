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
        TpAction(踩塔);    
        return true;
    }
    private static async void TpAction(List<KeyValuePair<string, Vector3>> partyPos)
    {
        await Task.Delay(400);
        foreach (var pos in partyPos)
        {
            RemoteControlHelper.SetPos(pos.Key, pos.Value);
        }
    }
}