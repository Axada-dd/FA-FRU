using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Extension;
using AEAssist.Helper;

namespace DSR.绝亚;

public class P1小怪 : ITriggerScript
{
    // 定义一个Vector3类型的变量pos0，表示坐标(100, 0, 100)
    private readonly Vector3 pos0 = new(100, 0, 100);

    // 检查条件是否满足
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        // 获取所有DataId为11337的单位的坐标
        var towers = TargetMgr.Instance.Units.Values
            .Where(e => e.DataId is 11337)
            .Select(e => e.Position)
            .ToList();
        // 如果数量不是3，返回false
        if (towers.Count is not 3) return false;

        // 找到三个坐标的右角坐标
        var basePos = FindRightAngle(towers[0], towers[1], towers[2]);

        // 获取所有DataId为11338的单位的坐标，并按与pos0的顺时针角度排序
        var enemies = TargetMgr.Instance.Enemys.Values
            .Where(e => e.DataId is 11338)
            .OrderBy(e => GetAngleClockwise(pos0, basePos, e.Position))
            .ToList();
        // 如果数量不是4，返回false
        if (enemies.Count is not 4) return false;

        // 根据AI.Instance.PartyRole的值，选择目标单位
        var target = AI.Instance.PartyRole switch
        {
            "D4" => enemies[0],
            "D1" => enemies[1],
            "D2" => enemies[2],
            "D3" => enemies[3],
            _ => enemies[0]
        };

        // 设置目标单位
        Core.Me.SetTarget(target);
        // 打印AI.Instance.PartyRole的值
        LogHelper.Print(AI.Instance.PartyRole);
        // 打印目标单位的坐标
        LogHelper.Print(target.Position.ToString());

        // 返回true
        return true;
    }

    // 找到三个坐标的右角坐标
    private static Vector3 FindRightAngle(Vector3 A, Vector3 B, Vector3 C)
    {
        // 计算AB、BC、CA的平方
        var AB2 = Vector3.DistanceSquared(A, B);
        var BC2 = Vector3.DistanceSquared(B, C);
        var CA2 = Vector3.DistanceSquared(C, A);

        // 如果AB的平方最大，返回C
        if (AB2 >= BC2 && AB2 >= CA2)
        {
            return C;
        }

        // 如果BC的平方最大，返回A
        if (BC2 >= AB2 && BC2 >= CA2)
        {
            return A;
        }

        // 否则返回B
        return B;
    }

    // 计算target相对于pivot和reference的顺时针角度
    private static float GetAngleClockwise(Vector3 pivot, Vector3 reference, Vector3 target)
    {
        // 计算baselineVec和targetVec
        var baselineVec = reference - pivot;
        var targetVec = target - pivot;

        // 计算baselineAngle和targetAngle
        var baselineAngle = Math.Atan2(baselineVec.X, baselineVec.Z) * (180.0 / Math.PI);
        var targetAngle = Math.Atan2(targetVec.X, targetVec.Z) * (180.0 / Math.PI);

        // 将baselineAngle和targetAngle转换为0-360的值
        baselineAngle = (baselineAngle + 360.0) % 360.0;
        targetAngle = (targetAngle + 360.0) % 360.0;

        // 计算diffAngleCW
        var diffAngleCW = baselineAngle - targetAngle;

        // 将diffAngleCW转换为0-360的值
        diffAngleCW = (diffAngleCW + 360.0) % 360.0;

        // 返回diffAngleCW
        return (float)diffAngleCW;
    }
}
