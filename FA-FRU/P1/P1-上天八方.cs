using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;
using DDDacr.工具;

namespace FA_FRU.P1;

public class P1_上天八方 : ITriggerScript
{
    private Dictionary<string,Vector3> P1上天八方pos = new();
    private Dictionary<string,Vector3> P1上天八方nextpos = new();
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not EnemyCastSpellCondParams spellCondParams) return false;
        if (spellCondParams.SpellId != 40329 && spellCondParams.SpellId != 40330) return false;
        var spread = spellCondParams.SpellId == 40330;
        Share.TrustDebugPoint.Clear();
        for (int index = 0; index < 8; index++)
        {
            var rot8 = index switch
            {
                0 => 0,
                1 => 2,
                2 => 6,
                3 => 4,
                4 => 5,
                5 => 3,
                6 => 7,
                7 => 1,
                _ => 0    
            };
            var playerRole = index switch
            {
                0 => "MT",
                1 => "ST",
                2 => "H1",
                3 => "H2",
                4 => "D1",
                5 => "D2",
                6 => "D3",
                7 => "D4",
                _ => "H1"
            };
            var outPoint = spread && (index == 2 || index == 3 || index == 6 || index == 7);
            var inPoint = !spread && (index == 0 || index == 1 || index == 2 || index == 3);
            var isTank = spread && (index == 0 || index == 1);
            var mPosEnd = 坐标计算.RotatePoint(outPoint ? new(100, 0, 85) : new(100, 0, 95), new(100, 0, 100), float.Pi / 4 * rot8);
            var nextPos=坐标计算.RotatePoint(mPosEnd, new(100, 0, 100), (inPoint || isTank) ? -float.Pi / 8 : float.Pi/8);
            P1上天八方pos.Add(playerRole,mPosEnd);
            P1上天八方nextpos.Add(playerRole,nextPos);
            //RemoteControlHelper.LockPos(playerRole,mPosEnd,4000);
            
        }
        Acton(P1上天八方pos);
        if(!scriptEnv.KV.ContainsKey("P1上天八方pos")) scriptEnv.KV.Add("P1上天八方pos",P1上天八方pos);
        if(!scriptEnv.KV.ContainsKey("P1上天八方nextpos")) scriptEnv.KV.Add("P1上天八方nextpos",P1上天八方nextpos);
        return true;
    }
    private static async void Acton(Dictionary<string, Vector3> partyPos)
    {
        await Task.Delay(4000);
        foreach (var pos in partyPos)
        {
            RemoteControlHelper.LockPos(pos.Key, pos.Value, 1000);
        }
    }
    
}