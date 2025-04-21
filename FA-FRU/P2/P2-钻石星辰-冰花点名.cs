using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using DDDacr.工具;

namespace FA_FRU.P2;

public class P2_钻石星辰_冰花点名 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not TargetIconEffectCondParams targetCondParams) return false;
        if (targetCondParams.Args0 != 127) return false;
        if(!scriptEnv.KV.ContainsKey("P2_冰花点D"))scriptEnv.KV.Add("P2_冰花点D", targetCondParams.Target.IsDpsRe());
        return true;
    }
}