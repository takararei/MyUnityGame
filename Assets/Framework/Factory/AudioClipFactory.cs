using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Framework.Factory
{
    public class AudioClipFactory : BaseResourceFactory<AudioClip>
    {
        public AudioClipFactory()
        {
            LoadPath = "AudioClips/";
        }
    }

}