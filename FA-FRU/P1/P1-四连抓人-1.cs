using System.Numerics;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;

namespace FA_FRU.P1;

public class P1_四连抓人_1 : ITriggerScript
{
    private int count = 0;
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not ReceviceAbilityEffectCondParams abilityEffectCondParams) return false;
        if (abilityEffectCondParams.ActionId != 40143) return false;
        if (!scriptEnv.KV.ContainsKey("P1四连抓人")) return false;
        count++;
        var nextpos = (List<KeyValuePair<string,Vector3>>)scriptEnv.KV["P1四连抓人"];
        if (count == 1)
        {
            RemoteControlHelper.SetPos(nextpos[0].Key, nextpos[0].Value);
            RemoteControlHelper.SetPos(nextpos[1].Key, nextpos[1].Value);
            RemoteControlHelper.SetPos(nextpos[2].Key, nextpos[2].Value);
            RemoteControlHelper.SetPos(nextpos[3].Key, nextpos[3].Value);
        }

        if (count == 2)
        {
            RemoteControlHelper.SetPos(nextpos[4].Key, nextpos[4].Value);
            RemoteControlHelper.SetPos(nextpos[5].Key, nextpos[5].Value);
            RemoteControlHelper.SetPos(nextpos[6].Key, nextpos[6].Value);
            RemoteControlHelper.SetPos(nextpos[7].Key, nextpos[7].Value);
            return true;
        }    
        return false;
    }
}