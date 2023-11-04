using Game.Configs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Data/GunData", order = 0)]
    public class GunConfig : Config
    {
        public int HP;
        public CharacterMesh Mesh;
    }
}