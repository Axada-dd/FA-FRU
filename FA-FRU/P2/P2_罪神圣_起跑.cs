using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;

namespace FA_FRU.P2;

public class P2_罪神圣_起跑 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if(condParams is not ReceviceAbilityEffectCondParams abilityEffectCondParams) return false;
        if (abilityEffectCondParams.ActionId != 40200) return false;
        if(!scriptEnv.KV.ContainsKey("起跑点1")) return false;
        if(!scriptEnv.KV.ContainsKey("起跑点2")) return false;
        
        Share.TrustDebugPoint.Clear();
        var 起跑点1 = (Vector3)scriptEnv.KV["起跑点1"];
        var 起跑点2 = (Vector3)scriptEnv.KV["起跑点2"];
        MTActon(起跑点1);
        STActon(起跑点2);
        return true;
    }

    private static async void MTActon(Vector3 partyPos)
    {
        await Task.Delay(300);
        RemoteControlHelper.SetPos("MT|H1|D1|D3", partyPos);
    }

    private static async void STActon(Vector3 partyPos)
    {
       await Task.Delay(300);
       RemoteControlHelper.SetPos("ST|H2|D2|D4", partyPos);
    }
}