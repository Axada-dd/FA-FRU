using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using DDDacr.工具;
using ECommons.GameFunctions;


namespace FA_FRU;

public class AE_Script1 : ITriggerScript
{

    public bool Check(ScriptEnv scriptEnv, ITriggerCondParams condParams)
    {
        if (condParams is TetherCondParams tetherCondParams)
        {
            if (tetherCondParams.Args0 == 249)
            {
                LogHelper.Print("火");
            }

            if (tetherCondParams.Args0 == 287)
            {
               LogHelper.Print("雷"); 
            }    
        }


        
        return false;
    }

}