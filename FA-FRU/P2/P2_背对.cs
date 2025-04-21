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
        TpAction(statusCondParams.Source.Rotation);
        return true;
    }

    private async void TpAction(float rotation)
    {
       await Task.Delay(2000);
       RemoteControlHelper.SetRot("MT|ST|H1|H2|D1|D2|D3|D4",rotation);
    }
}