using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;
using DDDacr.工具;

namespace FA_FRU.P1;

public class P1_上天八方_2 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not ReceviceAbilityEffectCondParams abilityEffectCondParams) return false;
        if (abilityEffectCondParams.ActionId != 40147 && abilityEffectCondParams.ActionId != 40149) return false;
        if(!scriptEnv.KV.ContainsKey("P1上天八方pos")) return false;
        Share.TrustDebugPoint.Clear();
        var pos = (Dictionary<string, Vector3>)scriptEnv.KV["P1上天八方pos"];
        TpAction(pos);
        return true;
    }
    private static async void TpAction(Dictionary<string, Vector3> partyPos)
    {
        await Task.Delay(300);
        foreach (var pos in partyPos)
        {
            RemoteControlHelper.SetPos(pos.Key, pos.Value);
        }
    }
}