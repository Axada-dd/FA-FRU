using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;
using DDDacr.工具;

namespace FA_FRU.P2;

public class P2_钻石星辰_引导扇形 : ITriggerScript
{
    List<int> DPS_Group = [4, 5, 6, 7];
    List<int> TH_Group = [0, 1, 2, 3];
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (!scriptEnv.KV.ContainsKey("P2_钻石星尘_钢铁") ) return false;
        if(!scriptEnv.KV.ContainsKey("P2_冰花点D") ) return false;
        if(!scriptEnv.KV.ContainsKey("冰圈index") ) return false;
        var P2_钻石星辰_钢铁 = (bool)scriptEnv.KV["P2_钻石星尘_钢铁"];
        var P2_冰花点D = (bool)scriptEnv.KV["P2_冰花点D"];
        var 冰圈index = (List<int>)scriptEnv.KV["冰圈index"];
        var 冰圈斜点 = 冰圈index.FirstOrDefault() % 4 is (0 or 2);
        Share.TrustDebugPoint.Clear();
        //冰花点名处理
        冰花点名处理(P2_冰花点D ? DPS_Group : TH_Group, P2_钻石星辰_钢铁, 冰圈斜点);
        //扇形引导
        扇形引导(P2_冰花点D ? TH_Group: DPS_Group, P2_钻石星辰_钢铁, 冰圈斜点);
            
        return true;
    }

    private void 冰花点名处理(List<int> group, bool 钢铁, bool 冰圈斜点)
    {
        Dictionary<string, Vector3> partyPos = [];
        Dictionary<string, Vector3> partyPos_c = [];
        for (int i = 0; i < 4; i++)
        {
            var rot = group[i] switch
            {
                0 => 6,
                1 => 0,
                2 => 4,
                3 => 2,
                4 => 4,
                5 => 2,
                6 => 6,
                7 => 0,
                _ => 0,
            }; 
            var playerRole = group[i] switch
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
            Vector3 epos1 = 钢铁 ? new(119.5f, 0, 100.0f) : new(103.5f, 0, 100.0f);
            Vector3 epos2 = 钢铁 ? new(119.5f, 0, 100.0f) : new(108.0f, 0, 100.0f);
            var dealpos1 = 坐标计算.RotatePoint(epos1, new(100, 0, 100), float.Pi / 4 * (rot + (冰圈斜点 ? -1:0)));
            partyPos_c.Add(playerRole, dealpos1);
            var dealpos2 = 坐标计算.RotatePoint(epos2, new(100, 0, 100), float.Pi / 4 * (rot + (冰圈斜点 ? -1:0)));
            partyPos.Add(playerRole, dealpos2);
            
                
        }
        TPaction(partyPos_c, 钢铁 ? 100:2000);
        TPaction(partyPos, 5500);
    }

    private void 扇形引导(List<int> group, bool 钢铁, bool 冰圈斜点)
    {
        Dictionary<string, Vector3> partyPos = [];
        for (int i = 0; i < 4; i++)
        {
            var rot = group[i] switch
            {
                0 => 6,
                1 => 0,
                2 => 4,
                3 => 2,
                4 => 4,
                5 => 2,
                6 => 6,
                7 => 0,
                _ => 0,
            };
            var playerRole = group[i] switch
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
            Vector3 epos = 钢铁 ? new(116.5f, 0, 100f) : new(101f, 0, 100f);
            var dealpos = 坐标计算.RotatePoint(epos, new(100, 0, 100), float.Pi / 4 * (rot + (冰圈斜点 ? 0:-1)));
            RemoteControlHelper.SetPos(playerRole, dealpos);
            partyPos.Add(playerRole,new Vector3(100,0,100));
                
        }
        if(钢铁)TPaction(partyPos,3500);
    }

    private async void TPaction(Dictionary<string, Vector3> partyPos, int delay)
    {
        await Task.Delay(delay);
        foreach (var pos in partyPos)
        {
            RemoteControlHelper.SetPos(pos.Key,pos.Value);
        }
        
    }
}