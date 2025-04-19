using System.Numerics;
using System.Text.RegularExpressions;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;
using ECommons;

namespace FA_FRU.P1;

public class P1_塔记录 : ITriggerScript
{
    List<int> P1塔 = [0, 0, 0, 0];
    private int _times = 0;
    

    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if(condParams is not EnemyCastSpellCondParams spellCondParams) return false;
        Regex myRegex = new Regex(@"(4012[234567]|4013[15])");
        if (!myRegex.IsMatch(spellCondParams.SpellId.ToString())) return false;
        _times++;
        var pos = spellCondParams.CastPos;
        var count = spellCondParams.SpellId switch
        {
            40135 => 1,
            40131 => 1,
            40122 => 2,
            40123 => 3,
            40124 => 4,
            40125 => 2,
            40126 => 3,
            40127 => 4,
        };
        if (MathF.Abs(pos.Z - 100) < 1)
        {
            P1塔[1] = count;
        }
        else
        {
            if (pos.Z - 100 > 1)
            {
                P1塔[2] = count;
            }
            else
            {
                P1塔[0] = count;
            }
        }
        if (pos.X - 100 > 1)
        {
            P1塔[3] = 1;
        }
        
        if (_times == 3)
        {
            LogHelper.Print(P1塔.Print());
            if(!scriptEnv.KV.ContainsKey("P1塔")) scriptEnv.KV.Add("P1塔",P1塔);
            return true;
        };
        return false;
    }
}