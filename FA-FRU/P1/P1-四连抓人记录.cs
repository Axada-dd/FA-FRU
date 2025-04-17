using AEAssist.CombatRoutine.Module.Target;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;
using DDDacr.工具;

namespace FA_FRU.P1;

public class P1_四连抓人记录 : ITriggerScript
{
    private List<string> GroupTH = new List<string>() { "MT", "ST", "H1", "H2"};
    private List<string> GroupD = new List<string>() { "D1", "D2", "D3", "D4" };
    private List<IGameObject> 点名目标 = new();
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not TetherCondParams tetherCondParams) return false;
        if (tetherCondParams.Args0 is not (249 or 287)) return false;
        点名目标.Add(tetherCondParams.Right);
        if (点名目标.Count == 1)
        {
            
            LogHelper.Print($"1号点名目标：{点名目标.Last().Name}，职能：{点名目标.Last().GetRoleByPlayerObjct()}，{(tetherCondParams.Args0==249?"火":"雷")}");
        }

        if (点名目标.Count == 2)
        {

            LogHelper.Print($"2号点名目标：{点名目标.Last().Name}，职能：{点名目标.Last().GetRoleByPlayerObjct()}，{(tetherCondParams.Args0==249?"火":"雷")}");    
        }

        if (点名目标.Count == 3)
        {
            LogHelper.Print($"{点名目标.Last().GetObjcetJobName()}");

           LogHelper.Print($"3号点名目标：{点名目标.Last().Name}，职能：{点名目标.Last().GetRoleByPlayerObjct()}，{(tetherCondParams.Args0==249?"火":"雷")}"); 
        }
        if(点名目标.Count !=4) return false;
        if (点名目标.Count == 4)
        {
            foreach (var 点名 in 点名目标)
            {
                if (点名.IsDpsRe())
                {
                    GroupD.Remove(点名目标.Last().GetRoleByPlayerObjct());
                }
                else
                {
                    GroupTH.Remove(点名目标.Last().GetRoleByPlayerObjct());
                }
            }
            LogHelper.Print($"4号点名目标：{点名目标.Last().Name}，职能：{点名目标.Last().GetRoleByPlayerObjct()}，{(tetherCondParams.Args0==249?"火":"雷")}");     
        }
        LogHelper.Print("剩余玩家: " + string.Join(",", GroupTH) + " " + string.Join(",", GroupD));
        return true;
    }
    
}