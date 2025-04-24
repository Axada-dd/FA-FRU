using System.Numerics;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;
using DDDacr.工具;

namespace FA_FRU.P2;

public class P2_白镜子处理 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not OnEnvControlEvent envControlEvent) return false;
        if (envControlEvent.Flag != 2) return false;
        var 白镜子坐标 = 坐标计算.RotatePoint(new(100, 0, 80.5f), new(100, 0, 100), float.Pi / 4 * (envControlEvent.Index-1));
        //共享白镜子坐标
        scriptEnv.KV.Add( "白镜子坐标", 白镜子坐标);
        //远程组去白镜子
        //获取白镜子的安全区
        var 起始点 = 坐标计算.CalculatePointOnLine(白镜子坐标, new (100, 0, 100), 2);
        RemoteControlHelper.SetPos("H1", 坐标计算.RotatePoint(起始点, 白镜子坐标, float.Pi / 4 * 2));
        RemoteControlHelper.SetPos("D3", 坐标计算.RotatePoint(起始点, 白镜子坐标, float.Pi / 4 * 3));
        RemoteControlHelper.SetPos("D4", 坐标计算.RotatePoint(起始点, 白镜子坐标, float.Pi / 4 * 5));
        RemoteControlHelper.SetPos("H2", 坐标计算.RotatePoint(起始点, 白镜子坐标, float.Pi / 4 * 6));
        //近战组去白镜子对面
        //MT拉BOSS去对面
        var MT坐标 = 坐标计算.RotatePoint(new(100, 0, 85f), new(100, 0, 100), float.Pi / 4 * (envControlEvent.Index-1+4));
        //共享MT坐标
        scriptEnv.KV.Add("MT坐标", MT坐标);
        RemoteControlHelper.SetPos("MT|ST|D1|D2", MT坐标);
        return true;
    }

    private static async void TpAction(string regex, Vector3 partyPos, int delay)
    {
        await Task.Delay(delay);
        RemoteControlHelper.SetPos(regex,partyPos);
    }
}