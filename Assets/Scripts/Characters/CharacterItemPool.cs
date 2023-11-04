using UnityEngine;

namespace Game.Character
{
    public class CharacterItemPool
    {
        public string Id;
        public GameObject Prefab;

        public CharacterItemPool(string id, GameObject prefab)
        {
            Id = id;
            Prefab = prefab;
        }
    }
}