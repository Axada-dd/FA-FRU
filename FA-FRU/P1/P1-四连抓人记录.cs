using AEAssist.CombatRoutine.Module.Target;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;

namespace FA_FRU.P1;

public class P1_四连抓人记录 : ITriggerScript
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
            }).ToList();
        if(点名目标.Count != 4) return false;
        LogHelper.Print("四连抓人记录: " + string.Join(",", 点名目标.Select(e => e.Name.ToString())));
        return false;
    }
}