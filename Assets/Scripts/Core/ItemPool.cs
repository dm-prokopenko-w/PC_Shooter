using UnityEngine;

namespace Game
{
    public class ItemPool
    {
        public string Id;
        public GameObject Prefab;

        public ItemPool(string id, GameObject prefab)
        {
            Id = id;
            Prefab = prefab;
        }
    }
}