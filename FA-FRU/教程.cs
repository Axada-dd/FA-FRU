using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;
using DDDacr.工具;

namespace FA_FRU;

public class 教程 : ITriggerScript
{

    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        
        if (condParams is UnitCreateCondParams unitCreateCondParams)
        {
            LogHelper.Print(unitCreateCondParams.BattleChara.Position.ToString());
            if (Core.Me.TargetObject != null)
            {
                var tp坐标 = 坐标计算.RotatePoint(unitCreateCondParams.BattleChara.Position,
                    Core.Me.TargetObject.Position, float.Pi / 4);
                位移.Tp(tp坐标);
            }


            return true;
        }
        return false;
        
    }

}
