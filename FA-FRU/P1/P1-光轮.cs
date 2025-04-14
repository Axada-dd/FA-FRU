using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Extension;
using DDDacr.工具;

namespace FA_FRU.P1;

public class P1_光轮 : ITriggerScript
{
    private bool 左红 = false;
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if(Core.Me is not null)
        {
            if (Core.Me.IsDps())
            {
                位移.Tp(new Vector3(100, 0, 116));
                return true;
            }
            else
            {
                位移.Tp(new Vector3(100, 0, 84));
                return true;
            }
        }

        return false;
    }
}