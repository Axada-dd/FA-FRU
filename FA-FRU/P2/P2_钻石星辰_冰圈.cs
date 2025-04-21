using System.Numerics;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using DDDacr.工具;

namespace FA_FRU.P2;

public class P2_钻石星辰_冰圈 : ITriggerScript
{
    public List<int> 冰圈index = [];
    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is not EnemyCastSpellCondParams spellCondParams) return false;
        if (spellCondParams.SpellId != 40198) return false;
        冰圈index.Add(坐标计算.PositionTo8Dir(spellCondParams.CastPos,new Vector3(100,0,100)));
        if (冰圈index.Count < 4) return false;
        if(!scriptEnv.KV.ContainsKey("冰圈index"))scriptEnv.KV.Add("冰圈index",冰圈index);
        return true;
    }
}