using System.Numerics;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;
using DDDacr.工具;

namespace FA_FRU.P2;

public class P2_红镜子处理 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if(!scriptEnv.KV.ContainsKey("红一位置"))return false;
        if(!scriptEnv.KV.ContainsKey("红二位置"))return false;
        if(!scriptEnv.KV.ContainsKey("白镜子坐标"))return false;
        if(!scriptEnv.KV.ContainsKey("MT坐标"))return false;
        var 红一位置 = (Vector3)scriptEnv.KV["红一位置"];
        var 红二位置 = (Vector3)scriptEnv.KV["红二位置"];
        var 白镜子坐标 = (Vector3)scriptEnv.KV["白镜子坐标"];
        var MT坐标 = (Vector3)scriptEnv.KV["MT坐标"];
        var 红一右 = 坐标计算.PositionTo8Dir(红一位置,  红二位置);
        if ((MT坐标 - 红一位置).Length() <= (MT坐标 - 红二位置).Length())
        {
            //近战组去红一
            RemoteControlHelper.SetPos("ST", 坐标计算.RotatePoint(坐标计算.CalculatePointOnLine(红一位置,new Vector3(100, 0, 100), 2), 红一位置, float.Pi / 4 * 2));
            RemoteControlHelper.SetPos("MT", 坐标计算.RotatePoint(坐标计算.CalculatePointOnLine(红一位置,new Vector3(100, 0, 100), 2), 红一位置, float.Pi / 4 * 6));
            
            //远程组去红二
            RemoteControlHelper.SetPos("H1", 坐标计算.RotatePoint(坐标计算.CalculatePointOnLine(红二位置,new Vector3(100, 0, 100), 2), 红二位置, float.Pi / 4 * 6));
            RemoteControlHelper.SetPos("H2", 坐标计算.RotatePoint(坐标计算.CalculatePointOnLine(红二位置,new Vector3(100, 0, 100), 2), 红二位置, float.Pi / 4 * 2));
            return true;

        }
        else
        {
            //近战组去红二
            RemoteControlHelper.SetPos("ST", 坐标计算.RotatePoint(坐标计算.CalculatePointOnLine(红二位置,new Vector3(100, 0, 100), 2), 红二位置, float.Pi / 4 * 2));
            RemoteControlHelper.SetPos("MT", 坐标计算.RotatePoint(坐标计算.CalculatePointOnLine(红二位置,new Vector3(100, 0, 100), 2), 红二位置, float.Pi / 4 * 6));
            
            //远程组去红一
            RemoteControlHelper.SetPos("H1", 坐标计算.RotatePoint(坐标计算.CalculatePointOnLine(红一位置,new Vector3(100, 0, 100), 2), 红一位置, float.Pi / 4 * 6));
            RemoteControlHelper.SetPos("H2", 坐标计算.RotatePoint(坐标计算.CalculatePointOnLine(红一位置,new Vector3(100, 0, 100), 2), 红一位置, float.Pi / 4 * 2));
            return true;
        }
        return false;
    }
}