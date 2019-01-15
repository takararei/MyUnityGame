using Assets.Framework.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Framework.Audio
{
    public class AudioManager:Singleton<AudioManager>
    {
        private AudioListener mAudioListener;
        private AudioSource mBGMSource = null;
        public GameObject Root;
        public void CheckBGMSource()
        {
            if(mBGMSource==null)
                mBGMSource = Root.AddComponent<AudioSource>();
        }

        public void CheckAudioListener()
        {
            if (mAudioListener == null)
                mAudioListener = Root.AddComponent<AudioListener>();
        }
        
        //TODO
        public void BGMPlay(string bgmName,bool loop)
        {
            CheckAudioListener();
            CheckBGMSource();
            AudioClip bgm= Resources.Load<AudioClip>(bgmName);//TODO
            mBGMSource.clip = bgm;
            mBGMSource.loop = loop;
            mBGMSource.Play();
        }

        public void BGMPause()
        {
            mBGMSource.Pause();
        }

        public void BGMStop()
        {
            mBGMSource.Stop();
        }

        public void BGMUnPause()
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
    }
}
