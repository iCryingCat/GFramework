using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// FIXME: 全部协议存放在一个枚举里的方式显得臃肿
public enum ProtoDefine
{
    C2S_Login = 1010,

    // 匹配
    C2S_Match = 1020,
    // 加入房间
    C2S_JoinOtherRoom = 1030,
    C2S_Tick_Input = 1040,

    // 加入房间
    S2C_OtherJoinRoom = 2010,
    // 帧同步更新
    S2C_Tick_Update = 2020,
    // 开始游戏
    S2C_Game_Start = 2030,
}