using Assets.Framework.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Framework.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        private AudioListener mAudioListener;
        private AudioSource mBGMSource = null;
        private AudioSource mEffectSource = null;
        public GameObject Root;
        public bool playEffectMusic = true;
        public bool playBGMusic = true;

        public override void Init()
        {
            //base.Init();
            Root = GameRoot.Instance.gameObject;
            if (PlayerPrefs.GetInt(StringMgr.isEffectOff) == 1)
            {
                playEffectMusic = false;
            }
            if (PlayerPrefs.GetInt(StringMgr.isMusicOff) == 1)
            {
                playBGMusic = false;
            }
        }

        public void CheckEffectSource()
        {
            if (mEffectSource == null)
                mEffectSource = Root.AddComponent<AudioSource>();
        }
        public void CheckBGMSource()
        {
            if (mBGMSource == null)
                mBGMSource = Root.AddComponent<AudioSource>();
        }

        public void CheckAudioListener()
        {
            if (mAudioListener == null)
                mAudioListener = Root.AddComponent<AudioListener>();
        }

        //TODO
        public void BGMPlay(string bgmName, bool loop)
        {
            CheckAudioListener();
            CheckBGMSource();
            AudioClip bgm = Resources.Load<AudioClip>(bgmName);//TODO
            mBGMSource.clip = bgm;
            mBGMSource.loop = loop;
            mBGMSource.Play();
        }

        private void BGMPause()
        {
            mBGMSource.Pause();
        }

        private void BGMStop()
        {
            mBGMSource.Stop();
        }

        private void BGMUnPause()
        {
            mBGMSource.UnPause();
        }

        public void BGMOn()
        {
            mBGMSource.UnPause();
            mBGMSource.mute = false;
        }

        public void BGMOff()
        {
            mBGMSource.Pause();
            mBGMSource.mute = false;
        }

        public void PlayEffectMusic(AudioClip audioClip)
        {
            if (playEffectMusic)
            {
                mEffectSource.PlayOneShot(audioClip);
            }
        }

        public void CloseOrOpenBGMusic()
        {
            playBGMusic = !playBGMusic;
            SetMusicPrefs(playBGMusic);
            if (playBGMusic)
            {
                BGMOn();
            }
            else
            {
                BGMOff();
            }
        }

        public void CloseOrOpenEffectMusic()
        {
            playEffectMusic = !playEffectMusic;
            SetEffectPrefs(playEffectMusic);
        }

        private void SetMusicPrefs(bool isMusicPlay)
        {
            if (isMusicPlay)
            {
                PlayerPrefs.SetInt(StringMgr.isMusicOff, 0);
            }
            else
            {
                PlayerPrefs.SetInt(StringMgr.isMusicOff, 1);
            }
        }

        private void SetEffectPrefs(bool isEffectPlay)
        {
            if (isEffectPlay)
            {
                PlayerPrefs.SetInt(StringMgr.isEffectOff, 0);
            }
            else
            {
                PlayerPrefs.SetInt(StringMgr.isEffectOff, 1);
            }
        }


        
    }
}
