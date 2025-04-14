using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using DDDacr.工具;
using ECommons;

namespace FA_FRU.P1;

public class P1_雾龙安全区 : ITriggerScript
{
    int[] P1雾龙记录 = [0, 0, 0, 0];
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if(condParams is not EnemyCastSpellCondParams spellCondParams) return false;
        
        
        if (spellCondParams.SpellId == 40158)
        {
            if (!scriptEnv.KV.ContainsKey("隐身分身")) return false;
            if (!scriptEnv.KV.ContainsKey("P1雾龙雷")) return false;
            var P1雾龙雷 = (bool)scriptEnv.KV["P1雾龙雷"];
            var 隐身分身 = (List<IBattleChara>)scriptEnv.KV["隐身分身"];
            var 举手分身 = TargetMgr.Instance.Units.Values
                .Where(u => u.IsCasting&&u.DataId==17840).Select(u => u.EntityId).ToList();
            var isH1group = true;
            var myindex = 6;
            if(举手分身.Count == 3)
            {
                foreach (var entityid in 举手分身)
                {
                    隐身分身 = 隐身分身.Where(u=>u.EntityId == (entityid+1)).ToList();
                }
                foreach (var obj in 隐身分身)
                {
                    var dir8= PositionTo8Dir(obj.Position, new(100, 0, 100));
                    P1雾龙记录[dir8 % 4] = 1;
                }
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
                    var mPosEnd = RotatePoint(new(100, 0, 84), new(100, 0, 100), float.Pi / 4 * rot8);
                    位移.Tp(mPosEnd);
                    mPosEnd.SharePoint();
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
                    var mPosEnd = RotatePoint(myPosA, new(100, 0, 100), float.Pi / 4 * rot8);
                    位移.Tp(mPosEnd);
                    mPosEnd.SharePoint();
                }

                return true;
            }
        }

        return false;
    }
    private int PositionTo8Dir(Vector3 point, Vector3 centre)
    {
        // Dirs: N = 0, NE = 1, ..., NW = 7
        var r = Math.Round(4 - 4 * Math.Atan2(point.X - centre.X, point.Z - centre.Z) / Math.PI) % 8;
        return (int)r;

    }
    private Vector3 RotatePoint(Vector3 point, Vector3 centre, float radian)
    {

        Vector2 v2 = new(point.X - centre.X, point.Z - centre.Z);

        var rot = (MathF.PI - MathF.Atan2(v2.X, v2.Y) + radian);
        var lenth = v2.Length();
        return new(centre.X + MathF.Sin(rot) * lenth, centre.Y, centre.Z - MathF.Cos(rot) * lenth);
    }
}