using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;

namespace FA_FRU.P1;

public class P1_光轮TP火 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not ReceviceNoTargetAbilityEffectCondParams noTargetAbilityEffectCondParams) return false;
        if (noTargetAbilityEffectCondParams.ActionId != 40161) return false;//火刀1段
        if (!scriptEnv.KV.ContainsKey("P1光轮坐标2")) return false;
        Share.TrustDebugPoint.Clear();
        var P1光轮坐标2 = (Dictionary<string, Vector3>)scriptEnv.KV["P1光轮坐标2"];
        foreach (var 坐标 in P1光轮坐标2)
        {
            RemoteControlHelper.SetPos(坐标.Key, 坐标.Value);
            RemoteControlHelper.LockPos(坐标.Key,坐标.Value,4000);
        }
        return true;
    }
}