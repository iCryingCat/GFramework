using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace GFramework
{
    public enum BgmID
    {

    }

    public enum SoundID
    {

    }

    /// <summary>
    /// ��Ч������
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class SoundMgr : DntdMonoSingleton<SoundMgr>
    {
        private Dictionary<BgmID, AudioClip> bgmMap = new Dictionary<BgmID, AudioClip>();
        private Dictionary<SoundID, AudioClip> soundMap = new Dictionary<SoundID, AudioClip>();

        private Dictionary<SoundID, AudioSource> audioMap = new Dictionary<SoundID, AudioSource>();

        private AudioSource audioSource;
        private bool isSlient = false;

        public bool IsSlient { get => isSlient; set => isSlient = value; }

        private void Awake()
        {
            if (!TryGetComponent<AudioSource>(out audioSource))
                audioSource = gameObject.AddComponent<AudioSource>();
        }

        public void Initialize()
        {
            // �����ñ�
        }

        private void LoadAsset()
        {
            AudioClip[] audioClips = Resources.LoadAll<AudioClip>(Constant.Path_Res_Bgm);
            foreach (AudioClip clip in audioClips)
            {
                bgmMap.Add((BgmID)Enum.Parse(typeof(BgmID), clip.name), clip);
            }
        }

        public void PlayBgm(BgmID scene, bool loop = true)
        {
            if (!bgmMap.ContainsKey(scene))
            {
                Debug.LogWarning(string.Format("{0} Bgms does not exist in the bgmDict", name));
                return;
            }
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
            audioSource.clip = bgmMap[scene];
            audioSource.loop = loop;
            audioSource.Play();
        }

        public AudioSource GetAvailableSource()
        {
            return new AudioSource();
        }

        public void Play3DEffect(SoundID sound, Vector3 position)
        {
            if (!soundMap.ContainsKey(sound))
            {
                Debug.LogWarning(string.Format("{0} Bgms does not exist in the bgmDict", name));
                return;
            }
            AudioSource source = null;
            if (audioMap.ContainsKey(sound))
            {
                source = audioMap[sound];
            }
            else
            {
                GameObject go = new GameObject(sound.ToString());
                go.transform.position = position;
                go.transform.SetParent(transform);
                source = go.AddComponent<AudioSource>();
                source.volume = audioSource.volume;
                audioMap.Add(sound, source);
            }
            source.clip = soundMap[sound];
            source.loop = false;
            source.PlayOneShot(soundMap[sound]);
        }

        public void StopBgm()
        {
            audioSource.Stop();
        }

        public void SetVolume(float value)
        {
            audioSource.volume = value;
            if (Mathf.Approximately(value, 0f))
            {
                isSlient = true;
            }
            else
            {
                isSlient = false;
            }
            foreach (AudioSource source in audioMap.Values)
            {
                source.volume = value;
            }
        }
    }
}