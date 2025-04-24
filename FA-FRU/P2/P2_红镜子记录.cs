using System.Numerics;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using DDDacr.工具;

namespace FA_FRU.P2;

public class P2_红镜子记录 : ITriggerScript
{
    private Vector3 红二位置 = new(100, 0, 100);
    private Vector3 红一位置 = new(100, 0, 100);
    private int 记录次数 = 0;

    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not OnEnvControlEvent envControlEvent) return false;
        if (envControlEvent.Flag != 512) return false;
        记录次数++;
        if (记录次数 == 1)
        {
            红一位置 = 坐标计算.RotatePoint(new(100, 0, 80.5f), new(100, 0, 100), float.Pi / 4 * (envControlEvent.Index-1));
            //记录红一位置
            scriptEnv.KV.Add("红一位置", 红一位置);
        }

        if (记录次数 == 2)
        {
            红二位置 = 坐标计算.RotatePoint(new(100, 0, 80.5f), new(100, 0, 100), float.Pi / 4 * (envControlEvent.Index-1));
            //记录红二位置
            scriptEnv.KV.Add("红二位置", 红二位置);
            return true;
        }
        return false;
    }
}