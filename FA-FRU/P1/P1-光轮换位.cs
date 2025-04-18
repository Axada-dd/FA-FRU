using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;
using DDDacr.工具;

namespace FA_FRU.P1;

public class P1_光轮换位 : ITriggerScript
{
    private List<IGameObject> 点名目标 = new();
    List<int> P1转轮召抓人 = [0, 0, 0, 0, 0, 0, 0, 0];
    List<int> upGroup = [];
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not TetherCondParams tetherCondParams) return false;
        if (tetherCondParams.Args0 is not (249 or 287)) return false;
        点名目标.Add(tetherCondParams.Right);

        P1转轮召抓人[tetherCondParams.Right.GetRoleByPlayerObjctIndex()] = 1;
        if(点名目标.Count != 2) return false;
        if (!scriptEnv.KV.ContainsKey("P1光轮右安全")) return false;
        var atEast = (bool)scriptEnv.KV["P1光轮右安全"];
        var o1 = P1转轮召抓人.IndexOf(1);
        var o2 = P1转轮召抓人.LastIndexOf(1);
        upGroup.Add(o1);
        if (o1 != 1 && o2 != 1) upGroup.Add(1);
        if (o1 != 2 && o2 != 2) upGroup.Add(2);
        if (o1 != 3 && o2 != 3) upGroup.Add(3);
        if (upGroup.Count < 4 && o1 != 0 && o2 != 0) upGroup.Add(0);
        if (upGroup.Count < 4 && o1 != 4 && o2 != 4) upGroup.Add(4);
        foreach (var plager in PartyHelper.Party)
        {
            var playerIndex = plager.GetRoleByPlayerObjctIndex();
            var dealpos1 = new Vector3(atEast ? 105.5f : 94.5f, 0, upGroup.Contains(playerIndex) ? 93 : 107);
            var dealpos2 = new Vector3(atEast ? 102 : 98, 0, upGroup.Contains(playerIndex) ? 93 : 107);

        }
        return true;
    }
}