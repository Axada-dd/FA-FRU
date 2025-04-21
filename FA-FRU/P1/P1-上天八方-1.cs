using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;
using DDDacr.工具;

namespace FA_FRU.P1;

public class P1_上天八方_1: ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not ReceviceAbilityEffectCondParams abilityEffectCondParams) return false;
        if (abilityEffectCondParams.ActionId != 40329 && abilityEffectCondParams.ActionId != 40330) return false;
        if(!scriptEnv.KV.ContainsKey("P1上天八方nextpos")) return false;
        Share.TrustDebugPoint.Clear();
        var nextpos = (Dictionary<string, Vector3>)scriptEnv.KV["P1上天八方nextpos"];
        TpAction(nextpos);
        return true;
    }
    private static async void TpAction(Dictionary<string, Vector3> partyPos)
    {
        await Task.Delay(800);
        foreach (var pos in partyPos)
        {
            RemoteControlHelper.SetPos(pos.Key, pos.Value);
        }
    }
}