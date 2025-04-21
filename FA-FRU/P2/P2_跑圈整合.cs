using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Helper;
using DDDacr.工具;

namespace FA_FRU.P2;

public class P2_跑圈整合 : ITriggerScript
{
    private bool flag_坐标计算 = true;
    private bool flag_起跑 = false;
    private bool flag_跑圈 = false;
    private List<Vector3> 跑圈点位_MT组 = [];
    private List<Vector3> 跑圈点位_ST组 = [];
    private int 跑圈点位计数 = 0;
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (flag_坐标计算)
        {
            if (condParams is not EnemyCastSpellCondParams spellCondParams) return false;
            if (spellCondParams.SpellId != 40208) return false;
            LogHelper.Print(spellCondParams.CastPos.ToString());
            Share.TrustDebugPoint.Clear();
            var 起跑点1 = 坐标计算.RotatePoint(坐标计算.CalculatePointOnLine(spellCondParams.CastPos, new Vector3(100,0,100),8), new Vector3(100, 0, 100), float.Pi / 8);
            var 起跑点2 = 坐标计算.RotatePoint(坐标计算.CalculatePointOnLine(spellCondParams.CastPos, new Vector3(100,0,100),8), new Vector3(100, 0, 100), -float.Pi / 8 );
            跑圈点位_MT组.Add(起跑点1);
            跑圈点位_ST组.Add(起跑点2);
            for (int i = 1; i < 4; i++)
            {
                跑圈点位_MT组.Add(坐标计算.RotatePoint(起跑点1,new (100,0,100),float.Pi/8*i));
                跑圈点位_ST组.Add(坐标计算.RotatePoint(起跑点2,new (100,0,100),-float.Pi/8*i));
            }
            flag_坐标计算 = false;
            flag_起跑 = true;
            return false;
        }

        if (flag_起跑)
        {
            if(condParams is not ReceviceNoTargetAbilityEffectCondParams abilityEffectCondParams) return false;
            if (abilityEffectCondParams.ActionId is not (40200 or 40201)) return false;
            TpAction("MT|H1|D1|D3", 跑圈点位_MT组[0],100);
            TpAction("ST|H2|D2|D4", 跑圈点位_ST组[0],100);
            flag_起跑 = false;
            flag_跑圈 = true;
        }

        if (flag_跑圈)
        {
            
            if (condParams is not ReceviceAbilityEffectCondParams abilityEffectCondParams) return false;
            if (abilityEffectCondParams.ActionId != 40209) return false;
            跑圈点位计数++;
            /*if(abilityEffectCondParams.Target == null) return false;
            if (abilityEffectCondParams.Target.DataId != PartyHelper.Party.ToList().First().DataId) return false;*/
            TpAction("MT|H1|D1|D3", 跑圈点位_MT组[1], 300);
            TpAction("ST|H2|D2|D4", 跑圈点位_ST组[1], 300);
            
            TpAction("MT|H1|D1|D3", 跑圈点位_MT组[2], 2100);
            TpAction("ST|H2|D2|D4", 跑圈点位_ST组[2], 2100);
            
            TpAction("MT|H1|D1|D3", 跑圈点位_MT组[3], 3900);
            TpAction("ST|H2|D2|D4", 跑圈点位_ST组[3], 3900);
            
            TpAction("MT|H1|D1|D3", 跑圈点位_MT组[4], 5700);
            TpAction("ST|H2|D2|D4", 跑圈点位_ST组[4], 5700);
            return true;
        }

        
        return false;
    }
    private static async void TpAction(string regex, Vector3 partyPos, int delay)
    {
        await Task.Delay(delay);
        RemoteControlHelper.SetPos(regex,partyPos);
    }
}