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
    private List<KeyValuePair<string, Vector3>> 踩塔 = [];

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
            LogHelper.Print($"中塔{count}人");
        }
        else
        {
            if (pos.Z - 100 > 1)
            {
                P1塔[2] = count;
                LogHelper.Print($"下塔{count}人");
            }
            else
            {
                P1塔[0] = count;
                LogHelper.Print($"上塔{count}人");
            }
        }
        if (pos.X - 100 > 1)
        {
            P1塔[3] = 1;
            LogHelper.Print($"右塔");
        }
        LogHelper.Print(P1塔.Print());
        if (_times != 3) return false;
        if (!scriptEnv.KV.ContainsKey("P1塔雷")) return false;

        var lei = (bool)scriptEnv.KV["P1塔雷"];
        var eastTower = P1塔[3] == 1;
        var T预站位 = new Vector3(eastTower ? 94.5f : 105.5f, 0, 100);
        var 近战D预站位 = new Vector3(!eastTower ? 94.5f : 105.5f, 0, 100);
        var 踩塔1 = new Vector3(eastTower ? 113.08f : 86.92f, 0, 90.81f);
        var 踩塔2 = new Vector3(eastTower ? 115.98f : 84.02f, 0, 100f);
        var 踩塔3 = new Vector3(eastTower ? 113.08f : 86.92f, 0, 109.18f);
        var 击退位1 = new Vector3(eastTower ? 102f : 98f, 0, 90.81f);
        var 击退位2 = new Vector3(eastTower ? 102f : 98f, 0, 100f);
        var 击退位3 = new Vector3(eastTower ? 102f : 98f, 0, 109.18f);
        if (lei)//雷
        {
            
            //近战预站位
            RemoteControlHelper.SetPos("MT", T预站位);
            RemoteControlHelper.SetPos("ST", T预站位);
            RemoteControlHelper.SetPos("D1", 近战D预站位);
            RemoteControlHelper.SetPos("D2", 近战D预站位);
            RemoteControlHelper.SetPos("D3", 近战D预站位);
            
            //远程一步到位
            RemoteControlHelper.SetPos("H1", 踩塔1);
            RemoteControlHelper.SetPos("H2", 踩塔2);
            RemoteControlHelper.SetPos("D4", 踩塔3);
    
    
            for (int i = 0; i < P1塔.Count-1; i++)
            {
                if (P1塔[i] >= 2)
                {
                    踩塔.Add(new ("D" + (i + 1), i switch{0=>踩塔1, 1=>踩塔2, 2=>踩塔3, _=>new Vector3()}));
                }
                else
                {
                    if (P1塔[i] >= 3)
                    {
                        踩塔.Add(new ("D" + (i + 1), i switch{0=>踩塔1, 1=>踩塔2, 2=>踩塔3, _=>new Vector3()}));
                    }
                }
            }
            
        }
        else
        {
            RemoteControlHelper.SetPos("MT", T预站位);
            RemoteControlHelper.SetPos("ST", T预站位);
            RemoteControlHelper.SetPos("D1", 近战D预站位);
            RemoteControlHelper.SetPos("D2", 近战D预站位);
            RemoteControlHelper.SetPos("D3", 近战D预站位);
            
            //添加双T击退位
            踩塔.Add(new ("MT", new (eastTower ? 98 : 102, 0, 94.5f)));
            踩塔.Add(new ("ST", new(eastTower ? 98 : 102, 0, 105.5f)));
            
            //H1,H2, D4预站位
            RemoteControlHelper.SetPos("H1", new(eastTower ? 105.5f : 94.5f, 0, 90.81f));
            RemoteControlHelper.SetPos("H2", new(eastTower ? 105.5f : 94.5f, 0, 100f));
            RemoteControlHelper.SetPos("D4", new(eastTower ? 105.5f : 94.5f, 0, 109.18f));
            
            //添加H1,H2,D4击退位
            踩塔.Add(new ("H1", 击退位1));
            踩塔.Add(new ("H2", 击退位2));
            踩塔.Add(new ("D4", 击退位3));
            
            for (int i = 0; i < P1塔.Count; i++)
            {
                if (P1塔[i] >= 2)
                {
                    踩塔.Add(new ("D" + (i + 1), i switch{0=>击退位1, 1=>击退位2, 2=>击退位3, _=>new Vector3()}));
                }
                else
                {
                    if (P1塔[i==0?1:0] >= 3)
                    {
                        踩塔.Add(new ("D" + (i + 1), i switch{0=>击退位1, 1=>击退位2, 2=>击退位3, _=>new Vector3()}));
                    }

                    if (P1塔[i == 2 ? 1 : 2] >= 3)
                    {
                        踩塔.Add(new ("D" + (i + 1), i switch{0=>击退位1, 1=>击退位2, 2=>击退位3, _=>new Vector3()}));    
                    }
                }
            }
            
        }
        if(!scriptEnv.KV.ContainsKey("P1踩塔")) scriptEnv.KV.Add("P1踩塔", 踩塔);
        return true;
        if(!scriptEnv.KV.ContainsKey("P1塔")) scriptEnv.KV.Add("P1塔", P1塔);
        return true;
    }
}