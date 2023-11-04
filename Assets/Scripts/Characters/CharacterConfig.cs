using Game.Configs;
using UnityEngine;

namespace Game.Character
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Data/CharacterData", order = 0)]
    public class CharacterConfig : Config
    {
        public int HP;
        public CharacterMesh Mesh;
    }
}