using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;
using DDDacr.工具;
using ECommons;

namespace FA_FRU.初始化;

public class 队伍职能获取 : ITriggerScript
{
    private Dictionary<string, string> partyRole = new Dictionary<string, string>();
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not ReceviceAbilityEffectCondParams abilityEffectCondParams) return false;
        if (abilityEffectCondParams.ActionId is not (40144 or 40148)) return false;
        foreach (var member in PartyHelper.Party)
        {
            partyRole.Add(member.Name.TextValue, 坐标计算.PositionTo8Dir(member.Position,new (100,0,100)) switch
            {
                0 => "MT",1=>"D4",2=>"ST",3=>"D2",4=>"H2",5=>"D1",6=>"H1",7=>"D3",
            });
        }
        LogHelper.Print($"{PartyHelper.Party.ToList().Select(e=>e.GetObjcetJobName()).Print()}");
        LogHelper.Print($"{partyRole.Values.Print()}");
        if(!scriptEnv.KV.ContainsKey("PartyRole"))scriptEnv.KV.Add("PartyRole", partyRole);
        return true;
    }
}