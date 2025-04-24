using System.Numerics;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;
using DDDacr.工具;

namespace FA_FRU.P2;

public class P2_白镜子近战组处理 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not EnemyCastSpellCondParams spellCondParams) return false;
        if(spellCondParams.SpellId != 40203)    return false;
        var 敌人坐标 = spellCondParams.CastPos;
        var 起始点 = 坐标计算.CalculatePointOnLine(敌人坐标, new Vector3(100, 0, 100), 5);
        RemoteControlHelper.SetPos("D1", 坐标计算.RotatePoint(起始点, 敌人坐标, float.Pi / 4 * 6));
        RemoteControlHelper.SetPos("D2", 坐标计算.RotatePoint(起始点, 敌人坐标, float.Pi / 4 * 2));
        RemoteControlHelper.SetPos("MT", 坐标计算.RotatePoint(起始点, 敌人坐标, float.Pi / 4 * 7));
        RemoteControlHelper.SetPos("ST", 坐标计算.RotatePoint(起始点, 敌人坐标, float.Pi / 4 * 1));
        return true;
    }
}