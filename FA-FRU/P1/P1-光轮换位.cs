using AEAssist.CombatRoutine.Module.Target;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;

namespace FA_FRU.P1;

public class P1_光轮换位 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if(condParams is not AddStatusCondParams statusCondParams) return false;
        if (statusCondParams.StatusId != 1051) return false;
        var 点名目标 = TargetMgr.Instance.Units.Values
            .Where(e =>
            {
                foreach (var status in e.StatusList.ToList())
                {
                    if (status.StatusId == 1051) return true;
                }

                return false;
            })
            .Select(e => e.Name.ToString()).ToList();
        LogHelper.Print("点名目标: " + string.Join(",", 点名目标));
        if(点名目标.Count != 2) return false;
        if (!scriptEnv.KV.ContainsKey("P1光轮左安全")) return false;
        return true;
    }
}