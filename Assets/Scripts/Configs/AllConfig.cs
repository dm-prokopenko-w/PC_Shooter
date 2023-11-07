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
        [Range(1, 8)] public int CountSpawners = 8;
        public CharactersParametrs CharactersParm;
        public PlayerParametrs PlayerParm;
        public EnemyParametrs EnemyParm;
        public BulletParm BulParm;
        public EffectParametrs EffectParm;
    }

    [Serializable] 
    public class CharactersParametrs
    {
        [Range(1, 5)] public int SpeedWalk = 3;
        [Range(1, 5)] public int SpeedRun = 5;
        [Range(1, 5)] public int JumpHeight = 3;
        public LayerMask GroundMask = 6;
    }

    [Serializable] 
    public class PlayerParametrs
    {
        [Range(1, 100)] public int MouseSensitivity = 30;
        [Range(1, 100)] public int BulletOnStart = 3;
    }

    [Serializable] 
    public class EnemyParametrs
    {
        [Range(1, 100)] public float ViewAngel = 90f;
        [Range(1, 100)] public float DetectionDistance = 10f;
        [Range(1, 10)] public float AwaitSecBeforeShoot = 2f;
        [Range(0.1f, 1f)] public float RotationSpeed = 0.1f;
    }

    [Serializable] 
    public class BulletParm
    {
        [Range(1, 50)] public int MinSecRespawn = 3;
        [Range(1, 50)] public int MaxSecRespawn = 5;
        [Range(1, 100)] public int CountAddedBullet = 10;
    }

    [Serializable] 
    public class EffectParametrs
    {
        public GameObject ShootPrefab;
        public GameObject BloodPrefab;
        public GameObject DiedPrefab;
    }
}