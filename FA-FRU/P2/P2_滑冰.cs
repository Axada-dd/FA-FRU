using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;
using DDDacr.工具;

namespace FA_FRU.P2;

public class P2_滑冰 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not EnemyCastSpellCondParams spellCondParams) return false;
        if (spellCondParams.SpellId is not (40193 or 40194)) return false;
        Share.TrustDebugPoint.Clear();
        if (spellCondParams.SpellId == 40193)
        {
            RemoteControlHelper.SetPos("ST|H2|D2|D4|MT|H1|D1|D3", 坐标计算.CalculatePointOnLine(spellCondParams.CastPos, new Vector3(100, 0, 100), 2));
            TpAction("ST|H2|D2|D4|MT|H1|D1|D3", 坐标计算.CalculatePointOnLine(spellCondParams.CastPos,new Vector3(100, 0, 100), -2));
            
        }
        else
        {
            RemoteControlHelper.SetPos("ST|H2|D2|D4|MT|H1|D1|D3", 坐标计算.CalculatePointOnLine(spellCondParams.CastPos, new Vector3(100, 0, 100), -2));
            TpAction("ST|H2|D2|D4|MT|H1|D1|D3",坐标计算.CalculatePointOnLine(spellCondParams.CastPos,new Vector3(100, 0, 100), 2));
        }
        return true;
    }
    private static async void TpAction(string regex, Vector3 partyPos)
    {
        await Task.Delay(3500);
        RemoteControlHelper.SetPos(regex,partyPos);
    }
}