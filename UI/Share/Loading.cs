using System;

using GFramework.UI;

using UnityEngine.UI;

namespace GFramework
{
    public class UILoading : BaseView<BaseViewModel>
    {
        Slider slider;
        Text tips;

        public override string BindPrefabPath()
        {
            return "";
        }

        protected override void BindVars()
        {
            throw new NotImplementedException();
        }
    }
}
