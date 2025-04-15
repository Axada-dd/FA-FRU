using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Extension;
using AEAssist.Helper;
using DDDacr.工具;

namespace FA_FRU.P1;

public class P1_光轮换位_me : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        var 点名目标 = TargetMgr.Instance.Units.Values
            .Where(e =>
            {
                foreach (var status in e.StatusList.ToList())
                {
                    if (status.StatusId == 1051) return true;
                }

                return false;
            }).ToList();
        var 点名目标Name = 点名目标.Select(e=> e.Name.TextValue).ToList();
        if(点名目标.Count != 2) return false;
        if (!scriptEnv.KV.ContainsKey("P1光轮左安全")) return false;
        LogHelper.Print("点名目标: " + string.Join(",", 点名目标Name));
        if (点名目标.Select(e => e.IsMe()).Contains(true))//我是点名目标
        {
            if (点名目标.All(e => e.IsDps()))
            {
                if (AI.Instance.PartyRole == "D1")//我是D1
                {
                    LogHelper.Print("换到上面");
                }
            }
            else if (点名目标.All(e => e.IsTankOrHealer()))
            {
                if (AI.Instance.PartyRole == "MT") //我是MT
                {
                   LogHelper.Print("换到下面"); 
                }
            }
        }
        else
        {
            if (点名目标.All(e => e.IsDps()))//点名DPS
            {
                if (AI.Instance.PartyRole == "MT")
                {
                    LogHelper.Print("换到下面");
                }
            }
            else if (点名目标.All(e => e.IsTankOrHealer()))//点名T奶
            {
                if (AI.Instance.PartyRole == "D1")
                {
                    LogHelper.Print("换到上面");
                } 
            }
        }
        return true;
    }
}