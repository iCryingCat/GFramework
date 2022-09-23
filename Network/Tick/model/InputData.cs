using System.Collections.Generic;
using Lockstep.Math;
using ProtoBuf;
using UnityEngine;

namespace GFramework.Network
{
    [ProtoContract]
    public class InputData
    {
        public int unitID = 0;
        public Dictionary<InputCode, LFloat> floatInput = new Dictionary<InputCode, LFloat>();
        public Dictionary<InputCode, bool> boolInput = new Dictionary<InputCode, bool>();
        public Dictionary<InputCode, LVector2> vector2Input = new Dictionary<InputCode, LVector2>();
    }
}