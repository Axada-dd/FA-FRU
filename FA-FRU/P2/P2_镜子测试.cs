using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using DDDacr.工具;

namespace FA_FRU.P2;

public class P2_镜子测试 : ITriggerScript
{
    private int 红一索引 = 0;
    private int 镜子记录 = 0;
    Vector3 镜子位置_白 = new(100, 0, 100);
    Vector3 镜子位置_红二 = new(100, 0, 100);
    Vector3 镜子位置_红一 = new(100, 0, 100);

    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not OnEnvControlEvent envControlEvent) return false;
        if (envControlEvent.Flag is not (2 or 512)) return false;
        镜子记录++;
        var index = envControlEvent.Index-1;
        if (envControlEvent.Flag == 2)
        {
            镜子位置_白 = 坐标计算.RotatePoint(new(100, 0, 80.5f), new(100, 0, 100), float.Pi / 4 * index);
            DebugPoint(镜子位置_白);
        }
        else
        {
            红一索引 = 镜子记录;
            
            if (红一索引 != 镜子记录)
            {
               镜子位置_红二 = 坐标计算.RotatePoint(new(100, 0, 80.5f), new(100, 0, 100), float.Pi / 4 * index); 
               DebugPoint(镜子位置_红二);
            }
            else
            {
                镜子位置_红一 = 坐标计算.RotatePoint(new(100, 0, 80.5f), new(100, 0, 100), float.Pi / 4 * index);
                DebugPoint(镜子位置_红一);
            }
        }
         
        
        
        return 镜子记录 == 3;
    }

    private void DebugPoint(Vector3 pos)
    {
        var 起始点 = 坐标计算.CalculatePointOnLine(pos, new Vector3(100, 0, 100), 2);
        for (int i = 2; i < 7; i++)
        {
            var 点位 = 坐标计算.RotatePoint(起始点, pos, float.Pi / 4 * i);
            Share.TrustDebugPoint.Add(点位);
        }
    }
}