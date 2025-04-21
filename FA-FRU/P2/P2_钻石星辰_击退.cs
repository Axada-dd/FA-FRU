using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;
using DDDacr.工具;

namespace FA_FRU.P2;

public class P2_钻石星辰_击退 : ITriggerScript
{
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not ReceviceAbilityEffectCondParams abilityEffectCondParams) return false;
        if (abilityEffectCondParams.ActionId != 40199) return false;
        if (!scriptEnv.KV.ContainsKey("冰圈index")) return false;
        Share.TrustDebugPoint.Clear();
        Dictionary<string,Vector3> partyPos = new();
        var 冰圈  = (List<int>)scriptEnv.KV["冰圈index"];
        int firstIcicleImpact = 冰圈.First() % 4;
        int rotation = firstIcicleImpact switch
        {
            0 => 2,
            1 => -1,
            2 => 0,
            3 => 1,
        };
        for (int i = 0; i < 7; i++)
        {
            var playerRole = i switch
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
            bool inStGroup = ((int[]) [1, 3, 5, 7]).Contains(i);
            rotation += ((inStGroup) ? (4) : (0));
            var 击退位置 = 坐标计算.RotatePoint(new Vector3(95, 0, 100), new(100, 0, 100), float.Pi / 4 * rotation);
            partyPos.Add(playerRole, 击退位置);
        }
        TpAction(partyPos);
        return true;
    }
    private static async void TpAction(Dictionary<string, Vector3> partyPos)
    {
        await Task.Delay(1000);
        foreach (var pos in partyPos)
        {
            RemoteControlHelper.SetPos(pos.Key, pos.Value);
        }
    }
}