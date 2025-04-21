using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;
using DDDacr.工具;

namespace FA_FRU.P2;

public class P2_罪神圣_跑圈 : ITriggerScript
{
    Vector3 起跑点1 = new(100, 0, 100);
    Vector3 起跑点2 = new(100, 0, 100);
    private int _times = 0;
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not ReceviceAbilityEffectCondParams abilityEffectCondParams) return false;
        if (abilityEffectCondParams.ActionId != 40209) return false;
        if(abilityEffectCondParams.Target == null) return false;
        if (abilityEffectCondParams.Target.DataId != PartyHelper.Party.ToList().First().DataId) return false;
        if(!scriptEnv.KV.ContainsKey("起跑点1")) return false;
        if(!scriptEnv.KV.ContainsKey("起跑点2")) return false;
        if (_times == 0)
        {
            Share.TrustDebugPoint.Clear();
            
            起跑点1 = (Vector3)scriptEnv.KV["起跑点1"];
            起跑点2 = (Vector3)scriptEnv.KV["起跑点2"];
        }

        _times++;
        
        起跑点1 = 坐标计算.RotatePoint(起跑点1,new (100,0,100),float.Pi/8);
        起跑点2 = 坐标计算.RotatePoint(起跑点2,new (100,0,100),-float.Pi/8);
        MTActon(起跑点1);
        STActon(起跑点2);
        return _times == 4;
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