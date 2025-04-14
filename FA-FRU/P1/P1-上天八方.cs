using System.Numerics;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using DDDacr.工具;

namespace FA_FRU.P1;

public class P1_上天八方 : ITriggerScript
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
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not EnemyCastSpellCondParams spellCondParams) return false;
        if (spellCondParams.SpellId != 40329 && spellCondParams.SpellId != 40330) return false;
        var spread = spellCondParams.SpellId == 40330;
        var rot8 = myindex switch
        {
            0 => 0,
            1 => 2,
            2 => 6,
            3 => 4,
            4 => 5,
            5 => 3,
            6 => 7,
            7 => 1,
            _ => 0,
        };

        var outPoint = spread && (myindex == 2 || myindex == 3 || myindex == 6 || myindex == 7);
        var inPoint = !spread && (myindex == 0 || myindex == 1 || myindex == 2 || myindex == 3);
        var isTank = spread && (myindex == 0 || myindex == 1);
        var mPosEnd = 坐标计算.RotatePoint(outPoint ? new(100, 0, 85) : new(100, 0, 95), new(100, 0, 100), float.Pi / 4 * rot8);
        var nextPos=坐标计算.RotatePoint(mPosEnd, new(100, 0, 100), (inPoint || isTank) ? -float.Pi / 8 : float.Pi/8);
        if (!scriptEnv.KV.ContainsKey("P1上天八方pos"))
        {
            scriptEnv.KV.Add("P1上天八方pos", mPosEnd);
        }

        if (!scriptEnv.KV.ContainsKey("P1上天八方nextpos"))
        {
            scriptEnv.KV.Add("P1上天八方nextpos", nextPos);    
        }
        runTp(mPosEnd);
        return true;
    }

    private static async Task runTp(Vector3 pos)
    {
       await Task.Delay(3000);
       位移.Tp(pos);
    }
}