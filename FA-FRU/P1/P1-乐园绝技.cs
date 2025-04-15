using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;
using DDDacr.工具;
using ECommons;
using ECommons.Hooks;


namespace FA_FRU.P1;

public class P1_乐园绝技 : ITriggerScript
{
    int myindex = AI.Instance.PartyRole switch
    {
        "MT" => 0,
        "ST" => 1,
        "H1" => 2,
        "H2" => 3,
        "D4" => 7,
        "D1" => 4,
        "D2" => 5,
        "D3" => 6,
        _ => 6
    };
    int rot8 = AI.Instance.PartyRole switch
    {
        "MT" => 0,
        "ST" => 1,
        "H1" => 3,
        "H2" => 4,
        "D1" => 5,
        "D2" => 3,
        "D3" => 7,
        "D4" => 2,
        _ => 0,
    };
    private bool P1雾龙雷 = false;
    int[] P1雾龙记录 = [0, 0, 0, 0];
    
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is EnemyCastSpellCondParams spellCondParams && (spellCondParams.SpellId == 40155 || spellCondParams.SpellId == 40154))
        {
            
            if (spellCondParams.SpellId == 40155)
            {
                P1雾龙雷 = true;
            }
            else
            {
                P1雾龙雷 = false;
            }
            run乐园绝技(坐标计算.RotatePoint(new Vector3(100,0,85), new(100,0,100), float.Pi / 4 * rot8));
            
        }
        var 举手分身 = TargetMgr.Instance.Units.Values
            .Where(u => u.IsCasting&&u.DataId==17840).Select(u => u.EntityId+1).ToList();
        if (举手分身.Count is not 3) return false;
        var 隐身分身 = TargetMgr.Instance.Units.Values.Where(u => u.DataId == 17820 && 举手分身.Contains(u.EntityId)).ToList();
        if (隐身分身.Count is not 3) return false;
        Share.TrustDebugPoint.Clear();
        foreach (var VAR in 隐身分身)
        {
            Share.TrustDebugPoint.Add(VAR.Position);
            LogHelper.Print($"{VAR.Position}");
        }
        foreach (var obj in 隐身分身)
        {
            var dir8= 坐标计算.PositionTo8Dir(obj.Position, new(100, 0, 100));
            P1雾龙记录[dir8 % 4] = 1;
        }
        bool isH1group = myindex is 0 or 2 or 4 or 6;
        if(!P1雾龙雷)
        {
            var safeDir = P1雾龙记录.IndexOf(0);
            
            var rot8 = safeDir switch
            {
                0 => isH1group ? 0 : 4,
                1 => isH1group ? 5 : 1,
                2 => isH1group ? 6 : 2,
                3 => isH1group ? 7 : 3,
                _ => 0
            };
            var mPosEnd = 坐标计算.RotatePoint(new(100, 0, 84), new(100, 0, 100), float.Pi / 4 * rot8);
            位移.Tp(mPosEnd);
            mPosEnd.SharePoint();
            LogHelper.Print(mPosEnd.ToString());
        }
        else
        {
            var safeDir = P1雾龙记录.IndexOf(0);
            Vector3 p1 = new(100.0f, 0, 88.0f);
            Vector3 p2 = new(100.0f, 0, 80.5f);
            Vector3 p3 = new(106.5f, 0, 81.5f);
            Vector3 p4 = new(093.5f, 0, 81.5f);
            var rot8 = safeDir switch
            {
                0 => isH1group ? 0 : 4,
                1 => isH1group ? 5 : 1,
                2 => isH1group ? 6 : 2,
                3 => isH1group ? 7 : 3,
                _ => 0
            };
            var myPosA = myindex switch
            {
                0 => p2,
                1 => p2,
                2 => p1,
                3 => p1,
                4 => p3,
                5 => p3,
                6 => p4,
                7 => p4,
                _ => p1,
            };
            var mPosEnd = 坐标计算.RotatePoint(myPosA, new(100, 0, 100), float.Pi / 4 * rot8);
            位移.Tp(mPosEnd);
            mPosEnd.SharePoint();
            LogHelper.Print(mPosEnd.ToString());
        }

        return true;
    }
    private static async Task run乐园绝技(Vector3 pos)
    {
        await Task.Delay(4000);
        位移.Tp(pos);
    }
}