﻿using Assets.Framework.Factory;
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
        public bool isPlayEffectMusic = true;
        public bool isPlayBGMusic = true;

        public override void Init()
        {
            Root = GameRoot.Instance.gameObject;
            mAudioListener = Root.GetComponent<AudioListener>();
            mBGMSource = Root.GetComponents<AudioSource>()[0];
            mEffectSource = Root.GetComponents<AudioSource>()[1];
            CheckAudioListener();
            CheckEffectSource();
            CheckBGMSource();
            if (PlayerPrefs.GetInt(StringMgr.isEffectOff) == 1)
            {
                CloseOrOpenEffectMusic();
            }
            if (PlayerPrefs.GetInt(StringMgr.isMusicOff) == 1)
            {
                CloseOrOpenBGMusic();
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
        
        public void PlayBGM(string bgmName, bool loop=true)
        {
            if(isPlayBGMusic)
            {
                AudioClip bgm = FactoryManager.Instance.GetAudioClip(bgmName); 
                mBGMSource.clip = bgm;
                mBGMSource.loop = loop;
                mBGMSource.Play();
            }
           
        }
        public void PlayEffectMusic(string effectName)
        {
            if (isPlayEffectMusic)
            {
                AudioClip effect = FactoryManager.Instance.GetAudioClip(effectName);
                mEffectSource.PlayOneShot(effect);
            }
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

        
        public void CloseOrOpenBGMusic()
        {
            isPlayBGMusic = !isPlayBGMusic;
            SetMusicPrefs(isPlayBGMusic);
            if (isPlayBGMusic)
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
            isPlayEffectMusic = !isPlayEffectMusic;
            SetEffectPrefs(isPlayEffectMusic);
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
