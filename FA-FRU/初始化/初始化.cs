using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;

namespace FA_FRU.初始化;

public class 初始化 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        Share.TrustDebugPoint.Clear();
        try
        {
            scriptEnv.KV.Clear();
        }
        catch (Exception e)
        {
            return true;
        }
        return true;
    }
}