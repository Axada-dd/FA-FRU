using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;

namespace FA_FRU.P1;

public class P1_雾龙隐身分身 : ITriggerScript
{
    private List<IBattleChara> 隐身分身 = new();
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if(condParams is not AddStatusCondParams statusCondParams) return false;
        if (statusCondParams.StatusId != 1627) return false;
        //if(statusCondParams.Target == null) return false;
        隐身分身.Add(statusCondParams.Target);
        if (隐身分身.Count == 8)
        {
            LogHelper.Print("隐身分身记录完成");
            return true;
        }
        return false;
    }
}