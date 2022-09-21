using GFramework;

using UnityEngine;
using UnityEngine.UI;

namespace Assets.GFramework.Core.Manager.Sound
{
    /// <summary>
    /// 音效类型
    /// </summary>
    enum UISoundType
    {
        // 按钮类型，点击播放音效
        Button,
        // 背景音乐类型，打开界面期间播放音效
        BGM,
        // 非按钮关闭界面类型，点击其他区域关闭界面播放音效
        Hide,
    }

    /// <summary>
    /// UI音效组件
    /// </summary>
    public class UISounder : MonoBehaviour
    {
        public AudioClip clip;

        [SerializeField] private UISoundType soundType = UISoundType.Button;

        private Button btn;
        private AudioSource source;


        private void Awake()
        {
            source = SoundMgr.Instance.GetAvailableSource();

            switch (soundType)
            {
                case UISoundType.Button:
                    source.loop = false;
                    btn.onClick.AddListener(OnBtnClick);
                    break;
                case UISoundType.BGM:
                    source.loop = true;
                    break;
                case UISoundType.Hide:
                    source.loop = false;
                    break;
            }
        }

        private void OnBtnClick()
        {
            if (source)
            {
                source.clip = clip;
                source.PlayOneShot(clip);
            }
        }

        private void OnEnable()
        {
            if (soundType == UISoundType.BGM)
            {
                source.clip = clip;

            }
        }

        private void OnDisable()
        {
            if (soundType == UISoundType.Hide)
            {
                source.clip = clip;
                source.PlayOneShot(clip);
            }
        }
    }
}
