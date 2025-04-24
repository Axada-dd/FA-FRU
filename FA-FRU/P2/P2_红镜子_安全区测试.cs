using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;
using DDDacr.工具;

namespace FA_FRU.P2;

public class P2_红镜子_安全区测试 : ITriggerScript
{
    private Vector3 红二位置 = new(100, 0, 100);
    private Vector3 红一位置 = new(100, 0, 100);
    private int 记录次数 = 0;

    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not OnEnvControlEvent envControlEvent) return false;
        if (envControlEvent.Flag != 512) return false;
        记录次数    ++;
        if (记录次数 == 1)
        {
            红一位置 = 坐标计算.RotatePoint(new(100, 0, 80.5f), new(100, 0, 100), float.Pi / 4 * (envControlEvent.Index-1));
        }

        if (记录次数 == 2)
        {
            红二位置 = 坐标计算.RotatePoint(new(100, 0, 80.5f), new(100, 0, 100), float.Pi / 4 * (envControlEvent.Index-1));
            LogHelper.Print($"红一镜子在红二镜子的{坐标计算.PositionTo8Dir(红一位置, 红二位置)}号位置");
            var 起始点 = 坐标计算.CalculatePointOnLine(红二位置, new Vector3(100, 0, 100), 2);
            for (int i = 2; i < 7; i++)
            {
                var 点位 = 坐标计算.RotatePoint(起始点, 红二位置, float.Pi / 4 * i);
                Share.TrustDebugPoint.Add(点位);
            }
            return true;
        }
        return false;
    }
}