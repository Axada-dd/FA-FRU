using System.Numerics;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;
using DDDacr.工具;

namespace FA_FRU.P1;

public class P1_Open_Remote : ITriggerScript
{
    private Dictionary<string,Vector3> P1开场八方pos = new();
    private Dictionary<string,Vector3> P1开场八方nextpos = new();
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not EnemyCastSpellCondParams spellCondParams) return false;
        if (spellCondParams.SpellId != 40144 && spellCondParams.SpellId != 40148) return false;
        var spread = spellCondParams.SpellId == 40148;
        
        foreach (var player in PartyHelper.Party)
        {
            var playerRole = player.GetRoleByPlayerObjct();
            int myindex = playerRole switch
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
            P1开场八方pos.Add(playerRole,mPosEnd);
            P1开场八方nextpos.Add(playerRole,nextPos);
            RemoteControlHelper.SetPos(playerRole,mPosEnd);
            
        }
        if(!scriptEnv.KV.ContainsKey("P1开场八方pos")) scriptEnv.KV.Add("P1开场八方pos",P1开场八方pos);
        if(!scriptEnv.KV.ContainsKey("P1开场八方nextpos")) scriptEnv.KV.Add("P1开场八方nextpos",P1开场八方nextpos);
        return true;
    }
}