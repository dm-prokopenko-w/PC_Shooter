using UnityEngine;

namespace Game.Core
{
    public class ItemPool
    {
        public string Id;
        public GameObject Prefab;
        public Animator Anim;
        public Transform StartShootPoint;

        public ItemPool(string id, GameObject prefab, Transform point)
        {
            Id = id;
            Prefab = prefab;
            StartShootPoint = point;
        }

        public ItemPool(string id, GameObject prefab, Animator anim)
        {
            Id = id;
            Prefab = prefab;
            Anim = anim;
        }
    }
}