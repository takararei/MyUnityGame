using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Framework.Factory
{
    public class SpriteFactory : BaseResourceFactory<Sprite>
    {
        public SpriteFactory()
        {
            LoadPath = "Pictures/";
        }
    }

}