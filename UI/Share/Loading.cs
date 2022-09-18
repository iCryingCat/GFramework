/*-------------------------------------------------------------------------
 * 作者：@白泽
 * 联系方式：xzjH5263@163.com
 * 创建时间：2022/7/16 17:28:06
 * 描述：
 *  -------------------------------------------------------------------------*/

using GFramework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
