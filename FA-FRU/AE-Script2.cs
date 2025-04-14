using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.JobApi;
using DDDacr.工具;

namespace FA_FRU;

public class AE_Script2 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if(condParams is not ReceviceAbilityEffectCondParams abilityEffectCondParams) return false;
        if (abilityEffectCondParams.ActionId != 15971) return false;
        if (scriptEnv.KV.ContainsKey("八方预占位"))
        {
            var pos = (Vector3)scriptEnv.KV["八方预占位"];
            var 转22度 = 坐标计算.RotatePoint(pos, Core.Me.TargetObject.Position, float.Pi / 4);
            延迟tp(转22度);
            return true;
        }

        if (condParams is OnMapEffectCreateEvent mapEffectCreateEvent)
        {
            
        }
        return false;
    }
    private static async Task 延迟tp(Vector3 pos)
    {
        await Task.Delay(1000);
        位移.Tp(pos);
    }
}