using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// FIXME: 全部协议存放在一个枚举里的方式显得臃肿
public enum ProtoDefine
{
    C2S_Login,
    C2S_Input,

    // 匹配相关
    C2S_CreateRoom,
    C2S_JoinOtherRoom,

    S2C_OtherJoinRoom,

    S2C_TICK_Update,
}