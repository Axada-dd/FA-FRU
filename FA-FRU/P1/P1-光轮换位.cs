using AEAssist.CombatRoutine.Module.Target;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;

namespace FA_FRU.P1;

public class P1_光轮换位 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (!scriptEnv.KV.ContainsKey("P1光轮左安全")) return false;
        if(condParams is not AddStatusCondParams statusCondParams) return false;
        if (statusCondParams.StatusId != 1051) return false;
        var 点名目标 = TargetMgr.Instance.Units.Values
            .Where(e =>
            {
                using var enumerator = e.StatusList.GetEnumerator();
                return enumerator.Current.StatusId == 1051;
            })
            .Select(e => e.Name.ToString());
        LogHelper.Print("点名目标: " + string.Join(",", 点名目标));
        return false;
    }
}