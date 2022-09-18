using System;

namespace GFramework.Extern
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    sealed class UIAttr : Attribute
    {
        public bool isSingleton = false;
    }
}