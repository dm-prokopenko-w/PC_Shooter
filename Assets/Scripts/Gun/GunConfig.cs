using Game.Configs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gun
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Data/GunData", order = 0)]
    public class GunConfig : Config
    {
        public int Damage;
        public GunMesh Mesh;
        public Sprite Icon;
    }
}