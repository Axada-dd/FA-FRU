using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using ShiyuviBlack.BLM_7;

namespace FA_FRU;

public class BLM停手 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        Qt.SetQt("停手", true);
        Qt.SetQt("停手", false);
        return true;
    }
}