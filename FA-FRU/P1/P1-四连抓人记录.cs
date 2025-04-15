using AEAssist.CombatRoutine.Module.Target;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;
using DDDacr.工具;

namespace FA_FRU.P1;

public class P1_四连抓人记录 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if(condParams is not AddStatusCondParams statusCondParams) return false;
        if (statusCondParams.StatusId != 1051) return false;
        var 点名目标 = TargetMgr.Instance.Units.Values
            .Where(e =>
                e.StatusList.ToList().Select(e => e.StatusId).ToList().Contains(1051)).ToList();
        if (点名目标.Count == 1)
        {
            LogHelper.Print($"1号点名目标：{点名目标.Last().Name}，职能：{点名目标.Last().GetRoleByPlayerObjct()}");
        }

        if (点名目标.Count == 2)
        {
            LogHelper.Print($"2号点名目标：{点名目标.Last().Name}，职能：{点名目标.Last().GetRoleByPlayerObjct()}");    
        }

        if (点名目标.Count == 3)
        {
           LogHelper.Print($"3号点名目标：{点名目标.Last().Name}，职能：{点名目标.Last().GetRoleByPlayerObjct()}"); 
        }

        if (点名目标.Count == 4)
        {
           LogHelper.Print($"4号点名目标：{点名目标.Last().Name}，职能：{点名目标.Last().GetRoleByPlayerObjct()}");     
        }
        LogHelper.Print("四连抓人记录: " + string.Join(",", 点名目标.Select(e => e.Name.ToString())));
        return true;
    }
}