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
            RemoteControlHelper.LockPos("", 坐标计算.坐标远离中心(spellCondParams.CastPos, new Vector3(100, 0, 100), 1), 3500);
            RemoteControlHelper.SetPos("",坐标计算.坐标远离中心(spellCondParams.CastPos,new Vector3(100, 0, 100), -2));
        }
        else
        {
            RemoteControlHelper.LockPos("", 坐标计算.坐标远离中心(spellCondParams.CastPos, new Vector3(100, 0, 100), -2), 3500);
            RemoteControlHelper.SetPos("",坐标计算.坐标远离中心(spellCondParams.CastPos,new Vector3(100, 0, 100), 1));
        }
        return true;
    }
}