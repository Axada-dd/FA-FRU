using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;
using DDDacr.工具;

namespace FA_FRU.P1;

public class P1_光轮换位_me : ITriggerScript
{
    private List<IGameObject> 点名目标 = new();
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not TetherCondParams tetherCondParams) return false;
        if (tetherCondParams.Args0 is not (249 or 287)) return false;
        点名目标.Add(tetherCondParams.Right);
        if(点名目标.Count != 2) return false;
        var 点名目标Name = 点名目标.Select(e=> e.Name.TextValue).ToList();
        if (!scriptEnv.KV.ContainsKey("P1光轮右安全")) return false;
        var P1光轮右安全 = (bool)scriptEnv.KV["P1光轮右安全"];
        LogHelper.Print("点名目标: " + string.Join(",", 点名目标Name));
        if (点名目标.Select(e => e.IsMe()).Contains(true))//我是点名目标
        {
            if (点名目标.All(e => e.IsDpsRe()))
            {
                if (AI.Instance.PartyRole == "D1")//我是D1
                {
                    LogHelper.Print("换到上面");
                    new Vector3(P1光轮右安全 ? 105.5f : 94.5f, 0, 93).SharePoint();
                }new Vector3(P1光轮右安全 ? 105.5f : 94.5f, 0, 107).SharePoint();
                
            }
            else if (点名目标.All(e => e.IsTankOrHealer()))
            {
                if (AI.Instance.PartyRole == "MT") //我是MT
                {
                   LogHelper.Print("换到下面");
                   new Vector3(P1光轮右安全 ? 105.5f : 94.5f, 0, 107).SharePoint();
                }new Vector3(P1光轮右安全 ? 105.5f : 94.5f, 0, 93).SharePoint();
            }
        }
        else
        {
            if (点名目标.All(e => e.IsDpsRe()))//点名DPS
            {
                if (AI.Instance.PartyRole == "MT")
                {
                    LogHelper.Print("换到下面");
                    new Vector3(P1光轮右安全 ? 105.5f : 94.5f, 0, 107).SharePoint();
                }new Vector3(P1光轮右安全 ? 105.5f : 94.5f, 0, 93).SharePoint();
            }
            else if (点名目标.All(e => e.IsTankOrHealer()))//点名T奶
            {
                if (AI.Instance.PartyRole == "D1")
                {
                    LogHelper.Print("换到上面");
                    new Vector3(P1光轮右安全 ? 105.5f : 94.5f, 0, 93).SharePoint();
                } new Vector3(P1光轮右安全 ? 105.5f : 94.5f, 0, 107).SharePoint();
            }
        }
        return true;
    }
}