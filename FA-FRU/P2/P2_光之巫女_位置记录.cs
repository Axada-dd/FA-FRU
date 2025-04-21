using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;
using DDDacr.工具;
using ECommons;

namespace FA_FRU.P2;

public class P2_光之巫女_位置记录 : ITriggerScript
{
    
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not EnemyCastSpellCondParams spellCondParams) return false;
        if (spellCondParams.SpellId != 40208) return false;
        LogHelper.Print(spellCondParams.CastPos.ToString());
        Share.TrustDebugPoint.Clear();
        var 起跑点1 = 坐标计算.RotatePoint(坐标计算.CalculatePointOnLine(spellCondParams.CastPos, new Vector3(100,0,100),8), new Vector3(100, 0, 100), float.Pi / 8);
        var 起跑点2 = 坐标计算.RotatePoint(坐标计算.CalculatePointOnLine(spellCondParams.CastPos, new Vector3(100,0,100),8), new Vector3(100, 0, 100), -float.Pi / 8 );
        Share.TrustDebugPoint.Add(起跑点1);
        Share.TrustDebugPoint.Add(起跑点2);
        scriptEnv.KV.Add("起跑点MT组", 起跑点1);
        scriptEnv.KV.Add("起跑点ST组", 起跑点2);
        return true;
    }
}