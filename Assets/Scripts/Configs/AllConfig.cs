using Game.Character;
using Game.Configs;
using Game.Gun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "AllConfig", menuName = "Data/AllConfigData", order = 0)]
    public class AllConfig : Config
    {
        public CharacterView CharacterPrefab;
        public List<CharacterConfig> Characters;
        public List<GunConfig> Guns;
        [Space]
        [Range(1, 8)] public int CountSpawners;
        public CharactersParm CharParm;
    }

    [Serializable] 
    public class CharactersParm
    {
        [Range(1, 5)] public int SpeedWalk;
        [Range(1, 5)] public int SpeedRun;
        [Range(1, 5)] public int JumpHeight;
        [Range(1, 100)] public int MouseSensitivity;
        public LayerMask GroundMask;
    }
}