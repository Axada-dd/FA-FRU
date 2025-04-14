using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using DDDacr.工具;
using ECommons.GameFunctions;


namespace FA_FRU;

public class AE_Script1 : ITriggerScript
{
    int myindex = AI.Instance.PartyRole switch
    {
        "MT" => 0,
        "ST" => 1,
        "H1" => 2,
        "H2" => 3,
        "D1" => 4,
        "D2" => 5,
        "D3" => 6,
        "D4" => 7,
        _ => 3
    };
    int rot8 = AI.Instance.PartyRole switch
    {
        "MT" => 0,
        "ST" => 2,
        "H1" => 3,
        "H2" => 1,
        "D1" => 3,
        "D2" => 2,
        "D3" => 0,
        "D4" => 1,
        _ => 0,
    };
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if(condParams is not EnemyCastSpellCondParams spellCondParams) return false;
        if (spellCondParams.SpellId != 15971 ) return false;
        var tp坐标 = 坐标计算.RotatePoint(new Vector3(100,0,Core.Me.TargetObject.Position.Z-3),
            Core.Me.TargetObject.Position, float.Pi / 2 * rot8);
        if (!scriptEnv.KV.ContainsKey("八方预占位"))
        {
            scriptEnv.KV.Add("八方预占位", tp坐标);    
        }
        
        位移.Tp(tp坐标);
        Share.TrustDebugPoint.Add(new Vector3(100,0,100));
        return true;
    }

}