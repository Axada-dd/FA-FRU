using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Extension;
using AEAssist.Helper;

namespace FA_FRU;

public class 点buff : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        return JobHelper.GetTranslation(Core.Me.CurrentJob()) != "忍者";
    }
}