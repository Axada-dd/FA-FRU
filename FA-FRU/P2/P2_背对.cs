using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;

namespace FA_FRU.P2;

public class P2_背对 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if(condParams is not AddStatusCondParams statusCondParams) return false;
        if (statusCondParams.Source == null) return false;
        if (statusCondParams.StatusId != 2273 && statusCondParams.Source.DataId != 17823) return false;
        Action(statusCondParams.Source.Rotation);
        return true;
    }

    private async void Action(float rotation)
    {
       await Task.Delay(4500);
       RemoteControlHelper.SetRot("",rotation);
    }
}