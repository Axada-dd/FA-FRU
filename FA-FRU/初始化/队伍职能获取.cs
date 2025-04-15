using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;
using DDDacr.工具;

namespace FA_FRU.初始化;

public class 队伍职能获取 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        foreach (var member in PartyHelper.Party)
        {
            LogHelper.Print($"{member.Name}：{member.GetRoleByPlayerObjct()}");
        }
        return true;
    }
}