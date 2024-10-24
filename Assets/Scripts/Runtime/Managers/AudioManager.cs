using UnityEngine;
namespace Managers
{
    public class AudioManager : TGameManager<AudioManager>
    {
        private AudioSource _bgmSource;

        protected override void OnAwake()
        {
            base.OnAwake();
            var audioSourceObj = new GameObject("AudioSource");
            Object.DontDestroyOnLoad(audioSourceObj);
            _bgmSource = audioSourceObj.AddComponent<AudioSource>();
        }
        
        /// <summary>
        /// 播放背景音
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isLoop"></param>
        public void PlayBgm(string name, bool isLoop = true)
        {
            //加载bgm声音剪辑
            AudioClip clip = Resources.Load<AudioClip>("Sounds/BGM/" + name);
            _bgmSource.clip = clip;//设置音频
            _bgmSource.loop = isLoop;//是否循环
            _bgmSource.Play();
        }


        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="name"></param>
        /// <param name="position"></param>
        public void PlayEffectAudio(string name, Vector3 position)
        {
            AudioClip clip = Resources.Load<AudioClip>("Sounds/" + name);
            AudioSource.PlayClipAtPoint(clip, position);
        }
    }
}
