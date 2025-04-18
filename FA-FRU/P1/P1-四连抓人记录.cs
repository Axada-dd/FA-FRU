using System.Numerics;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;
using DDDacr.工具;
using ECommons;

namespace FA_FRU.P1;

public class P1_四连抓人记录 : ITriggerScript
{
    private List<string> GroupOrder = new () { "H1", "H2", "MT", "ST", "D1", "D2", "D3", "D4" };
    private List<IGameObject> 点名目标 = new();
    static float dis = 2.5f;//距离点名人
    static float far = 5.25f;//距离boss
    Vector3 t1p1 = new(100, 0, 100 - far);
    Vector3 t1p2 = new(100, 0, 100 - far - dis);
    Vector3 t2p1 = new(100, 0, 100 + far);
    Vector3 t2p2 = new(100, 0, 100 + far + dis);
    Vector3 t3p1 = new(100, 0, 100 - far - dis);
    Vector3 t3p2 = new(100, 0, 100 - far);
    Vector3 t4p1 = new(100, 0, 100 + far + dis);
    Vector3 t4p2 = new(100, 0, 100 + far);
    Vector3 i1p1 = new (100, 0, 100);
    Vector3 i1p2 = new (100, 0, 100);
    Vector3 i2p1 = new (100, 0, 100);
    Vector3 i2p2 = new (100, 0, 100);
    Vector3 i3p1 = new (100, 0, 100);
    Vector3 i3p2 = new (100, 0, 100);
    Vector3 i4p1 = new (100, 0, 100);
    Vector3 i4p2 = new (100, 0, 100);
    private List<bool> 连线雷火顺序 = [];//火：true
    private List<KeyValuePair<string,Vector3>> 队伍tp位置 = [];//存储nextpos
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not TetherCondParams tetherCondParams) return false;
        if (tetherCondParams.Args0 is not (249 or 287)) return false;
        var 点名 = tetherCondParams.Right;
        点名目标.Add(点名);
        GroupOrder.Remove(点名.GetRoleByPlayerObjct());
        LogHelper.Print($"{点名目标.Count}号点名目标：{点名目标.Last().Name}，职能：{点名目标.Last().GetRoleByPlayerObjct()}，{(tetherCondParams.Args0==249?"火":"雷")}");
        连线雷火顺序.Add(tetherCondParams.Args0==249);
        
        if(点名目标.Count !=4) return false;
        i1p1 = 连线雷火顺序[0] ? new(100, 0, 100 - far - dis) : new(100 + dis, 0, 100 - far);
        i1p2 = 连线雷火顺序[2] ? new(100, 0, 100 - far - dis) : new(100 + dis, 0, 100 - far);
        i2p1 = 连线雷火顺序[0] ? new(100, 0, 100 - far - dis) : new(100 - dis, 0, 100 - far);
        i2p2 = 连线雷火顺序[2] ? new(100, 0, 100 - far - dis) : new(100 - dis, 0, 100 - far);
        i3p1 = 连线雷火顺序[1] ? new(100, 0, 100 + far + dis) : new(100 - dis, 0, 100 + far);
        i3p2 = 连线雷火顺序[3] ? new(100, 0, 100 + far + dis) : new(100 - dis, 0, 100 + far);
        i4p1 = 连线雷火顺序[1] ? new(100, 0, 100 + far + dis) : new(100 + dis, 0, 100 + far);
        i4p2 = 连线雷火顺序[3] ? new(100, 0, 100 + far + dis) : new(100 + dis, 0, 100 + far);
        RemoteControlHelper.SetPos(点名目标[0].GetRoleByPlayerObjct(),t1p1);
        RemoteControlHelper.SetPos(点名目标[1].GetRoleByPlayerObjct(),t2p1);
        RemoteControlHelper.SetPos(点名目标[2].GetRoleByPlayerObjct(),t3p1);
        RemoteControlHelper.SetPos(点名目标[3].GetRoleByPlayerObjct(),t4p1);
        RemoteControlHelper.SetPos(GroupOrder[0],i1p1);
        RemoteControlHelper.SetPos(GroupOrder[1],i2p1);
        RemoteControlHelper.SetPos(GroupOrder[2],i3p1);
        RemoteControlHelper.SetPos(GroupOrder[3],i4p1);
        //上组4人
        队伍tp位置.Add(new (点名目标[0].GetRoleByPlayerObjct(), t1p1));
        队伍tp位置.Add(new (点名目标[2].GetRoleByPlayerObjct(), t3p2));
        队伍tp位置.Add(new (GroupOrder[0], i1p2));
        队伍tp位置.Add(new (GroupOrder[1], i2p2));
        //下组4人
        队伍tp位置.Add(new(点名目标[1].GetRoleByPlayerObjct(), t2p2));
        队伍tp位置.Add(new(点名目标[3].GetRoleByPlayerObjct(), t4p2));
        队伍tp位置.Add(new(GroupOrder[2], i3p2));
        队伍tp位置.Add(new(GroupOrder[3], i4p2));
        
        if (!scriptEnv.KV.ContainsKey("P1四连抓人"))
        {
           scriptEnv.KV.Add("P1四连抓人",队伍tp位置); 
        }
        LogHelper.Print($"点名玩家：{点名目标.Select(e=>e.GetRoleByPlayerObjct()).Print()}");
        LogHelper.Print("剩余玩家: " + GroupOrder.Print());
        return true;
    }
    
}